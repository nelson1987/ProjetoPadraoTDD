using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels
{
    public class UsuarioVM : TrocaSenhaEsqueceuVM
    {
        public UsuarioVM()
        {
            ListaContratantes = new List<SelectListItem>();
            ListaPerfis = new List<SelectListItem>();
            ListaPapeis = new List<SelectListItem>();
        }

        [MaxLength(255, ErrorMessage = "Tamanho máximo do campo {0} excedido")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Email { get; set; }

        [Display(Name = "Data de nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DtNascimento { get; set; }
        

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Remote("ValidarNomeLogin", "Validador", ErrorMessage = "Este login já existe")]
        [StringLength(30, ErrorMessage = "O campo {0} deve possuir no mínimo {2} caracteres", MinimumLength = 4)]
        public string Login { get; set; }

        public bool Principal { get; set; }

        public string Cargo { get; set; }

        public int IdContratante { get; set; }

        public List<SelectListItem> ListaContratantes { get; set; }
        
        public int IdPerfil { get; set; }

        public List<SelectListItem> ListaPerfis { get; set; }

        public int IdPapel { get; set; }

        public List<SelectListItem> ListaPapeis { get; set; }

        public EnumTipoCadastroNovoUsuario TipoCadastroNovoUsuario { get; set; }
    }
}