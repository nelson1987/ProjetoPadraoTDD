namespace WebForLink.Web.ViewModels
{
    
    public class PapelModel
    {
        public int Id { get; set; }
        public int CONTRATANTE_ID { get; set; }
        public string PAPEL_SGL { get; set; }
        public string PAPEL_NM { get; set; }
        public ContratanteVM Contratante { get; set; }
    }

}