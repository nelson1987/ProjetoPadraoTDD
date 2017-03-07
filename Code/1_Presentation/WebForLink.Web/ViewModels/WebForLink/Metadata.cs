using System;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    public partial class WFD_MEUS_DOCUMENTOSMetadata
    {
        public int ID { get; set; }

        public int CONTRATANTE_ID { get; set; }
        
        public int DESCRICAO_DOCUMENTOS_ID { get; set; }

        [Display(Name = "Nome do Arquivo")]
        public string NOME_ARQUIVO { get; set; }

        [Display(Name = "Tamanho do Arquivo")]
        public Nullable<decimal> TAMANHO_ARQUIVO { get; set; }

        [Display(Name = "Extensão do Arquivo")]
        public string EXTENSAO_ARQUIVO { get; set; }

        [Display(Name = "Data Upload")]
        public Nullable<System.DateTime> DATA_UPLOAD { get; set; }

        [Display(Name = "Data Validade")]
        public Nullable<System.DateTime> DATA_VALIDADE { get; set; }
        
        [Display(Name = "Data Emissão")]
        public Nullable<System.DateTime> DATA_EMISSAO { get; set; }

        [Display(Name = "Ativo")]
        public Nullable<bool> ATIVO { get; set; }
    }

    public partial class WFD_TIPO_DOCUMENTOSMetadata
    {
        public int ID { get; set; }
        
        [Display(Name = "Descrição")]
        public string DESCRICAO { get; set; }

        public Nullable<int> CONTRATANTE_ID { get; set; }
        
        public Nullable<int> TIPO_DOCUMENTOS_CH_ID { get; set; }

        [Display(Name = "Ativo")]
        public bool ATIVO { get; set; }
    }

    public partial class WFD_DESCRICAO_DOCUMENTOSMetadata
    {
        public int ID { get; set; }
        
        public int TIPO_DOCUMENTOS_ID { get; set; }

        [Display(Name = "Descrição")]
        public string DESCRICAO { get; set; }
        
        public Nullable<int> CONTRATANTE_ID { get; set; }
        
        public Nullable<int> DESCRICAO_DOCUMENTOS_CH_ID { get; set; }

        [Display(Name = "Ativo")]
        public bool ATIVO { get; set; }
    }
}