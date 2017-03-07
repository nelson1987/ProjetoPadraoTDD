using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels
{
    public class RelatorioLeituraVM
    {
        private string mensagemSemCNPJouCPF = "Foi encontrada 1 linha sem CNPJ ou CPF. Segue o número:";
        private string mensagemComCNPJouCPFInvalido = "Foi encontrada 1 linha com CNPJ ou CPF em formato inválido. Segue o número:";

        public int TotalLinhas { get; set; }

        public string CodigosLinhasSemCNPJouCPF { get; set; }
        public string CodigosLinhasComCNPJouCPFInvalido { get; set; }

        public List<LinhaPlanilhaModel> LinhasComErro { get; set; }
        public List<LinhaPlanilhaModel> LinhasSemErro { get; set; }
        public List<LinhaPlanilhaModel> LinhasSemCNPJouCPF { get; set; }
        public List<LinhaPlanilhaModel> LinhasComCNPJouCPFInvalido { get; set; }

        public RelatorioLeituraVM()
        {        
        }

        public RelatorioLeituraVM(List<LinhaPlanilhaModel> linhas)
        {
            TotalLinhas = linhas.Count();

            LinhasComErro = linhas.Where(x => x.Erro != EnumTiposErroPlanilha.NaoAplicavel).ToList();
            LinhasSemErro = linhas.Where(x => x.Erro.Equals(EnumTiposErroPlanilha.NaoAplicavel)).ToList();

            LinhasSemCNPJouCPF = linhas.Where(x => x.Erro.Equals(EnumTiposErroPlanilha.SemCPFouCNPJ)).ToList();
            LinhasComCNPJouCPFInvalido = linhas.Where(x => x.Erro.Equals(EnumTiposErroPlanilha.CNPJouCPFInvalido)).ToList();

            if (LinhasSemCNPJouCPF.Any() && LinhasSemCNPJouCPF.Count() > 1)
                mensagemSemCNPJouCPF = string.Format("Foram encontradas {0} linhas sem CNPJ ou CPF. São elas:", LinhasSemCNPJouCPF.Count());

            if (LinhasComCNPJouCPFInvalido.Any() && LinhasComCNPJouCPFInvalido.Count() > 1)
                mensagemComCNPJouCPFInvalido = string.Format("Foram encontradas {0} linhas com CNPJ em formato válido. São elas:", LinhasComCNPJouCPFInvalido.Count());

            CodigosLinhasSemCNPJouCPF = string.Join(", ", LinhasSemCNPJouCPF.Select(x => x.ExcelRow.Row.ToString()).ToArray());
            CodigosLinhasComCNPJouCPFInvalido = string.Join(", ", LinhasComCNPJouCPFInvalido.Select(x => x.ExcelRow.Row.ToString()).ToArray());
        }

        public string MensagemSemCNPJouCPF 
        { 
            get { return mensagemSemCNPJouCPF; }
            set 
            {
                mensagemSemCNPJouCPF = value; 
            }  
        }

        public string MensagemComCNPJouCPFInvalido
        {
            get { return mensagemComCNPJouCPFInvalido; }
            set 
            {
                mensagemComCNPJouCPFInvalido = value;
            }
        }
    }
}