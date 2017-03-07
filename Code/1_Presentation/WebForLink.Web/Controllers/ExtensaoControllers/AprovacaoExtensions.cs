using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers.Extensoes
{
    public static class AprovacaoExtensions
    {
        public static Fornecedor RetornaFornecedor(this AprovacaoController controller, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = controller._fornecedorService.Buscar(x => x.ID == solicitacao.PJPF_ID);
            if (fornecedor != null)
            {
                ROBO fornecedorRobo = controller._roboService.Buscar(x => x.ID == fornecedor.ROBO_ID);
                if (fornecedorRobo != null)
                    fornecedor.ROBO = fornecedorRobo;
            }

            return fornecedor;
        }

        public static void CadastroFornecedor(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            SolicitacaoCadastroFornecedor fornNacional = solicitacao.SolicitacaoCadastroFornecedor.First();
            ficha.ID = fornNacional.ID;
            ficha.SolicitacaoFornecedor.Solicitacao = true;
            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;

            Mapeamento.PopularDadosReceita(ficha, fornNacional);
            Mapeamento.PopularEndereco(ficha, fornNacional);

            ficha.DadosBancarios = controller.ListarSolicitacaoModificacaoBancario(solicitacao, false);
            ficha.DadosEnderecos = Mapper.Map<List<DadosEnderecosVM>>(solicitacao.WFD_SOL_MOD_ENDERECO.ToList());
            ficha.DadosContatos = controller.ListarSolicitacaoDadosContato(solicitacao, true);

            if (ficha.TipoFornecedor != 2)
                ficha.SolicitacaoFornecedor.Documentos =
                    Mapper.Map<List<SolicitacaoDeDocumentos>, List<SolicitacaoDocumentosVM>>(solicitacao.SolicitacaoDeDocumentos.Where(x => x.ARQUIVO_ID != null).ToList());
        }

        public static void AmpliacaoFornecedor(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            SolicitacaoCadastroFornecedor fornExpansao = solicitacao.SolicitacaoCadastroFornecedor.First();

            Fornecedor fornecedor = controller.Db.WFD_PJPF
                .Include("BancoDoFornecedor")
                .Include("WFD_PJPF_CONTATOS")
                .Include("DocumentosDeFornecedor.DescricaoDeDocumentos.TipoDeDocumento")
                .FirstOrDefault(c => c.ID == solicitacao.PJPF_ID);

            List<DocumentosDoFornecedor> documentos = fornecedor.DocumentosDoFornecedor.ToList();

            ficha.DadosBancarios = controller.ListarSolicitacaoModificacaoBancario(solicitacao, false);

            ficha.DadosContatos = controller.ListarSolicitacaoDadosContato(solicitacao, false);

            ficha.SolicitacaoFornecedor.Solicitacao = true;

            ficha.SolicitacaoFornecedor.Documentos =
                Mapper.Map<List<SolicitacaoDeDocumentos>, List<SolicitacaoDocumentosVM>>(solicitacao.SolicitacaoDeDocumentos.Where(x => x.ARQUIVO_ID != null).ToList());

            // Popula a view model FichaCadastralVM
            ficha.ID = fornecedor.ID;
            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.CNPJ_CPF = fornecedor.TIPO_PJPF_ID == 3 ? Convert.ToUInt64(fornecedor.CPF).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(fornecedor.CNPJ).ToString(@"00\.000\.000\/0000\-00");

            ficha.Observacao = fornExpansao.OBSERVACAO;
            ficha.CategoriaId = fornExpansao.CATEGORIA_ID;

            Mapeamento.PopularDadosReceita(ficha, fornecedor);
            Mapeamento.PopularEndereco(ficha, fornecedor);

            ficha.Solicitacao = new SolicitacaoVM
            {
                Fluxo = new FluxoVM
                {
                    ID = solicitacao.FLUXO_ID
                }
            };
        }

        public static void ModificacoesGerais(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            controller.Db.Entry(solicitacao).Reference(x => x.Fornecedor).Load();
            Fornecedor fornecedor = solicitacao.Fornecedor;
            controller.Db.Entry(solicitacao).Collection(x => x.WFD_SOL_MOD_DGERAIS_SEQ).Load();
            SOLICITACAO_MODIFICACAO_DADOSGERAIS gerais = solicitacao.WFD_SOL_MOD_DGERAIS_SEQ.First();

            // Popula a view model FichaCadastralVM
            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;

            Mapeamento.PopularDadosReceita(ficha, fornecedor);
            ficha.RazaoSocial = fornecedor.TIPO_PJPF_ID == 3 ? fornecedor.NOME : fornecedor.RAZAO_SOCIAL;

            ficha.OutrosDadosVisao = gerais.VISAO_ID;
            ficha.OutrosDadosGrupo = gerais.GRUPO_ID;
            ficha.OutrosDadosDescricao = gerais.DESCRICAO_ID;
            ficha.OutrosDadosDescricaoMudança = gerais.DESCRICAOALTERACAO;

            controller.ViewBag.OutrosDadosVisao = new SelectList(controller.Db.WFL_TP_VISAO.ToList(), "ID", "VISAO_NM", gerais.VISAO_ID);
            controller.ViewBag.OutrosDadosGrupo = new SelectList(controller.Db.WFD_T_GRUPO.Where(g => g.VISAO_ID == gerais.VISAO_ID).ToList(), "ID", "GRUPO_NM", gerais.GRUPO_ID);
            controller.ViewBag.OutrosDadosDescricao = new SelectList(controller.Db.WFD_T_DESCRICAO.Where(d => d.GRUPO_ID == gerais.GRUPO_ID).ToList(), "ID", "DESCRICAO_NM", gerais.DESCRICAO_ID);
        }

        public static void ModificacoesDadosFiscais(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            //Db.Entry(solicitacao).Collection(x => x.WFD_SOL_MOD_DFICAIS_SEQ).Load();

            //if (solicitacao.WFD_SOL_MOD_DFICAIS_SEQ != null && solicitacao.WFD_SOL_MOD_DFICAIS_SEQ.Count > 0)
            //{
            //    foreach (var item in solicitacao.WFD_SOL_MOD_DFICAIS_SEQ)
            //    {
            //        DadosFiscaisVM dados = new DadosFiscaisVM()
            //        {
            //            Categoria = item.WFD_T_CATEGORIA_IRF.CATEG_IRF_COD,
            //            Descricao = item.WFD_T_CATEGORIA_IRF.CATEG_IRF_DSC,
            //            ID = item.ID,
            //            Sujeito = item.SUJEITO_A
            //        };

            //        ficha.DadosFiscais.Add(dados);
            //    }
            //}
        }

        public static void ModificacoesDadosBancarios(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;

            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;

            Mapeamento.PopularDadosReceita(ficha, fornecedor);
            ficha.RazaoSocial = fornecedor.TIPO_PJPF_ID == 3 ? fornecedor.NOME : fornecedor.RAZAO_SOCIAL;

            // Dados Bancários 
            ficha.DadosBancarios = controller.ListarSolicitacaoModificacaoBancario(solicitacao, true);
        }

        public static void ModificacaoDadosEnderecos(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;
            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            ficha.TipoFornecedor = (int)fornecedor.TIPO_PJPF_ID;
            ficha.RazaoSocial = fornecedor.TIPO_PJPF_ID != 3 ? fornecedor.RAZAO_SOCIAL : fornecedor.NOME;
            Mapeamento.PopularDadosReceita(ficha, fornecedor);
            Mapeamento.PopularEndereco(ficha, fornecedor);

            // Dados Endereços 
            ficha.DadosEnderecos = Mapper.Map<List<DadosEnderecosVM>>(solicitacao.WFD_SOL_MOD_ENDERECO).ToList();
        }

        public static void ModificacoesDadosContatos(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;
            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            ficha.RazaoSocial = fornecedor.TIPO_PJPF_ID == 3 ? fornecedor.NOME : fornecedor.RAZAO_SOCIAL;
            Mapeamento.PopularDadosReceita(ficha, fornecedor);

            // Dados de Contatos
            ficha.DadosContatos = controller.ListarSolicitacaoDadosContato(solicitacao, true);
        }

        public static void BloqueioFornecedor(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            Fornecedor fornNacional8 = solicitacao.Fornecedor;
            if (solicitacao.SOLICITACAO_BLOQUEIO != null && solicitacao.SOLICITACAO_BLOQUEIO.Count > 0)
            {
                foreach (var item in solicitacao.SOLICITACAO_BLOQUEIO)
                {
                    string lanc = item.BLQ_LANCAMENTO_TODAS_EMP != null ? (bool)item.BLQ_LANCAMENTO_TODAS_EMP ? "1" : "2" : null;
                    int contratantePjPfId = fornNacional8.WFD_CONTRATANTE_PJPF != null ? fornNacional8.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).ID : 0;
                    ficha.DadosBloqueio = new DadosBloqueioVM
                    {
                        ID = item.ID,
                        Compra = (bool)item.BLQ_COMPRAS_TODAS_ORG_COMPRAS,
                        ContratanteFornecedorID = contratantePjPfId,
                        ContratanteID = solicitacao.CONTRATANTE_ID,
                        FornecedorID = fornNacional8.ID,
                        Lancamento = lanc,
                        Motivo = item.BLQ_QUALIDADE_FUNCAO_BQL_ID,
                        MotivoQualidade = item.TipoDeFuncaoDuranteBloqueio != null ? item.TipoDeFuncaoDuranteBloqueio.FUNCAO_BLOQ_DSC : string.Empty,
                        MotivoSolicitacao = item.BLQ_MOTIVO_DSC,
                        SolicitacaoID = solicitacao.ID
                    };
                }

                ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
                ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;

                if (fornNacional8 != null)
                {
                    ficha.CategoriaNome = fornNacional8
                    .WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID)
                    .WFD_PJPF_CATEGORIA.DESCRICAO;
                    ficha.CNPJ_CPF = fornNacional8.TIPO_PJPF_ID == 3 ? Convert.ToUInt64(fornNacional8.CPF).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(fornNacional8.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                    ficha.RazaoSocial = fornNacional8.TIPO_PJPF_ID == 3 ? fornNacional8.NOME : fornNacional8.RAZAO_SOCIAL;
                    ficha.NomeFantasia = fornNacional8.NOME_FANTASIA;
                    //ficha.CNAE = fornNacional8.CNAE;
                    ficha.InscricaoEstadual = fornNacional8.INSCR_ESTADUAL;
                    ficha.InscricaoMunicipal = fornNacional8.INSCR_MUNICIPAL;
                    ficha.TipoFornecedor = (int)fornNacional8.TIPO_PJPF_ID;
                }
                else
                {
                    FORNECEDORBASE fornNacional = solicitacao.FORNECEDORBASE;
                    ficha.CategoriaNome = fornNacional.WFD_PJPF_CATEGORIA.DESCRICAO;
                    ficha.CNPJ_CPF = fornNacional.PJPF_TIPO == 3
                        ? Convert.ToUInt64(fornNacional.CPF).ToString(@"000\.000\.000\-00")
                        : Convert.ToUInt64(fornNacional.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                    ficha.RazaoSocial = fornNacional.PJPF_TIPO == 3
                        ? fornNacional.NOME
                        : fornNacional.RAZAO_SOCIAL;
                    ficha.NomeFantasia = fornNacional.NOME_FANTASIA;
                    //ficha.CNAE = fornNacional.CNAE;
                    ficha.InscricaoEstadual = fornNacional.INSCR_ESTADUAL;
                    ficha.InscricaoMunicipal = fornNacional.INSCR_MUNICIPAL;
                    ficha.TipoFornecedor = fornNacional.PJPF_TIPO;
                }
            }
        }

        public static void DesbloqueioFornecedor(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            Fornecedor fornNacional8 = solicitacao.Fornecedor;
            if (solicitacao.WFD_SOL_DESBLOQ != null && solicitacao.WFD_SOL_DESBLOQ.Count > 0)
            {
                foreach (var item in solicitacao.WFD_SOL_DESBLOQ)
                {
                    string lanc = item.BLQ_LANCAMENTO_TODAS_EMP != null ? (bool)item.BLQ_LANCAMENTO_TODAS_EMP ? "1" : "2" : null;
                    int contratantePjPfId = fornNacional8.WFD_CONTRATANTE_PJPF != null ? fornNacional8.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).ID : 0;
                    ficha.DadosBloqueio = new DadosBloqueioVM
                    {
                        ID = item.ID,
                        Compra = (bool)item.BLQ_COMPRAS_TODAS_ORG_COMPRAS,
                        ContratanteFornecedorID = contratantePjPfId,
                        ContratanteID = solicitacao.CONTRATANTE_ID,
                        FornecedorID = fornNacional8.ID,
                        Lancamento = lanc,
                        Motivo = item.BLQ_QUALIDADE_FUNCAO_BQL_ID,
                        MotivoQualidade = item.WFD_T_FUNCAO_BLOQUEIO != null ? item.WFD_T_FUNCAO_BLOQUEIO.FUNCAO_BLOQ_DSC : string.Empty,
                        MotivoSolicitacao = item.BLQ_MOTIVO_DSC,
                        SolicitacaoID = solicitacao.ID
                    };
                }

                ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
                ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;

                if (fornNacional8 != null)
                {
                    ficha.CategoriaNome = fornNacional8
                    .WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID)
                    .WFD_PJPF_CATEGORIA.DESCRICAO;
                    ficha.CNPJ_CPF = fornNacional8.TIPO_PJPF_ID == 3 ? Convert.ToUInt64(fornNacional8.CPF).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(fornNacional8.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                    ficha.RazaoSocial = fornNacional8.TIPO_PJPF_ID == 3 ? fornNacional8.NOME : fornNacional8.RAZAO_SOCIAL;
                    ficha.NomeFantasia = fornNacional8.NOME_FANTASIA;
                    //ficha.CNAE = fornNacional8.CNAE;
                    ficha.InscricaoEstadual = fornNacional8.INSCR_ESTADUAL;
                    ficha.InscricaoMunicipal = fornNacional8.INSCR_MUNICIPAL;
                    ficha.TipoFornecedor = (int)fornNacional8.TIPO_PJPF_ID;
                }
            }

            //ViewBag.BloqueioMotivoQualidade = funcaoBloqueioBP.ListarTodosPorCodigoFuncaoBloqueio();
        }

        public static void AtualizacaoDocumentos(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;

            ficha.TipoFornecedor = (int)fornecedor.TIPO_PJPF_ID;
            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            ficha.CNPJ_CPF = fornecedor.TIPO_PJPF_ID == 3 ? Convert.ToUInt64(fornecedor.CPF).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(fornecedor.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            ficha.RazaoSocial = fornecedor.TIPO_PJPF_ID == 3 ? fornecedor.NOME : fornecedor.RAZAO_SOCIAL;
            ficha.NomeFantasia = fornecedor.NOME_FANTASIA;
            //ficha.CNAE = fornecedor.CNAE;
            ficha.InscricaoEstadual = fornecedor.INSCR_ESTADUAL;
            ficha.InscricaoMunicipal = fornecedor.INSCR_MUNICIPAL;

            ficha.SolicitacaoFornecedor.Solicitacao = true;
            ficha.SolicitacaoFornecedor.Documentos =
                Mapper.Map<List<SolicitacaoDeDocumentos>, List<SolicitacaoDocumentosVM>>(solicitacao.SolicitacaoDeDocumentos.Where(x => x.ARQUIVO_ID != null).ToList());
        }

        public static void ModificacoesInformacoesComplementares(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;

            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            ficha.CNPJ_CPF = fornecedor.TIPO_PJPF_ID == 3 ? Convert.ToUInt64(fornecedor.CPF).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(fornecedor.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            ficha.RazaoSocial = fornecedor.TIPO_PJPF_ID == 3 ? fornecedor.NOME : fornecedor.RAZAO_SOCIAL;
            ficha.NomeFantasia = fornecedor.NOME_FANTASIA;
            //ficha.CNAE = fornecedor.CNAE;
            ficha.InscricaoEstadual = fornecedor.INSCR_ESTADUAL;
            ficha.InscricaoMunicipal = fornecedor.INSCR_MUNICIPAL;
            ficha.CategoriaId = fornecedor.WFD_CONTRATANTE_PJPF.Single(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).CATEGORIA_ID ?? 0;
        }

        public static void AtualizacaoUnspsc(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;

            ficha.TipoFornecedor = (int)fornecedor.TIPO_PJPF_ID;
            ficha.ContratanteID = solicitacao.CONTRATANTE_ID;
            ficha.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            ficha.CNPJ_CPF = fornecedor.TIPO_PJPF_ID == 3 ? Convert.ToUInt64(fornecedor.CPF).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(fornecedor.CNPJ).ToString(@"00\.000\.000\/0000\-00");
            ficha.RazaoSocial = fornecedor.TIPO_PJPF_ID == 3 ? fornecedor.NOME : fornecedor.RAZAO_SOCIAL;
            ficha.NomeFantasia = fornecedor.NOME_FANTASIA;
            //ficha.CNAE = fornecedor.CNAE;
            ficha.InscricaoEstadual = fornecedor.INSCR_ESTADUAL;
            ficha.InscricaoMunicipal = fornecedor.INSCR_MUNICIPAL;

            ficha.SolicitacaoFornecedor.Solicitacao = true;
        }

        public static void PreencheStatusRobo(this AprovacaoController controller, FichaCadastralWebForLinkVM ficha, SOLICITACAO solicitacao, int tpFluxoId)
        {
            ROBO robo;

            ficha.RoboReceita = "Aguardando...";
            ficha.RoboSintegra = "Aguardando...";
            ficha.RoboSimples = "Aguardando...";

            switch (tpFluxoId)
            {
                case (int)EnumTiposFluxo.CadastroFornecedorNacional:
                case (int)EnumTiposFluxo.CadastroFornecedorNacionalDireto:
                    robo = solicitacao.ROBO.FirstOrDefault();
                    if (robo != null)
                    {
                        ficha.RoboReceita = (robo.RF_CONSULTA_DTHR != null) ? robo.RF_SIT_CADASTRAL_CNPJ : "Aguardando...";
                        ficha.RoboSintegra = (robo.SINT_CONSULTA_DTHR != null) ? robo.SINT_IE_SITU_CADASTRAL : "Aguardando...";
                        ficha.RoboSimples = (robo.RF_CONSULTA_DTHR != null) ? robo.SIMPLES_NACIONAL_SITUACAO : "Aguardando...";

                        ficha.FornecedorRobo.SimplesNacionalSituacao = robo.SIMPLES_NACIONAL_SITUACAO == null ? "" : robo.SIMPLES_NACIONAL_SITUACAO;
                    }
                    break;

                case (int)EnumTiposFluxo.CadastroFornecedorPF:
                case (int)EnumTiposFluxo.CadastroFornecedorPFDireto:
                    robo = solicitacao.ROBO.FirstOrDefault();
                    if (robo != null)
                    {
                        ficha.RoboReceita = (robo.RF_CONSULTA_DTHR != null) ? robo.RF_SIT_CADASTRAL_CNPJ : "Aguardando...";
                        ficha.RoboSintegra = "---";
                        ficha.RoboSimples = "---";

                        ficha.FornecedorRobo.SimplesNacionalSituacao = robo.SIMPLES_NACIONAL_SITUACAO == null ? "" : robo.SIMPLES_NACIONAL_SITUACAO;
                    }
                    break;

                case (int)EnumTiposFluxo.CadastroFornecedorEstrangeiro:
                    ficha.RoboReceita = "---";
                    ficha.RoboSintegra = "---";
                    ficha.RoboSimples = "---";
                    break;

                case (int)EnumTiposFluxo.AmpliacaoFornecedor:
                case (int)EnumTiposFluxo.BloqueioFornecedor:
                case (int)EnumTiposFluxo.DesbloqueioFornecedor:
                case (int)EnumTiposFluxo.ModificacaoDadosBancarios:
                case (int)EnumTiposFluxo.ModificacaoEndereco:
                case (int)EnumTiposFluxo.ModificacaoDadosContato:
                case (int)EnumTiposFluxo.ModificacoesGerais:
                    robo = solicitacao.Fornecedor != null ? solicitacao.Fornecedor.ROBO : solicitacao.ROBO.FirstOrDefault();
                    if (robo != null)
                    {
                        ficha.RoboReceita = (robo.RF_CONSULTA_DTHR != null) ? robo.RF_SIT_CADASTRAL_CNPJ : "Aguardando...";
                        ficha.RoboSintegra = (robo.SINT_CONSULTA_DTHR != null) ? robo.SINT_IE_SITU_CADASTRAL : "Aguardando...";
                        ficha.RoboSimples = (robo.RF_CONSULTA_DTHR != null) ? robo.SIMPLES_NACIONAL_SITUACAO : "Aguardando...";

                        ficha.FornecedorRobo.SimplesNacionalSituacao = robo.SIMPLES_NACIONAL_SITUACAO == null ? "" : robo.SIMPLES_NACIONAL_SITUACAO;
                    }
                    break;
            }
        }

        public static List<DadosBancariosVM> ListarSolicitacaoModificacaoBancario(this AprovacaoController controller, SOLICITACAO solicitacao, bool isModificacao)
        {
            return solicitacao.SolicitacaoModificacaoDadosBancario
                .Select(item => new DadosBancariosVM
                {
                    NomeBanco = item.T_BANCO == null ? "" : item.T_BANCO.BANCO_NM,
                    BancoSolicitacaoID = item.ID,
                    BancoPJPFID = isModificacao ? item.BANCO_PJPF_ID : null,
                    Agencia = item.AGENCIA,
                    Digito = item.AG_DV,
                    Banco = item.BANCO_ID,
                    ContaCorrente = item.CONTA,
                    ContaCorrenteDigito = item.CONTA_DV,
                    ArquivoID = item.ARQUIVO_ID,
                    NomeArquivo = item.WFD_ARQUIVOS != null ? item.WFD_ARQUIVOS.NOME_ARQUIVO : null,
                    DataUpload = isModificacao ? item.DATA_UPLOAD : item.WFD_ARQUIVOS != null ? (DateTime?)item.WFD_ARQUIVOS.DATA_UPLOAD : null
                }).ToList();
        }

        public static List<DadosContatoVM> ListarSolicitacaoDadosContato(this AprovacaoController controller, SOLICITACAO solicitacao, bool isSolicitacao)
        {
            return solicitacao.SolicitacaoModificacaoDadosContato
                .Select(item => new DadosContatoVM
                {
                    ContatoID = item.ID,
                    PjPfId = isSolicitacao ? item.CONTATO_PJPF_ID : null,
                    NomeContato = item.NOME,
                    EmailContato = item.EMAIL,
                    Telefone = item.TELEFONE,
                    Celular = item.CELULAR
                }).ToList();
        }
    }
}