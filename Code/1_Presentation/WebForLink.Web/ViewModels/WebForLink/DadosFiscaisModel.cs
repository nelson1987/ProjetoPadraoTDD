using System.Text;
using WebForLink.Domain.Enums;
using WebForLink.Web.ViewModels.Carga;

namespace WebForLink.Web.ViewModels
{
    public class DadosFiscaisModel : DemandaSapModel
    {
        public string Categoria { get; set; }
        public string Codigo { get; set; }
        public string SujeitoA { get; set; }
        public string GerarLinhaModificarDadosFiscais()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.ModificacaoDadosFiscais + ";");
            //Campo 2 - Tamanho 6
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            //Campo 3 - Tamanho 4
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            //Campo 4 - Tamanho 10
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            //Campo 5 - Tamanho 2
            limitarTamanhoPropriedade(Categoria, 2, this.Categoria, Linha);
            //Campo 6 - Tamanho 2
            limitarTamanhoPropriedade(Codigo, 2, this.Codigo, Linha);
            //Campo 7 - Tamanho 1
            this.SujeitoA = SujeitoA != null ? "x" : "";
            limitarTamanhoPropriedade(SujeitoA, 1, this.SujeitoA, Linha);
            return Linha.ToString();
        }
    }
}