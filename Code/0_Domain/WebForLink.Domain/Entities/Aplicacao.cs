using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Aplicacao : ISelfValidation
    {
        public Aplicacao()
        {
            Perfis = new List<Perfil>();
            Contratantes = new List<Contratante>();
        }

        public int Id { get; set; }
        public bool EhValido { get; }
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public List<Contratante> Contratantes { get; private set; }        
        public List<Perfil> Perfis { get; private set; }

        public ValidationResult ValidationResult
        {
            get
            {
                throw new NotImplementedException();
            }
        }


        public void SetNome(string nome)
        {
            Nome = nome;
        }

        public void SetDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void SetAtivo(bool ativo)
        {
            Ativo = ativo;
        }

        public void AdicionarPerfil(Perfil perfil)
        {
            if (!TemEssePerfil(perfil))
                Perfis.Add(perfil);
        }

        public void AdicionarPerfil(params Perfil[] perfis)
        {
            foreach (Perfil perfil in perfis)
            {
                AdicionarPerfil(perfil);
            }
        }

        public void AdicionarContratante(Contratante contratante)
        {
            Contratantes.Add(contratante);
        }

        public void AdicionarContratante(params Contratante[] contratantes)
        {
            foreach (Contratante contratante in contratantes)
            {
                AdicionarContratante(contratante);
            }
        }

        private bool TemEssePerfil(Perfil perfil)
        {
            return Perfis.Any(x => x == perfil);
        }
    }
}