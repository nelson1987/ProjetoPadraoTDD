using OfficeOpenXml;
using System.Collections.Generic;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels
{
    public class LinhaPlanilhaModel
    {
        public EnumTiposErroPlanilha Erro { get; set; }
        public ExcelRow ExcelRow { get; set; }
        public List<CelulaPlanilhaModel> Celulas { get; set; }        
    }
}