using System;

namespace WebForLink.Domain.Infrastructure
{
    [Serializable]
    public class ProcessoLoginException : Exception
    {
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ProcessoLoginException(string message)
            : base(message)
        {
            //Log.Error(message);
        }

        public ProcessoLoginException(string message, Exception innerException)
            : base(message, innerException)
        {
            //Log.Error(message);
        }
    }
}