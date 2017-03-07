using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels
{
    public class CelulaPlanilhaModel
    {
        public EnumTiposErroPlanilha Erro { get; set; }
        public int Linha { get; set; }
        public int Coluna { get; set; }
        public object ValorOriginal { get; set; }
        public string ValorManipulado { get; set; }
        public string Endereco { get; set; }
        public string ClasseCSS { get; set; }
    }
}