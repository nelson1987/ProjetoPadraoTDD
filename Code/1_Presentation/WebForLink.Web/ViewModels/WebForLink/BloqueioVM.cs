using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public class BloqueioVM
    {
        [Required(ErrorMessage = "Necessário")]
        [Display(Name = "Bloqueio de Lançamento")]
        public String Lancamento { get; set; }
        [Display(Name = "Todas as Organizações de Compras")]
        public bool Compra { get; set; }
        public int Motivo { get; set; }
        public List<string> MotivoQualidade { get; set; }
        public string MotivoSolicitacao { get; set; }
    }
}