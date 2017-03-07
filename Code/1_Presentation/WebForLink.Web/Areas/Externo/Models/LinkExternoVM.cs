using System.ComponentModel.DataAnnotations;
using WebForLink.Domain.Enums;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Areas.Externo.Models
{
        public class LinkExternoVM
        {
            public LinkExternoVM()
            {
            }
            public int IdContratante { get; set; }
            public string NomeContratante { get; set; }
            public string Link { get; set; }

            public string Chave { get; set; }
        }

    public class InclusaoLinkExternoVM : LinkExternoVM
{
    public InclusaoLinkExternoVM()
    {
        isCNPJ = true;
    }
    public InclusaoLinkExternoVM(int idContratante, string chave, string url)
    {
        FichaCadastral = new FichaCadastralWebForLinkVM(idContratante, CasosPreCadastroEnum.CadastradoOutroContratante);
        FichaCadastral.ChaveUrl = url;
        IdContratante = idContratante;
        Chave = chave;
        isValidarSenha = false;
        Link = url;
        isCNPJ = true;
    }
    public FichaCadastralWebForLinkVM FichaCadastral { get; set; }

    [Required(ErrorMessage = "O campo CPF é obrigatório")]
    public string CPF { get; set; }

    [Required(ErrorMessage = "O campo CNPJ é obrigatório")]
    public string CNPJ { get; set; }

    [Required(ErrorMessage = "O campo Senha é obrigatório")]
    [DataType(DataType.Password)]
    public string Senha { get; set; }
    public bool isValidarSenha { get; set; }
    public bool isCNPJ { get; set; }
}
}