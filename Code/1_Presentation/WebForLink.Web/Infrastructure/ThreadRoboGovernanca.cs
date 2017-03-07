using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using WebForLink.Application.Services.Process;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.Infrastructure
{
    public static class ThreadRoboGovernanca
    {
        private static readonly IFornecedorBaseWebForLinkAppService PjpfBase;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly ContratanteConfiguracaoWebForLinkAppService ContratanteConfig;
        public static int ContratanteId { get; set; }
        public static DateTime DataSolicitacao { get; set; }
        public static string path { get; set; }

        public static void Principal()
        {
            ExecutaRobo();
        }

        private static int[] BuscarParaExecucaoRobo()
        {
            WebForLinkContexto dbbase = new WebForLinkContexto();

            int qtd = dbbase.WFD_CONFIG.FirstOrDefault().QTD_ACESSO_ROBO_SIMULTANEO;

            int[] tpFluxoids = { 10, 20, 30, 40 };

            return dbbase.WFD_SOLICITACAO
                .Where(x =>
                    x.CONTRATANTE_ID != ContratanteId &&
                    x.SOLICITACAO_STATUS_ID == (int)EnumStatusTramite.EmAprovacao && // EM APROVACAO
                    x.SOLICITACAO_DT_CRIA > DataSolicitacao &&
                    tpFluxoids.Contains(x.Fluxo.FLUXO_TP_ID) &&
                    x.ROBO_EXECUTADO == false && 
                    x.ROBO_TENTATIVAS_EXCEDIDAS == false
                )
                .OrderBy(y => new { y.SOLICITACAO_DT_CRIA, y.ID })
                .Select(z => z.ID)
                .Take(qtd)
                .ToArray();
        }

        private static SOLICITACAO BuscarsolicitacaoParaRobo(WebForLinkContexto db, int id)
        {
            return db.WFD_SOLICITACAO
                    .Include("ROBO")
                    .Include("SolicitacaoCadastroFornecedor")
                    .FirstOrDefault(x => x.ID == id);
        }

        private static void ExecutaRobo()
        {
            var solicitacoesIds = BuscarParaExecucaoRobo();
            if (ContratanteId > 0 && solicitacoesIds.Length == 0)
            {
                ContratanteId = 0;
                DataSolicitacao = DateTime.MinValue;
                solicitacoesIds = BuscarParaExecucaoRobo();
            }

            foreach (var item in solicitacoesIds)
            {
                System.Threading.Thread thread = new System.Threading.Thread(() => ChamaRobo(item));
                thread.Start();
                //ChamaRobo(item);
            }            
        }

        static void ChamaRobo(int solicitacaoId)
        {
            try
            {
                WebForLinkContexto db = new WebForLinkContexto();

                var solicitacao = BuscarsolicitacaoParaRobo(db, solicitacaoId);

                RoboReceitaCNPJ roboReceita = new RoboReceitaCNPJ();
                RoboSintegra roboSintegra = new RoboSintegra();
                RoboSimples roboSimples = new RoboSimples();
                RoboReceitaCPF roboReceitaCpf = new RoboReceitaCPF();

                var robo = solicitacao.ROBO.FirstOrDefault();
                var solForn = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault();

                bool validaReceita = (robo.ID == 0 || robo.RF_CONSULTA_DTHR == null);
                bool validaSintegra = (robo.ID == 0 || robo.SINT_CONSULTA_DTHR == null);
                bool validaSimples = (robo.ID == 0 || robo.SN_CONSULTA_DTHR == null);

                if (solForn.PJPF_TIPO == 1)
                {
                    if (validaReceita && solForn.CNPJ != null)
                    {
                        roboReceita = roboReceita.CarregaRoboCNPJ(solForn.CNPJ, path);

                        RoboReceitaCNPJ roboReceitaCnpj = new RoboReceitaCNPJ();
                        roboReceitaCnpj.GravaRoboReceita(roboReceita, ref robo);

                        GravaLog(db, roboReceita.Code, roboReceita.Data.Message, EnumRobo.ReceitaFederal.ToString(), solicitacao.CONTRATANTE_ID, solicitacao.PJPF_ID, solicitacao.ID);
                    }

                    if (validaSintegra && robo.RF_UF != null)
                    {
                        roboSintegra = roboSintegra.CarregaSintegra(robo.RF_UF, solForn.CNPJ, path);

                        RoboSintegra roboSintegraObj = new RoboSintegra();
                        roboSintegraObj.GravaRoboSintegra(roboSintegra, ref robo);

                        GravaLog(db, roboSintegra.Code, roboSintegra.Data.Message, EnumRobo.Sintegra.ToString(), solicitacao.CONTRATANTE_ID, solicitacao.PJPF_ID, solicitacao.ID);
                    }

                    if (validaSimples && solForn.CNPJ != null)
                    {
                        roboSimples = roboSimples.CarregaSimplesCNPJ(solForn.CNPJ, path);

                        RoboSimples roboSimplesObj = new RoboSimples();
                        roboSimplesObj.GravaRoboSimples(roboSimples, ref robo);

                        GravaLog(db, roboSimples.Code, roboSimples.Data.Message, EnumRobo.SimplesNacional.ToString(), solicitacao.CONTRATANTE_ID, solicitacao.PJPF_ID, solicitacao.ID);
                    }
                }

                // Pessoa Fisica
                if (solForn.PJPF_TIPO == 3)
                {
                    if (validaReceita && solForn.CPF != null)
                    {
                        roboReceitaCpf = roboReceitaCpf.CarregaRoboCPF(solForn.CPF, solForn.DT_NASCIMENTO.ToString(), path);

                        RoboReceitaCPF roboCpfObj = new RoboReceitaCPF();
                        roboCpfObj.GravaRoboCpf(roboReceitaCpf, ref robo);

                        GravaLog(db, roboReceitaCpf.Code, roboReceitaCpf.Data.Message, EnumRobo.ReceitaFederalPF.ToString(), solicitacao.CONTRATANTE_ID, solicitacao.PJPF_ID, solicitacao.ID);
                    }
                }

                if (robo.RF_CONSULTA_DTHR != null && robo.SINT_CONSULTA_DTHR != null && robo.SN_CONSULTA_DTHR != null)
                    solicitacao.ROBO_EXECUTADO = true;
                if (robo.RF_CONTADOR_TENTATIVA >= 3 && robo.SINT_CONTADOR_TENTATIVA >= 3 && robo.SN_CONTADOR_TENTATIVA >= 3)
                    solicitacao.ROBO_TENTATIVAS_EXCEDIDAS = true;

                ContratanteId = solicitacao.CONTRATANTE_ID;
                DataSolicitacao = (DateTime)solicitacao.SOLICITACAO_DT_CRIA;

                db.Entry(solicitacao).State = EntityState.Modified;
                db.SaveChanges();
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

        private static void GravaLog(WebForLinkContexto db, int? Code, string Mensagem, string orgao, int contratanteId, int? pjpfId, int? solicitacaoId)
        {
            ROBO_LOG entityLog = new ROBO_LOG()
            {
                COD_RETORNO = Code,
                DATA = DateTime.Now,
                MENSAGEM = Mensagem,
                ROBO = orgao,
                CONTRATANTE_ID = contratanteId,
                PJPF_ID = pjpfId,
                SOLICITACAO_ID = solicitacaoId
            };

            db.Entry(entityLog).State = System.Data.Entity.EntityState.Added;
        }

        private static void GeraBloqueioAutomatico(WebForLinkContexto db, FORNECEDORBASE pjpf, SOLICITACAO solicitacao, SOLICITACAO_BLOQUEIO bloq)
        {
            solicitacao.CONTRATANTE_ID = pjpf.CONTRATANTE_ID;
            solicitacao.FLUXO_ID = db.WFL_FLUXO.FirstOrDefault(x => x.CONTRATANTE_ID == pjpf.CONTRATANTE_ID && x.FLUXO_TP_ID == 110).ID;
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
    }
}