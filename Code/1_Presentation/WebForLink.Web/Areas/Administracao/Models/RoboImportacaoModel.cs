using System.ComponentModel;

namespace WebForLink.Web.Areas.Administracao.Models
{
    public class RoboImportacaoModel : ViewModelPadrao
    {
        [DisplayName("Última Execução")]
        public string UltimaExecucao { get; set; }

        [DisplayName("Próxima Execução")]
        public string ProximaExecucao { get; set; }

        public string DiasExecucao { get; set; }

        [DisplayName("Intervalo Programado")]
        public string TempoExecucao { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        public bool Ativo { get; set; }

        public bool Carregado { get; set; }
    }
}
