using System;

namespace WebForLink.Domain.Entities
{
    [Serializable]
    public class UsuarioSistema
    {
        public string Chave { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
    }
}
