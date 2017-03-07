using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class PerfilWebForLinkAppService : AppService<WebForLinkContexto>, IPerfilWebForLinkAppService
    {
        private readonly IFuncaoWebForLinkService _funcaoService;
        private readonly IPerfilWebForLinkService _perfilService;

        public PerfilWebForLinkAppService(IFuncaoWebForLinkService funcao, IPerfilWebForLinkService perfil)
        {
            try
            {
                _funcaoService = funcao;
                _perfilService = perfil;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public List<Perfil> ListarTodos()
        {
            try
            {
                return _perfilService.All().ToList();
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
                return _perfilService.Find(x => perfilId.Contains(x.ID)).ToList();
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

        public RetornoPesquisa<Perfil> PesquisarPerfil(PesquisaPerfilFiltrosDTO filtros, int pagina, int tamanhoPagina)
        {
            throw new NotImplementedException();
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
        //        return _perfilService.Pesquisar(predicate, tamanhoPagina, pagina, x => x.ID);
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
                return _perfilService.Get(id);
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
                var perfil = _perfilService.Get(entidade.ID);
                var funcoes = _funcaoService.Find(x => idfuncoes.Contains(x.ID)).ToList();

                var idFuncoesExistentes = perfil.WAC_FUNCAO.Select(x => x.ID).ToArray();
                var idFuncoesNovas = idfuncoes.Where(x => !idFuncoesExistentes.Contains(x)).ToArray();
                var idFuncoesRemover = idFuncoesExistentes.Where(x => !idfuncoes.Contains(x)).ToArray();
                var funcoesRemover = perfil.WAC_FUNCAO.Where(x => idFuncoesRemover.Contains(x.ID)).ToList();

                funcoesRemover.ForEach(x => perfil.WAC_FUNCAO.Remove(x));
                funcoes.Where(y => idFuncoesNovas.Contains(y.ID)).ToList().ForEach(x => perfil.WAC_FUNCAO.Add(x));

                perfil.PERFIL_NM = entidade.PERFIL_NM;
                perfil.PERFIL_DSC = entidade.PERFIL_DSC;

                _perfilService.Update(perfil);

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
                _perfilService.Add(entidade);
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
                entidade.WAC_FUNCAO = _funcaoService.Find(x => idFuncoes.Contains(x.ID)).ToList();
                _perfilService.Add(entidade);
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
                var perfil = _perfilService.Get(id);
                var funcoes = perfil.WAC_FUNCAO.ToList();
                funcoes.ForEach(x => perfil.WAC_FUNCAO.Remove(x));

                _perfilService.Delete(perfil);
                return perfil;
            }
            catch (Exception ex)
            {
                //Log.Error(ex);
                throw new ServiceWebForLinkException("Não foi possível excluir este Perfil.", ex);
            }
        }

        public Perfil Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Perfil Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public Perfil GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Perfil> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Perfil> Find(Expression<Func<Perfil, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(Perfil entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(Perfil entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(Perfil entity)
        {
            throw new NotImplementedException();
        }

        public Perfil Get(int id)
        {
            throw new NotImplementedException();
        }

        public Perfil Get(Expression<Func<Perfil, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Perfil> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Perfil> Find(Expression<Func<Perfil, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}