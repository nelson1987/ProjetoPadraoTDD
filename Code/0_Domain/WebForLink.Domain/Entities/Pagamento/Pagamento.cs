namespace WebForLink.Domain.Entities.Pagamento
{
    public class Pagamento
    {
        public Pagamento()
        {

        }

        public Pagamento(string Status, string CodigoTransacao, string CodigoReferencia, string Valor, string MeioPagamento)
        {

        }

        public string Status { get; private set; }
        public string CodigoTransacao { get; private set; }
        public string CodigoReferencia { get; private set; }
        public string Valor { get; private set; }
        public string MeioPagamento { get; private set; }
    }
}
