using System;
using System.Collections.Generic;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class DocumentoAnexado : ISelfValidation
    {
        public DocumentoAnexado()
        {
            Arquivos = new List<Arquivo>();
        }

        public int Id { get; set; }
        public int? IdDocumentoSolicitado { get; set; }
        public bool Reprovado { get; set; }
        public string MensagemReprovacao { get; set; }
        public DateTime? DataReprovacao { get; set; }
        public DateTime? DataUltimoDownload { get; set; }
        public virtual ICollection<Arquivo> Arquivos { get; set; }
        public virtual DocumentoSolicitacao DocumentosSolicitacao { get; set; }

        public bool EhValido
        {
            get
            {
                //var validacaoExterna = new SolicitacaoValidacao();
                //ValidationResult = validacaoExterna.Validar(this);
                return true;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }
}