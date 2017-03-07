using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class AplicacaoAdministracaoModel : ViewModelPadrao
    {
        public AplicacaoAdministracaoModel()
        {
            FuncaoList = new HashSet<FuncaoAdministracaoModel>();
        }

        public int Id { get; set; }

        [DisplayName("Nome da aplicação")]
        [StringLength(20, ErrorMessage = "Nome não pode ter mais que 20 caracteres.")]
        public string Nome { get; set; }

        [DisplayName("Descrição da aplicação")]
        public string Descricao { get; set; }

        public ICollection<FuncaoAdministracaoModel> FuncaoList { get; set; }

        public void Validar()
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();

            UrlEditar = Url.Action("AplicacaoEditarFrm", "Aplicacao", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idAplicacao={0}", Id), Key)
            });

            UrlDetalhar = Url.Action("AplicacaoDetalharFrm", "Aplicacao", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idAplicacao={0}", Id), Key)
            });

            UrlExcluir = Url.Action("Delete", "Aplicacao", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idAplicacao={0}", Id), Key)
            });
        }
    }
}