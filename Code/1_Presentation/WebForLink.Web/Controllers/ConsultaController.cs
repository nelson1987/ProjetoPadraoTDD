using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Controllers
{
    public class ConsultaController : ControllerPadrao
    {
        public readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        public readonly RoboSintegraWebForLinkAppService _roboSintegraBP;
        public ConsultaController(ISolicitacaoWebForLinkAppService solicitacao)
        {
            try
            {
                _solicitacaoService = solicitacao;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        public JsonResult ReceitaFederalCNPJ(string cnpj, int contratante, int tipoFornecedor, int solicitacaoId)
        {
            string path = Server.MapPath("~/");
            RoboReceitaCNPJ roboCNPJ = new RoboReceitaCNPJ();

            roboCNPJ = roboCNPJ.CarregaRoboCNPJ(cnpj, path);

            try
            {
                SOLICITACAO solicitacao = _solicitacaoService.BuscarPorId(solicitacaoId);

                SolicitacaoCadastroFornecedor solForn = solicitacao.SolicitacaoCadastroFornecedor.First();
                ROBO robo = solicitacao.ROBO.FirstOrDefault();

                roboCNPJ.GravaRoboReceita(roboCNPJ, ref robo);
                //_solicitacaoService.Alterar(solicitacao);
                //Db.Entry(robo).State = EntityState.Modified;
                //_repositorios.roboBP.Alterar(robo);

                if (roboCNPJ.Code < 100)
                {
                    if (roboCNPJ.Code == 1)
                    {
                        solForn.PJPF_TIPO = tipoFornecedor;
                        solForn.RAZAO_SOCIAL = roboCNPJ.Data.RazaoSocial;
                        solForn.NOME_FANTASIA = roboCNPJ.Data.NomeFantasia;
                        if (roboCNPJ.Data.AtividadeEconomicaPrincipal.Length > 10 && !roboCNPJ.Data.AtividadeEconomicaPrincipal.Contains("*"))
                            solForn.CNAE = roboCNPJ.Data.AtividadeEconomicaPrincipal.Substring(0, 10).Replace(".", "").Replace("-", "");
                        solForn.CNPJ = cnpj.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", ""); ;
                        solForn.ENDERECO = roboCNPJ.Data.Logradouro;
                        solForn.NUMERO = roboCNPJ.Data.Numero;
                        solForn.COMPLEMENTO = roboCNPJ.Data.Complemento;
                        solForn.CEP = roboCNPJ.Data.CEP;
                        solForn.BAIRRO = roboCNPJ.Data.Bairro;
                        solForn.CIDADE = roboCNPJ.Data.Municipio;
                        solForn.UF = roboCNPJ.Data.UF;
                        //Db.Entry(solForn).State = EntityState.Modified;
                        //_repositorios.solicitacaoCadastroPJPFBP.Alterar(solForn);
                    }
                }

                ROBO_LOG entityLog = new ROBO_LOG()
                {
                    COD_RETORNO = roboCNPJ.Code,
                    DATA = DateTime.Now,
                    MENSAGEM = roboCNPJ.Data.Message,
                    ROBO = EnumRobo.ReceitaFederal.ToString(),
                    CONTRATANTE_ID = contratante
                };
                solicitacao.WFD_PJPF_ROBO_LOG.Add(entityLog);
                _solicitacaoService.Alterar(solicitacao);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                throw;
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                Log.Error(string.Format("Error ao executar o método ReceitaFederalCNPJ: {0}", ex));
            }

            return Json(roboCNPJ);
        }

        public JsonResult ReceitaFederalCPF(string cpf, int contratante, string dataNascimento, int tipoFornecedor, int solicitacaoId)
        {
            string path = Server.MapPath("~/");

            RoboReceitaCPF RoboCPF = new RoboReceitaCPF();
            RoboCPF = RoboCPF.CarregaRoboCPF(cpf, dataNascimento, path);

            int UsuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            try
            {
                //WFD_SOLICITACAO solicitacao = Db.WFD_SOLICITACAO.Include("WFD_SOL_CAD_PJPF").Include("WFD_PJPF_ROBO").FirstOrDefault(s => s.ID == solicitacaoId);
                SOLICITACAO solicitacao = _solicitacaoService.BuscarPorId(solicitacaoId);
                SolicitacaoCadastroFornecedor solForn = solicitacao.SolicitacaoCadastroFornecedor.First();
                ROBO robo = solicitacao.ROBO.FirstOrDefault();

                RoboCPF.GravaRoboCpf(RoboCPF, ref robo);
                //_solicitacaoService.Alterar(solicitacao);
                //_repositorios.roboBP.Inserir(robo);
                //Db.Entry(robo).State = EntityState.Modified;

                if (RoboCPF.Code < 100)
                {
                    if (RoboCPF.Code == 1)
                    {
                        solForn.PJPF_TIPO = tipoFornecedor;
                        solForn.NOME = RoboCPF.Data.Nome;
                        solForn.CPF = cpf.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");
                        solForn.WFD_SOLICITACAO = solicitacao;

                        //Db.Entry(solForn).State = EntityState.Modified;
                        //_repositorios.solicitacaoCadastroPJPFBP.Alterar(solForn);
                    }
                }

                ROBO_LOG entityLog = new ROBO_LOG()
                {
                    COD_RETORNO = RoboCPF.Code,
                    DATA = DateTime.Now,
                    MENSAGEM = RoboCPF.Data.Message,
                    ROBO = EnumRobo.ReceitaFederalPF.ToString(),
                    //WFD_SOLICITACAO = solicitacao,
                    CONTRATANTE_ID = contratante
                };

                solicitacao.WFD_PJPF_ROBO_LOG.Add(entityLog);
                _solicitacaoService.Alterar(solicitacao);
                //_solicitacaoService.Dispose();
                RoboCPF.SolicitacaoID = solicitacao.ID;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                throw;
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                //dbContextTransaction.Rollback();
                Log.Error(string.Format("Error ao executar o método ReceitaFederalCPF: {0}", ex));
            }

            return Json(RoboCPF);
        }

        public JsonResult SimplesNacional(string cnpj, int solicitacaoID)
        {
            string path = Server.MapPath("~/");
            RoboSimples roboSimples = new RoboSimples();

            try
            {
                roboSimples = roboSimples.CarregaSimplesCNPJ(cnpj, path);

                if (roboSimples.Code < 100)
                {
                    SOLICITACAO solicitacao = _solicitacaoService.BuscarPorId(solicitacaoID);
                    var robo = solicitacao.ROBO.FirstOrDefault();
                    roboSimples.GravaRoboSimples(roboSimples, ref robo);
                    solicitacao.WFD_PJPF_ROBO_LOG.Add(new ROBO_LOG()
                    {
                        DATA = DateTime.Now,
                        ROBO = EnumRobo.SimplesNacional.ToString(),
                        COD_RETORNO = roboSimples.Code,
                        MENSAGEM = roboSimples.Data.Message,
                        SOLICITACAO_ID = solicitacao.ID,
                        CONTRATANTE_ID = solicitacao.CONTRATANTE_ID,
                    });
                    //solicitacaoRoboBP.Alterar(solicitacao);

                    //solicitacao.WFD_PJPF_ROBO_LOG.Add(entityLog);
                    _solicitacaoService.Alterar(solicitacao);
                    //_solicitacaoService.Dispose();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error ao executar o método SimplesNacional: {0}", ex));
            }

            return Json(roboSimples, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Sintegra(string cnpj, string uf, int solicitacaoID)
        {
            string path = Server.MapPath("~/");
            RoboSintegra sintegra = new RoboSintegra();

            try
            {
                sintegra = sintegra.CarregaSintegra(uf, cnpj, path);

                if (sintegra.Code < 100)
                {

                    WebForLink.Domain.Biblioteca.RoboSintegraWebForLinkAppService roboSintegraDominio = new WebForLink.Domain.Biblioteca.RoboSintegraWebForLinkAppService()
                    {
                        Code = sintegra.Code,
                        cssCor = sintegra.cssCor,
                        Data = new WebForLink.Domain.Biblioteca.DataSintegra()
                        {
                            AtividadeEconomicaPrincipal = sintegra.Data.AtividadeEconomicaPrincipal,
                            DataSituacaoCadastral = sintegra.Data.DataSituacaoCadastral,
                            InscricaoEstadual = sintegra.Data.InscricaoEstadual,
                            Contingencia = sintegra.Data.Contingencia,
                            SituacaoCadastral = sintegra.Data.SituacaoCadastral,
                            Complemento = sintegra.Data.Complemento,
                            RazaoSocial = sintegra.Data.RazaoSocial,
                            SituacaoEFD = sintegra.Data.SituacaoEFD,
                            MultiplasIE = sintegra.Data.MultiplasIE,
                            Telefone = sintegra.Data.Telefone,
                            EmissaoNFEObrigatorio = sintegra.Data.EmissaoNFEObrigatorio,
                            PerfilEFD = sintegra.Data.PerfilEFD,
                            CNPJ = sintegra.Data.CNPJ,
                            Bairro = sintegra.Data.Bairro,
                            Logradouro = sintegra.Data.Logradouro,
                            Numero = sintegra.Data.Numero,
                            CEP = sintegra.Data.CEP,
                            Municipio = sintegra.Data.Municipio,
                            CTE = sintegra.Data.CTE,
                            DataInclusao = sintegra.Data.DataInclusao,
                            UF = sintegra.Data.UF,
                            Message = sintegra.Data.Message,
                            EnquadramentoFiscal = sintegra.Data.EnquadramentoFiscal
                        },
                        DataConsulta = sintegra.DataConsulta,
                        HTML = sintegra.HTML,
                        tpPapel = sintegra.tpPapel,
                        UUID = sintegra.UUID
                    };
                    _roboSintegraBP.InserirRoboSintegraSolicitacao(roboSintegraDominio, solicitacaoID);
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error ao executar o método Sintegra: {0}", ex));
            }

            return Json(sintegra, JsonRequestBehavior.AllowGet);
        }
    }
}