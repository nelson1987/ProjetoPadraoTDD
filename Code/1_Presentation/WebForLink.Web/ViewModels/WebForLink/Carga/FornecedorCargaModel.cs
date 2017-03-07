using System;
using System.Text;
using WebForLink.Domain.Enums;
using WebForLink.Web.Controllers.Extensoes;

namespace WebForLink.Web.ViewModels.Carga
{
    [Serializable]
    public class FornecedorCargaModel : DemandaSapModel
    {
        public FornecedorCargaModel() { }
        /// <summary>
        /// Criação de Fornecedor
        /// </summary>
        /// <param name="codigoSolicitacao"></param>
        /// <param name="empresa"></param>
        /// <param name="grupoContas"></param>
        /// <param name="organizacaoCompras"></param>
        /// <param name="simplesNacional"></param>
        /// <param name="nome1"></param>
        /// <param name="nomeFantasia"></param>
        /// <param name="cep"></param>
        /// <param name="cidade"></param>
        /// <param name="tipoLogradouro"></param>
        /// <param name="rua"></param>
        /// <param name="numero"></param>
        /// <param name="complemento"></param>
        /// <param name="bairro"></param>
        /// <param name="estado"></param>
        /// <param name="telefone"></param>
        /// <param name="enderecoMail"></param>
        /// <param name="telefoneCelular"></param>
        /// <param name="cliente"></param>
        /// <param name="grupoEmpresas"></param>
        /// <param name="cnpj"></param>
        /// <param name="cpf"></param>
        /// <param name="inscricaoEstadual"></param>
        /// <param name="inscricaoMunicipal"></param>
        /// <param name="banco"></param>
        /// <param name="agencia"></param>
        /// <param name="codigoAgencia"></param>
        /// <param name="contaCorrente"></param>
        /// <param name="dVContaCorrente"></param>
        /// <param name="chaveOrdenacao"></param>
        /// <param name="nomeContatoVendedor"></param>
        /// <param name="telefoneVendedor"></param>
        /// <param name="pais"></param>
        public FornecedorCargaModel(string codigoSolicitacao, string empresa, string grupoContas, string organizacaoCompras, string simplesNacional
            , string nome1, string nomeFantasia, string cep, string cidade, string tipoLogradouro, string rua, string numero
            , string complemento, string bairro, string estado, string telefone, string enderecoMail, string telefoneCelular, string cliente
            , string grupoEmpresas, string cnpj, string cpf, string inscricaoEstadual, string inscricaoMunicipal, string banco, string agencia
            , string codigoAgencia, string contaCorrente, string dVContaCorrente, string chaveOrdenacao, string nomeContatoVendedor, string telefoneVendedor
            , string pais)
        {
            this.CodigoSolicitacao = codigoSolicitacao;
            this.Empresa = empresa;
            this.GrupoContas = grupoContas;
            this.OrganizacaoCompras = organizacaoCompras;
            this.SimplesNacional = simplesNacional;
            this.Nome1 = nome1;
            this.NomeFantasia = nomeFantasia;
            this.CEP = cep;
            this.Cidade = cidade;
            this.TipoLogradouro = tipoLogradouro;
            this.Rua = rua;
            this.Numero = numero;
            this.Complemento = complemento;
            this.Bairro = bairro;
            this.Estado = estado;
            this.Telefone = telefone;
            this.EnderecoMail = enderecoMail;
            this.TelefoneCelular = telefoneCelular;
            this.Cliente = cliente;
            this.GrupoEmpresas = grupoEmpresas;
            this.CNPJ = cnpj;
            this.CPF = cpf;
            this.InscricaoEstadual = inscricaoEstadual;
            this.InscricaoMunicipal = inscricaoMunicipal;
            this.Banco = banco;
            this.Agencia = agencia;
            this.CodigoAgencia = codigoAgencia;
            this.ContaCorrente = contaCorrente;
            this.DVContaCorrente = dVContaCorrente;
            this.ChaveOrdenacao = chaveOrdenacao;
            this.NomeContatoVendedor = nomeContatoVendedor;
            this.TelefoneVendedor = telefoneVendedor;
            this.Pais = pais;
        }
        public string GrupoContas { get; set; }
        public string SimplesNacional { get; set; }
        public string Nome1 { get; set; }
        public string Nome2 { get; set; }
        public string Nome3 { get; set; }
        public string Nome4 { get; set; }
        public string NomeFantasia { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string TipoLogradouro { get; set; }
        public string Rua { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Estado { get; set; }
        public string Telefone { get; set; }
        public string EnderecoMail { get; set; }
        public string TelefoneCelular { get; set; }
        public string Cliente { get; set; }
        public string GrupoEmpresas { get; set; }
        public string CNPJ { get; set; }
        public string CPF { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string CodigoAgencia { get; set; }
        public string ContaCorrente { get; set; }
        public string DVContaCorrente { get; set; }
        public string ChaveOrdenacao { get; set; }
        public string NomeContatoVendedor { get; set; }
        public string TelefoneVendedor { get; set; }
        public string Pais { get; set; }
        //--
        public string LinhaComLimites { get; set; }

        public string GerarLinhaCargaCriarFornecedor()
        {
            try
            {
                StringBuilder Linha = new StringBuilder();
                //Campo 1 - Tamanho 1
                Linha.Append((int)EnumTiposAcao.Criacao + ";");
                //Campo 2 - Tamanho 6
                limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
                //Campo 3 - Tamanho 4
                limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
                //Campo 4 - Tamanho 4
                limitarTamanhoPropriedade(GrupoContas, 4, this.GrupoContas, Linha);
                //Campo 5 - Tamanho 4
                limitarTamanhoPropriedade(OrganizacaoCompras, 4, this.OrganizacaoCompras, Linha);
                //Campo 6 - Tamanho 1
                this.SimplesNacional = SimplesNacional != null ? "x" : null;
                limitarTamanhoPropriedade(SimplesNacional, 1, this.SimplesNacional, Linha);
                //Campo 7 - Tamanho 35
                ValidarNomes(Linha);
                limitarTamanhoPropriedade(NomeFantasia, 20, this.NomeFantasia, Linha);
                //Campo 12 - Tamanho 10
                limitarTamanhoPropriedade(CEP, 10, this.CEP, Linha);
                //Campo 13 - Tamanho 35 	Localidade Maiusculo p/ determinar Domicilio Fiscal
                limitarTamanhoPropriedade(Cidade, 35, this.Cidade, Linha);
                //Campo 14 - Tamanho 10 	Quebrar conforme ADRC, rua, número, Compl.
                limitarTamanhoPropriedade(TipoLogradouro, 10, this.TipoLogradouro, Linha);
                //Campo 15 - Tamanho 60
                limitarTamanhoPropriedade(Rua, 60, this.Rua, Linha);
                //Campo 16 - Tamanho 10
                limitarTamanhoPropriedade(Numero, 10, this.Numero, Linha);
                //Campo 17 - Tamanho 10
                limitarTamanhoPropriedade(Complemento, 10, this.Complemento, Linha);
                //Campo 18 - Tamanho 40
                limitarTamanhoPropriedade(Bairro, 40, this.Bairro, Linha);
                //Campo 19 - Tamanho 2
                limitarTamanhoPropriedade(Estado, 2, this.Estado, Linha);
                //Campo 20 - Tamanho 30 	Considerar Tel1
                limitarTamanhoPropriedade(Telefone, 30, this.Telefone, Linha);
                //Campo 21 - Tamanho 241
                limitarTamanhoPropriedade(EnderecoMail, 241, this.EnderecoMail, Linha);
                //Campo 22 - Tamanho 30
                limitarTamanhoPropriedade(TelefoneCelular, 30, this.TelefoneCelular, Linha);
                //Campo 23 - Tamanho 10
                limitarTamanhoPropriedade(Cliente, 10, this.Cliente, Linha);
                //Campo 24 - Tamanho 4
                limitarTamanhoPropriedade(GrupoEmpresas, 4, this.GrupoEmpresas, Linha);
                //Campo 25 - Tamanho 16 	sem pontos
                this.CNPJ = CNPJ != null ? CNPJ.Replace(".", "") : CNPJ;
                limitarTamanhoPropriedade(CNPJ, 16, this.CNPJ, Linha);
                //Campo 26 - Tamanho 11 	sem pontos
                this.CPF = CPF != null ? CPF.Replace(".", string.Empty) : CPF;
                limitarTamanhoPropriedade(CPF, 11, this.CPF, Linha);
                //Campo 27 - Tamanho 18
                this.InscricaoEstadual = InscricaoEstadual != "ativo" ? "isento" : InscricaoEstadual;
                limitarTamanhoPropriedade(InscricaoEstadual, 18, this.InscricaoEstadual, Linha);
                //Campo 28 - Tamanho 18
                limitarTamanhoPropriedade(InscricaoMunicipal, 18, this.InscricaoMunicipal, Linha);
                //Campo 29 - Tamanho 3
                limitarTamanhoPropriedade(Banco, 3, this.Banco, Linha);
                //Campo 30 - Tamanho 4
                limitarTamanhoPropriedade(Agencia, 4, this.Agencia, Linha);
                //Campo 31 - Tamanho 1
                limitarTamanhoPropriedade(CodigoAgencia, 1, this.CodigoAgencia, Linha);
                //Campo 32 - Tamanho 18
                limitarTamanhoPropriedade(ContaCorrente, 18, this.ContaCorrente, Linha);
                //Campo 33 - Tamanho 2
                limitarTamanhoPropriedade(DVContaCorrente, 2, this.DVContaCorrente, Linha);
                //Campo 34 - Tamanho 30
                limitarTamanhoPropriedade("0001", 4, this.ChaveOrdenacao, Linha);
                //Campo 34 - Tamanho 30
                limitarTamanhoPropriedade(NomeContatoVendedor, 30, this.NomeContatoVendedor, Linha);
                //Campo 35 - Tamanho 16
                limitarTamanhoPropriedade(TelefoneVendedor, 16, this.TelefoneVendedor, Linha);
                //Campo 36 - Tamanho 2
                limitarTamanhoPropriedade(Pais, 2, this.Pais, Linha);
                return Linha.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string GerarLinhaCargaAmpliarFornecedor()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.Ampliacao + ";");
            //Campo 2 - Tamanho 6
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            //Campo 3 - Tamanho 4
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            //Campo 4 - Tamanho 10
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);
            //Campo 5 - Tamanho 4
            limitarTamanhoPropriedade(OrganizacaoCompras, 4, this.OrganizacaoCompras, Linha);
            //Campo 6 - Tamanho 3
            limitarTamanhoPropriedade(Banco, 3, this.Banco, Linha);
            //Campo 7 - Tamanho 4
            limitarTamanhoPropriedade(Agencia, 4, this.Agencia, Linha);
            //Campo 8 - Tamanho 1
            limitarTamanhoPropriedade(CodigoAgencia, 1, this.CodigoAgencia, Linha);
            //Campo 9 - Tamanho 18
            limitarTamanhoPropriedade(ContaCorrente, 18, this.ContaCorrente, Linha);
            //Campo 10 - Tamanho 2
            limitarTamanhoPropriedade(DVContaCorrente, 2, this.DVContaCorrente, Linha);
            //Campo 34 - Tamanho 30
            limitarTamanhoPropriedade("0001", 4, this.ChaveOrdenacao, Linha);
            //Campo 11 - Tamanho 30
            limitarTamanhoPropriedade(NomeContatoVendedor, 30, this.NomeContatoVendedor, Linha);
            //Campo 12 - Tamanho 16
            limitarTamanhoPropriedade(TelefoneVendedor, 16, this.TelefoneVendedor, Linha);
            return Linha.ToString();
        }
        private void ValidarNomes(StringBuilder Linha)
        {
            if (Nome1.Length >= 40)
            {
                AbreviadorExtensions abrevNovo = new AbreviadorExtensions();
                var abreviado = abrevNovo.AbreviadorRobo(140, this.Nome1);
                string inde1 = abreviado.Substring(0, 40);
                int proximoInicio1 = inde1.LastIndexOf(" ");
                string indeAbrev1 = inde1.Substring(0, proximoInicio1);

                string inde2 = abreviado.Substring(proximoInicio1 + 1, 40);
                int proximoInicio2 = inde2.LastIndexOf(" ");
                string indeAbrev2 = inde2.Substring(0, proximoInicio2);

                string inde3 = abreviado.Substring(40+proximoInicio2, 40);
                int proximoInicio3 = inde3.LastIndexOf(" ");
                string indeAbrev3 = inde3.Substring(0, proximoInicio3);

                string inde4 = abreviado.Substring(79 + proximoInicio3, abreviado.Length - (79 + proximoInicio3));;
                int proximoInicio4 = inde4.LastIndexOf(" ");
                string indeAbrev4 = inde4.Substring(0, proximoInicio4);


                limitarTamanhoPropriedade(indeAbrev1, 40, this.Nome1, Linha);
                limitarTamanhoPropriedade(indeAbrev2, 40, this.Nome2, Linha);
                limitarTamanhoPropriedade(indeAbrev3, 40, this.Nome3, Linha);
                limitarTamanhoPropriedade(indeAbrev4, 40, this.Nome4, Linha);
            }
            else
            {
                limitarTamanhoPropriedade(Nome1, 40, this.Nome1, Linha);
                limitarTamanhoPropriedade(Nome2, 40, this.Nome2, Linha);
                limitarTamanhoPropriedade(Nome3, 40, this.Nome3, Linha);
                limitarTamanhoPropriedade(Nome4, 40, this.Nome4, Linha);
            }
        }
        public string GerarLinhaCargaCriarFornecedorRetorno()
        {
            try
            {
                StringBuilder Linha = new StringBuilder();
                Linha.Append("1;");
                limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
                Linha.Append("1234567890123456;");
                limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
                limitarTamanhoPropriedade(GrupoContas, 4, this.GrupoContas, Linha);
                limitarTamanhoPropriedade(OrganizacaoCompras, 4, this.OrganizacaoCompras, Linha);
                this.CNPJ = CNPJ != null ? CNPJ.Replace(".", "") : CNPJ;
                limitarTamanhoPropriedade(CNPJ, 16, this.CNPJ, Linha);
                this.CPF = CPF != null ? CPF.Replace(".", string.Empty) : CPF;
                limitarTamanhoPropriedade(CPF, 11, this.CPF, Linha);
                Linha.Append("0;");
                Linha.Append(";");
                return Linha.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string RazaoSocial { get; set; }
    }
}
