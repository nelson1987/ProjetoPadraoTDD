using System;
using System.Collections.Generic;
using System.ComponentModel;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class ContratanteAdministracaoModel : ViewModelPadrao
    {
        public ContratanteAdministracaoModel()
        {
            PerfilList = new HashSet<PerfilAdministracaoModel>();
            UsuarioList = new HashSet<UsuarioAdministracaoModel>();
            PapelList = new HashSet<PapelAdministracaoModel>();
        }

        public int Id { get; set; }

        public int? TipoCadastroId { get; set; }

        public string CNPJ { get; set; }

        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }

        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        public byte[] LogoFoto { get; set; }

        public string ExtensaoImagem { get; set; }

        public string Estilo { get; set; }

        [DisplayName("Código ERP")]
        public string ContranteCodERP { get; set; }

        public bool Selecionado { get; set; }

        public ICollection<PerfilAdministracaoModel> PerfilList { get; set; }

        public ContratanteConfigAdministracaoModel ContratanteConfig { get; set; }

        public TipoCadastroAdministracaoModel TipoCadastro { get; set; }

        public ICollection<UsuarioAdministracaoModel> UsuarioList { get; set; }

        public ICollection<PapelAdministracaoModel> PapelList { get; set; }

        public void Validar()
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();

            UrlEditar = Url.Action("ContratanteEditarFrm", "Contratante", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}", Id), Key)
            });

            UrlDetalhar = Url.Action("ContratanteDetalharFrm", "Contratante", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}", Id), Key)
            });

            UrlExcluir = Url.Action("Delete", "Contratante", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}", Id), Key)
            });
        }
    }
}