//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
//using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
//using WebForLink.Domain.Infrastructure;
//using WebForLink.Domain.Models;
//using WebForLink.Web.Interfaces;

namespace WebForLink.Web.Infrastructure
{
    public class RoboReceitaCNPJ
    {
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //private static readonly IGeral _metodosGerais = new Geral();
        public int? Code { get; set; }
        public string HTML { get; set; }
        [JsonProperty("data")]
        public Data Data { get; set; }
        public string UUID { get; set; }
        public int tpPapel { get; set; }
        public int?[] ProximosPapeis { get; set; }
        public DateTime? DataConsulta { get; set; }
        public string cssCor { get; set; }

        public RoboReceitaCNPJ CarregaRoboCNPJ(string cnpj, string path)
        {
            RoboReceitaCNPJ robo = new RoboReceitaCNPJ();

            try
            {
                if (!Validacao.ValidaCNPJ(cnpj))
                {
                    robo.Code = 0;
                    robo.Data = new Data();
                    robo.Data.Message = "CNPJ Inválido!";
                    return robo;
                }

                var ativarConsultasFake = Convert.ToBoolean(ConfigurationManager.AppSettings["AtivarConsultasFake"]);
                string textResult = string.Empty;
                
                if (ativarConsultasFake)
                    textResult = ReceitaFederalCNPJ_fake(path);
                else
                {
                    string token = ConfigurationManager.AppSettings["SintegraToken"];
                    string cont = "7";
                    string url = String.Format("https://webservice.keyconsultas.net/receita/cnpj/?cnpj={0}&token={1}&cont={2}", cnpj, token, cont);
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    textResult = reader.ReadToEnd();
                }

                robo = JsonConvert.DeserializeObject<RoboReceitaCNPJ>(textResult);
                
                return robo;
            }
            catch
            {
                robo.Code = 0;
                robo.Data = new Data();
                robo.Data.Message = "Não foi possível acessar o serviço de consulta dos Orgãos Públicos! Tente novamente.";
                return robo;
            }
        }

        public void GravaRoboReceita(RoboReceitaCNPJ roboReceita, ref ROBO robo)
        {
            try
            {
                MontaRobo(roboReceita, robo);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error ao gravar os dados da Receita Federal", robo.ID), ex);
            }

        }

        private void MontaRobo(RoboReceitaCNPJ roboReceita, ROBO robo)
        {
            if (roboReceita != null)
            {
                if (roboReceita.Code == 1)
                {
                    #region Parse de Dados do Robo

                    string roboHtmlFormatado = roboReceita.HTML;
                    string asAscii = _metodosGerais.EncodeCodigoHtml(roboHtmlFormatado);
                    DateTime dataSituacaoCadastral;
                    DateTime dataSituacaocadastralEspecial;

                    DateTime.TryParse(roboReceita.Data.DataSituacaoCadastral, out dataSituacaoCadastral);
                    DateTime.TryParse(roboReceita.Data.DataSituacaoEspecial, out dataSituacaocadastralEspecial);

                    #endregion

                    robo.ROBO_DT_EXEC = DateTime.Now;
                    robo.RF_CODE_ROBO = roboReceita.Code;
                    robo.CNPJ = roboReceita.Data.CNPJ;
                    robo.RECEITA_FEDERAL_RAZAO_SOCIAL = roboReceita.Data.RazaoSocial;
                    robo.RF_BAIRRO = roboReceita.Data.Bairro;
                    robo.RF_CEP = roboReceita.Data.CEP;
                    robo.RF_CERTIFICADO_HTML = asAscii.Replace("images/brasao2.gif", "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/images/brasao2.gif");
                    robo.RF_CNAE_COD_PRINCIPAL = (roboReceita.Data.AtividadeEconomicaPrincipal.Length >= 10) ? roboReceita.Data.AtividadeEconomicaPrincipal.Substring(0, 10).Replace(".", "").Replace("-", "") : roboReceita.Data.AtividadeEconomicaPrincipal;
                    robo.RF_CNAE_DSC_PRINCIPAL = roboReceita.Data.AtividadeEconomicaPrincipal.Split(new string[] { " - " }, StringSplitOptions.None).Last().ToString();
                    robo.RF_CNPJ_DT_ABERTURA = DateTime.Parse(roboReceita.Data.DataAbertura);
                    robo.RF_COD_NATUREZA_JURIDICA = roboReceita.Data.NaturezaJuridica == "" ? "" : roboReceita.Data.NaturezaJuridica.Substring(0, 6).Replace("-", "").TrimEnd();
                    robo.RF_COMPLEMENTO = roboReceita.Data.Complemento;
                    robo.RF_CONSULTA_DTHR = DateTime.Now;
                    robo.RF_CONTADOR_TENTATIVA = 0;
                    robo.RF_DSC_NATUREZA_JURIDICA = roboReceita.Data.NaturezaJuridica.Split(new string[] { " - " }, StringSplitOptions.None).Last().ToString();
                    robo.RF_LOGRADOURO = roboReceita.Data.Logradouro;
                    robo.RF_MATRIZ_FILIAL = roboReceita.Data.MatrizFilial;
                    robo.RF_MOTIVO_CNPJ_SITU_CADASTRAL = roboReceita.Data.MotivoSituacaoCadastral;
                    robo.RF_MUNICIPIO = roboReceita.Data.Municipio;
                    robo.RF_NOME_FANTASIA = roboReceita.Data.NomeFantasia;
                    robo.RF_NUMERO = roboReceita.Data.Numero;
                    robo.RF_SIT_CADASTRAL_CNPJ = roboReceita.Data.SituacaoCadastral;
                    robo.RF_SIT_CADSTRAL_CNPJ_DT = dataSituacaoCadastral;
                    robo.RF_SIT_ESPECIAL_CNPJ = roboReceita.Data.SituacaoEspecial;
                    robo.RF_SIT_ESPECIAL_CNPJ_DT = dataSituacaocadastralEspecial;
                    robo.RF_UF = roboReceita.Data.UF;
                }
                else
                {
                    int? contador = robo.RF_CONTADOR_TENTATIVA;
                    if (!contador.HasValue) contador = 0;

                    if (roboReceita.Code == 2 || roboReceita.Code == 3)
                    {
                        robo.ROBO_DT_EXEC = DateTime.Now;
                        robo.RF_CODE_ROBO = roboReceita.Code;
                        robo.RF_SIT_CADASTRAL_CNPJ = roboReceita.Data.Message;
                        robo.RF_CONSULTA_DTHR = DateTime.Now;
                        robo.RF_CONTADOR_TENTATIVA = 0;
                    }
                    else
                    {
                        contador += 1;
                        robo.ROBO_DT_EXEC = DateTime.Now;
                        robo.RF_CONTADOR_TENTATIVA = contador;
                    }
                }
            }
        }

        public RoboReceitaCNPJ MontaRoboView(ROBO robo)
        {
            RoboReceitaCNPJ roboReceita = new RoboReceitaCNPJ();
            roboReceita.Data = new Data();

            if (robo != null)
            {
                roboReceita.Data.DataEmissao = robo.RF_CONSULTA_DTHR.HasValue ? robo.RF_CONSULTA_DTHR.Value.ToString("dd/MM/yyyy HH:mm:ss") : null;
                roboReceita.DataConsulta = robo.RF_CONSULTA_DTHR;
                roboReceita.Code = robo.RF_CODE_ROBO;
                roboReceita.Data.CNPJ = robo.CNPJ;
                roboReceita.Data.RazaoSocial = robo.RECEITA_FEDERAL_RAZAO_SOCIAL;
                roboReceita.Data.Bairro = robo.RF_BAIRRO;
                roboReceita.Data.CEP = robo.RF_CEP;
                roboReceita.HTML = robo.RF_CERTIFICADO_HTML;
                roboReceita.Data.AtividadeEconomicaPrincipal = robo.RF_CNAE_COD_PRINCIPAL + " - " + robo.RF_CNAE_DSC_PRINCIPAL;
                roboReceita.Data.DataAbertura = robo.RF_CNPJ_DT_ABERTURA.HasValue ? robo.RF_CNPJ_DT_ABERTURA.Value.ToString("dd/MM/yyyy"): null;
                roboReceita.Data.NaturezaJuridica = robo.RF_COD_NATUREZA_JURIDICA + " - " + robo.RF_DSC_NATUREZA_JURIDICA;

                roboReceita.Data.Complemento = robo.RF_COMPLEMENTO;
                roboReceita.Data.Logradouro = robo.RF_LOGRADOURO;
                roboReceita.Data.MatrizFilial = robo.RF_MATRIZ_FILIAL;
                roboReceita.Data.MotivoSituacaoCadastral = robo.RF_MOTIVO_CNPJ_SITU_CADASTRAL;
                roboReceita.Data.Municipio = robo.RF_MUNICIPIO;
                roboReceita.Data.NomeFantasia = robo.RF_NOME_FANTASIA;
                roboReceita.Data.Numero = robo.RF_NUMERO;
                roboReceita.Data.SituacaoCadastral= robo.RF_SIT_CADASTRAL_CNPJ;
                roboReceita.Data.DataSituacaoCadastral = robo.RF_SIT_CADSTRAL_CNPJ_DT.HasValue ? robo.RF_SIT_CADSTRAL_CNPJ_DT.Value.ToString("dd/MM/yyyy") : null;
                roboReceita.Data.SituacaoEspecial = robo.RF_SIT_ESPECIAL_CNPJ;
                roboReceita.Data.DataSituacaoEspecial = robo.RF_SIT_ESPECIAL_CNPJ_DT.HasValue ? robo.RF_SIT_ESPECIAL_CNPJ_DT.Value.ToString("dd/MM/yyyy") : null;
                roboReceita.Data.UF = robo.RF_UF;

                return roboReceita;
            }

            return null;
        }

        private string ReceitaFederalCNPJ_fake(string path)
        {        
            //string path = HttpContext.Current.Server.MapPath("~/");            
            FileStream fs = File.OpenRead(path + "ReceitaFederalCNPJ-fake.json");
            StreamReader reader = new StreamReader(fs);
            string textResult = reader.ReadToEnd();
            return textResult;
        }

        public void MontaCssMessagem(RoboReceitaCNPJ roboReceita)
        {
            if (roboReceita.Code == 1)
            {
                roboReceita.cssCor = (roboReceita.Data.SituacaoCadastral.ToUpper() == "ATIVA" ? "success" : "danger");
                roboReceita.Data.Message = roboReceita.Code + " - Consulta Realizada com sucesso. (Situação Cadastral: " + roboReceita.Data.SituacaoCadastral + ") &nbsp;&nbsp;&nbsp;<i class='fa fa-arrow-circle-down'></i>";
            }
            else if (roboReceita.Code == 2)
            {
                roboReceita.cssCor = "warning";
                roboReceita.Data.Message = roboReceita.Code + " - " + roboReceita.Data.Message;
            }
            else
            {
                roboReceita.cssCor = "danger";
                roboReceita.Data.Message = roboReceita.Code + " - " + roboReceita.Data.Message;
            }
        }

    }

    public class Data
    {
        [JsonProperty("data_situacao_cadastral")]
        public string DataSituacaoCadastral { get; set; }

        [JsonProperty("hora_emissao")]
        public string HoraEmissao { get; set; }

        ////Nova Propriedade no Retorno do Robô
        //[JsonProperty("municipio_ibge")]
        //public string Contingencia { get; set; }

        [JsonProperty("municipio_ibge")]
        public string MunicipioIBGE { get; set; }

        [JsonProperty("codigo_ibge")]
        public int CodigoIBGE { get; set; }

        [JsonProperty("outras_atividades")]
        public List<string> OutrasAtividades { get; set; }

        [JsonProperty("situacao_cadastral")]
        public string SituacaoCadastral { get; set; }

        [JsonProperty("data_abertura")]
        public string DataAbertura { get; set; }

        [JsonProperty("razao_social")]
        public string RazaoSocial { get; set; }

        //O Robô Não Retorna Esta Informação
        public string RazaoSocialAbreviado { get; set; } //Fazer com os demais campos abreviados

        [JsonProperty("ente_federativo_responsavel")]
        public string EnteFederativoResponsavel { get; set; }

        [JsonProperty("telefone")]
        public string Telefone { get; set; }

        [JsonProperty("cnpj")]
        public string CNPJ { get; set; }

        [JsonProperty("endereco_eletronico")]
        public string EnderecoEletronico { get; set; }

        [JsonProperty("obs_ibge")]
        public string ObservacaoIBGE { get; set; }

        [JsonProperty("bairro")]
        public string Bairro { get; set; }

        [JsonProperty("matriz_filial")]
        public string MatrizFilial { get; set; }

        [JsonProperty("numero")]
        public string Numero { get; set; }

        [JsonProperty("situacao_especial")]
        public string SituacaoEspecial { get; set; }

        [JsonProperty("cep")]
        public string CEP { get; set; }

        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        [JsonProperty("motivo_situacao_cadastral")]
        public string MotivoSituacaoCadastral { get; set; }

        [JsonProperty("municipio")]
        public string Municipio { get; set; }

        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }

        [JsonProperty("nome_fantasia")]
        public string NomeFantasia { get; set; }

        [JsonProperty("atividade_economica_principal")]
        public string AtividadeEconomicaPrincipal { get; set; }

        [JsonProperty("natureza_juridica")]
        public string NaturezaJuridica { get; set; }

        [JsonProperty("data_emissao")]
        public string DataEmissao { get; set; }

        [JsonProperty("data_situacao_especial")]
        public string DataSituacaoEspecial { get; set; }

        [JsonProperty("uf")]
        public string UF { get; set; }

        //O Robô Não Retorna Esta Informação
        public string Message { get; set; }
    }
}