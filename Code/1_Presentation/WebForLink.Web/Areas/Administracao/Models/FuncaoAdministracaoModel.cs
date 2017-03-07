using System.Collections.Generic;
using System.ComponentModel;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class FuncaoAdministracaoModel : ViewModelPadrao
    {
        public FuncaoAdministracaoModel()
        {
            FuncaoList = new List<FuncaoAdministracaoModel>();
            PerfilList = new HashSet<PerfilAdministracaoModel>();
        }

        public int Id { get; set; }

        [DisplayName("Código")]
        public string Codigo { get; set; }

        [DisplayName("Aplicação")]
        public int AplicacaoId { get; set; }

        public string Nome { get; set; }

        public string Tela { get; set; }

        [DisplayName("Descrição da função")]
        public string Descricao { get; set; }

        [DisplayName("Função pai")]
        public int? FuncaoPaiId { get; set; }

        public int? PerfilId { get; set; }

        public bool Selecionado { get; set; }

        public AplicacaoAdministracaoModel Aplicacao { get; set; }

        public List<FuncaoAdministracaoModel> FuncaoList { get; set; }

        public FuncaoAdministracaoModel FuncaoPai { get; set; }

        public ICollection<PerfilAdministracaoModel> PerfilList { get; set; }

        public void Validar()
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            UrlEditar = Url.Action("FuncaoEditarFrm", "Funcao", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}", Id), Key)
            });

            UrlDetalhar = Url.Action("FuncaoDetalharFrm", "Funcao", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}", Id), Key)
            });

            UrlExcluir = Url.Action("Delete", "Funcao", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}", Id), Key)
            });
        }
    }
}
