using System.Collections.Generic;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class FORNECEDOR_ENDERECO
    {
        public FORNECEDOR_ENDERECO()
        {
            WFD_SOL_MOD_ENDERECO = new List<SOLICITACAO_MODIFICACAO_ENDERECO>();
        }

        public int ID { get; set; }
        public int TP_ENDERECO_ID { get; set; }
        public int CONTRATANTE_PJPF_ID { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string CEP { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string UF { get; set; }
        public string PAIS { get; set; }
        public string LATITUDE { get; set; }
        public string LONGITUDE { get; set; }
        public virtual TiposDeEstado T_UF { get; set; }
        public virtual WFD_CONTRATANTE_PJPF WFD_CONTRATANTE_PJPF { get; set; }
        public virtual TIPO_ENDERECO WFD_T_TP_ENDERECO { get; set; }
        public virtual ICollection<SOLICITACAO_MODIFICACAO_ENDERECO> WFD_SOL_MOD_ENDERECO { get; set; }
    }
}