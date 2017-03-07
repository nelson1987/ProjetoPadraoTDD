using System;

namespace WebForLink.Web.ViewModels
{
    public class DocumentosPJPFVM
    {
        public int ID { get; set; }
        public int ContratanteID { get; set; }
        public int ContratantePJPFID { get; set; }
        public int PJPFID { get; set; }
        public int DescricaoDocumentoID { get; set; }
        public string DescricaoDocumento { get; set; }
        public DateTime? DataValidade { get; set; }
        public DateTime? DataUpload { get; set; }
        public int ArquivoID { get; set; }
        public string NomeArquivo { get; set; }
        public bool? ExigeValidade { get; set; }
        public int? PeriodicidadeID { get; set; }
        public bool? Obrigatorio { get; set; }
    }
}