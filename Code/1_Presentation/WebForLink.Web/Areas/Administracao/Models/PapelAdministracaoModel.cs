using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class PapelAdministracaoModel : ViewModelPadrao
    {
        public PapelAdministracaoModel()
        {
            UsuarioList = new HashSet<UsuarioAdministracaoModel>();
        }

        public int Id { get; set; }

        public int ContratanteId { get; set; }

        [Required(ErrorMessage = "Sigla é obrigatório")]
        [StringLength(3, ErrorMessage = "Sigla não pode ter mais que 3 caracteres.")]
        public string Sigla { get; set; }

        public string Nome { get; set; }

        public int TipoId { get; set; }

        public bool Selecionado { get; set; }
        public ContratanteAdministracaoModel Contratante { get; set; }
        public ICollection<UsuarioAdministracaoModel> UsuarioList { get; set; }

        public void Validar()
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            UrlEditar = Url.Action("PapelFrm", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idPapel={0}&Acao=Alterar", Id), Key)
            });

            UrlExcluir = Url.Action("PapelFrm", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idPapel={0}&Acao=Excluir", Id), Key)
            });
        }
    }
}