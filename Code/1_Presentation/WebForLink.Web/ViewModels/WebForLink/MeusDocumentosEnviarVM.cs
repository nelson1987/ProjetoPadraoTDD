using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class MeusDocumentosEnviarVM
    {
        public MeusDocumentosEnviarVM() 
        {
            EnviarFichaCadastral = false;
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "Digite um Assunto")]
        public string Assunto { get; set; }

        [Required(ErrorMessage = "Digite uma Mensagem")]
        public string Mensagem { get; set; }

        public bool SemPrazo { get; set; }

        [Display(Name = "Enviar Ficha Cadastral")]
        public bool EnviarFichaCadastral { get; set; }

        [Display(Name = "Prazo de Disponibilidade dos Documentos")]
        public DateTime? DataValidade { get; set; }

        [Required(ErrorMessage = "Informe ao menos um email")]
        public string Para { get; set; }

        public string Documentos { get; set; }
        public List<DocumentosDoFornecedor> MeusDocumentos { get; set; }
        
        public List<DadosContatoVM> DadosContatos { get; set; }
        
        public List<DadosEnderecosVM> DadosEnderecos { get; set; }
        
        public List<DadosBancariosVM> DadosBancarios { get; set; }
    }
}