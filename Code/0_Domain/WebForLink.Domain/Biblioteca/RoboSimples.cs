using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using WebForDocs.Dominio.Models;
using WebForDocs.ViewModels;

namespace WebForDocs.Biblioteca
{
    public class RoboSimples
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public int? Code { get; set; }
        public string HTML { get; set; }
        [JsonProperty("data")]
        public DataSimples Data { get; set; }
        public string UUID { get; set; }
        public int tpPapel { get; set; }
        public DateTime? DataConsulta { get; set; }
        public string cssCor { get; set; }

        public RoboSimples CarregaSimplesCNPJ(string cnpj, string path)
        {
            RoboSimples roboSimples = new RoboSimples();

            try
            {
                var ativarConsultasFake = Convert.ToBoolean(ConfigurationManager.AppSettings["AtivarConsultasFake"]);
                string textResult = string.Empty;

                if (ativarConsultasFake)
                    textResult = Simples_fake(path);
                else 
                {
                    string token = ConfigurationManager.AppSettings["SintegraToken"];
                    string cont = "7";
                    string url = string.Format("http://webservice.keyconsultas.net/receita/simples/?cnpj={0}&token={1}&cont={2}", cnpj, token, cont);
                    var requestSimples = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse responseSimples = requestSimples.GetResponse() as HttpWebResponse;
                    Stream streamSimples = responseSimples.GetResponseStream();
                    StreamReader readerSimples = new StreamReader(streamSimples);
                    textResult = readerSimples.ReadToEnd();                
                }

                roboSimples = JsonConvert.DeserializeObject<RoboSimples>(textResult);

                return roboSimples;
            }
            catch
            {
                roboSimples.Code = 0;
                roboSimples.Data = new DataSimples();
                roboSimples.Data.Message = "Não foi possível acessar o serviço de consulta dos Orgãos Públicos! Tente novamente.";
                return roboSimples;
            }
        }

        public void GravaRoboSimples(RoboSimples roboSimples, ref WFD_PJPF_ROBO robo)
        {
            MontaRobo(roboSimples, robo);
        }

        private void MontaRobo(RoboSimples roboSimples, WFD_PJPF_ROBO robo)
        {
            if (roboSimples != null)
            {
                if (roboSimples.Code == 1)
                {
                    robo.SN_CODE_ROBO = roboSimples.Code;
                    robo.SIMPLES_NACIONAL_SITUACAO = (!string.IsNullOrEmpty(roboSimples.Data.SituacaoSimplesNacional)) ? roboSimples.Data.SituacaoSimplesNacional : string.Empty;
                    robo.SN_PERIODOS_ANTERIORES = roboSimples.Data.SimplesNacionalPeriodosAnteriores;
                    robo.SN_SIMEI_PERIODOS_ANTERIORES = roboSimples.Data.SIMEIPeriodosAnteriores;
                    robo.SN_SITUACAO_SIMEI = roboSimples.Data.SituacaoSIMEI;
                    robo.SN_RAZAO_SOCIAL = roboSimples.Data.RazaoSocial;
                    robo.SN_CONSULTA_DTHR = DateTime.Now;
                }
                else
                {
                    int? contador = robo.SN_CONTADOR_TENTATIVA;
                    if (!contador.HasValue) contador = 0;

                    if (roboSimples.Code == 2 || roboSimples.Code == 3)
                    {
                        robo.SN_CODE_ROBO = roboSimples.Code;
                        robo.SIMPLES_NACIONAL_SITUACAO = roboSimples.Data.Message;
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
        }

        public RoboSimples MontaRoboView(WFD_PJPF_ROBO robo)
        {
            RoboSimples roboSimples = new RoboSimples();
            roboSimples.Data = new DataSimples(); 

            if (robo != null)
            {
                roboSimples.DataConsulta = robo.SN_CONSULTA_DTHR;
                roboSimples.Code = robo.SN_CODE_ROBO;
                roboSimples.Data.SituacaoSimplesNacional = robo.SIMPLES_NACIONAL_SITUACAO;
                roboSimples.Data.SimplesNacionalPeriodosAnteriores = robo.SN_PERIODOS_ANTERIORES;
                roboSimples.Data.SIMEIPeriodosAnteriores = robo.SN_SIMEI_PERIODOS_ANTERIORES;
                roboSimples.Data.SituacaoSIMEI = robo.SN_SITUACAO_SIMEI;
                roboSimples.Data.RazaoSocial = robo.SN_RAZAO_SOCIAL;

                return roboSimples;
            }

            return null;
        }

        private string Simples_fake(string path)
        {
            //string path = HttpContext.Current.Server.MapPath("~/");
            FileStream fs = File.OpenRead(path + "Simples-fake.json");
            StreamReader reader = new StreamReader(fs);
            string textResult = reader.ReadToEnd();
            return textResult;
        }

        public void MontaCssMessagem(RoboSimples roboSimples)
        {
            if (roboSimples.Code == null)
            {
                roboSimples.cssCor = "default";
                roboSimples.Data.Message = roboSimples.Code + " - Consulta Realizada com sucesso. (Situação Cadastral: " + roboSimples.Data.SituacaoSimplesNacional + ") &nbsp;&nbsp;&nbsp;<i class='fa fa-arrow-circle-down'></i>";
            }
            else if (roboSimples.Code == 1)
            {
                roboSimples.cssCor = "success";
                roboSimples.Data.Message = roboSimples.Code + " - Consulta Realizada com sucesso. (Situação Cadastral: " + roboSimples.Data.SituacaoSimplesNacional + ") &nbsp;&nbsp;&nbsp;<i class='fa fa-arrow-circle-down'></i>";
            }
            else if (roboSimples.Code == 2)
            {
                roboSimples.cssCor = "warning";
                roboSimples.Data.Message = roboSimples.Code + " - " + roboSimples.Data.Message;
            }
            else
            {
                roboSimples.cssCor = "danger";
                roboSimples.Data.Message = roboSimples.Code + " - " + roboSimples.Data.Message;
            }
        }
    }

    public class DataSimples
    {
        [JsonProperty("cnpj")]
        public string CNPJ { get; set; }

        [JsonProperty("eventos_futuros_simples_nacional")]
        public string EventosFuturosSimplesNacional { get; set; }

        [JsonProperty("simples_nacional_periodos_anteriores")]
        public string SimplesNacionalPeriodosAnteriores { get; set; }

        [JsonProperty("razao_social")]
        public string RazaoSocial { get; set; }

        [JsonProperty("simei_periodos_anteriores")]
        public string SIMEIPeriodosAnteriores { get; set; }

        [JsonProperty("situacao_simei")]
        public string SituacaoSIMEI { get; set; }

        [JsonProperty("situacao_simples_nacional")]
        public string SituacaoSimplesNacional { get; set; }

        //O Robô Não Retorna Esta Informação
        public string Message {get; set;}

        //Novas Propriedades no Retorno do Robô
        
        //[JsonProperty("contingencia")]
        //public string contingencia { get; set; }

        //[JsonProperty("agendamentos_simples_nacional")]
        //public string AgendamentosSimplesNacional { get; set; }        
    }
}