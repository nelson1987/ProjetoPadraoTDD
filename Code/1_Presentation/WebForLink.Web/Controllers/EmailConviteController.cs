using Ninject;
using System;
using System.Web.Http;
using WebForLink.Application.Interfaces;
using WebForLink.CrossCutting.InversionControl;

namespace WebForLink.Web.Controllers
{
    public class EmailConviteController : ApiController
    {
        readonly private IEmailAppService _service;
        public EmailConviteController()
        {
            var ioc = new IoC();
            _service = ioc.Kernel.Get<IEmailAppService>();
        }
        [HttpGet]
        [Route("EnviarEmail/{id}")]
        public string Get(int id)
        {
            try
            {
                _service.EnviarEmail("nelson.neto@chconsultoria.com.br");
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "Mensagem enviada";
        }
    }
}
