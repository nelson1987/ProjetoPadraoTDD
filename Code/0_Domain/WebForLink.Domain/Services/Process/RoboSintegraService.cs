using System;
using System.Linq;
using WebForLink.Domain.Biblioteca;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public class RoboSintegraWebForLinkService : Service<SOLICITACAO>
    {
        private readonly ISolicitacaoWebForLinkRepository _solicitacaoRepository;

        public RoboSintegraWebForLinkService(ISolicitacaoWebForLinkRepository solicitacaoRepository)
            : base(solicitacaoRepository)
        {
            try
            {
                _solicitacaoRepository = solicitacaoRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
        }

        public void GravaRoboSintegra(RoboSintegraWebForLinkAppService roboSintegra)
        {
            var robo = new ROBO();
            if (roboSintegra != null)
            {
                if (roboSintegra.Code == 1)
                {
                    #region Parse dos Dados do Robô

                    var asAsciiSintegra = string.Empty;
                    if (!string.IsNullOrEmpty(roboSintegra.HTML))
                    {
                        //asAsciiSintegra = _metodosGerais.EncodeCodigoHtml(roboSintegra.HTML);
                    }

                    DateTime dataInclusao;

                    DateTime.TryParse(roboSintegra.Data.DataInclusao, out dataInclusao);

                    #endregion

                    robo.SINT_CODE_ROBO = roboSintegra.Code;
                    robo.SINT_CERTIFICADO_HTML = asAsciiSintegra.Replace("imagens/sintegra.gif",
                        "http://www.fazenda.rj.gov.br/projetoCPS/imagens/sintegra.gif");
                    robo.SINT_CONSULTA_DTHR = DateTime.Now;
                    robo.SINT_CONTADOR_TENTATIVA = 0;
                    robo.SINT_IE_COD = roboSintegra.Data.InscricaoEstadual;
                    robo.SINT_IE_MULTIPLA = (roboSintegra.Data.MultiplasIE.HasValue &&
                                             !roboSintegra.Data.MultiplasIE.Value)
                        ? "Não"
                        : "Sim";
                    robo.SINT_IE_SITU_CADASTRAL = roboSintegra.Data.SituacaoCadastral;
                    robo.SINT_IE_SITU_CADSTRAL_DT = roboSintegra.Data.DataSituacaoCadastral;
                    robo.SINT_INCLUSAO_DT = dataInclusao;
                    robo.SINT_TEL = roboSintegra.Data.Telefone;
                    robo.SINT_ATIVIDADE_PRINCIPAL = roboSintegra.Data.AtividadeEconomicaPrincipal;
                    robo.SINT_BAIRRO = roboSintegra.Data.Bairro;
                    robo.SINT_CEP = roboSintegra.Data.CEP;
                    robo.SINT_CNPJ = roboSintegra.Data.CNPJ;
                    robo.SINT_COMPLEMENTO = roboSintegra.Data.Complemento;
                    robo.SINT_ENQUADRAMENTO_FISCAL = roboSintegra.Data.EnquadramentoFiscal;
                    robo.SINT_LOGRADOURO = roboSintegra.Data.Logradouro;
                    robo.SINT_MUNICIPIO = roboSintegra.Data.Municipio;
                    robo.SINT_NUMERO = roboSintegra.Data.Numero;
                    robo.SINT_RAZAO_SOCIAL = roboSintegra.Data.RazaoSocial;
                    robo.SINT_UF = roboSintegra.Data.UF;
                    robo.SINT_TEL = roboSintegra.Data.Telefone;
                }
                else
                {
                    var contador = robo.SINT_CONTADOR_TENTATIVA;
                    if (!contador.HasValue) contador = 0;

                    if (roboSintegra.Code == 2 || roboSintegra.Code == 3)
                    {
                        robo.SINT_CODE_ROBO = roboSintegra.Code;
                        robo.SINT_IE_SITU_CADASTRAL = roboSintegra.Data.Message;
                        robo.SINT_CONSULTA_DTHR = DateTime.Now;
                        robo.SINT_CONTADOR_TENTATIVA = 0;
                    }
                    else
                    {
                        contador += 1;
                        robo.SINT_CONTADOR_TENTATIVA = contador;
                    }
                }
            }
        }

        public void InserirRoboSintegraSolicitacao(RoboSintegraWebForLinkAppService sintegra, int solicitacaoId)
        {
            var solicitacao = _solicitacaoRepository.Find(x => x.ID == solicitacaoId).FirstOrDefault();
            var solForn = solicitacao.SolicitacaoCadastroFornecedor.First();

            var robo = solicitacao.ROBO.FirstOrDefault();

            GravaRoboSintegra(sintegra);
            //_roboFornecedorRepository.Update(robo);

            if (sintegra.Code == 1)
            {
                solForn.INSCR_ESTADUAL = sintegra.Data.InscricaoEstadual;
                //_solicitacaoCadastroFornecedorRepository.Update(solForn);
            }

            //_solicitacaoRepository.Update(solicitacao);

            var entityLog = new ROBO_LOG
            {
                COD_RETORNO = sintegra.Code,
                DATA = DateTime.Now,
                MENSAGEM = sintegra.Data.Message,
                ROBO = EnumRobo.Sintegra.ToString(),
                WFD_SOLICITACAO = solicitacao,
                CONTRATANTE_ID = solicitacao.CONTRATANTE_ID
            };
            //_logRoboRepository.Add(entityLog);
            //Processo.Finalizar();


            solicitacao.WFD_PJPF_ROBO_LOG.Add(entityLog);
            _solicitacaoRepository.Update(solicitacao);
        }
    }
}