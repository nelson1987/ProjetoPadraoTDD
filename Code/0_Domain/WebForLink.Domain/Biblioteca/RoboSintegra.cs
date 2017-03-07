using System;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Domain.Biblioteca
{
    /// <summary>
    ///     Essa classe consiste em ser o Robo para consultas ao sintegra, utilizando o webservice do vendor Lanna.
    /// </summary>
    public class RoboSintegraWebForLinkAppService
    {
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private readonly BibliotecaWFL _metodosGerais = new BibliotecaWFL();
        public int? Code { get; set; }
        public string HTML { get; set; }
        public DataSintegra Data { get; set; }
        public string UUID { get; set; }
        public int tpPapel { get; set; }
        public DateTime? DataConsulta { get; set; }
        public string cssCor { get; set; }

        public void GravaRoboSintegra(RoboSintegraWebForLinkAppService roboSintegra, ref ROBO robo)
        {
            MontaRobo(roboSintegra, robo);
        }

        private void MontaRobo(RoboSintegraWebForLinkAppService roboSintegra, ROBO robo)
        {
            if (roboSintegra != null)
            {
                if (roboSintegra.Code == 1)
                {
                    #region Parse dos Dados do Robô

                    var asAsciiSintegra = string.Empty;
                    if (!string.IsNullOrEmpty(roboSintegra.HTML))
                    {
                        asAsciiSintegra = _metodosGerais.EncodeCodigoHtml(roboSintegra.HTML);
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
    }

    public class DataSintegra
    {
        public string DataSituacaoCadastral { get; set; }
        public string InscricaoEstadual { get; set; }
        public string Contingencia { get; set; }
        public string SituacaoCadastral { get; set; }
        public string Complemento { get; set; }
        public string RazaoSocial { get; set; }
        //O Robô Não Retorna Esta Informação
        public string SituacaoEFD { get; set; }
        public bool? MultiplasIE { get; set; }
        public string Telefone { get; set; }
        //O Robô Não Retorna Esta Informação
        public string EmissaoNFEObrigatorio { get; set; }
        //O Robô Não Retorna Esta Informação
        public string PerfilEFD { get; set; }
        public string CNPJ { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string CEP { get; set; }
        public string Municipio { get; set; }
        //O Robô Não Retorna Esta Informação
        public string CTE { get; set; }
        public string AtividadeEconomicaPrincipal { get; set; }
        public string DataInclusao { get; set; }
        public string UF { get; set; }
        //O Robô Não Retorna Esta Informação
        public string Message { get; set; }
        //Novas Propriedades no Retorno do Robô
        public string EnquadramentoFiscal { get; set; }
    }
}