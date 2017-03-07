using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class PerfilAdministracaoModel : ViewModelPadrao
    {
        public PerfilAdministracaoModel()
        {
            FuncaoList = new List<FuncaoAdministracaoModel>();
            UsuarioList = new HashSet<UsuarioAdministracaoModel>();
        }

        public int Id { get; set; }

        [DisplayName("Nome do perfil")]
        public string Nome { get; set; }

        [DisplayName("Descrição do perfil")]
        public string Descricao { get; set; }

        [DisplayName("Contratante")]
        public int ContratanteId { get; set; }

        public bool Selecionado { get; set; }

        public ContratanteAdministracaoModel Contratante { get; set; }
        public List<FuncaoAdministracaoModel> FuncaoList { get; set; }
        public ICollection<UsuarioAdministracaoModel> UsuarioList { get; set; }

        public int[] SelectedGroupsFuncao { get; set; }
       
        public void Validar()
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            
            UrlEditar = Url.Action("PerfilFrm", "Perfil", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idPerfil={0}&Acao=Alterar", Id), Key)
            });

            UrlDetalhar = Url.Action("PerfilFrm", "Perfil", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idPerfil={0}&Acao=Detalhar", Id), Key)
            });

            UrlExcluir = Url.Action("PerfilFrm", "Perfil", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idPerfil={0}&Acao=Excluir", Id), Key)
            });
        }
    }
}