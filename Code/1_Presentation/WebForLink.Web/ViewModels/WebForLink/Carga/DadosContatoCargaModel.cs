using System.Text;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels.Carga
{
    public class DadosContatoCargaModel : DemandaSapModel
    {
        public DadosContatoCargaModel() { }
        public DadosContatoCargaModel(string codigoSolicitacao, string empresa, string codigoSAP, string organizacaoCompras
            , string nome, string eMail, string telefone, string celular)
        {
            this.CodigoSolicitacao = codigoSolicitacao;
            this.Empresa = empresa;
            this.CodigoSAP = codigoSAP;
            this.OrganizacaoCompras = organizacaoCompras;
            this.Nome = nome;
            this.EMail = eMail;
            this.Telefone = telefone;
            this.Celular = celular;
        }
        public string Nome { get; set; }
        public string EMail { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public string GerarLinhaModificarDadosContato()
        {

            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.DadosContato + ";");
            //Campo 2 - Tamanho 6
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            //Campo 3 - Tamanho 4
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            //Campo 4 - Tamanho 10
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            //Campo 4 - Tamanho 10
            limitarTamanhoPropriedade(OrganizacaoCompras, 4, this.OrganizacaoCompras, Linha);
            //Campo 5 - Tamanho 30
            limitarTamanhoPropriedade(Nome, 30, this.Nome, Linha);
            //Campo 6 - Tamanho 241
            limitarTamanhoPropriedade(EMail, 241, this.EMail, Linha);
            //Campo 7 - Tamanho 33
            limitarTamanhoPropriedade(Telefone, 33, this.Telefone, Linha);
            //Campo 7 - Tamanho 30
            limitarTamanhoPropriedade(Celular, 30, this.Celular, Linha);
            return Linha.ToString();
        }

        public int TipoContato { get; set; }
    }
}