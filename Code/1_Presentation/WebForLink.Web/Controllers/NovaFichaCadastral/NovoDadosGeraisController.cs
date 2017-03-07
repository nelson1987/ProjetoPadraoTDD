using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebForLink.Web.Controllers.NovaFichaCadastral
{
    public class NovoDadosGeraisVM
    {
        public NovoDadosGeraisVM()
        {
            Documento = "1234";
        }
        public int ContratanteId { get; set; }
        public int FornecedorId { get; set; }

        [DisplayName("CNPJ/CPF")]
        public string Documento { get; set; }
        [DisplayName("Razão Socia")]
        public string RazaoSocial { get; set; }
        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }
        [DisplayName("CNAE")]
        public string Cnae { get; set; }
        [DisplayName("Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }
        [DisplayName("Inscrição Municipal")]
        public string InscricaoMunicipal { get; set; }
    }
    public class NovoDadosGeraisController : Controller
    {
        // GET: NovoDadosGerais
        public ActionResult Detalhar()
        {
            return View(new NovoDadosGeraisVM());
        }
        // GET: NovoDadosGerais
        public ActionResult Criar()
        {
            return View(new NovoDadosGeraisVM());
        }
    }
}