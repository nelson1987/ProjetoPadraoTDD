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
    public class ContratanteWebForLinkRepository : EntityFrameworkRepository<Contratante, WebForLinkContexto>,
        IContratanteWebForLinkRepository
    {
        public int[] ListarTodosIds()
        {
            return DbSet
                .Select(x => x.ID)
                .Distinct()
                .ToArray();
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="incluir"></param>
        /// <returns></returns>
        public Contratante BuscarPorId(int id, bool incluir)
        {
            try
            {
                if (incluir)
                    return DbSet
                        .Include("WFD_CONTRATANTE_CONFIG")
                        .FirstOrDefault(x => x.ID == id);
                return DbSet.FirstOrDefault(x => x.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar um contratante", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Contratante BuscarPorIdDocumentoSolicitado(int id)
        {
            try
            {
                return DbSet
                    .Include("WFD_CONTRATANTE_CONFIG")
                    .Include("Usuario")
                    .FirstOrDefault(c => c.ID == id);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma lista de contratantes por Usuário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idGrupo"></param>
        /// <returns></returns>
        public List<Contratante> ListarTodosPorGrupo(int idGrupo)
        {
            try
            {
                return DbSet.Where(c => c.WFD_GRUPO.Any(g => g.ID == idGrupo)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma lista de contratantes por Grupo", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public List<Contratante> ListarTodosPorUsuario(int idUsuario)
        {
            try
            {
                return DbSet.Where(c => c.Usuario.Any(u => u.ID == idUsuario)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma lista de contratantes por Usuário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="papelId"></param>
        /// <returns></returns>
        public List<Contratante> ListarTodosPorPapel(int papelId)
        {
            try
            {
                return DbSet
                    .Include("WFL_PAPEL")
                    .Where(c => c.WFL_PAPEL.Any(u => u.ID == papelId))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma lista de contratantes por Usuário", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<Contratante> ListarTodos(int grupoId)
        {
            try
            {
                return DbSet.Where(x => x.WFD_GRUPO.Any(y => y.ID == grupoId)).ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma lista de contratantes por Grupo", ex);
            }
        }

        public int[] ListarTodasAprovadas()
        {
            return DbSet
                .Select(x => x.ID)
                .Distinct()
                .ToArray();
        }
    }
}