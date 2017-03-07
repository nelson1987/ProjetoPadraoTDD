using System;
using System.ComponentModel;

namespace WebForLink.Web.ViewModels
{
    public class ContratanteVM
    {
        public int Id { get; set; }
        public int TipoCadastroId { get; set; }
        public string CNPJ { get; set; }

        [DisplayName("Empresa")]
        public string RAZAO_SOCIAL { get; set; }

        public string NomeFantasia { get; set; }
        public DateTime DataCadastro { get; set; }
        public string LogoFoto { get; set; }
        public string ExtensaoImagem { get; set; }
        public string Estilo { get; set; }
        public string ContratanteCodigoERP { get; set; }
        public string Nome { get; set; }

    }
}