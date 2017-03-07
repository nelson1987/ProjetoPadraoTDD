using CommonServiceLocator.NinjectAdapter.Unofficial;
using Microsoft.Practices.ServiceLocation;
using Ninject;
using WebForLink.CrossCutting.InversionControl.Modules;

namespace WebForLink.CrossCutting.InversionControl
{
    public class IoC
    {
        public IoC()
        {
            Kernel = GetNinjectModules();
            ServiceLocator.SetLocatorProvider(() => new NinjectServiceLocator(Kernel));
        }

        public IKernel Kernel { get; private set; }

        private StandardKernel GetNinjectModules()
        {
            return new StandardKernel(
                new ServiceNinjectModule(),
                new InfrastructureNinjectModule(),
                new RepositoryNinjectModule(),
                new ApplicationServiceNinjectModule());
        }
    }
}