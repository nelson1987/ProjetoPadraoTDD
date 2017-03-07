using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Areas.Administracao.Models;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.ViewModels.WebForLink
{
    public class FornecedoresVM : ViewModelPadrao
    {
        public FornecedoresVM()
        {
            Contatos = new List<FornecedorContatosVM>();
            Bancos = new List<BancosVM>();
            Contato = new FornecedorContatosVM();
        }

        public int ID { get; set; }

        public int ContratanteID { get; set; }

        [Display(Name = "Empresa")]
        public int Empresa { get; set; }

        [Display(Name = "Empresa")]
        public string NomeEmpresa { get; set; }

        [Display(Name = "Organização de Compras")]
        public int Compras { get; set; }

        [Required(ErrorMessage = "Informe a Categoria (Grupo de Contas)")]
        [Display(Name = "Categoria (Grupo de Contas)")]
        public int Categoria { get; set; }

        [Display(Name = "Nome/Razão Social/Nome Fantasia")]
        [Required(ErrorMessage = "Informe o Fornecedor (Razão Social)")]
        public string RazaoSocial { get; set; }

        [Display(Name = "CNPJ/CPF")]
        [Required(ErrorMessage = "Informe o CNPJ")]
        public string CNPJ { get; set; }

        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [Display(Name = "Nome Contato")]
        public string NomeContato { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe E-mail")]
        public string Email { get; set; }

        public bool Ativo { get; set; }

        public int Solicitacao { get; set; }

        public int TudoAmpliado { get; set; }

        public int Bloqueado { get; set; }

        public int TipoCadastro { get; set; }

        public int TipoFornecedor { get; set; }

        public string HiddenFornecedorId { get; set; }

        [Display(Name = "Data Nascimento")]
        public DateTime? DataNascimento { get; set; }

        public string CodigoERP { get; set; }
        [Display(Name = "Status")]
        public string Status { get; set; }
        
        public FornecedorContatosVM Contato { get; set; }
        public List<FornecedorContatosVM> Contatos { get; set; }
        public List<BancosVM> Bancos { get; set; }
        public virtual ICollection<AnexoVM> Anexos { get; set; }

        public RoboReceitaCNPJ RoboReceitaCNPJ { get; set; }
        public RoboReceitaCPF RoboReceitaCPF { get; set; }
        public RoboSintegra RoboSintegra { get; set; }
        public RoboSimples RoboSimples { get; set; }

        public ModelStateDictionary ValidarCriacaoFornecedor()
        {
            ModelStateDictionary ModelState = new ModelStateDictionary();
            ModelState.Clear();

            if (!string.IsNullOrEmpty(Email))
                if (!Validacao.ValidarEmail(Email))
                    ModelState.AddModelError("Contato.Email", "O e-mail informado não está em um formato válido.");

            if (Categoria == 0)
                ModelState.AddModelError("Categoria", "Informe a Categoria!");

            if (TipoFornecedor == 1 || TipoFornecedor == 3)
            {
                if (CNPJ == null)
                    ModelState.AddModelError("CNPJ", "CNPJ/CPF Obrigatório");
                else
                {
                    if (TipoFornecedor == 1)
                    {
                        if (!Validacao.ValidaCNPJ(CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "")))
                            ModelState.AddModelError("CNPJ", "CNPJ Inválido");
                    }
                    else
                    {
                        if (!Validacao.ValidaCPF(CNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "")))
                            ModelState.AddModelError("CNPJ", "CPF Inválido");
                    }
                }
            }

            return ModelState;
        }
    }
}