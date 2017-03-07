using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IPapelWebForLinkService : IService<Papel>
    {
        Papel BuscarPorID(int id);
        Papel InserirPapel(Papel entidade);
        List<Papel> ListarTodos(int contratanteId);
        List<Papel> ListarTodos(int[] papeis);
        Papel BuscarPorContratanteETipoPapel(int contratanteId, int tipo);
        int[] EmpilharPorUsuarioId(int usuarioId);
        Papel AlterarPapel(Papel entidade);
        Papel BuscarPorSigla(string sigla);
        RetornoPesquisa<Papel> PesquisarPapel(PesquisaPapelFiltrosDTO filtros, int pagina, int tamanhoPagina);
        void ExcluirPapel(int id);
    }

    public class PapelWebForLinkService : Service<Papel>, IPapelWebForLinkService
    {
        private readonly IPapelWebForLinkRepository _papelRepository;

        public PapelWebForLinkService(IPapelWebForLinkRepository papel) : base(papel)
        {
            try
            {
                _papelRepository = papel;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Papel BuscarPorID(int id)
        {
            try
            {
                return _papelRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Papel InserirPapel(Papel entidade)
        {
            try
            {
                if (entidade.PAPEL_TP_ID == 0)
                {
                    entidade.PAPEL_TP_ID = null;
                }

                _papelRepository.Add(entidade);


                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir um papel.", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public List<Papel> ListarTodos(int contratanteId)
        {
            try
            {
                return _papelRepository.Find(x => x.CONTRATANTE_ID == contratanteId).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="papeis"></param>
        /// <returns></returns>
        public List<Papel> ListarTodos(int[] papeis)
        {
            try
            {
                return _papelRepository.Find(x => papeis.Contains(x.ID)).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="contratanteId"></param>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public Papel BuscarPorContratanteETipoPapel(int contratanteId, int tipo)
        {
            try
            {
                return _papelRepository.BuscarPorContratanteIdETipoPapelId(contratanteId, tipo);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o papel por Tipo", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public int[] EmpilharPorUsuarioId(int usuarioId)
        {
            try
            {
                return _papelRepository.Find(x => x.WFD_USUARIO.Any(y => y.ID == usuarioId)).Select(z => z.ID).ToArray();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public Papel AlterarPapel(Papel entidade)
        {
            try
            {
                if (entidade.PAPEL_TP_ID == 0)
                {
                    entidade.PAPEL_TP_ID = null;
                }
                _papelRepository.Update(entidade);

                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir um papel.", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sigla"></param>
        /// <returns></returns>
        public Papel BuscarPorSigla(string sigla)
        {
            try
            {
                return _papelRepository.Find(x => x.PAPEL_SGL == sigla).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um papel por sigla", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="filtros"></param>
        /// <param name="pagina"></param>
        /// <param name="tamanhoPagina"></param>
        /// <returns></returns>
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void ExcluirPapel(int id)
        {
            try
            {
                var entidade = _papelRepository.Get(id);
                _papelRepository.Delete(entidade);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir um papel.", ex);
            }
        }

        public RetornoPesquisa<Papel> PesquisarPapel(PesquisaPapelFiltrosDTO filtros, int pagina, int tamanhoPagina)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}