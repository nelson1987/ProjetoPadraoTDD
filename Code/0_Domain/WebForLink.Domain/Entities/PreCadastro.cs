using System.Collections.Generic;
using WebForLink.Domain.Entities.Categorias;
using WebForLink.Domain.Entities.Status;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Entities
{
    public abstract class PreCadastro
    {
        private PreCadastro()
        {
            Contratantes = new List<Contratante>();
            FichaCadastral = new FichaEmpresa();
        }

        protected PreCadastro(string razaoSocial, string documento, TipoEmpresa tipo)
            : this()
        {
            RazaoSocial = razaoSocial;
            Documento = documento;
            Tipo = tipo;
        }

        public int Id { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Documento { get; private set; }
        public TipoEmpresa Tipo { get; private set; }
        public StatusEmpresa Status { get; private set; }
        public List<Contratante> Contratantes { get; private set; }
        public CategoriaEmpresa Categoria { get; private set; }
        public FichaCadastral FichaCadastral { get; private set; }

        public void SetTipoEmpresa(TipoEmpresa tipoEmpresa)
        {
            Tipo = tipoEmpresa;
        }

        public void SetStatusEmpresa(StatusEmpresa statusEmpresa)
        {
            Status = statusEmpresa;
        }

        public void AdicionarContato(Contato contato)
        {
            FichaCadastral.Contatos.Add(contato);
        }

        public void AdicionarDocumento(Documento documento)
        {
            FichaCadastral.Anexos.Add(documento);
        }
    }
}