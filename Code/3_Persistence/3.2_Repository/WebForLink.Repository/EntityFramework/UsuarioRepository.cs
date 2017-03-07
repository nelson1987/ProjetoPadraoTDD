using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class UsuarioWebForLinkRepository : EntityFrameworkRepository<Usuario, WebForLinkContexto>,
        IUsuarioWebForLinkRepository
    {
        public Usuario BuscarPorLoginParaAcesso(string login)
        {
            try
            {
                return DbSet
                    .Include(x => x.Contratante.WFD_GRUPO)
                    .Include(x => x.Contratante.WFD_CONTRATANTE_CONFIG)
                    .Include("WAC_PERFIL.WAC_FUNCAO")
                    .FirstOrDefault(u => u.LOGIN == login && u.ATIVO);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário", ex);
            }
        }

        public Usuario BuscarPorLoginParaAcesso(string login, string senha)
        {
            try
            {
                return DbSet
                    .Include(x => x.Contratante.WFD_GRUPO)
                    .Include(x => x.Contratante.WFD_CONTRATANTE_CONFIG)
                    .Include("WAC_PERFIL.WAC_FUNCAO")
                    .FirstOrDefault(u => u.LOGIN == login && u.SENHA == senha && u.ATIVO);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário", ex);
            }
        }

        public Usuario BuscarPorCpf(string cpf)
        {
            try
            {
                return DbSet
                    .Include(x => x.Contratante)
                    .FirstOrDefault(x => x.CPF_CNPJ == cpf.Replace(".", "").Replace("-", "").Replace("/", ""));
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        public Usuario BuscarPorEmail(string email)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.LOGIN == email || x.EMAIL == email);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public Usuario BuscarPorDocumento(string documento)
        {
            return DbSet.FirstOrDefault(x => x.CPF_CNPJ == documento);
        }

        public Usuario ZerarTentativasLogin(Usuario usuario)
        {
            usuario.CONTA_TENTATIVA = 0;
            Update(usuario);
            return usuario;
        }

        public List<Usuario> ListarPorIdContratante(int idContratante)
        {
            try
            {
                return DbSet
                    .Include(x => x.Contratante)
                    .Where(x => x.Contratante.ID == idContratante)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por Contratante", ex);
            }
        }

        public bool VerificarLoginExistente(string login)
        {
            try
            {
                return DbSet.Any(x => x.LOGIN == login);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public bool ValidarPorEmail(string email)
        {
            try
            {
                return DbSet.Any(x => x.EMAIL == email);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public bool ValidarPorCnpj(string cnpj)
        {
            try
            {
                return DbSet.Any(x => x.LOGIN == cnpj.Replace(".", "").Replace("-", "").Replace("/", ""));
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao verificar duplicidade no login.", ex);
            }
        }

        public void GravarLogAcesso(int usuarioId, string ip, string navegador)
        {
            try
            {
                var log = new WAC_ACESSO_LOG
                {
                    DATA = DateTime.Now,
                    IP = ip,
                    NAVEGADOR = navegador,
                    USUARIO_ID = usuarioId
                };
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao gravar o log de acesso", ex);
            }
        }

        public void ContabilizarErroLogin(Usuario usuario)
        {
            usuario.CONTA_TENTATIVA = usuario.CONTA_TENTATIVA + 1;
            if (usuario.CONTA_TENTATIVA > 9)
                usuario.ATIVO = false;
            Update(usuario);
        }

        public void ExcluirUsuario(int id)
        {
            try
            {
                var usuario = DbSet.Include("WFL_PAPEL").Include("WAC_PERFIL").FirstOrDefault(x => x.ID == id);
                var papeis = usuario.WFL_PAPEL.ToList();
                var perfis = usuario.WAC_PERFIL.ToList();

                papeis.ForEach(x => usuario.WFL_PAPEL.Remove(x));
                perfis.ForEach(x => usuario.WAC_PERFIL.Remove(x));

                Update(usuario);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Não foi possível excluir este Usuário.", ex);
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

                Update(entidade);
                return true;
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao alterar um usuário", ex);
            }
        }

        public void IncluirNovoUsuarioPadrao(Usuario usuarioInclusao, USUARIO_SENHAS historicoSenhaInclusao,
            int[] papeis, int[] perfis)
        {
            try
            {
                Add(usuarioInclusao);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("IncluirNovoUsuarioPadrao", ex);
            }
        }
    }
}