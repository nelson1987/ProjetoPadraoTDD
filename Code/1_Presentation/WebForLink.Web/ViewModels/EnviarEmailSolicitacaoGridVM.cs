using System.ComponentModel;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Services;

namespace WebForLink.Web.ViewModels
{
    public class EnviarEmailSolicitacaoGridVM
    {
        public EnviarEmailSolicitacaoGridVM()
        {
            if (Id == 0)
            {
                var criptografia = new Criptografia(EnumCripto.Criptografar, string.Format("id={0}", Id), "r10X310y");
                LinkCriptografado = criptografia.Resultado;
            }
        }

        public int Id { get; set; }

        [DisplayName("Tipo de Documento")]
        public string Tipo { get; set; }

        [DisplayName("Descrição do Documento")]
        public string Descricao { get; set; }

        public string LinkCriptografado { get; private set; }
    }
}