using System.Collections.Generic;

namespace WebForLink.Domain.Entities.Status
{
    public class StatusEmpresa
    {
        private StatusEmpresa()
        {
        }

        public StatusEmpresa(string nome) : this()
        {
            Nome = nome;
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }
        public List<Empresa> Empresas { get; private set; }
    }
}