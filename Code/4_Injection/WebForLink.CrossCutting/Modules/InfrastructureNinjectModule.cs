using Ninject.Modules;
using WebForLink.Data.Context;
using WebForLink.Data.Context.Interfaces;

namespace WebForLink.CrossCutting.InversionControl.Modules
{
    public class InfrastructureNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDbContext>().To<WebForLinkContexto>();
            Bind(typeof (IContextManager<>)).To(typeof (ContextManager<>));
            Bind(typeof (IUnitOfWork<>)).To((typeof (UnitOfWork<>)));
        }
    }
}