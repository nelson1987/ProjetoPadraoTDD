using System;

namespace WebForLink.Domain.Validation
{
    public class StatusSolicitacaoException : Exception
    {
        public StatusSolicitacaoException(string message) : base(message)
        {
        }
    }
}