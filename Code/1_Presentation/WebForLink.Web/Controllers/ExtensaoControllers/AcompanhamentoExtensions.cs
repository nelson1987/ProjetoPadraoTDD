using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers.Extensoes
{
    public static class AcompanhamentoExtensions
    {
        public static void FornecedorRobo(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM ficha,
            SOLICITACAO solicitacao)
        {
        }

        public static void FornecedorRobo(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM ficha,
            Fornecedor fornecedor)
        {
        }
        
        private static readonly IFornecedorWebForLinkAppService PJpFbp;

        private static Fornecedor RetornaFornecedor(SOLICITACAO solicitacao)
        {
            int pjpfId = 0;
            if (solicitacao.PJPF_ID != null)
                pjpfId = (int)solicitacao.PJPF_ID;

            Fornecedor fornecedor = PJpFbp.BuscarPorId(pjpfId);
            return fornecedor;
        }

        public static void PopularAcompanhamentoQuestionarioDinamico(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;
            modelo.CategoriaNome = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
            modelo.CategoriaId = fornecedor.WFD_CONTRATANTE_PJPF.FirstOrDefault(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).CATEGORIA_ID ?? 0;
            modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            modelo.Solicitacao.Fluxo.Nome = solicitacao.Fluxo.FLUXO_NM;
            modelo.TipoFornecedor = (int)fornecedor.TIPO_PJPF_ID;
            Mapeamento.PopularDadosReceita(modelo, fornecedor);
            Mapeamento.PopularEndereco(modelo, fornecedor);
        }

        public static void PopularAcompanhamentoNovoFornecedor(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            SolicitacaoCadastroFornecedor solicitacaoCadastroFornecedorNacional = solicitacao.SolicitacaoCadastroFornecedor.FirstOrDefault();

            modelo.DadosEnderecos = Mapper.Map<List<SOLICITACAO_MODIFICACAO_ENDERECO>, List<DadosEnderecosVM>>(solicitacao.WFD_SOL_MOD_ENDERECO.ToList());
            modelo.DadosBancarios = Mapper.Map<List<SolicitacaoModificacaoDadosBancario>, List<DadosBancariosVM>>(solicitacao.SolicitacaoModificacaoDadosBancario.ToList());
            modelo.DadosContatos = Mapper.Map<List<SolicitacaoModificacaoDadosContato>, List<DadosContatoVM>>(solicitacao.SolicitacaoModificacaoDadosContato.ToList());
            modelo.SolicitacaoFornecedor.Solicitacao = true;

            //Mapear os Documentos
            modelo.SolicitacaoFornecedor.Documentos =
                Mapper.Map<List<SolicitacaoDeDocumentos>, List<SolicitacaoDocumentosVM>>(solicitacao.SolicitacaoDeDocumentos.Where(x => x.ARQUIVO_ID != null).ToList());

            // Popula a view model FichaCadastralVM
            if (solicitacaoCadastroFornecedorNacional != null)
            {
                modelo.CategoriaId = solicitacaoCadastroFornecedorNacional.CATEGORIA_ID;
                modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
                modelo.Solicitacao.Fluxo.Nome = solicitacao.Fluxo.FLUXO_NM;
                if (solicitacaoCadastroFornecedorNacional != null)
                    modelo.CategoriaNome = solicitacaoCadastroFornecedorNacional.WFD_PJPF_CATEGORIA.DESCRICAO;
                else
                    modelo.CategoriaNome = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
                modelo.RazaoSocial = solicitacaoCadastroFornecedorNacional.PJPF_TIPO != 3
                    ? solicitacaoCadastroFornecedorNacional.RAZAO_SOCIAL
                    : solicitacaoCadastroFornecedorNacional.NOME;
                modelo.NomeFantasia = solicitacaoCadastroFornecedorNacional.NOME_FANTASIA;
                //modelo.CNAE = solicitacaoCadastroFornecedorNacional.CNAE;
                modelo.CNPJ_CPF = solicitacaoCadastroFornecedorNacional.PJPF_TIPO == 3
                    ? Convert.ToUInt64(solicitacaoCadastroFornecedorNacional.CPF).ToString(@"000\.000\.000\-00")
                    : Convert.ToUInt64(solicitacaoCadastroFornecedorNacional.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                modelo.InscricaoEstadual = solicitacaoCadastroFornecedorNacional.INSCR_ESTADUAL;
                modelo.InscricaoMunicipal = solicitacaoCadastroFornecedorNacional.INSCR_MUNICIPAL;
                modelo.TipoFornecedor = solicitacaoCadastroFornecedorNacional.PJPF_TIPO;
                modelo.Observacao = solicitacaoCadastroFornecedorNacional.OBSERVACAO;
                modelo.TipoLogradouro = solicitacaoCadastroFornecedorNacional.TP_LOGRADOURO;
                modelo.Endereco = solicitacaoCadastroFornecedorNacional.ENDERECO;
                modelo.Numero = solicitacaoCadastroFornecedorNacional.NUMERO;
                modelo.Complemento = solicitacaoCadastroFornecedorNacional.COMPLEMENTO;
                modelo.Cep = solicitacaoCadastroFornecedorNacional.CEP;
                modelo.Bairro = solicitacaoCadastroFornecedorNacional.BAIRRO;
                modelo.Cidade = solicitacaoCadastroFornecedorNacional.CIDADE;
                modelo.Estado = solicitacaoCadastroFornecedorNacional.UF;
                modelo.Pais = solicitacaoCadastroFornecedorNacional.PAIS;
            }

            // this.FornecedorRobo(modelo, solicitacao);

            if (solicitacaoCadastroFornecedorNacional.WFD_PJPF_ROBO != null)
                modelo.FornecedorRobo.SimplesNacionalSituacao = solicitacaoCadastroFornecedorNacional.WFD_PJPF_ROBO.SIMPLES_NACIONAL_SITUACAO == null
                    ? ""
                    : solicitacaoCadastroFornecedorNacional.WFD_PJPF_ROBO.SIMPLES_NACIONAL_SITUACAO;
        }

        public static void PopularAcompanhamentoNovoFornecedorEstrangeiro(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            SolicitacaoCadastroFornecedor solicitacaoCadastroFornecedorEstrangeiro = solicitacao.SolicitacaoCadastroFornecedor.First();

            modelo.DadosEnderecos = Mapper.Map<List<SOLICITACAO_MODIFICACAO_ENDERECO>, List<DadosEnderecosVM>>(solicitacao.WFD_SOL_MOD_ENDERECO.ToList());
            modelo.DadosBancarios = Mapper.Map<List<SolicitacaoModificacaoDadosBancario>, List<DadosBancariosVM>>(solicitacao.SolicitacaoModificacaoDadosBancario.ToList());
            modelo.DadosContatos = Mapper.Map<List<SolicitacaoModificacaoDadosContato>, List<DadosContatoVM>>(solicitacao.SolicitacaoModificacaoDadosContato.ToList());
            modelo.SolicitacaoFornecedor.Solicitacao = true;

            modelo.SolicitacaoFornecedor.Documentos = solicitacao.WFD_PJPF_SOLICITACAO_DOCUMENTOS.Select(c => new SolicitacaoDocumentosVM
            {
                ID = c.ID,
                Documento = c.WFD_PJPF_LISTA_DOCUMENTOS.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO + " - " + c.WFD_PJPF_LISTA_DOCUMENTOS.DescricaoDeDocumentos.DESCRICAO,
                PorValidade = c.WFD_PJPF_LISTA_DOCUMENTOS.EXIGE_VALIDADE,
                DataValidade = c.DATA_VENCIMENTO,
                NomeArquivo = c.NOME_ARQUIVO,
                ArquivoID = c.PJPF_ARQUIVO_ID,
                SolicitacaoID = c.SOLICITACAO_ID
            }).ToList();

            // Popula a view model FichaCadastralVM
            modelo.CategoriaId = solicitacaoCadastroFornecedorEstrangeiro.CATEGORIA_ID;
            if (solicitacaoCadastroFornecedorEstrangeiro != null)
                modelo.CategoriaNome = solicitacaoCadastroFornecedorEstrangeiro.WFD_PJPF_CATEGORIA.DESCRICAO;
            else
                modelo.CategoriaNome = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
            modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            modelo.RazaoSocial = solicitacaoCadastroFornecedorEstrangeiro.RAZAO_SOCIAL;
            modelo.NomeFantasia = solicitacaoCadastroFornecedorEstrangeiro.NOME_FANTASIA;
            //modelo.CNAE = solicitacaoCadastroFornecedorEstrangeiro.CNAE;
            modelo.CNPJ_CPF = solicitacaoCadastroFornecedorEstrangeiro.CNPJ;
            modelo.InscricaoEstadual = solicitacaoCadastroFornecedorEstrangeiro.INSCR_ESTADUAL;
            modelo.InscricaoMunicipal = solicitacaoCadastroFornecedorEstrangeiro.INSCR_MUNICIPAL;
            modelo.TipoFornecedor = solicitacaoCadastroFornecedorEstrangeiro.PJPF_TIPO;
            modelo.Observacao = solicitacaoCadastroFornecedorEstrangeiro.OBSERVACAO;

            modelo.TipoFornecedor = solicitacaoCadastroFornecedorEstrangeiro.PJPF_TIPO;
            modelo.Observacao = solicitacaoCadastroFornecedorEstrangeiro.OBSERVACAO;
            modelo.TipoLogradouro = solicitacaoCadastroFornecedorEstrangeiro.TP_LOGRADOURO;
            modelo.Endereco = solicitacaoCadastroFornecedorEstrangeiro.ENDERECO;
            modelo.Numero = solicitacaoCadastroFornecedorEstrangeiro.NUMERO;
            modelo.Complemento = solicitacaoCadastroFornecedorEstrangeiro.COMPLEMENTO;
            modelo.Cep = solicitacaoCadastroFornecedorEstrangeiro.CEP;
            modelo.Bairro = solicitacaoCadastroFornecedorEstrangeiro.BAIRRO;
            modelo.Cidade = solicitacaoCadastroFornecedorEstrangeiro.CIDADE;
            modelo.Estado = solicitacaoCadastroFornecedorEstrangeiro.UF;
            modelo.Pais = solicitacaoCadastroFornecedorEstrangeiro.PAIS;

            modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            modelo.Solicitacao.Fluxo.Nome = solicitacao.Fluxo.FLUXO_NM;
        }

        public static void PopularAcompanhamentoAmpliacao(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            SolicitacaoCadastroFornecedor solicitacaoExpansaoFornecedor = solicitacao.SolicitacaoCadastroFornecedor.First();

            Fornecedor fornecedor = PJpFbp.BuscarPorIdComRelacionamentos((int)solicitacao.PJPF_ID);
            List<DocumentosDoFornecedor> documentosFornecedor = fornecedor.DocumentosDoFornecedor.ToList();

            modelo.DadosEnderecos = Mapper.Map<List<SOLICITACAO_MODIFICACAO_ENDERECO>, List<DadosEnderecosVM>>(solicitacao.WFD_SOL_MOD_ENDERECO.ToList());
            modelo.DadosBancarios = Mapper.Map<List<SolicitacaoModificacaoDadosBancario>, List<DadosBancariosVM>>(solicitacao.SolicitacaoModificacaoDadosBancario.ToList());
            modelo.DadosContatos = Mapper.Map<List<SolicitacaoModificacaoDadosContato>, List<DadosContatoVM>>(solicitacao.SolicitacaoModificacaoDadosContato.ToList());

            modelo.SolicitacaoFornecedor = new SolicitacaoFornecedorVM
            {
                Solicitacao = false,
                Documentos = documentosFornecedor.Select(d => new SolicitacaoDocumentosVM
                {
                    ID = d.ID,
                    Documento =
                        d.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO + " - " +
                        d.DescricaoDeDocumentos.DESCRICAO,
                    DataValidade = d.DATA_VENCIMENTO,
                    NomeArquivo = d.NOME_ARQUIVO,
                    ArquivoID = d.ARQUIVO_ID,
                    SolicitacaoID = d.SOLICITACAO_ID
                }).ToList()
            };

            // Popula a view model FichaCadastralVM
            modelo.CategoriaId = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).CATEGORIA_ID ?? 0;
            modelo.CategoriaNome = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
            modelo.ID = fornecedor.ID;
            modelo.ContratanteID = solicitacao.CONTRATANTE_ID;
            Mapeamento.PopularDadosReceita(modelo, fornecedor);
            Mapeamento.PopularEndereco(modelo, fornecedor);
            modelo.Observacao = solicitacaoExpansaoFornecedor.OBSERVACAO;

            modelo.Solicitacao = new SolicitacaoVM
            {
                Fluxo = new FluxoVM
                {
                    ID = 3
                }
            };

            controller.FornecedorRobo(modelo, RetornaFornecedor(solicitacao));
        }

        public static void PopularAcompanhamentoModificacaoGerais(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;
            SOLICITACAO_MODIFICACAO_DADOSGERAIS solicitacaoModDadosGerais = solicitacao.WFD_SOL_MOD_DGERAIS_SEQ.First();

            // Popula a view model FichaCadastralVM
            modelo.ContratanteID = solicitacao.CONTRATANTE_ID;
            modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            Mapeamento.PopularDadosReceita(modelo, fornecedor);
            modelo.OutrosDadosVisao = solicitacaoModDadosGerais.VISAO_ID;
            modelo.OutrosDadosGrupo = solicitacaoModDadosGerais.GRUPO_ID;
            modelo.OutrosDadosDescricao = solicitacaoModDadosGerais.DESCRICAO_ID;
            modelo.OutrosDadosDescricaoMudança = solicitacaoModDadosGerais.DESCRICAOALTERACAO;

            controller.FornecedorRobo(modelo, RetornaFornecedor(solicitacao));
        }

        public static void PopularAcompanhamentoModificacaoDadosBancarios(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            List<int> lstBancoId = solicitacao.SolicitacaoModificacaoDadosBancario.Select(x => x.BANCO_ID).ToList();
            List<TiposDeBanco> bancos = controller._BancoService.BuscarBancosPorId(lstBancoId);

            // Dados Bancários 
            if (solicitacao.SolicitacaoModificacaoDadosBancario != null && solicitacao.SolicitacaoModificacaoDadosBancario.Any())
            {
                modelo.DadosBancarios.Clear();
                //fazer o foreach de solicitacao.SolicitacoesModificacaoDadosBancario e incluir no ficha Dados de Banco
                modelo.DadosBancarios = Mapper.Map<List<SolicitacaoModificacaoDadosBancario>, List<DadosBancariosVM>>(solicitacao.SolicitacaoModificacaoDadosBancario.ToList());
            }

            Fornecedor fornecedor = solicitacao.Fornecedor;
            modelo.CategoriaNome = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
            modelo.ContratanteID = solicitacao.CONTRATANTE_ID;
            modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            Mapeamento.PopularDadosReceita(modelo, fornecedor);
            controller.FornecedorRobo(modelo, RetornaFornecedor(solicitacao));
        }

        public static void PopularAcompanhamentoModificacaoDadosContatos(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;
            // Dados de Contatos
            modelo.DadosContatos = Mapper.Map<List<DadosContatoVM>>(solicitacao.SolicitacaoModificacaoDadosContato.ToList());

            // Popula a view model FichaCadastralVM
            modelo.CategoriaNome = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
            modelo.ID = fornecedor.ID;
            modelo.ContratanteID = solicitacao.CONTRATANTE_ID;
            Mapeamento.PopularDadosReceita(modelo, fornecedor);
            Mapeamento.PopularEndereco(modelo, fornecedor);
            modelo.TipoFornecedor = (int)fornecedor.TIPO_PJPF_ID;


            modelo.Solicitacao = new SolicitacaoVM
            {
                Fluxo = new FluxoVM
                {
                    ID = 3
                }
            };

            controller.FornecedorRobo(modelo, RetornaFornecedor(solicitacao));
        }

        public static void PopularAcompanhamentoBloqueio(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;
            if (solicitacao.SOLICITACAO_BLOQUEIO != null && solicitacao.SOLICITACAO_BLOQUEIO.Count > 0)
            {
                foreach (var item in solicitacao.SOLICITACAO_BLOQUEIO)
                {
                    string lanc = item.BLQ_LANCAMENTO_TODAS_EMP != null ? (bool)item.BLQ_LANCAMENTO_TODAS_EMP ? "1" : "2" : null;
                    modelo.DadosBloqueio = new DadosBloqueioVM
                    {
                        ID = item.ID,
                        Compra = (bool)item.BLQ_COMPRAS_TODAS_ORG_COMPRAS,
                        ContratanteID = solicitacao.CONTRATANTE_ID,
                        FornecedorID = solicitacao.PJPF_ID,
                        Lancamento = lanc,
                        Motivo = item.BLQ_QUALIDADE_FUNCAO_BQL_ID,
                        MotivoQualidade = item.TipoDeFuncaoDuranteBloqueio != null ? item.TipoDeFuncaoDuranteBloqueio.FUNCAO_BLOQ_DSC : string.Empty,
                        MotivoSolicitacao = item.BLQ_MOTIVO_DSC,
                        SolicitacaoID = solicitacao.ID
                    };
                }

                modelo.ContratanteID = solicitacao.CONTRATANTE_ID;
                modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;

                if (fornecedor != null)
                {
                    modelo.CategoriaNome = fornecedor
                    .WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID)
                    .WFD_PJPF_CATEGORIA.DESCRICAO;
                    Mapeamento.PopularDadosReceita(modelo, fornecedor);
                    modelo.TipoFornecedor = (int)fornecedor.TIPO_PJPF_ID;
                }
                else
                {
                    FORNECEDORBASE fornecedorBase = solicitacao.FORNECEDORBASE;
                    modelo.CategoriaNome = fornecedorBase.WFD_PJPF_CATEGORIA.DESCRICAO;
                    modelo.CNPJ_CPF = fornecedorBase.PJPF_TIPO == 3
                        ? Convert.ToUInt64(fornecedorBase.CPF).ToString(@"000\.000\.000\-00")
                        : Convert.ToUInt64(fornecedorBase.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                    modelo.RazaoSocial = fornecedorBase.PJPF_TIPO == 3
                        ? fornecedorBase.NOME
                        : fornecedorBase.RAZAO_SOCIAL;
                    modelo.NomeFantasia = fornecedorBase.NOME_FANTASIA;
                    //modelo.CNAE = fornecedorBase.CNAE;
                    modelo.InscricaoEstadual = fornecedorBase.INSCR_ESTADUAL;
                    modelo.InscricaoMunicipal = fornecedorBase.INSCR_MUNICIPAL;
                    modelo.TipoFornecedor = fornecedorBase.PJPF_TIPO;
                }
            }

            if (fornecedor != null)
                controller.FornecedorRobo(modelo, RetornaFornecedor(solicitacao));
        }

        public static void PopularAcompanhamentoDesbloqueio(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;
            if (solicitacao.WFD_SOL_DESBLOQ != null && solicitacao.WFD_SOL_DESBLOQ.Count > 0)
            {
                foreach (var item in solicitacao.WFD_SOL_DESBLOQ)
                {
                    string lanc = item.BLQ_LANCAMENTO_TODAS_EMP != null ? (bool)item.BLQ_LANCAMENTO_TODAS_EMP ? "1" : "2" : null;
                    modelo.DadosBloqueio = new DadosBloqueioVM
                    {
                        ID = item.ID,
                        Compra = (bool)item.BLQ_COMPRAS_TODAS_ORG_COMPRAS,
                        ContratanteID = solicitacao.CONTRATANTE_ID,
                        FornecedorID = solicitacao.PJPF_ID,
                        Lancamento = lanc,
                        Motivo = item.BLQ_QUALIDADE_FUNCAO_BQL_ID,
                        MotivoQualidade = item.WFD_T_FUNCAO_BLOQUEIO != null ? item.WFD_T_FUNCAO_BLOQUEIO.FUNCAO_BLOQ_DSC : string.Empty,
                        MotivoSolicitacao = item.BLQ_MOTIVO_DSC,
                        SolicitacaoID = solicitacao.ID
                    };
                }

                modelo.ContratanteID = solicitacao.CONTRATANTE_ID;
                modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;

                if (fornecedor != null)
                {
                    modelo.CategoriaNome = fornecedor
                    .WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID)
                    .WFD_PJPF_CATEGORIA.DESCRICAO;

                    Mapeamento.PopularDadosReceita(modelo, fornecedor);

                    modelo.TipoFornecedor = (int)fornecedor.TIPO_PJPF_ID;
                }
            }

            if (fornecedor != null)
                controller.FornecedorRobo(modelo, RetornaFornecedor(solicitacao));
        }

        public static void PopularAcompanhamentoAtualizacaoDocumento(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            modelo.SolicitacaoFornecedor.Solicitacao = true;

            //Mapear os Documentos
            modelo.SolicitacaoFornecedor.Documentos =
                Mapper.Map<List<SolicitacaoDeDocumentos>, List<SolicitacaoDocumentosVM>>(solicitacao.SolicitacaoDeDocumentos.Where(x => x.ARQUIVO_ID != null).ToList());

            Fornecedor fornecedor = solicitacao.Fornecedor;
            modelo.CategoriaNome = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF.First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID).WFD_PJPF_CATEGORIA.DESCRICAO;
            modelo.ContratanteID = solicitacao.CONTRATANTE_ID;
            modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;

            Mapeamento.PopularDadosReceita(modelo, fornecedor);

            modelo.TipoFornecedor = fornecedor.TIPO_PJPF_ID ?? 0;

            controller.FornecedorRobo(modelo, RetornaFornecedor(solicitacao));

        }

        public static void PopularAcompanhamentoPreencheStatusRobo(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao, int tpFluxoId)
        {
            ROBO fornecedorRobo;

            modelo.RoboReceita = "Aguardando...";
            modelo.RoboSintegra = "Aguardando...";
            modelo.RoboSimples = "Aguardando...";

            switch (tpFluxoId)
            {
                case (int)EnumTiposFluxo.CadastroFornecedorNacional:
                case (int)EnumTiposFluxo.CadastroFornecedorNacionalDireto:
                    fornecedorRobo = solicitacao.PJPF_BASE_ID != null ? solicitacao.FORNECEDORBASE.ROBO : solicitacao.ROBO.FirstOrDefault();
                    if (fornecedorRobo != null)
                    {
                        modelo.RoboReceita = (fornecedorRobo.RF_CONSULTA_DTHR != null) ? fornecedorRobo.RF_SIT_CADASTRAL_CNPJ : "Aguardando...";
                        modelo.RoboSintegra = (fornecedorRobo.SINT_CONSULTA_DTHR != null) ? fornecedorRobo.SINT_IE_SITU_CADASTRAL : "Aguardando...";
                        modelo.RoboSimples = (fornecedorRobo.RF_CONSULTA_DTHR != null) ? fornecedorRobo.SIMPLES_NACIONAL_SITUACAO : "Aguardando...";

                        modelo.FornecedorRobo.SimplesNacionalSituacao = fornecedorRobo.SIMPLES_NACIONAL_SITUACAO == null ? "" : fornecedorRobo.SIMPLES_NACIONAL_SITUACAO;
                    }
                    break;

                case (int)EnumTiposFluxo.CadastroFornecedorPF:
                case (int)EnumTiposFluxo.CadastroFornecedorPFDireto:
                    fornecedorRobo = solicitacao.PJPF_BASE_ID != null ? solicitacao.FORNECEDORBASE.ROBO : solicitacao.ROBO.FirstOrDefault();
                    if (fornecedorRobo != null)
                    {
                        modelo.RoboReceita = (fornecedorRobo.RF_CONSULTA_DTHR != null) ? fornecedorRobo.RF_SIT_CADASTRAL_CNPJ : "Aguardando...";
                        modelo.RoboSintegra = "---";
                        modelo.RoboSimples = "---";

                        modelo.FornecedorRobo.SimplesNacionalSituacao = fornecedorRobo.SIMPLES_NACIONAL_SITUACAO == null ? "" : fornecedorRobo.SIMPLES_NACIONAL_SITUACAO;
                    }
                    break;

                case (int)EnumTiposFluxo.CadastroFornecedorEstrangeiro:
                    modelo.RoboReceita = "---";
                    modelo.RoboSintegra = "---";
                    modelo.RoboSimples = "---";
                    break;

                case (int)EnumTiposFluxo.AmpliacaoFornecedor:
                case (int)EnumTiposFluxo.BloqueioFornecedor:
                case (int)EnumTiposFluxo.DesbloqueioFornecedor:
                case (int)EnumTiposFluxo.ModificacaoDadosBancarios:
                case (int)EnumTiposFluxo.ModificacaoDadosContato:
                case (int)EnumTiposFluxo.ModificacoesGerais:
                case (int)EnumTiposFluxo.ModificacaoQuestionarioDinamico:

                    //robo = solicitacao.Fornecedor != null? solicitacao.Fornecedor.ROBO: solicitacao.ROBO.FirstOrDefault();
                    fornecedorRobo = solicitacao.PJPF_BASE_ID != null ? solicitacao.FORNECEDORBASE.ROBO : solicitacao.Fornecedor.ROBO;

                    if (fornecedorRobo != null)
                    {
                        modelo.RoboReceita = (fornecedorRobo.RF_CONSULTA_DTHR != null) ? fornecedorRobo.RF_SIT_CADASTRAL_CNPJ : "Aguardando...";
                        modelo.RoboSintegra = (fornecedorRobo.SINT_CONSULTA_DTHR != null) ? fornecedorRobo.SINT_IE_SITU_CADASTRAL : "Aguardando...";
                        modelo.RoboSimples = (fornecedorRobo.RF_CONSULTA_DTHR != null) ? fornecedorRobo.SIMPLES_NACIONAL_SITUACAO : "Aguardando...";

                        modelo.FornecedorRobo.SimplesNacionalSituacao = fornecedorRobo.SIMPLES_NACIONAL_SITUACAO == null ? "" : fornecedorRobo.SIMPLES_NACIONAL_SITUACAO;
                    }
                    break;
            }
        }

        public static void PopularAcompanhamentoModificacaoUnspsc(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;
            modelo.CategoriaNome = solicitacao.Fornecedor.WFD_CONTRATANTE_PJPF
                .First(x => x.CONTRATANTE_ID == solicitacao.CONTRATANTE_ID)
                .WFD_PJPF_CATEGORIA.DESCRICAO;
            modelo.ContratanteID = solicitacao.CONTRATANTE_ID;
            modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
            Mapeamento.PopularDadosReceita(modelo, fornecedor);
            controller.FornecedorRobo(modelo, RetornaFornecedor(solicitacao));
        }

        public static void PopularAcompanhamentoModificacaoEndereco(this ControleSolicitacaoController controller, FichaCadastralWebForLinkVM modelo, SOLICITACAO solicitacao)
        {
            Fornecedor fornecedor = solicitacao.Fornecedor;

            modelo.DadosEnderecos = Mapper.Map<List<SOLICITACAO_MODIFICACAO_ENDERECO>, List<DadosEnderecosVM>>(solicitacao.WFD_SOL_MOD_ENDERECO.ToList());

            modelo.SolicitacaoFornecedor.Solicitacao = true;

            // Popula a view model FichaCadastralVM
            if (fornecedor != null)
            {
                modelo.Solicitacao.Fluxo.ID = solicitacao.FLUXO_ID;
                modelo.Solicitacao.Fluxo.Nome = solicitacao.Fluxo.FLUXO_NM;
                Mapeamento.PopularDadosReceita(modelo, fornecedor);
                Mapeamento.PopularEndereco(modelo, fornecedor);
                modelo.TipoFornecedor = (int)fornecedor.TIPO_PJPF_ID;
            }
            controller.FornecedorRobo(modelo, solicitacao);
        }
    }
}