using System;
using System.Net;
using log4net;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Exceptions;

namespace WebForLink.Web.Infrastructure
{
    public class PadraoController : BaseController
    {
        public static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();

        public void TratamentoErro(Exception ex)
        {
            WebException web = new WebException(ex.Message);
            ProcessoLoginException dominio = new ProcessoLoginException(ex.Message);
            //WFLBusinessException bi = new WFLBusinessException(ex.Message);

            if (ex.GetType().Equals(web.GetType()) || ex.GetType().Equals(dominio.GetType()))
                //|| ex.GetType().Equals(bi.GetType())
                ModelState.AddModelError("", ex);

            Log.Error("Erro", ex);
        }
    }
    public class PadraoController<T> : PadraoController
    {
        public T Repositorio { get; set; }

        public PadraoController(T repositorio)
        {
            this.Repositorio = repositorio;
        }
        public PadraoController()
        {
        }
    }
}