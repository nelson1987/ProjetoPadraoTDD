using System;

namespace WebForLink.Domain.Infrastructure
{
    public class CadastrarSolicitacaoDTO
    {
        public int TipoCadastro { get; set; }
        public int TipoFornecedor { get; set; }
        public int ContratanteId { get; set; }
        public int UsuarioId { get; set; }
        public int CategoriaId { get; set; }
        public int ComprasId { get; set; }
        public string CNPJ { get; set; }
        public string ContatoNome { get; set; }
        public string ContatoEmail { get; set; }
        public string Telefone { get; set; }
        public string Assunto { get; set; }
        public string RazaoSocial { get; set; }
        public string CPF { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}