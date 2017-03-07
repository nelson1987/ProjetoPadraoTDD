using System.Linq;
using System.Text;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels.Carga
{
    public class RevalidacaoCadastroCargaModel : DemandaSapModel
    {
        public string Nome1 { get; set; }
        public string Nome2 { get; set; }
        public string Nome3 { get; set; }
        public string Nome4 { get; set; }
        public string NomeFantasia { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string GerarLinhaRevalidarCadastro()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.RevalidacaoCadastros + ";");
            //Campo 2 - Tamanho 6
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            //Campo 3 - Tamanho 10
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            //Campo 7 - Tamanho 35
            string inde = Nome1.Substring(0, (Nome1.IndexOf(Nome1.Substring(0, 35).Trim().Split().LastOrDefault()) - 1));
            limitarTamanhoPropriedade(inde, 35, this.Nome1, Linha);
            ////Campo 8 - Tamanho 35
            string inde2 = Nome1.Substring((Nome1.IndexOf(Nome1.Substring(0, 35).Trim().Split().LastOrDefault())), 35);
            limitarTamanhoPropriedade(inde2, 35, this.Nome2, Linha);
            //Campo 8 - Tamanho 20
            limitarTamanhoPropriedade(NomeFantasia, 20, this.NomeFantasia, Linha);
            //Campo 9 - Tamanho 60
            limitarTamanhoPropriedade(Rua, 60, this.Rua, Linha);
            //Campo 9 - Tamanho 60
            limitarTamanhoPropriedade(Numero, 60, this.Numero, Linha);
            //Campo 10 - Tamanho 10
            limitarTamanhoPropriedade(Complemento, 10, this.Complemento, Linha);
            //Campo 11 - Tamanho 10
            limitarTamanhoPropriedade(Bairro, 10, this.Bairro, Linha);
            //Campo 12 - Tamanho 10
            limitarTamanhoPropriedade(CEP, 10, this.CEP, Linha);
            //Campo 13 - Tamanho 35
            limitarTamanhoPropriedade(Cidade, 35, this.Cidade, Linha);
            //Campo 14 - Tamanho 2
            limitarTamanhoPropriedade(Estado, 2, this.Estado, Linha);
            //Campo 15 - Tamanho 18
            limitarTamanhoPropriedade(InscricaoEstadual, 18, this.InscricaoEstadual, Linha);
            //Campo 16 - Tamanho 18
            limitarTamanhoPropriedade(InscricaoMunicipal, 18, this.InscricaoMunicipal, Linha);
            return Linha.ToString();
        }
    }
}