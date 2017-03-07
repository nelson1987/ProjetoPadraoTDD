using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;

namespace WebForLink.Application.Interfaces
{
    public interface IUsuarioWebForLinkAppService : IAppService<Usuario>
    {
        Usuario ProcessoLoginConvencional(string usuario, string senha);

        void ContabilizarErroLogin(Usuario usuario);
        void IncluirUsuario(Contratante contratante, CONTRATANTE_CONFIGURACAO config, Usuario usuario);
        void IncluirUsuarioPadraoSenha(Usuario login, USUARIO_SENHAS senha, int[] papeis, int[] perfis);
        void IncluirNovoUsuarioPadraoPreCadastro(Usuario usuarioInclusao, USUARIO_SENHAS historicoSenhaInclusao);
        void IncluirUsuarioIncluirNovaSenhaUsuario(Usuario usuario, USUARIO_SENHAS historicoSenha);
        void IncluirUsuarioPadraoSenha(Usuario login, USUARIO_SENHAS senha, int idPapel, int idPerfil);

        void IncluirNovoUsuarioPadrao(Usuario usuarioInclusao, USUARIO_SENHAS historicoSenhaInclusao, int[] papeis,
            int[] perfis);

        void AlterarUsuario(Usuario entidade);
        void AlterarMinhaConta(Usuario entidade, int[] papeis, int[] perfis, int contratanteSelecionado);
        void ExecutarPrimeiroAcesso(Usuario entidade);
        void ExcluirUsuario(int id);
        bool Bloqueio90Dias(Usuario entidade);
        bool VerificaLoginExistente(string login);
        bool ValidarPorEmail(string email);
        bool ValidarPorCnpj(string cnpj);
        Usuario ZerarTentativasLogin(Usuario usuario);
        Usuario BuscarPorId(int id);
        Usuario BuscarFichaCadastral(int id);
        Usuario BuscarPorLoginParaAcesso(string login);
        Usuario BuscarPorLoginParaAcesso(string login, string senha);
        Usuario BuscarPorLogin(string login);
        Usuario BuscarPorCpf(string cpf);
        Usuario BuscarPorEmail(string email);
        Usuario BuscarPorDocumento(string documento);
        List<Usuario> ListarPorIdContratante(int idContratante);
        RetornoPesquisa<Usuario> PesquisarUsuarios(GerenciarContasFiltrosDTO filtros, int pagina, int qtdLinhas);

        RetornoPesquisa<Usuario> PesquisarUsuarios(Expression<Func<Usuario, bool>> predicate, int pagina,
            int tamanhoPagina);
    }
}
