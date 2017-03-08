using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.Categorias;
using WebForLink.Domain.Entities.Status;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Domain.Entities
{
    public abstract class Contratante
    {
        private Contratante()
        {
            Empresas = new List<Empresa>();
            Solicitacoes = new List<Solicitacao>();
            Usuarios = new List<Usuario>();
            ConfiguracaoSistemas = new List<ConfiguracaoSistema>();
            Perfis = new List<Perfil>();
            Papeis = new List<Papel>();
            Documentos = new List<Documento>();
            CategoriasCadastradas = new List<CategoriaEmpresa>();
            Importacoes = new List<Importacao>();
        }

        protected Contratante(string razaoSocial, TipoEmpresa tipoEmpresa)
            : this()
        {
            RazaoSocial = razaoSocial;
            TipoEmpresa = tipoEmpresa;
        }

        protected Contratante(string razaoSocial, TipoEmpresa tipoEmpresa, Aplicacao aplicacao)
            : this(razaoSocial, tipoEmpresa)
        {
            SetAplicacao(aplicacao);
        }

        public int Id { get; private set; }
        public string RazaoSocial { get; private set; }
        public string Documento { get; private set; }
        public TipoEmpresa TipoEmpresa { get; private set; }
        public Empresa DadosGerais { get; private set; }
        public Aplicacao Aplicacao { get; private set; }
        public List<ConfiguracaoSistema> ConfiguracaoSistemas { get; private set; }
        public List<Empresa> Empresas { get; private set; }        
        public List<Solicitacao> Solicitacoes { get; private set; }
        public List<Perfil> Perfis { get; private set; }
        public List<Papel> Papeis { get; private set; }
        public List<Documento> Documentos { get; private set; }
        public List<CategoriaEmpresa> CategoriasCadastradas { get; private set; }
        public List<Importacao> Importacoes { get; private set; }
        public List<Usuario> Usuarios { get; private set; }

        public bool TemEssePerfil(Perfil perfil)
        {
            return Perfis.Any(x => x == perfil);
        }

        public void AdicionarEmpresa(Empresa sorteq)
        {
            Empresas.Add(sorteq);
        }

        public void SetDadosGerais(Cliente empresa)
        {
            DadosGerais = empresa;
        }

        public void AdicionarSolicitacao(Solicitacao solicitacao)
        {
            Solicitacoes.Add(solicitacao);
        }

        public void AdicionarPerfil(Perfil perfil)
        {
            Perfis.Add(perfil);
        }

        public void AdicionarPapel(Papel papel)
        {
            Papeis.Add(papel);
        }

        public void AdicionarDocumento(Documento documento)
        {
            Documentos.Add(documento);
        }

        public void AdcionarCliente(Cliente empresa)
        {
            Empresas.Add(empresa);
        }

        public void AdicionarFabricante(Fabricante empresa)
        {
            Empresas.Add(empresa);
        }

        public void AdicionarCategoria(CategoriaEmpresa categoria)
        {
            CategoriasCadastradas.Add(categoria);
        }

        public void AdicionarUsuario(Usuario usuario)
        {
            Usuarios.Add(usuario);
        }

        public void AdicionarImportacao(Importacao importacao)
        {
            Importacoes.Add(importacao);
        }

        public void SetAplicacao(Aplicacao importacao)
        {
            Aplicacao = importacao;
        }
    }
}