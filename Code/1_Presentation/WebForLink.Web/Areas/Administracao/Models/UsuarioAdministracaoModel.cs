using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class UsuarioAdministracaoModel : ViewModelPadrao
    {
        public UsuarioAdministracaoModel()
        {
            PerfilList = new List<PerfilAdministracaoModel>();
            ContratanteList = new List<ContratanteAdministracaoModel>();
            PapelList = new List<PapelAdministracaoModel>();
        }

        public int Id { get; set; }

        [DisplayName("Contratante")]
        public int ContratanteId { get; set; }

        [DisplayName("Tentativas de Login")]
        public int ContaTentativa { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        public string Nome { get; set; }

        public string Senha { get; set; }

        public string TrocarSenha { get; set; }

        public string CPF { get; set; }

        public string Cargo { get; set; }

        public string Login { get; set; }

        public string LoginSSO { get; set; }

        [DisplayName("Domínio")]
        public string Dominio { get; set; }

        public bool Administrador { get; set; }

        public bool Ativo { get; set; }

        public bool PrimeiroAcesso { get; set; }

        [DisplayName("Data de Nascimento")]
        public string DataNascimento { get; set; }

        [DisplayName("Data de ativação")]
        public DateTime? DataAtivacao { get; set; }

        [DisplayName("Data de criação")]
        public DateTime DataCriacao { get; set; }

        public bool? Principal { get; set; }

        [DisplayName("Expira em")]
        public int ExpiraEmDias { get; set; }

        public ContratanteAdministracaoModel Contratante { get; set; }
        public List<PerfilAdministracaoModel> PerfilList { get; set; }
        public List<ContratanteAdministracaoModel> ContratanteList { get; set; }
        public List<PapelAdministracaoModel> PapelList { get; set; }
        public int[] SelectedGroupsPapel { get; set; }
        public int[] SelectedGroupsPerfil { get; set; }

        public static UsuarioAdministracaoModel ModelToViewModel(Usuario usuario, UrlHelper url)
        {
            //var viewModel = Mapper.Map<UsuarioAdministracaoModel>(usuario);
            UsuarioAdministracaoModel viewModel = new UsuarioAdministracaoModel();
            viewModel.Id = usuario.ID;
            viewModel.ContratanteId = usuario.CONTRATANTE_ID ?? 0;
            viewModel.ContaTentativa = usuario.CONTA_TENTATIVA ?? 0;
            viewModel.Email = usuario.EMAIL;
            viewModel.Nome = usuario.NOME;
            viewModel.Senha = usuario.SENHA;
            viewModel.TrocarSenha = usuario.TROCAR_SENHA;
            viewModel.CPF = usuario.CPF_CNPJ;//string.IsNullOrEmpty(usuario.CPF_CNPJ) ??  ? Convert.ToInt64(usuario.CPF_CNPJ).ToString(@"000\.000\.000\-00") : string.Empty;
            viewModel.Cargo = usuario.CARGO;
            viewModel.Login = usuario.LOGIN;
            viewModel.LoginSSO = usuario.LOGIN_SSO;
            viewModel.Dominio = usuario.DOMINIO;
            viewModel.Administrador = usuario.PRINCIPAL ?? false;
            viewModel.Ativo = usuario.ATIVO;
            viewModel.DataNascimento = usuario.DAT_NASCIMENTO.ToString().Replace(" 00:00:00", "");
            viewModel.PrimeiroAcesso = usuario.PRIMEIRO_ACESSO;
            viewModel.DataAtivacao = usuario.DT_ATIVACAO;
            viewModel.DataCriacao = usuario.DT_CRIACAO ?? DateTime.Now;
            //viewModel.Contratante = usuario.Contratante;
            //viewModel.PerfilList = usuario.WAC_PERFIL;
            //viewModel.ContratanteList = usuario.WFD_CONTRATANTE1;
            //viewModel.PapelList = usuario.WFL_PAPEL;
            viewModel.ExpiraEmDias = usuario.EXPIRA_EM_DIAS;
            viewModel.Validar(url);
            return viewModel;
        }
        public static List<UsuarioAdministracaoModel> ModelToViewModel(IList<Usuario> usuario, UrlHelper url)
        {
            List<UsuarioAdministracaoModel> listaViewModel = new List<UsuarioAdministracaoModel>();
            foreach (var item in usuario)
            {
                listaViewModel.Add(ModelToViewModel(item, url));
            }
            return listaViewModel;
        }
        public void Validar(UrlHelper url)
        {
            Url = url;
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            UrlEditar = Url.Action("UsuarioFrm", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idUsuario={0}&Acao=Alterar", Id), Key)
            });
            UrlExcluir = Url.Action("UsuarioFrm", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idUsuario={0}&Acao=Excluir", Id), Key)
            });
        }
    }
}
