namespace WebForLink.Domain.Entities
{
    public class Adesao
    {
        public Adesao(Usuario usuarioFornecedor)
        {
            UsuarioFornecedor = usuarioFornecedor;
        }
        public Adesao(Usuario usuarioFornecedor, int plano)
            : this(usuarioFornecedor)
        {
            UsuarioFornecedor = usuarioFornecedor;
            Plano = plano;
        }

        public Adesao(int plano, Categoria convite, Usuario usuarioFornecedor)
            : this(usuarioFornecedor, plano)
        {
            ConviteEnviado = convite;
        }

        public int? Plano { get; private set; }

        public Categoria ConviteEnviado { get; private set; }

        public Usuario UsuarioFornecedor { get; private set; }
    }
}
