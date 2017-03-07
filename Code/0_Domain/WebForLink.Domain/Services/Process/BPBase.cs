using log4net;
using System.Reflection;
using WebForDocs.Data.ModeloDB;
using WebForDocs.Dominio.Models;

namespace WebForDocs.Business.Process
{
    public class BPBase
    {
        public WFLModel Db = new WFLModel();
        public static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
    }
}
