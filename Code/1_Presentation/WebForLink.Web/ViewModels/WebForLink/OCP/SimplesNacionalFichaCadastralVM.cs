using System.ComponentModel;

namespace WebForLink.Web.ViewModels.OCP
{
    public class SimplesNacionalFichaCadastralVM
    {
        [DisplayName("Código")]
        public string Codigo { get; internal set; }
        [DisplayName("Situação")]
        public string SituacaoSimplesNacional { get; internal set; }

        [DisplayName("Data da consulta")]
        public string DataConsulta { get; internal set; }

        [DisplayName("Períodos Anteriores")]
        public string PeriodosAnteriores { get; internal set; }

        [DisplayName("SIMEI - Períodos Anteriores")]
        public string SIMEIPeriodosAnteriores { get; internal set; }

        [DisplayName("Situação SIMEI")]
        public string SituacaoSIMEI { get; internal set; }

        [DisplayName("Razão Social")]
        public string RazaoSocial { get; internal set; }
    }
}