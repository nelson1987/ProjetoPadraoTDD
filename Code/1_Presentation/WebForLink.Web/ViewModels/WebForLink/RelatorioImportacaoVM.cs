using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class RelatorioImportacaoVM
    {
        public List<string> Colunas { get; set; }
        public List<LinhaPlanilhaModel> Linhas { get; set; }
    }
}