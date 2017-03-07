using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public class FornecedorBaseFuncionalidadeVM
    {
        [Display(Name = "ID")]
        public int ID { get; set; }
        
        public int ContratanteID { get; set; }

        public int TipoFornecedor { get; set; }

        public int? CategoriaId { get; set; }

        [Display(Name = "Categoria")]
        public string CategoriaNome { get; set; }

        public bool? ExecutaRobo { get; set; }

        [Display(Name = "CPF")]
        public string CPF { get; set; }
        
        public string Nome { get; set; }

        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        public bool Selecionado { get; set; }

        public bool Convidado { get; set; }

        public List<ColunaOpcionalVM> Colunas { get; set; }

        public string CelulaCSS { get; set; }

        public string CelulaTitulo { get; set; }

        public List<string> CelulaValor { get; set; }

        public bool? Respondido { get; set; }

        public bool Prorrogado { get; set; }

        public bool? Aprovado { get; set; }

        public bool? Bloqueado { get; set; }

        public string ProrrogarPara { get; set; }

        public string Motivo { get; set; }
    }
    public class ColunaOpcionalVM
    {
        public string CSS { get; set; }

        public string Titulo { get; set; }

        public string Valor { get; set; }
    }
}