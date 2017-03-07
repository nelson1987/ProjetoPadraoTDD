using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class UsuarioSenhasHistRepository : EntityFrameworkRepository<USUARIO_SENHAS, WebForLinkContexto>,
        IUsuarioSenhasHistWebForLinkRepository
    {
        public USUARIO_SENHAS BuscarPorIdComUsuario(int id)
        {
            try
            {
                return DbSet.Include("Usuario")
                    .FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário", ex);
            }
        }

        public USUARIO_SENHAS BuscarHistoricoPorLogin(string login)
        {
            try
            {
                return DbSet
                    .Include("Usuario")
                    .Where(x => x.WFD_USUARIO.LOGIN == login)
                    .OrderBy(x => x.SENHA_DT)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        public USUARIO_SENHAS BuscarHistoricoPorIdUsuario(int id)
        {
            try
            {
                return DbSet
                    .Where(x => x.USUARIO_ID == id)
                    .OrderBy(x => x.SENHA_DT)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por login", ex);
            }
        }

        public List<USUARIO_SENHAS> ListarPorIdContratante(int idContratante)
        {
            try
            {
                return DbSet
                    .Include("Usuario")
                    .Where(x => x.WFD_USUARIO.ID == idContratante)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um usuário por Contratante", ex);
            }
        }

        public List<USUARIO_SENHAS> Listar6UltimasPorUsuarioId(int idUsuario)
        {
            try
            {
                return DbSet
                    .Where(x => x.USUARIO_ID == idUsuario)
                    .OrderBy(x => x.SENHA_DT)
                    .Take(6)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao cadastrar Perguntas", ex);
            }
        }
    }
}