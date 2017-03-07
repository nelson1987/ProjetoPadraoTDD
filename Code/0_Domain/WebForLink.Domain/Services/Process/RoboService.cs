using System;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Robos;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class RoboWebForLinkService : Service<ROBO>, IRoboWebForLinkService
    {
        private readonly IFornecedorBaseWebForLinkRepository _fornecedorBaseRepository;
        private readonly ILogRoboWebForLinkRepository _logRoboRepository;
        private readonly IRoboWebForLinkRepository _roboFornecedorRepository;
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;

        public RoboWebForLinkService(
            ISolicitacaoWebForLinkRepository solicitacaoRepository,
            IRoboWebForLinkRepository roboFornecedorRepository,
            IFornecedorBaseWebForLinkRepository fornecedorBaseRepository,
            ILogRoboWebForLinkRepository logRoboRepository) : base(roboFornecedorRepository)
        {
            try
            {
                _solicitacaoRepository = solicitacaoRepository;
                _roboFornecedorRepository = roboFornecedorRepository;
                _fornecedorBaseRepository = fornecedorBaseRepository;
                _logRoboRepository = logRoboRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public void Inserir(ROBO robo)
        {
            try
            {
                _roboFornecedorRepository.Add(robo);
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
                    _roboFornecedorRepository.Add(robo);
                    pjpf.ROBO = null;
                    pjpf.ROBO = robo;
                    _fornecedorBaseRepository.Update(pjpf);
                }
                else
                    _fornecedorBaseRepository.Update(pjpf);

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
                //if (string.IsNullOrEmpty(robo.LATITUDE))
                //{
                //    GeoEndereco teste = new GeoEndereco()
                //    {
                //        Bairro = robo.RF_BAIRRO,
                //        Cidade = robo.RF_MUNICIPIO,
                //        Cep = robo.RF_CEP,
                //        Rua = robo.RF_LOGRADOURO,
                //        Numero = robo.RF_NUMERO,
                //        Pais = "Brasil",
                //        Estado = robo.RF_UF,
                //    };
                //    GeoResponse resposta = GeoLocation.GetAddress(teste);
                //    if (resposta.Status == "OK")
                //    {
                //        robo.LATITUDE = resposta.Resultados[0].GeoPosicao.Localizacao.Latitude;
                //        robo.LONGITUDE = resposta.Resultados[0].GeoPosicao.Localizacao.Longitude;
                //    }
                //}
                _roboFornecedorRepository.Update(robo);
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
                var solicitacao = _solicitacaoRepository.Get(solicitacaoId);
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
                _roboFornecedorRepository.Update(robo);
                _logRoboRepository.Add(entityLog);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao tentar gravar os dados do Robô", ex);
            }
        }
    }
}