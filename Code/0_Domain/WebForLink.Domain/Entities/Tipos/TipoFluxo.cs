namespace WebForLink.Domain.Entities.Tipos
{
    public abstract class TipoFluxo
    {
        private TipoFluxo()
        {
        }

        protected TipoFluxo(string nome)
            : this()
        {
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
    }

    public class FluxoCriacao : TipoFluxo
    {
        public FluxoCriacao()
            : base("Fluxo de Criação")
        {
        }
    }

    public class FluxoAlteracao : TipoFluxo
    {
        public FluxoAlteracao(string nome)
            : base("Fluxo de Alteração")
        {
        }
    }

    public class FluxoExclusao : TipoFluxo
    {
        public FluxoExclusao()
            : base("Fluxo de Exclusão")
        {
        }
    }

    public class FluxoExpansao : TipoFluxo
    {
        public FluxoExpansao()
            : base("Fluxo de Expansão")
        {
        }
    }

    public class FluxoCustomizado : TipoFluxo
    {
        public FluxoCustomizado()
            : base("Fluxo Customizado")
        {
        }
    }
}