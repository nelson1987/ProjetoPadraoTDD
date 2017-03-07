using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.Controllers;
using WebForLink.Web.Resources;

namespace WebForLink.Web.ViewModels
{
    public class FichaCadastralTesteVM
    {
        public FichaCadastralTesteVM()
        {
            EnderecoList = new List<EnderecoTesteVM>();
            ContatoList = new List<ContatoTesteVM>();
            BancarioList = new List<BancarioTesteVM>();
            ServicoList = new List<ServicoTesteVM>();
            MaterialList = new List<MaterialTesteVM>();

        }
        public DadosGeraisTesteVM DadosGerais { get; set; }
        public List<EnderecoTesteVM> EnderecoList { get; set; }
        public List<ContatoTesteVM> ContatoList { get; set; }
        public List<BancarioTesteVM> BancarioList { get; set; }
        public List<ServicoTesteVM> ServicoList { get; set; }
        public List<MaterialTesteVM> MaterialList { get; set; }
        public RoboReceitaTesteVM RoboReceita { get; set; }
        public RoboSuframaTesteVM RoboSuframa { get; private set; }
        public RoboCorreiosTesteVM RoboCorreios { get; private set; }
        public RoboSimplesNacionalTesteVM RoboSimplesNacional { get; private set; }
        public RoboSintegraTesteVM RoboSintegra { get; private set; }

        public static FichaCadastralTesteVM ViewModelToView(WFD_CONTRATANTE_PJPF model)
        {
            Contratante contratante = model.WFD_CONTRATANTE;
            Fornecedor fornecedor = contratante.WFD_PJPF.FirstOrDefault();
            var ficha = new FichaCadastralTesteVM();
            //Dados Gerais
            ficha.DadosGerais = DadosGeraisTesteVM.ModelToViewModel(contratante, fornecedor);
            //Enderecos
            if (fornecedor.ROBO != null)
            {
                ficha.EnderecoList.Add(EnderecoTesteVM.ModelToViewModel(fornecedor.ROBO));
                ficha.RoboReceita = RoboReceitaTesteVM.ModelToViewModel(fornecedor.ROBO);
                ficha.RoboSintegra = RoboSintegraTesteVM.ModelToViewModel(fornecedor.ROBO);
                ficha.RoboSimplesNacional = RoboSimplesNacionalTesteVM.ModelToViewModel(fornecedor.ROBO);
                ficha.RoboCorreios = RoboCorreiosTesteVM.ModelToViewModel(fornecedor.ROBO);
                ficha.RoboSuframa = RoboSuframaTesteVM.ModelToViewModel(fornecedor.ROBO);
            }
            if (model.WFD_PJPF_ENDERECO.Any())
                ficha.EnderecoList.AddRange(EnderecoTesteVM.ModelToViewModel(model.WFD_PJPF_ENDERECO));
            //Contatos
            if (model.WFD_PJPF_CONTATOS.Any())
                ficha.ContatoList = ContatoTesteVM.ModelToViewModel(model.WFD_PJPF_CONTATOS);
            //Banco
            if (model.BancoDoFornecedor.Any())
                ficha.BancarioList = BancarioTesteVM.ModelToViewModel(model.BancoDoFornecedor);

            //Serviços
            var servicos = fornecedor.FornecedorServicoMaterialList.Where(x => x.T_UNSPSC.UNSPSC_COD > 70000000);
            if (servicos.Any())
                ficha.ServicoList = ServicoTesteVM.ViewToViewModel(servicos.OrderBy(x => x.T_UNSPSC.UNSPSC_DSC).ToList());

            //Materiais
            var materiais = fornecedor.FornecedorServicoMaterialList.Where(x => x.T_UNSPSC.UNSPSC_COD < 70000000);
            if (materiais.Any())
                ficha.MaterialList = MaterialTesteVM.ViewToViewModel(materiais.OrderBy(x=>x.T_UNSPSC.UNSPSC_DSC).ToList());

            return ficha;
        }
    }
    public class EnderecoTesteVM
    {
        public EnderecoTesteVM()
        {

        }

        public int Id { get; set; }
        public bool EnderecoReceita { get; set; }
        public int ContratantePjpfId { get; set; }
        [Display(Name = "Endereco", ResourceType = typeof(Language))]
        public string Endereco { get; set; }
        [Display(Name = "Numero", ResourceType = typeof(Language))]
        public string Numero { get; set; }
        [Display(Name = "Complemento", ResourceType = typeof(Language))]
        public string Complemento { get; set; }
        [Display(Name = "CEP", ResourceType = typeof(Language))]
        public string CEP { get; set; }
        [Display(Name = "Bairro", ResourceType = typeof(Language))]
        public string Bairro { get; set; }
        [Display(Name = "Cidade", ResourceType = typeof(Language))]
        public string Cidade { get; set; }
        [Display(Name = "UF", ResourceType = typeof(Language))]
        public string UF { get; set; }
        [Display(Name = "Pais", ResourceType = typeof(Language))]
        public string Pais { get; set; }
        [Display(Name = "Latitude", ResourceType = typeof(Language))]
        public string Latitude { get; set; }
        [Display(Name = "Longitude", ResourceType = typeof(Language))]
        public string Longitude { get; set; }

        public List<EnderecoTesteVM> Grid { get; set; }

        public static EnderecoTesteVM ViewToViewModel(FORNECEDOR_ENDERECO model)
        {
            return new EnderecoTesteVM()
            {
                Bairro = model.BAIRRO,
                Cidade = model.CIDADE,
                CEP = model.CEP,
                Complemento = model.COMPLEMENTO,
                ContratantePjpfId = model.CONTRATANTE_PJPF_ID,
                Endereco = model.ENDERECO,
                Id = model.ID,
                Numero = model.NUMERO,
                Pais = model.PAIS,
                UF = model.UF,
                EnderecoReceita = false,
                Longitude = model.LONGITUDE,
                Latitude = model.LATITUDE
            };
        }
        public static EnderecoTesteVM ModelToViewModel(ROBO model)
        {
            return new EnderecoTesteVM()
            {
                Bairro = model.RF_BAIRRO,
                Cidade = model.RF_MUNICIPIO,
                CEP = model.RF_CEP,
                Complemento = model.RF_COMPLEMENTO,
                Endereco = model.RF_LOGRADOURO,
                Id = model.ID,
                Numero = model.RF_NUMERO,
                Pais = "Brasil",
                UF = model.RF_UF,
                EnderecoReceita = true,
                Longitude = model.LONGITUDE,
                Latitude = model.LATITUDE
            };
        }
        public static List<EnderecoTesteVM> ModelToViewModel(ICollection<FORNECEDOR_ENDERECO> model)
        {
            List<EnderecoTesteVM> viewModel = new List<EnderecoTesteVM>();
            foreach (FORNECEDOR_ENDERECO item in model)
            {
                viewModel.Add(EnderecoTesteVM.ViewToViewModel(item));
            }
            return viewModel;
        }
        public static BoxesTesteVM stringToVieModel(string modelo, UrlHelper url, List<EnderecoTesteVM> enderecos)
        {
            return new BoxesTesteVM
            {
                NomeBox = string.Format("Dados {0}", modelo),
                Validation = string.Format("Dados{0}Validation", modelo),
                Collapse = string.Format("dados{0}Collapse", modelo),
                Excluir = string.Format("{0}Excluir", modelo),
                Div = string.Format("div{0}", modelo),
                DivInterna = string.Format("Dados{0}", modelo),
                MensagemInclusao = string.Format("Adicionar Dados de {0}", modelo),
                Validar = string.Format("validaSalvar{0}", modelo),
                DivConfirmacao = string.Format("Confirmacao{0}", modelo),
                BotaoSim = string.Format("btnSim{0}", modelo),
                BotaoNao = string.Format("btnNao{0}", modelo),
                UrlIncluir = url.Action("Incluir", "{0}"),
                UrlCancelar = url.Action("Cancelar", "{0}"),
            };
        }
    }
    public class DadosGeraisTesteVM
    {
        public DadosGeraisTesteVM()
        {
        }

        [Display(Name = "Documento", ResourceType = typeof(Language))]
        public string Documento { get; set; }
        [Display(Name = "RazaoSocial", ResourceType = typeof(Language))]
        public string RazaoSocial { get; set; }
        [Display(Name = "NomeFantasia", ResourceType = typeof(Language))]
        public string NomeFantasia { get; set; }
        [Display(Name = "InscricaoEstadual", ResourceType = typeof(Language))]
        [Required]
        public string InscricaoEstadual { get; set; }
        [Display(Name = "InscricaoMunicipal", ResourceType = typeof(Language))]
        public string InscricaoMunicipal { get; set; }

        public static DadosGeraisTesteVM ViewToViewModel(Fornecedor model)
        {
            return new DadosGeraisTesteVM()
            {
                Documento = model.CNPJ ?? model.CPF,
                RazaoSocial = model.RAZAO_SOCIAL ?? model.NOME,
                InscricaoEstadual = model.INSCR_ESTADUAL,
                InscricaoMunicipal = model.INSCR_MUNICIPAL,
                NomeFantasia = model.NOME_FANTASIA
            };
        }
        public static DadosGeraisTesteVM ModelToViewModel(Contratante model, Fornecedor fornecedor)
        {
            return new DadosGeraisTesteVM()
            {
                Documento = model.CNPJ,
                RazaoSocial = model.RAZAO_SOCIAL,
                NomeFantasia = model.NOME_FANTASIA,
                InscricaoEstadual = fornecedor.INSCR_ESTADUAL,
                InscricaoMunicipal = fornecedor.INSCR_MUNICIPAL
            };
        }
    }
    public class ContatoTesteVM
    {
        public ContatoTesteVM()
        {

        }

        public int Id { get; set; }
        public int ContratantePjPfId { get; set; }
        [Display(Name = "Nome", ResourceType = typeof(Language))]
        public string Nome { get; set; }
        [Display(Name = "Email", ResourceType = typeof(Language))]
        public string Email { get; set; }
        [Display(Name = "Telefone", ResourceType = typeof(Language))]
        public string Telefone { get; set; }
        [Display(Name = "Celular", ResourceType = typeof(Language))]
        public string Celular { get; set; }

        public static ContatoTesteVM ViewToViewModel(FORNECEDOR_CONTATOS model)
        {
            return new ContatoTesteVM()
            {
                Id = model.ID,
                Nome = model.NOME,
                Email = model.EMAIL,
                Telefone = model.TELEFONE,
                Celular = model.CELULAR,
                ContratantePjPfId = model.CONTRATANTE_PJPF_ID.Value
            };
        }
        public static List<ContatoTesteVM> ModelToViewModel(ICollection<FORNECEDOR_CONTATOS> model)
        {
            List<ContatoTesteVM> viewModel = new List<ContatoTesteVM>();
            foreach (FORNECEDOR_CONTATOS item in model)
            {
                viewModel.Add(ContatoTesteVM.ViewToViewModel(item));
            }
            return viewModel;
        }
    }
    public class BancarioTesteVM
    {
        public BancarioTesteVM()
        {

        }

        public int Id { get; set; }
        public int BancoId { get; set; }
        [Display(Name = "Agencia", ResourceType = typeof(Language))]
        public string Agencia { get; set; }
        [Display(Name = "AgenciaDigitoVerificador", ResourceType = typeof(Language))]
        public string AgenciaDigitoVerificador { get; set; }
        [Display(Name = "Conta", ResourceType = typeof(Language))]
        public string Conta { get; set; }
        [Display(Name = "ContaDigitoVerificador", ResourceType = typeof(Language))]
        public string ContaDigitoVerificador { get; set; }
        public bool Ativo { get; set; }
        public int ContratantePjPfId { get; set; }
        public int ArquivoId { get; set; }
        public string NomeArquivo { get; set; }

        public static BancarioTesteVM ViewToViewModel(BancoDoFornecedor model)
        {
            return new BancarioTesteVM()
            {
                Id = model.ID,
                BancoId = model.BANCO_ID,
                Agencia = model.AGENCIA,
                AgenciaDigitoVerificador = model.AG_DV,
                Conta = model.CONTA,
                ContaDigitoVerificador = model.CONTA_DV,
                Ativo = model.ATIVO,
                ContratantePjPfId = model.CONTRATANTE_PJPF_ID ?? 0,
                ArquivoId = model.ARQUIVO_ID ?? 0,
                NomeArquivo = model.NOME_ARQUIVO,
            };
        }
        public static List<BancarioTesteVM> ModelToViewModel(ICollection<BancoDoFornecedor> model)
        {
            List<BancarioTesteVM> viewModel = new List<BancarioTesteVM>();
            foreach (BancoDoFornecedor item in model)
            {
                viewModel.Add(BancarioTesteVM.ViewToViewModel(item));
            }
            return viewModel;
        }
    }
    public class ServicoTesteVM
    {
        public ServicoTesteVM()
        {

        }

        public string Descricao { get; private set; }
        public int Id { get; private set; }
        public int? UNSPSC_ID { get; private set; }

        public static ServicoTesteVM ViewToViewModel(FORNECEDOR_UNSPSC model)
        {
            return new ServicoTesteVM()
            {
                Id = model.ID,
                UNSPSC_ID = model.UNSPSC_ID,
                Descricao = model.T_UNSPSC.UNSPSC_DSC
            };
        }
        public static List<ServicoTesteVM> ViewToViewModel(ICollection<FORNECEDOR_UNSPSC> model)
        {
            List<ServicoTesteVM> viewModel = new List<ServicoTesteVM>();
            foreach (FORNECEDOR_UNSPSC item in model)
            {
                viewModel.Add(ServicoTesteVM.ViewToViewModel(item));
            }
            return viewModel;
        }
    }
    public class MaterialTesteVM
    {
        public MaterialTesteVM()
        {

        }

        public string Descricao { get; private set; }
        public int Id { get; private set; }
        public int? UNSPSC_ID { get; private set; }

        public static MaterialTesteVM ViewToViewModel(FORNECEDOR_UNSPSC model)
        {
            return new MaterialTesteVM()
            {
                Id = model.ID,
                UNSPSC_ID = model.UNSPSC_ID,
                Descricao = model.T_UNSPSC.UNSPSC_DSC
            };
        }
        public static List<MaterialTesteVM> ViewToViewModel(ICollection<FORNECEDOR_UNSPSC> model)
        {
            List<MaterialTesteVM> viewModel = new List<MaterialTesteVM>();
            foreach (FORNECEDOR_UNSPSC item in model)
            {
                viewModel.Add(MaterialTesteVM.ViewToViewModel(item));
            }
            return viewModel;
        }
    }
    public class RoboReceitaTesteVM
    {
        public RoboReceitaTesteVM()
        {

        }

        public int Id { get; set; }
        public DateTime ExecucaoRobo { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Nome { get; set; }
        //public string Endereco { get; set; }
        //public string Numero { get; set; }
        //public string Complemento { get; set; }
        //public string Bairro { get; set; }
        //public string Cidade { get; set; }
        //public string Estado { get; set; }
        //public string Cep { get; set; }
        public string SituacaoCadastral { get; set; }
        public DateTime? DataSituacaoCadastral { get; set; }
        public string SituacaoEspecial { get; set; }
        public DateTime? DataSituacaoEspecial { get; set; }
        public string MotivoSituacaoCadastral { get; set; }
        public DateTime? DataDeAbertura { get; set; }
        public string CnaePrincipal { get; set; }
        public string CnaeDescricaoPrincipal { get; set; }
        public string CnaeOutros { get; set; }
        public string CnaeOutrosDescricao { get; set; }
        public string MatrizFilial { get; set; }
        public string CodigoNaturezaJuridica { get; set; }
        public string DescricaoNaturezaJuridica { get; set; }
        public DateTime? Consulta { get; set; }
        public string CertificadoHTML { get; set; }
        public int? RF_CONTADOR_TENTATIVA { get; set; }
        public int? RF_CODE_ROBO { get; set; }
        public string IBGE_COD { get; set; }
        public static RoboReceitaTesteVM ModelToViewModel(ROBO model)
        {
            return new RoboReceitaTesteVM()
            {
                Id = model.ID,
                ExecucaoRobo = model.ROBO_DT_EXEC,
                CPF = model.CPF,
                CNPJ = model.CNPJ,
                RazaoSocial = model.RECEITA_FEDERAL_RAZAO_SOCIAL,
                IBGE_COD = model.IBGE_COD,
                NomeFantasia = model.RF_NOME_FANTASIA,
                Nome = model.RF_NOME,
                //Endereco = model.RF_LOGRADOURO,
                //Numero = model.RF_NUMERO,
                //Complemento = model.RF_COMPLEMENTO,
                //Bairro = model.RF_BAIRRO,
                //Cidade = model.RF_MUNICIPIO,
                //Estado = model.RF_UF,
                //Cep = model.RF_CEP,
                SituacaoCadastral = model.RF_SIT_CADASTRAL_CNPJ,
                DataSituacaoCadastral = model.RF_SIT_CADSTRAL_CNPJ_DT,
                SituacaoEspecial = model.RF_SIT_ESPECIAL_CNPJ,
                DataSituacaoEspecial = model.RF_SIT_ESPECIAL_CNPJ_DT,
                MotivoSituacaoCadastral = model.RF_MOTIVO_CNPJ_SITU_CADASTRAL,
                DataDeAbertura = model.RF_CNPJ_DT_ABERTURA,
                CnaePrincipal = model.RF_CNAE_COD_PRINCIPAL,
                CnaeDescricaoPrincipal = model.RF_CNAE_DSC_PRINCIPAL,
                CnaeOutros = model.RF_CNAE_COD_OUTROS,
                CnaeOutrosDescricao = model.RF_CNAE_DSC_OUTROS,
                MatrizFilial = model.RF_MATRIZ_FILIAL,
                CodigoNaturezaJuridica = model.RF_COD_NATUREZA_JURIDICA,
                DescricaoNaturezaJuridica = model.RF_DSC_NATUREZA_JURIDICA,
                Consulta = model.RF_CONSULTA_DTHR,
                CertificadoHTML = model.RF_CERTIFICADO_HTML,
                RF_CONTADOR_TENTATIVA = model.RF_CONTADOR_TENTATIVA,
                RF_CODE_ROBO = model.RF_CODE_ROBO
            };
        }
    }
    public class RoboSintegraTesteVM
    {
        public int ID { get; set; }
        public DateTime ROBO_DT_EXEC { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string RECEITA_FEDERAL_RAZAO_SOCIAL { get; set; }
        public string SINTEGRA_ERRO_ORIGINAL { get; set; }
        public string SINT_IE_QTD { get; set; }
        public string SINT_IE_MULTIPLA { get; set; }
        public string SINT_IE_MULTIPLA_CODIGOS { get; set; }
        public string SINT_IE_MULTIPLA_SITUACAO { get; set; }
        public string SINT_IE_COD { get; set; }
        public DateTime? SINT_CONSULTA_DTHR { get; set; }
        public string SINT_IE_SITU_CADASTRAL { get; set; }
        public string SINT_IE_SITU_CADSTRAL_DT { get; set; }
        public DateTime? SINT_INCLUSAO_DT { get; set; }
        public DateTime? SINT_BAIXA_DT { get; set; }
        public string SINT_BAIXA_MOTIVO { get; set; }
        public string SINT_EMAIL { get; set; }
        public string SINT_REGIME_APURACAO { get; set; }
        public string SINT_ENQUADRAMENTO_FISCAL { get; set; }
        public string SINT_TEL { get; set; }
        public string SINT_CAD_PROD_RURAL { get; set; }
        public string SINT_COMPLEMENTO { get; set; }
        public string SINT_RAZAO_SOCIAL { get; set; }
        public string SINT_CNPJ { get; set; }
        public string SINT_BAIRRO { get; set; }
        public string SINT_LOGRADOURO { get; set; }
        public string SINT_NUMERO { get; set; }
        public string SINT_CEP { get; set; }
        public string SINT_MUNICIPIO { get; set; }
        public string SINT_UF { get; set; }
        public string SINT_ATIVIDADE_PRINCIPAL { get; set; }
        public string SINT_CERTIFICADO_HTML { get; set; }
        public int? SINT_CONTADOR_TENTATIVA { get; set; }
        public int? SINT_CODE_ROBO { get; set; }
        public static RoboSintegraTesteVM ModelToViewModel(ROBO model)
        {
            return new RoboSintegraTesteVM()
            {
                ID = model.ID,
                ROBO_DT_EXEC = model.ROBO_DT_EXEC,
                CPF = model.CPF,
                CNPJ = model.CNPJ,
                RECEITA_FEDERAL_RAZAO_SOCIAL = model.RECEITA_FEDERAL_RAZAO_SOCIAL,
                SINTEGRA_ERRO_ORIGINAL = model.SINTEGRA_ERRO_ORIGINAL,
                SINT_IE_QTD = model.SINT_IE_QTD,
                SINT_IE_MULTIPLA = model.SINT_IE_MULTIPLA,
                SINT_IE_MULTIPLA_CODIGOS = model.SINT_IE_MULTIPLA_CODIGOS,
                SINT_IE_MULTIPLA_SITUACAO = model.SINT_IE_MULTIPLA_SITUACAO,
                SINT_IE_COD = model.SINT_IE_COD,
                SINT_CONSULTA_DTHR = model.SINT_CONSULTA_DTHR,
                SINT_IE_SITU_CADASTRAL = model.SINT_IE_SITU_CADASTRAL,
                SINT_IE_SITU_CADSTRAL_DT = model.SINT_IE_SITU_CADSTRAL_DT,
                SINT_INCLUSAO_DT = model.SINT_INCLUSAO_DT,
                SINT_BAIXA_DT = model.SINT_BAIXA_DT,
                SINT_BAIXA_MOTIVO = model.SINT_BAIXA_MOTIVO,
                SINT_EMAIL = model.SINT_EMAIL,
                SINT_REGIME_APURACAO = model.SINT_REGIME_APURACAO,
                SINT_ENQUADRAMENTO_FISCAL = model.SINT_ENQUADRAMENTO_FISCAL,
                SINT_TEL = model.SINT_TEL,
                SINT_CAD_PROD_RURAL = model.SINT_CAD_PROD_RURAL,
                SINT_COMPLEMENTO = model.SINT_COMPLEMENTO,
                SINT_RAZAO_SOCIAL = model.SINT_RAZAO_SOCIAL,
                SINT_CNPJ = model.SINT_CNPJ,
                SINT_BAIRRO = model.SINT_BAIRRO,
                SINT_LOGRADOURO = model.SINT_LOGRADOURO,
                SINT_NUMERO = model.SINT_NUMERO,
                SINT_CEP = model.SINT_CEP,
                SINT_MUNICIPIO = model.SINT_MUNICIPIO,
                SINT_UF = model.SINT_UF,
                SINT_ATIVIDADE_PRINCIPAL = model.SINT_ATIVIDADE_PRINCIPAL,
                SINT_CERTIFICADO_HTML = model.SINT_CERTIFICADO_HTML,
                SINT_CONTADOR_TENTATIVA = model.SINT_CONTADOR_TENTATIVA,
                SINT_CODE_ROBO = model.SINT_CODE_ROBO,
            };
        }
    }
    public class RoboSimplesNacionalTesteVM
    {
        public int ID { get; set; }
        public DateTime ROBO_DT_EXEC { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string RECEITA_FEDERAL_RAZAO_SOCIAL { get; set; }
        public string SIMPLES_NACIONAL_SITUACAO { get; set; }
        public string SN_SITUACAO_SIMEI { get; set; }
        public string SN_PERIODOS_ANTERIORES { get; set; }
        public string SN_SIMEI_PERIODOS_ANTERIORES { get; set; }
        public string SN_AGENDAMENTOS { get; set; }
        public string SN_RAZAO_SOCIAL { get; set; }
        public DateTime? SN_CONSULTA_DTHR { get; set; }
        public int? SN_CONTADOR_TENTATIVA { get; set; }
        public int? SN_CODE_ROBO { get; set; }
        public static RoboSimplesNacionalTesteVM ModelToViewModel(ROBO model)
        {
            return new RoboSimplesNacionalTesteVM()
            {
                ID = model.ID,
                ROBO_DT_EXEC = model.ROBO_DT_EXEC,
                CPF = model.CPF,
                CNPJ = model.CNPJ,
                RECEITA_FEDERAL_RAZAO_SOCIAL = model.RECEITA_FEDERAL_RAZAO_SOCIAL,
                SIMPLES_NACIONAL_SITUACAO = model.SIMPLES_NACIONAL_SITUACAO,
                SN_SITUACAO_SIMEI = model.SN_SITUACAO_SIMEI,
                SN_PERIODOS_ANTERIORES = model.SN_PERIODOS_ANTERIORES,
                SN_SIMEI_PERIODOS_ANTERIORES = model.SN_SIMEI_PERIODOS_ANTERIORES,
                SN_AGENDAMENTOS = model.SN_AGENDAMENTOS,
                SN_RAZAO_SOCIAL = model.SN_RAZAO_SOCIAL,
                SN_CONSULTA_DTHR = model.SN_CONSULTA_DTHR,
                SN_CONTADOR_TENTATIVA = model.SN_CONTADOR_TENTATIVA,
                SN_CODE_ROBO = model.SN_CODE_ROBO,
            };
        }
    }
    public class RoboCorreiosTesteVM
    {
        public int ID { get; set; }
        public DateTime ROBO_DT_EXEC { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string RECEITA_FEDERAL_RAZAO_SOCIAL { get; set; }
        public string CORREIOS_TP_LOGRADOURO { get; set; }
        public string CORR_LOGRADOURO { get; set; }
        public string CORR_COMPLEMENTO { get; set; }
        public string CORR_BAIRRO { get; set; }
        public string CORR_BAIRRO_COMPL { get; set; }
        public string CORR_UF { get; set; }
        public string CORR_MUNICIPIO { get; set; }
        public string CORR_CEP { get; set; }
        public DateTime? CORR_CONSULTA_DTHR { get; set; }
        public int? CORR_CONTADOR_TENTATIVA { get; set; }
        public string CORR_CERTIFICADO_HTML { get; set; }
        public static RoboCorreiosTesteVM ModelToViewModel(ROBO model)
        {
            return new RoboCorreiosTesteVM()
            {
                ID = model.ID,
                ROBO_DT_EXEC = model.ROBO_DT_EXEC,
                CPF = model.CPF,
                CNPJ = model.CNPJ,
                RECEITA_FEDERAL_RAZAO_SOCIAL = model.RECEITA_FEDERAL_RAZAO_SOCIAL,
                CORREIOS_TP_LOGRADOURO = model.CORREIOS_TP_LOGRADOURO,
                CORR_LOGRADOURO = model.CORR_LOGRADOURO,
                CORR_COMPLEMENTO = model.CORR_COMPLEMENTO,
                CORR_BAIRRO = model.CORR_BAIRRO,
                CORR_BAIRRO_COMPL = model.CORR_BAIRRO_COMPL,
                CORR_UF = model.CORR_UF,
                CORR_MUNICIPIO = model.CORR_MUNICIPIO,
                CORR_CEP = model.CORR_CEP,
                CORR_CONSULTA_DTHR = model.CORR_CONSULTA_DTHR,
                CORR_CONTADOR_TENTATIVA = model.CORR_CONTADOR_TENTATIVA,
                CORR_CERTIFICADO_HTML = model.CORR_CERTIFICADO_HTML,
            };
        }
    }
    public class RoboSuframaTesteVM
    {
        public static RoboSuframaTesteVM ModelToViewModel(ROBO model)
        {
            return new RoboSuframaTesteVM()
            {
                ID = model.ID,
                ROBO_DT_EXEC = model.ROBO_DT_EXEC,
                CPF = model.CPF,
                CNPJ = model.CNPJ,
                RECEITA_FEDERAL_RAZAO_SOCIAL = model.RECEITA_FEDERAL_RAZAO_SOCIAL,
                SUFRAMA_ERRO_MENSAGEM = model.SUFRAMA_ERRO_MENSAGEM,
                SUFRAMA_SIT_CADASTRAL = model.SUFRAMA_SIT_CADASTRAL,
                SUFRAMA_INSCRICAO = model.SUFRAMA_INSCRICAO,
                SUFRAMA_TEL = model.SUFRAMA_TEL,
                SUFRAMA_SIT_CADASTRAL_VALIDADE = model.SUFRAMA_SIT_CADASTRAL_VALIDADE,
                SUFRAMA_INCENTIVOS = model.SUFRAMA_INCENTIVOS,
                SUFRAMA_EMAIL = model.SUFRAMA_EMAIL,
                SUFRAMA_CONSULTA_DTHR = model.SUFRAMA_CONSULTA_DTHR,
                SUFRAMA_CONTADOR_TENTATIVA = model.SUFRAMA_CONTADOR_TENTATIVA,
                SUFRAMA_CERTIFICADO_HTML = model.SUFRAMA_CERTIFICADO_HTML
            };
        }
        public int ID { get; set; }
        public DateTime ROBO_DT_EXEC { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string RECEITA_FEDERAL_RAZAO_SOCIAL { get; set; }
        public string SUFRAMA_ERRO_MENSAGEM { get; set; }
        public string SUFRAMA_SIT_CADASTRAL { get; set; }
        public string SUFRAMA_INSCRICAO { get; set; }
        public string SUFRAMA_TEL { get; set; }
        public DateTime? SUFRAMA_SIT_CADASTRAL_VALIDADE { get; set; }
        public string SUFRAMA_INCENTIVOS { get; set; }
        public string SUFRAMA_EMAIL { get; set; }
        public DateTime? SUFRAMA_CONSULTA_DTHR { get; set; }
        public int? SUFRAMA_CONTADOR_TENTATIVA { get; set; }
        public string SUFRAMA_CERTIFICADO_HTML { get; set; }
    }
}