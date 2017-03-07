using System.Text;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels.Carga
{
    public class DesbloqueioCargaModel : DemandaSapModel
    {
        public DesbloqueioCargaModel() { }
        public DesbloqueioCargaModel(string codigoSolicitacao, string empresa, string organizacaoCompras, string codigoSap
            , string bloqueioEmpresasSelecionadas, string todasEmpresas, string todasOrganizacoesCompras, int funcaoBloqueio) 
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
        public int? FuncaoBloqueio { get; set; }

        public string GerarLinhaDesbloqueio()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.Desbloqueios + ";");
            //Campo 2 - Tamanho 6
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            //Campo 3 - Tamanho 4
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            //Campo 3 - Tamanho 4
            limitarTamanhoPropriedade(OrganizacaoCompras, 4, this.OrganizacaoCompras, Linha);
            //Campo 4 - Tamanho 10
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            //Campo 5 - Tamanho 1
            this.BloqueioEmpresaSelecionada = BloqueioEmpresaSelecionada == "x" ? "X" : null;
            limitarTamanhoPropriedade(BloqueioEmpresaSelecionada, 1, this.BloqueioEmpresaSelecionada, Linha);
            //Campo 6 - Tamanho 1
            this.TodasEmpresas = TodasEmpresas == "x" ? "X" : null;
            limitarTamanhoPropriedade(TodasEmpresas, 1, this.TodasEmpresas, Linha);
            //Campo 7 - Tamanho 1
            this.TodasOrganizacoesCompras = TodasOrganizacoesCompras == "x" ? "X" : null;
            limitarTamanhoPropriedade(TodasOrganizacoesCompras, 1, this.TodasOrganizacoesCompras, Linha);
            //Campo 8 - Tamanho 2
            //this.FuncaoBloqueio = FuncaoBloqueio != null ? "00" : null;
            limitarTamanhoPropriedade(FuncaoBloqueio.ToString(), 2, this.FuncaoBloqueio.ToString(), Linha);
            return Linha.ToString();
        }
    }

}