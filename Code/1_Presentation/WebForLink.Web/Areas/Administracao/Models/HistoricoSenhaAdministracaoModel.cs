using System;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class HistoricoSenhaAdministracaoModel : ViewModelPadrao
    {
        public int Id { get; set; }

        public int UsuarioId { get; set; }

        public string Senha { get; set; }

        public DateTime SenhaDt { get; set; }

        public UsuarioAdministracaoModel Usuario { get; set; }
    }
}