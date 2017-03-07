using System;
using System.Collections.Generic;
using System.Web;

namespace WebForLink.Web.ViewModels
{
    public class SolicitacaoDocumentosVM
    {
        public int ID { get; set; }

        public string GrupoDocumento { get; set; }

        public string Documento { get; set; }

        public bool? PorValidade { get; set; }

        public int? Periodicidade { get; set; }

        public string DescricaoPeriodicidade { get; set; }

        public bool Obrigatorio { get; set; }
        
        public DateTime? DataValidade { get; set; }

        public DateTime? DataUpload { get; set; }

        public int? ArquivoID { get; set; }
        
        public HttpPostedFileBase Arquivo { get; set; }

        public string NomeArquivo { get; set; }

        public int? SituacaoID { get; set; }

        public string Situacao { get; set; }

        public string Observacao { get; set; }

        public int? ListaDocumentosID { get; set; }

        public int? SolicitacaoID { get; set; }

        public List<VersaoVM> ListaVersao { get; set; }

        public int DescricaoDocumentoId { get; set; }

        public int DescricaoDocumentoId_CH { get; set; }

        public bool UsadoEmOutroContratante { get; set; }

        public bool AtualizarDocOutrosContratantes { get; set; }

        public int SolicitacaoDocumentosId { get; set; }
        public string ArquivoSubidoOriginal { get; set; }
        public string ArquivoSubido { get; set; }
        public string TipoArquivoSubido { get; set; }
    }
    public class VersaoVM
    {
        public int ID { get; set; }

        public string Nome { get; set; }
    }
}