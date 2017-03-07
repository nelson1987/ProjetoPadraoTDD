using System;
using System.IO;
using System.Linq;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.UnitOfWork;
using WebForLink.Repository.Repositories;
using WebForLink.Service.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Domain.Services.PreConfiguracao.Adesao
{
    public interface IFornecedorIndividualService
    {
        void IncluirFornecedorIndividual(string documento);
    }
    public class FornecedorIndividualService : PadraoService<IUnitOfWork>, IFornecedorIndividualService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPapelRepository _papelRepository;
        private readonly ISolicitacaoCadastroFornecedorRepository _solicitacaoCadastroRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IContratanteRepository _contratanteRepository;
        private readonly IUnitOfWork _unitOfWork;
        public FornecedorIndividualService(IUnitOfWork unitOfWork,
            IUsuarioRepository productReviewRepository,
            IPapelRepository papel, 
            ISolicitacaoCadastroFornecedorRepository solicitacaoCadastro,
            IFornecedorRepository fornecedor,
            IContratanteRepository contratante)
        {
            try
            {
                _unitOfWork = unitOfWork;
                _usuarioRepository = productReviewRepository;
                _papelRepository = papel;
                _solicitacaoCadastroRepository = solicitacaoCadastro;
                _fornecedorRepository = fornecedor;
                _contratanteRepository = contratante;
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
        public void IncluirFornecedorIndividual(string documento)
        {
            SolicitacaoCadastroFornecedor solicitacaoCadastroFornecedor = _solicitacaoCadastroRepository.Buscar(x => x.CNPJ == documento || x.CPF == documento);
            SOLICITACAO solicitacao = solicitacaoCadastroFornecedor.WFD_SOLICITACAO;
            ROBO roboFornecedor = solicitacaoCadastroFornecedor.WFD_PJPF_ROBO;
            Domain.Models.Fornecedor fornecedor = new Domain.Models.Fornecedor
            {
                CONTRATANTE_ID = solicitacao.CONTRATANTE_ID,
                TIPO_PJPF_ID = solicitacaoCadastroFornecedor.PJPF_TIPO,
                RAZAO_SOCIAL = solicitacaoCadastroFornecedor.RAZAO_SOCIAL,
                NOME_FANTASIA = solicitacaoCadastroFornecedor.NOME_FANTASIA,
                NOME = solicitacaoCadastroFornecedor.NOME,
                CNPJ = solicitacaoCadastroFornecedor.CNPJ,
                CPF = solicitacaoCadastroFornecedor.CPF,
                CNAE = solicitacaoCadastroFornecedor.CNAE,
                INSCR_ESTADUAL = solicitacaoCadastroFornecedor.INSCR_ESTADUAL,
                INSCR_MUNICIPAL = solicitacaoCadastroFornecedor.INSCR_MUNICIPAL,
                ENDERECO = solicitacaoCadastroFornecedor.ENDERECO,
                NUMERO = solicitacaoCadastroFornecedor.NUMERO,
                COMPLEMENTO = solicitacaoCadastroFornecedor.COMPLEMENTO,
                BAIRRO = solicitacaoCadastroFornecedor.BAIRRO,
                CIDADE = solicitacaoCadastroFornecedor.CIDADE,
                UF = solicitacaoCadastroFornecedor.UF,
                CEP = solicitacaoCadastroFornecedor.CEP,
                PAIS = solicitacaoCadastroFornecedor.PAIS,
                ATIVO = true,
            };
            if (roboFornecedor != null && solicitacaoCadastroFornecedor.PJPF_TIPO != 2)
                fornecedor.ROBO_ID = roboFornecedor.ID;

            #region Unspsc
            foreach (var item in solicitacao.WFD_SOL_UNSPSC)
            {
                fornecedor.FornecedorServicoMaterialList.Add(new FORNECEDOR_UNSPSC
                {
                    SOLICITACAO_ID = solicitacao.ID,
                    UNSPSC_ID = item.UNSPSC_ID,
                    DT_INCLUSAO = DateTime.Now,
                    WFD_PJPF = fornecedor
                });
            }
            #endregion

            WFD_CONTRATANTE_PJPF contratanteFornecedor = new WFD_CONTRATANTE_PJPF
            {
                CATEGORIA_ID = solicitacaoCadastroFornecedor.CATEGORIA_ID,
                CONTRATANTE_ID = solicitacao.CONTRATANTE_ID,
                PJPF_ID = fornecedor.ID,
                PJPF_COD_ERP = solicitacaoCadastroFornecedor.COD_PJPF_ERP,
                PJPF_STATUS_ID = 1,
                PJPF_STATUS_ID_SOL = solicitacao.ID,
                TP_PJPF = 2
            };

            #region Bancos
            foreach (var item in solicitacao.WFD_SOL_MOD_BANCO)
            {
                contratanteFornecedor.WFD_PJPF_BANCO.Add(new FORNECEDOR_BANCO
                {
                    CONTRATANTE_PJPF_ID = contratanteFornecedor.ID,
                    BANCO_ID = item.BANCO_ID,
                    AGENCIA = item.AGENCIA,
                    AG_DV = item.AG_DV,
                    CONTA = item.CONTA,
                    CONTA_DV = item.CONTA_DV,
                    ATIVO = true,
                    DATA_UPLOAD = item.DATA_UPLOAD,
                    NOME_ARQUIVO = item.NOME_ARQUIVO,
                    ARQUIVO_ID = item.ARQUIVO_ID
                });
            }
            #endregion

            #region Endereços
            foreach (var solicitacaoModificacaoEndereco in solicitacao.WFD_SOL_MOD_ENDERECO)
            {
                contratanteFornecedor.WFD_PJPF_ENDERECO.Add(new FORNECEDOR_ENDERECO
                {
                    BAIRRO = solicitacaoModificacaoEndereco.BAIRRO,
                    CEP = solicitacaoModificacaoEndereco.CEP,
                    CIDADE = solicitacaoModificacaoEndereco.CIDADE,
                    COMPLEMENTO = solicitacaoModificacaoEndereco.COMPLEMENTO,
                    ENDERECO = solicitacaoModificacaoEndereco.ENDERECO,
                    NUMERO = solicitacaoModificacaoEndereco.NUMERO,
                    PAIS = solicitacaoModificacaoEndereco.PAIS,
                    T_UF = solicitacaoModificacaoEndereco.T_UF,
                    TP_ENDERECO_ID = solicitacaoModificacaoEndereco.TP_ENDERECO_ID,
                    UF = solicitacaoModificacaoEndereco.UF,
                    CONTRATANTE_PJPF_ID = contratanteFornecedor.ID
                });
            }
            #endregion

            #region Contatos

            foreach (var item in solicitacao.WFD_SOL_MOD_CONTATO)
            {
                contratanteFornecedor.WFD_PJPF_CONTATOS.Add(new FORNECEDOR_CONTATOS
                {
                    CONTRAT_ORG_COMPRAS_ID = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_ORG_COMPRAS.First().ID,
                    CONTRATANTE_PJPF_ID = contratanteFornecedor.ID,
                    NOME = item.NOME,
                    EMAIL = item.EMAIL,
                    CELULAR = item.CELULAR,
                    TELEFONE = item.TELEFONE
                });
            }

            #endregion

            #region Documentos
            if (solicitacaoCadastroFornecedor.PJPF_TIPO != 2)
            {
                foreach (SOLICITACAO_DOCUMENTOS item in solicitacao.WFD_SOL_DOCUMENTOS)
                {
                    if (item.ARQUIVO_ID != null)
                    {
                        contratanteFornecedor.WFD_PJPF_DOCUMENTOS.Add(new FORNECEDOR_DOCUMENTOS
                        {
                            CONTRATANTE_PJPF_ID = contratanteFornecedor.ID,
                            ARQUIVO_ID = item.ARQUIVO_ID,
                            DATA_VENCIMENTO = item.DATA_VENCIMENTO,
                            DESCRICAO_DOCUMENTO_ID = item.DESCRICAO_DOCUMENTO_ID,
                            LISTA_DOCUMENTO_ID = item.LISTA_DOCUMENTO_ID,
                            OBRIGATORIO = item.OBRIGATORIO,
                            EXIGE_VALIDADE = item.EXIGE_VALIDADE,
                            PERIODICIDADE_ID = item.PERIODICIDADE_ID,
                            SOLICITACAO_ID = solicitacao.ID,
                            PJPF_ID = fornecedor.ID
                        });
                    }
                }
            }
            #endregion

            #region Informações Complementares
            foreach (var item in solicitacao.WFD_INFORM_COMPL)
            {
                contratanteFornecedor.WFD_PJPF_INFORM_COMPL.Add(new FORNECEDOR_INFORM_COMPL
                {
                    PERG_ID = item.PERG_ID,
                    RESPOSTA = item.RESPOSTA,
                    CONTRATANTE_PJPF_ID = contratanteFornecedor.ID
                });
            }
            #endregion


            CONTRATANTE contratanteAncora = CriarContratante(documento, solicitacaoCadastroFornecedor);

            fornecedor.WFD_CONTRATANTE_PJPF.Add(contratanteFornecedor);
            contratanteAncora.WFD_CONTRATANTE_PJPF.Add(contratanteFornecedor);

            _fornecedorRepository.Inserir(fornecedor);
            _contratanteRepository.Inserir(contratanteAncora);
            _unitOfWork.Finalizar();
            CriacaoUsuarioPadrao(documento, solicitacao.WFD_SOL_MOD_CONTATO.FirstOrDefault().EMAIL);
        }

        private CONTRATANTE CriarContratante(string documento, SolicitacaoCadastroFornecedor solicitacaoCadastroFornecedor)
        {
            CONTRATANTE contratanteAncora = new CONTRATANTE()
            {
                TIPO_CADASTRO_ID = (int)EnumTiposFornecedor.EmpresaNacional,
                TIPO_CONTRATANTE_ID = (int)EnumTipoContratante.FornecedorIndividual,
                DATA_CADASTRO = DateTime.Now,
                DATA_NASCIMENTO = solicitacaoCadastroFornecedor.DT_NASCIMENTO,
                NOME_FANTASIA = solicitacaoCadastroFornecedor.NOME_FANTASIA,
                RAZAO_SOCIAL = solicitacaoCadastroFornecedor.RAZAO_SOCIAL,
                CNPJ = documento,
                ATIVO = true,
                ATIVO_DT = DateTime.Now,
                ESTILO = "Azul"
            };

            var papel = _papelRepository.BuscarPorContratanteIdETipoPapelId(1, 50);
            contratanteAncora.WFL_PAPEL.Add(new WFL_PAPEL
            {
                PAPEL_NM = papel.PAPEL_NM,
                PAPEL_SGL = papel.PAPEL_SGL,
                PAPEL_TP_ID = papel.PAPEL_TP_ID
            });

            contratanteAncora.WAC_PERFIL.Add(new Domain.Models.Perfil
            {
                PERFIL_DSC = "Administrador do Sistema",
                PERFIL_NM = "Administrador"
            });
            contratanteAncora.WAC_PERFIL.Add(new Domain.Models.Perfil
            {
                PERFIL_DSC = "Usuário do Sistema",
                PERFIL_NM = "Usuário"
            });
            return contratanteAncora;
        }

        private void CriacaoUsuarioPadrao(string documento, string email)
        {
            string chave = Path.GetRandomFileName().Replace(".", "");
            Domain.Models.USUARIO usuario = new Domain.Models.USUARIO()
            {
                LOGIN = documento,
                ATIVO = true,
                TROCAR_SENHA = chave,
                DT_CRIACAO = DateTime.Now,
                PRIMEIRO_ACESSO = true,
                CONTA_TENTATIVA = 0,
                DT_ATIVACAO = null,
                PRINCIPAL = true,
                EMAIL = email,
                CPF_CNPJ = documento,
                SENHA = PasswordHash.CreateHash(documento)
            };
            USUARIO_SENHAS historico = new USUARIO_SENHAS()
            {
                SENHA = PasswordHash.CreateHash(documento),
                SENHA_DT = DateTime.Now,
                USUARIO_ID = usuario.ID
            };

            _usuarioRepository.IncluirNovoUsuarioPadrao(usuario, historico, null, null);
        }
    }
}
