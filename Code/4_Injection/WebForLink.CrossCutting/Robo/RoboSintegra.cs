using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using WebForLink.Domain.Models;
using WebForLink.Web.Interfaces;

namespace WebForLink.Web.Infrastructure
{
    /// <summary>
    /// Essa classe consiste em ser o Robo para consultas ao sintegra, utilizando o webservice do vendor Lanna.
    /// </summary>
    public class RoboSintegra
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly IGeral _metodosGerais = new Geral();

        public int? Code { get; set; }        
        public string HTML { get; set; }        
        [JsonProperty("data")]        
        public DataSintegra Data { get; set; }        
        public string UUID { get; set; }
        public int tpPapel { get; set; }
        public DateTime? DataConsulta { get; set; }
        public string cssCor { get; set; }

        public RoboSintegra CarregaSintegra(string estado, string cnpj, string path)
        {
            RoboSintegra robo = new RoboSintegra();

            try
            {

                if (!string.IsNullOrEmpty(estado) && !string.IsNullOrEmpty(cnpj))
                {
                    var ativarConsultasFake = Convert.ToBoolean(ConfigurationManager.AppSettings["AtivarConsultasFake"]);
                    string textResult = string.Empty;

                    if (ativarConsultasFake)
                        textResult = Sintegra_fake(path);
                    else 
                    {
                        string token = ConfigurationManager.AppSettings["SintegraToken"];
                        string cont = "7";

                        string url = String.Format(@"http://webservice.keyconsultas.net/sintegra_{0}/cnpj/?cnpj={1}&token={2}&cont={3}", estado, cnpj, token, cont);

                        var request = (HttpWebRequest)WebRequest.Create(url);
                        HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                        Stream stream = response.GetResponseStream();
                        StreamReader reader = new StreamReader(stream);
                        textResult = reader.ReadToEnd();
                    }                  

                    robo = JsonConvert.DeserializeObject<RoboSintegra>(textResult);
                }

                return robo;
            }
            catch
            {
                robo.Code = 0;
                robo.Data = new DataSintegra();
                robo.Data.Message = "Não foi possível acessar o serviço de consulta dos Orgãos Públicos! Tente novamente.";
                
                return robo;
            }
        }

        public void GravaRoboSintegra(RoboSintegra roboSintegra, ref ROBO robo)
        {
            MontaRobo(roboSintegra, robo);
        }

        private void MontaRobo(RoboSintegra roboSintegra, ROBO robo)
        {
            if (roboSintegra != null)
            {
                if (roboSintegra.Code == 1)
                {
                    #region Parse dos Dados do Robô

                    string asAsciiSintegra = string.Empty;
                    if (!string.IsNullOrEmpty(roboSintegra.HTML))
                    {
                        asAsciiSintegra = _metodosGerais.EncodeCodigoHtml(roboSintegra.HTML);
                    }

                    DateTime dataInclusao;

                    DateTime.TryParse(roboSintegra.Data.DataInclusao, out dataInclusao);

                    #endregion

                    robo.SINT_CODE_ROBO = roboSintegra.Code;
                    robo.SINT_CERTIFICADO_HTML = asAsciiSintegra.Replace("imagens/sintegra.gif", "http://www.fazenda.rj.gov.br/projetoCPS/imagens/sintegra.gif");
                    robo.SINT_CONSULTA_DTHR = DateTime.Now;
                    robo.SINT_CONTADOR_TENTATIVA = 0;
                    robo.SINT_IE_COD = roboSintegra.Data.InscricaoEstadual;
                    robo.SINT_IE_MULTIPLA = (roboSintegra.Data.MultiplasIE.HasValue && !roboSintegra.Data.MultiplasIE.Value) ? "Não" : "Sim";
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
                    int? contador = robo.SINT_CONTADOR_TENTATIVA;
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

        public RoboSintegra MontaRoboView(ROBO robo)
        {
            RoboSintegra roboSintegra = new RoboSintegra();
            roboSintegra.Data = new DataSintegra();

            if (robo != null)
            {
                roboSintegra.Code = robo.SINT_CODE_ROBO;
                roboSintegra.HTML = robo.SINT_CERTIFICADO_HTML;
                roboSintegra.DataConsulta =  robo.SINT_CONSULTA_DTHR;
                roboSintegra.Data.InscricaoEstadual = robo.SINT_IE_COD;
                roboSintegra.Data.MultiplasIE = robo.SINT_IE_MULTIPLA == "Sim" ? true : false;
                roboSintegra.Data.SituacaoCadastral = robo.SINT_IE_SITU_CADASTRAL;
                roboSintegra.Data.DataSituacaoCadastral = robo.SINT_IE_SITU_CADSTRAL_DT;
                roboSintegra.Data.DataInclusao = robo.SINT_INCLUSAO_DT.HasValue ? robo.SINT_INCLUSAO_DT.Value.ToString("dd/MM/yyyy") : null;
                roboSintegra.Data.Telefone = robo.SINT_TEL;
                roboSintegra.Data.AtividadeEconomicaPrincipal = robo.SINT_ATIVIDADE_PRINCIPAL;
                roboSintegra.Data.Bairro = robo.SINT_BAIRRO;
                roboSintegra.Data.CEP = robo.SINT_CEP;
                roboSintegra.Data.CNPJ = robo.CNPJ;
                roboSintegra.Data.Complemento = robo.SINT_COMPLEMENTO;
                roboSintegra.Data.EnquadramentoFiscal = robo.SINT_ENQUADRAMENTO_FISCAL;
                roboSintegra.Data.Logradouro = robo.SINT_LOGRADOURO;
                roboSintegra.Data.Numero = robo.SINT_NUMERO;
                roboSintegra.Data.Municipio = robo.SINT_MUNICIPIO;
                roboSintegra.Data.UF = robo.SINT_UF;
                roboSintegra.Data.RazaoSocial = robo.SINT_RAZAO_SOCIAL;                

                return roboSintegra;
            }

            return null;
        }

        private string Sintegra_fake(string path)
        {
            //string path = HttpContext.Current.Server.MapPath("~/");
            FileStream fs = File.OpenRead(path + "Sintegra-fake.json");
            StreamReader reader = new StreamReader(fs);
            string textResult = reader.ReadToEnd();
            return textResult;
        }

        public void MontaCssMessagem(RoboSintegra roboSintegra)
        {
            if (roboSintegra.Code == 1)
            {
                roboSintegra.cssCor = (roboSintegra.Data.SituacaoCadastral.ToUpper() == "HABILITADO ATIVO" || roboSintegra.Data.SituacaoCadastral.ToUpper() == "HABILITADO" ? "success" : "warning");
                roboSintegra.Data.Message = roboSintegra.Code + " - Consulta Realizada com sucesso. (Situação Cadastral: " + roboSintegra.Data.SituacaoCadastral + ") &nbsp;&nbsp;&nbsp;<i class='fa fa-arrow-circle-down'></i>";
            }
            else if (roboSintegra.Code == 2)
            {
                roboSintegra.cssCor = "success";
                roboSintegra.Data.Message = roboSintegra.Code + " - ISENTO";
            }
            else
            {
                roboSintegra.cssCor = "danger";
                roboSintegra.Data.Message = roboSintegra.Code + " - " + roboSintegra.Data.Message;
            }
        }
    }

    public class DataSintegra
    {
        [JsonProperty("data_situacao_cadastral")]
        public string DataSituacaoCadastral { get; set; }

        [JsonProperty("inscricao_estadual")]
        public string InscricaoEstadual { get; set; }

        [JsonProperty("contingencia")]
        public string Contingencia { get; set; }
        
        [JsonProperty("situacao_cadastral")]
        public string SituacaoCadastral { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("razao_social")]
        public string RazaoSocial { get; set; }

        //O Robô Não Retorna Esta Informação
        public string SituacaoEFD { get; set; }

        [JsonProperty("multiplas_ie")]
        public bool? MultiplasIE { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        //O Robô Não Retorna Esta Informação
        public string EmissaoNFEObrigatorio { get; set; }

        //O Robô Não Retorna Esta Informação
        public string PerfilEFD { get; set; }

        [JsonProperty("cnpj")]
        public string CNPJ { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("cep")]
        public string CEP { get; set; }

        [JsonProperty("municipio")]
        public string Municipio { get; set; }

        //O Robô Não Retorna Esta Informação
        public string CTE { get; set; }

        [JsonProperty("atividade_economica_principal")]
        public string AtividadeEconomicaPrincipal { get; set; }

        [JsonProperty("data_inclusao")]
        public string DataInclusao { get; set; }

        [JsonProperty("uf")]
        public string UF { get; set; }

        //O Robô Não Retorna Esta Informação
        public string Message { get; set; }

        //Novas Propriedades no Retorno do Robô

        [JsonProperty("enquadramento_fiscal")]         
        public string EnquadramentoFiscal { get; set; }

        /*[JsonProperty("data_consulta")]         
        public string DataConsulta { get; set; }

        [JsonProperty("inscricao_convertida_anterior")]         
        public string InscricaoConvertidaAnterior { get; set; }

        [JsonProperty("observacoes")]         
        public List<string> Observacoes  { get; set; }

        [JsonProperty("numero_consulta")]         
        public string NumeroConsulta  { get; set; }*/
    }
}