using System;

namespace WebForLink.Domain.Entities.WebForLink
{
    public class DDT_COLUNAS
    {
        public int ID { get; set; }
        public int TABELAS_ID { get; set; }
        public string COLUNA { get; set; }
        public DateTime MODIFY_DATE { get; set; }
        public string COLUNA_DSC { get; set; }
    }
}