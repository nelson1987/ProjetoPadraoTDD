using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WebForLink.Web.ViewModels
{
    public class DadosImportacaoVM
    {
        public int? Categoria { get; set; }

        [Required(ErrorMessage = "Selecione um Arquivo Excel para Importação.")/*,
         FileExtensions(ErrorMessage = "Selecione um Arquivo com Extensão \".xls\" ou \".xlxs\".", Extensions = ".xls, .xlxs")*/]
        public HttpPostedFileBase Arquivo { get; set; }
    }
}