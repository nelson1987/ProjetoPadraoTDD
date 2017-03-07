using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using WebForLink.Domain.Entities;

namespace WebForLink.Web.ViewModels
{
    public class EnviarEmailSolicitacaoVM
    {
        private string cnpj;
        private string mensagem;

        public EnviarEmailSolicitacaoVM()
        {
            TipoDocumento = new List<SelectListItem> {new SelectListItem {Text = "--Selecione", Value = "0"}};
            DescricaoDocumento = new List<SelectListItem> {new SelectListItem {Text = "--Selecione", Value = "0"}};
            DocumentoList = new List<EnviarEmailSolicitacaoGridVM>();
            ResponsavelList = new Dictionary<string, string>();
        }

        public string LoginUsuario { get; set; }
        public string CodigoCliente { get; set; }
        public int IdFornecedor { get; set; }

        [DisplayName("Cnpj")]
        public string Cnpj
        {
            get
            {
                if (string.IsNullOrEmpty(cnpj))
                    return "";
                return string.Format("{0}.{1}.{2}/{3}-{4}", cnpj.Substring(0, 2)
                    , cnpj.Substring(2, 3)
                    , cnpj.Substring(5, 3)
                    , cnpj.Substring(8, 4)
                    , cnpj.Substring(12, 2));
                ;
            } //83.310.441/0029-18
            set { cnpj = value; }
        }

        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }

        [DisplayName("Assunto")]
        public string Assunto { get; set; }

        [DisplayName("Mensagem")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Mensagem
        {
            get
            {
                if (string.IsNullOrEmpty(mensagem))
                    return "";
                return mensagem
                    .Replace("&lt;", "<")
                    .Replace("&gt;", ">");
            }
            set { mensagem = value; }
        }

        [DisplayName("Tipo de Documento")]
        public int TipoId { get; set; }

        [DisplayName("Descrição do Documento")]
        public int DescricaoId { get; set; }

        [DisplayName("Tipo de Documento")]
        public List<SelectListItem> TipoDocumento { get; set; }

        [DisplayName("Descrição do Documento")]
        public List<SelectListItem> DescricaoDocumento { get; set; }

        public List<EnviarEmailSolicitacaoGridVM> DocumentoList { get; set; }
        public Dictionary<string, string> ResponsavelList { get; set; }

        public static Solicitacao ToModel(EnviarEmailSolicitacaoVM solicitacao)
        {
            return Mapper.Map<Solicitacao>(solicitacao);
        }

        public static EnviarEmailSolicitacaoVM ToViewModel(Solicitacao solicitacao)
        {
            return Mapper.Map<EnviarEmailSolicitacaoVM>(solicitacao);
        }
    }
}