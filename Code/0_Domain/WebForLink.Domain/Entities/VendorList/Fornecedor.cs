using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.Validations;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Interfaces;
using WebForLink.Domain.Interfaces.Validation;
using WebForLink.Domain.Validation;

namespace WebForLink.Domain.Entities.VendorList
{
    public class ClienteVendorList : ISelfValidation
    {
        public ClienteVendorList(string codigo, string nomeSolicitante, string emailSolicitante)
        {
            Codigo = codigo;
            Nome = nomeSolicitante;
            Email = emailSolicitante;
        }

        public string Codigo { get; private set; }

        public string Email { get; set; }

        public string Nome { get; set; }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new ClienteValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }

    public class DocumentoSolicitado : IDocumento, ISelfValidation
    {
        public DocumentoSolicitado(string nome)
        {
            Nome = nome;
        }
        public string Nome { get; private set; }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new DocumentoSolicitadoValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }

    public class SolicitacaoDocumento : ISelfValidation, ISolicitacao
    {
        public SolicitacaoDocumento(ClienteVendorList cliente, FornecedorVendorList convidado, List<IDocumento> solicitados)
            : this(cliente, convidado)
        {
            Exigiveis = solicitados;
        }

        public SolicitacaoDocumento(ClienteVendorList cliente, FornecedorVendorList convidado)
        {
            Solicitado = convidado;
            Status = StatusSolicitacaoDocumento.AguardandoFornecedor;
            Validar();
        }

        public int Id { get; set; }

        public StatusSolicitacaoDocumento Status { get; private set; }

        public FornecedorVendorList Solicitado { get; set; }

        public ClienteVendorList Solicitante { get; set; }

        public List<IDocumento> Exigiveis { get; private set; }

        public void AdicionarDocumento(IDocumento documento)
        {
            Exigiveis.Add(documento);
        }

        public void AlterarStatus(StatusSolicitacaoDocumento novoStatus)
        {
            Status = novoStatus;
        }

        public void Validar()
        {
        }

        public bool EhValido
        {
            get
            {
                if (!Solicitado.EhValido && Solicitante.EhValido)
                    return false;
                var validacaoExterna = new SolicitacaoDocumentoValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }

    public class FornecedorVendorList : ISelfValidation
    {
        public FornecedorVendorList(string cnpj, List<IResponsavel> responsavel)
        {
            Cnpj = cnpj;
            Contatos = responsavel;
            Validar();
        }
        public FornecedorVendorList(string cnpj, string responsavelNome, string responsavelEmail)
        {
            Cnpj = cnpj;
            Contatos.Add(new ContatoFornecedor(responsavelEmail, responsavelNome));
            Validar();
        }
        public FornecedorVendorList(string cnpj, Dictionary<string, string> responsavel)
        {
            Cnpj = cnpj;
            foreach (var item in responsavel)
            {
                Contatos.Add(new ContatoFornecedor(item.Key, item.Value));
            }
            Validar();
        }
        public FornecedorVendorList(string cnpj, IResponsavel responsavel)
        {
            Cnpj = cnpj;
            Contatos.Add(responsavel);
            Validar();
        }

        private string cnpj;

        public string Cnpj
        {
            get
            {
                return cnpj
                 .Replace("-", "")
                 .Replace("/", "")
                 .Replace(".", "");
            }
            set
            {
                cnpj = value;
            }
        }

        public string Email { get; set; }

        public string Nome { get; set; }

        public string Radical
        {
            get
            {
                return Cnpj.Substring(0, 8);
            }
            set
            {
                Radical = value;
            }
        }

        public List<Endereco> Enderecos { get; private set; }

        public List<IResponsavel> Contatos { get; private set; }

        public void AdicionarContatos(IResponsavel contatos)
        {
            Contatos.Add(contatos);
        }

        public void Validar()
        {
            //if (string.IsNullOrEmpty(Cnpj))
            //    throw new Exception("Cnpj não pode ser vazio ou nulo.");
        }

        public bool EhValido
        {
            get
            {
                if (Contatos.Any(x => x.EhValido == false))
                    return false;

                var validacaoExterna = new FornecedorVendorListValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }

    public class FichaCadastral : ISelfValidation
    {
        public FichaCadastral(FornecedorVendorList fornecedor, List<Endereco> enderecos, List<Banco> bancos, List<Contato> contatos)
        {

        }

        public string RazaoSocial { get; internal set; }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new FichaCadastralValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }

    public class Banco
    {
        public Banco()
        {

        }
    }

    public class Contato
    {
        public Contato()
        {

        }
    }

    public class Arquivos
    {
        public Arquivos()
        {

        }
    }

    public class Endereco
    {
        public Endereco()
        {

        }
    }

    public class ContatoFornecedor : IResponsavel, ISelfValidation
    {
        public ContatoFornecedor(string email, string nome)
        {
            Email = email;
            Nome = nome;
        }

        public string Email { get; set; }

        public string Nome { get; set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Email))
                throw new Exception();
        }

        public bool EhValido
        {
            get
            {
                var validacaoExterna = new ContatoFornecedorValidacao();
                ValidationResult = validacaoExterna.Validar(this);
                return ValidationResult.EstaValidado;
            }
        }

        public ValidationResult ValidationResult { get; private set; }
    }

    public class DocumentoAnexado : IDocumento
    {

    }
    #region MyRegion
    //public class SolicitacaoDocumento //: ISelfValidation
    //{
    //    //public SolicitacaoDocumento(Fornecedor solicitado, Cliente solicitante)
    //    //{

    //    //}

    //    //public Fornecedor Solicitado { get; private set; }

    //    //public Cliente Solicitante { get; private set; }

    //    //public bool EhValido
    //    //{
    //    //    get
    //    //    {
    //    //        if (!Solicitado.EhValido && Solicitante.EhValido)
    //    //            return false;
    //    //        var validacaoExterna = new SolicitacaoDocumentoValidacao();
    //    //        ValidationResult = validacaoExterna.Validar(this);
    //    //        return ValidationResult.EstaValidado;
    //    //    }
    //    //}

    //    //public ValidationResult ValidationResult { get; private set; }
    //}


    //public class ResponsavelCliente
    //{
    //    public ResponsavelCliente(string email, string nome)
    //    {
    //        Email = email;
    //        Nome = nome;
    //    }
    //    public string Email { get; private set; }
    //    public string Nome { get; private set; }
    //}

    //public class DocumentoAnexado
    //{
    //} 
    #endregion
}
