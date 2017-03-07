using System;

namespace WebForLink.CrossCutting.InversionControl.Exceptions
{
    [Serializable]
    public class WebForLinkException : Exception
    {
        public WebForLinkException() 
            : base()
        {

        }
        public WebForLinkException(string message) 
            : base(message)
        {

        }
        public WebForLinkException(string message, Exception innerException) 
            : base(message, innerException)
        {

        }
    }
}
