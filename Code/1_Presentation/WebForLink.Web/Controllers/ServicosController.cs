using System;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Exceptions;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    public class ServicosController : ControllerPadrao
    {
        private const String AcessoNegado = "Acesso Negado!";
        readonly ServicosVM _servicos = new ServicosVM();
        private readonly IThreadRoboCargaErp _threadRoboCargaErp;

        private readonly IConfiguracaoWebForLinkAppService _config;
        public ServicosController(IConfiguracaoWebForLinkAppService configuracao, IThreadRoboCargaErp roboCarga) : base()
        {
            _config = configuracao;
            _threadRoboCargaErp = roboCarga;
        }

        [HttpGet]
        public JsonResult ChamaRoboImportacao(string chave)
        {
            try
            {
                if (VerificaChave(chave))
                {
                    ThreadRoboImportacao.path = Server.MapPath("~/");
                    ThreadRoboImportacao.Principal();
                    _servicos.Erro = 0;
                }
                else
                {
                    _servicos.Erro = 1;
                    _servicos.Mensagem = AcessoNegado;
                }
            }
            catch (Exception ex)
            {
                _servicos.Erro = 2;
                _servicos.Mensagem = ex.Message;
            }

            return Json(_servicos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChamaRoboGovernanca(string chave)
        {
            try
            {
                if (VerificaChave(chave))
                {
                    ThreadRoboGovernanca.path = Server.MapPath("~/");
                    ThreadRoboGovernanca.Principal();
                    _servicos.Erro = 0;
                }
                else
                {
                    _servicos.Erro = 1;
                    _servicos.Mensagem = AcessoNegado;
                }
            }
            catch (Exception ex)
            {
                _servicos.Erro = 2;
                _servicos.Mensagem = ex.Message;
            }

            return Json(_servicos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChamaRoboDocVencimento(string chave)
        {
            try
            {
                if (VerificaChave(chave))
                {
                    RoboGestaoDocumentos.InicializarRobo();
                    _servicos.Erro = 0;
                    _servicos.Mensagem = "Execução de Robô de documentos vencidos concluída.";
                }
                else
                {
                    _servicos.Erro = 1;
                    _servicos.Mensagem = AcessoNegado;
                }
            }
            catch (WebForLinkException ex)
            {
                Log.Error(ex);
                _servicos.Erro = 2;
                _servicos.Mensagem = ex.Message;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _servicos.Erro = 2;
                _servicos.Mensagem = ex.Message;
            }

            return Json(_servicos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChamaRoboCargaErp(string chave)
        {
            try
            {
                if (VerificaChave(chave))
                {
                    _threadRoboCargaErp.InicializarRobo();
                    _servicos.Erro = 0;
                    _servicos.Mensagem = "Execução de Robô de Carga ERP concluída.";
                }
                else
                {
                    _servicos.Erro = 1;
                    _servicos.Mensagem = AcessoNegado;
                }
            }
            catch (WebForLinkException ex)
            {
                Log.Error(ex);
                _servicos.Erro = 2;
                _servicos.Mensagem = ex.Message;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _servicos.Erro = 2;
                _servicos.Mensagem = ex.Message;
            }

            return Json(_servicos, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ChamaRoboRetornoCarga(string chave)
        {
            try
            {
                if (VerificaChave(chave))
                {
                    _threadRoboCargaErp.InicializarRobo();
                    _servicos.Erro = 0;
                    _servicos.Mensagem = "Execução de Robô de documentos vencidos concluída.";
                }
                else
                {
                    _servicos.Erro = 1;
                    _servicos.Mensagem = AcessoNegado;
                }
            }
            catch (WebForLinkException ex)
            {
                Log.Error(ex);
                _servicos.Erro = 2;
                _servicos.Mensagem = ex.Message;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                _servicos.Erro = 2;
                _servicos.Mensagem = ex.Message;
            }

            return Json(_servicos, JsonRequestBehavior.AllowGet);
        }

        private bool VerificaChave(string chave)
        {
            if (!String.IsNullOrEmpty(chave))
            {
                var cfg = _config.BuscarConfigGeral();
                var chaveDescrit = new Criptografia(EnumCripto.Descriptografar,chave, cfg.CHAVE_CRIPTO).Resultado;
                return _config.VerificarChaveWebService(chaveDescrit);
            }
            else
            {
                return false;
            }
        }

        public string UrlAction(int usuarioId)
        {
            string url = string.Empty;
            if (Request.Url != null)
            {
                Url.Action("CadastrarUsuario", "Home",
                      new { area = "", chaveurl = Cripto.Criptografar(string.Format("id={0}&tipocadastro={1}", usuarioId, (int)EnumTipoCadastroNovoUsuario.Cadastrado), Key) },
                      Request.Url.Scheme);
            }
            return url;
        }
    }
}