using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Biblioteca;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class RoboSintegraWebForLinkAppService : AppService<WebForLinkContexto>, IRoboSintegraWebForLinkAppService
    {
        private readonly BibliotecaWFL _metodosGerais = new BibliotecaWFL();

        public int? Code { get; set; }
        public string HTML { get; set; }

        public DataSintegra Data { get; set; }
        public string UUID { get; set; }
        public int tpPapel { get; set; }
        public DateTime? DataConsulta { get; set; }
        public string cssCor { get; set; }

        private readonly ISolicitacaoWebForLinkService _solicitacaoService;

        public RoboSintegraWebForLinkAppService(ISolicitacaoWebForLinkService solicitacaoService)
        {
            try
            {
                _solicitacaoService = solicitacaoService;
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
            var solicitacao = _solicitacaoService.Get(solicitacaoId);
            var solForn = solicitacao.SolicitacaoCadastroFornecedor.First();

            var robo = solicitacao.ROBO.FirstOrDefault();

            GravaRoboSintegra(sintegra);
            //_roboFornecedorService.Update(robo);

            if (sintegra.Code == 1)
            {
                solForn.INSCR_ESTADUAL = sintegra.Data.InscricaoEstadual;
                //_solicitacaoCadastroFornecedorService.Update(solForn);
            }

            //_solicitacaoService.Update(solicitacao);

            var entityLog = new ROBO_LOG
            {
                COD_RETORNO = sintegra.Code,
                DATA = DateTime.Now,
                MENSAGEM = sintegra.Data.Message,
                ROBO = EnumRobo.Sintegra.ToString(),
                WFD_SOLICITACAO = solicitacao,
                CONTRATANTE_ID = solicitacao.CONTRATANTE_ID
            };
            //_logRoboService.Add(entityLog);
            //Processo.Finalizar();


            solicitacao.WFD_PJPF_ROBO_LOG.Add(entityLog);
            _solicitacaoService.Update(solicitacao);
        }

        public void InserirRoboSintegraSolicitacao(Domain.Biblioteca.RoboSintegraWebForLinkAppService roboSintegraDominio, int solicitacaoID)
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

        public IEnumerable<SolicitacaoCadastroFornecedor> Find(Expression<Func<SolicitacaoCadastroFornecedor, bool>> predicate, bool @readonly = false)
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

        public IEnumerable<SolicitacaoCadastroFornecedor> Find(Expression<Func<SolicitacaoCadastroFornecedor, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}