using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForLink.Web.ViewModels
{
    public class SolicitacaoConviteAutoSaveVM
    {
        public int Id { get; set; }

        public string Cnpj { get; set; }

        public bool Preenchido { get; set; }

        public string RazaoSocial { get; set; }

        public string NomeFantasia { get; set; }

        public string IdCriptografado { get; set; }

        public int Status { get; set; }

        public List<BancoVM> Bancos { get; set; }

        public List<ContatoVM> Contatos { get; set; }

        public List<DocumentoAnexadoVM> DocumentoAnexados { get; set; }

        public List<EnderecoVM> Enderecos { get; set; }
    }
}