using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services
{
    public class CategoriaService : Service<Categoria>, ICategoriaService
    {
        public CategoriaService(ICategoriaRepository repository, ICategoriaReadOnlyRepository readOnlyRepository)
            : base(repository, readOnlyRepository)
        {
        }
    }
}