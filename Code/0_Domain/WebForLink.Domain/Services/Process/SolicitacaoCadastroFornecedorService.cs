using System;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface ISolicitacaoCadastroFornecedorWebForLinkService : IService<SolicitacaoCadastroFornecedor>
    {
        SolicitacaoCadastroFornecedor BuscarPorId(int id);
        void Alterar(SolicitacaoCadastroFornecedor solForn);
        SolicitacaoCadastroFornecedor BuscarPorRazaoSocial(string razaoSocial);
        SolicitacaoCadastroFornecedor BuscarPorStatusId(string cnpjteste, int statusId);
        SolicitacaoCadastroFornecedor ValidarSolicitacaoCriacao(int statusSolicitacao, string documento, int contratante);
        int BuscarIdSolicitacaoPorCnpj(string cnpj);
        SolicitacaoCadastroFornecedor BuscarPorSolicitacaoID(int solicitacaoID);
        SolicitacaoCadastroFornecedor BuscarPorSolicitacaoIDComDocumentos(int solicitacaoID);
        void AtualizarSolicitacao(SolicitacaoCadastroFornecedor solicitacao);
        string BuscarRazaoSocialPorCnpj(string cnpj);
        string BuscarRazaoOuNomePorSolicitacao(int solicitacaoID);
        string BuscarRazaoSocialPorCpf(string cpf);
        SolicitacaoCadastroFornecedor Buscar(Expression<Func<SolicitacaoCadastroFornecedor, bool>> filtro);
        string BuscarRazaoSocialPorcpf(string cpf);
        SolicitacaoCadastroFornecedor BuscarCadFornPorSolicitacao(int solicitacaoId);
    }

    public class SolicitacaoCadastroFornecedorWebForLinkService : Service<SolicitacaoCadastroFornecedor>,
        ISolicitacaoCadastroFornecedorWebForLinkService
    {
        private readonly ISolicitacaoCadastroFornecedorWebForLinkRepository _solicitacaoCadastroFornecedorRepository;

        public SolicitacaoCadastroFornecedorWebForLinkService(
            ISolicitacaoCadastroFornecedorWebForLinkRepository solicitacaoCadastroFornecedorRepository)
            : base(solicitacaoCadastroFornecedorRepository)
        {
            try
            {
                _solicitacaoCadastroFornecedorRepository = solicitacaoCadastroFornecedorRepository;
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
                return _solicitacaoCadastroFornecedorRepository.Get(id);
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
                _solicitacaoCadastroFornecedorRepository.Update(solForn);
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
                return _solicitacaoCadastroFornecedorRepository
                    .Find(x => x.NOME.Contains(razaoSocial) || x.RAZAO_SOCIAL.Contains(razaoSocial))
                    .FirstOrDefault();
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
                return _solicitacaoCadastroFornecedorRepository
                    .Find(
                        x =>
                            x.WFD_SOLICITACAO.SOLICITACAO_STATUS_ID != statusId &&
                            (x.CNPJ == cnpjteste || x.CPF == cnpjteste))
                    .FirstOrDefault();
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
                return _solicitacaoCadastroFornecedorRepository
                    .Find(x => x.WFD_SOLICITACAO.SOLICITACAO_STATUS_ID != statusSolicitacao
                               && (x.CNPJ == documento || x.CPF == documento)
                               && x.WFD_SOLICITACAO.CONTRATANTE_ID == contratante)
                    .FirstOrDefault();
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
                return _solicitacaoCadastroFornecedorRepository.Get(x => x.CNPJ == cnpj).SOLICITACAO_ID;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoID"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorSolicitacaoID(int solicitacaoID)
        {
            return _solicitacaoCadastroFornecedorRepository.BuscarPorSolicitacaoID(solicitacaoID);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoID"></param>
        /// <returns></returns>
        public SolicitacaoCadastroFornecedor BuscarPorSolicitacaoIDComDocumentos(int solicitacaoID)
        {
            return _solicitacaoCadastroFornecedorRepository.BuscarPorSolicitacaoIDComDocumentos(solicitacaoID);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacao"></param>
        public void AtualizarSolicitacao(SolicitacaoCadastroFornecedor solicitacao)
        {
            _solicitacaoCadastroFornecedorRepository.Update(solicitacao);
            //Dispose();
        }

        /// <summary>
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public string BuscarRazaoSocialPorCnpj(string cnpj)
        {
            return _solicitacaoCadastroFornecedorRepository.BuscarRazaoSocialPorCnpj(cnpj);
        }

        /// <summary>
        /// </summary>
        /// <param name="solicitacaoID"></param>
        /// <returns></returns>
        public string BuscarRazaoOuNomePorSolicitacao(int solicitacaoID)
        {
            var cadforn = _solicitacaoCadastroFornecedorRepository.BuscarCadFornPorSolicitacao(solicitacaoID);
            if (cadforn.PJPF_TIPO == 1)
                return cadforn.RAZAO_SOCIAL;
            return cadforn.NOME;
        }

        public string BuscarRazaoSocialPorCpf(string cpf)
        {
            return _solicitacaoCadastroFornecedorRepository.BuscarRazaoSocialPorcpf(cpf);
        }

        public SolicitacaoCadastroFornecedor Buscar(Expression<Func<SolicitacaoCadastroFornecedor, bool>> filtro)
        {
            return _solicitacaoCadastroFornecedorRepository.Find(filtro).FirstOrDefault();
        }

        public string BuscarRazaoSocialPorcpf(string cpf)
        {
            throw new NotImplementedException();
        }

        public SolicitacaoCadastroFornecedor BuscarCadFornPorSolicitacao(int solicitacaoId)
        {
            return Buscar(x => x.SOLICITACAO_ID == solicitacaoId);
        }

        public void Dispose()
        {
        }
    }
}