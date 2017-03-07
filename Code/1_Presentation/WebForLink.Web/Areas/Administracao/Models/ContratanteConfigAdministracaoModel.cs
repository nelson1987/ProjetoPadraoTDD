namespace WebForLink.Web.Areas.Administracao.Models
{
    public class ContratanteConfigAdministracaoModel : ViewModelPadrao
    {
        public int ContratanteId { get; set; }

        public bool SolicitaDocumentos { get; set; }

        public bool SolicitaFichaCadastral { get; set; }

        public ContratanteAdministracaoModel Contratante { get; set; }
    }
}