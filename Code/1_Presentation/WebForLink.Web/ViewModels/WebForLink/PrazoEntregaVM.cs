using System;

namespace WebForLink.Web.ViewModels
{
    public class PrazoEntregaVM
    {
        public PrazoEntregaVM()
        {
        }
        public PrazoEntregaVM(DateTime prazoentrega)
        {
            this.PrazoEntrega = prazoentrega;
            CriarMensagem();
        }
        public int Id { get; set; }
        public string MensagemEntrega { get; set; }
        public DateTime PrazoEntrega { get; set; }
        public void CriarMensagem()
        {
            this.MensagemEntrega = string.Format("O Prazo expira em: {0:dd/MM/yyyy}", this.PrazoEntrega);
        }
    }
}
