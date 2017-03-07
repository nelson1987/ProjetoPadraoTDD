namespace WebForLink.Web.ViewModels
{
    public class FornecedorBaseFiltroVM
    {
        public int ContratanteId { get; set; }
        public int? CategoriaId { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string CPF { get; set; }
        public string Nome { get; set; }
        public bool? Categorizados { get; set; }
        public bool? Convidados { get; set; }
        public bool? Validados { get; set; }
        public bool? Respondido { get; set; }
        public bool? Prorrogados { get; set; }
        public int? Aprovados { get; set; }
        public int? ArquivoImportado { get; set; }
        public int? Bloqueados { get; set; }
    }
}