using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using WebForDocs.Dominio.Models;

namespace WebForDocs.Biblioteca
{
    public class RoboReceitaCPF
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int? Code { get; set; }
        public string HTML { get; set; }
        [JsonProperty("data")]
        public DataCPF Data { get; set; }
        public string UUID { get; set; }
        public int? SolicitacaoID { get; set; }
        public DateTime? DataConsulta { get; set; }
        public string cssCor { get; set; }

        public RoboReceitaCPF CarregaRoboCPF(string cpf, string DataNasc, string path)
        {
            RoboReceitaCPF roboCPF = new RoboReceitaCPF();

            try
            {
                if (!Validacao.ValidaCPF(cpf))
                {
                    roboCPF.Code = 0;
                    roboCPF.Data = new DataCPF();
                    roboCPF.Data.Message = "CPF Inválido!";
                    return roboCPF;
                }

                var ativarConsultasFake = Convert.ToBoolean(ConfigurationManager.AppSettings["AtivarConsultasFake"]);
                string textResult = string.Empty;

                if (ativarConsultasFake)
                    textResult = ReceitaFederalCPF_fake(path);
                else
                {
                    string token = ConfigurationManager.AppSettings["SintegraToken"];
                    string cont = "7";
                    string url = String.Format("https://webservice.keyconsultas.net/receita/cpf/?cpf={0}&nascimento={1}&token={2}&cont={3}", cpf, DataNasc, token, cont);

                    var request = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    Stream stream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(stream);
                    textResult = reader.ReadToEnd();
                }                

                roboCPF = JsonConvert.DeserializeObject<RoboReceitaCPF>(textResult);
            }
            catch {

                roboCPF.Code = 0;
                roboCPF.Data = new DataCPF();
                roboCPF.Data.Message = "Não foi possível acessar o serviço de consulta dos Orgãos Públicos! Tente novamente.";
                return roboCPF;
            }

            return roboCPF;
        }

        public void GravaRoboCpf(RoboReceitaCPF roboReceitaCpf, ref WFD_PJPF_ROBO robo)
        {
            try
            {
                MontaRobo(roboReceitaCpf, robo);
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error ao gravar os dados da Receita Federal", robo.ID), ex);
            }
        }

        private void MontaRobo(RoboReceitaCPF roboReceitaCpf, WFD_PJPF_ROBO robo)
        {
            if (roboReceitaCpf != null)
            {
                if (roboReceitaCpf.Code == 1)
                {
                    #region Parse de dados do Robo

                    string roboHtmlFormatado = roboReceitaCpf.HTML;

                    #endregion
                    robo.RF_CODE_ROBO = roboReceitaCpf.Code;
                    robo.CPF = roboReceitaCpf.Data.CPF;
                    robo.RF_NOME = roboReceitaCpf.Data.Nome;
                    robo.RF_CERTIFICADO_HTML = roboHtmlFormatado;
                    robo.ROBO_DT_EXEC = DateTime.Now;
                    robo.RF_SIT_CADASTRAL_CNPJ = roboReceitaCpf.Data.SituacaoCadastral;
                    robo.RF_CONSULTA_DTHR = DateTime.Now;
                    robo.SINT_CONSULTA_DTHR = DateTime.MinValue;
                    robo.SN_CONSULTA_DTHR = DateTime.MinValue;
                }
                else
                {
                    int? contador = robo.RF_CONTADOR_TENTATIVA;
                    if (!contador.HasValue) contador = 0;

                    if (roboReceitaCpf.Code == 2 || roboReceitaCpf.Code == 3)
                    {
                        robo.ROBO_DT_EXEC = DateTime.Now;
                        robo.RF_CODE_ROBO = roboReceitaCpf.Code;
                        robo.RF_SIT_CADASTRAL_CNPJ = roboReceitaCpf.Data.Message;
                        robo.RF_CONTADOR_TENTATIVA = 0;
                        robo.RF_CONSULTA_DTHR = DateTime.Now;
                        robo.SINT_CONSULTA_DTHR = DateTime.MinValue;
                        robo.SN_CONSULTA_DTHR = DateTime.MinValue;
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

        public RoboReceitaCPF MontaRoboView(WFD_PJPF_ROBO robo)
        {
            RoboReceitaCPF roboReceitaCpf = new RoboReceitaCPF();
            roboReceitaCpf.Data = new DataCPF();

            if (robo != null)
            {
                roboReceitaCpf.Data.DataEmissao = robo.RF_CONSULTA_DTHR.HasValue ? robo.RF_CONSULTA_DTHR.Value.ToString("dd/MM/yyyy") : null;
                roboReceitaCpf.DataConsulta = robo.RF_CONSULTA_DTHR;
                roboReceitaCpf.Code = robo.RF_CODE_ROBO;
                roboReceitaCpf.Data.CPF = robo.CPF;
                roboReceitaCpf.Data.Nome = robo.RF_NOME;
                roboReceitaCpf.HTML = robo.RF_CERTIFICADO_HTML;                
                roboReceitaCpf.Data.SituacaoCadastral = robo.RF_SIT_CADASTRAL_CNPJ;

                return roboReceitaCpf;
            }

            return null;
        }

        private string ReceitaFederalCPF_fake(string path)
        {
            //string path = HttpContext.Current.Server.MapPath("~/");
            FileStream fs = File.OpenRead(path + "ReceitaFederalCPF-fake.json");
            StreamReader reader = new StreamReader(fs);
            string textResult = reader.ReadToEnd();

            return textResult;
        }

        public void MontaCssMessagem(RoboReceitaCPF roboReceitaCpf)
        {
            if (roboReceitaCpf.Code == 1)
            {
                roboReceitaCpf.cssCor = "success";
                roboReceitaCpf.Data.Message = roboReceitaCpf.Code + " - Consulta Realizada com sucesso. (Situação Cadastral: " + roboReceitaCpf.Data.SituacaoCadastral + ") &nbsp;&nbsp;&nbsp;<i class='fa fa-arrow-circle-down'></i>";
            }
            else if (roboReceitaCpf.Code == 2)
            {
                roboReceitaCpf.cssCor = "warning";
                roboReceitaCpf.Data.Message = roboReceitaCpf.Code + " - " + roboReceitaCpf.Data.Message;
            }
            else
            {
                roboReceitaCpf.cssCor = "danger";
                roboReceitaCpf.Data.Message = roboReceitaCpf.Code + " - " + roboReceitaCpf.Data.Message;
            }
        }
    }

    public class DataCPF
    {
        [JsonProperty("situacao_cadastral")]
        public string SituacaoCadastral { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("hora_emissao")]
        public string HoraEmissao { get; set; }

        [JsonProperty("cpf")]
        public string CPF { get; set; }

        [JsonProperty("contingencia")]
        public string Contingencia { get; set; }

        [JsonProperty("digito_verificador")]
        public string DigitoVerificador { get; set; }

        [JsonProperty("data_emissao")]
        public string DataEmissao { get; set; }

        [JsonProperty("comprovante")]
        public string Comprovante { get; set; }

        //O Robô Não Retorna Esta Informação
        public string Message { get; set; }

        //Novas Propriedades no Retorno do Robô

        //[JsonProperty("data_inscricao")]
        //public string DataInscricao { get; set; }

        //[JsonProperty("data_nascimento")]
        //public string DataNascimento { get; set; }

        //[JsonProperty("ano_obito")]
        //public string AnoObito { get; set; }      
    }
}