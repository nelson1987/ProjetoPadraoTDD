using System;

namespace WebForLink.CrossCutting.InversionControl.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string mensagem) : base(mensagem)
        {
        }

        public DomainException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {
        }
    }
}
