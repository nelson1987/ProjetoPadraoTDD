using System;
using System.Linq;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public class AprovacaoWebForLinkAppService : AppService<WebForLinkContexto>, IAprovacaoWebForLinkAppService
    {
        private readonly IFornecedorBancoWebForLinkService _bancoFornecedorService;
        private readonly IFornecedorContatoWebForLinkService _contatoFornecedorService;
        private readonly IContratantePjpfWebForLinkService _contratanteFornecedor;
        private readonly IFornecedorDocumentoWebForLinkService _documentoFornecedorService;
        private readonly IFornecedorEnderecoWebForLinkService _enderecoFornecedorService;
        private readonly IFornecedorWebForLinkService _fornecedorService;

        private readonly IFornecedorInformacaoComplementarComplWebForLinkService
            _informacaoComplementarFornecedorService;

        private readonly IRoboWebForLinkService _roboService;
        private readonly ISolicitacaoWebForLinkService _solicitacaoService;
        private readonly IFornecedorUnspscWebForLinkService _unspscFornecedorService;

        public AprovacaoWebForLinkAppService(
            ISolicitacaoWebForLinkService solicitacao,
            IRoboWebForLinkService robo,
            IContratantePjpfWebForLinkService contratanteFornecedor,
            IFornecedorWebForLinkService fornecedor,
            IFornecedorBancoWebForLinkService bancoFornecedor,
            IFornecedorEnderecoWebForLinkService enderecoFornecedor,
            IFornecedorContatoWebForLinkService contatoFornecedor,
            IFornecedorDocumentoWebForLinkService documentoFornecedor,
            IFornecedorUnspscWebForLinkService unspscFornecedor,
            IFornecedorInformacaoComplementarComplWebForLinkService informacaoComplementarFornecedor
            )
        {
            try
            {
                _solicitacaoService = solicitacao;
                _roboService = robo;
                _contratanteFornecedor = contratanteFornecedor;
                _fornecedorService = fornecedor;
                _bancoFornecedorService = bancoFornecedor;
                _enderecoFornecedorService = enderecoFornecedor;
                _contatoFornecedorService = contatoFornecedor;
                _documentoFornecedorService = documentoFornecedor;
                _unspscFornecedorService = unspscFornecedor;
                _informacaoComplementarFornecedorService = informacaoComplementarFornecedor;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void FinalizarCriacaoFornecedor(int solicitacaoId)
        {
            var solicitacao = _solicitacaoService.BuscarSolicitacaoFinalizaCriacaoFornecedor(solicitacaoId);

            var solicitacaoCadastroFornecedor = solicitacao.SolicitacaoCadastroFornecedor
                .FirstOrDefault(x => x.SOLICITACAO_ID == solicitacao.ID);

            var roboFornecedor = _roboService.Get(x => x.SOLICITACAO_ID == solicitacao.ID);

            var fornecedor = PopularFornecedor(solicitacao, solicitacaoCadastroFornecedor, roboFornecedor);
            BeginTransaction();
            _fornecedorService.Add(fornecedor);

            var contratanteFornecedor = new WFD_CONTRATANTE_PJPF
            {
                CATEGORIA_ID = solicitacaoCadastroFornecedor.CATEGORIA_ID,
                CONTRATANTE_ID = solicitacao.CONTRATANTE_ID,
                PJPF_ID = fornecedor.ID,
                PJPF_COD_ERP = solicitacaoCadastroFornecedor.COD_PJPF_ERP,
                PJPF_STATUS_ID = 1,
                PJPF_STATUS_ID_SOL = solicitacao.ID,
                TP_PJPF = 2
            };
            _contratanteFornecedor.Add(contratanteFornecedor);

            #region Bancos

            foreach (var solicitacaoModificacaoBanco in solicitacao.SolicitacaoModificacaoDadosBancario)
            {
                var fornecedorBanco = PopularBancoFornecedor(contratanteFornecedor.ID, solicitacaoModificacaoBanco);
                _bancoFornecedorService.Add(fornecedorBanco);
                //Db.Entry(fornecedorBanco).State = EntityState.Added;
            }

            #endregion

            #region Endereços

            foreach (var solicitacaoModificacaoEndereco in solicitacao.WFD_SOL_MOD_ENDERECO)
            {
                var fornecedorEndereco = new FORNECEDOR_ENDERECO
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
                };
                _enderecoFornecedorService.Add(fornecedorEndereco);
                //Db.Entry(fornecedorEndereco).State = EntityState.Added;
            }

            #endregion

            #region Contatos

            foreach (var item in solicitacao.SolicitacaoModificacaoDadosContato)
            {
                var contato = new FORNECEDOR_CONTATOS
                {
                    CONTRAT_ORG_COMPRAS_ID = solicitacao.Contratante.WFD_CONTRATANTE_ORG_COMPRAS.First().ID,
                    CONTRATANTE_PJPF_ID = contratanteFornecedor.ID,
                    NOME = item.NOME,
                    EMAIL = item.EMAIL,
                    CELULAR = item.CELULAR,
                    TELEFONE = Mascara.RemoverMascaraTelefone(item.TELEFONE)
                };
                _contatoFornecedorService.Add(contato);
                //Db.Entry(contato).State = EntityState.Added;
            }

            #endregion

            #region Documentos

            if (solicitacaoCadastroFornecedor.PJPF_TIPO != 2)
            {
                foreach (var item in solicitacao.SolicitacaoDeDocumentos)
                {
                    if (item.ARQUIVO_ID != null)
                    {
                        var documento = new DocumentosDoFornecedor
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
                        };
                        _documentoFornecedorService.Add(documento);
                        //Db.Entry(documento).State = EntityState.Added;
                    }
                }
            }

            #endregion

            #region Unspsc

            foreach (var item in solicitacao.WFD_SOL_UNSPSC)
            {
                var unspsc = new FORNECEDOR_UNSPSC
                {
                    SOLICITACAO_ID = solicitacao.ID,
                    UNSPSC_ID = item.UNSPSC_ID,
                    DT_INCLUSAO = DateTime.Now,
                    WFD_PJPF = fornecedor
                };
                _unspscFornecedorService.Add(unspsc);
                //Db.Entry(unspsc).State = EntityState.Added;
            }

            #endregion

            #region Informações Complementares

            foreach (var item in solicitacao.WFD_INFORM_COMPL)
            {
                var infoCompl = new FORNECEDOR_INFORM_COMPL
                {
                    PERG_ID = item.PERG_ID,
                    RESPOSTA = item.RESPOSTA,
                    CONTRATANTE_PJPF_ID = contratanteFornecedor.ID
                };
                _informacaoComplementarFornecedorService.Add(infoCompl);
                //Db.Entry(infoCompl).State = EntityState.Added;
            }

            #endregion
            Commit();
            //Db.SaveChanges();
        }

        public void FinalizarExpansao(int solicitacaoId)
        {
            var solicitacao = _solicitacaoService.Get(solicitacaoId);

            var solforn = solicitacao.SolicitacaoCadastroFornecedor.First();

            var solfornContato = solicitacao.SolicitacaoModificacaoDadosContato
                .FirstOrDefault(x => x.SOLICITACAO_ID == solicitacao.ID);

            #region Bancos

            foreach (var item in solicitacao.SolicitacaoModificacaoDadosBancario)
            {
                var banco = PopularBancoFornecedor(item.ID, item);
                if (item.BANCO_PJPF_ID != null)
                {
                    banco.ID = (int) item.BANCO_PJPF_ID;
                    _bancoFornecedorService.Update(banco);
                    //Db.Entry(banco).State = EntityState.Modified;
                }
                else
                    _bancoFornecedorService.Add(banco);
                //Db.Entry(banco).State = EntityState.Added;
            }

            #endregion

            #region Contatos

            var contato = new FORNECEDOR_CONTATOS();
            contato.CONTRAT_ORG_COMPRAS_ID = solicitacao.Contratante.WFD_CONTRATANTE_ORG_COMPRAS.First().ID;
            contato.NOME = solfornContato.NOME;
            contato.EMAIL = solfornContato.EMAIL;
            contato.CELULAR = solfornContato.CELULAR;
            contato.TELEFONE = Mascara.RemoverMascaraTelefone(solfornContato.TELEFONE);

            _contatoFornecedorService.Add(contato);
            //Db.Entry(contato).State = EntityState.Added;

            #endregion

            #region Contrantante

            var contratanteFornecedor = new WFD_CONTRATANTE_PJPF
            {
                CATEGORIA_ID = solforn.CATEGORIA_ID,
                CONTRATANTE_ID = solicitacao.CONTRATANTE_ID,
                PJPF_ID = (int) solicitacao.PJPF_ID,
                PJPF_COD_ERP = solforn.COD_PJPF_ERP,
                PJPF_STATUS_ID = 1,
                TP_PJPF = 2
            };
            _contratanteFornecedor.Add(contratanteFornecedor);
            //Db.Entry(contratanteForn).State = System.Data.Entity.EntityState.Added;

            #endregion

            //Db.SaveChanges();
        }

        public void FinalizarModificacaoDadosBancarios(int solicitacaoId)
        {
            var solicitacao = _solicitacaoService.Get(solicitacaoId);

            var solBancos = solicitacao.SolicitacaoModificacaoDadosBancario.ToList();

            var bancos =
                solicitacao.Contratante.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.PJPF_ID == solicitacao.PJPF_ID)
                    .BancoDoFornecedor.ToList();

            //Remove os Bancos que foram excluidos na tela
            foreach (var item in bancos)
            {
                if (solBancos.All(s => s.BANCO_PJPF_ID != item.ID))
                {
                    //Db.BancoDoFornecedor.Remove(item);
                    _bancoFornecedorService.Delete(item);
                }
            }

            foreach (var item in solBancos)
            {
                if (item.BANCO_PJPF_ID == null)
                {
                    var banco =
                        PopularBancoFornecedor(
                            solicitacao.Contratante.WFD_CONTRATANTE_PJPF.FirstOrDefault(
                                x => x.PJPF_ID == solicitacao.PJPF_ID).ID, item);
                    //Db.BancoDoFornecedor.Add(banco);

                    _bancoFornecedorService.Add(banco);
                }
                else
                {
                    var banco = bancos.FirstOrDefault(b => b.ID == item.BANCO_PJPF_ID);
                    banco.CONTRATANTE_PJPF_ID =
                        solicitacao.Contratante.WFD_CONTRATANTE_PJPF.FirstOrDefault(
                            x => x.PJPF_ID == solicitacao.PJPF_ID).ID;
                    banco.BANCO_ID = item.BANCO_ID;
                    banco.AGENCIA = item.AGENCIA;
                    banco.AG_DV = item.AG_DV;
                    banco.CONTA = item.CONTA;
                    banco.CONTA_DV = item.CONTA_DV;
                    banco.DATA_UPLOAD = item.DATA_UPLOAD;
                    banco.NOME_ARQUIVO = item.NOME_ARQUIVO;
                    banco.ARQUIVO_ID = item.ARQUIVO_ID;
                    //Db.Entry(banco).State = EntityState.Modified;
                    _bancoFornecedorService.Update(banco);
                }
            }

            //Db.SaveChanges();
        }

        public void FinalizarModificacaoDadosEnderecos(int solicitacaoId)
        {
            var solicitacao = _solicitacaoService.Get(solicitacaoId);

            var solEnderecos = solicitacao.WFD_SOL_MOD_ENDERECO.ToList();

            var Enderecos = solicitacao.Contratante.WFD_CONTRATANTE_PJPF
                .FirstOrDefault(x =>
                    x.PJPF_ID == solicitacao.PJPF_ID)
                .WFD_PJPF_ENDERECO
                .ToList();

            //Remove os Enderecos que foram excluidos na tela
            foreach (var item in Enderecos)
            {
                if (solEnderecos.All(s => s.PJPF_ID != item.ID))
                {
                    //Db.WFD_PJPF_ENDERECO.Remove(item);
                    _enderecoFornecedorService.Delete(item);
                }
            }

            foreach (var item in solEnderecos)
            {
                if (item.PJPF_ENDERECO_ID == null)
                {
                    var endereco = new FORNECEDOR_ENDERECO();
                    endereco.BAIRRO = item.BAIRRO;
                    endereco.CEP = item.CEP;
                    endereco.CIDADE = item.CIDADE;
                    endereco.UF = item.UF;
                    endereco.COMPLEMENTO = item.COMPLEMENTO;
                    endereco.ENDERECO = item.ENDERECO;
                    endereco.NUMERO = item.NUMERO;
                    endereco.PAIS = item.PAIS;
                    endereco.CONTRATANTE_PJPF_ID = solicitacao.Contratante.WFD_CONTRATANTE_PJPF.FirstOrDefault(x =>
                        x.PJPF_ID == solicitacao.PJPF_ID).ID;
                    endereco.TP_ENDERECO_ID = item.TP_ENDERECO_ID;

                    _enderecoFornecedorService.Add(endereco);

                    //Db.WFD_PJPF_ENDERECO.Add(endereco);
                }
                else
                {
                    var endereco = Enderecos.FirstOrDefault(x => x.ID == item.PJPF_ENDERECO_ID);
                    endereco.BAIRRO = item.BAIRRO;
                    endereco.CEP = item.CEP;
                    endereco.CIDADE = item.CIDADE;
                    endereco.UF = item.UF;
                    endereco.COMPLEMENTO = item.COMPLEMENTO;
                    endereco.ENDERECO = item.ENDERECO;
                    endereco.NUMERO = item.NUMERO;
                    endereco.PAIS = item.PAIS;

                    _enderecoFornecedorService.Update(endereco);

                    //Db.Entry(endereco).State = EntityState.Modified;
                }
            }

            //Db.SaveChanges();
        }

        public void FinalizarModificacaoDadosContatos(int solicitacaoId)
        {
            var solicitacao = _solicitacaoService.Get(solicitacaoId);
            var solContatos = solicitacao.SolicitacaoModificacaoDadosContato.ToList();
            var contatos =
                solicitacao.Contratante.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.PJPF_ID == solicitacao.PJPF_ID)
                    .WFD_PJPF_CONTATOS.ToList();
            var contratanteForn =
                solicitacao.Contratante.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.PJPF_ID == solicitacao.PJPF_ID);

            //Remove os Bancos que foram excluidos na tela
            foreach (var item in contatos)
            {
                if (solContatos.All(s => s.CONTATO_PJPF_ID != item.ID))
                {
                    //Db.WFD_PJPF_CONTATOS.Remove(item);
                    _contatoFornecedorService.Delete(item);
                }
            }

            foreach (var item in solContatos)
            {
                if (item.CONTATO_PJPF_ID == null || item.CONTATO_PJPF_ID == 0)
                {
                    var contato = new FORNECEDOR_CONTATOS();
                    contato.CONTRATANTE_PJPF_ID = contratanteForn.ID;
                    contato.CONTRAT_ORG_COMPRAS_ID = solicitacao.Contratante.WFD_CONTRATANTE_ORG_COMPRAS.First().ID;
                    contato.NOME = item.NOME;
                    contato.EMAIL = item.EMAIL;
                    contato.TELEFONE = Mascara.RemoverMascaraTelefone(item.TELEFONE);
                    contato.CELULAR = item.CELULAR;
                    _contatoFornecedorService.Add(contato);
                    //Db.WFD_PJPF_CONTATOS.Add(contato);
                }
                else
                {
                    var contato = contatos.FirstOrDefault(b => b.ID == item.CONTATO_PJPF_ID);
                    contato.CONTRATANTE_PJPF_ID = contratanteForn.ID;
                    contato.CONTRAT_ORG_COMPRAS_ID = solicitacao.Contratante.WFD_CONTRATANTE_ORG_COMPRAS.First().ID;
                    contato.NOME = item.NOME;
                    contato.EMAIL = item.EMAIL;
                    contato.TELEFONE = Mascara.RemoverMascaraTelefone(item.TELEFONE);
                    contato.CELULAR = item.CELULAR;
                    _contatoFornecedorService.Update(contato);
                    //Db.Entry(contato).State = EntityState.Modified;
                }
            }

            //Db.SaveChanges();
        }

        public void FinalizarBloqueio(int solicitacaoId, int? grupoId)
        {
            BloquearOuDesbloquear(solicitacaoId, grupoId, 2); //Solicitação Bloqueada
        }

        public void FinalizarDesbloqueio(int solicitacaoId, int? grupoId)
        {
            BloquearOuDesbloquear(solicitacaoId, grupoId, 1); //Solicitação Desbloqueada
        }

        public void FinalizarSolicitacao(int? grupoId, int tipoFluxoId, int solicitacaoId)
        {
            switch ((EnumTiposFluxo) tipoFluxoId)
            {
                case EnumTiposFluxo.CadastroFornecedorNacional:
                case EnumTiposFluxo.CadastroFornecedorEstrangeiro:
                case EnumTiposFluxo.CadastroFornecedorNacionalDireto:
                case EnumTiposFluxo.CadastroFornecedorPFDireto:
                case EnumTiposFluxo.CadastroFornecedorPF:
                    FinalizarCriacaoFornecedor(solicitacaoId);
                    break;

                case EnumTiposFluxo.AmpliacaoFornecedor:
                    FinalizarExpansao(solicitacaoId);
                    break;

                case EnumTiposFluxo.ModificacaoDadosBancarios:
                    FinalizarModificacaoDadosBancarios(solicitacaoId);
                    break;

                case EnumTiposFluxo.ModificacaoEndereco:
                    FinalizarModificacaoDadosEnderecos(solicitacaoId);
                    break;

                case EnumTiposFluxo.ModificacaoDadosContato:
                    FinalizarModificacaoDadosContatos(solicitacaoId);
                    break;

                case EnumTiposFluxo.BloqueioFornecedor:
                    FinalizarBloqueio(solicitacaoId, grupoId);
                    break;

                case EnumTiposFluxo.DesbloqueioFornecedor:
                    FinalizarDesbloqueio(solicitacaoId, grupoId);
                    break;

                default:
                    break;
            }
        }

        public void Dispose()
        {
        }

        private static Fornecedor PopularFornecedor(SOLICITACAO solicitacao,
            SolicitacaoCadastroFornecedor solicitacaoCadastroFornecedor, ROBO roboFornecedor)
        {
            var fornecedor = new Fornecedor
            {
                CONTRATANTE_ID = solicitacao.CONTRATANTE_ID,
                TIPO_PJPF_ID = solicitacaoCadastroFornecedor.PJPF_TIPO,
                RAZAO_SOCIAL = solicitacaoCadastroFornecedor.RAZAO_SOCIAL,
                NOME_FANTASIA = solicitacaoCadastroFornecedor.NOME_FANTASIA,
                NOME = solicitacaoCadastroFornecedor.NOME,
                CNPJ = solicitacaoCadastroFornecedor.CNPJ,
                CPF = solicitacaoCadastroFornecedor.CPF,
                //CNAE = solicitacaoCadastroFornecedor.CNAE,
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
                ATIVO = true
            };
            if (roboFornecedor != null && solicitacaoCadastroFornecedor.PJPF_TIPO != 2)
                fornecedor.ROBO_ID = roboFornecedor.ID;
            return fornecedor;
        }

        private static BancoDoFornecedor PopularBancoFornecedor(int contratanteFornId,
            SolicitacaoModificacaoDadosBancario item)
        {
            var banco = new BancoDoFornecedor
            {
                CONTRATANTE_PJPF_ID = contratanteFornId,
                BANCO_ID = item.BANCO_ID,
                AGENCIA = item.AGENCIA,
                AG_DV = item.AG_DV,
                CONTA = item.CONTA,
                CONTA_DV = item.CONTA_DV,
                ATIVO = true,
                DATA_UPLOAD = item.DATA_UPLOAD,
                NOME_ARQUIVO = item.NOME_ARQUIVO,
                ARQUIVO_ID = item.ARQUIVO_ID
            };
            return banco;
        }

        private void BloquearOuDesbloquear(int solicitacaoId, int? grupoId, int status)
        {
            var solicitacao = _solicitacaoService.Get(solicitacaoId);
            var bloq = solicitacao.WFD_SOL_DESBLOQ.First();

            if ((bool) bloq.BLQ_LANCAMENTO_EMP)
            {
                var contrForn = _contratanteFornecedor.Get(f => f.PJPF_ID == solicitacao.PJPF_ID
                                                                && f.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID);

                contrForn.PJPF_STATUS_ID = status;
                contrForn.PJPF_STATUS_DT = DateTime.Now;
                contrForn.PJPF_STATUS_TP_SOL = solicitacao.Fluxo.FLUXO_TP_ID;
                contrForn.PJPF_STATUS_ID_SOL = solicitacao.ID;
                _contratanteFornecedor.Update(contrForn);

                //Db.Entry(contrForn).State = EntityState.Modified;
                //Db.SaveChanges();
            }
            else if ((bool) bloq.BLQ_LANCAMENTO_TODAS_EMP)
            {
                var contrForn = _contratanteFornecedor.Find(f =>
                    f.PJPF_ID == solicitacao.PJPF_ID &&
                    f.WFD_CONTRATANTE.WFD_GRUPO.Any(g => g.ID == grupoId))
                    .ToList();

                foreach (var item in contrForn)
                {
                    item.PJPF_STATUS_ID = status;
                    item.PJPF_STATUS_DT = DateTime.Now;
                    item.PJPF_STATUS_TP_SOL = solicitacao.Fluxo.FLUXO_TP_ID;
                    item.PJPF_STATUS_ID_SOL = solicitacao.ID;
                    _contratanteFornecedor.Update(item);
                    //Db.Entry(item).State = EntityState.Modified;
                }

                //Db.SaveChanges();
            }
        }
    }
}