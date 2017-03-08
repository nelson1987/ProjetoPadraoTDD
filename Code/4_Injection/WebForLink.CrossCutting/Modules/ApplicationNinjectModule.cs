using Ninject.Modules;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services;

namespace WebForLink.CrossCutting.InversionControl.Modules
{
    public class ApplicationServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IUsuarioAppService>().To<UsuarioAppService>();
        }
    }
}