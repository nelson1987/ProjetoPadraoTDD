namespace WebForLink.Domain.Entities.Tipos
{
    public class RoboSimplesNacional : Robo
    {
        public RoboSimplesNacional(string razaoSocial)
            : base(razaoSocial)
        {
        }
        public string SN_SITUACAO_SIMEI { get; private set; }
        public string SN_PERIODOS_ANTERIORES { get; private set; }
        public string SN_SIMEI_PERIODOS_ANTERIORES { get; private set; }
        public string SN_AGENDAMENTOS { get; private set; }
        public string SN_RAZAO_SOCIAL { get; private set; }
        public string SN_CONSULTA_DTHR { get; private set; }
        public string SN_CONTADOR_TENTATIVA { get; private set; }
        public string SN_CODE_ROBO { get; private set; }
    }
}