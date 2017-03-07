using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Timers;
using WebForDocs.Business.Process;
using WebForDocs.Data.ModeloDB;
using WebForDocs.Dominio.Models;
using WebForDocs.Enums;

namespace WebForDocs.Biblioteca
{
    public static class ThreadRoboImportacao
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static DateTime UltimaExecucao { get; set; }
        public static DateTime ProximaExecucao { get; set; }
        public static int QtdExecucao { get; set; }
        public static TimeSpan TempoExecucao { get; set; }
        public static Timer timer { get; set; }
        public static bool Carregado
        {
            get
            {
                return timer == null ? false : true;
            }
        }
        public static bool Ativo 
        { 
            get 
            {
                return timer == null ? false : timer.Enabled;
            } 
        }
        public static string Status 
        { 
            get 
            {
                string ret = "";
                if (timer == null)
                    ret = "Não Iniciado";
                else
                    ret = timer.Enabled ? "Ativo" : "Parado";

                return ret;
            } 
        }
        public static string Url { get; set; }
        public static int ContratanteId { get; set; }
        public static DateTime DataSolicitacao { get; set; }
        public static string path { get; set; }

        //public static void Principal(Object source, System.Timers.ElapsedEventArgs e)
        //{
        //    UltimaExecucao = e.SignalTime;
        //    ProximaExecucao = UltimaExecucao.AddMilliseconds(timer.Interval);
        //    QtdExecucao++;
        //    ExecutaRobo();
        //}

        //public static void ModificaTempoExecucao(TimeSpan tempo)
        //{
        //    timer.Interval = tempo.TotalMilliseconds;
        //    ProximaExecucao = DateTime.Now.AddMilliseconds(timer.Interval);
        //    TempoExecucao = tempo;
        //}

        //public static void PararExecucao()
        //{
        //    timer.Stop();
        //    ProximaExecucao = DateTime.MinValue;
        //}

        //public static void IniciarExecucao()
        //{
        //    timer.Start();
        //    ProximaExecucao = DateTime.Now.AddMilliseconds(timer.Interval);
        //}

        //public static void CarregarServicaoRobo(TimeSpan tempo)
        //{
        //    if (timer == null)
        //    {
        //        var tm = new System.Timers.Timer();
        //        tm.Interval = tempo.TotalMilliseconds;
        //        tm.Elapsed += ThreadRoboImportacao.Principal;
        //        tm.AutoReset = true;
        //        tm.Enabled = true;

        //        timer = tm;
        //    }
        //    else
        //    {
        //        timer.Interval = tempo.TotalMilliseconds;
        //        timer.Enabled = true;
        //    }
        //    ProximaExecucao = DateTime.Now.AddMilliseconds(timer.Interval);
        //    TempoExecucao = tempo;
        //}

        public static void Principal()
        {
            ExecutaRobo();
        }

        private static int[] BuscarParaExecucaoRobo()
        {
            WFLModel dbbase = new WFLModel();

            try
            {
                //dbbase.Database.Log = s => _metodosGerais.LogQueries(s);

                int qtd = dbbase.WFD_CONFIG.FirstOrDefault().QTD_ACESSO_ROBO_SIMULTANEO;
                //int? tentativas = dbbase.WFD_CONTRATANTE_CONFIG.SingleOrDefault(x => x.CONTRATANTE_ID == ContratanteId).TOTAL_TENTATIVA_ROBO;

                return dbbase.WFD_PJPF_BASE
                        .Include("WFD_PJPF_ROBO")
                        .Where(x => x.CONTRATANTE_ID != ContratanteId
                            && x.EXECUTA_ROBO == true
                            && x.DT_SOLICITACAO_ROBO > DataSolicitacao
                            && x.ROBO_EXECUTADO == false
                            && x.ROBO_TENTATIVAS_EXCEDIDAS == false)
                        .OrderBy(y => new { y.DT_SOLICITACAO_ROBO, y.ID })
                        .Select(z => z.ID)
                        .Take(qtd).ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static WFD_PJPF_BASE BuscarFonecedorBaseParaRobo(WFLModel db, int id)
        {
            return db.WFD_PJPF_BASE
                    .Include("WFD_PJPF_ROBO")
                    .Include("WFD_SOLICITACAO.WFD_SOL_CAD_PJPF")
                    .SingleOrDefault(x => x.ID == id);
        }

        private static void ExecutaRobo()
        {
            PJPFBaseBP pjpfBase = new PJPFBaseBP();
            ContratanteConfiguracaoBP contratanteConfig = new ContratanteConfiguracaoBP();
            try
            {
                var pjpfs = BuscarParaExecucaoRobo();
                if (ContratanteId > 0 && pjpfs.Length == 0)
                {
                    ContratanteId = 0;
                    DataSolicitacao = DateTime.MinValue;
                    pjpfs = BuscarParaExecucaoRobo();
                }

                foreach (var item in pjpfs)
                {
                    System.Threading.Thread thread = new System.Threading.Thread(() => ChamaRobo(item));
                    thread.Start();
                    //ChamaRobo(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        static void ChamaRobo(int pjpfId)
        {
            try
            {
                WFLModel db = new WFLModel();

                var pjpf = BuscarFonecedorBaseParaRobo(db, pjpfId);
                
                RoboReceitaCNPJ roboReceita = new RoboReceitaCNPJ();
                RoboSintegra roboSintegra = new RoboSintegra();
                RoboSimples roboSimples = new RoboSimples();
                RoboReceitaCPF roboReceitaCpf = new RoboReceitaCPF();

                if (pjpf.WFD_PJPF_ROBO == null)
                    pjpf.WFD_PJPF_ROBO = new WFD_PJPF_ROBO();

                bool validaReceita = (pjpf.WFD_PJPF_ROBO.ID == 0 || pjpf.WFD_PJPF_ROBO.RF_CONSULTA_DTHR == null);
                bool validaSintegra = (pjpf.WFD_PJPF_ROBO.ID == 0 || pjpf.WFD_PJPF_ROBO.SINT_CONSULTA_DTHR == null);
                bool validaSimples = (pjpf.WFD_PJPF_ROBO.ID == 0 || pjpf.WFD_PJPF_ROBO.SN_CONSULTA_DTHR == null);

                if (pjpf.PJPF_TIPO == 1)
                {
                    if (validaReceita && pjpf.CNPJ != null)
                    {
                        roboReceita = roboReceita.CarregaRoboCNPJ(pjpf.CNPJ, path);

                        var robo = pjpf.WFD_PJPF_ROBO;
                        RoboReceitaCNPJ roboReceitaCnpj = new RoboReceitaCNPJ();
                        roboReceitaCnpj.GravaRoboReceita(roboReceita, ref robo);

                        GravaLog(db, roboReceita.Code, roboReceita.Data.Message, EnumRobo.ReceitaFederal.ToString(), pjpf.CONTRATANTE_ID, pjpf.ID);
                    }

                    if (validaSintegra && pjpf.WFD_PJPF_ROBO.RF_UF != null)
                    {
                        roboSintegra = roboSintegra.CarregaSintegra(pjpf.WFD_PJPF_ROBO.RF_UF, pjpf.CNPJ, path);

                        var robo = pjpf.WFD_PJPF_ROBO;
                        RoboSintegra roboSintegraObj = new RoboSintegra();
                        roboSintegraObj.GravaRoboSintegra(roboSintegra, ref robo);

                        GravaLog(db, roboSintegra.Code, roboSintegra.Data.Message, EnumRobo.Sintegra.ToString(), pjpf.CONTRATANTE_ID, pjpf.ID);
                    }

                    if (validaSimples && pjpf.CNPJ != null)
                    {
                        roboSimples = roboSimples.CarregaSimplesCNPJ(pjpf.CNPJ, path);

                        var robo = pjpf.WFD_PJPF_ROBO;
                        RoboSimples roboSimplesObj = new RoboSimples();
                        roboSimplesObj.GravaRoboSimples(roboSimples, ref robo);

                        GravaLog(db, roboSimples.Code, roboSimples.Data.Message, EnumRobo.SimplesNacional.ToString(), pjpf.CONTRATANTE_ID, pjpf.ID);
                    }
                }

                // Pessoa Fisica
                if (pjpf.PJPF_TIPO == 3)
                {
                    if (validaReceita && pjpf.CPF != null)
                    {
                        DateTime nasc = (DateTime)pjpf.DT_NASCIMENTO;
                        roboReceitaCpf = roboReceitaCpf.CarregaRoboCPF(pjpf.CPF, nasc.ToString("dd/MM/yyyy"), path);

                        var robo = pjpf.WFD_PJPF_ROBO;
                        RoboReceitaCPF roboCpfObj = new RoboReceitaCPF();
                        roboCpfObj.GravaRoboCpf(roboReceitaCpf, ref robo);

                        GravaLog(db, roboReceitaCpf.Code, roboReceitaCpf.Data.Message, EnumRobo.ReceitaFederalPF.ToString(), pjpf.CONTRATANTE_ID, pjpf.ID);
                    }
                }

                #region BLOQUEIO

                bool ReceitaIrregular = false, ReceitaInativa = false, SintegraNaoHabilitado = false;

                if (pjpf.PJPF_TIPO == 1)
                {
                    ReceitaInativa = (!String.IsNullOrEmpty(roboReceita.Data.SituacaoCadastral) && roboReceita.Data.SituacaoCadastral.ToUpper() != "ATIVA");
                    SintegraNaoHabilitado = (!String.IsNullOrEmpty(roboSintegra.Data.SituacaoCadastral) && roboSintegra.Data.SituacaoCadastral == "HABILITADO ATIVO" && roboSintegra.Data.SituacaoCadastral == "HABILITADO");
                }
                else
                {
                    ReceitaIrregular = (!String.IsNullOrEmpty(roboReceitaCpf.Data.SituacaoCadastral) && roboReceitaCpf.Data.SituacaoCadastral.ToUpper() != "REGULAR");
                }

                WFD_SOLICITACAO solicitacao = new WFD_SOLICITACAO();
                WFD_SOL_BLOQ bloq = new WFD_SOL_BLOQ();

                if (ReceitaInativa || SintegraNaoHabilitado || ReceitaIrregular)
                {
                    var BloqManual = db.WFD_CONTRATANTE_CONFIG.SingleOrDefault(x => x.CONTRATANTE_ID == pjpf.CONTRATANTE_ID).BLOQUEIO_MANUAL;
                    if (!BloqManual)
                    {
                        GeraBloqueioAutomatico(db, pjpf, solicitacao, bloq);
                    }
                }

                #endregion

                if (pjpf.WFD_PJPF_ROBO.RF_CONSULTA_DTHR != null && pjpf.WFD_PJPF_ROBO.SINT_CONSULTA_DTHR != null && pjpf.WFD_PJPF_ROBO.SN_CONSULTA_DTHR != null)
                    pjpf.ROBO_EXECUTADO = true;
                if (pjpf.WFD_PJPF_ROBO.RF_CONTADOR_TENTATIVA >= 3 && pjpf.WFD_PJPF_ROBO.SINT_CONTADOR_TENTATIVA >= 3 && pjpf.WFD_PJPF_ROBO.SN_CONTADOR_TENTATIVA >= 3)
                    pjpf.ROBO_TENTATIVAS_EXCEDIDAS = true;

                ContratanteId = pjpf.CONTRATANTE_ID;
                DataSolicitacao = (DateTime)pjpf.DT_SOLICITACAO_ROBO;

                atualizaPJPFBase(db, pjpf);

                db.Entry(pjpf).State = EntityState.Modified;
                db.SaveChanges();

                //ATUALIZA TRAMITE SE HOUVER CRIAÇÃO DE BLOQUEIO
                if (ReceitaInativa || SintegraNaoHabilitado)
                {
                    int papel = db.WFL_PAPEL.SingleOrDefault(x => x.CONTRATANTE_ID == pjpf.CONTRATANTE_ID && x.PAPEL_TP_ID == 10).ID;
                    Tramite.AtualizaTramite( pjpf.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, papel, 2, null);
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    foreach (var ve in eve.ValidationErrors)
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error ao percorrer a lista 'lstFornecedorRobo' exception: {0}", ex));
            }
        }

        private static void GravaLog(WFLModel db, int? codigoRetorno, string mensagem, string orgao, int contratanteId, int pjpfBaseId)
        {
            WFD_PJPF_ROBO_LOG pjpfRoboLog = new WFD_PJPF_ROBO_LOG()
            {
                COD_RETORNO = codigoRetorno,
                DATA = DateTime.Now,
                MENSAGEM = mensagem,
                ROBO = orgao,
                CONTRATANTE_ID = contratanteId,
                PJPF_BASE_ID = pjpfBaseId
                //SOLICITACAO_ID = solicitacao.ID
            };

            db.Entry(pjpfRoboLog).State = System.Data.Entity.EntityState.Added;
        }

        private static void GeraBloqueioAutomatico(WFLModel db, WFD_PJPF_BASE pjpf, WFD_SOLICITACAO solicitacao, WFD_SOL_BLOQ bloq)
        {
            solicitacao.CONTRATANTE_ID = pjpf.CONTRATANTE_ID;
            solicitacao.FLUXO_ID = db.WFL_FLUXO.SingleOrDefault(x => x.CONTRATANTE_ID == pjpf.CONTRATANTE_ID && x.FLUXO_TP_ID == 110).ID;
            solicitacao.PJPF_BASE_ID = pjpf.ID;
            solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
            solicitacao.SOLICITACAO_STATUS_ID = 5;

            bloq.BLQ_COMPRAS_TODAS_ORG_COMPRAS = false;
            bloq.BLQ_LANCAMENTO_EMP = true;
            bloq.BLQ_QUALIDADE_FUNCAO_BQL_ID = 2;
            bloq.WFD_SOLICITACAO = solicitacao;
            bloq.BLQ_MOTIVO_DSC = "Bloqueio gerado automaticamente.";

            db.Entry(solicitacao).State = EntityState.Added;
            db.Entry(bloq).State = EntityState.Added;
        }

        private static void atualizaPJPFBase(WFLModel db, WFD_PJPF_BASE pjpf)
        {
            pjpf.BAIRRO = pjpf.WFD_PJPF_ROBO.RF_BAIRRO;
            pjpf.CEP = pjpf.WFD_PJPF_ROBO.RF_CEP;
            pjpf.CIDADE = pjpf.WFD_PJPF_ROBO.RF_MUNICIPIO;
            pjpf.COMPLEMENTO = pjpf.WFD_PJPF_ROBO.RF_COMPLEMENTO;
            pjpf.ENDERECO = pjpf.WFD_PJPF_ROBO.RF_LOGRADOURO;
            pjpf.INSCR_ESTADUAL = pjpf.WFD_PJPF_ROBO.SINT_IE_COD;
            pjpf.NOME = pjpf.WFD_PJPF_ROBO.RF_NOME;
            pjpf.NOME_FANTASIA = pjpf.WFD_PJPF_ROBO.RF_NOME_FANTASIA;
            pjpf.NUMERO = pjpf.WFD_PJPF_ROBO.RF_NUMERO;
            pjpf.RAZAO_SOCIAL = pjpf.WFD_PJPF_ROBO.RECEITA_FEDERAL_RAZAO_SOCIAL;
            pjpf.UF = pjpf.WFD_PJPF_ROBO.RF_UF;

            if (pjpf.WFD_SOLICITACAO.Any())
            {
                var solicitacao = pjpf.WFD_SOLICITACAO.FirstOrDefault();
                if (solicitacao != null)
                {
                    var solForn = solicitacao.WFD_SOL_CAD_PJPF.FirstOrDefault();
                    if (solForn != null)
                    {
                        solForn.BAIRRO = pjpf.WFD_PJPF_ROBO.RF_BAIRRO;
                        solForn.CEP = pjpf.WFD_PJPF_ROBO.RF_CEP;
                        solForn.CIDADE = pjpf.WFD_PJPF_ROBO.RF_MUNICIPIO;
                        solForn.COMPLEMENTO = pjpf.WFD_PJPF_ROBO.RF_COMPLEMENTO;
                        solForn.ENDERECO = pjpf.WFD_PJPF_ROBO.RF_LOGRADOURO;
                        solForn.NOME = pjpf.WFD_PJPF_ROBO.RF_NOME;
                        solForn.NOME_FANTASIA = pjpf.WFD_PJPF_ROBO.RF_NOME_FANTASIA;
                        solForn.NUMERO = pjpf.WFD_PJPF_ROBO.RF_NUMERO;
                        solForn.RAZAO_SOCIAL = pjpf.WFD_PJPF_ROBO.RECEITA_FEDERAL_RAZAO_SOCIAL;
                        solForn.ROBO_ID = pjpf.WFD_PJPF_ROBO.ID;
                        solForn.UF = pjpf.WFD_PJPF_ROBO.RF_UF;
                        solForn.INSCR_ESTADUAL = pjpf.WFD_PJPF_ROBO.SINT_IE_COD;

                        db.Entry(solForn).State = EntityState.Modified;
                    }
                }
            }
        }

    }
}