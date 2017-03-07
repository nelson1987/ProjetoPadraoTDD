using System;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public class FornecedorRoboVM
    {
        public int ID { get; set; }
        public DateTime Robo_Dt_Exec { get; set; }
        public int? Solicitacao_ID { get; set; }
        public string Rf_Certificado_Html { get; set; }
        public string Sint_Certificado_Html { get; set; }
        
        [Display(Name = "Simples Nacional")]
        public string SimplesNacionalSituacao { get; set; }
    }
}