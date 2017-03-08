using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class UsuarioService : Service<Usuario>, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(
            IUsuarioRepository productReviewRepository)
            : base(productReviewRepository)
        {
            try
            {
                _usuarioRepository = productReviewRepository;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ContabilizarErroLogin(Usuario usuario)
        {
            //usuario.CONTA_TENTATIVA = usuario.CONTA_TENTATIVA + 1;
            //if (usuario.CONTA_TENTATIVA > 9)
            //    usuario.ATIVO = false;
            //AlterarUsuario(usuario);
        }
        /*
        public void IncluirUsuario(Contratante contratante, CONTRATANTE_CONFIGURACAO config, Usuario usuario)
        {
            try
            {
                _contratanteRepository.Add(contratante);
                _contratanteConfiguracao.Add(config);
                usuario.CONTRATANTE_ID = contratante.ID;
                usuario.Contratante = contratante;
                _usuarioRepository.Add(usuario);

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
                IncluirNovoUsuarioPadrao(login, senha, papeis, perfis);
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
                _usuarioRepository.Add(usuarioInclusao);
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
                _usuarioRepository.Update(usuario);
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
                        _papelRepository.Find(x => papeis.Contains(x.ID))
                            .ToList()
                            .ForEach(y => usuarioInclusao.WFL_PAPEL.Add(y));

                if (perfis != null)
                    if (perfis.Length != 0)
                        _perfilRepository.Find(x => perfis.Contains(x.ID))
                            .ToList()
                            .ForEach(y => usuarioInclusao.WAC_PERFIL.Add(y));

                if (historicoSenhaInclusao != null)
                    usuarioInclusao.WFD_USUARIO_SENHAS_HIST.Add(historicoSenhaInclusao);

                _usuarioRepository.Add(usuarioInclusao);
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
                _usuarioRepository.Update(entidade);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao Alterar Usuário", ex);
            }
        }

        public void AlterarMinhaConta(Usuario entidade, int[] papeis, int[] perfis, int contratanteSelecionado)
        {
            try
            {
                var usuario = _usuarioRepository.Get(entidade.ID);

                #region Alteração dos Perfis

                var idPerfisExistentes = usuario.WAC_PERFIL.Select(x => x.ID).ToArray();
                var idPerfisNovos = perfis.Where(x => !idPerfisExistentes.Contains(x)).ToArray();

                var perfisExcluidos = usuario.WAC_PERFIL.Where(x => !perfis.Contains(x.ID)).ToList();
                var perfisNovos = _perfilRepository.Find(x => idPerfisNovos.Contains(x.ID)).ToList();

                perfisExcluidos.ForEach(x => usuario.WAC_PERFIL.Remove(x));
                perfisNovos.ForEach(x => usuario.WAC_PERFIL.Add(x));

                #endregion

                #region Alteração dos Papéis

                var idPapeisExistentes = usuario.WFL_PAPEL.Select(x => x.ID).ToArray();
                var idPapeisNovos = papeis.Where(x => !idPapeisExistentes.Contains(x)).ToArray();

                var papeisExcluidos = usuario.WFL_PAPEL.Where(x => !papeis.Contains(x.ID)).ToList();
                var papeisNovos = _papelRepository.Find(x => idPapeisNovos.Contains(x.ID)).ToList();
                papeisExcluidos.ForEach(x => usuario.WFL_PAPEL.Remove(x));
                papeisNovos.ForEach(x => usuario.WFL_PAPEL.Add(x));

                #endregion

                #region Alteração do Usuário

                usuario.NOME = entidade.NOME;
                usuario.CPF_CNPJ = entidade.CPF_CNPJ;
                usuario.CARGO = entidade.CARGO;
                usuario.EMAIL = entidade.EMAIL;
                usuario.EXPIRA_EM_DIAS = entidade.EXPIRA_EM_DIAS;

                #endregion

                _usuarioRepository.Update(usuario);
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
                _usuarioRepository.Update(entidade);
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
                _usuarioRepository.ExcluirUsuario(id);
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
                _usuarioRepository.Update(entidade);
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
                //return Db.Usuario.Any(x => x.LOGIN == login);
                return _usuarioRepository.VerificarLoginExistente(login);
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
                return _usuarioRepository.ValidarPorEmail(email);
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
                return _usuarioRepository.ValidarPorCnpj(cnpj);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public Usuario ZerarTentativasLogin(Usuario usuario)
        {
            usuario.CONTA_TENTATIVA = 0;
            _usuarioRepository.Update(usuario);
            return usuario;
        }

        public Usuario BuscarPorId(int id)
        {
            try
            {
                return _usuarioRepository.Get(id);
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
                var usuario = _usuarioRepository.Get(id);
                var fornecedorIndividual =
                    _fornecedorRepository.Find(x => x.CNPJ == usuario.CPF_CNPJ || x.CPF == usuario.CPF_CNPJ)
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
                return _usuarioRepository.BuscarPorLoginParaAcesso(login);
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
                return _usuarioRepository.BuscarPorLoginParaAcesso(login, senha);
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
                return _usuarioRepository.Find(x => x.LOGIN == login).FirstOrDefault();
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
                return _usuarioRepository.BuscarPorCpf(cpf);
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
                return _usuarioRepository.Find(x => x.LOGIN == email || x.EMAIL == email).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public Usuario BuscarPorDocumento(string documento)
        {
            //return Db.Usuario.FirstOrDefault(x => x.CPF_CNPJ == documento);
            return _usuarioRepository.BuscarPorDocumento(documento);
        }

        public List<Usuario> ListarPorIdContratante(int idContratante)
        {
            try
            {
                //return Db.Usuario
                //    .Include("Contratante")
                //    .Where(x => x.Contratante.ID == idContratante)
                //    .ToList();
                return _usuarioRepository.ListarPorIdContratante(idContratante);
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

        public RetornoPesquisa<Usuario> PesquisarUsuarios(Expression<Func<Usuario, bool>> predicate, int pagina,
            int tamanhoPagina)
        {
            try
            {
                return _usuarioRepository.Pesquisar(predicate, tamanhoPagina, pagina, x => x.NOME);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Usuarios", ex);
            }
        }

        public RetornoPesquisa<Usuario> PesquisarUsuarios(GerenciarContasFiltrosDTO filtros, int pagina, int qtdLinhas)
        {
            throw new NotImplementedException();
        }*/
    }
}