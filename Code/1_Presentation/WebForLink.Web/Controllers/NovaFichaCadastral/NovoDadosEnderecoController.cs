using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebForLink.Web.Controllers.NovaFichaCadastral
{
    public class NovoDadosEnderecoVM
    {
        [DisplayName("Tipo de Endereço")]
        public string TipoEndereco { get; set; }

        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        [DisplayName("Número")]
        public string Numero { get; set; }

        [DisplayName("Complemento")]
        public string Complemento { get; set; }

        [Required]
        [DisplayName("CEP")]
        public string Cep { get; set; }

        [DisplayName("Bairro")]
        public string Bairro { get; set; }

        [DisplayName("Cidade")]
        public string Cidade { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; }

        [DisplayName("País")]
        public string Pais { get; set; }
    }
    public class NovoDadosEnderecoController : Controller
    {
        // GET: NovoDadosEndereco
        public ActionResult Criar()
        {
            return View();
        }
        public ActionResult Adicionar(NovaFichaCadastralVM model)
        {
            if (model.DadosEndereco == null)
                model.DadosEndereco = new List<NovoDadosEnderecoVM>();
            model.DadosEndereco.Add(new NovoDadosEnderecoVM());// { IdFichaCadastral = model.IdFichaCadastral });
            return PartialView("~/Views/NovoDadosEndereco/_DadosEnderecoFrm.cshtml", model.DadosEndereco);
        }
    }
}