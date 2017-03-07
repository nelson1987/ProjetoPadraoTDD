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
    public interface IPerfilWebForLinkService : IService<Perfil>
    {
        List<Perfil> ListarTodos();
        List<Perfil> ListarTodos(int[] perfilId);
        List<Perfil> ListarTodosPorContratante(int id);
        RetornoPesquisa<Perfil> PesquisarPerfil(PesquisaPerfilFiltrosDTO filtros, int pagina, int tamanhoPagina);
        Perfil BuscarPorId(int id);
        Perfil AlterarPerfil(Perfil entidade, int[] idfuncoes);
        Perfil InserirPerfil(Perfil entidade);
        Perfil InserirPerfilFuncoes(Perfil entidade);
        Perfil ExcluirPerfil(int id);
    }

    public class PerfilWebForLinkService : Service<Perfil>, IPerfilWebForLinkService
    {
        private readonly IFuncaoWebForLinkRepository _funcaoRepository;
        private readonly IPerfilWebForLinkRepository _perfilRepository;

        public PerfilWebForLinkService(IFuncaoWebForLinkRepository funcao, IPerfilWebForLinkRepository perfil)
            : base(perfil)
        {
            try
            {
                _funcaoRepository = funcao;
                _perfilRepository = perfil;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public List<Perfil> ListarTodos()
        {
            try
            {
                return _perfilRepository.All().ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        public List<Perfil> ListarTodos(int[] perfilId)
        {
            try
            {
                return _perfilRepository.Find(x => perfilId.Contains(x.ID)).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma lista de perfis", ex);
            }
        }

        public List<Perfil> ListarTodosPorContratante(int id)
        {
            return ListarTodos().Where(x => x.CONTRATANTE_ID == id).ToList();
        }

        //public RetornoPesquisa<Perfil> PesquisarPerfil(PesquisaPerfilFiltrosDTO filtros, int pagina, int tamanhoPagina)
        //{
        //    var predicate = PredicateBuilder.New<Perfil>();

        //    if (!string.IsNullOrEmpty(filtros.Nome))
        //        predicate = predicate.And(c => c.PERFIL_NM.Contains(filtros.Nome));
        //    if (!string.IsNullOrEmpty(filtros.Descricao))
        //        predicate = predicate.And(c => c.PERFIL_DSC.Contains(filtros.Descricao));
        //    if (filtros.ContratanteId.HasValue && filtros.ContratanteId != 0)
        //        predicate = predicate.And(c => c.CONTRATANTE_ID == filtros.ContratanteId);

        //    try
        //    {
        //        return _perfilRepository.Pesquisar(predicate, tamanhoPagina, pagina, x => x.ID);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perfis", ex);
        //    }
        //}

        public Perfil BuscarPorId(int id)
        {
            try
            {
                return _perfilRepository.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar Perfil por Id", ex);
            }
        }

        public Perfil AlterarPerfil(Perfil entidade, int[] idfuncoes)
        {
            try
            {
                var perfil = _perfilRepository.Get(entidade.ID);
                var funcoes = _funcaoRepository.Find(x => idfuncoes.Contains(x.ID)).ToList();

                var idFuncoesExistentes = perfil.WAC_FUNCAO.Select(x => x.ID).ToArray();
                var idFuncoesNovas = idfuncoes.Where(x => !idFuncoesExistentes.Contains(x)).ToArray();
                var idFuncoesRemover = idFuncoesExistentes.Where(x => !idfuncoes.Contains(x)).ToArray();
                var funcoesRemover = perfil.WAC_FUNCAO.Where(x => idFuncoesRemover.Contains(x.ID)).ToList();

                funcoesRemover.ForEach(x => perfil.WAC_FUNCAO.Remove(x));
                funcoes.Where(y => idFuncoesNovas.Contains(y.ID)).ToList().ForEach(x => perfil.WAC_FUNCAO.Add(x));

                perfil.PERFIL_NM = entidade.PERFIL_NM;
                perfil.PERFIL_DSC = entidade.PERFIL_DSC;

                _perfilRepository.Update(perfil);


                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro tentar alterar Perfil", ex);
            }
        }

        public Perfil InserirPerfil(Perfil entidade)
        {
            try
            {
                _perfilRepository.Add(entidade);

                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar Inserir um Perfil", ex);
            }
        }

        public Perfil InserirPerfilFuncoes(Perfil entidade)
        {
            try
            {
                var idFuncoes = entidade.WAC_FUNCAO.Select(y => y.ID).ToArray();
                entidade.WAC_FUNCAO = _funcaoRepository.Find(x => idFuncoes.Contains(x.ID)).ToList();
                _perfilRepository.Add(entidade);

                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar inserir um Perfil com Funções ", ex);
            }
        }

        public Perfil ExcluirPerfil(int id)
        {
            try
            {
                var perfil = _perfilRepository.Get(id);
                var funcoes = perfil.WAC_FUNCAO.ToList();
                funcoes.ForEach(x => perfil.WAC_FUNCAO.Remove(x));

                _perfilRepository.Delete(perfil);

                return perfil;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw new ServiceWebForLinkException("Não foi possível excluir este Perfil.", ex);
            }
        }

        public RetornoPesquisa<Perfil> PesquisarPerfil(PesquisaPerfilFiltrosDTO filtros, int pagina, int tamanhoPagina)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }
    }
}