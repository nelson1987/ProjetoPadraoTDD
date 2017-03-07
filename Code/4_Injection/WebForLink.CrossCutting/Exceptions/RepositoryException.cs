using System;

namespace WebForLink.CrossCutting.InversionControl.Exceptions
{
    public class RepositoryException : Exception
    {
        public RepositoryException(string mensagem) : base(mensagem)
        {
        }

        public RepositoryException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {
        }
    }
}
