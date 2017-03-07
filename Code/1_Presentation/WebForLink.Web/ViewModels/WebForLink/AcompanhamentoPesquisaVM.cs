
namespace WebForLink.Web.ViewModels
{
    public class AcompanhamentoPesquisaVM
    {
        public string Cnpj { get; internal set; }
        public int CodigoSolicitacao { get; internal set; }
        public string Cpf { get; internal set; }
        public int GrupoId { get; internal set; }
        public bool? Pendentes { get; internal set; }
        public string RazaoSocial { get; internal set; }
        public int TipoSolicitacao { get; internal set; }
    }
}