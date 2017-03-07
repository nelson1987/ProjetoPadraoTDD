using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using WebForLink.Domain.Entities;
using System;

namespace WebForLink.Web.ViewModels
{
    public class DocumentoAnexadoVM
    {
        public int Id { get; set; }

        public int IdFichaCadastral { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public string Validade { get; set; }
    }
    public class ConviteVM
    {
        [DisplayName("Descrição")]
        public string Descricao { get; set; }
        [DisplayName("Código")]
        public string Codigo { get; set; }
        public HttpPostedFileBase[] files { get; set; }
    }
    public class SolicitacaoConviteVM
    {
        public bool EhValido { get; set; }
        public int PassoAtual { get; set; }

        public int Id { get; set; }

        public int IdFichaCadastral { get; set; }
        public string Cnpj { get; set; }

        public bool Preenchido { get; set; }

        public bool FichaCadastralPreenchido { get; set; }
        public bool DocumentosPreenchido { get; set; }

        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }

        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }

        public FichaCadastralVM FichaCadastral { get; set; }

        public string IdCriptografado { get; set; }

        public static SolicitacaoConviteVM ToViewModel(Solicitacao model)
        {
            return Mapper.Map<SolicitacaoConviteVM>(model);
        }
        public static Solicitacao ToModel(SolicitacaoConviteVM model)
        {
            return Mapper.Map<Solicitacao>(model);
        }

        public static Solicitacao ToModelFichaCadastral(SolicitacaoConviteVM model)
        {
            return Mapper.Map<Solicitacao>(model.FichaCadastral);
        }

        public void AdicionarIdCriptografado(string id)
        {
            IdCriptografado = id;
        }
    }
    public class FichaCadastralVM
    {
        public FichaCadastralVM()
        {
            Bancos = new List<BancoVM>();
            Contatos = new List<ContatoVM>();
            DocumentoAnexados = new List<DocumentoAnexadoVM>();
            Enderecos = new List<EnderecoVM>();
        }

        public int Id { get; set; }

        public int Status { get; set; }

        public int ContratanteID { get; set; }

        [UIHint("_Bancos")]
        public List<BancoVM> Bancos { get; set; }

        [UIHint("_Contatos")]
        public List<ContatoVM> Contatos { get; set; }

        [UIHint("DocumentoAnexadoVM")]
        public List<DocumentoAnexadoVM> DocumentoAnexados { get; set; }

        [UIHint("_Enderecos")]
        public List<EnderecoVM> Enderecos { get; set; }

        public static FichaCadastralVM ToViewModel(Domain.Entities.FichaCadastral model)
        {
            return Mapper.Map<FichaCadastralVM>(model);
        }

    }
    public class BancoVM
    {
        public int Id { get; set; }

        public int IdFichaCadastral { get; set; }

        [DisplayName("Banco")]
        [Required]
        public string Banco { get; set; }

        [DisplayName("Agência")]
        public string Agencia { get; set; }

        public string AgenciaDV { get; set; }

        [DisplayName("Conta Corrente")]
        public string ContaCorrente { get; set; }

        public string ContaCorrenteDV { get; set; }

        [DisplayName("Arquivo")]
        public string Arquivo { get; set; }

        public string ArquivoDescricao { get; set; }


        public List<SelectListItem> BancoList
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Value = "0", Text = "-- Selecione --" },
                    new SelectListItem { Value = "1", Text = "ABC Brasil Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "2", Text = "Administradora de Consórcio Nacional Gazin LTDA" },
                    new SelectListItem { Value = "3", Text = "Administradora de Consórcio Nacional Honda LTDA" },
                    new SelectListItem { Value = "4", Text = "Administradora de Consórcio RCI Brasil LTDA" },
                    new SelectListItem { Value = "5", Text = "Administradora de Consórcios Sicredi LTDA" },
                    new SelectListItem { Value = "6", Text = "Agiplan Administradora de Consórcios S.A." },
                    new SelectListItem { Value = "7", Text = "Agiplan Financeira S.A. Crédito Financiamento e Investimento  " },
                    new SelectListItem { Value = "8", Text = "Ágora Corretora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "9", Text = "Alfa Arrendamento Mercantil S.A." },
                    new SelectListItem { Value = "10", Text = "Alfa Corretora de Câmbio e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "11", Text = "Aymoré Crédito Financiamento e Investimento S.A." },
                    new SelectListItem { Value = "12", Text = "Bacor Corretora de Câmbio e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "654 - Banco A.J.Renner S.A." },
                    new SelectListItem { Value = "12", Text = "246 - Banco ABC Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "075 - Banco ABN AMRO S.A." },
                    new SelectListItem { Value = "12", Text = "121 - Banco Agiplan S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Alfa de Investimentos S.A." },
                    new SelectListItem { Value = "12", Text = "025 - Banco Alfa S.A." },
                    new SelectListItem { Value = "12", Text = "641 - Banco Alvorada S.A." },
                    new SelectListItem { Value = "12", Text = "065 - Banco Andbank (Brasil) S.A." },
                    new SelectListItem { Value = "12", Text = "213 - Banco Arbi S.A." },
                    new SelectListItem { Value = "12", Text = "019 - Banco Azteca do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "024 - Banco BANDEPE S.A." },
                    new SelectListItem { Value = "12", Text = "740 - Banco Barclays S.A." },
                    new SelectListItem { Value = "12", Text = "107 - Banco BBM S.A." },
                    new SelectListItem { Value = "12", Text = "096 - Banco BM&FBOVESPA de Serviços de Liquidação e Custódia S.A" },
                    new SelectListItem { Value = "12", Text = "318 - Banco BMG S.A." },
                    new SelectListItem { Value = "12", Text = "752 - Banco BNP Paribas Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "248 - Banco Boavista Interatlântico S.A." },
                    new SelectListItem { Value = "12", Text = "218 - Banco Bonsucesso S.A." },
                    new SelectListItem { Value = "12", Text = "069 - Banco BPN Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "063 - Banco Bradescard S.A." },
                    new SelectListItem { Value = "12", Text = "036 - Banco Bradesco BBI S.A." },
                    new SelectListItem { Value = "12", Text = "122 - Banco Bradesco BERJ S.A." },
                    new SelectListItem { Value = "12", Text = "204 - Banco Bradesco Cartões S.A." },
                    new SelectListItem { Value = "12", Text = "394 - Banco Bradesco Financiamentos S.A." },
                    new SelectListItem { Value = "12", Text = "237 - Banco Bradesco S.A." },
                    new SelectListItem { Value = "12", Text = "225 - Banco Brascan S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco BRJ S.A." },
                    new SelectListItem { Value = "12", Text = "208 - Banco BTG Pactual S.A." },
                    new SelectListItem { Value = "12", Text = "044 - Banco BVA S.A." },
                    new SelectListItem { Value = "12", Text = "263 - Banco Cacique S.A." },
                    new SelectListItem { Value = "12", Text = "473 - Banco Caixa Geral - Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "412 - Banco Capital S.A." },
                    new SelectListItem { Value = "12", Text = "040 - Banco Cargill S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Caterpillar S.A." },
                    new SelectListItem { Value = "12", Text = "Banco CBSS S.A." },
                    new SelectListItem { Value = "12", Text = "266 - Banco Cédula S.A." },
                    new SelectListItem { Value = "12", Text = "739 - Banco Cetelem S.A." },
                    new SelectListItem { Value = "12", Text = "233 - Banco Cifra S.A." },
                    new SelectListItem { Value = "12", Text = "745 - Banco Citibank S.A." },
                    new SelectListItem { Value = "12", Text = "241 - Banco Clássico S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco CNH Industrial Capital S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Commercial Investment Trus do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "095 - Banco Confidence de Câmbio S.A." },
                    new SelectListItem { Value = "12", Text = "756 - Banco Cooperativo do Brasil S.A. - BANCOOB" },
                    new SelectListItem { Value = "12", Text = "748 - Banco Cooperativo Sicredi S.A." },
                    new SelectListItem { Value = "12", Text = "721 - Banco Credibel S.A." },
                    new SelectListItem { Value = "12", Text = "222 - Banco Credit Agricole Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "505 - Banco Credit Suisse(Brasil) S.A." },
                    new SelectListItem { Value = "12", Text = "229 - Banco Cruzeiro do Sul S.A." },
                    new SelectListItem { Value = "12", Text = "Banco CSF S.A." },
                    new SelectListItem { Value = "12", Text = "003 - Banco da Amazônia S.A." },
                    new SelectListItem { Value = "12", Text = "083 - Banco da China Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "707 - Banco Daycoval S.A." },
                    new SelectListItem { Value = "12", Text = "Banco de Investimento Credit Suisse(Brasil) S.A." },
                    new SelectListItem { Value = "12", Text = "300 - Banco de La Nacion Argentina" },
                    new SelectListItem { Value = "12", Text = "495 - Banco de La Provincia de Buenos Aires" },
                    new SelectListItem { Value = "12", Text = "494 - Banco de La Republica Oriental del Uruguay" },
                    new SelectListItem { Value = "12", Text = "000 - Banco de Lage Landen Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "456 - Banco de Tokyo - Mitsubishi UFJ Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "214 - Banco Dibens S.A." },
                    new SelectListItem { Value = "12", Text = "001 - Banco do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "047 - Banco do Estado de Sergipe S.A." },
                    new SelectListItem { Value = "12", Text = "037 - Banco do Estado do Pará S.A." },
                    new SelectListItem { Value = "12", Text = "039 - Banco do Estado do Piauí S.A. - BEP" },
                    new SelectListItem { Value = "12", Text = "041 - Banco do Estado do Rio Grande do Sul S.A." },
                    new SelectListItem { Value = "12", Text = "004 - Banco do Nordeste do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "265 - Banco Fator S.A." },
                    new SelectListItem { Value = "12", Text = "224 - Banco Fibra S.A." },
                    new SelectListItem { Value = "12", Text = "626 - Banco Ficsa S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Fidis S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco Ford S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco GMAC S.A." },
                    new SelectListItem { Value = "12", Text = "612 - Banco Guanabara S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco Honda S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco IBM S.A." },
                    new SelectListItem { Value = "12", Text = "012 - Banco INBURSA de Investimentos S.A." },
                    new SelectListItem { Value = "12", Text = "604 - Banco Industrial do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "653 - Banco Indusval S.A." },
                    new SelectListItem { Value = "12", Text = "630 - Banco Intercap S.A." },
                    new SelectListItem { Value = "12", Text = "077 - Banco Intermedium S.A." },
                    new SelectListItem { Value = "12", Text = "249 - Banco Investcred Unibanco S.A." },
                    new SelectListItem { Value = "12", Text = "184 - Banco Itaú BBA S.A." },
                    new SelectListItem { Value = "12", Text = "029 - Banco Itaú BMG Consignado S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco Itaú Veículos S.A." },
                    new SelectListItem { Value = "12", Text = "479 - Banco ItauBank S.A" },
                    new SelectListItem { Value = "12", Text = "Banco Itaucard S.A." },
                    new SelectListItem { Value = "12", Text = "000 Banco Itaucred Financiamentos S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Itauleasing S.A." },
                    new SelectListItem { Value = "12", Text = "376 Banco J. P.Morgan S.A." },
                    new SelectListItem { Value = "12", Text = "074 Banco J. Safra S.A." },
                    new SelectListItem { Value = "12", Text = "217 Banco John Deere S.A." },
                    new SelectListItem { Value = "12", Text = "076 Banco KDB S.A." },
                    new SelectListItem { Value = "12", Text = "757 Banco KEB HANA do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Komatsu S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Losango S.A. - Banco Múltiplo" },
                    new SelectListItem { Value = "12", Text = "600 - Banco Luso Brasileiro S.A." },
                    new SelectListItem { Value = "12", Text = "072 - Banco Mais S.A." },
                    new SelectListItem { Value = "12", Text = "243 - Banco Máxima S.A." },
                    new SelectListItem { Value = "12", Text = "720 - Banco Maxinvest S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Mercantil de Investimentos S.A." },
                    new SelectListItem { Value = "12", Text = "389 - Banco Mercantil do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Mercedes - Benz do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "370 - Banco Mizuho do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "746 - Banco Modal S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco Moneo S.A." },
                    new SelectListItem { Value = "12", Text = "066 - Banco Morgan Stanley S.A." },
                    new SelectListItem { Value = "12", Text = "007 - Banco Nacional de Desenvolvimento Econômico e Social" },
                    new SelectListItem { Value = "12", Text = "735 - Banco Neon S.A." },
                    new SelectListItem { Value = "12", Text = "169 - Banco Olé Bonsucesso Consignado S.A." },
                    new SelectListItem { Value = "12", Text = "079 - Banco Original do Agronegócio S.A." },
                    new SelectListItem { Value = "12", Text = "212 - Banco Original S.A." },
                    new SelectListItem { Value = "12", Text = "712 - Banco Ourinvest S.A." },
                    new SelectListItem { Value = "12", Text = "623 - Banco PAN S.A." },
                    new SelectListItem { Value = "12", Text = "611 - Banco Paulista S.A." },
                    new SelectListItem { Value = "12", Text = "613 - Banco Pecúnia S.A." },
                    new SelectListItem { Value = "12", Text = "094 - Banco Petra S.A." },
                    new SelectListItem { Value = "12", Text = "643 - Banco Pine S.A." },
                    new SelectListItem { Value = "12", Text = "658 - Banco Porto Real de Investimentos S.A." },
                    new SelectListItem { Value = "12", Text = "724 - Banco Porto Seguro S.A." },
                    new SelectListItem { Value = "12", Text = "638 - Banco Prosper S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco PSA Finance Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "747 - Banco Rabobank International Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "088 - Banco Randon S.A." },
                    new SelectListItem { Value = "12", Text = "Banco RCI Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "633 - Banco Rendimento S.A." },
                    new SelectListItem { Value = "12", Text = "741 - Banco Ribeirão Preto S.A." },
                    new SelectListItem { Value = "12", Text = "120 - Banco Rodobens S.A." },
                    new SelectListItem { Value = "12", Text = "453 - Banco Rural S.A." },
                    new SelectListItem { Value = "12", Text = "422 - Banco Safra S.A." },
                    new SelectListItem { Value = "12", Text = "033 - Banco Santander (Brasil)S.A." },
                    new SelectListItem { Value = "12", Text = "743 - Banco Semear S.A." },
                    new SelectListItem { Value = "12", Text = "749 - Banco Simples S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Sistema S.A." },
                    new SelectListItem { Value = "12", Text = "366 - Banco Société Générale Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "637 - Banco Sofisa S.A." },
                    new SelectListItem { Value = "12", Text = "464 - Banco Sumitomo Mitsui Brasileiro S.A." },
                    new SelectListItem { Value = "12", Text = "082 - Banco Topázio S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco Toyota do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "634 - Banco Triângulo S.A." },
                    new SelectListItem { Value = "12", Text = "018 - Banco Tricury S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Vipal S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco Volkswagen S.A." },
                    new SelectListItem { Value = "12", Text = "000 - Banco Volvo Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "655 - Banco Votorantim S.A." },
                    new SelectListItem { Value = "12", Text = "610 - Banco VR S.A." },
                    new SelectListItem { Value = "12", Text = "119 - Banco Western Union do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "124 - Banco Woori Bank do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "Banco Yamaha Motor do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "Bancoob Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "Banestes Distribuidora de Títulos e Valores Mobiliários S/ A" },
                    new SelectListItem { Value = "12", Text = "021 BANESTES S.A.Banco do Estado do Espírito Santo" },
                    new SelectListItem { Value = "12", Text = "Banif Banco de Investimento(Brasil) S.A." },
                    new SelectListItem { Value = "12", Text = "719 Banif - Banco Internacional do Funchal(Brasil)S.A." },
                    new SelectListItem { Value = "12", Text = "755 Bank of America Merrill Lynch Banco Múltiplo S.A." },
                    new SelectListItem { Value = "12", Text = "744 BankBoston N.A." },
                    new SelectListItem { Value = "12", Text = "Banrisul S.A - Administradora de Consórcios" },
                    new SelectListItem { Value = "12", Text = "Banrisul S/ A Corretora de Valores Mobiliários e Câmbio" },
                    new SelectListItem { Value = "12", Text = "Barclays Corretora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Barigui Companhia Hipotecária" },
                    new SelectListItem { Value = "12", Text = "Barigui S.A.Crédito, Financiamento e Investimentos" },
                    new SelectListItem { Value = "12", Text = "BB Administradora de Consórcios S.A." },
                    new SelectListItem { Value = "12", Text = "BB Banco de Investimento S.A." },
                    new SelectListItem { Value = "12", Text = "BB Gestão de Recursos -Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "BB Leasing S / A Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "BBM Administração de Recursos Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "081 BBN Banco Brasileiro de Negócios S.A." },
                    new SelectListItem { Value = "12", Text = "250 BCV - Banco de Crédito e Varejo S.A." },
                    new SelectListItem { Value = "12", Text = "BEC Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "BEM Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "Besc Distribuidora de Títulos e Valores Mobiliários SA-Bescval" },
                    new SelectListItem { Value = "12", Text = "144 BEXS Banco de Câmbio S.A." },
                    new SelectListItem { Value = "12", Text = "Bexs Corretora de Câmbio S / A" },
                    new SelectListItem { Value = "12", Text = "BMC Asset Management Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "BMG Leasing S.A.Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "BMW Financeira S.A. - Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "BMW Leasing do Brasil S.A. - Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "017 BNY Mellon Banco S.A." },
                    new SelectListItem { Value = "12", Text = "BNY Mellon Serviços Financeiros Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "BR Consórcios Administradora de Consórcios LTDA" },
                    new SelectListItem { Value = "12", Text = "BR Partners Banco de Investimento S.A." },
                    new SelectListItem { Value = "12", Text = "BR Partners Corretora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Bradesco Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "Bradesco Leasing S.A.Arrecadamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "Bradesco S.A.Corretora de Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Bradesco - Kirton Corretora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "  BRAM Bradesco Asset S.A.Distribuidora de Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Brascan CIA. Hipotecária" },
                    new SelectListItem { Value = "12", Text = "Brasil Plural Corretora de Câmbio, Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "125 Brasil Plural S.A. - Banco Múltiplo" },
                    new SelectListItem { Value = "12", Text = "Brazilian Mortgages Companhia Hipotecária" },
                    new SelectListItem { Value = "12", Text = "070 BRB - Banco de Brasília S.A." },
                    new SelectListItem { Value = "12", Text = "BRB - Crédito, Financiamento e Investimento S.A." },
                    new SelectListItem { Value = "12", Text = "BRB - Distribuidora de Títulos e Valores Mobiliários SA" },
                    new SelectListItem { Value = "12", Text = "092 Brickell S.A.Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "BRKB Distribuidora de Títulos e Valores Mobiliário S.A." },
                    new SelectListItem { Value = "12", Text = "BRQualy Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "BTG Pactual Asset Management S.A.Distribuidora de Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "BTG Pactual Corretora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "BTG Pactual Serviços Financeiros S.A.Distribuidora de Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "BV Financeira S.A. - Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "BV Leasing - Arrendamento Mercantil S.A." },
                    new SelectListItem { Value = "12", Text = "Ca Indosuez Wealth(Brazil) S.A.Distribuidora de Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Caixa Consórcios S.A Administradora de Consórcios" },
                    new SelectListItem { Value = "12", Text = "104 Caixa Econômica Federal" },
                    new SelectListItem { Value = "12", Text = "CCB Brasil Arrendamento Mercantil S/ A" },
                    new SelectListItem { Value = "12", Text = "CCB Brasil Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "CCB Brasil S.A. - Crédito, Financiamentos e Investimentos" },
                    new SelectListItem { Value = "12", Text = "CDG Investimentos Corretora de Valores e Câmbio S.A." },
                    new SelectListItem { Value = "12", Text = "114 - 7   Central das Cooperativas de Economia e Crédito Mútuo do Estado do Espírito Santo Ltda." },
                    new SelectListItem { Value = "12", Text = "320 China Construction Bank(Brasil) Banco Múltiplo S.A." },
                    new SelectListItem { Value = "12", Text = "Cifra S.A.Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "Citibank Distribuidora de Títulos e Valores Mobiliários S.A" },
                    new SelectListItem { Value = "12", Text = "Citibank Leasing S.A - Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "477 Citibank N.A." },
                    new SelectListItem { Value = "12", Text = "Citigroup Global Markets Brasil, Corretora de Câmbio, Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Clear Corretora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "CM Capital Markets Corretora de Câmbio, Títulos e Valores Mobiliários LTDA" },
                    new SelectListItem { Value = "12", Text = "CM Capital Markets Distribuidora de Títulos e Valores Mobiliários LTDA" },
                    new SelectListItem { Value = "12", Text = "CNF - Administradora de Consórcios Nacional LTDA" },
                    new SelectListItem { Value = "12", Text = "Coin Distribuidora de Títulos e Valores Mobiliários LTDA" },
                    new SelectListItem { Value = "12", Text = "Coinvalores Corretora de Câmbio e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "Commerzbank Brasil S.A. - Banco Múltiplo" },
                    new SelectListItem { Value = "12", Text = "Companhia de Crédito, Financiamento e Investimento RCI Brasil" },
                    new SelectListItem { Value = "12", Text = "Companhia Hipotecária Piratini - CHP" },
                    new SelectListItem { Value = "12", Text = "Companhia Província de Crédito Imobiliário" },
                    new SelectListItem { Value = "12", Text = "CONBR Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "136 CONFEDERACAO NACIONAL DAS COOPERATIVAS CENTRAIS UNICREDS" },
                    new SelectListItem { Value = "12", Text = "Confidence Corretora de Câmbio S.A." },
                    new SelectListItem { Value = "12", Text = "Consórcio Nacional Volkswagen - Administradora de Consórcio LTDA." },
                    new SelectListItem { Value = "12", Text = "097 Cooperativa Central de Crédito Noroeste Brasileiro Ltda." },
                    new SelectListItem { Value = "12", Text = "085 - x   Cooperativa Central de Crédito Urbano - CECRED" },
                    new SelectListItem { Value = "12", Text = "099 - x   Cooperativa Central de Economia e Credito Mutuo das Unicreds" },
                    new SelectListItem { Value = "12", Text = "090 - 2   Cooperativa Central de Economia e Crédito Mutuo das Unicreds" },
                    new SelectListItem { Value = "12", Text = "  Cooperativa de Crédito Mútuo dos Despachantes de Trâns.Sta.Catarina" },
                    new SelectListItem { Value = "12", Text = "089 - 2   Cooperativa de Crédito Rural da Região de Mogiana" },
                    new SelectListItem { Value = "12", Text = "087 - 6   Cooperativa Unicred Central Santa Catarina" },
                    new SelectListItem { Value = "12", Text = "  Cotação Distribuidora de Títulos e Valores Mobiliários S.A" },
                    new SelectListItem { Value = "12", Text = "098 - 1   CREDIALIANÇA COOPERATIVA DE CRÉDITO RURAL" },
                    new SelectListItem { Value = "12", Text = "  Crediare S.A.Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = " Credit Suisse(Brasil) Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Credit Suisse (Brasil)S.A.Corretora de Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Credit Suisse Hedging - Griffo Corretora de Valores S.A" },
                    new SelectListItem { Value = "12", Text = "Dacasa Financeira S.A.- SOC.de Créd.Financ.e Investimento" },
                    new SelectListItem { Value = "12", Text = "Daycoval Leasing - Banco Múltiplo S.A." },
                    new SelectListItem { Value = "12", Text = "Deutsche Bank - Corretora de Valores S.A." },
                    new SelectListItem { Value = "12", Text = "487 Deutsche Bank S.A. - Banco Alemão" },
                    new SelectListItem { Value = "12", Text = "Dibens Leasing S.A.Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "Distribuidora Intercap de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Estrela Mineira Crédito, Financiamento e Investimento S / A" },
                    new SelectListItem { Value = "12", Text = "Everest Leasing S.A.Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "Fator S.A. - Corretora de Valores" },
                    new SelectListItem { Value = "12", Text = "Finamax S / A C.F.I." },
                    new SelectListItem { Value = "12", Text = "Financeira Alfa S.A.Crédito, Financiamento e Investimentos" },
                    new SelectListItem { Value = "12", Text = "Financeira Itaú CBD S.A.Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "Gazincred S.A.Sociedade de Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "Geração Futuro Corretora de Valores SA." },
                    new SelectListItem { Value = "12", Text = "Globo Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "Gmac Administradora de Consórcios LTDA" },
                    new SelectListItem { Value = "12", Text = "Golcred S / A - Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "064 Goldman Sachs do Brasil Banco Múltiplo S.A." },
                    new SelectListItem { Value = "12", Text = "Goldman Sachs do Brasil Corretora de Títulos e Valores Mobiliárias S.A." },
                    new SelectListItem { Value = "12", Text = "Guide Investimentos S.A. - Corretora de Valores" },
                    new SelectListItem { Value = "12", Text = "H H Picchioni S/ A Corretora de Câmbio e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "078 Haitong Banco de Investimento do Brasil S.A." },
                    new SelectListItem { Value = "12", Text = "Haitong do Brasil Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Haitong Securities do Brasil Corretora de Câmbio e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "062 Hipercard Banco Múltiplo S.A." },
                    new SelectListItem { Value = "12", Text = "Honda Leasing S.A. - Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "HS Administradora de Consórcios LTDA" },
                    new SelectListItem { Value = "12", Text = "HS Financeira S / A Crédito, Financiamento e Investimentos" },
                    new SelectListItem { Value = "12", Text = "  HSBC Brasil S.A. - Banco de Investimento" },
                    new SelectListItem { Value = "12", Text = "132 ICBC do Brasil Banco Múltiplo S.A." },
                    new SelectListItem { Value = "12", Text = "IclaTrust Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Industrial do Brasil Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "492 ING Bank N.V." },
                    new SelectListItem { Value = "12", Text = "ING Corretora de Câmbio e Títulos S.A." },
                    new SelectListItem { Value = "12", Text = "Intermedium Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "Intesa Sanpaolo Brasil S.A. - Banco Múltiplo" },
                    new SelectListItem { Value = "12", Text = "Intrag Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "Itabens Administradora de Consórcios LTDA" },
                    new SelectListItem { Value = "12", Text = "Itaú Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "Itaú Corretora de Valores S.A." },
                    new SelectListItem { Value = "12", Text = "Itaú Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "652 Itaú Unibanco Holding S.A." },
                    new SelectListItem { Value = "12", Text = "341 Itaú Unibanco S.A." },
                    new SelectListItem { Value = "12", Text = "Itaú Unibanco Veículos Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "Itauvest Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "J.Malucelli Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "J.Safra Corretora de Valores e Câmbio LTDA" },
                    new SelectListItem { Value = "12", Text = "J.P.Morgan Corretora de Câmbio e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "J.P.Morgan S.A.Distribuidora de Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "John Deere Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "488 JPMorgan Chase Bank, National Association" },
                    new SelectListItem { Value = "12", Text = "Kirton Administradora de Consórcio Ltda." },
                    new SelectListItem { Value = "12", Text = "Lecca Crédito, Financiamento e Investimento S / A" },
                    new SelectListItem { Value = "12", Text = "Lecca Distribuidora de Títulos e Valores Mobiliários LTDA" },
                    new SelectListItem { Value = "12", Text = "LLA - Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "Luizacred S.A.Sociedade de Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "Maggi Administradora de Consórcios LTDA" },
                    new SelectListItem { Value = "12", Text = "Mapfre Administradora de Consórcios S.A." },
                    new SelectListItem { Value = "12", Text = "Mapfre Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Massey Ferguson Administradora de Consórcios LTDA" },
                    new SelectListItem { Value = "12", Text = "Máxima S.A.Corretora de Câmbio, Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Mercantil do Brasil Corretora S.A. - Câmbio, Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Mercantil do Brasil Distribuidora S.A. - Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Mercantil do Brasil Financeira S.A - Crédito, Financiamento e Investimentos" },
                    new SelectListItem { Value = "12", Text = "Mercantil do Brasil Leasing S.A. - Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "Mercedes - Benz Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "Mercedes - Benz Leasing do Brasil Arrendamento Mercantil S.A." },
                    new SelectListItem { Value = "12", Text = "  Merrill Lynch S.A.Corretora de Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Microinvest S.A.Sociedade de Crédito ao Microempreendedor" },
                    new SelectListItem { Value = "12", Text = "Modal Distribuidora de Títulos e Valores Mobiliários LTDA" },
                    new SelectListItem { Value = "12", Text = "Morgan Stanley Corretora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "128 MS Bank S.A.Banco de Câmbio" },
                    new SelectListItem { Value = "12", Text = "014 Natixis Brasil S.A.Banco Múltiplo" },
                    new SelectListItem { Value = "12", Text = "753 Novo Banco Continental S.A. - Banco Múltiplo" },
                    new SelectListItem { Value = "12", Text = "086 - 8   OBOE Crédito Financiamento e Investimento S.A." },
                    new SelectListItem { Value = "12", Text = "  Omni S.A.Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "Omni SA Crédito Financiamento Investimento" },
                    new SelectListItem { Value = "12", Text = "Ourinvest Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "PAN Arrendamento Mercantil S.A." },
                    new SelectListItem { Value = "12", Text = "Panamericano Administradora de Consórcio LTDA" },
                    new SelectListItem { Value = "12", Text = "254 Paraná Banco S.A." },
                    new SelectListItem { Value = "12", Text = "PBM - Picchioni - Belgo - Mineira Distribuidora de Títulos e Valres Mobiliários S / A" },
                    new SelectListItem { Value = "12", Text = "Pernambucanas Financiadora S.A. - Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "Petra - Personal Trader Corretora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Pine Investimentos Distribuidoras de Títulos e Valores Mobiliários LTDA" },
                    new SelectListItem { Value = "12", Text = "Planner Corretora de Valores S.A." },
                    new SelectListItem { Value = "12", Text = "Planner Sociedade de Crédito ao Microempreendedor S.A." },
                    new SelectListItem { Value = "12", Text = "Planner Trustee Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "Ponta Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "Porto Seguro Administradora de Consórcios LTDA" },
                    new SelectListItem { Value = "12", Text = "Portobens Administradora de Consórcios LTDA" },
                    new SelectListItem { Value = "12", Text = "Portopar Distribuidora de Títulos e Valores Mobiliários LTDA" },
                    new SelectListItem { Value = "12", Text = "PortoSeg S.A. - Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "Positiva Corretora de Câmbio, Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "PSA Finance Arrendamento Mercantil S.A." },
                    new SelectListItem { Value = "12", Text = "Randon Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "Rodobens Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "Safra Leasing S.A.Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "Santana S.A.Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "Santander Brasil Administradora de Consórcio LTDA." },
                    new SelectListItem { Value = "12", Text = "Santander Brasil Asset Manag Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Santander Corretora de Câmbio e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Santander Leasing S.A.Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "Santander Securities Services Brasil Distribuidora de Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "Santinvest S.A - Crédito, Financiamento e Investimentos" },
                    new SelectListItem { Value = "12", Text = "Santinvest S.A - Distribuidora de Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Scania Administradora de Consórcios LTDA." },
                    new SelectListItem { Value = "12", Text = "Scania Banco S.A." },
                    new SelectListItem { Value = "12", Text = "751 Scotiabank Brasil S.A.Banco Múltiplo" },
                    new SelectListItem { Value = "12", Text = "SG Equipment Finance S.A.Arrendamento Mercantil" },
                    new SelectListItem { Value = "12", Text = "Sinosserra Administradora de Consórcios S / A." },
                    new SelectListItem { Value = "12", Text = "Sinosserra Financeira S/ A - Sociedade de Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "Societe Generale S.A.Corretora de Câmbio, Títulos e Valores Mobiliários" },
                    new SelectListItem { Value = "12", Text = "Socopa Sociedade Corretora Paulista S.A." },
                    new SelectListItem { Value = "12", Text = "Sofisa S.A.Crédito, Financiamento e Investimento" },
                    new SelectListItem { Value = "12", Text = "118 Standard Chartered Bank(Brasil) S / A–Bco Invest." },
                    new SelectListItem { Value = "12", Text = "Sul Financeira S / A - Crédito, Financiamentos e Investimentos" },
                    new SelectListItem { Value = "12", Text = "  Tibre Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "129 UBS Brasil Banco de Investimento S.A." },
                    new SelectListItem { Value = "12", Text = "UBS Brasil Corretora de Câmbio, Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "409 UNIBANCO - União de Bancos Brasileiros S.A." },
                    new SelectListItem { Value = "12", Text = "230 Unicard Banco Múltiplo S.A." },
                    new SelectListItem { Value = "12", Text = "091 - 4   Unicred Central do Rio Grande do Sul" },
                    new SelectListItem { Value = "12", Text = "  Uniletra Corretora de Câmbio, Títulos e Valores Mobiliários S.A." },
                    new SelectListItem { Value = "12", Text = "084 Uniprime Norte do Paraná - Coop de Economia e Crédito Mútuo dos Médicos, Profissionais das Ciências" },
                    new SelectListItem { Value = "12", Text = "Valtra Administradora de Consórcios LTDA" },
                    new SelectListItem { Value = "12", Text = "Volvo Administradora de Consórcio LTDA." },
                    new SelectListItem { Value = "12", Text = "Votorantim - Corretora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "Votorantim Asset Management Distribuidora de Títulos e Valores Mobiliários LTDA" },
                    new SelectListItem { Value = "12", Text = "VR Distribuidora de Títulos e Valores Mobiliários LTDA." },
                    new SelectListItem { Value = "12", Text = "Western Union Corretora de Câmbio S.A." },
                    new SelectListItem { Value = "12", Text = "XP Investimentos Corretora de Câmbio, Títulos e Valores Mobiliários S / A" },
                    new SelectListItem { Value = "12", Text = "Yamaha Administradora de Consórcio LTDA." },
                    new SelectListItem { Value = "12", Text = "Zema Administradora de Consórcio LTDA" }
                };
            }
        }

        public static List<BancoVM> ToViewModel(List<Banco> bancos)
        {
            return Mapper.Map<List<BancoVM>>(bancos);
        }

        public static List<Banco> ToModelList(List<BancoVM> viewModel, int id)
        {
            foreach (var item in viewModel)
            {
                item.IdFichaCadastral = id;
            }
            return Mapper.Map<List<Banco>>(viewModel);
        }
    }
    public class ContatoVM
    {
        public int Id { get; set; }

        public int IdFichaCadastral { get; set; }

        [DisplayName("Nome Contato")]
        public string NomeContato { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [DisplayName("Celular")]
        public string Celular { get; set; }

        public static List<Contato> ToModelList(List<ContatoVM> viewModel, int id)
        {
            foreach (var item in viewModel)
            {
                item.IdFichaCadastral = id;
            }
            return Mapper.Map<List<Contato>>(viewModel);
        }

        public static List<ContatoVM> ToViewModel(List<Contato> contatos)
        {
            return Mapper.Map<List<ContatoVM>>(contatos);
        }
    }
}