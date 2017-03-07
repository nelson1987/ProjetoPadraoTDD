using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Robos;
using WebForLink.Domain.Services.Process;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class RoboWebForLinkAppService : AppService<WebForLinkContexto>, IRoboWebForLinkAppService
    {
        private readonly IFornecedorBaseWebForLinkService _fornecedorBaseService;
        private readonly ILogRoboWebForLinkService _logRoboService;
        private readonly IRoboWebForLinkService _roboFornecedorService;
        private readonly ISolicitacaoWebForLinkService _solicitacaoService;

        public RoboWebForLinkAppService(
            ISolicitacaoWebForLinkService solicitacaoService,
            IRoboWebForLinkService roboFornecedorService,
            IFornecedorBaseWebForLinkService fornecedorBaseService,
            ILogRoboWebForLinkService logRoboService)
        {
            try
            {
                _solicitacaoService = solicitacaoService;
                _roboFornecedorService = roboFornecedorService;
                _fornecedorBaseService = fornecedorBaseService;
                _logRoboService = logRoboService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public ROBO Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ROBO Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ROBO GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ROBO> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ROBO> Find(Expression<Func<ROBO, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(ROBO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(ROBO entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(ROBO entity)
        {
            throw new NotImplementedException();
        }

        public ROBO Get(int id)
        {
            throw new NotImplementedException();
        }

        public ROBO Get(Expression<Func<ROBO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ROBO> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ROBO> Find(Expression<Func<ROBO, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public void Inserir(ROBO robo)
        {
            try
            {
                _roboFornecedorService.Add(robo);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar incluir os dados do Robô", ex);
            }
        }

        public bool GravaRoboImportacao(FORNECEDORBASE pjpf, ROBO robo)
        {
            try
            {
                if (robo.ID == 0)
                {
                    _roboFornecedorService.Add(robo);
                    pjpf.ROBO = null;
                    pjpf.ROBO = robo;
                    _fornecedorBaseService.Update(pjpf);
                }
                else
                    _fornecedorBaseService.Update(pjpf);

                return true;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar gravar os dados do Robô", ex);
            }
        }

        public void Alterar(ROBO robo)
        {
            try
            {
                _roboFornecedorService.Update(robo);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar gravar os dados do Robô", ex);
            }
        }

        public void GravaRoboSimplesNacional(int solicitacaoId, SimplesNacional roboSimples)
        {
            try
            {
                var solicitacao = _solicitacaoService.Get(solicitacaoId);
                var robo = solicitacao.ROBO.FirstOrDefault();

                if (roboSimples != null)
                {
                    if (roboSimples.Code == 1)
                    {
                        robo.SN_CODE_ROBO = roboSimples.Code;
                        robo.SIMPLES_NACIONAL_SITUACAO = (!string.IsNullOrEmpty(roboSimples.SituacaoSimplesNacional))
                            ? roboSimples.SituacaoSimplesNacional
                            : string.Empty;
                        robo.SN_PERIODOS_ANTERIORES = roboSimples.SimplesNacionalPeriodosAnteriores;
                        robo.SN_SIMEI_PERIODOS_ANTERIORES = roboSimples.SIMEIPeriodosAnteriores;
                        robo.SN_SITUACAO_SIMEI = roboSimples.SituacaoSIMEI;
                        robo.SN_RAZAO_SOCIAL = roboSimples.RazaoSocial;
                        robo.SN_CONSULTA_DTHR = DateTime.Now;
                    }
                    else
                    {
                        var contador = robo.SN_CONTADOR_TENTATIVA;
                        if (!contador.HasValue) contador = 0;

                        if (roboSimples.Code == 2 || roboSimples.Code == 3)
                        {
                            robo.SN_CODE_ROBO = roboSimples.Code;
                            robo.SIMPLES_NACIONAL_SITUACAO = roboSimples.Message;
                            robo.SN_CONSULTA_DTHR = DateTime.Now;
                            robo.SN_CONTADOR_TENTATIVA = 0;
                        }
                        else
                        {
                            contador += 1;
                            robo.SN_CONTADOR_TENTATIVA = contador;
                        }
                    }
                }

                var entityLog = new ROBO_LOG
                {
                    COD_RETORNO = roboSimples.Code,
                    DATA = DateTime.Now,
                    MENSAGEM = roboSimples.Message,
                    ROBO = EnumRobo.SimplesNacional.ToString(),
                    WFD_SOLICITACAO = solicitacao,
                    SOLICITACAO_ID = solicitacao.CONTRATANTE_ID
                };
                _roboFornecedorService.Update(robo);
                _logRoboService.Add(entityLog);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar gravar os dados do Robô", ex);
            }
        }
    }
}