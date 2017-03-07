using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class SolicitacaoCadastroFornecedorWebForLinkAppService
        : AppService<WebForLinkContexto>, ISolicitacaoCadastroFornecedorWebForLinkAppService
    {
        private readonly ISolicitacaoCadastroFornecedorWebForLinkService _solicitacaoCadastroFornecedorService;

        public SolicitacaoCadastroFornecedorWebForLinkAppService(
            ISolicitacaoCadastroFornecedorWebForLinkService solicitacaoCadastroFornecedorService)
        {
            try
            {
                _solicitacaoCadastroFornecedorService = solicitacaoCadastroFornecedorService;
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
        public SolicitacaoCadastroFornecedor BuscarPorId(int id)
        {
            try
            {
                return _solicitacaoCadastroFornecedorService.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public void Alterar(SolicitacaoCadastroFornecedor solForn)
        {
            try
            {
                _solicitacaoCadastroFornecedorService.Update(solForn);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao alterar uma solicitação de cadastro de fornecedor", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="razaoSocial"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorRazaoSocial(string razaoSocial)
        {
            try
            {
                return
                    _solicitacaoCadastroFornecedorService.Get(
                        x => x.NOME.Contains(razaoSocial) || x.RAZAO_SOCIAL.Contains(razaoSocial));
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cnpjteste"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorStatusId(string cnpjteste, int statusId)
        {
            try
            {
                return
                    _solicitacaoCadastroFornecedorService.Get(
                        x =>
                            x.WFD_SOLICITACAO.SOLICITACAO_STATUS_ID != statusId &&
                            (x.CNPJ == cnpjteste || x.CPF == cnpjteste));
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public SolicitacaoCadastroFornecedor ValidarSolicitacaoCriacao(int statusSolicitacao, string documento,
            int contratante)
        {
            try
            {
                return
                    _solicitacaoCadastroFornecedorService.Get(
                        x => x.WFD_SOLICITACAO.SOLICITACAO_STATUS_ID != statusSolicitacao
                             && (x.CNPJ == documento || x.CPF == documento)
                             && x.WFD_SOLICITACAO.CONTRATANTE_ID == contratante);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public int BuscarIdSolicitacaoPorCnpj(string cnpj)
        {
            try
            {
                return _solicitacaoCadastroFornecedorService.Get(x => x.CNPJ == cnpj).SOLICITACAO_ID;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorSolicitacaoId(int solicitacaoId)
        {
            return _solicitacaoCadastroFornecedorService.BuscarPorSolicitacaoID(solicitacaoId);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorSolicitacaoIdComDocumentos(int solicitacaoId)
        {
            return _solicitacaoCadastroFornecedorService.BuscarPorSolicitacaoIDComDocumentos(solicitacaoId);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        public void AtualizarSolicitacao(SolicitacaoCadastroFornecedor solicitacao)
        {
            BeginTransaction();
            _solicitacaoCadastroFornecedorService.Update(solicitacao);
            Commit();
            //Dispose();
        }

        /// <summary>
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public string BuscarRazaoSocialPorCnpj(string cnpj)
        {
            return _solicitacaoCadastroFornecedorService.BuscarRazaoSocialPorCnpj(cnpj);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoId"></param>
        /// <returns></returns>
        public string BuscarRazaoOuNomePorSolicitacao(int solicitacaoId)
        {
            var cadforn = _solicitacaoCadastroFornecedorService.BuscarCadFornPorSolicitacao(solicitacaoId);
            if (cadforn.PJPF_TIPO == 1)
                return cadforn.RAZAO_SOCIAL;
            return cadforn.NOME;
        }

        public string BuscarRazaoSocialPorCpf(string cpf)
        {
            return _solicitacaoCadastroFornecedorService.BuscarRazaoSocialPorcpf(cpf);
        }

        public void Dispose()
        {
        }

        public SolicitacaoCadastroFornecedor Buscar(Expression<Func<SolicitacaoCadastroFornecedor, bool>> filtro)
        {
            return _solicitacaoCadastroFornecedorService.Get(filtro);
        }
        
        public SolicitacaoCadastroFornecedor Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoCadastroFornecedor Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoCadastroFornecedor GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoCadastroFornecedor> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoCadastroFornecedor> Find(
            Expression<Func<SolicitacaoCadastroFornecedor, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(SolicitacaoCadastroFornecedor entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(SolicitacaoCadastroFornecedor entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(SolicitacaoCadastroFornecedor entity)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoCadastroFornecedor Get(int id)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoCadastroFornecedor Get(Expression<Func<SolicitacaoCadastroFornecedor, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoCadastroFornecedor> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SolicitacaoCadastroFornecedor> Find(
            Expression<Func<SolicitacaoCadastroFornecedor, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}