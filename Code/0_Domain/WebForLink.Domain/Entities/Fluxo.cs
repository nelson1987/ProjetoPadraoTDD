using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Entities
{
    public class Fluxo
    {
        private Fluxo()
        {
            Etapas = new List<Etapa>();
        }

        public Fluxo(TipoFluxo tipo, Contratante contratante, TipoEmpresa tipoEmpresa)
            : this()
        {
            Nome = tipo.Nome;
            TipoFluxo = tipo;
            Contratante = contratante;
            TipoEmpresa = tipoEmpresa;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public TipoFluxo TipoFluxo { get; private set; }
        public TipoEmpresa TipoEmpresa { get; private set; }
        public Contratante Contratante { get; private set; }
        public List<Etapa> Etapas { get; private set; }

        public Etapa EtapaAtual
        {
            get
            {
                foreach (var etapa in Etapas)
                {
                    foreach (var passo in etapa.Passos)
                    {
                        if (!passo.Aprovado)
                            return etapa;
                    }
                }
                return null;
            }
        }

        public void AdicionarPassos(Etapa etapa, params Passo[] passo1)
        {
            etapa.AdicionarPassos(passo1);
            Etapas.Add(etapa);
        }

        public void AprovarPasso(Passo passo)
        {
            var passoAprovado = EtapaAtual.Passos.FirstOrDefault(y => y.Descricao.Equals(passo.Descricao));
            if (passoAprovado != null)
                passoAprovado.Aprovar();
        }

        public void ReprovarPasso(Passo passo)
        {
            var passoAprovado = EtapaAtual.Passos.FirstOrDefault(y => y.Descricao.Equals(passo.Descricao));
            if (passoAprovado != null)
                passoAprovado.Reprovar();
        }

        public void AprovarPasso(Usuario aprovador, Etapa etapa, Passo aprovado)
        {
            if (EtapaAtual.Equals(etapa))
            {
                foreach (var item in Etapas)
                {
                    if (!item.Aprovado && item.Equals(etapa))
                    {
                        foreach (var passo in item.Passos)
                        {
                            if (!passo.Aprovado && passo.Equals(aprovado))
                            {
                                passo.Aprovar();
                                break;
                            }
                        }
                        break;
                    }
                }
                /*
                Passo passoAprovado = EtapaAtual.Passos.FirstOrDefault(y => y == aprovado);
                if (passoAprovado != null)
                    if (aprovador.Papeis.FirstOrDefault(x => passoAprovado.Papeis.Contains(x)) != null)
                        passoAprovado.SetAprovado(true);
                */
            }
        }
    }
}