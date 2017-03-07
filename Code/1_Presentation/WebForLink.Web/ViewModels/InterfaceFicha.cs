using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebForLink.Web.ViewModels
{
    //public class FichaCadastralVM
    //{
    //    public FichaCadastralVM(string documento, string razaoSocial, string nomeFantasia)
    //    {
    //        DadoGeral = new DadosGeraisVM(documento, razaoSocial, nomeFantasia);
    //        DadosEnderecos = new List<IDadosEnderecoVM>();
    //        DadosBancarios = new List<DadosBancarioVM>();
    //        Validar();
    //    }

    //    public DadosGeraisVM DadoGeral { get; private set; }
    //    public SimplesVM RoboSimples { get; private set; }
    //    public SintegraVM RoboSintegra { get; private set; }
    //    public ReceitaFederalVM RoboReceita { get; private set; }
    //    public List<IDadosEnderecoVM> DadosEnderecos { get; private set; }
    //    public List<DadosBancarioVM> DadosBancarios { get; private set; }
    //    public DadosContatoVM DadosContatos { get; private set; }
    //    public ServicosVM ListaServicos { get; private set; }
    //    public MateriaisVM ListaMateriais { get; private set; }

    //    public void Validar()
    //    {
    //        //Enderecos
    //        DadosEnderecos.Add(new DadosEnderecoObtidoNaReceitaVM("AV MARECHAL FLORIANO", "56", "", "CENTRO",
    //            "RIO DE JANEIRO", "RJ", "BRASIL", "20030457"));
    //        DadosEnderecos.Add(new DadosEnderecoVM(TiposDadosEndereco.Comercial, "AV MARECHAL FLORIANO", "57", "",
    //            "CENTRO", "RIO DE JANEIRO", "RJ", "BRASIL", "20030456"));
    //        //Bancos
    //        DadosBancarios.Add(new DadosBancarioVM("123", "12345", "12355", "AB12", "12345"));
    //        //DadosEnderecos.Add(new DadosEnderecoVM() { });
    //    }
    //}

    public class DadosGeraisVM
    {
        public DadosGeraisVM(string documento, string razaoSocial, string nomeFantasia)
            : this(documento, razaoSocial, nomeFantasia, "", "", "")
        {
        }

        public DadosGeraisVM(string documento, string razaoSocial, string nomeFantasia, string cnae,
            string inscricaoEstadual, string inscricaoMunicipal)
        {
            Documento = documento;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            Cnae = cnae;
            InscricaoEstadual = inscricaoEstadual;
            InscricaoMunicipal = inscricaoMunicipal;
            Validar();
        }

        public string Documento { get; private set; }
        public string RazaoSocial { get; private set; }
        public string NomeFantasia { get; private set; }
        public string Cnae { get; private set; }
        public string InscricaoEstadual { get; private set; }
        public string InscricaoMunicipal { get; private set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Documento))
                throw new Exception("Documento não pode ser nulo");
            if (string.IsNullOrEmpty(RazaoSocial))
                throw new Exception("Razão Social não pode ser nulo");
            if (string.IsNullOrEmpty(NomeFantasia))
                throw new Exception("Nome de Fantasia não pode ser nulo");
        }
    }

    public class DadosBancarioVM
    {
        public DadosBancarioVM(string banco, string agencia, string digitoAgencia, string conta, string digitoConta)
        {
            Banco = banco;
            Agencia = agencia;
            DigitoAgencia = digitoAgencia;
            Conta = conta;
            DigitoConta = digitoConta;
        }

        [DisplayName("Banco")]
        public string Banco { get; private set; }

        [DisplayName("Agência")]
        public string Agencia { get; private set; }

        [DisplayName("Dígito")]
        public string DigitoAgencia { get; private set; }

        [DisplayName("Conta Corrente")]
        public string Conta { get; private set; }

        [DisplayName("Digito")]
        public string DigitoConta { get; private set; }

        public string ResumoBancario
        {
            get { return string.Format("{0}-{1}/{2}-{3}", Agencia, DigitoAgencia, Conta, DigitoConta); }
        }
    }

    public interface IDadosEnderecoVM
    {
        string ResumoEndereco { get; }
        string Endereco { get; set; }
        string Numero { get; set; }
        string Complemento { get; set; }
        string Bairro { get; set; }
        string Cidade { get; set; }
        string Estado { get; set; }
        string Pais { get; set; }
        string CEP { get; set; }
        TiposDadosEndereco Tipo { get; }
    }

    public enum TiposDadosEndereco
    {
        ObtidoReceita = 1,
        Comercial = 2,
        Residencial = 3
    }

    public class CEPVO
    {
        public CEPVO(string cep)
        {
            if (!string.IsNullOrEmpty(cep))
            {
                if (cep.Length == 8)
                {
                    var a = cep.Substring(0, 5);
                    var b = cep.Substring(5, 3);
                    CepFormatado = string.Format("{0}-{1}", a, b);
                }
            }
        }

        public string CepFormatado { get; private set; }
    }

    public class DadosEnderecoVM : IDadosEnderecoVM
    {
        private readonly CEPVO _cep;

        public DadosEnderecoVM(TiposDadosEndereco tipo, string endereco, string numero, string complemento,
            string bairro, string cidade, string estado, string pais, string cep)
        {
            Endereco = endereco;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            _cep = new CEPVO(cep);
            //CEP = cep;
            _tipo = tipo;
        }

        public DadosEnderecoVM()
        {
        }

        private TiposDadosEndereco _tipo { get; }

        [DisplayName("Endereço")]
        //[StringLength(255, MinimumLength = 3, ErrorMessage = "O Campo endereço deve conter no mínimo 3 caracteres e no máximo 255 caracteres")]
        [StringLength(255)]
        public string Endereco { get; set; }

        [DisplayName("Número")]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = "O Campo endereço deve conter no mínimo 3 caracteres e no máximo 50 caracteres")]
        [StringLength(50)]
        public string Numero { get; set; }

        [DisplayName("Complemento")]
        [StringLength(255)]
        public string Complemento { get; set; }

        [DisplayName("Bairro")]
        [StringLength(100)]
        public string Bairro { get; set; }

        [DisplayName("Cidade")]
        [StringLength(100)]
        public string Cidade { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; }

        [DisplayName("País")]
        [StringLength(100)]
        public string Pais { get; set; }

        [StringLength(8)]
        public string CEP
        {
            get { return _cep != null ? _cep.CepFormatado : null; }
            set { CEP = value; }
        }

        public TiposDadosEndereco Tipo
        {
            get { return _tipo; }
        }

        public string ResumoEndereco
        {
            get { return string.Format("{0}, {1} - {2} - {3}", Endereco, Numero, Complemento, Bairro); }
        }
    }

    public class DadosEnderecoObtidoNaReceitaVM : IDadosEnderecoVM
    {
        private readonly CEPVO _cep;

        public DadosEnderecoObtidoNaReceitaVM(string endereco, string numero, string complemento, string bairro,
            string cidade, string estado, string pais, string cep)
        {
            Endereco = endereco;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            _cep = new CEPVO(cep);
        }

        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }

        public string CEP
        {
            get { return _cep.CepFormatado; }
            set { CEP = value; }
        }

        public TiposDadosEndereco Tipo
        {
            get { return TiposDadosEndereco.ObtidoReceita; }
        }

        public string ResumoEndereco
        {
            get { return string.Format("{0}, {1} - {2} - {3}", Endereco, Numero, Complemento, Bairro); }
        }
    }

    public class SimplesVM
    {
    }

    public class SintegraVM
    {
    }

    public class ReceitaFederalVM
    {
    }


    public class MateriaisVM
    {
    }

    public interface IFichaCadastral
    {
    }

    public interface IDadoGeral
    {
        string Documento { get; }
        string RazaoSocial { get; }
        string NomeFantasia { get; }
        string Cnae { get; }
        string InscricaoEstadual { get; }
        string InscricaoMunicipal { get; }
        void Validar();
    }

    public interface IOrgaosPublico
    {
    }

    public interface IDadoEndereco
    {
        string TipoEndereco { get; }
        string Endereco { get; }
        string Numero { get; }
        string Complemento { get; }
        string Cep { get; }
        string Bairro { get; }
        string Cidade { get; }
        string Estado { get; }
        string Pais { get; }
        bool Preenchido { get; }
    }

    public interface IDadoBancario
    {
    }
}