using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class DocumentoSolicitacao : ISelfValidation
    {
        private DocumentoSolicitacao()
        {
            DocumentoAnexados = new List<DocumentoAnexado>();
        }

        public DocumentoSolicitacao(string descricao) : this()
        {
            DescricaoDocumento = descricao;
            Validar();
        }

        public DocumentoSolicitacao(int tipoCh, string descricao) : this()
        {
            IdTipoDocumentoCH = tipoCh;
            DescricaoDocumento = descricao;
            Validar();
        }

        public int Id { get; set; }
        public string DescricaoDocumento { get; set; }
        public int IdSolicitacao { get; set; }
        public int? IdTipoDocumentoCH { get; set; }
        public string DescricaoDocumentoCH { get; set; }
        public virtual Solicitacao Solicitacao { get; set; }
        public virtual List<DocumentoAnexado> DocumentoAnexados { get; set; }

        public bool ValidaArquivos
        {
            get
            {
                var retorno = true;
                if (!DocumentoAnexados.Any())
                    retorno = false;
                foreach (var documento in DocumentoAnexados)
                {
                    if (!documento.Arquivos.Any())
                        retorno = false;
                }
                return retorno;
            }
        }

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

        public void Validar()
        {
        }
    }
}