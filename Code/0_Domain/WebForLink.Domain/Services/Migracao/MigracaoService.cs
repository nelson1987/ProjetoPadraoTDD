using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.UnitOfWork;

using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Domain.Services.Migracao
{
    public interface IMigracaoService
    {
        void ContratanteParaFornecedorIndividual();
        void SolicitacaoParaFornecedor(int idSolicitacaoCadastro);
        void FornecedorParaFornecedorIndividual(int idFornecedor);
        void FornecedorIndividualParaContratante();
    }
    public class MigracaoService : PadraoService<IUnitOfWork>
    {
        #region Base
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly ISolicitacaoCadasatroFornecedorRepository _solicitacaoCadastroFornecedorRepository;
        public MigracaoService()
        {
            try
            {
                
                    
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void Dispose()
        {
            _unitOfWork.Finalizar();
        }
        #endregion
        public void ContratanteParaFornecedorIndividual() { }
        public void SolicitacaoParaFornecedor(int idSolicitacaoCadastro)
        {
            var solicitacaoCadastro = _solicitacaoCadastroFornecedorRepository.Buscar(x => x.ID == idSolicitacaoCadastro);
            #region Fornecedor e Robô
            Fornecedor fornecedor = new Fornecedor
            {
                TIPO_PJPF_ID = 2,//Fornecedor
                ROBO_ID = solicitacaoCadastro.ROBO_ID, //Seguir o Robo
                CONTRATANTE_ID = solicitacaoCadastro.WFD_SOLICITACAO.CONTRATANTE_ID,
                RAZAO_SOCIAL = solicitacaoCadastro.RAZAO_SOCIAL,
                CNPJ = solicitacaoCadastro.CNPJ,
                CPF = solicitacaoCadastro.CPF,
                ATIVO = true,
                NOME = solicitacaoCadastro.NOME,
                NOME_FANTASIA = solicitacaoCadastro.NOME_FANTASIA,
                CNAE = solicitacaoCadastro.CNAE,
                INSCR_ESTADUAL = solicitacaoCadastro.INSCR_ESTADUAL,
                INSCR_MUNICIPAL = solicitacaoCadastro.INSCR_MUNICIPAL,
                ENDERECO = solicitacaoCadastro.ENDERECO,
                NUMERO = solicitacaoCadastro.NUMERO,
                COMPLEMENTO = solicitacaoCadastro.COMPLEMENTO,
                CEP = solicitacaoCadastro.CEP,
                BAIRRO = solicitacaoCadastro.BAIRRO,
                CIDADE = solicitacaoCadastro.CIDADE,
                UF = solicitacaoCadastro.UF,
                PAIS = solicitacaoCadastro.PAIS,
                DT_NASCIMENTO = solicitacaoCadastro.DT_NASCIMENTO,
                //TELEFONE = "",
                //EMAIL = "",
                //RF_SIT_CADASTRAL_CNPJ = "",
                //IBGE_COD = "",
                //SINT_IE_COD = ,
                //SINT_IE_SITU_CADASTRAL = ,
                //SIMPLES_NACIONAL_SITUACAO = ,
                //SUFRAMA_SIT_CADASTRAL = ,
                //SUFRAMA_INSCRICAO = ,
                //SITUACAO_ID =  ,
                //RF_SIT_CADASTRAL_CNPJ_DT = ,
                //RF_CONSULTA_DTHR = ,
                //SINT_DTHR_CONSULTA =  ,
                //SINT_IE_SITU_CADASTRAL_DT = ,
                //SUFRAMA_SIT_CADASTRAL_VALIDADE = ,
                //DT_ATUALIZACAO_UNSPSC = ,
            };
            #endregion
            
            #region ContratanteFornecedor
            WFD_CONTRATANTE_PJPF contratanteFornecedor = new WFD_CONTRATANTE_PJPF()
            {
                CONTRATANTE_ID = solicitacaoCadastro.WFD_SOLICITACAO.CONTRATANTE_ID,
                PJPF_ID = fornecedor.ID,
                TP_PJPF = 2,
                PJPF_COD_ERP = "FRN01",
                CATEGORIA_ID = solicitacaoCadastro.CATEGORIA_ID,
                CRIA_USUARIO_ID = solicitacaoCadastro.WFD_SOLICITACAO.USUARIO_ID,
                PJPF_STATUS_ID = 1,
                CRIA_DT = DateTime.Now,
                PJPF_STATUS_DT = DateTime.Now,
                //PJPF_STATUS_TP_SOL = ,
                //PJPF_STATUS_ID_SOL = ,
            };
            #endregion
            fornecedor.WFD_CONTRATANTE_PJPF.Add(contratanteFornecedor);

            _fornecedorRepository.Inserir(fornecedor);
        }
        public void FornecedorParaFornecedorIndividual(int idFornecedor)
        {
        }
        public void FornecedorIndividualParaContratante() { }
    }
}
