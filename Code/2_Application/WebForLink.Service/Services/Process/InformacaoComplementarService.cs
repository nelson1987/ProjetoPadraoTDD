using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;

namespace WebForLink.Application.Services.Process
{
    public class InformacaoComplementarWebForLinkAppService : AppService<WebForLinkContexto>,
        IInformacaoComplementarWebForLinkAppService
    {
        private readonly IFornecedorInformacaoComplementarComplService _informacaoComplementarFornecedorService;
        private readonly IInformacaoComplementarWebForLinkService _informacaoComplementarService;

        public InformacaoComplementarWebForLinkAppService(
            IInformacaoComplementarWebForLinkService informacaoComplementar,
            IFornecedorInformacaoComplementarComplService informacaoComplementarFornecedor)
        {
            try
            {
                _informacaoComplementarService = informacaoComplementar;
                _informacaoComplementarFornecedorService = informacaoComplementarFornecedor;
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
        public WFD_INFORM_COMPL BuscarPorId(int id)
        {
            try
            {
                return _informacaoComplementarService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idPergunta"></param>
        /// <returns></returns>
        public WFD_INFORM_COMPL BuscarPorPerguntaId(int idPergunta)
        {
            try
            {
                return _informacaoComplementarService.Get(x => x.PERG_ID == idPergunta);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idPergunta"></param>
        /// <param name="idSolicitacao"></param>
        /// <returns></returns>
        public WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoId(int idPergunta, int idSolicitacao)
        {
            try
            {
                return
                    _informacaoComplementarService.Get(x => x.PERG_ID == idPergunta && x.SOLICITACAO_ID == idSolicitacao);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="idPergunta"></param>
        /// <param name="idSolicitacao"></param>
        /// <param name="resposta"></param>
        /// <returns></returns>
        public WFD_INFORM_COMPL BuscarPorPerguntaIdSolicitacaoIdResposta(int idPergunta, int idSolicitacao,
            string resposta)
        {
            try
            {
                return _informacaoComplementarService.Get(x => x.PERG_ID == idPergunta
                                                               && x.SOLICITACAO_ID == idSolicitacao
                                                               && x.RESPOSTA == resposta);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<WFD_INFORM_COMPL> UpdateAll(List<WFD_INFORM_COMPL> entidade)
        {
            try
            {
                foreach (var item in entidade)
                {
                    var novo = _informacaoComplementarService.Get(item.ID);
                    if (!item.Equals(novo))
                    {
                        //Db.Entry(novo).State = EntityState.Detached;
                        //Desalocar o Id
                        //Db.Entry(item).State = EntityState.Modified;
                        _informacaoComplementarService.Update(novo);
                    }
                }
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir uma Pergunta", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<WFD_INFORM_COMPL> InserirTodos(List<WFD_INFORM_COMPL> entidade)
        {
            try
            {
                foreach (var item in entidade)
                {
                    if (item.ID != 0)
                    {
                        var novo = BuscarPorId(item.ID);
                        if (!item.Equals(novo))
                        {
                            //Db.Entry(item).State = EntityState.Added;
                            _informacaoComplementarService.Add(item);
                        }
                    }
                    else
                    //Db.Entry(item).State = EntityState.Added;
                        _informacaoComplementarService.Add(item);
                }
                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao inserir uma Pergunta", ex);
            }
        }

        public FORNECEDOR_INFORM_COMPL BuscarPorPerguntaIdPJPFId(int idPergunta, int idPJPF)
        {
            try
            {
                return
                    _informacaoComplementarFornecedorService.Get(
                        x => x.PERG_ID == idPergunta && x.CONTRATANTE_PJPF_ID == idPJPF);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public WFD_INFORM_COMPL ValidaExistente(WFD_INFORM_COMPL entidade)
        {
            return BuscarPorPerguntaIdSolicitacaoId(entidade.PERG_ID, entidade.SOLICITACAO_ID);
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public bool ValidaDuplicado(WFD_INFORM_COMPL entidade)
        {
            if (
                BuscarPorPerguntaIdSolicitacaoIdResposta(entidade.PERG_ID, entidade.SOLICITACAO_ID, entidade.RESPOSTA) !=
                null)
                return true;
            return false;
        }

        /// <summary>
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns></returns>
        public List<WFD_INFORM_COMPL> InsertAll(List<WFD_INFORM_COMPL> entidade)
        {
            try
            {
                var insertList = new List<WFD_INFORM_COMPL>();
                var updateList = new List<WFD_INFORM_COMPL>();

                foreach (var item in entidade)
                {
                    if (item.RESPOSTA == null)
                        continue;

                    if (!ValidaDuplicado(item)) //Valida se existe
                    {
                        var infoCompl = ValidaExistente(item);
                        if (infoCompl != null) //Valida se existe mas há mudança
                        {
                            item.ID = infoCompl.ID;
                            updateList.Add(item);
                        }
                        else //Valida se não existe
                        {
                            insertList.Add(item);
                        }
                    }
                }
                if (insertList.Any())
                    InserirTodos(insertList);
                if (updateList.Any())
                    UpdateAll(updateList);

                return entidade;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao cadastrar Perguntas", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}