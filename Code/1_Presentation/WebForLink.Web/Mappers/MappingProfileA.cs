using AutoMapper;
using System;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Domain.Robos;
using WebForLink.Domain.Services;
using WebForLink.Web.Areas.Administracao.Models;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.Adesao;
using WebForLink.Web.ViewModels.Carga;
using WebForLink.Web.ViewModels.Fornecedores;
using WebForLink.Web.ViewModels.Novo;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Mappers
{
    public class MappingProfileA : Profile
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }
        public MappingProfileA()
        {

            //}
            //protected override void Configure()
            //{
            try
            {
                MapeamentoConversores();
                MapearCarga();
                MapearRetorno();
                MapearDto();
                //--Mapear Banco
                MapearWacPapel();
                MapearWfdUsuario();
                MapearWacAplicacao();
                MapearBanco();
                MapearWacPerfil();
                MapearWacFuncao();
                MapearQicQuestAbaPerg();
                MapearQicQuestionario();
                MapearQicQuestAba();
                MapearQicQuestAbaPergPerfil();
                MapearWfdUsuarioSenhasHist();
                MapearWfdContratanteConfig();
                MapearWfdTipoCadastro();
                MapearWfdInformCompl();
                MapearArquivoUpload();
                MapearWdfContratantePjpf();
                MapearPjpfCategoria();
                MapearWfdContratanteOrgCompras();
                MapearTBanco();

                // Model > ViewModel
                MapearWFDPJPFBanco();
                MapearWFDPJPFBase();
                MapearWFDSolDocumentos();
                MapearWFDPJPFDocumentos();
                MapearImportacaoFornecedoresFiltro();
                MapearFornecedorBaseTopoDTO();
                MapearDocumentosSolicitadosVM();

                // ViewModel > Model
                MapearDadosBancariosVM();
                MapearDadosEnderecosVM();
                MapearFornecedorBaseVM();
                MapearCarga();

                MapearRetorno();
                MapearWfdPjpfBase();
                MapeaContatos();
                MapeamentoPagSeguro();
                //Mapper.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }


        private void MapearDto()
        {
            CreateMap<QuestionarioDinamico, QuestionarioVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.QuestionarioID))
                .ForMember(dest => dest.Titulo, ori => ori.MapFrom(x => x.Titulo))
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.ContratanteId))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.Descricao))
                .ForMember(dest => dest.ExibeDadosBancarios, ori => ori.MapFrom(x => x.ExibeDadosBancarios))
                .ForMember(dest => dest.ExibeDadosContato, ori => ori.MapFrom(x => x.ExibeDadosContato))
                .ForMember(dest => dest.ExibeDadosGerais, ori => ori.MapFrom(x => x.ExibeDadosGerais))
                .ForMember(dest => dest.ExibeInformacaoComplementar, ori => ori.MapFrom(x => x.ExibeInformacaoComplementar))
                .ForMember(dest => dest.AbaList, ori => ori.MapFrom(x => x.Abas))
                .ForMember(dest => dest.EstiloClassCss, ori => ori.Ignore());

            CreateMap<AbaQuestionarioDinamico, AbaVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.AbaID))
                .ForMember(dest => dest.Titulo, ori => ori.MapFrom(x => x.Titulo))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.Descricao))
                .ForMember(dest => dest.QuestionarioId, ori => ori.Ignore())
                .ForMember(dest => dest.PerguntaList, ori => ori.MapFrom(x => x.Perguntas));

            CreateMap<PerguntaAbaDinamico, PerguntaVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.PerguntaId))
                .ForMember(dest => dest.AbaId, ori => ori.MapFrom(x => x.AbaId))
                .ForMember(dest => dest.Titulo, ori => ori.MapFrom(x => x.Titulo))
                .ForMember(dest => dest.PerguntaPai, ori => ori.MapFrom(x => x.PerguntaPai))
                .ForMember(dest => dest.TipoDado, ori => ori.MapFrom(x => x.TpDadoDominio.ToString()))
                .ForMember(dest => dest.Dominio, ori => ori.MapFrom(x => x.Dominio))
                .ForMember(dest => dest.EPai, ori => ori.MapFrom(x => x.EPai))
                .ForMember(dest => dest.Tamanho, ori => ori.MapFrom(x => x.Tamanho))
                .ForMember(dest => dest.ExibeNome, ori => ori.MapFrom(x => x.ExibeNome))
                .ForMember(dest => dest.DominioList, ori => ori.MapFrom(x => x.ListaSelecionavel))
                .ForMember(dest => dest.Escrita, ori => ori.MapFrom(x => !x.Bloqueado))
                .ForMember(dest => dest.Obrigatorio, ori => ori.MapFrom(x => x.Obrigatorio))
                .ForMember(dest => dest.Leitura, ori => ori.MapFrom(x => x.Visivel))
                .ForMember(dest => dest.SolicitacaoId, ori => ori.MapFrom(x => x.SolicitacaoId))
                .ForMember(dest => dest.RespostaId, ori => ori.MapFrom(x => x.RespostaId))
                .ForMember(dest => dest.Resposta, ori => ori.MapFrom(x => x.Resposta))
                .ForMember(dest => dest.PulaLinha, ori => ori.MapFrom(x => x.PulaLinha))
                .ForMember(dest => dest.RespostaFornecedor, ori => ori.MapFrom(x => x.RespostaFornecedor))
                .ForMember(dest => dest.DominioListId, ori => ori.Ignore())
                .ForMember(dest => dest.PaiRespondido, ori => ori.Ignore())
                .ForMember(dest => dest.Ordem, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaCheckbox, ori => ori.Ignore());

            CreateMap<ListaDeDocumentosDeFornecedor, ListaDocumentosVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.ExigeValidade, ori => ori.MapFrom(x => x.EXIGE_VALIDADE))
                .ForMember(dest => dest.Ativo, ori => ori.MapFrom(x => x.ATIVO))
                .ForMember(dest => dest.TipoDocumento, ori => ori.Ignore())
                .ForMember(dest => dest.DescricaoDocumento, ori => ori.Ignore())
                .ForMember(dest => dest.Descricao, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore());

            CreateMap<PerguntaDTO, PerguntaVM>()
                .ForMember(dest => dest.TipoDado, ori => ori.Ignore())
                .ForMember(dest => dest.ExibeNome, ori => ori.Ignore())
                .ForMember(dest => dest.DominioListId, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoId, ori => ori.Ignore())
                .ForMember(dest => dest.Tamanho, ori => ori.Ignore())
                .ForMember(dest => dest.Ordem, ori => ori.Ignore())
                .ForMember(dest => dest.PaiRespondido, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaFornecedorId, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaCheckbox, ori => ori.Ignore())
                .ForMember(dest => dest.PulaLinha, ori => ori.Ignore());

            CreateMap<FornecedoresVM, CadastrarSolicitacaoDTO>()
                .ForMember(dest => dest.ContatoNome, ori => ori.MapFrom(x => x.NomeContato))
                .ForMember(dest => dest.ContatoEmail, ori => ori.MapFrom(x => x.Email))
                .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.Telefone))
                .ForMember(dest => dest.TipoCadastro, ori => ori.MapFrom(x => x.TipoCadastro))
                .ForMember(dest => dest.TipoFornecedor, ori => ori.MapFrom(x => x.TipoFornecedor))
                .ForMember(dest => dest.CategoriaId, ori => ori.MapFrom(x => x.Categoria))
                .ForMember(dest => dest.ComprasId, ori => ori.MapFrom(x => x.Compras))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RazaoSocial))
                .ForMember(dest => dest.CPF, ori => ori.MapFrom(x => x.CNPJ))
                .ForMember(dest => dest.DataNascimento, ori => ori.MapFrom(x => x.DataNascimento))
                .ForMember(dest => dest.UsuarioId, ori => ori.Ignore())
                .ForMember(dest => dest.Assunto, ori => ori.Ignore());

            CreateMap<QUESTIONARIO_PERGUNTA, PerguntaVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.AbaId, ori => ori.MapFrom(x => x.QUEST_ABA_ID))
                .ForMember(dest => dest.Titulo, ori => ori.MapFrom(x => x.PERG_NM))
                .ForMember(dest => dest.PerguntaPai, ori => ori.MapFrom(x => x.PERG_PAI))
                .ForMember(dest => dest.TipoDado, ori => ori.MapFrom(x => x.TP_DADO))
                .ForMember(dest => dest.Dominio, opt => opt.Condition(src => src.DOMINIO ?? false))
                .ForMember(dest => dest.EPai, opt => opt.MapFrom(src => src.E_PAI))
                .ForMember(dest => dest.Tamanho, opt => opt.MapFrom(x => x.RESP_TAMANHO ?? 0))
                .ForMember(dest => dest.ExibeNome, ori => ori.MapFrom(x => x.EXIBE_NM))
                .ForMember(dest => dest.DominioList,
                    ori => ori.MapFrom(x => x.QIC_QUEST_ABA_PERG_RESP.Where(y => y.PERG_ID == x.ID)))
                .ForMember(dest => dest.Escrita,
                    ori => ori.MapFrom(x => x.QIC_QUEST_ABA_PERG_PAPEL.FirstOrDefault(y => y.PERG_ID == x.ID).ESCRITA))
                .ForMember(dest => dest.Obrigatorio,
                    opt => opt.MapFrom(x => x.QIC_QUEST_ABA_PERG_PAPEL.FirstOrDefault(y => y.PERG_ID == x.ID).OBRIG))
                .ForMember(dest => dest.Leitura,
                    ori => ori.MapFrom(x => x.QIC_QUEST_ABA_PERG_PAPEL.FirstOrDefault().LEITURA))
                .ForMember(dest => dest.SolicitacaoId,
                    ori => ori.MapFrom(x => x.WFD_INFORM_COMPL.FirstOrDefault(y => y.PERG_ID == x.ID).SOLICITACAO_ID))
                .ForMember(dest => dest.RespostaId,
                    ori => ori.MapFrom(x => x.WFD_INFORM_COMPL.FirstOrDefault(y => y.PERG_ID == x.ID).ID))
                .ForMember(dest => dest.Resposta,
                    ori => ori.MapFrom(x => x.WFD_INFORM_COMPL.FirstOrDefault(y => y.PERG_ID == x.ID).RESPOSTA))
                .ForMember(dest => dest.RespostaFornecedorId,
                    ori => ori.MapFrom(x => x.WFD_PJPF_INFORM_COMPL.FirstOrDefault(y => y.PERG_ID == x.ID).ID))
                .ForMember(dest => dest.RespostaFornecedor,
                    ori => ori.MapFrom(x => x.WFD_PJPF_INFORM_COMPL.FirstOrDefault(y => y.PERG_ID == x.ID).RESPOSTA))
                .ForMember(dest => dest.DominioListId, ori => ori.Ignore())
                .ForMember(dest => dest.PaiRespondido, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaCheckbox, ori => ori.Ignore())
                .ForMember(dest => dest.PulaLinha, ori => ori.Ignore());

            CreateMap<QUESTIONARIO_PAPEL, PerguntaVM>()
                .ForMember(dest => dest.Leitura, ori => ori.MapFrom(x => x.LEITURA))
                .ForMember(dest => dest.Escrita, ori => ori.MapFrom(x => x.ESCRITA))
                .ForMember(dest => dest.Obrigatorio, ori => ori.MapFrom(x => x.OBRIG))
                .ForMember(dest => dest.Id, ori => ori.Ignore())
                .ForMember(dest => dest.AbaId, ori => ori.Ignore())
                .ForMember(dest => dest.Titulo, ori => ori.Ignore())
                .ForMember(dest => dest.TipoDado, ori => ori.Ignore())
                .ForMember(dest => dest.ExibeNome, ori => ori.Ignore())
                .ForMember(dest => dest.Dominio, ori => ori.Ignore())
                .ForMember(dest => dest.DominioListId, ori => ori.Ignore())
                .ForMember(dest => dest.Resposta, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoId, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaId, ori => ori.Ignore())
                .ForMember(dest => dest.Tamanho, ori => ori.Ignore())
                .ForMember(dest => dest.Ordem, ori => ori.Ignore())
                .ForMember(dest => dest.EPai, ori => ori.Ignore())
                .ForMember(dest => dest.PerguntaPai, ori => ori.Ignore())
                .ForMember(dest => dest.DominioList, ori => ori.Ignore())
                .ForMember(dest => dest.PaiRespondido, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaFornecedorId, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaCheckbox, ori => ori.Ignore())
                .ForMember(dest => dest.PulaLinha, ori => ori.Ignore());

            CreateMap<WFD_INFORM_COMPL, PerguntaVM>()
                .ForMember(dest => dest.Resposta, ori => ori.MapFrom(x => x.RESPOSTA))
                .ForMember(dest => dest.RespostaId, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Id, ori => ori.Ignore())
                .ForMember(dest => dest.AbaId, ori => ori.Ignore())
                .ForMember(dest => dest.Titulo, ori => ori.Ignore())
                .ForMember(dest => dest.TipoDado, ori => ori.Ignore())
                .ForMember(dest => dest.ExibeNome, ori => ori.Ignore())
                .ForMember(dest => dest.Dominio, ori => ori.Ignore())
                .ForMember(dest => dest.DominioListId, ori => ori.Ignore())
                .ForMember(dest => dest.Obrigatorio, ori => ori.Ignore())
                .ForMember(dest => dest.Leitura, ori => ori.Ignore())
                .ForMember(dest => dest.Escrita, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoId, ori => ori.Ignore())
                .ForMember(dest => dest.Tamanho, ori => ori.Ignore())
                .ForMember(dest => dest.Ordem, ori => ori.Ignore())
                .ForMember(dest => dest.EPai, ori => ori.Ignore())
                .ForMember(dest => dest.PerguntaPai, ori => ori.Ignore())
                .ForMember(dest => dest.DominioList, ori => ori.Ignore())
                .ForMember(dest => dest.PaiRespondido, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaFornecedorId, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaCheckbox, ori => ori.Ignore())
                .ForMember(dest => dest.PulaLinha, ori => ori.Ignore());

            CreateMap<FORNECEDOR_INFORM_COMPL, PerguntaVM>()
                .ForMember(dest => dest.RespostaFornecedor, ori => ori.MapFrom(x => x.RESPOSTA))
                .ForMember(dest => dest.RespostaFornecedorId, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Resposta, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaId, ori => ori.Ignore())
                .ForMember(dest => dest.Id, ori => ori.Ignore())
                .ForMember(dest => dest.AbaId, ori => ori.Ignore())
                .ForMember(dest => dest.Titulo, ori => ori.Ignore())
                .ForMember(dest => dest.TipoDado, ori => ori.Ignore())
                .ForMember(dest => dest.ExibeNome, ori => ori.Ignore())
                .ForMember(dest => dest.Dominio, ori => ori.Ignore())
                .ForMember(dest => dest.DominioListId, ori => ori.Ignore())
                .ForMember(dest => dest.Obrigatorio, ori => ori.Ignore())
                .ForMember(dest => dest.Leitura, ori => ori.Ignore())
                .ForMember(dest => dest.Escrita, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoId, ori => ori.Ignore())
                .ForMember(dest => dest.Tamanho, ori => ori.Ignore())
                .ForMember(dest => dest.Ordem, ori => ori.Ignore())
                .ForMember(dest => dest.EPai, ori => ori.Ignore())
                .ForMember(dest => dest.PerguntaPai, ori => ori.Ignore())
                .ForMember(dest => dest.DominioList, ori => ori.Ignore())
                .ForMember(dest => dest.PaiRespondido, ori => ori.Ignore())
                .ForMember(dest => dest.RespostaCheckbox, ori => ori.Ignore())
                .ForMember(dest => dest.PulaLinha, ori => ori.Ignore());

            CreateMap<QUESTIONARIO_PERGUNTA, PerguntaVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Escrita, ori => ori.MapFrom(x => x
                    .QIC_QUEST_ABA_PERG_PAPEL
                        .FirstOrDefault(y => y.PERG_ID == x.ID
                            && y.PAPEL_ID == 1)
                                .ESCRITA))
                .ForMember(dest => dest.Resposta, opt => opt.Condition(src => (src.ID >= 0)));


            CreateMap<QUESTIONARIO, QuestionarioVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Titulo, ori => ori.MapFrom(x => x.QUEST_NM))
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.QUEST_DSC))
                .ForMember(dest => dest.ExibeDadosBancarios, ori => ori.MapFrom(x => x.LE_D_BANCARIO))
                .ForMember(dest => dest.ExibeDadosContato, ori => ori.MapFrom(x => x.LE_D_CONTATO))
                .ForMember(dest => dest.ExibeDadosGerais, ori => ori.MapFrom(x => x.LE_D_GERAIS))
                .ForMember(dest => dest.ExibeInformacaoComplementar, ori => ori.MapFrom(x => x.LE_INFO_COMPL))
                .ForMember(dest => dest.AbaList, ori => ori.MapFrom(x => x.QIC_QUEST_ABA))
                .ForMember(dest => dest.EstiloClassCss, ori => ori.Ignore());

            CreateMap<QUESTIONARIO_ABA, AbaVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Titulo, ori => ori.MapFrom(x => x.ABA_NM))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.ABA_DSC))
                .ForMember(dest => dest.QuestionarioId, ori => ori.MapFrom(x => x.QUESTIONARIO_ID))
                .ForMember(dest => dest.PerguntaList, ori => ori.MapFrom(x => x.QIC_QUEST_ABA_PERG));

            CreateMap<SOLICITACAO, FluxoVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.FLUXO_ID))
                .ForMember(dest => dest.Nome, ori => ori.Ignore())
                .ForMember(dest => dest.Papel_Inicial, ori => ori.Ignore());

            CreateMap<SOLICITACAO, SolicitacaoVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Aprovado, ori => ori.Ignore())
                .ForMember(dest => dest.Fluxo, ori => ori.Ignore())
                .ForMember(dest => dest.Tramite, ori => ori.Ignore())
                .ForMember(dest => dest.Tramites, ori => ori.Ignore());

            CreateMap<FichaCadastralWebForLinkVM, SolicitacaoCadastroFornecedor>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.BAIRRO, ori => ori.MapFrom(x => x.Bairro))
                .ForMember(dest => dest.CEP, ori => ori.MapFrom(x => x.Cep))
                .ForMember(dest => dest.CIDADE, ori => ori.MapFrom(x => x.Cidade))
                .ForMember(dest => dest.COMPLEMENTO, ori => ori.MapFrom(x => x.Complemento))
                .ForMember(dest => dest.ENDERECO, ori => ori.MapFrom(x => x.Endereco))
                .ForMember(dest => dest.NOME_FANTASIA, ori => ori.MapFrom(x => x.NomeFantasia))
                .ForMember(dest => dest.NUMERO, ori => ori.MapFrom(x => x.Numero))
                .ForMember(dest => dest.OBSERVACAO, ori => ori.MapFrom(x => x.Observacao))
                .ForMember(dest => dest.PAIS, ori => ori.MapFrom(x => x.Pais))
                .ForMember(dest => dest.RAZAO_SOCIAL, ori => ori.MapFrom(x => x.RazaoSocial))
                .ForMember(dest => dest.TP_LOGRADOURO, ori => ori.MapFrom(x => x.TipoLogradouro))
                .ForMember(dest => dest.PAIS, ori => ori.MapFrom(x => x.Pais))
                .ForMember(dest => dest.PJPF_TIPO, ori => ori.Ignore())
                .ForMember(dest => dest.CPF, ori => ori.Ignore())
                .ForMember(dest => dest.CNPJ, ori => ori.Ignore())
                .ForMember(dest => dest.NOME, ori => ori.MapFrom(x => x.RazaoSocial))
                .ForMember(dest => dest.INSCR_ESTADUAL, ori => ori.Ignore())
                .ForMember(dest => dest.INSCR_MUNICIPAL, ori => ori.Ignore())
                .ForMember(dest => dest.UF, ori => ori.MapFrom(x => x.Estado))
                .ForMember(dest => dest.COD_PJPF_ERP, ori => ori.Ignore())
                .ForMember(dest => dest.CLIENTE, ori => ori.Ignore())
                .ForMember(dest => dest.GRUPO_EMPRESA, ori => ori.Ignore())
                .ForMember(dest => dest.DT_NASCIMENTO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_CATEGORIA, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_ROBO, ori => ori.Ignore())
                .ForMember(dest => dest.EhExpansao, ori => ori.Ignore())
                .ForMember(dest => dest.EXPANSAO_PARA_CONTR_ID, ori => ori.Ignore())
                //.ForMember(dest => dest.WFD_SOL_CAD_PJPF_MAT_SERV, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOLICITACAO, ori => ori.Ignore())
                //.ForMember(dest => dest.CNAE, ori => ori.MapFrom(x => x.CNAE))
                .AfterMap((src, dest) => dest.INSCR_MUNICIPAL = src.TipoFornecedor != 2 ? src.InscricaoMunicipal : null)
                .AfterMap((src, dest) => dest.CPF = src.TipoFornecedor != 3 ? src.CNPJ_CPF.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "") : null)
                .AfterMap((src, dest) => dest.CPF = src.TipoFornecedor != 3 ? src.InscricaoEstadual : null);

            CreateMap<SOLICITACAO, AprovacaoVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.NomeContratante, ori => ori.MapFrom(x => x.Contratante.RAZAO_SOCIAL))
                .ForMember(dest => dest.Solicitacao_Dt_Cria, ori => ori.MapFrom(x => x.SOLICITACAO_DT_CRIA))
                .ForMember(dest => dest.NomeSolicitacao, ori => ori.MapFrom(x => x.Fluxo.FLUXO_NM))
                .ForMember(dest => dest.FluxoId, ori => ori.MapFrom(x => x.Fluxo.ID))
                .ForMember(dest => dest.Login, ori => ori.MapFrom(x => x.Usuario.NOME))
                .ForMember(dest => dest.Fornecedor, ori => ori.MapFrom(x => x.SolicitacaoCadastroFornecedor.FirstOrDefault()))
                .AfterMap((src, dest) => dest.Solicitacao_Tramites = src.WFD_SOLICITACAO_TRAMITE.Where(y => y.SOLICITACAO_ID == dest.ID).ToList());

            CreateMap<SOLICITACAO_UNSPSC, FornecedorUnspscVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.SolicitacaoId, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.UsnpscCodigo, ori => ori.MapFrom(x => x.T_UNSPSC.UNSPSC_COD))
                .ForMember(dest => dest.UsnpscDescricao, ori => ori.MapFrom(x => x.T_UNSPSC.UNSPSC_DSC))
                .ForMember(dest => dest.UsnpscId, ori => ori.MapFrom(x => x.UNSPSC_ID))
                .ForMember(dest => dest.Niv, ori => ori.MapFrom(x => x.T_UNSPSC.NIV));

            CreateMap<FORNECEDOR_UNSPSC, FornecedorUnspscVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.SolicitacaoId, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.FornecedorId, ori => ori.MapFrom(x => x.PJPF_ID))
                .ForMember(dest => dest.UsnpscCodigo, ori => ori.MapFrom(x => x.T_UNSPSC.UNSPSC_COD))
                .ForMember(dest => dest.UsnpscDescricao, ori => ori.MapFrom(x => x.T_UNSPSC.UNSPSC_DSC))
                .ForMember(dest => dest.UsnpscId, ori => ori.MapFrom(x => x.UNSPSC_ID))
                .ForMember(dest => dest.Niv, ori => ori.MapFrom(x => x.T_UNSPSC.NIV));

            CreateMap<FornecedorUnspscVM, FORNECEDOR_UNSPSC>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.PJPF_ID, ori => ori.MapFrom(x => x.FornecedorId))
                .ForMember(dest => dest.SOLICITACAO_ID, ori => ori.Ignore())
                .ForMember(dest => dest.UNSPSC_ID, ori => ori.MapFrom(x => x.UsnpscId));

            CreateMap<FORNECEDORBASE_UNSPSC, FornecedorUnspscVM>()
                 .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                 .ForMember(dest => dest.FornecedorId, ori => ori.MapFrom(x => x.PJPF_BASE_ID))
                 .ForMember(dest => dest.UsnpscCodigo, ori => ori.MapFrom(x => x.T_UNSPSC.UNSPSC_COD))
                 .ForMember(dest => dest.UsnpscDescricao, ori => ori.MapFrom(x => x.T_UNSPSC.UNSPSC_DSC))
                 .ForMember(dest => dest.UsnpscId, ori => ori.MapFrom(x => x.UNSPSC_ID))
                 .ForMember(dest => dest.Niv, ori => ori.MapFrom(x => x.T_UNSPSC.NIV));

            CreateMap<FornecedorUnspscVM, FORNECEDORBASE_UNSPSC>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.PJPF_BASE_ID, ori => ori.MapFrom(x => x.FornecedorId))
                .ForMember(dest => dest.UNSPSC_ID, ori => ori.MapFrom(x => x.UsnpscId));

            CreateMap<Fornecedor, FichaCadastralWebForLinkVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.ContratanteID, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.NomeEmpresa, ori => ori.MapFrom(x => x.RAZAO_SOCIAL ?? x.NOME))
                .ForMember(dest => dest.CNPJ_CPF, ori => ori.MapFrom(x => Mascara.MascararCNPJouCPF(x.CNPJ) ?? Mascara.MascararCNPJouCPF(x.CPF)))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RAZAO_SOCIAL ?? x.NOME))
                .ForMember(dest => dest.NomeFantasia, ori => ori.MapFrom(x => x.NOME_FANTASIA ?? x.NOME))
                //.ForMember(dest => dest.CNAE, ori => ori.MapFrom(x => x.CNAE))
                .ForMember(dest => dest.InscricaoEstadual, ori => ori.MapFrom(x => x.INSCR_ESTADUAL))
                .ForMember(dest => dest.InscricaoMunicipal, ori => ori.MapFrom(x => x.INSCR_MUNICIPAL))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.ENDERECO))
                .ForMember(dest => dest.Numero, ori => ori.MapFrom(x => x.NUMERO))
                .ForMember(dest => dest.Complemento, ori => ori.MapFrom(x => x.COMPLEMENTO))
                .ForMember(dest => dest.Cep, ori => ori.MapFrom(x => x.CEP))
                .ForMember(dest => dest.Bairro, ori => ori.MapFrom(x => x.BAIRRO))
                .ForMember(dest => dest.Cidade, ori => ori.MapFrom(x => x.CIDADE))
                .ForMember(dest => dest.Estado, ori => ori.MapFrom(x => x.UF))
                .ForMember(dest => dest.Pais, ori => ori.MapFrom(x => x.PAIS))
                .ForMember(dest => dest.ContratanteFornecedorID, ori => ori.Ignore())
                .ForMember(dest => dest.TipoLogradouro, ori => ori.Ignore())
                .ForMember(dest => dest.Observacao, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosVisao, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosGrupo, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosDescricao, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosDescricaoMudança, ori => ori.Ignore())
                .ForMember(dest => dest.TipoFornecedor, ori => ori.MapFrom(x => x.TIPO_PJPF_ID))
                .ForMember(dest => dest.DadosBancarios, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.ServicosMateriais, ori => ori.Ignore())

                .ForMember(dest => dest.DadosBloqueio, ori => ori.Ignore())
                .ForMember(dest => dest.Aprovacao, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.FornecedoresUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.Solicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.Documentos, ori => ori.Ignore())
                .ForMember(dest => dest.Bloqueio, ori => ori.Ignore())
                .ForMember(dest => dest.Desbloqueio, ori => ori.Ignore())
                .ForMember(dest => dest.Expansao, ori => ori.Ignore())
                .ForMember(dest => dest.FornecedorRobo, ori => ori.Ignore())
                .ForMember(dest => dest.Questionarios, ori => ori.Ignore())
                .ForMember(dest => dest.BancoList, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoDocumentos, ori => ori.Ignore())
                .ForMember(dest => dest.CategoriaId, ori => ori.MapFrom(x => x.WFD_CONTRATANTE_PJPF != null
                    ? x.WFD_CONTRATANTE_PJPF.FirstOrDefault(y => y.CONTRATANTE_ID == x.CONTRATANTE_ID).CATEGORIA_ID
                    : 0))
                .ForMember(dest => dest.CategoriaNome, ori => ori.MapFrom(x => x.WFD_CONTRATANTE_PJPF != null
                    ? x.WFD_CONTRATANTE_PJPF.FirstOrDefault(y => y.CONTRATANTE_ID == x.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO
                    : null))
                .ForMember(dest => dest.TipoPreenchimento, ori => ori.Ignore())
                .ForMember(dest => dest.HabilitaEdicao, ori => ori.Ignore())
                .ForMember(dest => dest.RoboReceita, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSintegra, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSimples, ori => ori.Ignore())
                .ForMember(dest => dest.FichaFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.ApenasSalvar, ori => ori.Ignore())
                .ForMember(dest => dest.DataAtualizacaoUnspsc, ori => ori.MapFrom(x => x.DT_ATUALIZACAO_UNSPSC));

            CreateMap<SOLICITACAO, FichaCadastralWebForLinkVM>()
                .ForMember(destino => destino.BloqueioId, ori => ori.Ignore())
                .ForMember(destino => destino.NomeFuncionalidade, ori => ori.Ignore())
                .ForMember(destino => destino.Mensagem, ori => ori.Ignore())
                .ForMember(destino => destino.Assunto, ori => ori.Ignore())
                .ForMember(destino => destino.AtualizacaoDocumento, ori => ori.Ignore())
                .ForMember(destino => destino.DocumentoId, ori => ori.Ignore())
                .ForMember(destino => destino.IsPjpfBaseProprio, ori => ori.Ignore())
                .ForMember(destino => destino.IsPjpfProprio, ori => ori.Ignore())
                .ForMember(destino => destino.PJPFID, ori => ori.Ignore())
                .ForMember(destino => destino.PjpfBaseId, ori => ori.Ignore())
                .ForMember(destino => destino.SolicitacaoID, ori => ori.Ignore())
                .ForMember(destino => destino.ActionOrigem, ori => ori.Ignore())
                .ForMember(destino => destino.ControllerOrigem, ori => ori.Ignore())
                .ForMember(destino => destino.CategoriaNome, ori => ori.Ignore())
                .ForMember(destino => destino.Nome, ori => ori.Ignore())
                .ForMember(destino => destino.HabilitaEdicaoUnspsc, ori => ori.Ignore())
                .ForMember(destino => destino.isCPF, ori => ori.Ignore())
                .ForMember(destino => destino.DataAtualizacaoUnspsc, ori => ori.Ignore())
                .ForMember(destino => destino.DocumentoEditavel, ori => ori.Ignore())
                .ForMember(destino => destino.TermoAceite, ori => ori.Ignore())
                .ForMember(destino => destino.TextoTermoAceite, ori => ori.Ignore())
                .ForMember(destino => destino.PrazoPreenchimento, ori => ori.Ignore())
                .ForMember(destino => destino.ChaveUrl, ori => ori.Ignore())
                .ForMember(destino => destino.DataProrrogacao, ori => ori.Ignore())
                .ForMember(destino => destino.MotivoProrrogacao, ori => ori.Ignore())
                .ForMember(destino => destino.AprovacaoProrrogacao, ori => ori.Ignore())
                .ForMember(destino => destino.ReprovacaoProrrogacao, ori => ori.Ignore())
                .ForMember(destino => destino.MensagemImportacao, ori => ori.Ignore())
                .ForMember(destino => destino.TipoFuncionalidade, ori => ori.Ignore())
                .ForMember(destino => destino.PreCadastroEnum, ori => ori.Ignore())
                .ForMember(destino => destino.ProrrogacaoPrazo, ori => ori.Ignore())
                .ForMember(destino => destino.PrazoEntrega, ori => ori.Ignore())
                .ForMember(destino => destino.Categoria, ori => ori.Ignore())
                .ForMember(destino => destino.DadosEnderecos, ori => ori.Ignore())
                .ForMember(destino => destino.Contratantes, ori => ori.Ignore())
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.ContratanteID, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.Observacao, ori => ori.MapFrom(x => String.Empty))
                .ForMember(dest => dest.NomeEmpresa, ori => ori.Ignore())
                .ForMember(dest => dest.ContratanteFornecedorID, ori => ori.Ignore())
                .ForMember(dest => dest.RazaoSocial, ori => ori.Ignore())
                .ForMember(dest => dest.NomeFantasia, ori => ori.Ignore())
                //.ForMember(dest => dest.CNAE, ori => ori.Ignore())
                .ForMember(dest => dest.CNPJ_CPF, ori => ori.Ignore())
                .ForMember(dest => dest.InscricaoEstadual, ori => ori.Ignore())
                .ForMember(dest => dest.InscricaoMunicipal, ori => ori.Ignore())
                .ForMember(dest => dest.TipoLogradouro, ori => ori.Ignore())
                .ForMember(dest => dest.Endereco, ori => ori.Ignore())
                .ForMember(dest => dest.Numero, ori => ori.Ignore())
                .ForMember(dest => dest.Complemento, ori => ori.Ignore())
                .ForMember(dest => dest.Cep, ori => ori.Ignore())
                .ForMember(dest => dest.Bairro, ori => ori.Ignore())
                .ForMember(dest => dest.Cidade, ori => ori.Ignore())
                .ForMember(dest => dest.Estado, ori => ori.Ignore())
                .ForMember(dest => dest.Pais, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosVisao, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosGrupo, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosDescricao, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosDescricaoMudança, ori => ori.Ignore())
                .ForMember(dest => dest.TipoFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.DadosBancarios, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.ServicosMateriais, ori => ori.Ignore())

                .ForMember(dest => dest.DadosBloqueio, ori => ori.Ignore())
                .ForMember(dest => dest.Aprovacao, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.FornecedoresUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.Solicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.Documentos, ori => ori.Ignore())
                .ForMember(dest => dest.Bloqueio, ori => ori.Ignore())
                .ForMember(dest => dest.Desbloqueio, ori => ori.Ignore())
                .ForMember(dest => dest.Expansao, ori => ori.Ignore())
                .ForMember(dest => dest.FornecedorRobo, ori => ori.Ignore())
                .ForMember(dest => dest.Questionarios, ori => ori.Ignore())
                .ForMember(dest => dest.BancoList, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoDocumentos, ori => ori.Ignore())
                .ForMember(dest => dest.CategoriaId, ori => ori.Ignore())
                .ForMember(dest => dest.TipoPreenchimento, ori => ori.Ignore())
                .ForMember(dest => dest.HabilitaEdicao, ori => ori.Ignore())
                .ForMember(dest => dest.RoboReceita, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSintegra, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSimples, ori => ori.Ignore())
                .ForMember(dest => dest.FichaFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.ApenasSalvar, ori => ori.Ignore());

            CreateMap<SolicitacaoCadastroFornecedor, FichaCadastralWebForLinkVM>()
                .ForMember(dest => dest.NomeEmpresa, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.PJPF_TIPO == 3
                    ? x.NOME
                    : x.RAZAO_SOCIAL))
                .ForMember(dest => dest.NomeFantasia, ori => ori.MapFrom(x => x.NOME_FANTASIA))
                //.ForMember(dest => dest.CNAE, ori => ori.MapFrom(x => x.CNAE))
                .ForMember(dest => dest.CNPJ_CPF, ori => ori.MapFrom(x => x.PJPF_TIPO == 3
                    ? Convert.ToUInt64(x.CPF).ToString(@"000\.000\.000\-00")
                    : Convert.ToUInt64(x.CNPJ).ToString(@"00\.000\.000\/0000\-00")))
                .ForMember(dest => dest.InscricaoEstadual, ori => ori.MapFrom(x => x.INSCR_ESTADUAL))
                .ForMember(dest => dest.InscricaoMunicipal, ori => ori.MapFrom(x => x.INSCR_MUNICIPAL))
                .ForMember(dest => dest.TipoLogradouro, ori => ori.MapFrom(x => x.TP_LOGRADOURO))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.ENDERECO))
                .ForMember(dest => dest.Numero, ori => ori.MapFrom(x => x.NUMERO))
                .ForMember(dest => dest.Complemento, ori => ori.MapFrom(x => x.COMPLEMENTO))
                .ForMember(dest => dest.Cep, ori => ori.MapFrom(x => x.CEP))
                .ForMember(dest => dest.Bairro, ori => ori.MapFrom(x => x.BAIRRO))
                .ForMember(dest => dest.Cidade, ori => ori.MapFrom(x => x.CIDADE))
                .ForMember(dest => dest.Estado, ori => ori.MapFrom(x => x.UF))
                .ForMember(dest => dest.Pais, ori => ori.MapFrom(x => x.PAIS))
                .ForMember(dest => dest.Observacao, ori => ori.MapFrom(x => x.OBSERVACAO))
                .ForMember(dest => dest.TipoFornecedor, ori => ori.MapFrom(x => x.PJPF_TIPO))
                .ForMember(dest => dest.Expansao, ori => ori.Ignore())
                .ForMember(dest => dest.ContratanteID, ori => ori.Ignore())
                .ForMember(dest => dest.ContratanteFornecedorID, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosVisao, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosGrupo, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosDescricao, ori => ori.Ignore())
                .ForMember(dest => dest.OutrosDadosDescricaoMudança, ori => ori.Ignore())
                .ForMember(dest => dest.DadosBancarios, ori => ori.Ignore())
                .ForMember(dest => dest.DadosContatos, ori => ori.Ignore())
                .ForMember(dest => dest.ServicosMateriais, ori => ori.Ignore())

                .ForMember(dest => dest.DadosBloqueio, ori => ori.Ignore())
                .ForMember(dest => dest.Aprovacao, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.FornecedoresUnspsc, ori => ori.Ignore())
                .ForMember(dest => dest.Solicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.Documentos, ori => ori.Ignore())
                .ForMember(dest => dest.Bloqueio, ori => ori.Ignore())
                .ForMember(dest => dest.Desbloqueio, ori => ori.Ignore())
                .ForMember(dest => dest.FornecedorRobo, ori => ori.Ignore())
                .ForMember(dest => dest.Questionarios, ori => ori.Ignore())
                .ForMember(dest => dest.BancoList, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoDocumentos, ori => ori.Ignore())
                .ForMember(dest => dest.CategoriaId, ori => ori.Ignore())
                .ForMember(dest => dest.TipoPreenchimento, ori => ori.Ignore())
                .ForMember(dest => dest.HabilitaEdicao, ori => ori.Ignore())
                .ForMember(dest => dest.RoboReceita, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSintegra, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSimples, ori => ori.Ignore())
                .ForMember(dest => dest.FichaFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.ApenasSalvar, ori => ori.Ignore());

            CreateMap<FORNECEDOR_CATEGORIA, CategoriaVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Contratante_ID, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.Codigo, ori => ori.MapFrom(x => x.CODIGO))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.DESCRICAO))
                .ForMember(dest => dest.Ativo, ori => ori.MapFrom(x => x.ATIVO))
                .ForMember(dest => dest.CH_ID, ori => ori.MapFrom(x => x.PJPF_CATEGORIA_CH_ID))
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore())
                .ForMember(dest => dest.IsentoDocumentos, ori => ori.MapFrom(x => x.ISENTO_DOCUMENTOS))
                .ForMember(dest => dest.IsentoDadosBancarios, ori => ori.MapFrom(x => x.ISENTO_DADOSBANCARIOS))
                .ForMember(dest => dest.IsentoDadosContato, ori => ori.MapFrom(x => x.ISENTO_CONTATOS))
                .ForMember(dest => dest.TemFilhos, ori => ori.MapFrom(x => x.WFD_PJPF_CATEGORIA1.Any()))
                .ForMember(dest => dest.PaiId, ori => ori.MapFrom(x => x.CATEGORIA_PAI_ID))
                .ForMember(dest => dest.DescricaoCategoriaPai, ori => ori.Ignore())
                .ForMember(dest => dest.Nivel, ori => ori.Ignore())
                .ForMember(dest => dest.TotalNiveis, ori => ori.Ignore())
                .ForMember(dest => dest.UrlNovaSubCategoria, ori => ori.Ignore());

            CreateMap<ListaDeDocumentosDeFornecedor, ListaDocumentosVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.ExigeValidade, ori => ori.MapFrom(x => x.EXIGE_VALIDADE))
                .ForMember(dest => dest.Ativo, ori => ori.MapFrom(x => x.ATIVO))
                .ForMember(dest => dest.TipoDocumento, ori => ori.MapFrom(x => x.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO))
                .ForMember(dest => dest.DescricaoDocumento, ori => ori.MapFrom(x => x.DescricaoDeDocumentos.DESCRICAO))
                .ForMember(dest => dest.Obrigatorio, ori => ori.MapFrom(x => x.OBRIGATORIO))
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore())
                .AfterMap((x, y) =>
                {
                    if (BloqueadoSemAtualizacao(x))
                        y.TipoAtualizacaoDesc = "Sem Atualização";
                    else if (BloqeadoPorValidade(x))
                        y.TipoAtualizacaoDesc = "Por Validade";
                    else if (BloqueadoPorPeriodo(x))
                        y.TipoAtualizacaoDesc = "Por Período (" + x.WFD_T_PERIODICIDADE.PERIODICIDADE_NM + ")";
                    else
                        y.TipoAtualizacaoDesc = null;
                });

            CreateMap<CategoriaVM, FORNECEDOR_CATEGORIA>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.CONTRATANTE_ID, ori => ori.MapFrom(x => x.Contratante_ID))
                .ForMember(dest => dest.CODIGO, ori => ori.MapFrom(x => x.Codigo))
                .ForMember(dest => dest.DESCRICAO, ori => ori.MapFrom(x => x.Descricao))
                .ForMember(dest => dest.ATIVO, ori => ori.MapFrom(x => x.Ativo))
                .ForMember(dest => dest.PJPF_CATEGORIA_CH_ID, ori => ori.MapFrom(x => x.CH_ID))
                .ForMember(dest => dest.WFD_CONTRATANTE, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_CONTRATANTE_PJPF, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_BASE, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_CATEGORIA_CH, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOL_CAD_PJPF, ori => ori.Ignore())
                .ForMember(dest => dest.ListaDeDocumentosDeFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.ISENTO_DOCUMENTOS, ori => ori.MapFrom(x => x.IsentoDocumentos))
                .ForMember(dest => dest.ISENTO_DADOSBANCARIOS, ori => ori.MapFrom(x => x.IsentoDadosBancarios))
                .ForMember(dest => dest.ISENTO_CONTATOS, ori => ori.MapFrom(x => x.IsentoDadosContato))
                .ForMember(dest => dest.WFD_PJPF_CATEGORIA1, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_CATEGORIA2, ori => ori.Ignore())
                .ForMember(dest => dest.CATEGORIA_PAI_ID, ori => ori.MapFrom(x => x.PaiId))
                .ForMember(dest => dest.QIC_QUESTIONARIO_CATEGORIA, ori => ori.Ignore());

            CreateMap<FORNECEDORBASE, FornecedoresVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => x.CNPJ ?? x.CPF))
                .ForMember(dest => dest.Contatos, ori => ori.MapFrom(x => x.WFD_PJPF_BASE_CONTATOS))
                .ForMember(dest => dest.ContratanteID, ori => ori.Ignore())
                .ForMember(dest => dest.Empresa, ori => ori.Ignore())
                .ForMember(dest => dest.NomeEmpresa, ori => ori.Ignore())
                .ForMember(dest => dest.Compras, ori => ori.Ignore())
                .ForMember(dest => dest.Categoria, ori => ori.Ignore())
                .ForMember(dest => dest.Solicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.TudoAmpliado, ori => ori.Ignore())
                .ForMember(dest => dest.Bloqueado, ori => ori.Ignore())
                .ForMember(dest => dest.DataNascimento, ori => ori.Ignore())
                .ForMember(dest => dest.CodigoERP, ori => ori.Ignore())
                .ForMember(dest => dest.Status, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.Contato, ori => ori.Ignore())
                .ForMember(dest => dest.Bancos, ori => ori.Ignore())
                .ForMember(dest => dest.Anexos, ori => ori.Ignore())
                .ForMember(dest => dest.RoboReceitaCNPJ, ori => ori.Ignore())
                .ForMember(dest => dest.RoboReceitaCPF, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSintegra, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSimples, ori => ori.Ignore())
                .ForMember(dest => dest.TipoCadastro, ori => ori.Ignore())
                .ForMember(dest => dest.NomeContato, ori => ori.Ignore())
                .ForMember(dest => dest.Email, ori => ori.Ignore())
                .ForMember(dest => dest.TipoFornecedor, ori => ori.Ignore())
                .AfterMap((src, dest) => dest.Telefone = !string.IsNullOrEmpty(dest.Telefone) ? src.TELEFONE.PadLeft(13, ' ') : src.TELEFONE)
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore());

            CreateMap<FornecedoresVM, SOLICITACAO>()
                .ForMember(dest => dest.CONTRATANTE_ID, ori => ori.MapFrom(x => x.Empresa))
                .ForMember(dest => dest.SOLICITACAO_DT_CRIA, ori => ori.Ignore())
                .ForMember(dest => dest.USUARIO_ID, ori => ori.Ignore())
                .ForMember(dest => dest.SOLICITACAO_STATUS_ID, ori => ori.Ignore())
                .ForMember(dest => dest.MOTIVO, ori => ori.Ignore())
                .ForMember(dest => dest.Fornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.DocumentosDoFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_ROBO_LOG, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_SOLICITACAO_DOCUMENTOS, ori => ori.Ignore())
                .ForMember(dest => dest.SOLICITACAO_BLOQUEIO, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoCadastroFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOL_DESBLOQ, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoModificacaoDadosBancario, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOL_MOD_DGERAIS_SEQ, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOLICITACAO_TRAMITE, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_INFORM_COMPL, ori => ori.Ignore())
                .ForMember(dest => dest.Usuario, ori => ori.Ignore())
                .ForMember(dest => dest.Fluxo, ori => ori.Ignore())
                .ForMember(dest => dest.FLUXO_ID, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOLICITACAO_STATUS, ori => ori.Ignore())
                .ForMember(dest => dest.PJPF_ID, ori => ori.Ignore())
                .ForMember(dest => dest.TP_PJPF, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoModificacaoDadosContato, ori => ori.MapFrom(x => x.Contatos))
                .ForMember(dest => dest.Contratante, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoDeDocumentos, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOL_MOD_ENDERECO, ori => ori.Ignore())
                .ForMember(dest => dest.ROBO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOL_MENSAGEM, ori => ori.Ignore());

            CreateMap<FornecedorContatosVM, SolicitacaoModificacaoDadosContato>()
                .ForMember(dest => dest.NOME, ori => ori.MapFrom(x => x.Nome))
                .ForMember(dest => dest.EMAIL, ori => ori.MapFrom(x => x.Email))
                .ForMember(dest => dest.TELEFONE, ori => ori.Ignore())
                .ForMember(dest => dest.CELULAR, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_CONTATOS, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOLICITACAO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_T_TP_CONTATO, ori => ori.Ignore());

            CreateMap<UsuarioAdministracaoModel, Usuario>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.CONTRATANTE_ID, ori => ori.MapFrom(x => x.ContratanteId))
                .ForMember(dest => dest.NOME, ori => ori.MapFrom(x => x.Nome))
                .ForMember(dest => dest.EMAIL, ori => ori.MapFrom(x => x.Email))
                .ForMember(dest => dest.SENHA, ori => ori.MapFrom(x => x.Senha))
                .ForMember(dest => dest.TROCAR_SENHA, ori => ori.MapFrom(x => x.TrocarSenha))
                .ForMember(dest => dest.CPF_CNPJ, ori => ori.MapFrom(x => !string.IsNullOrEmpty(x.CPF)?
                x.CPF
                .Replace(".", "")
                .Replace("-", "")
                .Replace("/", "")
                : ""
                ))
                .ForMember(dest => dest.ATIVO, ori => ori.MapFrom(x => x.Ativo))
                .ForMember(dest => dest.CARGO, ori => ori.MapFrom(x => x.Cargo))
                .ForMember(dest => dest.LOGIN, ori => ori.MapFrom(x => x.Login))
                .ForMember(dest => dest.LOGIN_SSO, ori => ori.MapFrom(x => x.LoginSSO))
                .ForMember(dest => dest.DOMINIO, ori => ori.MapFrom(x => x.Dominio))
                .ForMember(dest => dest.DAT_NASCIMENTO, ori => ori.MapFrom(x => x.DataNascimento))
                .ForMember(dest => dest.PRINCIPAL, ori => ori.MapFrom(x => x.Principal))
                .ForMember(dest => dest.PRIMEIRO_ACESSO, ori => ori.MapFrom(x => x.PrimeiroAcesso))
                .ForMember(dest => dest.DT_ATIVACAO, ori => ori.MapFrom(x => x.DataAtivacao))
                .ForMember(dest => dest.DT_CRIACAO, ori => ori.MapFrom(x => x.DataCriacao))
                .ForMember(dest => dest.CONTA_TENTATIVA, ori => ori.MapFrom(x => x.ContaTentativa))
                .ForMember(dest => dest.WAC_PERFIL, ori => ori.MapFrom(x => x.PerfilList))
                .ForMember(dest => dest.Contratante, ori => ori.MapFrom(x => x.Contratante))
                .ForMember(dest => dest.WFD_CONTRATANTE1, ori => ori.MapFrom(x => x.ContratanteList))
                .ForMember(dest => dest.WFL_PAPEL, ori => ori.MapFrom(x => x.PapelList))
                .ForMember(dest => dest.EXPIRA_EM_DIAS, ori => ori.MapFrom(x => x.ExpiraEmDias))
                .ForMember(dest => dest.WFD_SOLICITACAO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOLICITACAO_TRAMITE, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_USUARIO_SENHAS_HIST, ori => ori.Ignore())
                .ForMember(dest => dest.EMAIL_ALTERNATIVO, ori => ori.Ignore());

            CreateMap<SOLICITACAO_PRORROGACAO, ProrrogacaoPrazoVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Aprovado, ori => ori.MapFrom(x => x.APROVADO))
                .ForMember(dest => dest.DataProrrogacao, ori => ori.MapFrom(x => x.DT_PRORROGACAO_PRAZO.ToShortDateString()))
                .ForMember(dest => dest.DataSolicitacaoProrrogacao, ori => ori.MapFrom(x => x.DT_SOL_PRORROGACAO))
                .ForMember(dest => dest.Motivo, ori => ori.MapFrom(x => x.MOTIVO_PRORROGACAO))
                .ForMember(dest => dest.MotivoReprovacao, ori => ori.MapFrom(x => x.MOTIVO_REPROVACAO))
                .ForMember(dest => dest.PrazoPreenchimento, ori => ori.Ignore())
                .ForMember(dest => dest.StDataProrrogacao, ori => ori.MapFrom(x => x.DT_PRORROGACAO_PRAZO.ToShortDateString()))
                .AfterMap((src, dest) => dest.Status = src.APROVADO != null ? (bool)src.APROVADO ? "Aprovado" : "Reprovado" : "Aguardando Aprovação...");


        }

        private static bool BloqueadoPorPeriodo(ListaDeDocumentosDeFornecedor x)
        {
            return x.EXIGE_VALIDADE == false && x.PERIODICIDADE_ID != null;
        }

        private static bool BloqeadoPorValidade(ListaDeDocumentosDeFornecedor x)
        {
            return x.EXIGE_VALIDADE && x.PERIODICIDADE_ID == null;
        }

        private static bool BloqueadoSemAtualizacao(ListaDeDocumentosDeFornecedor x)
        {
            return x.EXIGE_VALIDADE == false && x.PERIODICIDADE_ID == null;
        }

        private void MapearBanco()
        {

            CreateMap<SolicitacaoModificacaoDadosBancario, DadosBancariosVM>()
                .ForMember(dest => dest.BancoSolicitacaoID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.BancoPJPFID, ori => ori.MapFrom(x => x.BANCO_PJPF_ID))
                .ForMember(dest => dest.NomeBanco, ori => ori.MapFrom(x => x.T_BANCO.BANCO_NM))
                .ForMember(dest => dest.Agencia, ori => ori.MapFrom(x => x.AGENCIA))
                .ForMember(dest => dest.Digito, ori => ori.MapFrom(x => x.AG_DV))
                .ForMember(dest => dest.Banco, ori => ori.MapFrom(x => x.BANCO_ID))
                .ForMember(dest => dest.ContaCorrente, ori => ori.MapFrom(x => x.CONTA))
                .ForMember(dest => dest.ContaCorrenteDigito, ori => ori.MapFrom(x => x.CONTA_DV))
                .ForMember(dest => dest.NomeArquivo, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                .ForMember(dest => dest.ArquivoID, ori => ori.MapFrom(x => x.ARQUIVO_ID))
                .ForMember(dest => dest.DataUpload, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.DATA_UPLOAD))
                .ForMember(dest => dest.SolicitacaoID, ori => ori.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.Bancos, ori => ori.Ignore());

            CreateMap<SolicitacaoModificacaoDadosContato, DadosContatoVM>()
                .ForMember(dest => dest.ContatoID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.PjPfId, ori => ori.MapFrom(x => x.CONTATO_PJPF_ID))
                .ForMember(dest => dest.NomeContato, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.EmailContato, ori => ori.MapFrom(x => x.EMAIL))
                .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.TELEFONE))
                .ForMember(dest => dest.Celular, ori => ori.MapFrom(x => x.CELULAR))
                .ForMember(dest => dest.Estrangeiro, ori => ori.Ignore());

            CreateMap<FORNECEDOR_SOLICITACAO_DOCUMENTOS, SolicitacaoDocumentosVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Documento,
                    ori =>
                        ori.MapFrom(
                            x =>
                                x.WFD_PJPF_LISTA_DOCUMENTOS.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO +
                                " - " + x.WFD_PJPF_LISTA_DOCUMENTOS.DescricaoDeDocumentos.DESCRICAO))
                .ForMember(dest => dest.PorValidade, ori => ori.MapFrom(x => x.WFD_PJPF_LISTA_DOCUMENTOS.EXIGE_VALIDADE))
                .ForMember(dest => dest.DataValidade, ori => ori.MapFrom(x => x.DATA_VENCIMENTO))
                .ForMember(dest => dest.NomeArquivo, ori => ori.MapFrom(x => x.NOME_ARQUIVO))
                .ForMember(dest => dest.ArquivoID, ori => ori.MapFrom(x => x.PJPF_ARQUIVO_ID))
                .ForMember(dest => dest.DataUpload, ori => ori.Ignore())
                .ForMember(dest => dest.Arquivo, ori => ori.Ignore())
                .ForMember(dest => dest.SituacaoID, ori => ori.Ignore())
                .ForMember(dest => dest.Situacao, ori => ori.Ignore())
                .ForMember(dest => dest.Obrigatorio, ori => ori.Ignore())
                .ForMember(dest => dest.ListaDocumentosID, ori => ori.Ignore())
                .ForMember(dest => dest.SolicitacaoID, ori => ori.Ignore());

            CreateMap<Contratante, ContratanteAdministracaoModel>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.TipoCadastroId, ori => ori.MapFrom(x => x.TIPO_CADASTRO_ID))
                .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => x.CNPJ))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(dest => dest.NomeFantasia, ori => ori.MapFrom(x => x.NOME_FANTASIA))
                .ForMember(dest => dest.DataCadastro, ori => ori.MapFrom(x => x.DATA_CADASTRO))
                .ForMember(dest => dest.LogoFoto, ori => ori.MapFrom(x => x.LOGO_FOTO))
                .ForMember(dest => dest.ExtensaoImagem, ori => ori.MapFrom(x => x.EXTENSAO_IMAGEM))
                .ForMember(dest => dest.Estilo, ori => ori.MapFrom(x => x.ESTILO))
                //.ForMember(dest => dest.ContranteCodERP, ori => ori.MapFrom(x => x.CONTRANTE_COD_ERP))
                .ForMember(dest => dest.PerfilList, ori => ori.MapFrom(x => x.WAC_PERFIL))
                .ForMember(dest => dest.ContratanteConfig, ori => ori.MapFrom(x => x.WFD_CONTRATANTE_CONFIG))
                .ForMember(dest => dest.TipoCadastro, ori => ori.MapFrom(x => x.WFD_TIPO_CADASTRO))
                .ForMember(dest => dest.UsuarioList, ori => ori.MapFrom(x => x.Usuario))
                .ForMember(dest => dest.PapelList, ori => ori.MapFrom(x => x.WFL_PAPEL))
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.Selecionado, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore());

            CreateMap<Contratante, ContratanteVM>()
                    .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                    .ForMember(dest => dest.RAZAO_SOCIAL, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                    .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => x.CNPJ));

            CreateMap<Contratante, ContratantesVM>()
                    .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                    .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                    .ForMember(dest => dest.CNPJouCPF, ori => ori.MapFrom(x => x.CNPJ))
                    .ForMember(dest => dest.NomeFantasia, ori => ori.MapFrom(x => x.NOME_FANTASIA))
                    .ForMember(dest => dest.TipoCadastroId, ori => ori.MapFrom(x => x.TIPO_CADASTRO_ID))
                    .ForMember(dest => dest.ParamCripto, ori => ori.Ignore());
        }

        private void MapearWacAplicacao()
        {
            CreateMap<APLICACAO, AplicacaoVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.APLICACAO_NM))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.APLICACAO_DSC));

            CreateMap<APLICACAO, AplicacaoAdministracaoModel>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.APLICACAO_NM))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.APLICACAO_DSC))
                .ForMember(dest => dest.FuncaoList, ori => ori.MapFrom(x => x.WAC_FUNCAO))
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore())
                //.ForMember(d => d.Url, opt => opt.ResolveUsing(res => res.Context.Options.Items["Url"]))
                .AfterMap((dest, ori) => ori.Validar());

        }

        private void MapearWacPerfil()
        {
            CreateMap<Perfil, PerfilVM>()
                .ForMember(dest => dest.Contratante, ori => ori.MapFrom(x => x.WFD_CONTRATANTE))
                .ForMember(dest => dest.Obrigatorio, ori => ori.Ignore())
                .ForMember(dest => dest.Leitura, ori => ori.Ignore())
                .ForMember(dest => dest.Escrita, ori => ori.Ignore())
                .ForMember(dest => dest.PAPEL_NM, ori => ori.Ignore())
                .ForMember(dest => dest.PAPEL_DSC, ori => ori.Ignore());

            CreateMap<Perfil, PerfilAdministracaoModel>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.PERFIL_NM))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.PERFIL_DSC))
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.Contratante, ori => ori.MapFrom(x => x.WFD_CONTRATANTE))
                .ForMember(dest => dest.FuncaoList, ori => ori.MapFrom(x => x.WAC_FUNCAO))
                .ForMember(dest => dest.UsuarioList, ori => ori.MapFrom(x => x.WFD_USUARIO))
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.Selecionado, ori => ori.Ignore())
                .ForMember(dest => dest.SelectedGroupsFuncao, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemErro, ori => ori.Ignore())
                //.ForMember(d => d.Url, opt => opt.ResolveUsing(res => res.Context.Options.Items["Url"]))
                .AfterMap((dest, ori) => ori.Validar());



            CreateMap<PerfilAdministracaoModel, Perfil>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.PERFIL_NM, ori => ori.MapFrom(x => x.Nome))
                .ForMember(dest => dest.PERFIL_DSC, ori => ori.MapFrom(x => x.Descricao))
                .ForMember(dest => dest.CONTRATANTE_ID, ori => ori.MapFrom(x => x.ContratanteId))
                .ForMember(dest => dest.WFD_CONTRATANTE, ori => ori.MapFrom(x => x.Contratante))
                .ForMember(dest => dest.WAC_FUNCAO, ori => ori.MapFrom(x => x.FuncaoList))
                .ForMember(dest => dest.WFD_USUARIO, ori => ori.MapFrom(x => x.UsuarioList));
            CreateMap<ContratanteAdministracaoModel, Contratante>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.TIPO_CADASTRO_ID, ori => ori.MapFrom(x => x.TipoCadastroId))
                .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => x.CNPJ))
                .ForMember(dest => dest.RAZAO_SOCIAL, ori => ori.MapFrom(x => x.RazaoSocial))
                .ForMember(dest => dest.NOME_FANTASIA, ori => ori.MapFrom(x => x.NomeFantasia))
                .ForMember(dest => dest.DATA_CADASTRO, ori => ori.MapFrom(x => x.DataCadastro))
                .ForMember(dest => dest.LOGO_FOTO, ori => ori.MapFrom(x => x.LogoFoto))
                .ForMember(dest => dest.EXTENSAO_IMAGEM, ori => ori.MapFrom(x => x.ExtensaoImagem))
                .ForMember(dest => dest.ESTILO, ori => ori.MapFrom(x => x.Estilo))
                //.ForMember(dest => dest.CONTRANTE_COD_ERP, ori => ori.MapFrom(x => x.ContranteCodERP))
                .ForMember(dest => dest.WAC_PERFIL, ori => ori.MapFrom(x => x.PerfilList))
                .ForMember(dest => dest.WFD_CONTRATANTE_CONFIG, ori => ori.MapFrom(x => x.ContratanteConfig))
                .ForMember(dest => dest.WFD_TIPO_CADASTRO, ori => ori.MapFrom(x => x.TipoCadastro))
                .ForMember(dest => dest.Usuario, ori => ori.MapFrom(x => x.UsuarioList))
                .ForMember(dest => dest.WFL_PAPEL, ori => ori.MapFrom(x => x.PapelList))
                .ForMember(dest => dest.ATIVO, ori => ori.Ignore())
                .ForMember(dest => dest.ATIVO_DT, ori => ori.Ignore())
                .ForMember(dest => dest.QIC_QUESTIONARIO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_CONTRATANTE_ORG_COMPRAS, ori => ori.Ignore())

                .ForMember(dest => dest.WFD_CONTRATANTE_CONFIG_EMAIL, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_CONTRATANTE_LOG, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_CONTRATANTE_PJPF, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_DESCRICAO_DOCUMENTOS, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_DESTINATARIO, ori => ori.Ignore())

                .ForMember(dest => dest.WFD_PJPF_BASE, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_CATEGORIA, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_SOLICITACAO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOLICITACAO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_TIPO_DOCUMENTOS, ori => ori.Ignore())
                .ForMember(dest => dest.WFL_FLUXO_SEQUENCIA, ori => ori.Ignore())
                .ForMember(dest => dest.WFL_FLUXO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_GRUPO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_USUARIO1, ori => ori.Ignore())
                .ForMember(dest => dest.WAC_FUNCAO, ori => ori.Ignore());
        }

        private void MapearWacPapel()
        {
            CreateMap<Papel, PapelModel>()
                .ForMember(dest => dest.Contratante, ori => ori.MapFrom(x => x.WFD_CONTRATANTE));
            CreateMap<Papel, PapelAdministracaoModel>()
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.PAPEL_NM))
                .ForMember(dest => dest.Sigla, ori => ori.MapFrom(x => x.PAPEL_SGL))
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.Contratante, ori => ori.MapFrom(x => x.WFD_CONTRATANTE))
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.TipoId, ori => ori.MapFrom(x => x.PAPEL_TP_ID))
                .ForMember(dest => dest.Selecionado, ori => ori.Ignore())
                .ForMember(dest => dest.UsuarioList, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore())
                //.ForMember(d => d.Url, opt => opt.ResolveUsing(res => res.Context.Options.Items["Url"]))
                .AfterMap((dest, ori) => ori.Validar())
                ;
            CreateMap<PapelAdministracaoModel, Papel>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.CONTRATANTE_ID, ori => ori.MapFrom(x => x.ContratanteId))
                .ForMember(dest => dest.PAPEL_SGL, ori => ori.MapFrom(x => x.Sigla))
                .ForMember(dest => dest.PAPEL_NM, ori => ori.MapFrom(x => x.Nome))
                .ForMember(dest => dest.PAPEL_TP_ID, ori => ori.MapFrom(x => x.TipoId))
                .ForMember(dest => dest.WFD_CONTRATANTE, ori => ori.MapFrom(x => x.Contratante))
                .ForMember(dest => dest.WFD_USUARIO, ori => ori.MapFrom(x => x.UsuarioList))
                .ForMember(dest => dest.QIC_QUEST_ABA_PERG_PAPEL, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOLICITACAO_TRAMITE, ori => ori.Ignore())
                .ForMember(dest => dest.WFL_FLUXO_SEQUENCIA, ori => ori.Ignore())
                .ForMember(dest => dest.WFL_FLUXO_SEQUENCIA1, ori => ori.Ignore())
                .ForMember(dest => dest.TipoDePapel, ori => ori.Ignore());
            //.ForMember(dest => dest.WFL_FLUXO_SEQUENCIA2, ori => ori.Ignore());
        }

        private void MapearWacFuncao()
        {
            CreateMap<FUNCAO, FuncaoAdministracaoModel>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Codigo, ori => ori.MapFrom(x => x.CODIGO))
                .ForMember(dest => dest.AplicacaoId, ori => ori.MapFrom(x => x.APLICACAO_ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.FUNCAO_NM))
                .ForMember(dest => dest.Tela, ori => ori.MapFrom(x => x.FUNCAO_TELA))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.FUNCAO_DSC))
                .ForMember(dest => dest.FuncaoPaiId, ori => ori.MapFrom(x => x.FUNCAO_PAI))
                .ForMember(dest => dest.Aplicacao, ori => ori.MapFrom(x => x.WAC_APLICACAO))
                .ForMember(dest => dest.FuncaoList, ori => ori.MapFrom(x => x.FUNCOES))
                .ForMember(dest => dest.PerfilList, ori => ori.MapFrom(x => x.WAC_PERFIL))
                .ForMember(dest => dest.FuncaoPai, ori => ori.MapFrom(x => x.FUNCAOPRINCIPAL))
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.PerfilId, ori => ori.Ignore())
                .ForMember(dest => dest.Selecionado, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore())
                //.ForMember(d => d.Url, opt => opt.ResolveUsing(res => res.Context.Options.Items["Url"]))
                .AfterMap((dest, ori) => ori.Validar());
            CreateMap<FuncaoAdministracaoModel, FUNCAO>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.FUNCAO_NM, ori => ori.MapFrom(x => x.Nome))
                .ForMember(dest => dest.FUNCAO_DSC, ori => ori.MapFrom(x => x.Descricao))
                .ForMember(dest => dest.APLICACAO_ID, ori => ori.MapFrom(x => x.AplicacaoId))
                .ForMember(dest => dest.CODIGO, ori => ori.MapFrom(x => x.Codigo))
                .ForMember(dest => dest.WAC_APLICACAO, ori => ori.MapFrom(x => x.Aplicacao))
                .ForMember(dest => dest.FUNCAO_PAI, ori => ori.Ignore())
                .ForMember(dest => dest.FUNCOES, ori => ori.Ignore())
                .ForMember(dest => dest.FUNCAOPRINCIPAL, ori => ori.Ignore())
                .ForMember(dest => dest.WAC_PERFIL, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_CONTRATANTE, ori => ori.Ignore());
            CreateMap<AplicacaoAdministracaoModel, APLICACAO>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.APLICACAO_NM, ori => ori.MapFrom(x => x.Nome))
                .ForMember(dest => dest.APLICACAO_DSC, ori => ori.MapFrom(x => x.Descricao))
                .ForMember(dest => dest.WAC_FUNCAO, ori => ori.Ignore());
        }

        private void MapearWfdUsuarioSenhasHist()
        {
            CreateMap<USUARIO_SENHAS, UsuarioAdministracaoModel>()
                .ForMember(dest => dest.ContratanteId, ori => ori.Ignore())
                .ForMember(dest => dest.ContaTentativa, ori => ori.Ignore())
                .ForMember(dest => dest.Email, ori => ori.Ignore())
                .ForMember(dest => dest.Nome, ori => ori.Ignore())
                .ForMember(dest => dest.TrocarSenha, ori => ori.Ignore())
                .ForMember(dest => dest.CPF, ori => ori.Ignore())
                .ForMember(dest => dest.Cargo, ori => ori.Ignore())
                .ForMember(dest => dest.Login, ori => ori.Ignore())
                .ForMember(dest => dest.LoginSSO, ori => ori.Ignore())
                .ForMember(dest => dest.Dominio, ori => ori.Ignore())
                .ForMember(dest => dest.Administrador, ori => ori.Ignore())
                .ForMember(dest => dest.Ativo, ori => ori.Ignore())
                .ForMember(dest => dest.PrimeiroAcesso, ori => ori.Ignore())
                .ForMember(dest => dest.DataNascimento, ori => ori.Ignore())
                .ForMember(dest => dest.DataAtivacao, ori => ori.Ignore())
                .ForMember(dest => dest.DataCriacao, ori => ori.Ignore())
                .ForMember(dest => dest.Contratante, ori => ori.Ignore())
                .ForMember(dest => dest.PerfilList, ori => ori.Ignore())
                .ForMember(dest => dest.ContratanteList, ori => ori.Ignore())
                .ForMember(dest => dest.PapelList, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.SelectedGroupsPapel, ori => ori.Ignore())
                .ForMember(dest => dest.SelectedGroupsPerfil, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore())
                .ForMember(dest => dest.ExpiraEmDias, ori => ori.Ignore())
                .ForMember(dest => dest.Principal, ori => ori.Ignore());
        }

        private void MapearWfdUsuario()
        {
            CreateMap<Usuario, UsuarioModel>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.Email, ori => ori.MapFrom(x => x.EMAIL))
                .ForMember(dest => dest.Senha, ori => ori.MapFrom(x => x.SENHA))
                .ForMember(dest => dest.DatNascimento, ori => ori.MapFrom(x => x.DAT_NASCIMENTO))
                .ForMember(dest => dest.Principal, ori => ori.MapFrom(x => x.PRINCIPAL))
                .ForMember(dest => dest.TrocarSenha, ori => ori.MapFrom(x => x.TROCAR_SENHA))
                .ForMember(dest => dest.CPF, ori => ori.MapFrom(x => x.CPF_CNPJ))
                .ForMember(dest => dest.Ativo, ori => ori.MapFrom(x => x.ATIVO))
                .ForMember(dest => dest.Cargo, ori => ori.MapFrom(x => x.CARGO))
                .ForMember(dest => dest.Login, ori => ori.MapFrom(x => x.LOGIN))
                .ForMember(dest => dest.LoginSSO, ori => ori.MapFrom(x => x.LOGIN_SSO))
                .ForMember(dest => dest.Dominio, ori => ori.MapFrom(x => x.DOMINIO))
                .ForMember(dest => dest.PrimeiroAcesso, ori => ori.MapFrom(x => x.PRIMEIRO_ACESSO))
                .ForMember(dest => dest.DtAtivacao, ori => ori.MapFrom(x => x.DT_ATIVACAO))
                .ForMember(dest => dest.DtCriacao, ori => ori.MapFrom(x => x.DT_CRIACAO))
                .ForMember(dest => dest.ContaTentativa, ori => ori.MapFrom(x => x.CONTA_TENTATIVA))
                .ForMember(dest => dest.Contratante, ori => ori.MapFrom(x => x.Contratante))
                .ForMember(dest => dest.ContratanteList, ori => ori.Ignore())
                .ForMember(dest => dest.PapelModelList, ori => ori.MapFrom(x => x.WFL_PAPEL))
                .ForMember(dest => dest.PerfilModelList, ori => ori.MapFrom(x => x.WAC_PERFIL))
                .ForMember(dest => dest.ContratantesModelList, ori => ori.Ignore())
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.Papel, ori => ori.Ignore())
                .ForMember(dest => dest.PapelModelId, ori => ori.Ignore())
                .ForMember(dest => dest.Perfil, ori => ori.Ignore())
                .ForMember(dest => dest.PerfilModelId, ori => ori.Ignore())
                .ForMember(dest => dest.PapelList, ori => ori.Ignore())
                .ForMember(dest => dest.PerfilList, ori => ori.Ignore());

            CreateMap<Usuario, UsuarioAdministracaoModel>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.ContaTentativa, ori => ori.MapFrom(x => x.CONTA_TENTATIVA))
                .ForMember(dest => dest.Email, ori => ori.MapFrom(x => x.EMAIL))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.Senha, ori => ori.MapFrom(x => x.SENHA))
                .ForMember(dest => dest.TrocarSenha, ori => ori.MapFrom(x => x.TROCAR_SENHA))
                .ForMember(dest => dest.CPF, ori => ori.MapFrom(x => string.IsNullOrEmpty(x.CPF_CNPJ) ? null : Convert.ToInt64(x.CPF_CNPJ).ToString(@"000\.000\.000\-00")))
                .ForMember(dest => dest.Cargo, ori => ori.MapFrom(x => x.CARGO))
                .ForMember(dest => dest.Login, ori => ori.MapFrom(x => x.LOGIN))
                .ForMember(dest => dest.LoginSSO, ori => ori.MapFrom(x => x.LOGIN_SSO))
                .ForMember(dest => dest.Dominio, ori => ori.MapFrom(x => x.DOMINIO))
                .ForMember(dest => dest.Administrador, ori => ori.MapFrom(x => x.PRINCIPAL))
                .ForMember(dest => dest.Ativo, ori => ori.MapFrom(x => x.ATIVO))
                .ForMember(dest => dest.DataNascimento, ori => ori.MapFrom(x => x.DAT_NASCIMENTO.ToString().Replace(" 00:00:00", "")))
                .ForMember(dest => dest.PrimeiroAcesso, ori => ori.MapFrom(x => x.PRIMEIRO_ACESSO))
                .ForMember(dest => dest.DataAtivacao, ori => ori.MapFrom(x => x.DT_ATIVACAO))
                .ForMember(dest => dest.DataCriacao, ori => ori.MapFrom(x => x.DT_CRIACAO))
                .ForMember(dest => dest.Contratante, ori => ori.MapFrom(x => x.Contratante))
                .ForMember(dest => dest.PerfilList, ori => ori.MapFrom(x => x.WAC_PERFIL))
                .ForMember(dest => dest.ContratanteList, ori => ori.MapFrom(x => x.WFD_CONTRATANTE1))
                .ForMember(dest => dest.PapelList, ori => ori.MapFrom(x => x.WFL_PAPEL))
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.SelectedGroupsPapel, ori => ori.Ignore())
                .ForMember(dest => dest.SelectedGroupsPerfil, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.ExpiraEmDias, ori => ori.MapFrom(x => x.EXPIRA_EM_DIAS))
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore());

            CreateMap<Usuario, Autenticado>()
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.TipoContratante, ori => ori.MapFrom(x => x.Contratante.TIPO_CADASTRO_ID))
                .ForMember(dest => dest.Principal, ori => ori.MapFrom(x => (bool)x.PRINCIPAL))
                .ForMember(dest => dest.UsuarioId, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.NomeCompletoUsuario, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.Estilo, ori => ori.MapFrom(x => x.Contratante.ESTILO))
                .ForMember(dest => dest.SolicitaDocumentos, ori => ori.MapFrom(x => x.Contratante.WFD_CONTRATANTE_CONFIG.SOLICITA_DOCS))
                .ForMember(dest => dest.SolicitaFichaCadastral, ori => ori.MapFrom(x => x.Contratante.WFD_CONTRATANTE_CONFIG.SOLICITA_FICHA_CAD))
                .ForMember(dest => dest.Perfil, ori => ori.MapFrom(x => x.WAC_PERFIL.ToList().Select(y => y.ID)))
                .ForMember(dest => dest.NomeReduzidoUsuario, ori => ori.Ignore())
                .ForMember(dest => dest.Imagem, ori => ori.Ignore())
                .ForMember(dest => dest.NomeEmpresa, ori => ori.Ignore())
                .ForMember(dest => dest.Grupo, ori => ori.Ignore())
                .AfterMap((src, dest) => dest.NomeReduzidoUsuario = src.NOME != null
                ? src.NOME.Substring(0, (src.NOME.IndexOf(" ", StringComparison.Ordinal) > -1) ? src.NOME.IndexOf(" ", StringComparison.Ordinal) : src.NOME.Length)
                : src.NOME);

            CreateMap<UsuarioVM, Usuario>()
                    .ForMember(dest => dest.CONTRATANTE_ID, ori => ori.MapFrom(x => x.IdContratante))
                    .ForMember(dest => dest.NOME, ori => ori.MapFrom(x => x.Nome))
                    .ForMember(dest => dest.EMAIL, ori => ori.MapFrom(x => x.Email))
                    .ForMember(dest => dest.SENHA, ori => ori.MapFrom(x => PasswordHash.CreateHash(x.Senha)))
                    .ForMember(dest => dest.CPF_CNPJ, ori => ori.MapFrom(x => x.CPF))
                    .ForMember(dest => dest.LOGIN, ori => ori.MapFrom(x => x.Login))
                    .ForMember(dest => dest.CARGO, ori => ori.MapFrom(x => x.Cargo))
                    .ForMember(dest => dest.DAT_NASCIMENTO, ori => ori.Ignore())
                    .ForMember(dest => dest.ATIVO, ori => ori.Ignore())
                    .ForMember(dest => dest.LOGIN_SSO, ori => ori.Ignore())
                    .ForMember(dest => dest.DOMINIO, ori => ori.Ignore())
                    .ForMember(dest => dest.PRIMEIRO_ACESSO, ori => ori.Ignore())
                    .ForMember(dest => dest.DT_ATIVACAO, ori => ori.Ignore())
                    .ForMember(dest => dest.DT_CRIACAO, ori => ori.Ignore())
                    .ForMember(dest => dest.CONTA_TENTATIVA, ori => ori.Ignore())
                    .ForMember(dest => dest.Contratante, ori => ori.Ignore())
                    .ForMember(dest => dest.WFD_SOLICITACAO, ori => ori.Ignore())
                    .ForMember(dest => dest.WFD_SOLICITACAO_TRAMITE, ori => ori.Ignore())
                    .ForMember(dest => dest.WFD_USUARIO_SENHAS_HIST, ori => ori.Ignore())
                    .ForMember(dest => dest.WAC_PERFIL, ori => ori.Ignore())
                    .ForMember(dest => dest.WFD_CONTRATANTE1, ori => ori.Ignore())
                    .ForMember(dest => dest.WFL_PAPEL, ori => ori.Ignore())
                    .ForMember(dest => dest.EMAIL_ALTERNATIVO, ori => ori.Ignore())
                    .AfterMap((src, dest) => dest.PRINCIPAL = false)
                    .AfterMap((src, dest) => dest.ATIVO = true)
                    .AfterMap((src, dest) => dest.CONTA_TENTATIVA = 0)
                    .AfterMap((src, dest) => dest.TROCAR_SENHA = null)
                    .AfterMap((src, dest) => dest.PRIMEIRO_ACESSO = true)
                    .AfterMap((src, dest) => dest.DT_CRIACAO = DateTime.Now);

            CreateMap<Usuario, USUARIO_SENHAS>()
                    .ForMember(dest => dest.SENHA, ori => ori.MapFrom(x => x.SENHA))
                    .ForMember(dest => dest.USUARIO_ID, ori => ori.MapFrom(x => x.ID))
                    .ForMember(dest => dest.SENHA_DT, ori => ori.Ignore())
                    .ForMember(dest => dest.WFD_USUARIO, ori => ori.Ignore())
                    .AfterMap((src, dest) => dest.SENHA_DT = DateTime.Now);
        }

        private void MapearWfdContratanteConfig()
        {
            CreateMap<CONTRATANTE_CONFIGURACAO, ContratanteConfigAdministracaoModel>()
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.SolicitaDocumentos, ori => ori.MapFrom(x => x.SOLICITA_DOCS))
                .ForMember(dest => dest.SolicitaFichaCadastral, ori => ori.MapFrom(x => x.SOLICITA_FICHA_CAD))
                .ForMember(dest => dest.Contratante, ori => ori.MapFrom(x => x.WFD_CONTRATANTE))
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore());
            CreateMap<ContratanteConfigAdministracaoModel, CONTRATANTE_CONFIGURACAO>()
                .ForMember(dest => dest.CONTRATANTE_ID, ori => ori.MapFrom(x => x.ContratanteId))
                .ForMember(dest => dest.SOLICITA_DOCS, ori => ori.MapFrom(x => x.SolicitaDocumentos))
                .ForMember(dest => dest.SOLICITA_FICHA_CAD, ori => ori.MapFrom(x => x.SolicitaFichaCadastral))
                .ForMember(dest => dest.WFD_CONTRATANTE, ori => ori.MapFrom(x => x.Contratante))
                .ForMember(dest => dest.LOGOTIPO, ori => ori.Ignore())
                .ForMember(dest => dest.TERMO_ACEITE, ori => ori.Ignore())
                .ForMember(dest => dest.ROBO_CICLO_ATU, ori => ori.Ignore())
                .ForMember(dest => dest.ROBO_DT_PROX_EXEC, ori => ori.Ignore())
                .ForMember(dest => dest.BLOQUEIO_MANUAL, ori => ori.Ignore())
                .ForMember(dest => dest.BLOQUIEO_MANUAL_PRAZO, ori => ori.Ignore())
                .ForMember(dest => dest.TOTAL_TENTATIVA_ROBO, ori => ori.Ignore())
                .ForMember(dest => dest.NIVEIS_CATEGORIA, ori => ori.Ignore())
                .ForMember(dest => dest.QTD_ROBO_SIMULTANEA, ori => ori.Ignore());
        }

        private void MapearWfdTipoCadastro()
        {
            CreateMap<TIPO_CADASTRO_FORNECEDOR, TipoCadastroAdministracaoModel>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.ContratanteList, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore());
        }

        private void MapearQicQuestionario()
        {
            CreateMap<QUESTIONARIO, QuestionarioVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Titulo, ori => ori.MapFrom(x => x.QUEST_NM))
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.QUEST_DSC))
                .ForMember(dest => dest.ExibeDadosBancarios, ori => ori.MapFrom(x => x.LE_D_BANCARIO))
                .ForMember(dest => dest.ExibeDadosContato, ori => ori.MapFrom(x => x.LE_D_CONTATO))
                .ForMember(dest => dest.ExibeDadosGerais, ori => ori.MapFrom(x => x.LE_D_GERAIS))
                .ForMember(dest => dest.ExibeInformacaoComplementar, ori => ori.MapFrom(x => x.LE_INFO_COMPL))
                .ForMember(dest => dest.AbaList, ori => ori.MapFrom(x => x.QIC_QUEST_ABA))
                .ForMember(dest => dest.EstiloClassCss, ori => ori.Ignore());
        }

        private void MapearQicQuestAba()
        {
            CreateMap<QUESTIONARIO_ABA, AbaVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Titulo, ori => ori.MapFrom(x => x.ABA_NM))
                .ForMember(dest => dest.QuestionarioId, ori => ori.MapFrom(x => x.QUESTIONARIO_ID))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.ABA_DSC))
                .ForMember(dest => dest.PerguntaList, ori => ori.MapFrom(x => x.QIC_QUEST_ABA_PERG));
        }

        private void MapearQicQuestAbaPerg()
        {
            CreateMap<QUESTIONARIO_PERGUNTA, PerguntaVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.AbaId, ori => ori.MapFrom(x => x.QUEST_ABA_ID))
                .ForMember(dest => dest.Titulo, ori => ori.MapFrom(x => x.PERG_NM))
                .ForMember(dest => dest.TipoDado, ori => ori.MapFrom(x => x.TP_DADO))
                .ForMember(dest => dest.Dominio, ori => ori.MapFrom(x => x.DOMINIO))
                .ForMember(dest => dest.Tamanho, ori => ori.MapFrom(x => x.RESP_TAMANHO))
                .ForMember(dest => dest.Obrigatorio,
                    ori => ori.MapFrom(x => x.QIC_QUEST_ABA_PERG_PAPEL.FirstOrDefault().OBRIG))
                .ForMember(dest => dest.Escrita,
                    ori => ori.MapFrom(x => x.QIC_QUEST_ABA_PERG_PAPEL.FirstOrDefault().ESCRITA))
                .ForMember(dest => dest.Leitura,
                    ori => ori.MapFrom(x => x.QIC_QUEST_ABA_PERG_PAPEL.FirstOrDefault().LEITURA))
                .ForMember(dest => dest.SolicitacaoId,
                    ori => ori.MapFrom(x => x.WFD_INFORM_COMPL.FirstOrDefault(y => y.PERG_ID == x.ID).SOLICITACAO_ID))
                .ForMember(dest => dest.RespostaId,
                    ori => ori.MapFrom(x => x.WFD_INFORM_COMPL.FirstOrDefault(y => y.PERG_ID == x.ID).ID))
                .ForMember(dest => dest.Resposta,
                    ori => ori.MapFrom(x => x.WFD_INFORM_COMPL.FirstOrDefault(y => y.PERG_ID == x.ID).RESPOSTA))
                .ForMember(dest => dest.DominioListId, ori => ori.Ignore())
                .ForMember(dest => dest.PaiRespondido, ori => ori.Ignore())
                .ForMember(dest => dest.DominioList, ori => ori.MapFrom(x => x.QIC_QUEST_ABA_PERG_RESP.Where(y => y.PERG_ID == x.ID)));
        }

        private void MapearQicQuestAbaPergPerfil()
        {

            CreateMap<QUESTIONARIO_PAPEL, PerfilVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.PAPEL_ID))
                .ForMember(dest => dest.Obrigatorio, ori => ori.MapFrom(x => x.OBRIG))
                .ForMember(dest => dest.Escrita, ori => ori.MapFrom(x => x.ESCRITA))
                .ForMember(dest => dest.Leitura, ori => ori.Ignore())
                .ForMember(dest => dest.PAPEL_NM, ori => ori.Ignore())
                .ForMember(dest => dest.PAPEL_DSC, ori => ori.Ignore())
                .ForMember(dest => dest.Contratante, ori => ori.Ignore());

        }

        private void MapearWfdInformCompl()
        {
            CreateMap<SalvaInformacaComplementarVM, WFD_INFORM_COMPL>()
                .ForMember(dest => dest.PERG_ID, ori => ori.MapFrom(x => x.PerguntaId))
                .ForMember(dest => dest.SOLICITACAO_ID, ori => ori.MapFrom(x => x.SolicitacaoId))
                .ForMember(dest => dest.RESPOSTA, ori => ori.MapFrom(x => x.Resposta))
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.RespostaId))
                .ForMember(dest => dest.QIC_QUEST_ABA_PERG, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOLICITACAO, ori => ori.Ignore());
        }

        private void MapearArquivoUpload()
        {
        }

        private void MapearWdfContratantePjpf()
        {
            CreateMap<WFD_CONTRATANTE_PJPF, FornecedoresVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.ContratanteID, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.Categoria, ori => ori.MapFrom(x => x.CATEGORIA_ID))
                .ForMember(dest => dest.CodigoERP, ori => ori.MapFrom(x => x.PJPF_COD_ERP))
                .ForMember(dest => dest.NomeEmpresa, ori => ori.MapFrom(x => x.WFD_CONTRATANTE.RAZAO_SOCIAL))
                .ForMember(dest => dest.RazaoSocial,
                    ori => ori.MapFrom(
                        x =>
                            x.WFD_PJPF.TIPO_PJPF_ID == 3
                                ? x.WFD_PJPF.NOME
                                : x.WFD_PJPF.RAZAO_SOCIAL))
                .ForMember(dest => dest.CNPJ,
                    ori => ori.MapFrom(
                            x =>
                                x.WFD_PJPF.TIPO_PJPF_ID == 3
                                    ? Convert.ToUInt64(x.WFD_PJPF.CPF).ToString(@"000\.000\.000\-00")
                                    : Convert.ToUInt64(x.WFD_PJPF.CNPJ).ToString(@"00\.000\.000\/0000\-00")))
                .ForMember(dest => dest.Empresa, ori => ori.Ignore())
                .ForMember(dest => dest.Compras, ori => ori.Ignore())
                .ForMember(dest => dest.Telefone, ori => ori.Ignore())
                .ForMember(dest => dest.Ativo, ori => ori.Ignore())
                .ForMember(dest => dest.Solicitacao, ori => ori.Ignore())
                .ForMember(dest => dest.TudoAmpliado, ori => ori.Ignore())
                .ForMember(dest => dest.Bloqueado, ori => ori.Ignore())
                .ForMember(dest => dest.DataNascimento, ori => ori.Ignore())
                .ForMember(dest => dest.Status, ori => ori.Ignore())
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.Contato, ori => ori.Ignore())
                .ForMember(dest => dest.Contatos, ori => ori.Ignore())
                .ForMember(dest => dest.Bancos, ori => ori.Ignore())
                .ForMember(dest => dest.Anexos, ori => ori.Ignore())
                .ForMember(dest => dest.RoboReceitaCNPJ, ori => ori.Ignore())
                .ForMember(dest => dest.RoboReceitaCPF, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSintegra, ori => ori.Ignore())
                .ForMember(dest => dest.RoboSimples, ori => ori.Ignore())
                .ForMember(dest => dest.NomeContato, ori => ori.Ignore())
                .ForMember(dest => dest.Email, ori => ori.Ignore())
                .ForMember(dest => dest.TipoFornecedor, ori => ori.Ignore())
                .ForMember(dest => dest.TipoCadastro, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore());
        }

        private void MapearPjpfCategoria()
        {
            CreateMap<FORNECEDOR_CATEGORIA, FornecedorCategoriaVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.DESCRICAO));
        }

        private void MapearWfdContratanteOrgCompras()
        {
            CreateMap<CONTRATANTE_ORGANIZACAO_COMPRAS, OrganizacaoComprasVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.ORG_COMPRAS_DSC));
        }

        private void MapearTBanco()
        {
            CreateMap<TiposDeBanco, BancosVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.BANCO_COD + " - " + x.BANCO_NM));
        }

        private void MapeamentoConversores()
        {
            //CreateMap<string, int>().ConvertUsing(new IntTypeConverter());
            //CreateMap<string, int?>().ConvertUsing(new NullIntTypeConverter());
            //CreateMap<string, decimal?>().ConvertUsing(new NullDecimalTypeConverter());
            //CreateMap<string, decimal>().ConvertUsing(new DecimalTypeConverter());
            //CreateMap<string, bool?>().ConvertUsing(new NullBooleanTypeConverter());
            //CreateMap<string, bool>().ConvertUsing(new BooleanTypeConverter());
            //CreateMap<string, Int64?>().ConvertUsing(new NullInt64TypeConverter());
            //CreateMap<string, Int64>().ConvertUsing(new Int64TypeConverter());
            //CreateMap<string, DateTime?>().ConvertUsing(new NullDateTimeTypeConverter());
            //CreateMap<string, DateTime>().ConvertUsing(new DateTimeTypeConverter());
        }

        // Model > ViewModel
        private void MapearWFDPJPFBanco()
        {
            CreateMap<BancoDoFornecedor, DadosBancariosVM>()
                .ForMember(destino => destino.BancoSolicitacaoID, ori => ori.Ignore())
                .ForMember(destino => destino.BancoPJPFID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.Banco, origem => origem.MapFrom(x => x.BANCO_ID))
                .ForMember(destino => destino.NomeBanco, origem => origem.MapFrom(x => x.T_BANCO.BANCO_NM))
                .ForMember(destino => destino.Agencia, origem => origem.MapFrom(x => x.AGENCIA))
                .ForMember(destino => destino.Digito, origem => origem.MapFrom(x => x.AG_DV))
                .ForMember(destino => destino.ContaCorrente, origem => origem.MapFrom(x => x.CONTA))
                .ForMember(destino => destino.ContaCorrenteDigito, origem => origem.MapFrom(x => x.CONTA_DV))
                .ForMember(destino => destino.Bancos, ori => ori.Ignore())
                .ForMember(destino => destino.ArquivoID, origem => origem.MapFrom(x => x.ARQUIVO_ID))
                .ForMember(destino => destino.NomeArquivo, origem => origem.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                .ForMember(destino => destino.DataUpload, origem => origem.MapFrom(x => x.WFD_ARQUIVOS.DATA_UPLOAD));

            CreateMap<DadosBancariosVM, BancoDoFornecedor>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.BancoPJPFID))
                .ForMember(destino => destino.CONTRATANTE_PJPF_ID, origem => origem.MapFrom(x => x.ContratantePjPfId))
                .ForMember(destino => destino.BANCO_ID, origem => origem.MapFrom(x => x.Banco))
                .ForMember(destino => destino.AGENCIA, origem => origem.MapFrom(x => x.Agencia))
                .ForMember(destino => destino.AG_DV, origem => origem.MapFrom(x => x.Digito))
                .ForMember(destino => destino.CONTA, origem => origem.MapFrom(x => x.ContaCorrente))
                .ForMember(destino => destino.CONTA_DV, origem => origem.MapFrom(x => x.ContaCorrenteDigito))
                .ForMember(destino => destino.ARQUIVO_ID, origem => origem.MapFrom(x => x.ArquivoID))
                .ForMember(destino => destino.ATIVO, origem => origem.MapFrom(x => x.Ativo))
                .ForMember(destino => destino.T_BANCO, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_ARQUIVOS, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_CONTRATANTE_PJPF, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_SOL_MOD_BANCO, origem => origem.Ignore());
        }

        private void MapearWFDSolDocumentos()
        {
            CreateMap<SolicitacaoDeDocumentos, SolicitacaoDocumentosVM>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.NomeArquivo, origem => origem.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                .ForMember(destino => destino.DataUpload, origem => origem.MapFrom(x => x.WFD_ARQUIVOS.DATA_UPLOAD))
                .ForMember(destino => destino.DataValidade, origem => origem.MapFrom(x => x.DATA_VENCIMENTO))
                .ForMember(destino => destino.Obrigatorio, origem => origem.MapFrom(x => x.ListaDeDocumentosDeFornecedor.OBRIGATORIO))
                .ForMember(destino => destino.Documento, origem => origem.MapFrom(x => string.Format("{0} - {1}", x.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO, x.DescricaoDeDocumentos.DESCRICAO)))
                .ForMember(destino => destino.PorValidade, origem => origem.MapFrom(x => x.EXIGE_VALIDADE))
                .ForMember(destino => destino.Periodicidade, origem => origem.MapFrom(x => x.PERIODICIDADE_ID))
                .ForMember(destino => destino.DescricaoPeriodicidade, origem => origem.MapFrom(x => x.WFD_T_PERIODICIDADE.PERIODICIDADE_NM))
                .ForMember(destino => destino.ArquivoID, origem => origem.MapFrom(x => x.ARQUIVO_ID))
                .ForMember(destino => destino.Arquivo, ori => ori.Ignore())
                .ForMember(destino => destino.SituacaoID, ori => ori.MapFrom(x => x.SITUACAO_ID))
                .ForMember(destino => destino.Situacao, ori => ori.Ignore())
                .ForMember(destino => destino.Observacao, ori => ori.Ignore())
                .ForMember(destino => destino.ListaDocumentosID, origem => origem.MapFrom(x => x.ListaDeDocumentosDeFornecedor.ID))
                .ForMember(destino => destino.SolicitacaoID, origem => origem.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.DescricaoDocumentoId, ori => ori.MapFrom(x => x.DESCRICAO_DOCUMENTO_ID))
                .ForMember(dest => dest.DescricaoDocumentoId_CH, ori => ori.MapFrom(x => x.DescricaoDeDocumentos.DESCRICAO_DOCUMENTOS_CH_ID));

            CreateMap<SolicitacaoDocumentosVM, SolicitacaoDeDocumentos>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.DESCRICAO_DOCUMENTO_ID, origem => origem.MapFrom(x => x.DescricaoDocumentoId))
                .ForMember(destino => destino.ARQUIVO_ID, origem => origem.MapFrom(x => x.ArquivoID))
                .ForMember(destino => destino.DATA_VENCIMENTO, origem => origem.MapFrom(x => x.DataValidade))
                .ForMember(destino => destino.SITUACAO_ID, ori => ori.MapFrom(x => x.SituacaoID))
                .ForMember(destino => destino.SOLICITACAO_ID, origem => origem.MapFrom(x => x.SolicitacaoID))
                .ForMember(destino => destino.NOME_ARQUIVO, origem => origem.MapFrom(x => x.NomeArquivo))
                .ForMember(destino => destino.EXIGE_VALIDADE, origem => origem.MapFrom(x => x.PorValidade))
                .ForMember(destino => destino.PERIODICIDADE_ID, origem => origem.MapFrom(x => x.Periodicidade))
                .ForMember(destino => destino.OBRIGATORIO, origem => origem.MapFrom(x => x.Obrigatorio));
        }

        private void MapearWFDPJPFDocumentos()
        {
            CreateMap<DocumentosDoFornecedor, SolicitacaoDocumentosVM>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.NomeArquivo, origem => origem.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                .ForMember(destino => destino.DataUpload, origem => origem.MapFrom(x => x.WFD_ARQUIVOS.DATA_UPLOAD))
                .ForMember(destino => destino.DataValidade, origem => origem.MapFrom(x => (!x.DATA_VENCIMENTO.HasValue) ? null : (DateTime?)x.DATA_VENCIMENTO.Value))
                .ForMember(destino => destino.Documento, origem => origem.MapFrom(x => string.Format("{0} - {1}", x.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO, x.DescricaoDeDocumentos.DESCRICAO)))
                .ForMember(destino => destino.PorValidade, origem => origem.MapFrom(x => x.EXIGE_VALIDADE))
                .ForMember(destino => destino.Periodicidade, origem => origem.MapFrom(x => x.PERIODICIDADE_ID))
                .ForMember(destino => destino.DescricaoPeriodicidade, origem => origem.MapFrom(x => x.WFD_T_PERIODICIDADE.PERIODICIDADE_NM))
                .ForMember(destino => destino.ArquivoID, origem => origem.MapFrom(x => x.ARQUIVO_ID))
                .ForMember(destino => destino.Arquivo, ori => ori.Ignore())
                .ForMember(destino => destino.SituacaoID, ori => ori.Ignore())
                .ForMember(destino => destino.Situacao, ori => ori.Ignore())
                .ForMember(destino => destino.Observacao, ori => ori.Ignore())
                .ForMember(destino => destino.Obrigatorio, origem => origem.MapFrom(x => x.WFD_PJPF_LISTA_DOCUMENTOS != null ? x.WFD_PJPF_LISTA_DOCUMENTOS.OBRIGATORIO : false))
                .ForMember(destino => destino.ListaVersao, ori => ori.MapFrom(x => x.WFD_PJPF_DOCUMENTOS_VERSAO))
                .ForMember(destino => destino.ListaDocumentosID, origem => origem.MapFrom(x => x.WFD_PJPF_LISTA_DOCUMENTOS.ID))
                .ForMember(destino => destino.SolicitacaoID, origem => origem.MapFrom(x => x.SOLICITACAO_ID))
                .ForMember(dest => dest.DescricaoDocumentoId, ori => ori.MapFrom(x => x.DESCRICAO_DOCUMENTO_ID))
                .ForMember(dest => dest.DescricaoDocumentoId_CH, ori => ori.MapFrom(x => x.DescricaoDeDocumentos.DESCRICAO_DOCUMENTOS_CH_ID));

            CreateMap<VersionamentoDeDocumentoDoFornecedor, VersaoVM>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.Nome, origem => origem.MapFrom(x => x.NOME_ARQUIVO));

            CreateMap<DocumentosDoFornecedor, DocumentosPJPFVM>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.ContratanteID, ori => ori.Ignore())
                .ForMember(destino => destino.ContratantePJPFID, origem => origem.MapFrom(x => x.CONTRATANTE_PJPF_ID))
                .ForMember(destino => destino.PJPFID, ori => ori.Ignore())
                .ForMember(destino => destino.DescricaoDocumentoID, origem => origem.MapFrom(x => x.DESCRICAO_DOCUMENTO_ID))
                .ForMember(destino => destino.DescricaoDocumento, origem => origem.MapFrom(x => x.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO + " - " + x.DescricaoDeDocumentos.DESCRICAO))
                .ForMember(destino => destino.DataValidade, origem => origem.MapFrom(x => x.DATA_VENCIMENTO))
                .ForMember(destino => destino.DataUpload, origem => origem.MapFrom(x => x.DATA_UPLOAD))
                .ForMember(destino => destino.ArquivoID, origem => origem.MapFrom(x => x.ARQUIVO_ID))
                .ForMember(destino => destino.ExigeValidade, origem => origem.MapFrom(x => x.EXIGE_VALIDADE))
                .ForMember(destino => destino.PeriodicidadeID, origem => origem.MapFrom(x => x.PERIODICIDADE_ID))
                .ForMember(destino => destino.Obrigatorio, origem => origem.MapFrom(x => x.OBRIGATORIO));
        }

        private void MapearWFDPJPFBase()
        {
            CreateMap<FORNECEDORBASE, FornecedorBaseVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.PlanilhaId, ori => ori.MapFrom(x => x.PLANILHA_ID))

                .ForMember(dest => dest.UF, ori => ori.Ignore())
                //.ForMember(dest => dest.T_UF, ori => ori.Ignore())
                //.ForMember(dest => dest.UF, ori => ori.MapFrom(x => x.UF.ToString()))
                //.ForMember(dest => dest.UF, ori => ori.MapFrom(x => x.T_UF.UF_NM))
                .ForMember(dest => dest.InscricaoEstadual, ori => ori.MapFrom(x => x.INSCR_ESTADUAL))
                .ForMember(dest => dest.ContratanteID, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.TipoFornecedor, ori => ori.MapFrom(x => x.PJPF_TIPO))
                .ForMember(dest => dest.CategoriaId, ori => ori.MapFrom(x => x.CATEGORIA_ID))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(dest => dest.NomeFantasia, ori => ori.MapFrom(x => x.NOME_FANTASIA))
                .ForMember(dest => dest.InscricaoMunicipal, ori => ori.MapFrom(x => x.INSCR_MUNICIPAL))
                .ForMember(dest => dest.TipoLogradouro, ori => ori.MapFrom(x => x.TP_LOGRADOURO))
                .ForMember(dest => dest.Numero, ori => ori.MapFrom(x => x.NUMERO))
                .ForMember(dest => dest.Pais, ori => ori.MapFrom(x => x.PAIS))
                .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => x.CNPJ))
                .ForMember(dest => dest.CNAE, ori => ori.MapFrom(x => x.CNAE))
                .ForMember(dest => dest.CPF, ori => ori.MapFrom(x => x.CPF))
                .ForMember(dest => dest.CEP, ori => ori.MapFrom(x => x.CEP))
                .ForMember(dest => dest.Bairro, ori => ori.MapFrom(x => x.BAIRRO))
                .ForMember(dest => dest.Complemento, ori => ori.MapFrom(x => x.COMPLEMENTO))
                .ForMember(dest => dest.Ativo, ori => ori.MapFrom(x => x.ATIVO))
                .ForMember(dest => dest.Cidade, ori => ori.MapFrom(x => x.CIDADE))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.ENDERECO))
                .ForMember(dest => dest.Contratante, ori => ori.MapFrom(x => x.WFD_CONTRATANTE))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.NomeContato, ori => ori.MapFrom(x => x.NOME_CONTATO))
                .ForMember(dest => dest.Email, ori => ori.MapFrom(x => x.EMAIL))
                .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.TELEFONE))
                .ForMember(dest => dest.Celular, ori => ori.MapFrom(x => x.CELULAR))
                .ForMember(dest => dest.DataPrazo, ori => ori.MapFrom(x =>
                    (x.WFD_PJPF_BASE_CONVITE.FirstOrDefault(y => y.PJPF_BASE_ID == x.ID) != null)
                    ? (x.WFD_PJPF_BASE_CONVITE.FirstOrDefault(y => y.PJPF_BASE_ID == x.ID).WFD_SOLICITACAO.DT_PRORROGACAO_PRAZO != null)
                        ? x.WFD_PJPF_BASE_CONVITE.FirstOrDefault(y => y.PJPF_BASE_ID == x.ID).WFD_SOLICITACAO.DT_PRORROGACAO_PRAZO.Value.ToString("dd/MM/yyyy")
                        : x.WFD_PJPF_BASE_CONVITE.FirstOrDefault(y => y.PJPF_BASE_ID == x.ID).WFD_SOLICITACAO.DT_PRAZO.Value.ToString("dd/MM/yyyy")
                    : string.Empty
                    ))
                .ForMember(dest => dest.DataImportacao, ori => ori.MapFrom(x => x.DT_IMPORTACAO))
                .ForMember(dest => dest.CategoriaNome, ori => ori.MapFrom(x => x.WFD_PJPF_CATEGORIA.DESCRICAO))
                .ForMember(dest => dest.DataNascimento, ori => ori.MapFrom(x => (x.DT_NASCIMENTO.HasValue
                        && x.DT_NASCIMENTO
                        != DateTime.MinValue)
                            ? x.DT_NASCIMENTO.Value.ToString("dd/MM/yyyy")
                            : string.Empty))
                .ForMember(dest => dest.ExecutaRobo, ori => ori.MapFrom(x => x.EXECUTA_ROBO))
                .ForMember(dest => dest.DataConvite, ori => ori.MapFrom(x => x.WFD_PJPF_BASE_CONVITE
                    .FirstOrDefault(y => y.PJPF_BASE_ID == x.ID)
                    .DT_ENVIO
                    .ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlDetalhar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.Pagina, ori => ori.Ignore())
                .ForMember(dest => dest.MensagemSucesso, ori => ori.Ignore());

            CreateMap<FORNECEDORBASE, FornecedorBaseFuncionalidadeVM>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.ContratanteID, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.TipoFornecedor, ori => ori.MapFrom(x => x.PJPF_TIPO))
                .ForMember(dest => dest.CategoriaId, ori => ori.MapFrom(x => x.CATEGORIA_ID))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => x.CNPJ))
                .ForMember(dest => dest.CPF, ori => ori.MapFrom(x => x.CPF))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.CategoriaNome, ori => ori.MapFrom(x => x.WFD_PJPF_CATEGORIA.DESCRICAO))
                .ForMember(dest => dest.Selecionado, ori => ori.MapFrom(x => x.EXECUTA_ROBO))
                .ForMember(dest => dest.Convidado, ori => ori.MapFrom(x => x.WFD_PJPF_BASE_CONVITE.Any()))
                .ForMember(dest => dest.Colunas, ori => ori.Ignore())
                .ForMember(dest => dest.CelulaCSS, ori => ori.Ignore())
                .ForMember(dest => dest.CelulaTitulo, ori => ori.Ignore())
                .ForMember(dest => dest.CelulaValor, ori => ori.Ignore())
                .ForMember(dest => dest.ExecutaRobo, ori => ori.Ignore())
                .ForMember(dest => dest.Respondido, ori => ori.MapFrom(x => x.WFD_PJPF_BASE_CONVITE
                            .FirstOrDefault(sol => sol.PJPF_BASE_ID == x.ID)
                            .WFD_SOLICITACAO
                            .WFD_SOLICITACAO_TRAMITE
                            .FirstOrDefault(xy => xy.Papel.PAPEL_TP_ID == 50) != null));
            ;
        }

        private void MapearImportacaoFornecedoresFiltro()
        {
            CreateMap<ImportacaoFornecedoresFiltrosDTO, FornecedorBaseFiltroVM>()
                .ForMember(destino => destino.ContratanteId, origem => origem.MapFrom(x => x.ContratanteId))
                .ForMember(destino => destino.CategoriaId, origem => origem.MapFrom(x => x.CategoriaId))
                .ForMember(destino => destino.CNPJ, origem => origem.MapFrom(x => x.CNPJ))
                .ForMember(destino => destino.RazaoSocial, origem => origem.MapFrom(x => x.RazaoSocial))
                .ForMember(destino => destino.CPF, origem => origem.MapFrom(x => x.CPF))
                .ForMember(destino => destino.Nome, origem => origem.MapFrom(x => x.Nome))
                .ForMember(destino => destino.Categorizados, origem => origem.MapFrom(x => x.Categorizados))
                .ForMember(destino => destino.Respondido, origem => origem.MapFrom(x => x.Respondido))
                .ForMember(destino => destino.Convidados, origem => origem.MapFrom(x => x.Convidados));

            CreateMap<FornecedorBaseFiltroVM, ImportacaoFornecedoresFiltrosDTO>()
                .ForMember(destino => destino.ContratanteId, origem => origem.MapFrom(x => x.ContratanteId))
                .ForMember(destino => destino.CategoriaId, origem => origem.MapFrom(x => x.CategoriaId))
                .ForMember(destino => destino.CNPJ, origem => origem.MapFrom(x => x.CNPJ))
                .ForMember(destino => destino.RazaoSocial, origem => origem.MapFrom(x => x.RazaoSocial))
                .ForMember(destino => destino.CPF, origem => origem.MapFrom(x => x.CPF))
                .ForMember(destino => destino.Nome, origem => origem.MapFrom(x => x.Nome))
                .ForMember(destino => destino.Categorizados, origem => origem.MapFrom(x => x.Categorizados))
                .ForMember(destino => destino.Respondido, origem => origem.MapFrom(x => x.Respondido))
                .ForMember(destino => destino.Convidados, origem => origem.MapFrom(x => x.Convidados))
                .ForMember(destino => destino.Reenviados, origem => origem.Ignore())
                .ForMember(destino => destino.Gerados, origem => origem.Ignore())
                .ForMember(destino => destino.Completos, origem => origem.Ignore());
        }

        private void MapearFornecedorBaseTopoDTO()
        {
            CreateMap<FornecedorBaseTopoDTO, FornecedorBaseTopoVM>()
                .ForMember(destino => destino.TotalSemCategoria, origem => origem.MapFrom(x => x.TotalSemCategoria))
                .ForMember(destino => destino.TotalSemValidacao, origem => origem.MapFrom(x => x.TotalSemValidacao));
        }

        // ViewModel > Model

        private void MapearDadosBancariosVM()
        {
            CreateMap<DadosBancariosVM, SolicitacaoModificacaoDadosBancario>()
                .ForMember(destino => destino.ID, ori => ori.Ignore())
                .ForMember(destino => destino.BANCO_PJPF_ID, origem => origem.MapFrom(x => x.BancoPJPFID))
                .ForMember(destino => destino.CONTRATANTE_ID, origem => origem.MapFrom(x => x.ContratanteID))
                .ForMember(destino => destino.BANCO_ID, origem => origem.MapFrom(x => x.Banco))
                .ForMember(destino => destino.AGENCIA, origem => origem.MapFrom(x => x.Agencia))
                .ForMember(destino => destino.AG_DV, origem => origem.MapFrom(x => x.Digito))
                .ForMember(destino => destino.CONTA, origem => origem.MapFrom(x => x.ContaCorrente))
                .ForMember(destino => destino.CONTA_DV, origem => origem.MapFrom(x => x.ContaCorrenteDigito))
                .ForMember(destino => destino.SOLICITACAO_ID, origem => origem.MapFrom(x => x.SolicitacaoID))
                .ForMember(destino => destino.DATA_UPLOAD, origem => origem.Ignore())
                .ForMember(destino => destino.NOME_ARQUIVO, origem => origem.Ignore())
                .ForMember(destino => destino.ARQUIVO_ID, origem => origem.MapFrom(x => x.ArquivoID))
                .ForMember(destino => destino.CONTRATANTE_ID, origem => origem.Ignore())
                .ForMember(destino => destino.PJPF_ID, origem => origem.Ignore())
                .ForMember(destino => destino.T_BANCO, origem => origem.Ignore())
                .ForMember(destino => destino.BancoDoFornecedor, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_SOLICITACAO, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_ARQUIVOS, origem => origem.Ignore());
        }

        private void MapearDadosEnderecosVM()
        {
            CreateMap<SOLICITACAO_MODIFICACAO_ENDERECO, DadosEnderecosVM>()
                .ForMember(destino => destino.TipoEndereco, origem => origem.MapFrom(x => x.WFD_T_TP_ENDERECO.NM_TP_ENDERECO))
                .ForMember(destino => destino.Endereco, origem => origem.MapFrom(x => x.ENDERECO))
                .ForMember(destino => destino.Numero, origem => origem.MapFrom(x => x.NUMERO))
                .ForMember(destino => destino.Complemento, origem => origem.MapFrom(x => x.COMPLEMENTO))
                .ForMember(destino => destino.CEP, origem => origem.MapFrom(x => x.CEP))
                .ForMember(destino => destino.Bairro, origem => origem.MapFrom(x => x.BAIRRO))
                .ForMember(destino => destino.Cidade, origem => origem.MapFrom(x => x.CIDADE))
                .ForMember(destino => destino.UF, origem => origem.MapFrom(x => x.UF))
                .ForMember(destino => destino.Pais, origem => origem.MapFrom(x => x.PAIS))
                .ForMember(destino => destino.T_UF, origem => origem.MapFrom(x => x.T_UF))
                .ForMember(destino => destino.TipoEnderecoId, origem => origem.MapFrom(x => x.TP_ENDERECO_ID))
                .ForMember(destino => destino.PjPjId, origem => origem.MapFrom(x => x.PJPF_ENDERECO_ID))
                .ForMember(destino => destino.SolicitacaoID, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_SOLICITACAO, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_T_TP_ENDERECO, origem => origem.Ignore());

            CreateMap<FORNECEDORBASE_ENDERECO, DadosEnderecosVM>()
                .ForMember(destino => destino.PjPjId, ori => ori.Ignore())
                .ForMember(destino => destino.ContratantePjPfId, ori => ori.Ignore())
                .ForMember(destino => destino.SolicitacaoID, ori => ori.Ignore())
                .ForMember(destino => destino.ListTipoEndereco, ori => ori.Ignore())
                .ForMember(destino => destino.ListUF, ori => ori.Ignore())
                .ForMember(destino => destino.WFD_SOLICITACAO, ori => ori.Ignore())
                .ForMember(destino => destino.TipoEndereco, origem => origem.MapFrom(x => x.WFD_T_TP_ENDERECO.NM_TP_ENDERECO))
                .ForMember(destino => destino.Endereco, origem => origem.MapFrom(x => x.ENDERECO))
                .ForMember(destino => destino.Numero, origem => origem.MapFrom(x => x.NUMERO))
                .ForMember(destino => destino.Complemento, origem => origem.MapFrom(x => x.COMPLEMENTO))
                .ForMember(destino => destino.CEP, origem => origem.MapFrom(x => x.CEP))
                .ForMember(destino => destino.Bairro, origem => origem.MapFrom(x => x.BAIRRO))
                .ForMember(destino => destino.Cidade, origem => origem.MapFrom(x => x.CIDADE))
                .ForMember(destino => destino.UF, origem => origem.MapFrom(x => x.UF))
                .ForMember(destino => destino.Pais, origem => origem.MapFrom(x => x.PAIS))
                .ForMember(destino => destino.T_UF, origem => origem.MapFrom(x => x.T_UF))
                .ForMember(destino => destino.TipoEnderecoId, origem => origem.MapFrom(x => x.TP_ENDERECO_ID));

            CreateMap<FORNECEDOR_ENDERECO, DadosEnderecosVM>()
                .ForMember(destino => destino.TipoEndereco, origem => origem.MapFrom(x => x.WFD_T_TP_ENDERECO.NM_TP_ENDERECO))
                .ForMember(destino => destino.Endereco, origem => origem.MapFrom(x => x.ENDERECO))
                .ForMember(destino => destino.Numero, origem => origem.MapFrom(x => x.NUMERO))
                .ForMember(destino => destino.Complemento, origem => origem.MapFrom(x => x.COMPLEMENTO))
                .ForMember(destino => destino.CEP, origem => origem.MapFrom(x => x.CEP))
                .ForMember(destino => destino.Bairro, origem => origem.MapFrom(x => x.BAIRRO))
                .ForMember(destino => destino.Cidade, origem => origem.MapFrom(x => x.CIDADE))
                .ForMember(destino => destino.UF, origem => origem.MapFrom(x => x.UF))
                .ForMember(destino => destino.Pais, origem => origem.MapFrom(x => x.PAIS))
                .ForMember(destino => destino.T_UF, origem => origem.MapFrom(x => x.T_UF))
                .ForMember(destino => destino.TipoEnderecoId, origem => origem.MapFrom(x => x.TP_ENDERECO_ID))
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                //.ForMember(destino => destino.ListTipoEndereco, origem => origem.Ignore())    
                .ForMember(destino => destino.SolicitacaoID, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_SOLICITACAO, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_T_TP_ENDERECO, origem => origem.Ignore());

            CreateMap<DadosEnderecosVM, FORNECEDORBASE_ENDERECO>()
                .ForMember(destino => destino.PAIS, origem => origem.MapFrom(x => x.Pais))
                .ForMember(destino => destino.CEP, origem => origem.MapFrom(x => x.CEP))
                .ForMember(destino => destino.BAIRRO, origem => origem.MapFrom(x => x.Bairro))
                .ForMember(destino => destino.CIDADE, origem => origem.MapFrom(x => x.Cidade))
                .ForMember(destino => destino.COMPLEMENTO, origem => origem.MapFrom(x => x.Complemento))
                .ForMember(destino => destino.ENDERECO, origem => origem.MapFrom(x => x.Endereco))
                .ForMember(destino => destino.NUMERO, origem => origem.MapFrom(x => x.Numero))
                .ForMember(destino => destino.UF, origem => origem.MapFrom(x => x.T_UF.UF_SGL))
                .ForMember(destino => destino.T_UF, origem => origem.Ignore())
                .ForMember(destino => destino.TP_ENDERECO_ID, origem => origem.MapFrom(x => x.TipoEnderecoId));

            CreateMap<DadosEnderecosVM, SOLICITACAO_MODIFICACAO_ENDERECO>()
                .ForMember(destino => destino.ENDERECO, origem => origem.MapFrom(x => x.Endereco))
                .ForMember(destino => destino.NUMERO, origem => origem.MapFrom(x => x.Numero))
                .ForMember(destino => destino.BAIRRO, origem => origem.MapFrom(x => x.Bairro))
                .ForMember(destino => destino.CEP, origem => origem.MapFrom(x => x.CEP))
                .ForMember(destino => destino.CIDADE, origem => origem.MapFrom(x => x.Cidade))
                .ForMember(destino => destino.COMPLEMENTO, origem => origem.MapFrom(x => x.Complemento))
                .ForMember(destino => destino.PAIS, origem => origem.MapFrom(x => x.Pais))
                .ForMember(destino => destino.PJPF_ENDERECO_ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.TP_ENDERECO_ID, origem => origem.MapFrom(x => x.TipoEnderecoId))
                .ForMember(destino => destino.SOLICITACAO_ID, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_SOLICITACAO, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_PJPF, origem => origem.Ignore())
                .ForMember(destino => destino.PJPF_ID, origem => origem.Ignore())
                .ForMember(destino => destino.UF, origem => origem.MapFrom(x => x.T_UF.UF_SGL))
                .ForMember(destino => destino.T_UF, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_T_TP_ENDERECO, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_PJPF_ENDERECO, origem => origem.Ignore());

            CreateMap<DadosEnderecosVM, FORNECEDORBASE_ENDERECO>()
                .ForMember(destino => destino.PAIS, origem => origem.MapFrom(x => x.Pais))
                .ForMember(destino => destino.CEP, origem => origem.MapFrom(x => x.CEP))
                .ForMember(destino => destino.BAIRRO, origem => origem.MapFrom(x => x.Bairro))
                .ForMember(destino => destino.CIDADE, origem => origem.MapFrom(x => x.Cidade))
                .ForMember(destino => destino.COMPLEMENTO, origem => origem.MapFrom(x => x.Complemento))
                .ForMember(destino => destino.ENDERECO, origem => origem.MapFrom(x => x.Endereco))
                .ForMember(destino => destino.NUMERO, origem => origem.MapFrom(x => x.Numero))
                .ForMember(destino => destino.UF, origem => origem.MapFrom(x => x.T_UF.UF_SGL))
                .ForMember(destino => destino.T_UF, origem => origem.Ignore())
                .ForMember(destino => destino.TP_ENDERECO_ID, origem => origem.MapFrom(x => x.TipoEnderecoId));

            CreateMap<DadosEnderecosVM, FORNECEDOR_ENDERECO>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.BAIRRO, origem => origem.MapFrom(x => x.Bairro))
                .ForMember(destino => destino.CEP, origem => origem.MapFrom(x => x.CEP))
                .ForMember(destino => destino.CIDADE, origem => origem.MapFrom(x => x.Cidade))
                .ForMember(destino => destino.COMPLEMENTO, origem => origem.MapFrom(x => x.Complemento))
                .ForMember(destino => destino.ENDERECO, origem => origem.MapFrom(x => x.Endereco))
                .ForMember(destino => destino.CONTRATANTE_PJPF_ID, origem => origem.MapFrom(x => x.ContratantePjPfId))
                .ForMember(destino => destino.NUMERO, origem => origem.MapFrom(x => x.Numero))
                .ForMember(destino => destino.PAIS, origem => origem.MapFrom(x => x.Pais))
                .ForMember(destino => destino.TP_ENDERECO_ID, origem => origem.MapFrom(x => x.TipoEnderecoId))
                .ForMember(destino => destino.UF, origem => origem.MapFrom(x => x.T_UF.UF_SGL))
                .ForMember(destino => destino.WFD_CONTRATANTE_PJPF, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_CONTRATANTE_PJPF, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_SOL_MOD_ENDERECO, origem => origem.Ignore())
                .ForMember(destino => destino.WFD_T_TP_ENDERECO, origem => origem.Ignore())
                .ForMember(destino => destino.T_UF, origem => origem.Ignore());
        }

        private void MapearDocumentosSolicitadosVM()
        {
            CreateMap<TimelineVM, TimelineDTO>().ReverseMap();
        }

        private void MapearFornecedorBaseVM()
        {
            CreateMap<FornecedorBaseVM, FORNECEDORBASE>()
                .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.UF, ori => ori.Ignore())
                .ForMember(dest => dest.T_UF, ori => ori.Ignore())
                .ForMember(dest => dest.INSCR_ESTADUAL, ori => ori.MapFrom(x => x.InscricaoEstadual))
                .ForMember(dest => dest.CONTRATANTE_ID, ori => ori.MapFrom(x => x.ContratanteID))
                .ForMember(dest => dest.PJPF_TIPO, ori => ori.MapFrom(x => x.TipoFornecedor))
                .ForMember(dest => dest.CATEGORIA_ID, ori => ori.MapFrom(x => (x.CategoriaId.HasValue) ? x.CategoriaId.Value : x.CategoriaId))
                .ForMember(dest => dest.RAZAO_SOCIAL, ori => ori.MapFrom(x => x.RazaoSocial))
                .ForMember(dest => dest.NOME_FANTASIA, ori => ori.MapFrom(x => x.NomeFantasia))
                .ForMember(dest => dest.INSCR_MUNICIPAL, ori => ori.MapFrom(x => x.InscricaoMunicipal))
                .ForMember(dest => dest.TP_LOGRADOURO, ori => ori.MapFrom(x => x.TipoLogradouro))
                .ForMember(dest => dest.NUMERO, ori => ori.MapFrom(x => x.Numero))
                .ForMember(dest => dest.PAIS, ori => ori.MapFrom(x => x.Pais))
                //.ForMember(dest => dest.T_UF.UF_NM, ori => ori.MapFrom(x => x.UF))
                .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => x.CNPJ))
                .ForMember(dest => dest.CNAE, ori => ori.MapFrom(x => x.CNAE))
                .ForMember(dest => dest.CPF, ori => ori.MapFrom(x => x.CPF))
                .ForMember(dest => dest.CEP, ori => ori.MapFrom(x => x.CEP))
                .ForMember(dest => dest.BAIRRO, ori => ori.MapFrom(x => x.Bairro))
                .ForMember(dest => dest.COMPLEMENTO, ori => ori.MapFrom(x => x.Complemento))
                .ForMember(dest => dest.ATIVO, ori => ori.MapFrom(x => x.Ativo))
                .ForMember(dest => dest.CIDADE, ori => ori.MapFrom(x => x.Cidade))
                .ForMember(dest => dest.ENDERECO, ori => ori.MapFrom(x => x.Endereco))
                .ForMember(dest => dest.NOME, ori => ori.MapFrom(x => x.Nome))
                .ForMember(dest => dest.NOME_CONTATO, ori => ori.MapFrom(x => x.NomeContato))
                .ForMember(dest => dest.EMAIL, ori => ori.MapFrom(x => x.Email))
                .ForMember(dest => dest.TELEFONE, ori => ori.MapFrom(x => x.Telefone))
                .ForMember(dest => dest.CELULAR, ori => ori.MapFrom(x => x.Celular))
                .ForMember(dest => dest.DT_IMPORTACAO, ori => ori.MapFrom(x => x.DataImportacao))
                .ForMember(dest => dest.WFD_CONTRATANTE, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_BASE_CONTATOS, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_CATEGORIA, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_SOLICITACAO_DOCUMENTOS, ori => ori.Ignore())
                .ForMember(dest => dest.DT_NASCIMENTO, ori => ori.MapFrom(x => x.DataNascimento))
                .ForMember(dest => dest.PLANILHA_ID, ori => ori.MapFrom(x => x.PlanilhaId))
                .ForMember(dest => dest.EXECUTA_ROBO, ori => ori.Ignore())
                .ForMember(dest => dest.ROBO, ori => ori.Ignore())
                .ForMember(dest => dest.ROBO_ID, ori => ori.Ignore());
        }

        private void MapearCarga()
        {
            CreateMap<SOLICITACAO, SolicitacoesCarga>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.OrganizacaoCompras, ori => ori.MapFrom(x => x.Contratante
                    .WFD_CONTRATANTE_ORG_COMPRAS
                    .FirstOrDefault(y => y.CONTRATANTE_ID == x.CONTRATANTE_ID)
                    .ID
                    ))
                .ForMember(dest => dest.GrupoContas, ori => ori.MapFrom(x => x.Contratante
                    .WFD_PJPF_CATEGORIA
                    .FirstOrDefault(y => y.CONTRATANTE_ID == x.CONTRATANTE_ID)
                    .ID))
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.Contratante.ID))
                //.ForMember(dest => dest.CodigoSap, ori => ori.MapFrom(x => x.Contratante.CONTRANTE_COD_ERP))
                //.ForMember(dest => dest.Fornecedor, ori => ori.MapFrom(x => x.SolicitacaoCadastroFornecedor.FirstOrDefault()))
                .ForMember(dest => dest.Bloqueio, ori => ori.MapFrom(x => x.SOLICITACAO_BLOQUEIO.FirstOrDefault()))
                .ForMember(dest => dest.Desbloqueio, ori => ori.MapFrom(x => x.WFD_SOL_DESBLOQ.FirstOrDefault()))
                .ForMember(dest => dest.Contatos, ori => ori.MapFrom(x => x.SolicitacaoModificacaoDadosContato))
                .ForMember(dest => dest.Bancos, ori => ori.MapFrom(x => x.SolicitacaoModificacaoDadosBancario))
                .AfterMap((ori, dest) =>
                {
                    int retorno = 0;
                    switch (ori.Fluxo.FLUXO_TP_ID)
                    {
                        case 10:
                        case 20:
                        case 30:
                        case 40:
                        case 50:
                            retorno = (int)EnumTipoAcao.Criacao;
                            break;
                        case 60:
                            retorno = (int)EnumTipoAcao.Ampliacao;
                            break;
                        case 90:
                            retorno = (int)EnumTipoAcao.DadosBancarios;
                            break;
                        case 100:
                            retorno = (int)EnumTipoAcao.DadosContato;
                            break;
                        case 110:
                            retorno = (int)EnumTipoAcao.Bloqueio;
                            break;
                        case 120:
                            retorno = (int)EnumTipoAcao.DesbloqueioFornecedor;
                            break;
                    }
                    dest.TipoAcao = retorno;
                });

            //CreateMap<SolicitacaoCadastroFornecedor, FornecedorCargaModel>()
            //    .ForMember(dest => dest.SimplesNacional, ori => ori.MapFrom(x => x.ROBO.SIMPLES_NACIONAL_SITUACAO))
            //    .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
            //    .ForMember(dest => dest.NomeFantasia, ori => ori.MapFrom(x => x.NOME_FANTASIA))
            //    .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => x.CNPJ))
            //    .ForMember(dest => dest.CPF, ori => ori.MapFrom(x => x.CPF))
            //    .ForMember(dest => dest.CEP, ori => ori.MapFrom(x => x.CEP))
            //    .ForMember(dest => dest.Cidade, ori => ori.MapFrom(x => x.CIDADE))
            //    .ForMember(dest => dest.TipoLogradouro, ori => ori.MapFrom(x => x.TP_LOGRADOURO))
            //    .ForMember(dest => dest.Rua, ori => ori.MapFrom(x => x.ENDERECO))
            //    .ForMember(dest => dest.Numero, ori => ori.MapFrom(x => x.NUMERO))
            //    .ForMember(dest => dest.Complemento, ori => ori.MapFrom(x => x.COMPLEMENTO))
            //    .ForMember(dest => dest.Bairro, ori => ori.MapFrom(x => x.BAIRRO))
            //    .ForMember(dest => dest.Estado, ori => ori.MapFrom(x => x.UF))
            //    .ForMember(dest => dest.Cliente, ori => ori.MapFrom(x => x.CLIENTE))
            //    .ForMember(dest => dest.InscricaoEstadual, ori => ori.MapFrom(x => x.INSCR_ESTADUAL))
            //    .ForMember(dest => dest.InscricaoMunicipal, ori => ori.MapFrom(x => x.INSCR_MUNICIPAL));

            CreateMap<SolicitacaoModificacaoDadosContato, DadosContatoCargaModel>()
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.EMail, ori => ori.MapFrom(x => x.EMAIL))
                .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.TELEFONE))
                .ForMember(dest => dest.Celular, ori => ori.MapFrom(x => x.CELULAR))
                .ForMember(dest => dest.TipoContato, ori => ori.MapFrom(x => x.TP_CONTATO_ID));

            CreateMap<SolicitacaoModificacaoDadosBancario, DadosBancariosCargaModel>()
                .ForMember(dest => dest.Banco, ori => ori.MapFrom(x => x.BANCO_ID))
                .ForMember(dest => dest.Agencia, ori => ori.MapFrom(x => x.AGENCIA))
                .ForMember(dest => dest.CodigoAgencia, ori => ori.MapFrom(x => x.AG_DV))
                .ForMember(dest => dest.ContaCorrente, ori => ori.MapFrom(x => x.CONTA))
                .ForMember(dest => dest.ContaCorrente, ori => ori.MapFrom(x => x.CONTA_DV));

            CreateMap<SOLICITACAO_BLOQUEIO, BloqueioCargaModel>()
                .ForMember(dest => dest.BloqueioEmpresaSelecionada, ori => ori.MapFrom(x => x.BLQ_LANCAMENTO_EMP == true ? "x" : string.Empty))
                .ForMember(dest => dest.TodasEmpresas, ori => ori.MapFrom(x => x.BLQ_LANCAMENTO_TODAS_EMP == true ? "x" : string.Empty))
                .ForMember(dest => dest.TodasOrganizacoesCompras, ori => ori.MapFrom(x => x.BLQ_COMPRAS_TODAS_ORG_COMPRAS == true ? "x" : string.Empty))
                .ForMember(dest => dest.FuncaoBloqueio, ori => ori.MapFrom(x => x.TipoDeFuncaoDuranteBloqueio.FUNCAO_BLOQ_COD));

            CreateMap<SOLICITACAO_DESBLOQUEIO, DesbloqueioCargaModel>()
.ForMember(destino => destino.CodigoSolicitacao, ori => ori.Ignore())
.ForMember(destino => destino.Empresa, ori => ori.Ignore())
.ForMember(destino => destino.CodigoSAP, ori => ori.Ignore())
.ForMember(destino => destino.OrganizacaoCompras, ori => ori.Ignore())
                .ForMember(dest => dest.BloqueioEmpresaSelecionada, ori => ori.MapFrom(x => x.BLQ_LANCAMENTO_EMP == true ? "x" : string.Empty))
                .ForMember(dest => dest.TodasEmpresas, ori => ori.MapFrom(x => x.BLQ_LANCAMENTO_TODAS_EMP == true ? "x" : string.Empty))
                .ForMember(dest => dest.TodasOrganizacoesCompras, ori => ori.MapFrom(x => x.BLQ_COMPRAS_TODAS_ORG_COMPRAS == true ? "x" : string.Empty))
                .ForMember(dest => dest.FuncaoBloqueio, ori => ori.MapFrom(x => x.WFD_T_FUNCAO_BLOQUEIO.FUNCAO_BLOQ_COD));
        }

        private void MapearRetorno()
        {

            CreateMap<SolicitacaoModificacaoDadosBancario, DadosBancariosCargaModel>()
                .ForMember(dest => dest.Banco, ori => ori.MapFrom(x => x.BANCO_ID))
                .ForMember(dest => dest.Agencia, ori => ori.MapFrom(x => x.AGENCIA))
                .ForMember(dest => dest.CodigoAgencia, ori => ori.MapFrom(x => x.AG_DV))
                .ForMember(dest => dest.ContaCorrente, ori => ori.MapFrom(x => x.CONTA))
                .ForMember(dest => dest.ContaCorrente, ori => ori.MapFrom(x => x.CONTA_DV));
        }

        private void MapearWfdPjpfBase()
        {
        }

        private void MapeaContatos()
        {
            CreateMap<Fornecedor, FORNECEDORBASE>()
                .ForMember(destino => destino.CONTRATANTE_ID, origem => origem.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(destino => destino.RAZAO_SOCIAL, origem => origem.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(destino => destino.NOME_FANTASIA, origem => origem.MapFrom(x => x.NOME_FANTASIA))
                .ForMember(destino => destino.WFD_PJPF_BASE_CONTATOS, origem => origem.MapFrom(x => x.WFD_CONTRATANTE_PJPF
                    .FirstOrDefault(y => y.CONTRATANTE_ID == x.CONTRATANTE_ID && y.PJPF_ID == x.ID)
                    .WFD_PJPF_CONTATOS
                    ))
                .ForMember(destino => destino.WFD_PJPF_BASE_ENDERECO, origem => origem.MapFrom(x => x.WFD_CONTRATANTE_PJPF
                    .FirstOrDefault(y => y.CONTRATANTE_ID == x.CONTRATANTE_ID && y.PJPF_ID == x.ID)
                    .WFD_PJPF_ENDERECO
                    ));
            CreateMap<FORNECEDOR_ENDERECO, FORNECEDORBASE_ENDERECO>()
                .ForMember(destino => destino.WFD_PJPF_BASE, ori => ori.Ignore())
                .ForMember(destino => destino.BAIRRO, origem => origem.MapFrom(x => x.BAIRRO))
                .ForMember(destino => destino.CEP, origem => origem.MapFrom(x => x.CEP))
                .ForMember(destino => destino.CIDADE, origem => origem.MapFrom(x => x.CIDADE))
                .ForMember(destino => destino.COMPLEMENTO, origem => origem.MapFrom(x => x.COMPLEMENTO))
                .ForMember(destino => destino.ENDERECO, origem => origem.MapFrom(x => x.ENDERECO))
                .ForMember(destino => destino.NUMERO, origem => origem.MapFrom(x => x.NUMERO))
                .ForMember(destino => destino.PAIS, origem => origem.MapFrom(x => x.PAIS));
            CreateMap<FichaCadastralWebForLinkVM, FORNECEDORBASE>()
                .ForMember(destino => destino.CONTRATANTE_ID, origem => origem.MapFrom(x => x.ContratanteID))
                .ForMember(destino => destino.CNPJ, origem => origem.MapFrom(x => x.CNPJ_CPF))
                .ForMember(destino => destino.RAZAO_SOCIAL, origem => origem.MapFrom(x => x.RazaoSocial))
                .ForMember(destino => destino.NOME_FANTASIA, origem => origem.MapFrom(x => x.NomeFantasia))
                .ForMember(destino => destino.INSCR_MUNICIPAL, origem => origem.MapFrom(x => x.InscricaoMunicipal))
                .ForMember(destino => destino.DT_IMPORTACAO, origem => origem.MapFrom(x => DateTime.Now))
                .ForMember(destino => destino.WFD_PJPF_BASE_ENDERECO, origem => origem.MapFrom(x => x.DadosEnderecos))
                .ForMember(destino => destino.WFD_PJPF_BASE_CONTATOS, origem => origem.MapFrom(x => x.DadosContatos))
                .ForMember(destino => destino.WFD_PJPF_BASE_UNSPSC, origem => origem.MapFrom(x => x.FornecedoresUnspsc))
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.PjpfBaseId))
                .ForMember(dest => dest.UF, ori => ori.Ignore())
                .ForMember(dest => dest.T_UF, ori => ori.Ignore())
                .ForMember(dest => dest.PJPF_TIPO, ori => ori.Ignore())
                .ForMember(dest => dest.CPF, ori => ori.Ignore())
                .ForMember(dest => dest.DT_NASCIMENTO, ori => ori.Ignore())
                .ForMember(dest => dest.INSCR_ESTADUAL, ori => ori.Ignore())
                .ForMember(dest => dest.TP_LOGRADOURO, ori => ori.Ignore())
                .ForMember(dest => dest.NOME_CONTATO, ori => ori.Ignore())
                .ForMember(dest => dest.EMAIL, ori => ori.Ignore())
                .ForMember(dest => dest.TELEFONE, ori => ori.Ignore())
                .ForMember(dest => dest.CELULAR, ori => ori.Ignore())
                .ForMember(dest => dest.ATIVO, ori => ori.Ignore())
                .ForMember(dest => dest.EXECUTA_ROBO, ori => ori.Ignore())
                .ForMember(dest => dest.DT_SOLICITACAO_ROBO, ori => ori.Ignore())
                .ForMember(dest => dest.ROBO_EXECUTADO, ori => ori.Ignore())
                .ForMember(dest => dest.ROBO_TENTATIVAS_EXCEDIDAS, ori => ori.Ignore())
                .ForMember(dest => dest.COD_ERP, ori => ori.Ignore())
                .ForMember(dest => dest.NOVO_PJPF, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_CONTRATANTE, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_BASE_CONVITE, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_BASE_IMPORTACAO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_CATEGORIA, ori => ori.Ignore())
                .ForMember(dest => dest.ROBO, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_PJPF_SOLICITACAO_DOCUMENTOS, ori => ori.Ignore())
                .ForMember(dest => dest.WFD_SOLICITACAO, ori => ori.Ignore());

            CreateMap<FORNECEDORBASE, FichaCadastralWebForLinkVM>()
                .ForMember(destino => destino.RazaoSocial, origem => origem.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(destino => destino.CNPJ_CPF, origem => origem.MapFrom(x => x.CNPJ ?? x.CPF))
                .ForMember(destino => destino.NomeFantasia, origem => origem.MapFrom(x => x.NOME_FANTASIA))
                .ForMember(destino => destino.InscricaoMunicipal, origem => origem.MapFrom(x => x.INSCR_MUNICIPAL))
                .ForMember(destino => destino.DadosContatos, origem => origem.MapFrom(x => x.WFD_PJPF_BASE_CONTATOS))
                .ForMember(destino => destino.DadosEnderecos, origem => origem.MapFrom(x => x.WFD_PJPF_BASE_ENDERECO))
                .ForMember(destino => destino.FornecedoresUnspsc, origem => origem.MapFrom(x => x.WFD_PJPF_BASE_UNSPSC))
                .ForMember(destino => destino.ContratanteID, origem => origem.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(destino => destino.PjpfBaseId, origem => origem.MapFrom(x => x.ID));

            CreateMap<RegistroVM, Contratante>()
                .ForMember(destino => destino.TIPO_CADASTRO_ID, origem => origem.MapFrom(x => x.TipoCadastro))
                .ForMember(destino => destino.RAZAO_SOCIAL, origem => origem.MapFrom(x => x.RazaoSocial))
                .ForMember(destino => destino.NOME_FANTASIA, origem => origem.MapFrom(x => x.NomeFantasia))
                .ForMember(destino => destino.CNPJ, origem => origem.MapFrom(x => x.CNPJ))
                .ForMember(destino => destino.DATA_CADASTRO, origem => origem.MapFrom(x => DateTime.Now))
                .ForMember(destino => destino.ESTILO, origem => origem.MapFrom(x => "lilas"));

            CreateMap<RegistroVM, Usuario>()
                .ForMember(destino => destino.EMAIL, origem => origem.MapFrom(x => x.Email))
                .ForMember(destino => destino.SENHA, origem => origem.MapFrom(x => PasswordHash.CreateHash(x.Senha)))
                .ForMember(destino => destino.NOME, origem => origem.MapFrom(x => x.Nome))
                .ForMember(destino => destino.CPF_CNPJ, origem => origem.MapFrom(x => x.CPF))
                .ForMember(destino => destino.CARGO, origem => origem.MapFrom(x => x.Cargo))
                .ForMember(destino => destino.PRINCIPAL, origem => origem.MapFrom(x => true))
                .ForMember(destino => destino.LOGIN, origem => origem.MapFrom(x => x.login))
                .ForMember(destino => destino.DT_CRIACAO, origem => origem.MapFrom(x => DateTime.Now));

            #region Destinatario
            CreateMap<DESTINATARIO, DestinatariosVM>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.Nome, origem => origem.MapFrom(x => x.NOME))
                .ForMember(destino => destino.Email, origem => origem.MapFrom(x => x.EMAIL))
                .ForMember(destino => destino.Empresa, origem => origem.MapFrom(x => x.EMPRESA))
                .ForMember(destino => destino.Obs, origem => origem.MapFrom(x => x.OBS))
                .ForMember(destino => destino.Ativo, origem => origem.MapFrom(x => x.ATIVO))
                .ForMember(destino => destino.Sobrenome, origem => origem.MapFrom(x => x.SOBRENOME))
                .ForMember(destino => destino.TelefoneFixo, origem => origem.MapFrom(x => x.TELEFONE_FIXO))
                .ForMember(destino => destino.Celular, origem => origem.MapFrom(x => x.CELULAR))
                .ForMember(destino => destino.TelefoneTrabalho, origem => origem.MapFrom(x => x.TELEFONE_TRABALHO))
                .ForMember(destino => destino.Fax, origem => origem.MapFrom(x => x.FAX))
.ForMember(destino => destino.DataValidade, ori => ori.Ignore())
.ForMember(destino => destino.UrlDetalhar, ori => ori.Ignore())
.ForMember(destino => destino.Pagina, ori => ori.Ignore())
.ForMember(destino => destino.MensagemSucesso, ori => ori.Ignore())
.ForMember(destino => destino.MensagemErro, ori => ori.Ignore());

            CreateMap<DESTINATARIO, MeusDocumentosPesquisarEmailGridVM>()
            .ForMember(destino => destino.Id, origem => origem.MapFrom(x => x.ID))
            .ForMember(destino => destino.Email, origem => origem.MapFrom(x => x.EMAIL))
            .ForMember(destino => destino.Empresa, origem => origem.MapFrom(x => x.EMPRESA))
            .ForMember(destino => destino.Origem, origem => origem.MapFrom(x => TipoEmailVM.Destinatario))
            .AfterMap((src, dest) => dest.Nome = !string.IsNullOrEmpty(src.NOME) ? src.NOME.Replace(",", "") + " (" + src.EMAIL + ")" : src.EMAIL);
            CreateMap<DESTINATARIO, EmailsVM>()
                .ForMember(destino => destino.value, origem => origem.MapFrom(x => TipoEmailVM.Destinatario + ":" + x.ID.ToString() + ":" + x.EMAIL))
                .ForMember(destino => destino.text, origem => origem.MapFrom(x => (!string.IsNullOrEmpty(x.NOME) ? x.NOME.Replace(",", "") + " (" + x.EMAIL + ")" : x.EMAIL)));
            CreateMap<DestinatariosVM, DESTINATARIO>()
            .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
            .ForMember(destino => destino.NOME, origem => origem.MapFrom(x => x.Nome))
            .ForMember(destino => destino.EMAIL, origem => origem.MapFrom(x => x.Email))
            .ForMember(destino => destino.EMPRESA, origem => origem.MapFrom(x => x.Empresa))
            .ForMember(destino => destino.OBS, origem => origem.MapFrom(x => x.Obs))
            .ForMember(destino => destino.ATIVO, origem => origem.MapFrom(x => x.Ativo))
            .ForMember(destino => destino.EMAIL_AVULSO, origem => origem.MapFrom(x => false))
            .ForMember(destino => destino.SOBRENOME, origem => origem.MapFrom(x => x.Sobrenome))
            .ForMember(destino => destino.TELEFONE_FIXO, origem => origem.MapFrom(x => Mascara.RemoverMascaraTelefone(x.TelefoneFixo)))
            .ForMember(destino => destino.CELULAR, origem => origem.MapFrom(x => Mascara.RemoverMascaraTelefone(x.Celular)))
            .ForMember(destino => destino.TELEFONE_TRABALHO, origem => origem.MapFrom(x => Mascara.RemoverMascaraTelefone(x.TelefoneTrabalho)))
            .ForMember(destino => destino.FAX, origem => origem.MapFrom(x => Mascara.RemoverMascaraTelefone(x.Fax)));
            #endregion

            #region Documentos
            CreateMap<DocumentosCompartilhados, DocsCompartilhadosVM>()
        .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.DocumentosDoFornecedor.ID))
        .ForMember(destino => destino.TipoDocumento, origem => origem.MapFrom(x => x.DocumentosDoFornecedor.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO))
        .ForMember(destino => destino.DescricaoDocumento, origem => origem.MapFrom(x => x.DocumentosDoFornecedor.DescricaoDeDocumentos.DESCRICAO))
        .ForMember(destino => destino.NomeArquivo, origem => origem.MapFrom(x => x.DocumentosDoFornecedor.WFD_ARQUIVOS.NOME_ARQUIVO));

            CreateMap<DocumentosDoFornecedor, MeusDocumentosVM>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.TipoDocumento, origem => origem.MapFrom(x => x.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO))
                .ForMember(destino => destino.DescricaoDocumento, origem => origem.MapFrom(x => x.DescricaoDeDocumentos.DESCRICAO))
                .ForMember(destino => destino.NomeArquivo, origem => origem.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                .ForMember(destino => destino.DataUpload, origem => origem.MapFrom(x => x.DATA_UPLOAD))
                .ForMember(destino => destino.DataValidade, origem => origem.MapFrom(x => x.DATA_VENCIMENTO))
                .ForMember(destino => destino.DataEmissao, origem => origem.MapFrom(x => x.DATA_EMISSAO))
                .ForMember(destino => destino.Ativo, origem => origem.MapFrom(x => x.ATIVO))
                .ForMember(destino => destino.SemValidade, origem => origem.MapFrom(x => x.SEM_VALIDADE));
            CreateMap<MeusDocumentosVM, DocumentosDoFornecedor>()
                .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ID))
                .ForMember(destino => destino.DATA_EMISSAO, origem => origem.MapFrom(x => x.DataEmissao))
                .ForMember(destino => destino.DATA_VENCIMENTO, origem => origem.MapFrom(x => x.DataValidade))
                .ForMember(destino => destino.SEM_VALIDADE, origem => origem.MapFrom(x => x.SemValidade))
                .ForMember(destino => destino.ATIVO, origem => origem.MapFrom(x => x.Ativo));
            #endregion
        }

        public void MapeamentoPagSeguro()
        {

            //CreateMap<OrdemPagamento, PreCadastroAdesaoVM>()
            //    .ForMember(dest => dest.PlanoEscolhido, ori => ori.MapFrom(x => x.TipoConta))
            //    .ForMember(dest => dest.ResponsavelNome, ori => ori.MapFrom(x => x.Nome))
            //    .ForMember(dest => dest.ResponsavelCPF, ori => ori.MapFrom(x => x.Cpf))
            //    .ForMember(dest => dest.ResponsavelEmail, ori => ori.MapFrom(x => x.Email))
            //    .ForMember(dest => dest.ResponsavelTelefone, ori => ori.MapFrom(x => x.Telefone))
            //    .ForMember(dest => dest.EmpresaDocumento, ori => ori.Ignore())
            //    .ForMember(dest => dest.EmpresaCEP, ori => ori.Ignore())
            //    .ForMember(dest => dest.EmpresaEndereco, ori => ori.Ignore())
            //    .ForMember(dest => dest.EmpresaNumero, ori => ori.Ignore())
            //    .ForMember(dest => dest.EmpresaCidade, ori => ori.Ignore())
            //    .ForMember(dest => dest.EmpresaUF, ori => ori.Ignore())
            //    .ForMember(dest => dest.EmpresaPais, ori => ori.Ignore())
            //    .ForMember(dest => dest.EmpresaTelefone, ori => ori.Ignore())
            //    .ForMember(destino => destino.EmpresaComplemento, ori => ori.Ignore())
            //    .ForMember(destino => destino.EmpresaNome, ori => ori.Ignore())
            //    .ForMember(dest => dest.Referencia, ori => ori.MapFrom(x => x.Referencia));

            //CreateMap<PreCadastroAdesaoVM, OrdemPagamento>()
            //    .ForMember(ori => ori.Id, dest => dest.Ignore())
            //    .ForMember(ori => ori.Referencia, dest => dest.MapFrom(x => x.Referencia))
            //    .ForMember(ori => ori.TipoConta, dest => dest.MapFrom(x => x.PlanoEscolhido))
            //    .ForMember(dest => dest.TipoConta, ori => ori.MapFrom(x => x.PlanoEscolhido))
            //    .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.ResponsavelNome))
            //    .ForMember(dest => dest.Cpf, ori => ori.MapFrom(x => x.ResponsavelCPF))
            //    .ForMember(dest => dest.Email, ori => ori.MapFrom(x => x.ResponsavelEmail))
            //    .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.ResponsavelTelefone.Replace("-", "").Substring(5, 8)))
            //    .ForMember(dest => dest.Ddd, ori => ori.MapFrom(x => x.ResponsavelTelefone.Substring(1, 2)));


            CreateMap<SimplesNacional, RoboSimples>()
                .ForMember(ori => ori.Code, dest => dest.MapFrom(x => x.Code))
                .ForMember(ori => ori.cssCor, dest => dest.MapFrom(x => x.cssCor))
                .ForMember(ori => ori.DataConsulta, dest => dest.MapFrom(x => x.DataConsulta))
                .ForMember(ori => ori.HTML, dest => dest.MapFrom(x => x.HTML))
                .ForMember(ori => ori.tpPapel, dest => dest.MapFrom(x => x.tpPapel))
                .ForMember(ori => ori.UUID, dest => dest.MapFrom(x => x.UUID));
            CreateMap<SimplesNacional, DataSimples>()
                .ForMember(ori => ori.Message, dest => dest.MapFrom(x => x.Message))
                .ForMember(ori => ori.RazaoSocial, dest => dest.MapFrom(x => x.RazaoSocial))
                .ForMember(ori => ori.SIMEIPeriodosAnteriores, dest => dest.MapFrom(x => x.SIMEIPeriodosAnteriores))
                .ForMember(ori => ori.SimplesNacionalPeriodosAnteriores, dest => dest.MapFrom(x => x.SimplesNacionalPeriodosAnteriores))
                .ForMember(ori => ori.SituacaoSIMEI, dest => dest.MapFrom(x => x.SituacaoSIMEI))
                .ForMember(ori => ori.SituacaoSimplesNacional, dest => dest.MapFrom(x => x.SituacaoSimplesNacional));

            MapearNovasVM();
        }

        private void MapearNovasVM()
        {
            CreateMap<DESTINATARIO, DestinatariosVM>()
                .ForMember(d => d.ID, ori => ori.MapFrom(x => x.ID))
                .ForMember(d => d.Nome, ori => ori.MapFrom(x => x.NOME))
                .ForMember(d => d.Email, ori => ori.MapFrom(x => x.EMAIL))
                .ForMember(d => d.Empresa, ori => ori.MapFrom(x => x.EMPRESA))
                .ForMember(d => d.Ativo, ori => ori.MapFrom(x => x.ATIVO))
                .ForMember(d => d.EmailAvulso, ori => ori.MapFrom(x => x.EMAIL_AVULSO))
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore());
            //.ForMember(d => d.Url, opt => opt.ResolveUsing(res => res.Context.Options.Items["Url"]))
            //.AfterMap((dest, ori) => ori.Validar());

            CreateMap<FORNECEDOR_CATEGORIA, ListaPesquisaCategoriaVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.Codigo, ori => ori.MapFrom(x => x.CODIGO))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => x.DESCRICAO))
                .ForMember(dest => dest.Ativo, ori => ori.MapFrom(x => x.ATIVO))
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore())
                .ForMember(dest => dest.UrlExcluir, ori => ori.Ignore())
                .ForMember(dest => dest.UrlNovaSubCategoria, ori => ori.Ignore());
            //.ForMember(d => d.Url, opt => opt.ResolveUsing(res => res.Context.Options.Items["Url"]))
            //.AfterMap((dest, ori) => ori.Validar());

            CreateMap<WFD_CONTRATANTE_PJPF, ListaPesquisaFornecedorVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => Convert.ToUInt64(x.WFD_PJPF.CNPJ).ToString(@"00\.000\.000\/0000\-00")))
                .ForMember(dest => dest.CodigoERP, ori => ori.MapFrom(x => x.PJPF_COD_ERP))
                .ForMember(dest => dest.NomeEmpresa, ori => ori.MapFrom(x => x.WFD_PJPF.NOME))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.WFD_PJPF.RAZAO_SOCIAL))
                .ForMember(dest => dest.Status, ori => ori.MapFrom(x => x.WFD_PJPF.ATIVO))
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.FornecedorId, ori => ori.MapFrom(x => x.PJPF_ID))
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore());
            CreateMap<Fornecedor, ListaPesquisaFornecedorVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.CNPJ, ori => ori.MapFrom(x => Convert.ToUInt64(x.CNPJ).ToString(@"00\.000\.000\/0000\-00")))
                .ForMember(dest => dest.CodigoERP, ori => ori.MapFrom(x => x.WFD_CONTRATANTE_PJPF.FirstOrDefault(y => y.CONTRATANTE_ID == x.CONTRATANTE_ID).PJPF_COD_ERP))
                .ForMember(dest => dest.NomeEmpresa, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(dest => dest.Status, ori => ori.MapFrom(x => x.ATIVO))
                .ForMember(dest => dest.ContratanteId, ori => ori.MapFrom(x => x.CONTRATANTE_ID))
                .ForMember(dest => dest.FornecedorId, ori => ori.MapFrom(x => x.ID))
                .ForMember(dest => dest.UrlEditar, ori => ori.Ignore());


        }
    }
}