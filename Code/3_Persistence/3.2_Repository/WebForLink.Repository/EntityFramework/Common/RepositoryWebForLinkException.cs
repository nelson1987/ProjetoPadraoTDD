using System;

namespace WebForLink.Repository.Infrastructure
{
    [Serializable]
    public class RepositoryWebForLinkException : Exception//, IRepositoryWebForLinkException
    {
        public RepositoryWebForLinkException()
        {
        }

        public RepositoryWebForLinkException(string message)
            : base(message)
        {
        }

        public RepositoryWebForLinkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}