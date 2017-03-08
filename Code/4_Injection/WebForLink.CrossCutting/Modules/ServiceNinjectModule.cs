using Ninject.Modules;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;
using WebForLink.Domain.Services.Process;

namespace WebForLink.CrossCutting.InversionControl.Modules
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof (IService<>)).To(typeof (Service<>));

            Bind<IUsuarioService>().To<UsuarioService>();

            Bind<IEmpresaService>().To<EmpresaService>();
        }
    }
}