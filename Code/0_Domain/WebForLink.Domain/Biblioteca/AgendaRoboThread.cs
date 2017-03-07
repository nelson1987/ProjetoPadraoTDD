using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;
using WebForDocs.Data.ModeloDB;
using WebForDocs.Enum;

namespace WebForDocs.Biblioteca
{
    public static class AgendaRoboThread
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool Para_Thread { get; set; }

        public static AgendaRobo Agenda = new AgendaRobo();

        public static string path { get; set; }

        //public static void Principal()
        //{
        //    while (!Para_Thread)
        //    {
        //        if (Agenda.Contratantes == null)
        //        {
        //            Agenda = CarregaAgendaXml();
        //        }
        //        else
        //        {
        //            DateTime UmaHoraDepois = Agenda.DataCarga.AddHours(1);
        //            if (UmaHoraDepois < DateTime.Now)
        //            {
        //                // Busca o arquivo xml depois de 1 hora
        //                Agenda = CarregaAgendaXml();
        //            }
        //            //Todo Através do objeto Agenda, buscar as solicitações e ir chamando os robos para serem executados.
        //            // Usar o método BuscarSolicitacoes()
        //            BuscarSolicitacoes(Agenda);
        //        }

        //        Thread.Sleep(40000);

        //        //BuscarRoboPendente(1);
        //        //Thread.Sleep(120000);
        //    }
        //}

        public static void Principal()
        {
            BuscarSolicitacoes(Agenda);
        }

        private static void BuscarSolicitacoes(AgendaRobo agenda)
        {
            foreach (var item in agenda.Contratantes)
            {
                foreach (var hora in item.hora)
                {
                    if (VerificarHorario(hora))
                    {
                        BuscarRoboPendente(item.id);
                    }
                }
            }
        }

        private static void BuscarRoboPendente(int idContratante)
        {
            //TODO
            //Buscar todas as solicitações com status = 5 e tramite = 3           
            WFDModel db = new WFDModel();
            int? tentativas = db.WFD_CONTRATANTE_CONFIG.SingleOrDefault(c => c.CONTRATANTE_ID == idContratante).TOTAL_TENTATIVA_ROBO;
            if (!tentativas.HasValue) tentativas = int.MaxValue;

            try
            {
                if (idContratante != 0)
                {
                    int[] tpFluxoids = { 10, 20, 30, 40 };
                    List<WFD_SOLICITACAO> lstSolicitacao = db.WFD_SOLICITACAO
                        .Include("WFD_SOLICITACAO_STATUS")
                        .Include("WFD_SOL_CAD_PJPF")
                        .Include("WFD_PJPF_ROBO")
                        .Include("WFD_CONTRATANTE")
                        .Where(x =>
                            x.CONTRATANTE_ID == idContratante &&
                            x.SOLICITACAO_STATUS_ID == 5 &&
                            tpFluxoids.Contains(x.WFL_FLUXO.FLUXO_TP_ID) &&
                            x.WFD_PJPF_ROBO.Any(c => c.RF_CONSULTA_DTHR == null || c.SINT_CONSULTA_DTHR == null || c.SN_CONSULTA_DTHR == null) &&
                            x.WFD_PJPF_ROBO.Any(c => c.RF_CONTADOR_TENTATIVA <= tentativas || c.SINT_CONTADOR_TENTATIVA <= tentativas || c.SN_CONTADOR_TENTATIVA <= tentativas) &&
                            x.WFD_PJPF_ROBO.Any(c => c.RF_CODE_ROBO == null || c.SINT_CODE_ROBO == null || c.SN_CODE_ROBO == null)
                        ).ToList();

                    if (lstSolicitacao != null && lstSolicitacao.Any())
                        ExecutaRobo(db, lstSolicitacao, (int)tentativas);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error ao tentar buscar os fornecedores pendentes do robos.", ex);
            }
        }

        static async void ExecutaRobo(WFDModel db, List<WFD_SOLICITACAO> lstFornecedorRobo, int Tentativa)
        {
            
            // Todo
            // Verificar na lista se o robo da receita Federal foi executado com sucesso, através do RF_CONSULTA_DTHR. Caso esteja nulo não foi executado. Sendo assim vai executar a chamada no robo do Lanna. Essa ´lógica serve para o Sintegra e Simples
            // Se obtiver sucesso na consulta do robo, será gravado os dados na tabela, referente ao robo. Se for falha, vai gravar no COUNT da tabela
            // Além de gravar na tabela de WFD_PJPF_ROBO, tbm terá que atualizar o Tramite, em caso de sucesso daquele robo
            // Verificar em qual momento será enviado email e para quem será esse envio.
            // verificar como será a regra feita em cima das tentativas ao robo.

            RoboReceitaCNPJ roboReceita = new RoboReceitaCNPJ();
            RoboSintegra roboSintegra = new RoboSintegra();
            RoboSimples roboSimples = new RoboSimples();
            RoboReceitaCPF roboReceitaCpf = new RoboReceitaCPF();

            try
            {
                foreach (var item in lstFornecedorRobo)
                {
                    if (item.WFD_SOL_CAD_PJPF.LastOrDefault().PJPF_TIPO == 1)
                    {
                        
                        if (item.WFD_PJPF_ROBO.First().RF_CONSULTA_DTHR == null && item.WFD_PJPF_ROBO.First().RF_CONTADOR_TENTATIVA <= Tentativa)
                        {
                            // Chamada Robo da Receita Federal
                            // TODO chamado do ROBO da receita federal ASYNC
                            roboReceita = await Task.Run(() => roboReceita.CarregaRoboCNPJ(item.WFD_SOL_CAD_PJPF.FirstOrDefault().CNPJ, path));

                            // TODO chama o método de gravar no ROBO ASYNC  
                            await Task.Run(() => GravaRoboReceita(db, roboReceita, item));
                        }


                        // TODO verificar se já tem DTHR no robo da Receita
                        // Chamada Robo Sintegra
                        if (item.WFD_PJPF_ROBO.First().SINT_CONSULTA_DTHR == null && item.WFD_PJPF_ROBO.First().SINT_CONTADOR_TENTATIVA <= Tentativa)
                        {
                            // TODO chamado do ROBO Sintegra ASYNC
                            roboSintegra = await Task.Run(() => roboSintegra.CarregaSintegra(item.WFD_SOL_CAD_PJPF.FirstOrDefault().UF, item.WFD_SOL_CAD_PJPF.FirstOrDefault().CNPJ, path));

                            // TODO chama o método de gravar no ROBO ASYNC
                            await Task.Run(() => GravaRoboSintegra(db, roboSintegra, item));
                        }


                        // Chamada Robo Simples Nacional
                        if (item.WFD_PJPF_ROBO.First().SN_CONSULTA_DTHR == null && item.WFD_PJPF_ROBO.First().SN_CONTADOR_TENTATIVA <= Tentativa)
                        {
                            // TODO chamado do ROBO Simples Nacional ASYNC
                            roboSimples = await Task.Run(() => roboSimples.CarregaSimplesCNPJ(item.WFD_SOL_CAD_PJPF.FirstOrDefault().CNPJ, path));
                            // TODO chama o método de gravar no ROBO ASYNC
                            await Task.Run(() => GravaRoboSimples(db, roboSimples, item));
                        }
                    }

                    // Pessoa Fisica
                    if (item.WFD_SOL_CAD_PJPF.LastOrDefault().PJPF_TIPO == 3)
                    {
                        if (item.WFD_PJPF_ROBO.First().RF_CONSULTA_DTHR == null && item.WFD_PJPF_ROBO.First().RF_CONTADOR_TENTATIVA <= Tentativa)
                        {
                            // Chamada Robo da Receita Federal
                            // TODO chamado do ROBO da receita federal ASYNC
                            roboReceitaCpf = await Task.Run(() => roboReceitaCpf.CarregaRoboCPF(item.WFD_SOL_CAD_PJPF.FirstOrDefault().CPF, item.WFD_SOL_CAD_PJPF.FirstOrDefault().DT_NASCIMENTO.ToString(), path));
                                
                            // TODO chama o método de gravar no ROBO ASYNC  
                            await Task.Run(() => GravaRoboReceitaCpf(db, roboReceitaCpf, item));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error ao percorrer a lista 'lstFornecedorRobo' exception: {0}", ex));
            }
        }

        static void GravaRoboReceita(WFDModel db, RoboReceitaCNPJ roboReceita, WFD_SOLICITACAO solicitacao)
        {            
            string emailRobo = ConfigurationManager.AppSettings["EmailRobo"];

            /***             
            1- Grava na tabela WFD_PJPF_robo com os dados referente ao robo da receita
            2- Atualiza o Tramite
            3- Envia e-mail
            4- Grava na tabela de WFD_PJPF_log
             * se cod = 1 então zera o contador, senão acrescenta o count
             ** Se count for igual a 7 envia email
             *** Se count for igual a 10 mandar email (solicitante e ch)
            5- O tramite só será atualizado quando rodar o robo da receita federal
            ***/

            // 1-
            if (roboReceita != null && roboReceita.Code == 1)
            {
                try
                {
                    //db.Entry(solicitacao.WFD_PJPF_ROBO).Reload();
                    //WFD_PJPF_ROBO fornecedorRobo = solicitacao.WFD_PJPF_ROBO.First();

                    if (solicitacao.WFD_PJPF_ROBO.Count > 0)
                    {
                        RoboReceitaCNPJ roboReceitaCnpj = new RoboReceitaCNPJ();
                        bool gravaDados = roboReceitaCnpj.GravaRoboReceita(db, roboReceita, solicitacao);
                        if (gravaDados)
                        {
                            //Todo

                            //Atualização do Tramite 
                            //Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 2, 2, null);

                            // ????? TODO 
                                // -----> verificar se vai gravar os dados na FOR_SOL_CAD_FORN <-----

                            GravaFornecedorDadosRoboReceitaFederal(db, solicitacao, roboReceita);
                            //Db.SaveChanges();

                            // ????? TODO 
                                // -----> Verificar o envio de email para o fornecedor, com o link para ele acessar a ficha <-----

                            //Envio de email
                            //Geral.EnviarEmail(emailRobo, "Robo Receita Federal", string.Format("Robo Receita Federal de solicitação id {0} gravado com sucesso. Situação: {1}", solicitacao.ID, roboReceita.Data.SituacaoCadastral.ToUpper()));
                        }
                    }
                }
                catch (Exception ex)
                {
                    //TODO verificar se vai remover os dados que foi inserido/modificado
                    Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
                    Geral.EnviarEmail(emailRobo, "Receita CNPJ", string.Format("Robo Receita CNPJ de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
                }
            }

            // ------> BAIXADO NA RECEITA <-------
            //if (roboReceita != null && roboReceita.Code == 1 && roboReceita.Data.SituacaoCadastral.ToUpper() == "BAIXADA")
            //{
            //    try
            //    {
            //        WFD_PJPF_ROBO fornecedorRobo = solicitacao.WFD_PJPF_ROBO.First();

            //        if (fornecedorRobo != null)
            //        {
            //            RoboReceitaCNPJ roboReceitaCnpj = new RoboReceitaCNPJ();
            //            bool gravaDados = roboReceitaCnpj.GravaRoboReceita(db, roboReceita, solicitacao);
            //            if (gravaDados)
            //            {
            //                //Todo

            //                //Atualização do Tramite
            //                //Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 2, 3, null);
            //                //solicitacao.SOLICITACAO_STATUS_ID = 3;
            //                //db.Entry(solicitacao).State = System.Data.Entity.EntityState.Modified;
            //                //Db.SaveChanges();

            //                //Envio de email
            //                Geral.EnviarEmail(emailRobo, "Robo Receita Federal", string.Format("Robo Receita Federal de solicitação id {0} gravado com sucesso e BAIXADO NA RECEITA.", solicitacao.ID));
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
            //        Geral.EnviarEmail(emailRobo, "Robo Sintegra", string.Format("Robo Sintegra de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
            //    }
            //}

            // Ocorreu error ao tentar pegar os dados do fornecedor na Receita Federal
            if (roboReceita != null && roboReceita.Code != 1)
            {
                //int? roboId = solicitacao.WFD_PJPF_ROBO.FirstOrDefault().ID;
                try
                {
                    WFD_PJPF_ROBO fr = solicitacao.WFD_PJPF_ROBO.First();
                    int? contador = fr.RF_CONTADOR_TENTATIVA;
                    if (!contador.HasValue) contador = 0;

                    if (roboReceita.Code == 2 || roboReceita.Code == 3)
                    {
                        //Atualiza Tramite
                        //Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 2, 3, null);
                        //solicitacao.SOLICITACAO_STATUS_ID = 3;
                        //db.Entry(solicitacao).State = System.Data.Entity.EntityState.Modified;
                        //db.SaveChanges();

                        fr.RF_CODE_ROBO = roboReceita.Code;
                        fr.RF_SIT_CADASTRAL_CNPJ = roboReceita.Data.Message;
                        db.Entry(fr).State = System.Data.Entity.EntityState.Modified;

                        //Envio de email
                        //Geral.EnviarEmail(emailRobo, "Robo Receita Federal", string.Format("Robo Receita Federal de solicitação id {0} COM ERROR na consulta do webservice código retornado {1}.", solicitacao.ID, roboReceita.Code));
                    }
                    else
                    {
                        contador += 1;
                        fr.RF_CONTADOR_TENTATIVA = contador;
                        db.Entry(fr).State = System.Data.Entity.EntityState.Modified;

                        Geral.EnviarEmail(emailRobo, "Receita CNPJ", string.Format("Robo Receita CNPJ de solicitação id {0} Gravada no banco com ", solicitacao.ID, roboReceita.Code));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
                    Geral.EnviarEmail(emailRobo, "Receita CNPJ", string.Format("Robo Receita CNPJ de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
                }
            }

            WFD_PJPF_ROBO_LOG entityLog = new WFD_PJPF_ROBO_LOG()
            {
                COD_RETORNO = roboReceita.Code,
                DATA = DateTime.Now,
                MENSAGEM = roboReceita.Data.Message,
                ROBO = EnumRobo.ReceitaFederal.ToString(),
                SOLICITACAO_ID = solicitacao.ID
            };

            db.Entry(entityLog).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }

        static void GravaRoboReceitaCpf(WFDModel db, RoboReceitaCPF roboReceitaCpf, WFD_SOLICITACAO solicitacao)
        {
            string emailRobo = ConfigurationManager.AppSettings["EmailRobo"];

            if (roboReceitaCpf != null && roboReceitaCpf.Code == 1)
            {
                try
                {
                    db.Entry(solicitacao.WFD_PJPF_ROBO).Reload();
                    WFD_PJPF_ROBO fornecedorRobo = solicitacao.WFD_PJPF_ROBO.FirstOrDefault();

                    if (fornecedorRobo != null)
                    {
                        RoboReceitaCPF roboCpf = new RoboReceitaCPF();
                        bool gravaDados = roboCpf.GravaRoboCpf(db, roboReceitaCpf, solicitacao);
                        if (gravaDados)
                        {
                            //Todo

                            //Atualização do Tramite 
                            //Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 2, 2, null);

                            // ????? TODO 
                            // -----> verificar se vai gravar os dados na FOR_SOL_CAD_FORN <-----

                            GravaFornecedorDadosRoboReceitaFederalPF(db, solicitacao, roboReceitaCpf);
                            //Db.SaveChanges();

                            // ????? TODO 
                            // -----> Verificar o envio de email para o fornecedor, com o link para ele acessar a ficha <-----

                            //Envio de email
                            //Geral.EnviarEmail(emailRobo, "Robo Receita Federal", string.Format("Robo Receita Federal de solicitação id {0} gravado com sucesso", solicitacao.ID));
                        }
                    }
                }
                catch (Exception ex)
                {
                    //TODO verificar se vai remover os dados que foi inserido/modificado
                    Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
                    Geral.EnviarEmail(emailRobo, "Robo Receita CPF", string.Format("Robo Receita CPF de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
                }
            }

            //// ------> BAIXADO NA RECEITA <-------
            //if (roboReceitaCpf != null && roboReceitaCpf.Code == 1 && roboReceitaCpf.Data.SituacaoCadastral.ToUpper() != "REGULAR")
            //{
            //    try
            //    {
            //        WFD_PJPF_ROBO fornecedorRobo = solicitacao.WFD_SOL_CAD_PJPF.FirstOrDefault().WFD_PJPF_ROBO;

            //        if (fornecedorRobo != null)
            //        {
            //            RoboReceitaCPF roboCpf = new RoboReceitaCPF();
            //            bool gravaDados = roboCpf.GravaRoboCpf(db, roboReceitaCpf, solicitacao);
            //            if (gravaDados)
            //            {
            //                //Todo

            //                //Atualização do Tramite
            //                Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 2, 3, null);
            //                solicitacao.SOLICITACAO_STATUS_ID = 3;
            //                db.Entry(solicitacao).State = System.Data.Entity.EntityState.Modified;
            //                //Db.SaveChanges();

            //                //Envio de email
            //                Geral.EnviarEmail(emailRobo, "Robo Receita Federal Pessoa Fisica", string.Format("Robo Receita Federal Pessoa Fisica de solicitação id {0} gravado com sucesso e BAIXADO NA RCEITA.", solicitacao.ID));
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
            //        Geral.EnviarEmail(emailRobo, "Robo Pessoa Fisica", string.Format("Robo Pessoa Fisica de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
            //    }
            //}

            // Ocorreu error ao tentar pegar os dados do fornecedor na Receita Federal
            if (roboReceitaCpf != null && roboReceitaCpf.Code != 1)
            {
                int? roboId = solicitacao.WFD_PJPF_ROBO.FirstOrDefault().ID;
                try
                {
                    WFD_PJPF_ROBO fr = solicitacao.WFD_PJPF_ROBO.FirstOrDefault();
                    int? contador = fr.RF_CONTADOR_TENTATIVA;
                    if (!contador.HasValue) contador = 0;
                    if (roboReceitaCpf.Code == 2 || roboReceitaCpf.Code == 3)
                    {
                        //Atualiza Tramite
                        //Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 2, 3, null);
                        //solicitacao.SOLICITACAO_STATUS_ID = 3;
                        //db.Entry(solicitacao).State = System.Data.Entity.EntityState.Modified;
                        //Db.SaveChanges();

                        fr.RF_CODE_ROBO = roboReceitaCpf.Code;
                        fr.RF_SIT_CADASTRAL_CNPJ = roboReceitaCpf.Data.Message;
                        db.Entry(fr).State = System.Data.Entity.EntityState.Modified;

                        //Envio de email
                        //Geral.EnviarEmail(emailRobo, "Robo Receita Federal Pessoa Fisica", string.Format("Robo Receita Federal Pessoa Física de solicitação id {0} COM ERROR na consulta do webservice código retornado {1}.", solicitacao.ID, roboReceitaCpf.Code));
                    }
                    else
                    {
                        contador += 1;
                        fr.RF_CONTADOR_TENTATIVA = contador;
                        db.Entry(fr).State = System.Data.Entity.EntityState.Modified;
                        //Db.SaveChanges();

                        //Geral.EnviarEmail(emailRobo, "Robo Receita CPF", string.Format("Robo Receita CPF de solicitação id {0} Gravada no banco com ", solicitacao.ID, roboReceitaCpf.Code));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
                    Geral.EnviarEmail(emailRobo, "Robo Receita CPF", string.Format("Robo Receita CPF de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
                }
            }

            WFD_PJPF_ROBO_LOG entityLog = new WFD_PJPF_ROBO_LOG()
            {
                COD_RETORNO = roboReceitaCpf.Code,
                DATA = DateTime.Now,
                MENSAGEM = roboReceitaCpf.Data.Message,
                ROBO = EnumRobo.ReceitaFederalPF.ToString(),
                SOLICITACAO_ID = solicitacao.ID
            };

            db.Entry(entityLog).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }

        static void GravaRoboSintegra(WFDModel db, RoboSintegra roboSintegra, WFD_SOLICITACAO solicitacao)
        {
            string emailRobo = ConfigurationManager.AppSettings["EmailRobo"];

            // Grava na tabela WFD_PJPF_robo 
            // Grava na tabela de WFD_PJPF_robo_log

            if (roboSintegra != null && roboSintegra.Code == 1)
            {
                try
                {
                    if (solicitacao.WFD_PJPF_ROBO.Count > 0)
                    {
                        RoboSintegra roboSintegraObj = new RoboSintegra();
                        bool gravaDados = roboSintegraObj.GravaRoboSintegra(db, roboSintegra, solicitacao);
                        if (gravaDados)
                        {
                            //Todo
                            
                            //Atualização do Tramite
                            //Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 14, 2, null);

                            //Envio de email
                            //Geral.EnviarEmail(emailRobo, "Robo Sintegra", string.Format("Robo Sintegra de solicitação id {0} gravado com sucesso", solicitacao.ID));
                        }
                    }
                }
                catch (Exception ex)
                {
                    //TODO verificar se vai remover os dados que foi inserido/modificado
                    Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
                    Geral.EnviarEmail(emailRobo, "Robo Sintegra", string.Format("Robo Sintegra de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
                }
            }

            if (roboSintegra != null && roboSintegra.Code != 1)
            {
                //int? roboId = solicitacao.WFD_PJPF_ROBO.FirstOrDefault().ID;
                try
                {
                    WFD_PJPF_ROBO fr = solicitacao.WFD_PJPF_ROBO.FirstOrDefault();
                    int? contador = fr.SINT_CONTADOR_TENTATIVA;
                    if (!contador.HasValue) contador = 0;

                    if (roboSintegra.Code == 2 || roboSintegra.Code == 3)
                    {
                        //Atualiza Tramite
                        //Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 14, 3, null);
                        //solicitacao.SOLICITACAO_STATUS_ID = 3;
                        //db.Entry(solicitacao).State = System.Data.Entity.EntityState.Modified;

                        fr.SINT_CODE_ROBO = roboSintegra.Code;
                        fr.SINT_IE_SITU_CADASTRAL = roboSintegra.Data.Message;
                        db.Entry(fr).State = System.Data.Entity.EntityState.Modified;

                        //Envio de email
                        //Geral.EnviarEmail(emailRobo, "Robo Sintegra", string.Format("Robo Sintegra de solicitação id {0} COM ERROR, ROBO CÓDIGO {0}", solicitacao.ID, roboSintegra.Code));
                    }
                    else
                    {
                        contador += 1;
                        fr.SINT_CONTADOR_TENTATIVA = contador;
                        db.Entry(fr).State = System.Data.Entity.EntityState.Modified;                        

                        Geral.EnviarEmail(emailRobo, "Robo Sintegra", string.Format("Robo Sintegra de solicitação id {0} já atingiu 7 tentativas", solicitacao.ID, roboSintegra.Code));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
                    Geral.EnviarEmail(emailRobo, "Robo Sintegra", string.Format("Robo Sintegra de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
                }
            }

            WFD_PJPF_ROBO_LOG entityLog = new WFD_PJPF_ROBO_LOG()
            {
                COD_RETORNO = roboSintegra.Code,
                DATA = DateTime.Now,
                MENSAGEM = roboSintegra.Data.Message,
                ROBO = EnumRobo.Sintegra.ToString(),
                SOLICITACAO_ID = solicitacao.ID
            };

            db.Entry(entityLog).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }

        static void GravaRoboSimples(WFDModel db, RoboSimples roboSimples, WFD_SOLICITACAO solicitacao)
        {
            string emailRobo = ConfigurationManager.AppSettings["EmailRobo"];
            // Grava na tabela WFD_PJPF_robo 
            // Grava na tabela de WFD_PJPF_robo_log

            if (roboSimples != null && roboSimples.Code == 1)
            {
                try
                {
                    if (solicitacao.WFD_PJPF_ROBO.Count > 0)
                    {
                        RoboSimples roboSimplesObj = new RoboSimples();
                        bool gravaDados = roboSimplesObj.GravaRoboSimples(db, roboSimples, solicitacao);
                        if (gravaDados)
                        {
                            //Atualização do Tramite
                            //Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 15, 2, null);

                            //Envio de email
                            //Geral.EnviarEmail(emailRobo, "Robo Simples", string.Format("Robo Simples de solicitação id {0} gravado com sucesso", solicitacao.ID));
                        }
                    }
                }
                catch (Exception ex)
                {
                    //TODO verificar se vai remover os dados que foi inserido/modificado
                    Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
                    Geral.EnviarEmail(emailRobo, "Robo Simples", string.Format("Robo Simples de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
                }
            }

            if (roboSimples != null && roboSimples.Code != 1)
            {
                //int? roboId = solicitacao.WFD_PJPF_ROBO.FirstOrDefault().ID;
                try
                {
                    WFD_PJPF_ROBO fr = solicitacao.WFD_PJPF_ROBO.FirstOrDefault();
                    int? contador = fr.SN_CONTADOR_TENTATIVA;
                    if (!contador.HasValue) contador = 0;

                    if (roboSimples.Code == 2 || roboSimples.Code == 3)
                    {
                        //Atualiza Tramite
                        //Tramite.AtualizaTramite(db, solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, 15, 3, null);
                        //solicitacao.SOLICITACAO_STATUS_ID = 3;
                        //db.Entry(solicitacao).State = System.Data.Entity.EntityState.Modified;
                        //Db.SaveChanges();

                        fr.SN_CODE_ROBO = roboSimples.Code;
                        fr.SIMPLES_NACIONAL_SITUACAO = roboSimples.Data.Message;
                        db.Entry(fr).State = System.Data.Entity.EntityState.Modified;

                        //Envio de email
                        //Geral.EnviarEmail(emailRobo, "Robo Simples", string.Format("Robo Simples de solicitação id {0} COM ERROR  CÓDIGO RETORNADO {1}", solicitacao.ID, roboSimples.Code));
                    }
                    else
                    {
                        
                        contador += 1;
                        fr.SN_CONTADOR_TENTATIVA = contador;
                        db.Entry(fr).State = System.Data.Entity.EntityState.Modified;
                        //Db.SaveChanges();
                        Geral.EnviarEmail(emailRobo, "Robo Simples", string.Format("Robo Simples de solicitação id {0} já atingiu 7 tentativas", solicitacao.ID, roboSimples.Code));
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(string.Format("Error ao tentar buscar dados na PJPF_robo {0} ", solicitacao.ID), ex);
                    Geral.EnviarEmail(emailRobo, "Robo Simples", string.Format("Robo Simples de solicitação id {0} COM ERROR EXCEPTION {1}", solicitacao.ID, ex));
                }
            }

            WFD_PJPF_ROBO_LOG entityLog = new WFD_PJPF_ROBO_LOG()
            {
                COD_RETORNO = roboSimples.Code,
                DATA = DateTime.Now,
                MENSAGEM = roboSimples.Data.Message,
                ROBO = EnumRobo.SimplesNacional.ToString(),
                SOLICITACAO_ID = solicitacao.ID
            };

            db.Entry(entityLog).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
        }

        private static AgendaRobo CarregaAgendaXml()
        {
            AgendaRobo Agenda = new AgendaRobo();
            string pathAgendaRobo = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get(@"DiretorioAgendaRobo") + "\\" + ConfigurationManager.AppSettings["NomeArquivoAgendaRobo"];
            if (!string.IsNullOrEmpty(pathAgendaRobo))
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(AgendaRobo));
                    using (StreamReader stream = new StreamReader(pathAgendaRobo))
                    {
                        Agenda = (AgendaRobo)serializer.Deserialize(stream);
                        Agenda.DataCarga = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    //throw new NotImplementedException(ex.Message);
                }
            }
            return Agenda;
        }

        private static bool VerificarHorario(string agendaHorario)
        {
            DateTime dtAgendaHorario = new DateTime();
            try
            {
                DateTime.TryParse(agendaHorario, out dtAgendaHorario);
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                // agendaHorario pode ta vazio ou diferente de 12:00 (formato)
                // TODO Tratar a Exception
            }

            //DateTime dtAgendaHorario = Convert.ToDateTime(agendaHorario);
            DateTime HoraAtual = DateTime.Now;
            bool dentroDoHorario = false;
            if (HoraAtual.Hour.Equals(dtAgendaHorario.Hour) && HoraAtual.Minute.Equals(dtAgendaHorario.Minute))
                dentroDoHorario = true;

            return dentroDoHorario;
        }

        public static void GravaFornecedorDadosRoboReceitaFederal(WFDModel db, WFD_SOLICITACAO solicitacao, RoboReceitaCNPJ roboReceita)
        {
            WFD_SOL_CAD_PJPF solforn = solicitacao.WFD_SOL_CAD_PJPF.FirstOrDefault();
            solforn.RAZAO_SOCIAL = roboReceita.Data.RazaoSocial;
            solforn.NOME_FANTASIA = roboReceita.Data.NomeFantasia;
            solforn.CNAE = roboReceita.Data.AtividadeEconomicaPrincipal.Substring(0, 10).Replace(".", "").Replace("-", "");
            solforn.ENDERECO = roboReceita.Data.Logradouro;
            solforn.NUMERO = roboReceita.Data.Numero;
            solforn.COMPLEMENTO = roboReceita.Data.Complemento;
            solforn.CEP = roboReceita.Data.CEP;
            solforn.BAIRRO = roboReceita.Data.Bairro;
            solforn.CIDADE = roboReceita.Data.Municipio;
            solforn.UF = roboReceita.Data.UF;
            solforn.PAIS = "BR";

            db.Entry(solicitacao).State = System.Data.Entity.EntityState.Modified;
        }

        public static void GravaFornecedorDadosRoboReceitaFederalPF(WFDModel db, WFD_SOLICITACAO solicitacao, RoboReceitaCPF roboReceitaCpf)
        {
            solicitacao.WFD_SOL_CAD_PJPF.FirstOrDefault().CPF = roboReceitaCpf.Data.CPF;
            solicitacao.WFD_SOL_CAD_PJPF.FirstOrDefault().NOME = roboReceitaCpf.Data.Nome;
            solicitacao.WFD_SOL_CAD_PJPF.FirstOrDefault().PAIS = "BR";            

            db.Entry(solicitacao).State = System.Data.Entity.EntityState.Modified;
        }

        public static void GravaFornecedorDadosSintegra(WFDModel db, WFD_SOLICITACAO solicitacao, RoboSintegra roboSintegra)
        {
            solicitacao.WFD_SOL_CAD_PJPF.FirstOrDefault().INSCR_ESTADUAL = roboSintegra.Data.InscricaoEstadual;
        }
    }
}