using System;

namespace WebForLink.Domain.Infrastructure.Exceptions
{
    [Serializable]
    public class ServiceWebForLinkException : Exception, IServiceWebForLinkException
    {
        public ServiceWebForLinkException(string message)
            : base(message)
        {
        }

        public ServiceWebForLinkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}