using System;

namespace WebForLink.Domain.Robos
{
    [Serializable]
    public class SimplesNacional
    {
        public int? Code { get; set; }
        public string HTML { get; set; }
        public string UUID { get; set; }
        public int tpPapel { get; set; }
        public DateTime? DataConsulta { get; set; }
        public string cssCor { get; set; }
        public string SituacaoSimplesNacional { get; set; }
        public string SimplesNacionalPeriodosAnteriores { get; set; }
        public string SIMEIPeriodosAnteriores { get; set; }
        public string SituacaoSIMEI { get; set; }
        public string RazaoSocial { get; set; }
        public string Message { get; set; }
    }
}