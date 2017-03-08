using System;
using System.Collections.Generic;

namespace WebForLink.Domain.Entities
{
    public class Compartilhamento
    {
        private Compartilhamento()
        {
            Documentos = new List<Documento>();
        }

        public Compartilhamento(Usuario usuario, FichaCadastral fichaCadastral, List<Documento> documentos)
            : this()
        {
            Usuario = usuario;
            FichaCadastral = fichaCadastral;
            Documentos = documentos;
            Criacao = DateTime.Now;
            DataExpiracao = Criacao.AddDays(30);
        }
        public Compartilhamento(Usuario usuario, List<Documento> documentos)
            : this()
        {
            Usuario = usuario;
            Documentos = documentos;
            Criacao = DateTime.Now;
            DataExpiracao = Criacao.AddDays(30);
        }

        public int Id { get; private set; }
        public Usuario Usuario { get; private set; }
        public DateTime Criacao { get; private set; }
        public FichaCadastral FichaCadastral { get; private set; }
        public List<Documento> Documentos { get; private set; }
        public DateTime DataExpiracao { get; private set; }
    }
}