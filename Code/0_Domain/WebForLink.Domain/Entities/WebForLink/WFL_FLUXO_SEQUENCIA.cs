namespace WebForLink.Domain.Entities.WebForLink
{
    public class FLUXO_SEQUENCIA
    {
        public int CONTRATANTE_ID { get; set; }
        public int FLUXO_ID { get; set; }
        public int SEQUENCIA { get; set; }
        public int PAPEL_ID_INI { get; set; }
        public int? PAPEL_ID_FIM { get; set; }
        public string FLUXO_ETAPA_NM { get; set; }
        public string FLUXO_ETAPA_DSC { get; set; }
        public string FLUXO_SEQ_ANTERIOR { get; set; }
        public int? GRUPO_ORIGEM { get; set; }
        public int? GRUPO_DESTINO { get; set; }
        public bool EXECUCAO_MANUAL { get; set; }
        public bool APROV_SEM_ROBO { get; set; }
        public bool BLOQ_INATIVO_RECEITA { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual Fluxo WFL_FLUXO { get; set; }
        public virtual Papel Papel { get; set; }
        public virtual Papel Papel1 { get; set; }
    }
}