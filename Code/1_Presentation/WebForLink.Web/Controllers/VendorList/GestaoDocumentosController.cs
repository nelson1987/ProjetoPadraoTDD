using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebForLink.Web.Controllers.Extensoes;

namespace WebForLink.Web.Controllers.VendorList
{
    public interface IGestaoDocumentosViewModel
    {
        List<SelectListItem> Tipos { get; set; }
        List<SelectListItem> Descricoes { get; set; }
        DateTime DataEmissao { get; set; }
        DateTime DataValidade { get; set; }
        bool TemValidade { get; set; }
        bool Ativo { get; set; }
        List<HttpPostedFileBase> File { get; set; }

    }
    public class PesquisaDocumentoViewModel
    {

    }
    public class IncluirDocumentoViewModel : IGestaoDocumentosViewModel
    {
        public bool Ativo { get; set; }

        public DateTime DataEmissao { get; set; }

        public DateTime DataValidade { get; set; }

        public List<SelectListItem> Descricoes { get; set; }

        public List<HttpPostedFileBase> File { get; set; }

        public bool TemValidade { get; set; }

        public List<SelectListItem> Tipos { get; set; }
    }

    public class GestaoDocumentosController : ControllerPadrao
    {
        public GestaoDocumentosController()
        {
        }
        // GET: GestaoDocumentos
        public ActionResult Index()
        {
            ViewBag.TipoDocumento = "";
            ViewBag.DescricaoDocumento = "";
            return View();
        }
        // GET: GestaoDocumentos
        public ActionResult Incluir()
        {
            ViewBag.TipoDocumento = "";
            ViewBag.DescricaoDocumento = "";
            return View();
        }
        // GET: GestaoDocumentos
        [HttpPost]
        public ActionResult Incluir(IGestaoDocumentosViewModel modelo)
        {
            foreach (var file in modelo.File)
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
                    file.SaveAs(path);
                }
            }
            ViewBag.TipoDocumento = "";
            ViewBag.DescricaoDocumento = "";
            return View();
        }
    }
}