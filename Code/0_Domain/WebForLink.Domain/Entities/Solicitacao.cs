using System;

namespace WebForLink.Domain.Entities
{
    public abstract class Solicitacao
    {
        private Solicitacao()
        {
        }

        protected Solicitacao(Usuario criador, Empresa solicitado)
            : this()
        {
            Criador = criador;
            Solicitado = solicitado;
        }

        public int Id { get; private set; }
        public bool Aprovado { get; private set; }
        public Empresa Solicitado { get; private set; }
        public Fluxo Fluxo { get; private set; }
        public Usuario Criador { get; private set; }

        public Contratante Contratante
        {
            get { return Criador.Contratante; }
        }

        public Etapa EtapaAtual
        {
            get { return Fluxo.EtapaAtual; }
        }
        
        public void SetFluxo(Fluxo fluxo)
        {
            if (fluxo.Contratante != Contratante)
                throw new Exception("O contratante da solicitação não é o mesmo do fluxo.");

            Fluxo = fluxo;
        }

        public void Aprovar(Usuario aprovador, Etapa etapa, Passo aprovado)
        {
            Fluxo.AprovarPasso(aprovador, etapa, aprovado);
        }
    }
}