using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.UnitOfWork;

using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Domain.Services.Integracao
{
    public interface IVendorListService
    {
        void CriarNovoFornecedor(string nome, string documento, bool prequalificado, CONTRATANTE contratanteCH);
    }
    public class VendorListService : AppService<WebForLinkContexto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISolicitacaoRepository _solicitacaoRepository;
        private readonly IRoboRepository _roboRepository;
        private readonly IContratantePjpfRepository _contratanteFornecedor;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorBancoRepository _bancoFornecedorRepository;
        private readonly IFornecedorEnderecoRepository _enderecoFornecedorRepository;
        private readonly IFornecedorContatoRepository _contatoFornecedorRepository;
        private readonly IFornecedorDocumentoRepository _documentoFornecedorRepository;
        private readonly IFornecedorUnspscRepository _unspscFornecedorRepository;
        private readonly IFornecedorInformacaoComplementarComplRepository _informacaoComplementarFornecedorRepository;

        public void Dispose()
        {
            _unitOfWork.Finalizar();
        }

        public VendorListService(
            IUnitOfWork unitOfWork,
        ISolicitacaoRepository solicitacaoRepository,
        IRoboRepository roboRepository,
        IContratantePjpfRepository contratanteFornecedor,
        IFornecedorRepository fornecedorRepository,
        IFornecedorBancoRepository bancoFornecedorRepository,
        IFornecedorEnderecoRepository enderecoFornecedorRepository,
        IFornecedorContatoRepository contatoFornecedorRepository,
        IFornecedorDocumentoRepository documentoFornecedorRepository,
        IFornecedorUnspscRepository unspscFornecedorRepository,
        IFornecedorInformacaoComplementarComplRepository informacaoComplementarFornecedorRepository)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _solicitacaoRepository = solicitacaoRepository;
                _roboRepository = roboRepository;
                _contratanteFornecedor = contratanteFornecedor;
                _fornecedorRepository = fornecedorRepository;
                _bancoFornecedorRepository = bancoFornecedorRepository;
                _enderecoFornecedorRepository = enderecoFornecedorRepository;
                _contatoFornecedorRepository = contatoFornecedorRepository;
                _documentoFornecedorRepository = documentoFornecedorRepository;
                _unspscFornecedorRepository = unspscFornecedorRepository;
                _informacaoComplementarFornecedorRepository = informacaoComplementarFornecedorRepository;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }
        /*
        public void IncluirNovoContratante(string nome, string documento)
        {
            WFD_CONTRATANTE contratante = new WFD_CONTRATANTE()
            {
                ATIVO = true,
                ATIVO_DT = DateTime.Now,
                CNPJ = documento,
                COD_WEBFORMAT = "",
                CONTRANTE_COD_ERP = "",
                DATA_CADASTRO = DateTime.Now,
                DATA_NASCIMENTO = null,
                ESTILO = "Azul",
                EXTENSAO_IMAGEM = ".png",
                NOME_FANTASIA = "",
                RAZAO_SOCIAL = "",
                TIPO_CADASTRO_ID = 1,
                TIPO_CONTRATANTE_ID = 1
            };
            //18.306.371/0001-61
            WFD_PJPF fornecedor = new WFD_PJPF()
            {
                RAZAO_SOCIAL = "",
                CNPJ = "",
                CPF = "",
                TELEFONE = "",
                ATIVO = true,
                NOME = "",
                NOME_FANTASIA = "",
                CNAE = "",
                INSCR_ESTADUAL = "",
                INSCR_MUNICIPAL = "",
                ENDERECO = "",
                NUMERO = "",
                COMPLEMENTO = "",
                CEP = "",
                BAIRRO = "",
                CIDADE = "",
                UF = "",
                PAIS = "",
                EMAIL = "",
                RF_SIT_CADASTRAL_CNPJ = "",
                IBGE_COD = "",
                SINT_IE_COD = "",
                SINT_IE_SITU_CADASTRAL = "",
                SIMPLES_NACIONAL_SITUACAO = "",
                SUFRAMA_SIT_CADASTRAL = "",
                SUFRAMA_INSCRICAO = "",
                TIPO_PJPF_ID = "",
                SITUACAO_ID = "",
                ROBO_ID = "",
                RF_SIT_CADASTRAL_CNPJ_DT = "",
                RF_CONSULTA_DTHR = "",
                SINT_DTHR_CONSULTA = "",
                SINT_IE_SITU_CADASTRAL_DT = "",
                SUFRAMA_SIT_CADASTRAL_VALIDADE = "",
                DT_NASCIMENTO = "",
                DT_ATUALIZACAO_UNSPSC = "",
                T_UF = new T_UF(),
                WFD_PJPF_ROBO = ,
                WFD_CONTRATANTE_PJPF = ,
                WFD_PJPF_CONTRATANTE_ORG_COMPRAS = ,
                WFD_PJPF_DOCUMENTOS = ,
                WFD_PJPF_ROBO_LOG = ,
                WFD_PJPF_UNSPSC = ,
                WFD_SOL_MOD_ENDERECO = ,
                WFD_SOLICITACAO = ,
                WFD_CONTRATANTE = contratante
            };

            WFD_CONTRATANTE_PJPF contratantePjpf = new WFD_CONTRATANTE_PJPF();
        }
        public void IncluirNovoFornecedorIndividual(string nome, string documento)
        {
            //12.310.142/0001-34

        }
        //23.048.843/0001-63
        public void IncluirFornecedor(string nome, string documento, bool prequalificado, string contratante)
        {
            WFD_CONTRATANTE contratanteCH = Processo.Contratante.Buscar(x => x.NOME_FANTASIA.Contains(contratante) && x.RAZAO_SOCIAL.Contains(contratante));
            CriarNovoFornecedor(nome, documento, prequalificado, contratanteCH);
        }
        public void IncluirFornecedor(string nome, string documento, bool prequalificado)
        {
            WFD_CONTRATANTE contratanteCH = Processo.Contratante.Buscar(x => x.ID == 1);
            CriarNovoFornecedor(nome, documento, prequalificado, contratanteCH);
        }
        */
        public void CriarNovoFornecedor(string nome, string documento, bool prequalificado, CONTRATANTE contratanteCH)
        {
            Fornecedor fornecedor = new Fornecedor
            {
                CONTRATANTE_ID = contratanteCH.ID,
                TIPO_PJPF_ID = 2,
                RAZAO_SOCIAL = nome,
                NOME_FANTASIA = nome,
                NOME = nome,
                CNPJ = documento,
                ENDERECO = "Rua José Domingues",
                NUMERO = "198",
                BAIRRO = "Centro",
                CIDADE = "Rio de Janeiro",
                UF = "RJ",
                CEP = "20745-366",
                PAIS = "BRASIL",
                ATIVO = true,
            };
            WFD_CONTRATANTE_PJPF contratanteBuscado = _contratanteFornecedor.Buscar(x => x.ID == 33);
            WFD_CONTRATANTE_PJPF contratantePjPf = new WFD_CONTRATANTE_PJPF
            {
                CATEGORIA_ID = contratanteBuscado.CATEGORIA_ID,
                CONTRATANTE_ID = contratanteCH.ID,
                PJPF_COD_ERP = "E5452",
                PJPF_STATUS_ID = contratanteBuscado.PJPF_STATUS_ID,
                PJPF_STATUS_ID_SOL = contratanteBuscado.PJPF_STATUS_ID_SOL,
                TP_PJPF = contratanteBuscado.TP_PJPF,
            };
            //Bancos
            int[] idsBanco = new[] { 46, 48 };
            List<FORNECEDOR_BANCO> listaBancosBuscados = _bancoFornecedorRepository.Listar(x => idsBanco.Contains(x.ID)).ToList();

            foreach (var banco in listaBancosBuscados)
            {
                contratantePjPf.WFD_PJPF_BANCO.Add(new FORNECEDOR_BANCO
                {
                    BANCO_ID = banco.BANCO_ID,
                    AGENCIA = banco.AGENCIA,
                    AG_DV = banco.AG_DV,
                    CONTA = banco.CONTA,
                    CONTA_DV = banco.CONTA_DV,
                    ATIVO = banco.ATIVO,
                    CONTRATANTE_PJPF_ID = contratantePjPf.ID,
                    ARQUIVO_ID = banco.ARQUIVO_ID,
                    NOME_ARQUIVO = banco.NOME_ARQUIVO,
                    DATA_UPLOAD = banco.DATA_UPLOAD,
                    T_BANCO = banco.T_BANCO
                });
            }
            //Contatos
            int[] idsContatos = new[] { 44, 45 };
            List<FORNECEDOR_CONTATOS> listaContatosBuscados = _contatoFornecedorRepository.Listar(x => idsContatos.Contains(x.ID)).ToList();

            foreach (var contato in listaContatosBuscados)
            {
                contratantePjPf.WFD_PJPF_CONTATOS.Add(new FORNECEDOR_CONTATOS
                {
                    CONTRAT_ORG_COMPRAS_ID = contato.CONTRAT_ORG_COMPRAS_ID,
                    NOME = contato.NOME,
                    EMAIL = contato.EMAIL,
                    TELEFONE = contato.TELEFONE,
                    CELULAR = contato.CELULAR,
                    CONTRATANTE_PJPF_ID = contratantePjPf.ID,
                    TP_CONTATO_ID = contato.TP_CONTATO_ID,
                    WFD_T_TP_CONTATO = contato.WFD_T_TP_CONTATO,
                });
            }
            //Endereços
            int[] idsEnderecos = new[] { 44, 45 };
            List<FORNECEDOR_ENDERECO> listaEnderecosBuscados = _enderecoFornecedorRepository.Listar(x => idsEnderecos.Contains(x.ID)).ToList();

            foreach (var endereco in listaEnderecosBuscados)
            {
                contratantePjPf.WFD_PJPF_ENDERECO.Add(new FORNECEDOR_ENDERECO
                {
                    TP_ENDERECO_ID = endereco.TP_ENDERECO_ID,
                    ENDERECO = endereco.ENDERECO,
                    NUMERO = endereco.NUMERO,
                    COMPLEMENTO = endereco.COMPLEMENTO,
                    CEP = endereco.CEP,
                    BAIRRO = endereco.BAIRRO,
                    CIDADE = endereco.CIDADE,
                    UF = endereco.UF,
                    PAIS = endereco.PAIS,
                    CONTRATANTE_PJPF_ID = contratantePjPf.ID,
                    T_UF = endereco.T_UF,
                });
            }

            fornecedor.WFD_CONTRATANTE_PJPF.Add(contratantePjPf);
            //Robô
            if (prequalificado)
            {

                fornecedor.WFD_PJPF_ROBO = _roboRepository.Buscar(x => x.ID == 1040);
                /*
                WFD_PJPF_ROBO roboBuscado = _roboFornecedorRepository.Buscar(x => x.ID == 890);
                fornecedor.WFD_PJPF_ROBO = new WFD_PJPF_ROBO
                {
                    ROBO_DT_EXEC = roboBuscado.ROBO_DT_EXEC,
                    SOLICITACAO_ID = roboBuscado.SOLICITACAO_ID,
                    CPF = roboBuscado.CPF,
                    CNPJ = documento,
                    RECEITA_FEDERAL_RAZAO_SOCIAL = roboBuscado.RECEITA_FEDERAL_RAZAO_SOCIAL,
                    RF_NOME_FANTASIA = roboBuscado.RF_NOME_FANTASIA,
                    RF_NOME = roboBuscado.RF_NOME,
                    RF_LOGRADOURO = roboBuscado.RF_LOGRADOURO,
                    RF_NUMERO = roboBuscado.RF_NUMERO,
                    RF_COMPLEMENTO = roboBuscado.RF_COMPLEMENTO,
                    RF_BAIRRO = roboBuscado.RF_BAIRRO,
                    RF_MUNICIPIO = roboBuscado.RF_MUNICIPIO,
                    RF_UF = roboBuscado.RF_UF,
                    RF_CEP = roboBuscado.RF_CEP,
                    RF_SIT_CADASTRAL_CNPJ = "ATIVA",
                    RF_SIT_CADSTRAL_CNPJ_DT = roboBuscado.RF_SIT_CADSTRAL_CNPJ_DT,
                    RF_SIT_ESPECIAL_CNPJ = roboBuscado.RF_SIT_ESPECIAL_CNPJ,
                    RF_SIT_ESPECIAL_CNPJ_DT = roboBuscado.RF_SIT_ESPECIAL_CNPJ_DT,
                    RF_MOTIVO_CNPJ_SITU_CADASTRAL = roboBuscado.RF_MOTIVO_CNPJ_SITU_CADASTRAL,
                    RF_CNPJ_DT_ABERTURA = roboBuscado.RF_CNPJ_DT_ABERTURA,
                    RF_CNAE_COD_PRINCIPAL = roboBuscado.RF_CNAE_COD_PRINCIPAL,
                    RF_CNAE_DSC_PRINCIPAL = roboBuscado.RF_CNAE_DSC_PRINCIPAL,
                    RF_CNAE_COD_OUTROS = roboBuscado.RF_CNAE_COD_OUTROS,
                    RF_CNAE_DSC_OUTROS = roboBuscado.RF_CNAE_DSC_OUTROS,
                    RF_MATRIZ_FILIAL = roboBuscado.RF_MATRIZ_FILIAL,
                    RF_COD_NATUREZA_JURIDICA = roboBuscado.RF_COD_NATUREZA_JURIDICA,
                    RF_DSC_NATUREZA_JURIDICA = roboBuscado.RF_DSC_NATUREZA_JURIDICA,
                    RF_CONSULTA_DTHR = roboBuscado.RF_CONSULTA_DTHR,
                    IBGE_COD = roboBuscado.IBGE_COD,
                    SINTEGRA_ERRO_ORIGINAL = roboBuscado.SINTEGRA_ERRO_ORIGINAL,
                    SINT_IE_QTD = roboBuscado.SINT_IE_QTD,
                    SINT_IE_MULTIPLA = roboBuscado.SINT_IE_MULTIPLA,
                    SINT_IE_MULTIPLA_CODIGOS = roboBuscado.SINT_IE_MULTIPLA_CODIGOS,
                    SINT_IE_MULTIPLA_SITUACAO = roboBuscado.SINT_IE_MULTIPLA_SITUACAO,
                    SINT_IE_COD = roboBuscado.SINT_IE_COD,
                    SINT_CONSULTA_DTHR = roboBuscado.SINT_CONSULTA_DTHR,
                    SINT_IE_SITU_CADASTRAL = "HABILITADO",
                    SINT_IE_SITU_CADSTRAL_DT = roboBuscado.SINT_IE_SITU_CADSTRAL_DT,
                    SINT_INCLUSAO_DT = roboBuscado.SINT_INCLUSAO_DT,
                    SINT_BAIXA_DT = roboBuscado.SINT_BAIXA_DT,
                    SINT_BAIXA_MOTIVO = roboBuscado.SINT_BAIXA_MOTIVO,
                    SINT_EMAIL = roboBuscado.SINT_EMAIL,
                    SINT_REGIME_APURACAO = roboBuscado.SINT_REGIME_APURACAO,
                    SINT_ENQUADRAMENTO_FISCAL = roboBuscado.SINT_ENQUADRAMENTO_FISCAL,
                    SINT_TEL = roboBuscado.SINT_TEL,
                    SINT_CAD_PROD_RURAL = roboBuscado.SINT_CAD_PROD_RURAL,
                    SINT_COMPLEMENTO = roboBuscado.SINT_COMPLEMENTO,
                    SINT_RAZAO_SOCIAL = roboBuscado.SINT_RAZAO_SOCIAL,
                    SINT_CNPJ = roboBuscado.SINT_CNPJ,
                    SINT_BAIRRO = roboBuscado.SINT_BAIRRO,
                    SINT_LOGRADOURO = roboBuscado.SINT_LOGRADOURO,
                    SINT_NUMERO = roboBuscado.SINT_NUMERO,
                    SINT_CEP = roboBuscado.SINT_CEP,
                    SINT_MUNICIPIO = roboBuscado.SINT_MUNICIPIO,
                    SINT_UF = roboBuscado.SINT_UF,
                    SINT_ATIVIDADE_PRINCIPAL = roboBuscado.SINT_ATIVIDADE_PRINCIPAL,
                    SIMPLES_NACIONAL_SITUACAO = roboBuscado.SIMPLES_NACIONAL_SITUACAO,
                    SN_SITUACAO_SIMEI = roboBuscado.SN_SITUACAO_SIMEI,
                    SN_PERIODOS_ANTERIORES = roboBuscado.SN_PERIODOS_ANTERIORES,
                    SN_SIMEI_PERIODOS_ANTERIORES = roboBuscado.SN_SIMEI_PERIODOS_ANTERIORES,
                    SN_AGENDAMENTOS = roboBuscado.SN_AGENDAMENTOS,
                    SN_RAZAO_SOCIAL = roboBuscado.SN_RAZAO_SOCIAL,
                    CORREIOS_TP_LOGRADOURO = roboBuscado.CORREIOS_TP_LOGRADOURO,
                    CORR_LOGRADOURO = roboBuscado.CORR_LOGRADOURO,
                    CORR_COMPLEMENTO = roboBuscado.CORR_COMPLEMENTO,
                    CORR_BAIRRO = roboBuscado.CORR_BAIRRO,
                    CORR_BAIRRO_COMPL = roboBuscado.CORR_BAIRRO_COMPL,
                    CORR_UF = roboBuscado.CORR_UF,
                    CORR_MUNICIPIO = roboBuscado.CORR_MUNICIPIO,
                    CORR_CEP = roboBuscado.CORR_CEP,
                    SUFRAMA_ERRO_MENSAGEM = roboBuscado.SUFRAMA_ERRO_MENSAGEM,
                    SUFRAMA_SIT_CADASTRAL = roboBuscado.SUFRAMA_SIT_CADASTRAL,
                    SUFRAMA_INSCRICAO = roboBuscado.SUFRAMA_INSCRICAO,
                    SUFRAMA_TEL = roboBuscado.SUFRAMA_TEL,
                    SUFRAMA_SIT_CADASTRAL_VALIDADE = roboBuscado.SUFRAMA_SIT_CADASTRAL_VALIDADE,
                    SUFRAMA_INCENTIVOS = roboBuscado.SUFRAMA_INCENTIVOS,
                    SUFRAMA_EMAIL = roboBuscado.SUFRAMA_EMAIL,
                    RF_CERTIFICADO_HTML = roboBuscado.RF_CERTIFICADO_HTML,
                    SINT_CERTIFICADO_HTML = roboBuscado.SINT_CERTIFICADO_HTML,
                    SN_CONSULTA_DTHR = roboBuscado.SN_CONSULTA_DTHR,
                    SUFRAMA_CONSULTA_DTHR = roboBuscado.SUFRAMA_CONSULTA_DTHR,
                    CORR_CONSULTA_DTHR = roboBuscado.CORR_CONSULTA_DTHR,
                    RF_CONTADOR_TENTATIVA = roboBuscado.RF_CONTADOR_TENTATIVA,
                    SINT_CONTADOR_TENTATIVA = roboBuscado.SINT_CONTADOR_TENTATIVA,
                    SN_CONTADOR_TENTATIVA = roboBuscado.SN_CONTADOR_TENTATIVA,
                    SUFRAMA_CONTADOR_TENTATIVA = roboBuscado.SUFRAMA_CONTADOR_TENTATIVA,
                    CORR_CONTADOR_TENTATIVA = roboBuscado.CORR_CONTADOR_TENTATIVA,
                    SUFRAMA_CERTIFICADO_HTML = roboBuscado.SUFRAMA_CERTIFICADO_HTML,
                    CORR_CERTIFICADO_HTML = roboBuscado.CORR_CERTIFICADO_HTML,
                    RF_CODE_ROBO = roboBuscado.RF_CODE_ROBO,
                    SINT_CODE_ROBO = roboBuscado.SINT_CODE_ROBO,
                    SN_CODE_ROBO = roboBuscado.SN_CODE_ROBO
                };
                */
            }
            //UNSPSC

            //int[] idUnspsc = new[] { 110, 111, 112 };
            int[] idUnspsc = new[] { 4795, 4797, 4800, 4807, 5952 };
            List<FORNECEDOR_UNSPSC> unspscBuscado = _unspscFornecedorRepository.Listar(x => idUnspsc.Contains(x.ID)).ToList();

            foreach (var unspsc in unspscBuscado)
            {
                unspsc.PJPF_ID = fornecedor.ID;
                fornecedor.FornecedorServicoMaterialList.Add(unspsc);
            }
            _fornecedorRepository.Inserir(fornecedor);
        }
    }
}
