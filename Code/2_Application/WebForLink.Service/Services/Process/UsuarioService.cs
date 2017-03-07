using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.Common;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class UsuarioWebForLinkAppService : AppService<WebForLinkContexto>, IUsuarioWebForLinkAppService
    {
        private readonly IContratanteConfiguracaoWebForLinkService _contratanteConfiguracao;
        private readonly IContratanteWebForLinkService _contratanteService;
        private readonly IDescricaoDocumentosWebForLinkService _descricaoDocumentos;
        private readonly IDescricaoDocumentosChWebForLinkService _descricaoDocumentosCH;
        private readonly IFornecedorCategoriaWebForLinkService _fornecedorCategoria;
        private readonly IFornecedorWebForLinkService _fornecedorService;
        private readonly IPapelWebForLinkService _papelService;
        private readonly IPerfilWebForLinkService _perfilService;
        private readonly IFornecedorCategoriaChWebForLinkService _pjpfCategoriaCH;
        private readonly IUsuarioSenhasHistWebForLinkService _processoSenhasHist;
        private readonly ITipoDocumentoWebForLinkService _tipoDocumentos;
        private readonly ITipoDocumentosChWebForLinkService _tipoDocumentosCH;
        private readonly IUsuarioWebForLinkService _usuarioService;

        public UsuarioWebForLinkAppService(
            IUsuarioWebForLinkService usuarioService,
            IFornecedorWebForLinkService fornecedorService,
            IContratanteWebForLinkService contratanteService,
            IPapelWebForLinkService papelService,
            IPerfilWebForLinkService perfilService,
            IFornecedorCategoriaWebForLinkService fornecedorCategoria,
            IDescricaoDocumentosWebForLinkService descricaoDocumentos,
            IContratanteConfiguracaoWebForLinkService contratanteConfiguracao,
            ITipoDocumentosChWebForLinkService tipoDocumentosCH,
            ITipoDocumentoWebForLinkService tipoDocumentos,
            IDescricaoDocumentosChWebForLinkService descricaoDocumentosCH,
            IFornecedorCategoriaChWebForLinkService pjpfCategoriaCH,
            IUsuarioSenhasHistWebForLinkService processoSenhasHist)
        {
            try
            {
                _fornecedorService = fornecedorService;
                _contratanteService = contratanteService;
                _papelService = papelService;
                _perfilService = perfilService;
                _fornecedorCategoria = fornecedorCategoria;
                _descricaoDocumentos = descricaoDocumentos;
                _contratanteConfiguracao = contratanteConfiguracao;
                _tipoDocumentosCH = tipoDocumentosCH;
                _tipoDocumentos = tipoDocumentos;
                _descricaoDocumentosCH = descricaoDocumentosCH;
                _pjpfCategoriaCH = pjpfCategoriaCH;
                _processoSenhasHist = processoSenhasHist;
                _usuarioService = usuarioService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        //public void Dispose()
        //{
        //    
        //}

        public void ContabilizarErroLogin(Usuario usuario)
        {
            usuario.CONTA_TENTATIVA = usuario.CONTA_TENTATIVA + 1;
            if (usuario.CONTA_TENTATIVA > 9)
                usuario.ATIVO = false;
            AlterarUsuario(usuario);
        }

        public void IncluirUsuario(Contratante contratante, CONTRATANTE_CONFIGURACAO config, Usuario usuario)
        {
            try
            {
                _contratanteService.Add(contratante);
                _contratanteConfiguracao.Add(config);
                usuario.CONTRATANTE_ID = contratante.ID;
                usuario.Contratante = contratante;
                _usuarioService.Add(usuario);

                // CARREGA AS SUGESTÕES DE TIPOS DE DOCUMENTOS, DESCRIÇÃO DE DOCUMENTOS e CATEGORIA
                var listaTipoDocCh = _tipoDocumentosCH.All().ToList();
                foreach (var tipoDocCh in listaTipoDocCh)
                {
                    _tipoDocumentos.Add(new TipoDeDocumento
                    {
                        CONTRATANTE_ID = contratante.ID,
                        DESCRICAO = tipoDocCh.DESCRICAO,
                        TIPO_DOCUMENTOS_CH_ID = tipoDocCh.ID,
                        ATIVO = true
                    });
                }

                var listaTipoDoc = _tipoDocumentos.All().Where(td => td.CONTRATANTE_ID == contratante.ID).ToList();
                var listaDescricaoDocCh = _descricaoDocumentosCH.All().ToList();
                foreach (var tipoDoc in listaTipoDoc)
                {
                    foreach (
                        var descricaoDocCh in
                            listaDescricaoDocCh.Where(dd => dd.TIPO_DOCUMENTOS_ID == tipoDoc.TIPO_DOCUMENTOS_CH_ID)
                                .ToList())
                    {
                        _descricaoDocumentos.Add(new DescricaoDeDocumentos
                        {
                            CONTRATANTE_ID = contratante.ID,
                            TIPO_DOCUMENTOS_ID = tipoDoc.ID,
                            DESCRICAO = descricaoDocCh.DESCRICAO,
                            DESCRICAO_DOCUMENTOS_CH_ID = descricaoDocCh.ID,
                            ATIVO = true
                        });
                    }
                }

                var listaCategoriaCh = _pjpfCategoriaCH.All().ToList();
                foreach (var categoriaCh in listaCategoriaCh)
                {
                    _fornecedorCategoria.Add(new FORNECEDOR_CATEGORIA
                    {
                        CONTRATANTE_ID = contratante.ID,
                        PJPF_CATEGORIA_CH_ID = categoriaCh.ID,
                        DESCRICAO = categoriaCh.DESCRICAO,
                        ATIVO = true
                    });
                }
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Usuário", ex);
            }
        }

        public void IncluirUsuarioPadraoSenha(Usuario login, USUARIO_SENHAS senha, int[] papeis, int[] perfis)
        {
            try
            {
                BeginTransaction();
                IncluirNovoUsuarioPadrao(login, senha, papeis, perfis);
                Commit();
                //Processo.Dispose();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Usuário", ex);
            }
        }

        public void IncluirNovoUsuarioPadraoPreCadastro(Usuario usuarioInclusao, USUARIO_SENHAS historicoSenhaInclusao)
        {
            try
            {
                usuarioInclusao.ATIVO = true;
                _usuarioService.Add(usuarioInclusao);
                if (historicoSenhaInclusao != null)
                    _processoSenhasHist.Add(historicoSenhaInclusao);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao IncluirNovoUsuarioPadraoPreCadastro", ex);
            }
        }

        public void IncluirUsuarioIncluirNovaSenhaUsuario(Usuario usuario, USUARIO_SENHAS historicoSenha)
        {
            try
            {
                _processoSenhasHist.Add(historicoSenha);
                _usuarioService.Update(usuario);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Usuário", ex);
            }
        }

        public void IncluirUsuarioPadraoSenha(Usuario login, USUARIO_SENHAS senha, int idPapel, int idPerfil)
        {
            try
            {
                var idPapeis = new int[1] { idPapel };
                var idPerfis = new int[1] { idPerfil };
                IncluirNovoUsuarioPadrao(login, senha, idPapeis, idPerfis);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Usuário", ex);
            }
        }

        public void IncluirNovoUsuarioPadrao(Usuario usuarioInclusao, USUARIO_SENHAS historicoSenhaInclusao,
            int[] papeis, int[] perfis)
        {
            try
            {
                //Processo.IncluirNovoUsuarioPadrao(usuarioInclusao, historicoSenhaInclusao, papeis, perfis);

                usuarioInclusao.WFL_PAPEL.Clear();
                usuarioInclusao.WAC_PERFIL.Clear();
                if (papeis != null)
                    if (papeis.Length != 0)
                        _papelService.Find(x => papeis.Contains(x.ID))
                            .ToList()
                            .ForEach(y => usuarioInclusao.WFL_PAPEL.Add(y));

                if (perfis != null)
                    if (perfis.Length != 0)
                        _perfilService.Find(x => perfis.Contains(x.ID))
                            .ToList()
                            .ForEach(y => usuarioInclusao.WAC_PERFIL.Add(y));

                if (historicoSenhaInclusao != null)
                    usuarioInclusao.WFD_USUARIO_SENHAS_HIST.Add(historicoSenhaInclusao);

                _usuarioService.Add(usuarioInclusao);
            }
            //catch (DbEntityValidationException e)
            //{
            //    throw;
            //}
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("IncluirNovoUsuarioPadrao", ex);
            }
        }

        public void AlterarUsuario(Usuario entidade)
        {
            try
            {
                _usuarioService.Update(entidade);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao Alterar Usuário", ex);
            }
        }
        public bool PerfilAlterado { get { return false; } }
        public void AlterarMinhaConta(Usuario entidade, int[] papeis, int[] perfis, int contratanteSelecionado)
        {
            try
            {
                BeginTransaction();
                var usuario = _usuarioService.Get(entidade.ID);

                if (PerfilAlterado)
                {
                    #region Alteração dos Perfis
                    var idPerfisExistentes = usuario.WAC_PERFIL.Select(x => x.ID).ToArray();
                    var idPerfisNovos = perfis.Where(x => idPerfisExistentes.Contains(x)).ToArray();

                    var perfisExcluidos = usuario.WAC_PERFIL.Where(x => !perfis.Contains(x.ID)).ToList();
                    var perfisNovos = _perfilService.Find(x => idPerfisNovos.Contains(x.ID)).ToList();

                    perfisExcluidos.ForEach(x => usuario.WAC_PERFIL.Remove(x));
                    perfisNovos.ForEach(x => usuario.WAC_PERFIL.Add(x));
                    #endregion

                    #region Alteração dos Papéis

                    var idPapeisExistentes = usuario.WFL_PAPEL.Select(x => x.ID).ToArray();
                    var idPapeisNovos = papeis.Where(x => !idPapeisExistentes.Contains(x)).ToArray();

                    var papeisExcluidos = usuario.WFL_PAPEL.Where(x => !papeis.Contains(x.ID)).ToList();
                    var papeisNovos = _papelService.Find(x => idPapeisNovos.Contains(x.ID)).ToList();
                    papeisExcluidos.ForEach(x => usuario.WFL_PAPEL.Remove(x));
                    papeisNovos.ForEach(x => usuario.WFL_PAPEL.Add(x));

                    #endregion
                }
                #region Alteração do Usuário

                usuario.NOME = entidade.NOME;
                usuario.CPF_CNPJ = entidade.CPF_CNPJ;
                usuario.CARGO = entidade.CARGO;
                usuario.EMAIL = entidade.EMAIL;
                usuario.EXPIRA_EM_DIAS = entidade.EXPIRA_EM_DIAS;

                #endregion

                _usuarioService.Update(usuario);
                Commit();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Usuário", ex);
            }
        }

        public void ExecutarPrimeiroAcesso(Usuario entidade)
        {
            try
            {
                entidade.PRIMEIRO_ACESSO = true;
                _usuarioService.Update(entidade);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Usuário", ex);
            }
        }

        public void ExcluirUsuario(int id)
        {
            try
            {
                _usuarioService.ExcluirUsuario(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Não foi possível excluir este Usuário.", ex);
            }
        }

        public bool Bloqueio90Dias(Usuario entidade)
        {
            try
            {
                var chave = Path.GetRandomFileName().Replace(".", "");
                entidade.TROCAR_SENHA = chave;
                entidade.ATIVO = false;
                entidade.CONTA_TENTATIVA = 0;
                //Db.Entry(entidade).State = EntityState.Modified;
                _usuarioService.Update(entidade);
                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao alterar um usuário", ex);
            }
        }

        public bool VerificaLoginExistente(string login)
        {
            try
            {
                return _usuarioService.VerificaLoginExistente(login);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public bool ValidarPorEmail(string email)
        {
            try
            {
                //return Db.Usuario.Any(x => x.EMAIL == email);
                return _usuarioService.ValidarPorEmail(email);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public bool ValidarPorCnpj(string cnpj)
        {
            try
            {
                //return Db.Usuario.Any(x => x.LOGIN == cnpj.Replace(".", "").Replace("-", "").Replace("/", ""));
                return _usuarioService.ValidarPorCnpj(cnpj);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public Usuario ZerarTentativasLogin(Usuario usuario)
        {
            usuario.CONTA_TENTATIVA = 0;
            _usuarioService.Update(usuario);
            return usuario;
        }

        public Usuario BuscarPorId(int id)
        {
            try
            {
                return _usuarioService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário", ex);
            }
        }

        public Usuario BuscarFichaCadastral(int id)
        {
            try
            {
                var usuario = _usuarioService.Get(id);
                var fornecedorIndividual =
                    _fornecedorService.Find(x => x.CNPJ == usuario.CPF_CNPJ || x.CPF == usuario.CPF_CNPJ)
                        .FirstOrDefault();
                return new Usuario();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário", ex);
            }
        }

        public Usuario BuscarPorLoginParaAcesso(string login)
        {
            try
            {
                return _usuarioService.BuscarPorLoginParaAcesso(login);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário", ex);
            }
        }

        public Usuario BuscarPorLoginParaAcesso(string login, string senha)
        {
            try
            {
                return _usuarioService.BuscarPorLoginParaAcesso(login, senha);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário", ex);
            }
        }

        public Usuario BuscarPorLogin(string login)
        {
            try
            {
                return _usuarioService.BuscarPorLogin(login);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        public Usuario BuscarPorCpf(string cpf)
        {
            try
            {
                //return Db.Usuario
                //    .Include("Contratante")
                //    .FirstOrDefault(x => x.CPF_CNPJ == cpf.Replace(".", "").Replace("-", "").Replace("/", ""));
                return _usuarioService.BuscarPorCpf(cpf);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        public Usuario BuscarPorEmail(string email)
        {
            try
            {
                return _usuarioService.Find(x => x.LOGIN == email || x.EMAIL == email).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public Usuario BuscarPorDocumento(string documento)
        {
            //return Db.Usuario.FirstOrDefault(x => x.CPF_CNPJ == documento);
            return _usuarioService.BuscarPorDocumento(documento);
        }

        public List<Usuario> ListarPorIdContratante(int idContratante)
        {
            try
            {
                //return Db.Usuario
                //    .Include("Contratante")
                //    .Where(x => x.Contratante.ID == idContratante)
                //    .ToList();
                return _usuarioService.ListarPorIdContratante(idContratante);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um usuário por Contratante", ex);
            }
        }

        //public RetornoPesquisa<Usuario> PesquisarUsuarios(GerenciarContasFiltrosDTO filtros, int pagina, int qtdLinhas)
        //{
        //    try
        //    {
        //        var predicate = PredicateBuilder.New<Usuario>();
        //        predicate = predicate.And(d => d.Contratante.WFD_GRUPO.Any(dd => dd.ID == filtros.GrupoId));
        //        predicate = predicate.And(d => d.ID != filtros.UsuarioId);

        //        if (!string.IsNullOrEmpty(filtros.CPF))
        //            predicate = predicate.And(d => d.CPF_CNPJ == filtros.CPF.Replace(".", "").Replace("-", "").Replace("/", ""));
        //        if (!string.IsNullOrEmpty(filtros.Login))
        //            predicate = predicate.And(x => x.LOGIN.Contains(filtros.Login));
        //        if (!string.IsNullOrEmpty(filtros.Email))
        //            predicate = predicate.And(x => x.EMAIL.Contains(filtros.Email));
        //        if (filtros.Ativo)
        //            predicate = predicate.And(x => x.ATIVO == filtros.Ativo);
        //        if (filtros.Administrador)
        //            predicate = predicate.And(x => x.PRINCIPAL == filtros.Administrador);
        //        if (!string.IsNullOrEmpty(filtros.Nome))
        //            predicate = predicate.And(d => d.NOME.Contains(filtros.Nome));


        //        return PesquisarUsuarios(predicate, pagina, qtdLinhas);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao listar Usuário por Id de Contratante", ex);
        //    }
        //}

        //public RetornoPesquisa<Usuario> PesquisarUsuarios(Expression<Func<Usuario, bool>> predicate, int pagina, int tamanhoPagina)
        //{
        //    try
        //    {
        //        return _usuarioService.Pesquisar(predicate, tamanhoPagina, pagina, x => x.NOME);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Usuarios", ex);
        //    }
        //}

        public Usuario Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Usuario Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Usuario GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Usuario> Find(Expression<Func<Usuario, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Usuario entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<Usuario> PesquisarUsuarios(GerenciarContasFiltrosDTO filtros, int pagina, int qtdLinhas)
        {
            throw new NotImplementedException();
        }

        public RetornoPesquisa<Usuario> PesquisarUsuarios(Expression<Func<Usuario, bool>> predicate, int pagina,
            int tamanhoPagina)
        {
            throw new NotImplementedException();
        }

        public Usuario ProcessoLoginConvencional(string usuario, string senha)
        {
            if (string.IsNullOrEmpty(usuario))
            { }
            else if (string.IsNullOrEmpty(usuario))
            { }
            var usuarioLogado = _usuarioService.Find(x => x.LOGIN == usuario).FirstOrDefault();
            if (usuarioLogado == null)
                throw new Exception("Usuario não encontrado.");
            if (usuarioLogado.SENHA != senha)
                throw new Exception("Senha incorreta.");


            throw new NotImplementedException();
        }
    }
}