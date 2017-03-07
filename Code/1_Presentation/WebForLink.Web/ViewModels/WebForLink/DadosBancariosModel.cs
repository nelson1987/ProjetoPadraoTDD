using System.Text;
using WebForLink.Domain.Enums;
using WebForLink.Web.ViewModels.Carga;

namespace WebForLink.Web.ViewModels
{
    public class DadosBancariosModel : DemandaSapModel
    {
        public DadosBancariosModel() { }
        /// <summary>
        /// Dados bancarios
        /// </summary>
        /// <param name="codigoSolicitacao"></param>
        /// <param name="empresa"></param>
        /// <param name="codigoSap"></param>
        /// <param name="banco1"></param>
        /// <param name="agencia1"></param>
        /// <param name="codigoAgencia1"></param>
        /// <param name="contaCorrente1"></param>
        /// <param name="dVContaCorrente1"></param>
        /// <param name="banco2"></param>
        /// <param name="agencia2"></param>
        /// <param name="codigoAgencia2"></param>
        /// <param name="contaCorrente2"></param>
        /// <param name="dVContaCorrente2"></param>
        /// <param name="banco3"></param>
        /// <param name="agencia3"></param>
        /// <param name="codigoAgencia3"></param>
        /// <param name="contaCorrente3"></param>
        /// <param name="dVContaCorrente3"></param>
        /// <param name="banco4"></param>
        /// <param name="agencia4"></param>
        /// <param name="codigoAgencia4"></param>
        /// <param name="contaCorrente4"></param>
        /// <param name="dVContaCorrente4"></param>
        /// <param name="banco5"></param>
        /// <param name="agencia5"></param>
        /// <param name="codigoAgencia5"></param>
        /// <param name="contaCorrente5"></param>
        /// <param name="dVContaCorrente5"></param>
        public DadosBancariosModel(string codigoSolicitacao, string empresa, string codigoSap
            , string banco1, string agencia1, string codigoAgencia1, string contaCorrente1, string dVContaCorrente1
            , string banco2, string agencia2, string codigoAgencia2, string contaCorrente2, string dVContaCorrente2
            , string banco3, string agencia3, string codigoAgencia3, string contaCorrente3, string dVContaCorrente3
            , string banco4, string agencia4, string codigoAgencia4, string contaCorrente4, string dVContaCorrente4
            , string banco5, string agencia5, string codigoAgencia5, string contaCorrente5, string dVContaCorrente5)
        {
            this.CodigoSolicitacao = codigoSolicitacao;
            this.Empresa = empresa;
            this.CodigoSAP = codigoSap;
            this.Banco1 = banco1;
            this.Agencia1 = agencia1;
            this.CodigoAgencia1 = codigoAgencia1;
            this.ContaCorrente1 = contaCorrente1;
            this.DVContaCorrente1 = dVContaCorrente1;
            this.Banco2 = banco2;
            this.Agencia2 = agencia2;
            this.CodigoAgencia2 = codigoAgencia2;
            this.ContaCorrente2 = contaCorrente2;
            this.DVContaCorrente2 = dVContaCorrente2;
            this.Banco3 = banco3;
            this.Agencia3 = agencia3;
            this.CodigoAgencia3 = codigoAgencia3;
            this.ContaCorrente3 = contaCorrente3;
            this.DVContaCorrente3 = dVContaCorrente3;
            this.Banco4 = banco4;
            this.Agencia4 = agencia4;
            this.CodigoAgencia4 = codigoAgencia4;
            this.ContaCorrente4 = contaCorrente4;
            this.DVContaCorrente4 = dVContaCorrente4;
            this.Banco5 = banco5;
            this.Agencia5 = agencia5;
            this.CodigoAgencia5 = codigoAgencia5;
            this.ContaCorrente5 = contaCorrente5;
            this.DVContaCorrente5 = dVContaCorrente5;
        }
        public string OrdemPriorizacao { get; set; }
        public string Banco1 { get; set; }
        public string Agencia1 { get; set; }
        public string CodigoAgencia1 { get; set; }
        public string ContaCorrente1 { get; set; }
        public string DVContaCorrente1 { get; set; }
        public string ChaveOrdenacao1 { get; set; }
        public string Banco2 { get; set; }
        public string Agencia2 { get; set; }
        public string CodigoAgencia2 { get; set; }
        public string ContaCorrente2 { get; set; }
        public string DVContaCorrente2 { get; set; }
        public string ChaveOrdenacao2 { get; set; }
        public string Banco3 { get; set; }
        public string Agencia3 { get; set; }
        public string CodigoAgencia3 { get; set; }
        public string ContaCorrente3 { get; set; }
        public string DVContaCorrente3 { get; set; }
        public string ChaveOrdenacao3 { get; set; }
        public string Banco4 { get; set; }
        public string Agencia4 { get; set; }
        public string CodigoAgencia4 { get; set; }
        public string ContaCorrente4 { get; set; }
        public string DVContaCorrente4 { get; set; }
        public string ChaveOrdenacao4 { get; set; }
        public string Banco5 { get; set; }
        public string Agencia5 { get; set; }
        public string CodigoAgencia5 { get; set; }
        public string ContaCorrente5 { get; set; }
        public string DVContaCorrente5 { get; set; }
        public string ChaveOrdenacao5 { get; set; }


        public string GerarLinhaModificarDadosBancarios()
        {
            StringBuilder Linha = new StringBuilder();
            //Campo 1 - Tamanho 1
            Linha.Append((int)EnumTiposAcao.DadosBancarios + ";");
            //Campo 2 - Tamanho 6
            limitarTamanhoPropriedade(CodigoSolicitacao, 6, this.CodigoSolicitacao, Linha);
            //Campo 3 - Tamanho 4
            limitarTamanhoPropriedade(Empresa, 4, this.Empresa, Linha);
            //Campo 4 - Tamanho 10
            limitarTamanhoPropriedade(CodigoSAP, 10, this.CodigoSAP, Linha);

            //Campo 6 - Tamanho 3
            limitarTamanhoPropriedade(Banco1, 3, this.Banco1, Linha);
            //Campo 7 - Tamanho 4
            limitarTamanhoPropriedade(Agencia1, 4, this.Agencia1, Linha);
            //Campo 8 - Tamanho 1
            limitarTamanhoPropriedade(CodigoAgencia1, 1, this.CodigoAgencia1, Linha);
            //Campo 9 - Tamanho 18
            limitarTamanhoPropriedade(ContaCorrente1, 18, this.ContaCorrente1, Linha);
            //Campo 10 - Tamanho 2
            limitarTamanhoPropriedade(DVContaCorrente1, 2, this.DVContaCorrente1, Linha);
            //Campo 34 - Tamanho 30
            limitarTamanhoPropriedade("0001", 4, this.ChaveOrdenacao1, Linha);

            //Campo 6 - Tamanho 3
            limitarTamanhoPropriedade(Banco2, 3, this.Banco2, Linha);
            //Campo 7 - Tamanho 4
            limitarTamanhoPropriedade(Agencia2, 4, this.Agencia2, Linha);
            //Campo 8 - Tamanho 1
            limitarTamanhoPropriedade(CodigoAgencia2, 1, this.CodigoAgencia2, Linha);
            //Campo 9 - Tamanho 18
            limitarTamanhoPropriedade(ContaCorrente2, 18, this.ContaCorrente2, Linha);
            //Campo 10 - Tamanho 2
            limitarTamanhoPropriedade(DVContaCorrente2, 2, this.DVContaCorrente2, Linha);
            //Campo 34 - Tamanho 30
            limitarTamanhoPropriedade("0002", 4, this.ChaveOrdenacao2, Linha);

            //Campo 6 - Tamanho 3
            limitarTamanhoPropriedade(Banco3, 3, this.Banco3, Linha);
            //Campo 7 - Tamanho 4
            limitarTamanhoPropriedade(Agencia3, 4, this.Agencia3, Linha);
            //Campo 8 - Tamanho 1
            limitarTamanhoPropriedade(CodigoAgencia3, 1, this.CodigoAgencia3, Linha);
            //Campo 9 - Tamanho 18
            limitarTamanhoPropriedade(ContaCorrente3, 18, this.ContaCorrente3, Linha);
            //Campo 10 - Tamanho 2
            limitarTamanhoPropriedade(DVContaCorrente3, 2, this.DVContaCorrente3, Linha);
            //Campo 34 - Tamanho 30
            limitarTamanhoPropriedade("0003", 4, this.ChaveOrdenacao3, Linha);

            //Campo 6 - Tamanho 3
            limitarTamanhoPropriedade(Banco4, 3, this.Banco4, Linha);
            //Campo 7 - Tamanho 4
            limitarTamanhoPropriedade(Agencia4, 4, this.Agencia4, Linha);
            //Campo 8 - Tamanho 1
            limitarTamanhoPropriedade(CodigoAgencia4, 1, this.CodigoAgencia4, Linha);
            //Campo 9 - Tamanho 18
            limitarTamanhoPropriedade(ContaCorrente4, 18, this.ContaCorrente4, Linha);
            //Campo 10 - Tamanho 2
            limitarTamanhoPropriedade(DVContaCorrente4, 2, this.DVContaCorrente4, Linha);
            //Campo 34 - Tamanho 30
            limitarTamanhoPropriedade("0004", 4, this.ChaveOrdenacao4, Linha);

            //Campo 6 - Tamanho 3
            limitarTamanhoPropriedade(Banco5, 3, this.Banco5, Linha);
            //Campo 7 - Tamanho 4
            limitarTamanhoPropriedade(Agencia5, 4, this.Agencia5, Linha);
            //Campo 8 - Tamanho 1
            limitarTamanhoPropriedade(CodigoAgencia5, 1, this.CodigoAgencia5, Linha);
            //Campo 9 - Tamanho 18
            limitarTamanhoPropriedade(ContaCorrente5, 18, this.ContaCorrente5, Linha);
            //Campo 10 - Tamanho 2
            limitarTamanhoPropriedade(DVContaCorrente5, 2, this.DVContaCorrente5, Linha);
            //Campo 34 - Tamanho 30
            limitarTamanhoPropriedade("0005", 4, this.ChaveOrdenacao5, Linha);

            return Linha.ToString();
        }
    }

}