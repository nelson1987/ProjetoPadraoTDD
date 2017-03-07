using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebForDocs.ViewModels
{
    public class DadosContatosVM
    {
        public int ContatoID { get; set; }
        
        [DisplayName("Nome Contato")]
        public string NomeContato { get; set; }
        
        [DisplayName("E-mail")]
        public string EmailContato { get; set; }
        
        [DisplayName("Telefone")]
        //[Required(ErrorMessage="Telephone Number Required")]
        //[RegularExpression(@"^\(?\d{2}\)?\d{4,5}-?\d{4}$", ErrorMessage = "Entered phone format is not valid.")] // Com máscara
        
        [RegularExpression(@"^\d{2}?\d{4,5}?\d{4}$", ErrorMessage = "Entered phone format is not valid.")] //Sem máscara
        public string Telefone { get; set; }
        
        [DisplayName("Celular")]
        public string Celular { get; set; }
    }
}