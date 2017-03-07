using System;
using System.Data.Entity.Infrastructure.Interception;
using System.Diagnostics;

namespace WebForLink.Web.Controllers.Extensoes
{
    public class WFDSqlLogger : DbCommandInterceptor
    {
        private static readonly log4net.ILog Logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private delegate void ExecutingMethod<T>(System.Data.Common.DbCommand command, DbCommandInterceptionContext<T> interceptionContext);

        public override void NonQueryExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
        {
            CommandExecuting(base.NonQueryExecuting, command, interceptionContext);
        }

        public override void ReaderExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<System.Data.Common.DbDataReader> interceptionContext)
        {
            CommandExecuting(base.ReaderExecuting, command, interceptionContext);
        }

        public override void ScalarExecuting(System.Data.Common.DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
        {
            CommandExecuting(base.ScalarExecuting, command, interceptionContext);
        }

        private void CommandExecuting<T>(ExecutingMethod<T> executingMethod, System.Data.Common.DbCommand command, DbCommandInterceptionContext<T> interceptionContext)
        {
            Stopwatch sw = Stopwatch.StartNew();
            executingMethod.Invoke(command, interceptionContext);
            sw.Stop();

            if (interceptionContext.Exception != null)
            {
                Logger.Error(String.Format("Error executing command: {0}", command.CommandText),interceptionContext.Exception);
            }
            else
            {
                Logger.Info(String.Format("{0} took {1}", command.CommandText, sw.Elapsed));
            }
        }

    }
}