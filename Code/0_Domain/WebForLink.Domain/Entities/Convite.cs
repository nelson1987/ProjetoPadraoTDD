using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities.Validations;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities
{
    public class Convite : ISelfValidation
    {
        public Convite(FornecedorVendorlist fornecedor, ClienteWebFormat cliente, List<Documento> documentos)
        {
            Fornecedor = fornecedor;
            Cliente = cliente;
            Documentos = documentos;
            Validar();
        }
        public Convite(FornecedorVendorlist fornecedor, ClienteWebFormat cliente)
            : this(fornecedor, cliente, new List<Documento>())
        {

        }
        public FornecedorVendorlist Fornecedor { get; set; }
        public ClienteWebFormat Cliente { get; set; }
        public List<Documento> Documentos { get; set; }


        public bool EhValido
        {
            get
            {
                var validacaoExterna = new ConviteValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }

        public void Validar()
        {
            if (Fornecedor == null)
                throw new ArgumentNullException("Fornecedor não pode ser Nulo");
            if (Cliente == null)
                throw new ArgumentNullException("Cliente não pode ser Nulo");
        }
        public void AdicionarDocumento(Documento documento)
        {
            Documentos.Add(documento);
        }
    }

}
