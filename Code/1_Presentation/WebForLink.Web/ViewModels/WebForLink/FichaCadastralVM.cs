using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels.WebForLink
{
    public class FichaCadastralWebForLinkVM
    {
        public static FichaCadastralWebForLinkVM ModelToViewModel(Fornecedor model)
        {
            var viewModel = Mapper.Map<Fornecedor, FichaCadastralWebForLinkVM>(model);
            return viewModel;
        }
        public FichaCadastralWebForLinkVM()
        {
            Expansao = new ExpansaoVM();
            BancoList = new List<SelectListItem>();
            Questionarios = new RetornoQuestionario<QuestionarioVM>();
            FornecedoresUnspsc = new List<FornecedorUnspscVM>();
            DadosEnderecos = new List<DadosEnderecosVM>();
        }
        public FichaCadastralWebForLinkVM(bool exibeTudo)
        {
            AprovacaoProrrogacao = new AprovacaoPrazoVM();
            ReprovacaoProrrogacao = new ReprovacaoPrazoVM();
            MensagemImportacao = new MensagemImportacaoVM();
            TipoFuncionalidade = new EnumTiposFuncionalidade();
            PreCadastroEnum = new CasosPreCadastroEnum();
            ProrrogacaoPrazo = new ProrrogacaoPrazoVM();
            DadosBloqueio = new DadosBloqueioVM();
            Aprovacao = new AprovacaoVM();
            SolicitacaoFornecedor = new SolicitacaoFornecedorVM();
            Solicitacao = new SolicitacaoVM();
            Bloqueio = new BloqueioVM();
            Desbloqueio = new DesbloqueioVM();
            Expansao = new ExpansaoVM();
            FornecedorRobo = new FornecedorRoboVM();
            PrazoEntrega = new PrazoEntregaVM();
            Questionarios = new RetornoQuestionario<QuestionarioVM>();
            Categoria = new CategoriaFichaVM();
            DadosBancarios = new List<DadosBancariosVM>();
            DadosEnderecos = new List<DadosEnderecosVM>();
            DadosContatos = new List<DadosContatoVM>();
            BancoList = new List<SelectListItem>();
            SolicitacaoDocumentos = new List<SolicitacaoDocumentosVM>();
            ServicosMateriais = new List<ServicoMaterialVM>();
            FornecedoresUnspsc = new List<FornecedorUnspscVM>();
            Documentos = new List<DocumentosPJPFVM>();
            Contratantes = new List<ContratantesVM>();
        }
        public FichaCadastralWebForLinkVM(int idSolicitacao)
        {
            DadosBancarios = new List<DadosBancariosVM>();
            DadosContatos = new List<DadosContatoVM>();
            DadosEnderecos = new List<DadosEnderecosVM>();
            SolicitacaoFornecedor = new SolicitacaoFornecedorVM
            {
                Documentos = new List<SolicitacaoDocumentosVM>(),
                SolicitacaoCriacaoID = idSolicitacao
            };
            Aprovacao = new AprovacaoVM();
            Solicitacao = new SolicitacaoVM { Fluxo = new FluxoVM() };
            DadosBloqueio = new DadosBloqueioVM();
            FornecedorRobo = new FornecedorRoboVM();
            FornecedoresUnspsc = new List<FornecedorUnspscVM>();
        }
        public FichaCadastralWebForLinkVM(int idSolicitacao, AprovacaoVM aprovacao)
        {
            Aprovacao = aprovacao;
            DadosBancarios = new List<DadosBancariosVM>();
            DadosContatos = new List<DadosContatoVM>();
            FornecedorRobo = new FornecedorRoboVM();
            ProrrogacaoPrazo = new ProrrogacaoPrazoVM();
            SolicitacaoFornecedor = new SolicitacaoFornecedorVM
            {
                Solicitacao = true,
                SolicitacaoCriacaoID = idSolicitacao,
                Documentos = new List<SolicitacaoDocumentosVM>(),
            };
            Solicitacao = new SolicitacaoVM
            {
                Fluxo = new FluxoVM()
            };
        }

        public FichaCadastralWebForLinkVM(string nomeEmpresa,
            string razaoSocial,
            string nomeFantasia,
            //string cnae,
            string documento,
            string inscricaoEstadual,
            string inscricaoMunicipal)
        {
            Expansao = new ExpansaoVM();
            Solicitacao = new SolicitacaoVM();
            DadosBancarios = new List<DadosBancariosVM>();
            DadosContatos = new List<DadosContatoVM>();
            DadosEnderecos = new List<DadosEnderecosVM>();
            BancoList = new List<SelectListItem>();
            Questionarios = new RetornoQuestionario<QuestionarioVM>();
            NomeEmpresa = nomeEmpresa;
            RazaoSocial = razaoSocial;
            NomeFantasia = nomeFantasia;
            //CNAE = cnae;
            CNPJ_CPF = documento;
            InscricaoEstadual = inscricaoEstadual;
            InscricaoMunicipal = inscricaoMunicipal;
        }

        public FichaCadastralWebForLinkVM(int idContratante, CasosPreCadastroEnum preCadastroEnum)
        {
            DadosBancarios = new List<DadosBancariosVM>();
            DadosContatos = new List<DadosContatoVM>();
            DadosEnderecos = new List<DadosEnderecosVM>();
            FornecedoresUnspsc = new List<FornecedorUnspscVM>();
            ContratanteID = idContratante;
            IsPjpfBaseProprio = false;
            IsPjpfProprio = false;
            PreCadastroEnum = preCadastroEnum;
        }

        public int BloqueioId { get; set; }
        public string NomeFuncionalidade { get; set; }
        public string Mensagem { get; set; }
        public string Assunto { get; set; }
        public bool AtualizacaoDocumento { get; set; }
        public int DocumentoId { get; set; }
        /// <summary>
        /// Se modelo veio da tabela FORNECEDORBASE
        /// </summary>
        public bool IsPjpfBaseProprio { get; set; }
        /// <summary>
        /// Se modelo veio da tabela Fornecedor
        /// </summary>
        public bool IsPjpfProprio { get; set; }

        public int ID { get; set; }

        public int ContratanteID { get; set; }

        public int ContratanteFornecedorID { get; set; }

        public int PJPFID { get; set; }

        public int PjpfBaseId { get; set; }

        public int? SolicitacaoID { get; set; }

        public int? CategoriaId { get; set; }

        public string ActionOrigem { get; set; }

        public string ControllerOrigem { get; set; }

        public string CategoriaNome { get; set; }

        [Display(Name = "Empresa")]
        public string NomeEmpresa { get; set; }

        [Display(Name = "Razão Social")]
        public string RazaoSocial { get; set; }

        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }

        //[Display(Name = "CNAE")]
        //public string CNAE { get; set; }

        [Display(Name = "CNPJ/CPF")]
        public string CNPJ_CPF { get; set; }

        [Display(Name = "Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }

        [Display(Name = "Inscrição Municipal")]
        [MaxLength(20, ErrorMessage = "O máximo permitido são 20 caracteres")]
        public string InscricaoMunicipal { get; set; }

        [Display(Name = "Tipo Logradouro")]
        public string TipoLogradouro { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "CEP")]
        public string Cep { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "País")]
        public string Pais { get; set; }

        [Display(Name = "Observações")]
        public string Observacao { get; set; }

        [Display(Name = "Descrição da Alteração")]
        public string OutrosDadosDescricaoMudança { get; set; }

        [Display(Name = "Visão")]
        public int OutrosDadosVisao { get; set; }

        [Display(Name = "Grupo")]
        public int OutrosDadosGrupo { get; set; }

        [Display(Name = "Descrição")]
        public int OutrosDadosDescricao { get; set; }

        public int TipoFornecedor { get; set; }

        public int TipoPreenchimento { get; set; }

        public bool HabilitaEdicao { get; set; }

        public bool HabilitaEdicaoUnspsc { get; set; }
        public bool isCPF { get; set; }

        public DateTime? DataAtualizacaoUnspsc { get; set; }

        public string RoboReceita { get; set; }
        public string RoboSintegra { get; set; }
        public string RoboSimples { get; set; }

        public bool DocumentoEditavel { get; set; }
        /// <summary>
        /// Propriedade para validar se essa é uma ficha cadastral de um fornecedor já existente no banco
        /// </summary>
        public bool FichaFornecedor { get; set; }

        /// <summary>
        /// Propriedade para validar se deverá ser apenas salvo ou salvo e finalizado - True: Salvar/False: Salvar+Finalizar
        /// </summary>
        public bool ApenasSalvar { get; set; }

        /// <summary>
        /// Propriedade para validar se o termo de aceite foi aceito pelo fornecedor - True: Aceito/False: Não aceito
        /// </summary>
        public bool TermoAceite { get; set; }

        /// <summary>
        /// Propriedade do Texto do termo de aceite para ser exibido antes da ficha cadastral
        /// </summary>
        public string TextoTermoAceite { get; set; }

        /// <summary>
        /// Propriedade com a data máxima de preenchimento da ficha cadastral
        /// </summary>
        public string PrazoPreenchimento { get; set; }

        /// <summary>
        /// Propriedade com a chave url que recebe quando o fornecedor clica no link no email. Ela é repassada de novo por get se o fornecedor tiver mais de um contratante.
        /// </summary>
        public string ChaveUrl { get; set; }

        [Display(Name = "Data de Prorrogação")]
        public DateTime? DataProrrogacao { get; set; }

        [Display(Name = "Motivo")]
        public string MotivoProrrogacao { get; set; }

        public AprovacaoPrazoVM AprovacaoProrrogacao { get; set; }
        public ReprovacaoPrazoVM ReprovacaoProrrogacao { get; set; }
        public MensagemImportacaoVM MensagemImportacao { get; set; }
        public EnumTiposFuncionalidade TipoFuncionalidade { get; set; }
        public CasosPreCadastroEnum PreCadastroEnum { get; set; }
        public ProrrogacaoPrazoVM ProrrogacaoPrazo { get; set; }
        public DadosBloqueioVM DadosBloqueio { get; set; }
        public AprovacaoVM Aprovacao { get; set; }
        public SolicitacaoFornecedorVM SolicitacaoFornecedor { get; set; }
        public SolicitacaoVM Solicitacao { get; set; }
        public BloqueioVM Bloqueio { get; set; }
        public DesbloqueioVM Desbloqueio { get; set; }
        public ExpansaoVM Expansao { get; set; }
        public FornecedorRoboVM FornecedorRobo { get; set; }
        public PrazoEntregaVM PrazoEntrega { get; set; }
        public RetornoQuestionario<QuestionarioVM> Questionarios { get; set; }
        public CategoriaFichaVM Categoria { get; set; }
        public List<SelectListItem> BancoList { get; set; }
        public List<DadosBancariosVM> DadosBancarios { get; set; }
        public List<DadosEnderecosVM> DadosEnderecos { get; set; }
        public List<DadosContatoVM> DadosContatos { get; set; }
        public List<SolicitacaoDocumentosVM> SolicitacaoDocumentos { get; set; }
        public ICollection<ServicoMaterialVM> ServicosMateriais { get; set; }
        public ICollection<FornecedorUnspscVM> FornecedoresUnspsc { get; set; }
        public ICollection<DocumentosPJPFVM> Documentos { get; set; }
        public ICollection<ContratantesVM> Contratantes { get; set; }
    }
}