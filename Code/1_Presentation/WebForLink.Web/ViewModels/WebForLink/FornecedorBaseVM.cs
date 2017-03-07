using System;
using System.ComponentModel.DataAnnotations;
using WebForLink.Web.Areas.Administracao.Models;

namespace WebForLink.Web.ViewModels
{
    public class FornecedorBaseVM : ViewModelPadrao
    {
        public FornecedorBaseVM()
        {
        }

        [Display(Name = "ID")]
        public int ID { get; set; }

        public int ContratanteID { get; set; }

        public int TipoFornecedor { get; set; }

        [Display(Name = "Categoria")]
        public int? CategoriaId { get; set; }

        [Display(Name = "Categoria")]
        public string CategoriaNome { get; set; }

        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Display(Name = "Data Nascimento")]
        public DateTime? DataNascimento { get; set; }

        [Display(Name = "CNPJ")]
        public string CNPJ { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome Contato")]
        public string Nome { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [Display(Name = "CNAE")]
        public string CNAE { get; set; }

        [Display(Name = "Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "Inscrição Municipal")]
        public string InscricaoMunicipal { get; set; }

        public int TipoLogradouro { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string CEP { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        [Display(Name = "UF")]
        public string UF { get; set; }

        [Display(Name = "País")]
        public Nullable<int> Pais { get; set; }

        [Display(Name = "Nome Contato")]
        public string NomeContato { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public bool Ativo { get; set; }

        public ContratanteVM Contratante { get; set; }

        public DateTime DataImportacao { get; set; }

        public bool? ExecutaRobo { get; set; }

        public string DataConvite { get; set; }

        public string DataPrazo { get; set; }

        public int PlanilhaId { get; set; }
    }
}