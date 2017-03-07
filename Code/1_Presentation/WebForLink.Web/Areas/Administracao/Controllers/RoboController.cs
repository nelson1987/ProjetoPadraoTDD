using System;
using System.Web.Mvc;
using WebForLink.Service.Process;
using WebForLink.Web.Areas.Administracao.Models;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;

namespace WebForDocs.Areas.Administracao.Controllers
{
    public class RoboController : ControllerPadrao
    {
        [Authorize]
        public ActionResult RoboImportacao()
        {
            RoboImportacaoModel model = new RoboImportacaoModel();
            carregaModel(model);

            return View(model);
        }

        [Authorize]
        public JsonResult ModificarTempo(string tempo)
        {
            RoboImportacaoModel model = new RoboImportacaoModel();
            TimeSpan ts = TimeSpan.Zero;
            if (configBP.validaTempo(tempo, ref ts))
            {
                if (configBP.AlterarConfig(ts))
                    ThreadRoboImportacao.ModificaTempoExecucao(ts);
                else
                    model.MensagemErro = "Não foi possível modificar o tempo de execução!";
            }
            else
            {
                model.MensagemErro = "Tempo Inválido de Execução!";
            }

            carregaModel(model);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult IniciarExecucao()
        {
            RoboImportacaoModel model = new RoboImportacaoModel();
            ThreadRoboImportacao.IniciarExecucao();
            
            carregaModel(model);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult PararExecucao()
        {
            RoboImportacaoModel model = new RoboImportacaoModel();
            ThreadRoboImportacao.PararExecucao();
            
            carregaModel(model);

            return Json(model, JsonRequestBehavior.AllowGet);
        }
        
        [Authorize]
        public JsonResult CarregarServico(string tempo)
        {
            RoboImportacaoModel model = new RoboImportacaoModel();
            TimeSpan ts = TimeSpan.Zero;
            if (configBP.validaTempo(tempo, ref ts))
                ThreadRoboImportacao.CarregarServicaoRobo(ts);
            else
                model.MensagemErro = "Tempo Inválido de Execução!";
                        
            carregaModel(model);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private void carregaModel(RoboImportacaoModel model)
        {
            //WFDConfigBP configBP = new WFDConfigBP();

            model.UltimaExecucao = ThreadRoboImportacao.UltimaExecucao != DateTime.MinValue ? ThreadRoboImportacao.UltimaExecucao.ToString("dd/MM/yyyy HH:mm:ss") : null;
            model.ProximaExecucao = ThreadRoboImportacao.ProximaExecucao != DateTime.MinValue ? ThreadRoboImportacao.ProximaExecucao.ToString("dd/MM/yyyy HH:mm:ss") : null;
            model.TempoExecucao = configBP.formataTempo(ThreadRoboImportacao.TempoExecucao);
            model.Status = ThreadRoboImportacao.Status;
            model.Ativo = ThreadRoboImportacao.Ativo;
            model.Carregado = ThreadRoboImportacao.Carregado;
        }
       
    }
}