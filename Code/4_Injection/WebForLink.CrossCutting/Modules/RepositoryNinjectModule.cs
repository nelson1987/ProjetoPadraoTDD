using Ninject.Modules;
using WebForLink.Data.Repository.Dapper;
using WebForLink.Data.Repository.EntityFramework;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Repository.Common;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.CrossCutting.InversionControl.Modules
{
    public class RepositoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsuarioRepository>().To<UsuarioEntityRepository>();
            Bind<IUsuarioReadOnlyRepository>().To<UsuarioDapperRepository>();
            Bind<IReadOnlyRepository<Usuario>>().To<UsuarioDapperRepository>();
        }
    }
}