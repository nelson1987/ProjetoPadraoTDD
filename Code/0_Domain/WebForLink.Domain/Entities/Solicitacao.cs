using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.Validations;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Solicitacao : ISelfValidation
    {
        public Solicitacao()
        {
            DocumentoSolicitacao = new List<DocumentoSolicitacao>();
            FichaCadastral = new FichaCadastral();
            //DocumentoAnexados = new List<DocumentoAnexado>();
        }

        public Solicitacao(Solicitante solicitante, Solicitado solicitado)
            : this()
        {
            IdSolicitante = solicitante.Id;
            IdSolicitado = solicitado.Id;
        }

        public Solicitacao(Solicitante solicitante, Solicitado solicitado, DocumentoSolicitacao documentoSolicitado)
            : this(solicitante, solicitado)
        {
            DocumentoSolicitacao.Add(documentoSolicitado);
        }

        public int Id { get; set; }
        public int IdSolicitante { get; set; }
        public int IdSolicitado { get; set; }
        public int? IdFichaCadastral { get; set; }
        public int? StatusSolicitacao { get; set; }
        //private int myVar;

        public int FichaCadastralId
        {
            get { return (int) IdFichaCadastral != 0 ? (int) IdFichaCadastral : FichaCadastral.Id; }
        }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataReenvio { get; set; }
        public DateTime? DataCancelamento { get; set; }
        public DateTime? DataVisualizado { get; set; }
        public bool? FichaCadastralObrigatoria { get; set; }
        public virtual ICollection<DocumentoSolicitacao> DocumentoSolicitacao { get; set; }
        public virtual FichaCadastral FichaCadastral { get; set; }
        public virtual Solicitado Solicitado { get; set; }
        public virtual Solicitante Solicitante { get; set; }

        public bool EhEnviada
        {
            get { return StatusSolicitacao == (int) StatusSolicitacaoEnum.Enviado; }
        }

        public bool EhVisualizada
        {
            get { return StatusSolicitacao == (int) StatusSolicitacaoEnum.Visualizado; }
        }

        public bool EhRespondida
        {
            get { return StatusSolicitacao == (int) StatusSolicitacaoEnum.Respondido; }
        }

        public bool EhCancelada
        {
            get { return StatusSolicitacao == (int) StatusSolicitacaoEnum.Cancelado; }
        }

        public bool EhExpirada
        {
            get { return StatusSolicitacao == (int) StatusSolicitacaoEnum.Expirado; }
        }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new SolicitacaoValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }

        public void MudarStatusParaVisualizado()
        {
            if (EhEnviada)
            {
                StatusSolicitacao = (int) StatusSolicitacaoEnum.Visualizado;
                DataVisualizado = DateTime.Now;
                //return true;
            }
            else if (!EhVisualizada)
                throw new StatusSolicitacaoException("A solicitação já foi visualizada.");
        }

        public void MudarStatusParaCancelado()
        {
            if (!(EhEnviada || EhVisualizada))
                throw new StatusSolicitacaoException("A solicitação já foi cancelada.");

            StatusSolicitacao = (int) StatusSolicitacaoEnum.Cancelado;
            DataCancelamento = DateTime.Now;
        }

        public void MudarStatusParaExpirado()
        {
            if (!(EhEnviada || EhVisualizada))
                throw new StatusSolicitacaoException("A solicitação já foi expirada.");

            StatusSolicitacao = (int) StatusSolicitacaoEnum.Expirado;
        }

        public void MudarStatusParaRespondido()
        {
            if (!EhVisualizada)
                throw new StatusSolicitacaoException("A solicitação já foi respondida.");

            StatusSolicitacao = (int) StatusSolicitacaoEnum.Respondido;
        }

        public void IncluirDocumentoSolicitado(DocumentoSolicitacao documento)
        {
            DocumentoSolicitacao.Add(documento);
        }

        public void IncluirFichaCadastral(FichaCadastral fichaCadastral)
        {
            FichaCadastral = fichaCadastral;
        }

        public void IncluirSolicitante(Solicitante cli)
        {
            Solicitante = cli;
        }

        public void IncluirSolicitado(Solicitado forn)
        {
            Solicitado = forn;
        }
    }
}