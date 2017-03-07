using System.Text;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels.Carga
{
    public class RetornoSapCargaModel : FornecedorCargaModel
    {
        public string CodigoFornecedorSAP { get; set; }
        public string CodigoRetorno { get; set; }
        public string DescricaoErro { get; set; }
        public string TextoRetorno { get; set; }
        public string GerarRetornoCriarFornecedor()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.Criacao + ";");
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            limitarTamanhoPropriedade(CodigoFornecedorSAP, 16, this.CodigoFornecedorSAP, Linha);
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            limitarTamanhoPropriedade(GrupoContas, 4, this.GrupoContas, Linha);
            limitarTamanhoPropriedade(OrganizacaoCompras, 4, this.OrganizacaoCompras, Linha);
            limitarTamanhoPropriedade(CNPJ, 16, this.CNPJ, Linha);
            limitarTamanhoPropriedade(CPF, 11, this.CPF, Linha);
            limitarTamanhoPropriedade(CodigoRetorno, 1, this.CodigoRetorno, Linha);
            limitarTamanhoPropriedade(DescricaoErro, 60, this.DescricaoErro, Linha);
            return Linha.ToString();
        }
        public string GerarRetornoAmpliarFornecedor()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.Ampliacao + ";");
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            limitarTamanhoPropriedade(CodigoRetorno, 1, this.CodigoRetorno, Linha);
            limitarTamanhoPropriedade(TextoRetorno, 60, this.TextoRetorno, Linha);
            return Linha.ToString();
        }
        public string GerarRetornoModificarDadosFiscais()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.ModificacaoDadosFiscais + ";");
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            limitarTamanhoPropriedade(CodigoRetorno, 1, this.CodigoRetorno, Linha);
            limitarTamanhoPropriedade(TextoRetorno, 60, this.TextoRetorno, Linha);
            return Linha.ToString();
        }
        public string GerarRetornoModificarDadosBancarios()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.DadosBancarios + ";");
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            limitarTamanhoPropriedade(CodigoRetorno, 1, this.CodigoRetorno, Linha);
            limitarTamanhoPropriedade(TextoRetorno, 60, this.TextoRetorno, Linha);
            return Linha.ToString();
        }
        public string GerarRetornoModificarDadosContatos()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.DadosBancarios + ";");
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            limitarTamanhoPropriedade(CodigoRetorno, 1, this.CodigoRetorno, Linha);
            limitarTamanhoPropriedade(TextoRetorno, 60, this.TextoRetorno, Linha);
            return Linha.ToString();
        }
        public string GerarRetornoBloqueio()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.DadosBancarios + ";");
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            limitarTamanhoPropriedade(CodigoRetorno, 1, this.CodigoRetorno, Linha);
            limitarTamanhoPropriedade(TextoRetorno, 60, this.TextoRetorno, Linha);
            return Linha.ToString();
        }
        public string GerarRetornoDesBloqueio()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.DadosBancarios + ";");
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            limitarTamanhoPropriedade(CodigoRetorno, 1, this.CodigoRetorno, Linha);
            limitarTamanhoPropriedade(TextoRetorno, 60, this.TextoRetorno, Linha);
            return Linha.ToString();
        }

    }
}