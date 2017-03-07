using AutoMapper;
using System;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.OCP;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Mappers
{
    public class RefactorMappingProfile : Profile
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string ProfileName
        {
            get { return "RefactorMappingProfile"; }
        }
        public RefactorMappingProfile()
        {

        //}
        //protected override void Configure()
        //{
            try
            {
                MapeamentoConversores();
                //Mapper.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        public void MapeamentoConversores()
        {
            MapearPaginaFichaCadastral();
            //-- Solicitação partial
            MapearSolicitacaoPartial();
            //--Órgão Público Partial
            MapearOrgaosPublicoPartial();
            //--
            MapearSolicitacaoCadastroFornecedor();

            MapearEnderecos();

            MapearBancos();

            MapearContatos();

            MapearSolicitacaoDocumento();

            MapearUnspsc();

            MapearInformacaoComplementar();

            CreateMap<Fornecedor, FichaCadastralWebForLinkVM>()
                .ForMember(dest => dest.CNPJ_CPF, ori => ori.MapFrom(x => x.TIPO_PJPF_ID == 3
                    ? Convert.ToUInt64(x.CPF).ToString(@"000\.000\.000\-00")
                    : Convert.ToUInt64(x.CNPJ).ToString(@"00\.000\.000\/0000\-00")))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.TIPO_PJPF_ID != 3
                    ? x.RAZAO_SOCIAL
                    : x.NOME))
                .ForMember(dest => dest.NomeFantasia, ori => ori.MapFrom(x => x.NOME_FANTASIA))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                //.ForMember(dest => dest.CNAE, ori => ori.MapFrom(x => x.CNAE))
                .ForMember(dest => dest.InscricaoEstadual, ori => ori.MapFrom(x => x.INSCR_ESTADUAL))
                .ForMember(dest => dest.InscricaoMunicipal, ori => ori.MapFrom(x => x.INSCR_MUNICIPAL))
                .ForMember(dest => dest.Bairro, ori => ori.MapFrom(x => x.BAIRRO))
                .ForMember(dest => dest.Cep, ori => ori.MapFrom(x => x.CEP))
                .ForMember(dest => dest.Cidade, ori => ori.MapFrom(x => x.CIDADE))
                .ForMember(dest => dest.Complemento, ori => ori.MapFrom(x => x.COMPLEMENTO))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.ENDERECO))
                .ForMember(dest => dest.Numero, ori => ori.MapFrom(x => x.NUMERO))
                .ForMember(dest => dest.Pais, ori => ori.MapFrom(x => x.PAIS))
                .ForMember(dest => dest.Estado, ori => ori.MapFrom(x => x.UF));

            //Mapper.AssertConfigurationIsValid();
        }

        private void MapearContatos()
        {
            CreateMap<SolicitacaoModificacaoDadosContato, DadosContatosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.IdContratante, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.Ignore())
                //--
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Celular, ori => ori.MapFrom(x => x.CELULAR))
                .ForMember(dest => dest.Email, ori => ori.MapFrom(x => x.EMAIL))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.TELEFONE));

            CreateMap<FORNECEDOR_CONTATOS, DadosContatosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratante, ori => ori.MapFrom(x => x.WFD_CONTRATANTE_PJPF.CONTRATANTE_ID))
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.MapFrom(x => x.WFD_CONTRATANTE_PJPF.ID))
                .ForMember(dest => dest.IdPjPf, ori => ori.MapFrom(x => x.WFD_CONTRATANTE_PJPF.PJPF_ID))
                //--
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Celular, ori => ori.MapFrom(x => x.CELULAR))
                .ForMember(dest => dest.Email, ori => ori.MapFrom(x => x.EMAIL))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.TELEFONE));
        }

        private void MapearUnspsc()
        {
            CreateMap<SOLICITACAO_UNSPSC, UnspscFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.IdContratante, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.Ignore())
                //--
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.T_UNSPSC.UNSPSC_DSC));

            CreateMap<FORNECEDOR_UNSPSC, UnspscFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratante, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.MapFrom(x => x.PJPF_ID))
                //--
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.T_UNSPSC.UNSPSC_DSC));
        }

        private void MapearInformacaoComplementar()
        {
            CreateMap<WFD_INFORM_COMPL, InformacaoComplementaresFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_ID));

            CreateMap<FORNECEDOR_INFORM_COMPL, InformacaoComplementaresFichaCadastralVM>()

.ForMember(destino => destino.IdPjPf, ori => ori.Ignore())
.ForMember(destino => destino.IdContratante, ori => ori.Ignore())
.ForMember(destino => destino.IdContratanteFornecedor, ori => ori.Ignore())
.ForMember(destino => destino.TipoFornecedor, ori => ori.Ignore())
.ForMember(destino => destino.IdPjPfBase, ori => ori.Ignore())
.ForMember(destino => destino.OrgaosPublicos, ori => ori.Ignore())
.ForMember(destino => destino.DadosEnderecos, ori => ori.Ignore())
.ForMember(destino => destino.DadosContatos, ori => ori.Ignore())
.ForMember(destino => destino.DadosUnspsc, ori => ori.Ignore())
.ForMember(destino => destino.DadosInformacaoComplementares, ori => ori.Ignore())
.ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.CONTRATANTE_PJPF_ID));
        }

        private void MapearSolicitacaoDocumento()
        {
            CreateMap<SolicitacaoDeDocumentos, DocumentosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.Documento, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                .ForMember(dest => dest.LinkDocumento, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.CAMINHO))
                .ForMember(dest => dest.ValidadePeriodo, ori => ori.MapFrom(x => x.DATA_VENCIMENTO));

            CreateMap<DocumentosDoFornecedor, DocumentosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.PJPF_ID))
                .ForMember(dest => dest.Documento, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                .ForMember(dest => dest.LinkDocumento, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.CAMINHO))
                .ForMember(dest => dest.ValidadePeriodo, ori => ori.MapFrom(x => x.DATA_VENCIMENTO));


        }

        private void MapearBancos()
        {
            CreateMap<SolicitacaoModificacaoDadosBancario, DadosBancariosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.Agencia, ori => ori.MapFrom(x => string.Format("{0}-{1}", x.AGENCIA, x.AG_DV)))
                .ForMember(dest => dest.Arquivo, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                .ForMember(dest => dest.Banco, ori => ori.MapFrom(x => x.T_BANCO.BANCO_NM))
                .ForMember(dest => dest.ContaCorrente, ori => ori.MapFrom(x => string.Format("{0}-{1}", x.CONTA, x.CONTA_DV)))
                .ForMember(dest => dest.LinkArquivo, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.CAMINHO));
            CreateMap<BancoDoFornecedor, DadosBancariosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.CONTRATANTE_PJPF_ID))
                .ForMember(dest => dest.Agencia, ori => ori.MapFrom(x => string.Format("{0}-{1}", x.AGENCIA, x.AG_DV)))
                .ForMember(dest => dest.Arquivo, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                .ForMember(dest => dest.Banco, ori => ori.MapFrom(x => x.T_BANCO.BANCO_NM))
                .ForMember(dest => dest.ContaCorrente, ori => ori.MapFrom(x => string.Format("{0}-{1}", x.CONTA, x.CONTA_DV)))
                .ForMember(dest => dest.LinkArquivo, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.CAMINHO));
        }

        private void MapearEnderecos()
        {
            CreateMap<SOLICITACAO_MODIFICACAO_ENDERECO, DadosEnderecosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.IdContratante, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.MapFrom(x => x.PJPF_ID))
                .ForMember(dest => dest.IdPjPfBase, ori => ori.Ignore())
                .ForMember(dest => dest.OrgaosPublicos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosEnderecos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.DadosInformacaoComplementares, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                //--
                .ForMember(dest => dest.Bairro, ori => ori.MapFrom(x => x.BAIRRO))
                .ForMember(dest => dest.CEP, ori => ori.MapFrom(x => x.CEP))
                .ForMember(dest => dest.Cidade, ori => ori.MapFrom(x => x.CIDADE))
                .ForMember(dest => dest.Complemento, ori => ori.MapFrom(x => x.COMPLEMENTO))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.ENDERECO))
                .ForMember(dest => dest.Estado, ori => ori.MapFrom(x => x.UF))
                .ForMember(dest => dest.Numero, ori => ori.MapFrom(x => x.NUMERO))
                .ForMember(dest => dest.Pais, ori => ori.MapFrom(x => x.PAIS))
                .ForMember(dest => dest.TipoEndereco, ori => ori.MapFrom(x => x.WFD_T_TP_ENDERECO.NM_TP_ENDERECO));

            CreateMap<FORNECEDORBASE_ENDERECO, DadosEnderecosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratante, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPfBase, ori => ori.MapFrom(x => x.PJPF_BASE_ID))
                .ForMember(dest => dest.OrgaosPublicos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosEnderecos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.DadosInformacaoComplementares, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                //--
                .ForMember(dest => dest.Bairro, ori => ori.MapFrom(x => x.BAIRRO))
                .ForMember(dest => dest.CEP, ori => ori.MapFrom(x => x.CEP))
                .ForMember(dest => dest.Cidade, ori => ori.MapFrom(x => x.CIDADE))
                .ForMember(dest => dest.Complemento, ori => ori.MapFrom(x => x.COMPLEMENTO))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.ENDERECO))
                .ForMember(dest => dest.Estado, ori => ori.MapFrom(x => x.UF))
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.PJPF_BASE_ID))
                .ForMember(dest => dest.Numero, ori => ori.MapFrom(x => x.NUMERO))
                .ForMember(dest => dest.Pais, ori => ori.MapFrom(x => x.PAIS))
                .ForMember(dest => dest.TipoEndereco, ori => ori.MapFrom(x => x.WFD_T_TP_ENDERECO.NM_TP_ENDERECO));


            CreateMap<FORNECEDOR_ENDERECO, DadosEnderecosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratante, ori => ori.MapFrom(x => x.WFD_CONTRATANTE_PJPF.CONTRATANTE_ID))
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.MapFrom(x => x.CONTRATANTE_PJPF_ID))
                .ForMember(dest => dest.IdPjPf, ori => ori.MapFrom(x => x.WFD_CONTRATANTE_PJPF.PJPF_ID))
                .ForMember(dest => dest.IdPjPfBase, ori => ori.Ignore())
                .ForMember(dest => dest.OrgaosPublicos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosEnderecos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.DadosInformacaoComplementares, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))

                .ForMember(dest => dest.Bairro, ori => ori.MapFrom(x => x.BAIRRO))
                .ForMember(dest => dest.CEP, ori => ori.MapFrom(x => x.CEP))
                .ForMember(dest => dest.Cidade, ori => ori.MapFrom(x => x.CIDADE))
                .ForMember(dest => dest.Complemento, ori => ori.MapFrom(x => x.COMPLEMENTO))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.ENDERECO))
                .ForMember(dest => dest.Estado, ori => ori.MapFrom(x => x.UF))
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.CONTRATANTE_PJPF_ID))
                .ForMember(dest => dest.Numero, ori => ori.MapFrom(x => x.NUMERO))
                .ForMember(dest => dest.Pais, ori => ori.MapFrom(x => x.PAIS))
                .ForMember(dest => dest.TipoEndereco, ori => ori.MapFrom(x => x.WFD_T_TP_ENDERECO.NM_TP_ENDERECO));

        }

        private void MapearSolicitacaoCadastroFornecedor()
        {
            CreateMap<SolicitacaoCadastroFornecedor, DadosGeraisFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.IdContratante, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPfBase, ori => ori.Ignore())
                .ForMember(dest => dest.OrgaosPublicos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosEnderecos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.DadosInformacaoComplementares, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))

                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.CategoriaGrupoContas, ori => ori.MapFrom(x => x.WFD_PJPF_CATEGORIA.DESCRICAO))
                .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => x.CNPJ))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(dest => dest.NomeFantasia, ori => ori.MapFrom(x => x.NOME_FANTASIA))
                .ForMember(dest => dest.CNAE, ori => ori.MapFrom(x => x.CNAE))
                .ForMember(dest => dest.InscricaoEstadual, ori => ori.MapFrom(x => x.INSCR_ESTADUAL))
                .ForMember(dest => dest.InscricaoMunicipal, ori => ori.MapFrom(x => x.INSCR_MUNICIPAL));
        }

        private void MapearOrgaosPublicoPartial()
        {
            CreateMap<ROBO, OrgaosPublicosFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.IdContratante, ori => ori.MapFrom(x => x.WFD_SOLICITACAO.CONTRATANTE_ID))
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPfBase, ori => ori.Ignore())
                .ForMember(dest => dest.OrgaosPublicos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosEnderecos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.DadosInformacaoComplementares, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))

                .ForMember(dest => dest.ReceitaFederal, ori => ori.MapFrom(x =>
                new ReceitaFederalFichaCadastralVM
                {
                    RazaoSocial = x.RECEITA_FEDERAL_RAZAO_SOCIAL,
                    NomeFantasia = x.RF_NOME_FANTASIA,
                    SituacaoCadastral = x.RF_SIT_CADASTRAL_CNPJ,
                    MotivoSituacaoCadastral = x.RF_MOTIVO_CNPJ_SITU_CADASTRAL,
                    DataSituacaoCadastral = x.RF_SIT_CADSTRAL_CNPJ_DT.HasValue ? x.RF_SIT_CADSTRAL_CNPJ_DT.Value.ToString("dd/MM/yyyy") : string.Empty,
                    DataEmissao = x.RF_CONSULTA_DTHR.HasValue ? x.RF_CONSULTA_DTHR.Value.ToString("dd/MM/yyyy") : string.Empty,
                    HoraEmissao = string.Empty,
                    DataAbertura = x.RF_CNPJ_DT_ABERTURA.HasValue ? x.RF_CNPJ_DT_ABERTURA.Value.ToString("dd/MM/yyyy") : string.Empty,
                    EnteFederativo = string.Empty,
                    EnderecoEletronico = string.Empty,
                    Telefone = string.Empty,
                    ObservacaoIbge = string.Empty,
                    Logradouro = x.RF_LOGRADOURO,
                    Numero = x.RF_NUMERO,
                    Complemento = x.RF_COMPLEMENTO,
                    Bairro = x.RF_BAIRRO,
                    Municipio = x.RF_MUNICIPIO,
                    Estado = x.RF_UF,
                    Cep = x.RF_CEP,
                    MatrizOuFilial = x.RF_MATRIZ_FILIAL,
                    AtividadePrincipal = string.Empty,
                    NaturezaJuridica = x.RF_DSC_NATUREZA_JURIDICA,
                    SituacaoEspecial = x.RF_SIT_ESPECIAL_CNPJ,
                    DatasSituacaoEspecial = x.RF_SIT_ESPECIAL_CNPJ_DT.HasValue ? x.RF_SIT_ESPECIAL_CNPJ_DT.Value.ToString("dd/MM/yyyy") : string.Empty
                }))
                .ForMember(dest => dest.Sintegra, ori => ori.MapFrom(robo =>
                new SintegraFichaCadastralVM
                {
                    HTML = robo.SINT_CERTIFICADO_HTML,
                    DataConsulta = robo.SINT_CONSULTA_DTHR.HasValue ? robo.SINT_CONSULTA_DTHR.Value.ToString("dd/MM/yyyy") : string.Empty,
                    InscricaoEstadual = robo.SINT_IE_COD,
                    MultiplasIE = robo.SINT_IE_MULTIPLA,
                    SituacaoCadastral = robo.SINT_IE_SITU_CADASTRAL,
                    DataSituacaoCadastral = robo.SINT_IE_SITU_CADSTRAL_DT,
                    DataInclusao = robo.SINT_INCLUSAO_DT.HasValue ? robo.SINT_INCLUSAO_DT.Value.ToString("dd/MM/yyyy") : string.Empty,
                    Telefone = robo.SINT_TEL,
                    AtividadeEconomicaPrincipal = robo.SINT_ATIVIDADE_PRINCIPAL,
                    Bairro = robo.SINT_BAIRRO,
                    CEP = robo.SINT_CEP,
                    CNPJ = robo.CNPJ,
                    Complemento = robo.SINT_COMPLEMENTO,
                    EnquadramentoFiscal = robo.SINT_ENQUADRAMENTO_FISCAL,
                    Logradouro = robo.SINT_LOGRADOURO,
                    Numero = robo.SINT_NUMERO,
                    Municipio = robo.SINT_MUNICIPIO,
                    UF = robo.SINT_UF,
                    RazaoSocial = robo.SINT_RAZAO_SOCIAL,
                    Codigo = robo.SINT_CODE_ROBO.ToString(),
                    CertificadoHtml = robo.SINT_CERTIFICADO_HTML,
                    SituacaoCadastralInscricaoEstadual = robo.SINT_IE_SITU_CADASTRAL,
                    DataSituacaoCadastralInscricaoEstadual = robo.SINT_IE_SITU_CADSTRAL_DT,
                }))
                .ForMember(dest => dest.SimplesNacional, ori => ori.MapFrom(robo =>
                new SimplesNacionalFichaCadastralVM
                {
                    DataConsulta = robo.SN_CONSULTA_DTHR.HasValue ? robo.SN_CONSULTA_DTHR.Value.ToString("dd/MM/yyyy") : string.Empty,
                    Codigo = robo.SN_CODE_ROBO.ToString(),
                    SituacaoSimplesNacional = robo.SIMPLES_NACIONAL_SITUACAO,
                    PeriodosAnteriores = robo.SN_PERIODOS_ANTERIORES,
                    SIMEIPeriodosAnteriores = robo.SN_SIMEI_PERIODOS_ANTERIORES,
                    SituacaoSIMEI = robo.SN_SITUACAO_SIMEI,
                    RazaoSocial = robo.SN_RAZAO_SOCIAL,
                }));
        }

        private void MapearSolicitacaoPartial()
        {
            CreateMap<SOLICITACAO, DadosSolicitacaoFichaCadastralVM>()
                .ForMember(destino => destino.TipoFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.IdContratante, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPfBase, ori => ori.Ignore())
                .ForMember(dest => dest.OrgaosPublicos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosEnderecos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.DadosInformacaoComplementares, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.DataSolicitacao, ori => ori.MapFrom(x => x.SOLICITACAO_DT_CRIA))
                .ForMember(dest => dest.Observacao, ori => ori.MapFrom(x => x.MOTIVO))
                .ForMember(dest => dest.PrazoEntrega, ori => ori.MapFrom(x => x.DT_PRAZO))
                .ForMember(dest => dest.SituacaoSolicitacaoList, ori => ori.MapFrom(x => x.WFD_SOLICITACAO_TRAMITE))
                .ForMember(dest => dest.Solicitante, ori => ori.MapFrom(x => x.Usuario.LOGIN))
                .ForMember(dest => dest.StatusReceitaFederal, ori => ori.MapFrom(x => x.ROBO.FirstOrDefault().RF_SIT_CADASTRAL_CNPJ))
                .ForMember(dest => dest.StatusSimplesNacional, ori => ori.MapFrom(x => x.ROBO.FirstOrDefault().SIMPLES_NACIONAL_SITUACAO))
                .ForMember(dest => dest.StatusSintegra, ori => ori.MapFrom(x => x.ROBO.FirstOrDefault().SINT_IE_SITU_CADASTRAL))
                .ForMember(dest => dest.TipoSolicitacao, ori => ori.MapFrom(x => x.Fluxo.FLUXO_NM));

            CreateMap<SOLICITACAO_TRAMITE, TabelaSituacaoSolicitacao>()
                .ForMember(dest => dest.Status, ori => ori.MapFrom(x => x.WFD_SOLICITACAO_STATUS.NOME))
                .ForMember(dest => dest.Situacao, ori => ori.MapFrom(x => x.Papel.PAPEL_NM))
                .ForMember(dest => dest.Data, ori => ori.MapFrom(x => x.TRMITE_DT_FIM));
        }

        private void MapearPaginaFichaCadastral()
        {
            CreateMap<SOLICITACAO, SolicitacaoFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.IdContratante, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.MapFrom(x => x.PJPF_ID))
                .ForMember(dest => dest.IdPjPfBase, ori => ori.MapFrom(x => x.PJPF_BASE_ID))
                .ForMember(dest => dest.OrgaosPublicos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosEnderecos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.DadosInformacaoComplementares, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                //--
                .ForMember(dest => dest.DadosSolicitacao, ori => ori.MapFrom(x => x))
                .ForMember(dest => dest.TipoFluxo, ori => ori.MapFrom(x => (EnumTiposFluxo)x.FLUXO_ID))
                .ForMember(dest => dest.Gerais, ori => ori.MapFrom(x => x.WFD_SOL_MOD_DGERAIS_SEQ))
                .ForMember(dest => dest.Gerais, ori => ori.MapFrom(x => x.SolicitacaoCadastroFornecedor))
                .ForMember(dest => dest.Contatos, ori => ori.MapFrom(x => x.SolicitacaoModificacaoDadosContato))
                .ForMember(dest => dest.Bancarios, ori => ori.MapFrom(x => x.SolicitacaoModificacaoDadosBancario))
                .ForMember(dest => dest.Enderecos, ori => ori.MapFrom(x => x.WFD_SOL_MOD_ENDERECO))
                .ForMember(dest => dest.Documentos, ori => ori.MapFrom(x => x.SolicitacaoDeDocumentos))
                .ForMember(dest => dest.InformacaoComplementares, ori => ori.MapFrom(x => x.WFD_INFORM_COMPL))
                .ForMember(dest => dest.DadosRobo, ori => ori.MapFrom(x => x.ROBO))
                .ForMember(dest => dest.Unspsc, ori => ori.MapFrom(x => x.WFD_SOL_UNSPSC));

            CreateMap<WFD_CONTRATANTE_PJPF, FornecedorFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratante, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.IdPjPf, ori => ori.MapFrom(x => x.PJPF_ID))
                .ForMember(dest => dest.IdPjPfBase, ori => ori.Ignore())
                .ForMember(dest => dest.OrgaosPublicos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosEnderecos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.DadosInformacaoComplementares, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                //.ForMember(dest => dest.DadosSolicitacao, ori => ori.MapFrom(x => x))
                //.ForMember(dest => dest.TipoFluxo, ori => ori.MapFrom(x => (EnumTiposFluxo)x.FLUXO_ID))
                //.ForMember(dest => dest.Gerais, ori => ori.MapFrom(x => x.WFD_SOL_MOD_DGERAIS_SEQ))
                //.ForMember(dest => dest.Gerais, ori => ori.MapFrom(x => x.SolicitacaoCadastroFornecedor))
                .ForMember(dest => dest.Contatos, ori => ori.MapFrom(x => x.WFD_PJPF_CONTATOS))
                .ForMember(dest => dest.Bancarios, ori => ori.MapFrom(x => x.BancoDoFornecedor))
                .ForMember(dest => dest.Enderecos, ori => ori.MapFrom(x => x.WFD_PJPF_ENDERECO))
                .ForMember(dest => dest.Documentos, ori => ori.MapFrom(x => x.WFD_PJPF_DOCUMENTOS))
                .ForMember(dest => dest.InformacaoComplementares, ori => ori.MapFrom(x => x.WFD_PJPF_INFORM_COMPL))
                .ForMember(dest => dest.DadosRobo, ori => ori.MapFrom(x => x.WFD_PJPF.ROBO))
                .ForMember(dest => dest.Unspsc, ori => ori.MapFrom(x => x.WFD_PJPF.FornecedorServicoMaterialList));

            //FORNECEDORBASE
            CreateMap<FORNECEDORBASE, PreCadastroFichaCadastralVM>()
                .ForMember(dest => dest.IdSolicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.IdContratante, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.IdContratanteFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPf, ori => ori.Ignore())
                .ForMember(dest => dest.IdPjPfBase, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.OrgaosPublicos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosEnderecos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.DadosUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.DadosInformacaoComplementares, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                //--
                .ForMember(dest => dest.Contatos, ori => ori.MapFrom(x => x.WFD_PJPF_BASE_CONTATOS))
                .ForMember(dest => dest.Enderecos, ori => ori.MapFrom(x => x.WFD_PJPF_BASE_ENDERECO))
                .ForMember(dest => dest.DadosRobo, ori => ori.MapFrom(x => x.ROBO))
                .ForMember(dest => dest.Unspsc, ori => ori.MapFrom(x => x.WFD_PJPF_BASE_UNSPSC));
            //.ForMember(dest => dest.Gerais, ori => ori.MapFrom(x => x.WFD_PJPF_BASE_IMPORTACAO))
            //.ForMember(dest => dest.Documentos, ori => ori.MapFrom(x => x.WFD_PJPF_SOLICITACAO_DOCUMENTOS))
            //.ForMember(dest => dest.Convite, ori => ori.MapFrom(x => x.WFD_PJPF_BASE_CONVITE))
            //.ForMember(dest => dest.DadosSolicitacao, ori => ori.MapFrom(x => x))
            //.ForMember(dest => dest.TipoFluxo, ori => ori.MapFrom(x => (EnumTiposFluxo)x.FLUXO_ID))
            //.ForMember(dest => dest.Bancarios, ori => ori.MapFrom(x => x.ban))
            //.ForMember(dest => dest.InformacaoComplementares, ori => ori.MapFrom(x => x.))
            //public virtual WFD_PJPF_CATEGORIA WFD_PJPF_CATEGORIA { get; set; }
            //public virtual TipoDeStatusDoFornecedorPreCadastrado TipoDeStatusDoFornecedorPreCadastrado { get; set; }
            //public virtual ICollection<WFD_PJPF_SOLICITACAO_DOCUMENTOS> WFD_PJPF_SOLICITACAO_DOCUMENTOS { get; set; }
            //public virtual ICollection<WFD_SOLICITACAO> WFD_SOLICITACAO { get; set; }
        }
    }
}