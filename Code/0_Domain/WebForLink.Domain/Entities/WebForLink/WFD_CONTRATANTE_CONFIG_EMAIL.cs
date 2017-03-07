namespace WebForLink.Domain.Entities.WebForLink
{
    public class CONTRATANTE_CONFIGURACAO_EMAIL
    {
        public int CONTRATANTE_ID { get; set; }
        public int EMAIL_TP_ID { get; set; }
        public string ASSUNTO { get; set; }
        public string CORPO { get; set; }
        public virtual Contratante WFD_CONTRATANTE { get; set; }
        public virtual TIPO_EMAIL WFD_T_TP_EMAIL { get; set; }
    }
}