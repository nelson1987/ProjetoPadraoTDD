using System.Text;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels.Carga
{
    public class BloqueioCargaModel : DemandaSapModel
    {
        public BloqueioCargaModel() { }
        public BloqueioCargaModel(string codigoSolicitacao, string empresa, string organizacaoCompras, string codigoSap
            , string bloqueioEmpresasSelecionadas, string todasEmpresas, string todasOrganizacoesCompras,string funcaoBloqueio) 
        {
            this.CodigoSolicitacao = codigoSolicitacao;
            this.Empresa = empresa;
            this.OrganizacaoCompras = organizacaoCompras;
            this.CodigoSAP = codigoSap;
            this.BloqueioEmpresaSelecionada = bloqueioEmpresasSelecionadas;
            this.TodasEmpresas = todasEmpresas;
            this.TodasOrganizacoesCompras = todasOrganizacoesCompras;
            this.FuncaoBloqueio = funcaoBloqueio;
        }
        public string BloqueioEmpresaSelecionada { get; set; }
        public string TodasEmpresas { get; set; }
        public string TodasOrganizacoesCompras { get; set; }
        public string FuncaoBloqueio { get; set; }
        public string GerarLinhaBloqueio()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.Bloqueio + ";");
            //Campo 2 - Tamanho 6
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            //Campo 3 - Tamanho 4
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            //Campo 3 - Tamanho 4
            limitarTamanhoPropriedade(OrganizacaoCompras, 4, this.OrganizacaoCompras, Linha);
            //Campo 4 - Tamanho 10
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            //Campo 5 - Tamanho 1
            this.BloqueioEmpresaSelecionada = BloqueioEmpresaSelecionada != null ? "x" : "";
            limitarTamanhoPropriedade(BloqueioEmpresaSelecionada, 1, this.BloqueioEmpresaSelecionada, Linha);
            //Campo 6 - Tamanho 1
            this.TodasEmpresas = TodasEmpresas != null ? "x" : "";
            limitarTamanhoPropriedade(TodasEmpresas, 1, this.TodasEmpresas, Linha);
            //Campo 7 - Tamanho 1
            this.TodasOrganizacoesCompras = TodasOrganizacoesCompras != null ? "x" : "";
            limitarTamanhoPropriedade(TodasOrganizacoesCompras, 1, this.TodasOrganizacoesCompras, Linha);
            //Campo 8 - Tamanho 2
            limitarTamanhoPropriedade(FuncaoBloqueio, 2, this.FuncaoBloqueio, Linha);
            return Linha.ToString();
        }
    }
}