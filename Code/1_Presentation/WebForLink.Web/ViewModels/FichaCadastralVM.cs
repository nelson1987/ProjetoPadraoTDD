using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebForLink.Web.ViewModels
{
    public enum TipoFornecedorIndividual
    {
        PessoaJuridica = 1,
        PessoaFisica = 2
    }
    public class FichaCadastralVM
    {
        public FichaCadastralVM(int tipo, string documento)
            : this((TipoFornecedorIndividual)tipo, documento)
        {

        }

        private FichaCadastralVM(TipoFornecedorIndividual tipo, string documento)
        {
            if (tipo == TipoFornecedorIndividual.PessoaJuridica)
                this.Documento = new DadosGeraisPessoaJuridicaVM(documento);
            else if (tipo == TipoFornecedorIndividual.PessoaFisica)
                this.Documento = new DadosGeraisPessoaFisicaVM(documento);
            Endereco = new List<IDadoEndereco>();
            Validar();
        }
        public IDadoGeral Documento { get; private set; }

        public List<IDadoEndereco> Endereco { get; private set; }

        public void Validar()
        {
            Endereco.Add(new EnderecoReceitaVM() { });
            Endereco.Add(new EnderecoFornecedorVM { });
        }
    }
    public class DadosGeraisPessoaJuridicaVM : IDadoGeral
    {
        public DadosGeraisPessoaJuridicaVM(string documento)
        {
            Documento = documento;
        }

        public string Cnae { get; }

        public string Documento { get; }

        public string InscricaoEstadual { get; }

        public string InscricaoMunicipal { get; }

        public string NomeFantasia { get; }

        public string RazaoSocial { get; }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }

    public class DadosGeraisPessoaFisicaVM : IDadoGeral
    {
        public DadosGeraisPessoaFisicaVM(string documento)
        {

        }
        public string Cnae
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Documento
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string InscricaoEstadual
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string InscricaoMunicipal
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string NomeFantasia
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string RazaoSocial
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void Validar()
        {
            throw new NotImplementedException();
        }
    }

    public class EnderecoReceitaVM : IDadoEndereco
    {
        public string Bairro { get; }

        public string Cep { get; }

        public string Cidade { get; }
        public string Complemento { get; }

        public string Endereco { get; }
        public string Estado { get; }

        public string Numero { get; }
        public string Pais { get; }

        public bool Preenchido
        {
            get
            {
                return true;
            }
        }

        [Display(Name = "Tipo de Endereço")]
        public string TipoEndereco { get { return "Obtido na receita"; } }
    }
    public class EnderecoFornecedorVM : IDadoEndereco
    {
        public string Bairro { get; }

        public string Cep { get; }
        public string Cidade { get; }
        public string Complemento { get; }
        public string Endereco { get; }
        public string Estado { get; }
        public string Numero { get; }
        public string Pais { get; }

        public bool Preenchido { get; }

        [Display(Name = "Tipo de Endereço")]
        public string TipoEndereco { get; }

        public List<SelectListItem> DropTipo
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem() { Text = "Selecione um endereço", Value = "0" },
                    new SelectListItem() { Text = "COMERCIAL", Value = "1" },
                    new SelectListItem() { Text = "RESIDENCIAL", Value = "2" },
                };
            }
            set {
                value = DropTipo;
            }
        }

    }
}