using System;

namespace WebForLink.CrossCutting.InversionControl.Exceptions
{
    public class ServiceException : Exception
    {
        public ServiceException(string mensagem) : base(mensagem)
        {
        }

        public ServiceException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {
        }
    }
}