namespace WebForLink.Web.ViewModels
{
    public class PerfilVM
    {
        public int Id { get; set; }
        public bool Obrigatorio { get; set; }
        public bool Leitura { get; set; }
        public bool Escrita { get; set; }
        public string PAPEL_NM { get; set; }
        public string PAPEL_DSC { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public ContratanteVM Contratante { get; set; }
    }
}