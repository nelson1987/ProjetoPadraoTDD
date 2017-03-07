using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebForDocs.Business.Infrastructure.Enum;
using WebForDocs.Business.Process;
using WebForDocs.Data.ModeloDB;
using WebForDocs.Dominio.Models;
using WebForDocs.Interfaces;

namespace WebForDocs.Biblioteca
{
    public static class Aprovacao
    {
        private static IGeral _metodosGerais = new Geral();

        private static SolicitacaoBP solicitacaoBP = new SolicitacaoBP();
        public static void FinalizaSolicitacao(WFLModel Db, int FluxoTpId, int SolicitacaoId)
        {
            switch ((EnumTiposFluxo)FluxoTpId)
            {
                case EnumTiposFluxo.CadastroFornecedorNacional:
                case EnumTiposFluxo.CadastroFornecedorEstrangeiro:
                case EnumTiposFluxo.CadastroFornecedorNacionalDireto:
                case EnumTiposFluxo.CadastroFornecedorPFDireto:
                case EnumTiposFluxo.CadastroFornecedorPF:
                    FinalizaCriacaoFornecedor(Db, SolicitacaoId);
                    break;

                case EnumTiposFluxo.AmpliacaoFornecedor:
                    FinalizaExpansao(Db, SolicitacaoId);
                    break;

                case EnumTiposFluxo.ModificacaoDadosBancarios:
                    FinalizaModificacaoDadosBancarios(Db, SolicitacaoId);
                    break;

                case EnumTiposFluxo.ModificacaoEndereco:
                    FinalizaModificacaoDadosEnderecos(Db, SolicitacaoId);
                    break;

                case EnumTiposFluxo.ModificacaoDadosContato:
                    FinalizaModificacaoDadosContatos(Db, SolicitacaoId);
                    break;

                case EnumTiposFluxo.BloqueioFornecedor:
                    FinalizaBloqueio(Db, SolicitacaoId);
                    break;

                case EnumTiposFluxo.DesbloqueioFornecedor:
                    FinalizaDesbloqueio(Db, SolicitacaoId);
                    break;

                default:
                    break;
            }
        }

        private static void FinalizaCriacaoFornecedor(WFLModel Db, int SolicitacaoId)
        {
            WFD_SOLICITACAO solicitacao = solicitacaoBP.BuscarSolicitacaoFinalizaCriacaoFornecedor(SolicitacaoId);

            WFD_SOL_CAD_PJPF solicitacaoCadastroFornecedor = solicitacao.WFD_SOL_CAD_PJPF
                .FirstOrDefault(x => x.SOLICITACAO_ID == solicitacao.ID);

            WFD_PJPF_ROBO roboFornecedor = Db.WFD_PJPF_ROBO.SingleOrDefault(x => x.SOLICITACAO_ID == solicitacao.ID);

            WFD_PJPF fornecedor = PopularFornecedor(solicitacao, solicitacaoCadastroFornecedor, roboFornecedor);
            Db.Entry(fornecedor).State = EntityState.Added;

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
            Db.Entry(contratanteFornecedor).State = EntityState.Added;

            #region Bancos
            foreach (var solicitacaoModificacaoBanco in solicitacao.WFD_SOL_MOD_BANCO)
            {
                WFD_PJPF_BANCO fornecedorBanco = PopularBancoFornecedor(contratanteFornecedor.ID, solicitacaoModificacaoBanco);
                Db.Entry(fornecedorBanco).State = EntityState.Added;
            }
            #endregion

            #region Endereços
            foreach (var solicitacaoModificacaoEndereco in solicitacao.WFD_SOL_MOD_ENDERECO)
            {
                WFD_PJPF_ENDERECO fornecedorEndereco = new WFD_PJPF_ENDERECO
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
                Db.Entry(fornecedorEndereco).State = EntityState.Added;
            }
            #endregion

            #region Contatos

            foreach (var item in solicitacao.WFD_SOL_MOD_CONTATO)
            {
                WFD_PJPF_CONTATOS contato = new WFD_PJPF_CONTATOS
                {
                    CONTRAT_ORG_COMPRAS_ID = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_ORG_COMPRAS.First().ID,
                    CONTRATANTE_PJPF_ID = contratanteFornecedor.ID,
                    NOME = item.NOME,
                    EMAIL = item.EMAIL,
                    CELULAR = item.CELULAR,
                    TELEFONE = Mascara.RemoverMascaraTelefone(item.TELEFONE)
                };
                Db.Entry(contato).State = EntityState.Added;
            }

            #endregion

            #region Documentos
            if (solicitacaoCadastroFornecedor.PJPF_TIPO != 2)
            {
                foreach (WFD_SOL_DOCUMENTOS item in solicitacao.WFD_SOL_DOCUMENTOS)
                {
                    if (item.ARQUIVO_ID != null)
                    {
                        WFD_PJPF_DOCUMENTOS documento = new WFD_PJPF_DOCUMENTOS
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
                        Db.Entry(documento).State = EntityState.Added;
                    }
                }
            }
            #endregion

            #region Unspsc
            foreach (var item in solicitacao.WFD_SOL_UNSPSC)
            {
                WFD_PJPF_UNSPSC unspsc = new WFD_PJPF_UNSPSC
                {
                    SOLICITACAO_ID = solicitacao.ID,
                    UNSPSC_ID = item.UNSPSC_ID,
                    DT_INCLUSAO = DateTime.Now,
                    WFD_PJPF = fornecedor
                };
                Db.Entry(unspsc).State = EntityState.Added;
            }
            #endregion

            #region Informações Complementares
            foreach (var item in solicitacao.WFD_INFORM_COMPL)
            {
                WFD_PJPF_INFORM_COMPL infoCompl = new WFD_PJPF_INFORM_COMPL
                {
                    PERG_ID = item.PERG_ID,
                    RESPOSTA = item.RESPOSTA,
                    CONTRATANTE_PJPF_ID = contratanteFornecedor.ID
                };
                Db.Entry(infoCompl).State = EntityState.Added;
            }
            #endregion

            Db.SaveChanges();
        }

        private static WFD_PJPF PopularFornecedor(WFD_SOLICITACAO solicitacao, WFD_SOL_CAD_PJPF solicitacaoCadastroFornecedor, WFD_PJPF_ROBO roboFornecedor)
        {
            WFD_PJPF fornecedor = new WFD_PJPF
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
                ATIVO = true
            };
            if (roboFornecedor != null && solicitacaoCadastroFornecedor.PJPF_TIPO != 2)
                fornecedor.ROBO_ID = roboFornecedor.ID;
            return fornecedor;
        }

        private static WFD_PJPF_BANCO PopularBancoFornecedor(int contratanteFornId, WFD_SOL_MOD_BANCO item)
        {
            WFD_PJPF_BANCO banco = new WFD_PJPF_BANCO
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

        private static void FinalizaExpansao(WFLModel Db, int SolicitacaoId)
        {
            WFD_SOLICITACAO solicitacao = Db.WFD_SOLICITACAO
                .Include("WFD_SOL_CAD_PJPF")
                .Include("WFD_CONTRATANTE.WFD_CONTRATANTE_ORG_COMPRAS")
                .SingleOrDefault(s => s.ID == SolicitacaoId);

            WFD_SOL_CAD_PJPF solforn = solicitacao.WFD_SOL_CAD_PJPF.First();

            WFD_SOL_MOD_CONTATO solfornContato = solicitacao.WFD_SOL_MOD_CONTATO
                .FirstOrDefault(x => x.SOLICITACAO_ID == solicitacao.ID);

            #region Bancos
            foreach (WFD_SOL_MOD_BANCO item in solicitacao.WFD_SOL_MOD_BANCO)
            {

                WFD_PJPF_BANCO banco = PopularBancoFornecedor(item.ID, item);
                if (item.BANCO_PJPF_ID != null)
                {
                    banco.ID = (int)item.BANCO_PJPF_ID;
                    Db.Entry(banco).State = EntityState.Modified;
                }
                else
                {
                    Db.Entry(banco).State = EntityState.Added;
                }
            }
            #endregion

            #region Contatos
            WFD_PJPF_CONTATOS contato = new WFD_PJPF_CONTATOS();
            contato.CONTRAT_ORG_COMPRAS_ID = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_ORG_COMPRAS.First().ID;
            contato.NOME = solfornContato.NOME;
            contato.EMAIL = solfornContato.EMAIL;
            contato.CELULAR = solfornContato.CELULAR;
            contato.TELEFONE = Mascara.RemoverMascaraTelefone(solfornContato.TELEFONE);
            Db.Entry(contato).State = EntityState.Added;
            #endregion

            #region Contrantante
            WFD_CONTRATANTE_PJPF contratanteForn = new WFD_CONTRATANTE_PJPF();
            contratanteForn.CATEGORIA_ID = (int)solforn.CATEGORIA_ID;
            contratanteForn.CONTRATANTE_ID = solicitacao.CONTRATANTE_ID;
            contratanteForn.PJPF_ID = (int)solicitacao.PJPF_ID;
            contratanteForn.PJPF_COD_ERP = solforn.COD_PJPF_ERP;
            contratanteForn.PJPF_STATUS_ID = 1;
            contratanteForn.TP_PJPF = 2;
            Db.Entry(contratanteForn).State = System.Data.Entity.EntityState.Added;
            #endregion

            Db.SaveChanges();
        }

        private static void FinalizaModificacaoDadosBancarios(WFLModel Db, int SolicitacaoId)
        {
            WFD_SOLICITACAO solicitacao = Db.WFD_SOLICITACAO
                .Include("WFD_SOL_MOD_BANCO")
                .Include("WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF.WFD_PJPF_BANCO")
                .SingleOrDefault(s => s.ID == SolicitacaoId);

            List<WFD_SOL_MOD_BANCO> solBancos = solicitacao.WFD_SOL_MOD_BANCO.ToList();

            List<WFD_PJPF_BANCO> Bancos = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF.SingleOrDefault(x => x.PJPF_ID == solicitacao.PJPF_ID).WFD_PJPF_BANCO.ToList();

            //Remove os Bancos que foram excluidos na tela
            foreach (WFD_PJPF_BANCO item in Bancos)
            {
                if (solBancos.All(s => s.BANCO_PJPF_ID != item.ID))
                {
                    Db.WFD_PJPF_BANCO.Remove(item);
                }
            }

            foreach (WFD_SOL_MOD_BANCO item in solBancos)
            {
                if (item.BANCO_PJPF_ID == null)
                {
                    WFD_PJPF_BANCO banco = PopularBancoFornecedor(solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF.SingleOrDefault(x => x.PJPF_ID == solicitacao.PJPF_ID).ID, item);
                    Db.WFD_PJPF_BANCO.Add(banco);
                }
                else
                {
                    WFD_PJPF_BANCO banco = Bancos.SingleOrDefault(b => b.ID == item.BANCO_PJPF_ID);
                    banco.CONTRATANTE_PJPF_ID = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF.SingleOrDefault(x => x.PJPF_ID == solicitacao.PJPF_ID).ID;
                    banco.BANCO_ID = (int)item.BANCO_ID;
                    banco.AGENCIA = item.AGENCIA;
                    banco.AG_DV = item.AG_DV;
                    banco.CONTA = item.CONTA;
                    banco.CONTA_DV = item.CONTA_DV;
                    banco.DATA_UPLOAD = item.DATA_UPLOAD;
                    banco.NOME_ARQUIVO = item.NOME_ARQUIVO;
                    banco.ARQUIVO_ID = item.ARQUIVO_ID;
                    Db.Entry(banco).State = EntityState.Modified;
                }
            }

            Db.SaveChanges();
        }

        private static void FinalizaModificacaoDadosEnderecos(WFLModel Db, int SolicitacaoId)
        {
            WFD_SOLICITACAO solicitacao = Db.WFD_SOLICITACAO
                .Include("WFD_SOL_MOD_ENDERECO")
                .Include("WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF.WFD_PJPF_ENDERECO")
                .SingleOrDefault(s => s.ID == SolicitacaoId);

            List<WFD_SOL_MOD_ENDERECO> solEnderecos = solicitacao.WFD_SOL_MOD_ENDERECO.ToList();

            List<WFD_PJPF_ENDERECO> Enderecos = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF
                .FirstOrDefault(x =>
                    x.PJPF_ID == solicitacao.PJPF_ID)
                        .WFD_PJPF_ENDERECO
                        .ToList();

            //Remove os Enderecos que foram excluidos na tela
            foreach (WFD_PJPF_ENDERECO item in Enderecos)
            {
                if (solEnderecos.All(s => s.PJPF_ID != item.ID))
                {
                    Db.WFD_PJPF_ENDERECO.Remove(item);
                }
            }

            foreach (WFD_SOL_MOD_ENDERECO item in solEnderecos)
            {
                if (item.PJPF_ENDERECO_ID == null)
                {
                    WFD_PJPF_ENDERECO endereco = new WFD_PJPF_ENDERECO();
                    endereco.BAIRRO = item.BAIRRO;
                    endereco.CEP = item.CEP;
                    endereco.CIDADE = item.CIDADE;
                    endereco.UF = item.UF;
                    endereco.COMPLEMENTO = item.COMPLEMENTO;
                    endereco.ENDERECO = item.ENDERECO;
                    endereco.NUMERO = item.NUMERO;
                    endereco.PAIS = item.PAIS;
                    endereco.CONTRATANTE_PJPF_ID = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF.SingleOrDefault(x =>
                        x.PJPF_ID == solicitacao.PJPF_ID).ID;
                    endereco.TP_ENDERECO_ID = item.TP_ENDERECO_ID;

                    Db.WFD_PJPF_ENDERECO.Add(endereco);
                }
                else
                {
                    WFD_PJPF_ENDERECO endereco = Enderecos.SingleOrDefault(x => x.ID == item.PJPF_ENDERECO_ID);
                    endereco.BAIRRO = item.BAIRRO;
                    endereco.CEP = item.CEP;
                    endereco.CIDADE = item.CIDADE;
                    endereco.UF = item.UF;
                    endereco.COMPLEMENTO = item.COMPLEMENTO;
                    endereco.ENDERECO = item.ENDERECO;
                    endereco.NUMERO = item.NUMERO;
                    endereco.PAIS = item.PAIS;

                    Db.Entry(endereco).State = EntityState.Modified;
                }
            }

            Db.SaveChanges();
        }

        private static void FinalizaModificacaoDadosContatos(WFLModel Db, int SolicitacaoId)
        {
            WFD_SOLICITACAO solicitacao = Db.WFD_SOLICITACAO
                .Include("WFD_CONTRATANTE.WFD_CONTRATANTE_ORG_COMPRAS")
                .Include("WFD_SOL_MOD_CONTATO")
                .Include("WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF.WFD_PJPF_CONTATOS")
                .SingleOrDefault(s => s.ID == SolicitacaoId);
            List<WFD_SOL_MOD_CONTATO> solContatos = solicitacao.WFD_SOL_MOD_CONTATO.ToList();
            List<WFD_PJPF_CONTATOS> contatos = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF.SingleOrDefault(x => x.PJPF_ID == solicitacao.PJPF_ID).WFD_PJPF_CONTATOS.ToList();
            var contratanteForn = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_PJPF.SingleOrDefault(x => x.PJPF_ID == solicitacao.PJPF_ID);

            //Remove os Bancos que foram excluidos na tela
            foreach (var item in contatos)
            {
                if (solContatos.All(s => s.CONTATO_PJPF_ID != item.ID))
                {
                    Db.WFD_PJPF_CONTATOS.Remove(item);
                }
            }

            foreach (WFD_SOL_MOD_CONTATO item in solContatos)
            {
                if (item.CONTATO_PJPF_ID == null || item.CONTATO_PJPF_ID == 0)
                {
                    WFD_PJPF_CONTATOS contato = new WFD_PJPF_CONTATOS();
                    contato.CONTRATANTE_PJPF_ID = contratanteForn.ID;
                    contato.CONTRAT_ORG_COMPRAS_ID = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_ORG_COMPRAS.First().ID;
                    contato.NOME = item.NOME;
                    contato.EMAIL = item.EMAIL;
                    contato.TELEFONE = Mascara.RemoverMascaraTelefone(item.TELEFONE);
                    contato.CELULAR = item.CELULAR;
                    Db.WFD_PJPF_CONTATOS.Add(contato);
                }
                else
                {
                    WFD_PJPF_CONTATOS contato = contatos.SingleOrDefault(b => b.ID == item.CONTATO_PJPF_ID);
                    contato.CONTRATANTE_PJPF_ID = contratanteForn.ID;
                    contato.CONTRAT_ORG_COMPRAS_ID = solicitacao.WFD_CONTRATANTE.WFD_CONTRATANTE_ORG_COMPRAS.First().ID;
                    contato.NOME = item.NOME;
                    contato.EMAIL = item.EMAIL;
                    contato.TELEFONE = Mascara.RemoverMascaraTelefone(item.TELEFONE);
                    contato.CELULAR = item.CELULAR;

                    Db.Entry(contato).State = EntityState.Modified;
                }
            }

            Db.SaveChanges();
        }

        private static void FinalizaBloqueio(WFLModel Db, int SolicitacaoId)
        {
            WFD_SOLICITACAO solicitacao = Db.WFD_SOLICITACAO
                .Include("WFD_SOL_BLOQ")
                .Include("WFL_FLUXO")
                .SingleOrDefault(s => s.ID == SolicitacaoId);
            WFD_SOL_BLOQ bloq = solicitacao.WFD_SOL_BLOQ.First();

            if ((bool)bloq.BLQ_LANCAMENTO_EMP)
            {
                WFD_CONTRATANTE_PJPF contrForn = Db.WFD_CONTRATANTE_PJPF
                    .SingleOrDefault(f => f.PJPF_ID == solicitacao.PJPF_ID && f.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID);

                contrForn.PJPF_STATUS_ID = 2;
                contrForn.PJPF_STATUS_DT = DateTime.Now;
                contrForn.PJPF_STATUS_TP_SOL = solicitacao.WFL_FLUXO.FLUXO_TP_ID;
                contrForn.PJPF_STATUS_ID_SOL = solicitacao.ID;
                Db.Entry(contrForn).State = EntityState.Modified;

                Db.SaveChanges();
            }
            else if ((bool)bloq.BLQ_LANCAMENTO_TODAS_EMP)
            {
                int? grupoId = (int?)_metodosGerais.PegaAuthTicket("Grupo");

                List<WFD_CONTRATANTE_PJPF> contrForn = Db.WFD_CONTRATANTE_PJPF
                    .Where(f => f.PJPF_ID == solicitacao.PJPF_ID && f.WFD_CONTRATANTE.WFD_GRUPO.Any(g => g.ID == grupoId))
                    .ToList();

                foreach (var item in contrForn)
                {
                    item.PJPF_STATUS_ID = 1;
                    item.PJPF_STATUS_DT = DateTime.Now;
                    item.PJPF_STATUS_TP_SOL = solicitacao.WFL_FLUXO.FLUXO_TP_ID;
                    item.PJPF_STATUS_ID_SOL = solicitacao.ID;
                    Db.Entry(item).State = EntityState.Modified;
                }

                Db.SaveChanges();
            }
        }

        private static void FinalizaDesbloqueio(WFLModel Db, int SolicitacaoId)
        {
            WFD_SOLICITACAO solicitacao = Db.WFD_SOLICITACAO
                .Include("WFD_SOL_DESBLOQ")
                .Include("WFL_FLUXO")
                .SingleOrDefault(s => s.ID == SolicitacaoId);
            WFD_SOL_DESBLOQ bloq = solicitacao.WFD_SOL_DESBLOQ.First();

            if ((bool)bloq.BLQ_LANCAMENTO_EMP)
            {
                WFD_CONTRATANTE_PJPF contrForn = Db.WFD_CONTRATANTE_PJPF.SingleOrDefault(f => f.PJPF_ID == solicitacao.PJPF_ID && f.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID);

                contrForn.PJPF_STATUS_ID = 1;
                contrForn.PJPF_STATUS_DT = DateTime.Now;
                contrForn.PJPF_STATUS_TP_SOL = solicitacao.WFL_FLUXO.FLUXO_TP_ID;
                contrForn.PJPF_STATUS_ID_SOL = solicitacao.ID;
                Db.Entry(contrForn).State = EntityState.Modified;

                Db.SaveChanges();
            }
            else if ((bool)bloq.BLQ_LANCAMENTO_TODAS_EMP)
            {
                int? grupoId = (int?)_metodosGerais.PegaAuthTicket("Grupo");

                List<WFD_CONTRATANTE_PJPF> contrForn = Db.WFD_CONTRATANTE_PJPF
                    .Where(f => f.PJPF_ID == solicitacao.PJPF_ID &&
                        f.WFD_CONTRATANTE.WFD_GRUPO.Any(g => g.ID == grupoId))
                    .ToList();

                foreach (var item in contrForn)
                {
                    item.PJPF_STATUS_ID = 1;
                    item.PJPF_STATUS_DT = DateTime.Now;
                    item.PJPF_STATUS_TP_SOL = solicitacao.WFL_FLUXO.FLUXO_TP_ID;
                    item.PJPF_STATUS_ID_SOL = solicitacao.ID;
                    Db.Entry(item).State = EntityState.Modified;
                }

                Db.SaveChanges();
            }
        }
    }
}