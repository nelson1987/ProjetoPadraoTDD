using System;
namespace WebForLink.Web.Exceptions
{
    [Serializable]
    public class WebForLinkException : Exception
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public WebForLinkException(string message)
            : base(message)
        {
            log.Warn(message);
        }
        public WebForLinkException(string message, Exception innerException)
            : base(message, innerException)
        {
            log.Warn(message);
        }
    }
}