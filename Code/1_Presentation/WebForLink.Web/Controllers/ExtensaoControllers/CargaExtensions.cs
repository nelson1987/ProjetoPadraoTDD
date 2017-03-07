using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Models;
using WebForLink.Service.Process;
using WebForLink.Service.Services.Process;
using WebForLink.Web.Exceptions;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.Carga;

namespace WebForLink.Web.Controllers.Extensoes
{
    public static class CargaExtensions
    {
        public DbContext Db;
        #region Mapeamento de Retorno
        public static RetornoModel MapearRetornoModificacaoDesbloqueio(string[] words)
        {
            var linhaNova = new RetornoModel
            {
                TipoAcao = int.Parse(words[0]),
                CodigoSolicitacao = ConverterDadosRetorno(words[1]),
                Empresa = int.Parse(words[2]),
                CodigoSap = words[3],
                CodigoRetorno = ConverterDadosRetorno(words[4]), //0-sucesso 1-erro
                TextoRetorno = words[5],
            };
            return linhaNova;
        }

        public static RetornoModel MapearRetornoModificacaoBloqueio(string[] words)
        {
            var linhaNova = new RetornoModel
            {
                TipoAcao = int.Parse(words[0]),
                CodigoSolicitacao = ConverterDadosRetorno(words[1]),
                Empresa = int.Parse(words[2]),
                CodigoSap = words[3],
                CodigoRetorno = ConverterDadosRetorno(words[4]), //0-sucesso 1-erro
                TextoRetorno = words[5],
            };
            return linhaNova;
        }

        public static RetornoModel MapearRetornoModificacaoContatos(string[] words)
        {
            var linhaNova = new RetornoModel
            {
                TipoAcao = int.Parse(words[0]),
                CodigoSolicitacao = ConverterDadosRetorno(words[1]),
                Empresa = int.Parse(words[2]),
                CodigoSap = words[3],
                CodigoRetorno = ConverterDadosRetorno(words[4]), //0-sucesso 1-erro
                TextoRetorno = words[5],
            };
            return linhaNova;
        }

        public static RetornoModel MapearRetornoModificacaoBancario(string[] words)
        {
            var linhaNova = new RetornoModel
            {
                TipoAcao = int.Parse(words[0]),
                CodigoSolicitacao = ConverterDadosRetorno(words[1]),
                Empresa = int.Parse(words[2]),
                CodigoSap = words[3],
                CodigoRetorno = ConverterDadosRetorno(words[4]), //0-sucesso 1-erro
                TextoRetorno = words[5],
            };
            return linhaNova;
        }

        public static RetornoModel MapearRetornoModificacaoFiscal(string[] words)
        {
            var linhaNova = new RetornoModel
            {
                TipoAcao = int.Parse(words[0]),
                CodigoSolicitacao = ConverterDadosRetorno(words[1]),
                Empresa = int.Parse(words[2]),
                CodigoSap = words[3],
                CodigoRetorno = ConverterDadosRetorno(words[4]), //0-sucesso 1-erro
                TextoRetorno = words[5],
            };
            return linhaNova;
        }

        public static RetornoModel MapearRetornoAmpliacaoFornecedores(string[] words)
        {
            var dadoSaida = ValidarDadoSaida(words);
            var linhaNova = new RetornoModel
            {
                TipoAcao = dadoSaida,
                CodigoSolicitacao = ConverterDadosRetorno(words[1]),
                Empresa = int.Parse(words[2]),
                CodigoSap = words[3],
                CodigoRetorno = ConverterDadosRetorno(words[4]), //0-sucesso 1-erro
                TextoRetorno = words[5],
            };
            return linhaNova;
        }

        private static int ValidarDadoSaida(string[] words)
        {
            int dadoSaida;
            if (!string.IsNullOrEmpty(words[0]))
            {
                dadoSaida = int.Parse(words[0]);
            }
            else
            {
                throw new WebForLinkException("Erro na conversão de dados da carga!");
            }
            return dadoSaida;
        }

        #endregion

        #region Mapear Carga
        public static RetornoFornecedoresModel MapearRetornoCriacaoFornecedores(string[] words)
        {
            var dadoSaida = ValidarDadoSaida(words);
            var linhaNova = new RetornoFornecedoresModel
            {
                TipoAcao = dadoSaida,
                CodigoSolicitacao = ConverterDadosRetorno(words[1]),
                CodigoFornecedorSap = words[2],
                Empresa = int.Parse(words[3]),
                GrupoContas = int.Parse(words[4]),
                OrganizacaoCompras = int.Parse(words[5]),
                CNPJ = words[6],
                CPF = words[7],
                CodigoRetorno = ConverterDadosRetorno(words[8]), //0-sucesso 1-erro
                DescricaoErro = words[9],
            };
            return linhaNova;
        }
               
        public static FornecedorCargaModel MapearAmpliacaoFornecedorComSolicitacao(this CargaController controller, SOLICITACAO item, SOLICITACAO_CADASTRO_FORNECEDOR fornecedor)
        {
            //EMPRESA -  CONTRANTE_COD_ERP
            CONTRATANTE empresa = controller.contratanteBP.BuscarPorId(item.CONTRATANTE_ID);
            if (empresa == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Empresa está nulo.{0}", item.ID)));

            //GRUPOCONTAS - CODIGo
            FORNECEDOR_CATEGORIA categoria = controller.pjPfCategoriaBP.BuscarPorId(fornecedor.CATEGORIA_ID);
            if (categoria == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Categoria está nulo.{0}", item.ID)));

            //OrganizacaoCompras - ORG_COMPRAS_COD
            CONTRATANTE_ORGANIZACAO_COMPRAS organizacao = controller.contratanteOrganizacaoComprasBP.BuscarPorContratanteId(item.CONTRATANTE_ID);
            if (organizacao == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Organização está nulo.{0}", item.ID)));

            WFD_CONTRATANTE_PJPF contratanteFornecedor = controller.contratantePJPFBP.BuscarPorPjPfId((int)item.PJPF_ID);
            if (contratanteFornecedor == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("ContratanteFornecedor está nulo.{0}", item.ID)));

            FornecedorCargaModel ampliacaoFornecedor = new FornecedorCargaModel()
            {
                CodigoSolicitacao = ValidaVazio(item.ID.ToString()),
                Empresa = ValidaVazio(empresa.CONTRANTE_COD_ERP),
                CodigoSAP = ValidaVazio(contratanteFornecedor.PJPF_COD_ERP),
                OrganizacaoCompras = ValidaVazio(organizacao.ORG_COMPRAS_COD),

                //TODO: Descomentar para prosseguir os teste desta demanda
                /*
                Banco = ValidaVazio(fornecedor.BANCO_ID != null ? fornecedor.BANCO_ID.ToString() : string.Empty),
                Agencia = ValidaVazio(fornecedor.AGENCIA),
                CodigoAgencia = ValidaVazio(fornecedor.AG_DV),
                ContaCorrente = ValidaVazio(fornecedor.CONTA),
                DVContaCorrente = ValidaVazio(fornecedor.CONTA_DV),
                NomeContatoVendedor = ValidaVazio(fornecedor.CNTT_NM),
                TelefoneVendedor = ValidaVazio(fornecedor.CNTT_TEL),
                */
            };
            return ampliacaoFornecedor;
        }
               
        public static FornecedorCargaModel MapearFornecedorEstrangeiroComSolicitacao(this CargaController controller, SOLICITACAO item, SOLICITACAO_CADASTRO_FORNECEDOR fornecedor)
        {
            CONTRATANTE empresa = controller.contratanteBP.BuscarPorId(item.CONTRATANTE_ID);//EMPRESA -  CONTRANTE_COD_ERP
            if (empresa == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Empresa está nulo.{0}", item.ID)));

            FORNECEDOR_CATEGORIA categoria = controller.pjPfCategoriaBP.BuscarPorId(fornecedor.CATEGORIA_ID);      //GRUPOCONTAS - CODIGo
            if (categoria == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Categoria está nulo.{0}", item.ID)));

            CONTRATANTE_ORGANIZACAO_COMPRAS organizacao = controller.contratanteOrganizacaoComprasBP.BuscarPorContratanteId(item.CONTRATANTE_ID);   //OrganizacaoCompras - ORG_COMPRAS_COD
            if (organizacao == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Organização está nulo.{0}", item.ID)));

            FornecedorCargaModel dadosFornecedorEstrangeiro = new FornecedorCargaModel()
            {
                CodigoSolicitacao = ValidaVazio(item.ID.ToString()),
                Empresa = ValidaVazio(empresa.CONTRANTE_COD_ERP),
                GrupoContas = ValidaVazio(categoria.CODIGO),
                OrganizacaoCompras = ValidaVazio(organizacao.ORG_COMPRAS_COD),
                Cliente = ValidaVazio(fornecedor.CLIENTE),
                SimplesNacional = string.Empty,
                Nome1 = fornecedor.PJPF_TIPO == 3 ? ValidaVazio(fornecedor.NOME) : ValidaVazio(fornecedor.RAZAO_SOCIAL),
                NomeFantasia = ValidaVazio(fornecedor.NOME_FANTASIA),
                CEP = ValidaVazioAdicionarTraco(fornecedor.CEP),
                Cidade = string.IsNullOrEmpty(ValidaVazio(fornecedor.CIDADE)) ? string.Empty : fornecedor.CIDADE.ToUpper(),
                TipoLogradouro = ValidaVazio(fornecedor.TP_LOGRADOURO),
                Rua = ValidaVazio(fornecedor.ENDERECO),
                Numero = ValidaVazio(fornecedor.NUMERO),
                Complemento = ValidaVazio(fornecedor.COMPLEMENTO),
                Bairro = ValidaVazio(fornecedor.BAIRRO),
                Estado = ValidaVazio(fornecedor.UF),

                //TODO: Descomentar para prosseguir os teste desta demanda
                /*
                Telefone = ValidaVazio(fornecedor.CNTT_TEL),
                EnderecoMail = ValidaVazio(fornecedor.CNTT_EMAIL),
                TelefoneCelular = ValidaVazio(fornecedor.CNTT_CEL),
                GrupoEmpresas = ValidaVazio(fornecedor.GRUPO_EMPRESA),
                InscricaoEstadual = string.IsNullOrEmpty(fornecedor.INSCR_ESTADUAL) ? fornecedor.INSCR_ESTADUAL : "Isento",
                InscricaoMunicipal = ValidaVazio(fornecedor.INSCR_MUNICIPAL),
                Banco = ValidaVazio(fornecedor.BANCO_ID != null ? fornecedor.BANCO_ID.ToString() : string.Empty),
                Agencia = ValidaVazio(fornecedor.AGENCIA),
                CodigoAgencia = ValidaVazio(fornecedor.AG_DV),
                ContaCorrente = ValidaVazio(fornecedor.CONTA),
                DVContaCorrente = ValidaVazio(fornecedor.CONTA_DV),
                NomeContatoVendedor = ValidaVazio(fornecedor.CNTT_NM),
                TelefoneVendedor = ValidaVazio(fornecedor.CNTT_TEL),
                */
                Pais = ValidaVazio(fornecedor.PAIS),
            };
            return dadosFornecedorEstrangeiro;
        }
               
        public static FornecedorCargaModel MapearFornecedorNacionalComSolicitacao(this CargaController controller, SOLICITACAO item, SOLICITACAO_CADASTRO_FORNECEDOR fornecedor)
        {
            CONTRATANTE empresa = controller.contratanteBP.BuscarPorId(item.CONTRATANTE_ID);//EMPRESA -  CONTRANTE_COD_ERP
            if (empresa == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Empresa está nulo.{0}", item.ID)));

            ROBO robo = controller.pJPFRoboBP.BuscarPorIdSolicitacao(item.ID);//SIMPLES NACIONAL

            FORNECEDOR_CATEGORIA categoria = controller.pjPfCategoriaBP.BuscarPorId(fornecedor.CATEGORIA_ID);//GRUPOCONTAS - CODIGo
            if (categoria == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Categoria está nulo.{0}", item.ID)));

            CONTRATANTE_ORGANIZACAO_COMPRAS organizacao = controller.contratanteOrganizacaoComprasBP.BuscarPorContratanteId(item.CONTRATANTE_ID);  //OrganizacaoCompras - ORG_COMPRAS_COD
            if (organizacao == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Organização está nulo.{0}", item.ID)));
            FornecedorCargaModel dadosFornecedor = new FornecedorCargaModel()
            {
                CodigoSolicitacao = ValidaVazio(item.ID.ToString()),
                Empresa = ValidaVazio(empresa.CONTRANTE_COD_ERP),
                GrupoContas = ValidaVazio(categoria.CODIGO),
                OrganizacaoCompras = ValidaVazio(organizacao.ORG_COMPRAS_COD),
                Cliente = ValidaVazio(fornecedor.CLIENTE),
                SimplesNacional = robo != null ? ValidaVazio(robo.SIMPLES_NACIONAL_SITUACAO) : "",
                Nome1 = fornecedor.PJPF_TIPO == 3 ? ValidaVazio(fornecedor.NOME) : ValidaVazio(fornecedor.RAZAO_SOCIAL),
                NomeFantasia = ValidaVazio(fornecedor.NOME_FANTASIA),
                CEP = ValidaVazioAdicionarTraco(fornecedor.CEP),
                Cidade = string.IsNullOrEmpty(ValidaVazio(fornecedor.CIDADE)) ? string.Empty : fornecedor.CIDADE.ToUpper(),
                TipoLogradouro = ValidaVazio(fornecedor.TP_LOGRADOURO),
                Rua = ValidaVazio(fornecedor.ENDERECO),
                Numero = ValidaVazio(fornecedor.NUMERO),
                Complemento = ValidaVazio(fornecedor.COMPLEMENTO),
                Bairro = ValidaVazio(fornecedor.BAIRRO),
                Estado = ValidaVazio(fornecedor.UF),

                //TODO: Descomentar para prosseguir os teste desta demanda
                /*
                Telefone = ValidaVazio(fornecedor.CNTT_TEL),
                EnderecoMail = ValidaVazio(fornecedor.CNTT_EMAIL),
                TelefoneCelular = ValidaVazio(fornecedor.CNTT_CEL),
                GrupoEmpresas = ValidaVazio(fornecedor.GRUPO_EMPRESA),
                CNPJ = ValidaVazio(fornecedor.CNPJ),
                CPF = ValidaVazio(fornecedor.CPF),
                InscricaoEstadual = string.IsNullOrEmpty(fornecedor.INSCR_ESTADUAL) ? fornecedor.INSCR_ESTADUAL : "Isento",
                InscricaoMunicipal = ValidaVazio(fornecedor.INSCR_MUNICIPAL),
                Banco = ValidaVazio(fornecedor.BANCO_ID != null ? fornecedor.BANCO_ID.ToString() : string.Empty),
                Agencia = ValidaVazio(fornecedor.AGENCIA),
                CodigoAgencia = ValidaVazio(fornecedor.AG_DV),
                ContaCorrente = ValidaVazio(fornecedor.CONTA),
                DVContaCorrente = ValidaVazio(fornecedor.CONTA_DV),
                NomeContatoVendedor = ValidaVazio(fornecedor.CNTT_NM),
                TelefoneVendedor = ValidaVazio(fornecedor.CNTT_TEL),
                */
                Pais = ValidaVazio(fornecedor.PAIS),
            };
            return dadosFornecedor;
        }
               
        public static List<string> MapearModificacaoContatosComSolicitacaoDadosContatos(SOLICITACAO item, List<SOLICITACAO_MODIFICACAO_CONTATO> solDadosContatos)
        {
            List<string> dadosContatosList = new List<string>();
            foreach (var solContato in solDadosContatos)
            {
                DadosContatoCargaModel dadosContato = new DadosContatoCargaModel
                {
                    CodigoSolicitacao = item.ID.ToString(),
                    Empresa = item.PJPF_ID.ToString(),
                    OrganizacaoCompras = item.CONTRATANTE_ID.ToString(),
                    CodigoSAP = null,
                    Nome = solContato.NOME,
                    EMail = solContato.EMAIL,
                    Telefone = solContato.TELEFONE,
                    Celular = solContato.CELULAR,
                };
                dadosContatosList.Add(dadosContato.GerarLinhaModificarDadosContato());
            }
            return dadosContatosList;
        }
        
        public static DadosBancariosCargaModel MapearModificacaoBancoComSolicitacaoDadosBancario(int? solicitacaoId, string empresa, string grupoContas, string organizacaoCompras, List<SOLICITACAO_MODIFICACAO_BANCO> solBancario)
        {
            DadosBancariosCargaModel dadosBancarios = new DadosBancariosCargaModel();
            dadosBancarios.CodigoSAP = ValidaVazio(solicitacaoId.ToString());
            dadosBancarios.CodigoSolicitacao = ValidaVazio(empresa);
            dadosBancarios.Empresa = ValidaVazio(grupoContas);
            dadosBancarios.OrganizacaoCompras = ValidaVazio(organizacaoCompras);
            int solicitaBanco = 0;
            foreach (var itemBanco in solBancario)
            {
                if (solicitaBanco == 0)
                {
                    dadosBancarios.Agencia = itemBanco.AGENCIA;
                    dadosBancarios.Banco = itemBanco.BANCO_ID.ToString();
                    dadosBancarios.CodigoAgencia = itemBanco.AGENCIA;
                    dadosBancarios.ContaCorrente = itemBanco.CONTA;
                    dadosBancarios.DVContaCorrente = itemBanco.CONTA_DV;
                }
                if (solicitaBanco == 1)
                {
                    dadosBancarios.Agencia2 = itemBanco.AGENCIA;
                    dadosBancarios.Banco2 = itemBanco.BANCO_ID.ToString();
                    dadosBancarios.CodigoAgencia2 = itemBanco.AGENCIA;
                    dadosBancarios.ContaCorrente2 = itemBanco.CONTA;
                    dadosBancarios.DVContaCorrente2 = itemBanco.CONTA_DV;
                }
                if (solicitaBanco == 2)
                {
                    dadosBancarios.Agencia3 = itemBanco.AGENCIA;
                    dadosBancarios.Banco3 = itemBanco.BANCO_ID.ToString();
                    dadosBancarios.CodigoAgencia3 = itemBanco.AGENCIA;
                    dadosBancarios.ContaCorrente3 = itemBanco.CONTA;
                    dadosBancarios.DVContaCorrente3 = itemBanco.CONTA_DV;
                }
                if (solicitaBanco == 3)
                {
                    dadosBancarios.Agencia4 = itemBanco.AGENCIA;
                    dadosBancarios.Banco4 = itemBanco.BANCO_ID.ToString();
                    dadosBancarios.CodigoAgencia4 = itemBanco.AGENCIA;
                    dadosBancarios.ContaCorrente4 = itemBanco.CONTA;
                    dadosBancarios.DVContaCorrente4 = itemBanco.CONTA_DV;
                }
                if (solicitaBanco == 4)
                {
                    dadosBancarios.Agencia5 = itemBanco.AGENCIA;
                    dadosBancarios.Banco5 = itemBanco.BANCO_ID.ToString();
                    dadosBancarios.CodigoAgencia5 = itemBanco.AGENCIA;
                    dadosBancarios.ContaCorrente5 = itemBanco.CONTA;
                    dadosBancarios.DVContaCorrente5 = itemBanco.CONTA_DV;
                }
                solicitaBanco++;
            }
            return dadosBancarios;
        }
        #endregion

        private static int ConverterDadosRetorno(string dadoEntrada2)
        {
            var dadoSaida = 0;
            if (!string.IsNullOrEmpty(dadoEntrada2))
            {
                dadoSaida = int.Parse(dadoEntrada2);
            }
            return dadoSaida;
        }

        public static List<SOLICITACAO> TrazerSolicitacaoBancosAprovadaPorId()
        {
            return Db.WFD_SOLICITACAO
                                .Include("WFD_CONTRATANTE")
                                .Include("WFD_SOLICITACAO_STATUS")
                                .Include("WFD_USUARIO")
                                .Include("WFD_SOLICITACAO_TRAMITE.WFL_PAPEL")
                                .Include("WFD_PJPF")
                                .Include("WFL_FLUXO")
                                .Include("WFD_SOL_CAD_PJPF")
                                .Where(x => x.WFD_SOLICITACAO_TRAMITE
                                    .Any(y => y.PAPEL_ID == 6 &&
                                        y.SOLICITACAO_STATUS_ID == 1)
                                )
                                .OrderBy(x => x.ID)
                                .ToList();
        }

        public static void IncluirObjetoEmListaGenerica<T>(T objeto, string nomeArquivo, IList<T> listaSaida)
        {
            listaSaida.Add(objeto);
        }

        public static string ValidaVazio(string dadoBanco)
        {

            return string.IsNullOrEmpty(dadoBanco) ? string.Empty : dadoBanco;
        }

        public static string ValidaVazioAdicionarTraco(string cep)
        {
            return string.IsNullOrEmpty(cep) ? string.Empty : cep.Substring(0, cep.Length - 3) + "-" + cep.Substring(cep.Length - 3);
        }

        public static List<string> CriarArquivoRetornoCriacaoForn(DemandaSapModel geradorArquivo, FornecedorCargaModel dadosFornecedor)
        {
            var dadosCriacaoFornecedorListRetorno = new List<string>();
            dadosCriacaoFornecedorListRetorno.Add(dadosFornecedor.GerarLinhaCargaCriarFornecedorRetorno());
            geradorArquivo.GerarArquivoRetorno(dadosCriacaoFornecedorListRetorno, "RET_PJPF_Criacao");
            return dadosCriacaoFornecedorListRetorno;
        }

        public static void BackupDeleteArquivoRetorno(string diretorioBackup, string arquivoCriacaoFornecedores, StreamReader arquivo)
        {
            if (System.IO.File.Exists(arquivoCriacaoFornecedores))
            {
                string nomeArquivo = Path.GetFileName(arquivoCriacaoFornecedores);
                arquivo.Close();
                System.IO.File.Copy(arquivoCriacaoFornecedores, diretorioBackup + @"\" + nomeArquivo, true);
                System.IO.File.Delete(arquivoCriacaoFornecedores);
            }
        }

        public static void FinalizarSolicitacaoRetorno(RetornoModel linhaNova)
        {
            SOLICITACAO solicitacao = SolicitacaoBP.BuscarPorIdIncluindoFluxo(linhaNova.CodigoSolicitacao);

            if (solicitacao == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", linhaNova.CodigoSolicitacao)));

            SOLICITACAO_CADASTRO_FORNECEDOR fornecedor = Db.WFD_SOL_CAD_PJPF
                    .FirstOrDefault(x => x.SOLICITACAO_ID == solicitacao.ID);

            if (fornecedor == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", linhaNova.CodigoSolicitacao)));

            fornecedor.COD_PJPF_ERP = linhaNova.CodigoSap;
            Db.Entry(fornecedor).State = EntityState.Modified;
            Db.SaveChanges();

            //Aprovacao.FinalizaSolicitacao(solicitacao.FLUXO_ID, solicitacao.ID);

            using (var aprovacaoBp = new AprovacaoService())
            {
                int? grupoId = (int?)_metodosGerais.PegaAuthTicket("Grupo");
                aprovacaoBp.FinalizaSolicitacao(grupoId, solicitacao.WFL_FLUXO.FLUXO_TP_ID, solicitacao.ID);
            }
            Tramite.AtualizaTramite(solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, (int)EnumPapeisWorkflow.RetornoCarga, 2, null);
        }

        public static void FinalizarModificacaoRetorno(RetornoModel linhaNova)
        {
            SOLICITACAO solicitacao = SolicitacaoBP.BuscarPorIdIncluindoFluxo(linhaNova.CodigoSolicitacao);

            if (solicitacao == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", linhaNova.CodigoSolicitacao)));

            //Aprovacao.FinalizaSolicitacao(solicitacao.FLUXO_ID, solicitacao.ID);
            using (var aprovacaoBp = new AprovacaoService())
            {
                int? grupoId = (int?)_metodosGerais.PegaAuthTicket("Grupo");
                aprovacaoBp.FinalizaSolicitacao(grupoId, solicitacao.WFL_FLUXO.FLUXO_TP_ID, solicitacao.ID);
            }
            Tramite.AtualizaTramite(solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, (int)EnumPapeisWorkflow.RetornoCarga, 2, null);
            //if (_metodosGerais.EnviarEmail("nelson.neto@chconsultoria.com.br", "Teste de Email", "Email Enviado Com Sucesso!"))
            //{
            //}
        }

        public static void FinalizarSolicitacaoRetorno(RetornoFornecedoresModel linhaNova)
        {
            SOLICITACAO solicitacao = solicitacaoBP.BuscarPorIdIncluindoFluxo(linhaNova.CodigoSolicitacao);

            if (solicitacao == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", linhaNova.CodigoSolicitacao)));

            IncluirCodigoErpCadastroFornecedor(linhaNova, solicitacao);

            //Aprovacao.FinalizaSolicitacao(solicitacao.FLUXO_ID, solicitacao.ID);
            using (var aprovacaoBp = new AprovacaoService())
            {
                int? grupoId = (int?)_metodosGerais.PegaAuthTicket("Grupo");
                aprovacaoBp.FinalizaSolicitacao(grupoId, solicitacao.WFL_FLUXO.FLUXO_TP_ID, solicitacao.ID);
            }
            Tramite.AtualizaTramite(solicitacao.CONTRATANTE_ID, solicitacao.ID, solicitacao.FLUXO_ID, (int)EnumPapeisWorkflow.RetornoCarga, 2, null);

            USUARIO usuarioSolicitante = Db.WFD_USUARIO
                .FirstOrDefault(x => x.ID == solicitacao.USUARIO_ID);

            if (usuarioSolicitante == null)
                throw new WebForLinkException(string.Format("A solicitação Nº{0} não tem solicitante cadastrado na base.", solicitacao.ID));

            if (usuarioSolicitante.EMAIL == null)
                throw new WebForLinkException("O Solicitante não tem e-mail cadastrado na base");

            Db.SaveChanges();

            EnviarEmailFornecedorUsuario(usuarioSolicitante);

        }

        private static void EnviarEmailFornecedorUsuario(this CargaController controller, USUARIO usuarioSolicitante)
        {
            // CRIPTOGRAFA A URL QUE SERA ENVIADA AO USUÁRIO
            string url = controller.Url.Action("CadastrarUsuario", "Home",
                new { chaveurl = controller.Cripto.Encrypt(string.Format("id={0}&tipocadastro={1}", usuarioSolicitante.ID, (int)EnumTipoCadastroNovoUsuario.Cadastrado), Key) },
                controller.Request.Url.Scheme);
            string assunto = "WebForLink - Cadastro de Usuário concluído com sucesso";
            string mensagem = "";
            mensagem += "<p style='text-align: center;'><h3><b>WebForLink</b></h3><br />";
            mensagem += "<b>Incluir novo usuário</b></p>";
            mensagem += "<p style='text-align: left'>";
            mensagem += "Você está recebendo este e-mail porque sua solicitação de cadastro foi concluída.<br />";
            mensagem +=
                "Para proceder com a inclusão da senha clique no link abaixo ou copie e cole em seu navegador<br /><br />";
            mensagem += "<a href='" + url + "'>Link</a> - " + url;
            mensagem += "</p><br /><br />";
            mensagem += "<p style='font-size: 10px;'>Este é um e-mail automático, favor não responder!</p>";

            _metodosGerais.EnviarEmail(usuarioSolicitante.EMAIL, assunto, mensagem);
        }

        private static void IncluirCodigoErpCadastroFornecedor(RetornoFornecedoresModel linhaNova, SOLICITACAO solicitacao)
        {
            using (var trans = Db.Database.BeginTransaction())
            {
                try
                {
                    SOLICITACAO_CADASTRO_FORNECEDOR fornecedor = Db.WFD_SOL_CAD_PJPF
                        .FirstOrDefault(x => x.SOLICITACAO_ID == solicitacao.ID);

                    if (fornecedor == null)
                        throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                            new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.",
                                linhaNova.CodigoSolicitacao)));

                    fornecedor.COD_PJPF_ERP = linhaNova.CodigoFornecedorSap;
                    Db.Entry(fornecedor).State = EntityState.Modified;
                    Db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                    trans.Rollback();
                }
            }
        }

        public static void ExecutarRetornoCargaSap(this CargaController controller)
        {
            var diretorioPadrao = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioRetornoSap");
            var diretorioBackup = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioRetornoSapBackup");
            if (!Directory.Exists(diretorioPadrao))
            {
                Directory.CreateDirectory(diretorioPadrao);
            }
            if (!Directory.Exists(diretorioBackup))
            {
                Directory.CreateDirectory(diretorioBackup);
            }

            //Criação de Fornecedor Nacional
            CriacaoFornecedores(diretorioPadrao, diretorioBackup);

            //Ampliação de Fornecedor
            AmpliacaoFornecedores(diretorioPadrao, diretorioBackup);

            //Modificação de Dados Fiscais
            ModificacaoFiscal(diretorioPadrao, diretorioBackup);

            //Modificação de Dados Bancários
            ModificacaoBancario(diretorioPadrao, diretorioBackup);

            //Modificação de Dados de Contato
            ModificacaoContato(diretorioPadrao, diretorioBackup);

            //Bloqueio
            BloqueioFornecedor(diretorioPadrao, diretorioBackup);

            //Desbloqueio
            DesbloqueioFornecedor(diretorioPadrao, diretorioBackup);
        }

        #region Execução Retorno Carga
        public static void DesbloqueioFornecedor(string diretorioPadrao, string diretorioBackup)
        {
            foreach (string arquivoDesbloqueio in Directory.EnumerateFiles(diretorioPadrao, "*_RET_PJPF_Desbloqueio.txt"))
            {
                string linha;
                var arquivo = new StreamReader(arquivoDesbloqueio);
                while ((linha = arquivo.ReadLine()) != null)
                {
                    string[] words = linha.Split(';');
                    var linhaNova = MapearRetornoModificacaoDesbloqueio(words);

                    FinalizarModificacaoRetorno(linhaNova);
                }
                BackupDeleteArquivoRetorno(diretorioBackup, arquivoDesbloqueio, arquivo);
            }
        }
               
        public static void BloqueioFornecedor(string diretorioPadrao, string diretorioBackup)
        {
            foreach (string arquivoBloqueio in Directory.EnumerateFiles(diretorioPadrao, "*_RET_PJPF_Bloqueio.txt"))
            {
                string linha;
                var arquivo = new StreamReader(arquivoBloqueio);
                while ((linha = arquivo.ReadLine()) != null)
                {
                    string[] words = linha.Split(';');
                    var linhaNova = MapearRetornoModificacaoBloqueio(words);

                    FinalizarModificacaoRetorno(linhaNova);
                }
                BackupDeleteArquivoRetorno(diretorioBackup, arquivoBloqueio, arquivo);
            }
        }
               
        public static void ModificacaoContato(string diretorioPadrao, string diretorioBackup)
        {
            foreach (string arquivoModificacaoContato in Directory.EnumerateFiles(diretorioPadrao, "*_RET_PJPF_Mod_Contato.txt"))
            {
                string linha;
                var arquivo = new StreamReader(arquivoModificacaoContato);
                while ((linha = arquivo.ReadLine()) != null)
                {
                    string[] words = linha.Split(';');
                    var linhaNova = MapearRetornoModificacaoContatos(words);
                    FinalizarModificacaoRetorno(linhaNova);
                }
                BackupDeleteArquivoRetorno(diretorioBackup, arquivoModificacaoContato, arquivo);
            }
        }
               
        public static void ModificacaoBancario(string diretorioPadrao, string diretorioBackup)
        {
            foreach (string arquivoModificacaoBancario in Directory.EnumerateFiles(diretorioPadrao, "*_RET_PJPF_Mod_Bancario.txt"))
            {
                string linha;
                var arquivo = new StreamReader(arquivoModificacaoBancario);
                while ((linha = arquivo.ReadLine()) != null)
                {
                    string[] words = linha.Split(';');
                    var linhaNova = MapearRetornoModificacaoBancario(words);
                    FinalizarModificacaoRetorno(linhaNova);
                }
                BackupDeleteArquivoRetorno(diretorioBackup, arquivoModificacaoBancario, arquivo);
            }
        }
               
        public static void ModificacaoFiscal(string diretorioPadrao, string diretorioBackup)
        {
            foreach (string arquivoModificacaoFiscal in Directory.EnumerateFiles(diretorioPadrao, "*_RET_PJPF_Mod_Fiscal.txt"))
            {
                string linha;
                var arquivo = new StreamReader(arquivoModificacaoFiscal);
                while ((linha = arquivo.ReadLine()) != null)
                {
                    string[] words = linha.Split(';');
                    var linhaNova = MapearRetornoModificacaoFiscal(words);
                    FinalizarModificacaoRetorno(linhaNova);
                }
                BackupDeleteArquivoRetorno(diretorioBackup, arquivoModificacaoFiscal, arquivo);
            }
        }
               
        public static void AmpliacaoFornecedores(string diretorioPadrao, string diretorioBackup)
        {
            foreach (string arquivoAmpliacaoFornecedor in Directory.EnumerateFiles(diretorioPadrao, "*_RET_PJPF_Ampliacao.txt"))
            {
                string linha;
                var arquivo = new StreamReader(arquivoAmpliacaoFornecedor);
                while ((linha = arquivo.ReadLine()) != null)
                {
                    string[] words = linha.Split(';');
                    var linhaNova = MapearRetornoAmpliacaoFornecedores(words);

                    if (linhaNova.CodigoRetorno == 0)
                    {
                        FinalizarSolicitacaoRetorno(linhaNova);
                    }
                }
                BackupDeleteArquivoRetorno(diretorioBackup, arquivoAmpliacaoFornecedor, arquivo);
            }
        }
               
        public static void CriacaoFornecedores(string diretorioPadrao, string diretorioBackup)
        {
            foreach (var arquivoCriacaoFornecedores in Directory.EnumerateFiles(diretorioPadrao, "*_RET_PJPF_Criacao.txt"))
            {
                string linha;
                var arquivo = new StreamReader(arquivoCriacaoFornecedores);
                while ((linha = arquivo.ReadLine()) != null)
                {
                    string[] words = linha.Split(';');
                    var linhaNova = MapearRetornoCriacaoFornecedores(words);

                    if (linhaNova.CodigoRetorno == 0)
                    {
                        FinalizarSolicitacaoRetorno(linhaNova);
                    }
                }
                BackupDeleteArquivoRetorno(diretorioBackup, arquivoCriacaoFornecedores, arquivo);
            }
        }
        #endregion

        #region CriacaoCarga
        public  static void CriacaoCargaSap(this CargaController controller, int? idSolicitacao)
        {
            var lstWfdSolicitacao = idSolicitacao == null
                ? controller.solicitacaoBP.ListarSolicitacaoAprovadaPorId()
                : controller.solicitacaoBP.ListarTodasSolicitacoesAprovadas((int)idSolicitacao);

            if (lstWfdSolicitacao.Count == 0)
                throw new WebForLinkException("A Lista de Solicitações retornou vazia.");

            var geradorArquivo = new DemandaSapModel();
            var dadosCriacaoFornecedorList = new List<string>();
            //TODO: Retirado pois nunca é usado
            //var dadosCriacaoFornecedorList2 = "CHAMADA PRA BP";
            var dadosAmpliacaoFornecedorList = new List<string>();
            var dadosContatosList = new List<string>();
            var dadosBancosList = new List<string>();
            var dadosFiscaisList = new List<string>();

            foreach (var item in lstWfdSolicitacao)
            {
                if (item.WFL_FLUXO == null)
                    throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Fluxo da solicitação Nº{0} está nulo.", item.ID)));


                switch (item.WFL_FLUXO.FLUXO_TP_ID)
                {
                    case (int)EnumTiposFluxo.CadastroFornecedorNacional:
                        CriarCargaCadastroFornecedorNacional(item, dadosCriacaoFornecedorList);
                        break;
                    case (int)EnumTiposFluxo.CadastroFornecedorNacionalDireto:
                        CriarCargaAmpliacaoFornecedor(controller, item, dadosAmpliacaoFornecedorList);
                        break;
                    //case (int)EnumTiposFluxo.CadastroFornecedorPF:
                    //case (int)EnumTiposFluxo.CadastroFornecedorPFDireto:
                    //    CriarCargaCadastroFornecedorPF(item, dadosCriacaoFornecedorList);
                    //break;
                    case (int)EnumTiposFluxo.CadastroFornecedorEstrangeiro:
                        CriarCargaCadastroFornecedorEstrangeiro(item, dadosCriacaoFornecedorList);
                        break;
                    //case (int)EnumTiposFluxo.AmpliacaoFornecedor:
                    //    break;
                    case (int)EnumTiposFluxo.ModificacaoDadosBancarios:
                        CriarCargaModificacaoDadosBancarios(controller, item, dadosBancosList);
                        break;
                    case (int)EnumTiposFluxo.ModificacaoDadosContato:
                        CriarCargaModificacaoDadosContato(controller, item, dadosContatosList);
                        break;
                    case (int)EnumTiposFluxo.BloqueioFornecedor:	//Bloqueio de Fornecedor
                        #region Bloqueio de Fornecedor
                        SOLICITACAO_BLOQUEIO solBloqueio = item.WFD_SOL_BLOQ.Where(x => x.SOLICITACAO_ID == item.ID).FirstOrDefault();
                        BloqueioCargaModel dadosBloqueio = new BloqueioCargaModel
                        {
                            CodigoSolicitacao = item.ID.ToString(),
                            Empresa = item.PJPF_ID.ToString(),
                            OrganizacaoCompras = item.CONTRATANTE_ID.ToString(),
                            CodigoSAP = null,
                            BloqueioEmpresaSelecionada = null,//TODO: Tirar Duvida com o César via e-mail
                            TodasEmpresas = solBloqueio.BLQ_LANCAMENTO_TODAS_EMP.Value == true ? "X" : null,
                            TodasOrganizacoesCompras = solBloqueio.BLQ_COMPRAS_TODAS_ORG_COMPRAS.Value == true ? "X" : null,
                            FuncaoBloqueio = solBloqueio.WFD_T_FUNCAO_BLOQUEIO.FUNCAO_BLOQ_COD,
                        };
                        #endregion
                        break;
                    case (int)EnumTiposFluxo.DesbloqueioFornecedor:	//Desbloqueio de Fornecedor
                        #region Desbloqueio de Fornecedor
                        SOLICITACAO_BLOQUEIO solDesbloqueio = item.WFD_SOL_BLOQ.Where(x => x.SOLICITACAO_ID == item.ID).FirstOrDefault();
                        DesbloqueioCargaModel dadosDesbloqueio = new DesbloqueioCargaModel()
                        {
                            CodigoSolicitacao = item.ID.ToString(),
                            Empresa = item.PJPF_ID.ToString(),
                            OrganizacaoCompras = item.CONTRATANTE_ID.ToString(),
                            CodigoSAP = null,
                            BloqueioEmpresaSelecionada = null,
                            TodasEmpresas = solDesbloqueio.BLQ_LANCAMENTO_TODAS_EMP.Value == true ? "X" : null,
                            TodasOrganizacoesCompras = solDesbloqueio.BLQ_COMPRAS_TODAS_ORG_COMPRAS.Value == true ? "X" : null,
                            FuncaoBloqueio = solDesbloqueio.WFD_T_FUNCAO_BLOQUEIO.ID,
                        };
                        #endregion
                        break;
                        //case 5:
                        //    CriarCargaModificacaoDadosFiscais(item, dadosFiscaisList);
                        //    break;
                        #region Modificações de Dados Gerais
                        /*
                        case 4:	//Modificações de Dados Gerais
                            List<WFD_SOL_MOD_DGERAIS_SEQ> solDadosGerais = item.WFD_SOL_MOD_DGERAIS_SEQ.Where(x => x.SOLICITACAO_ID == item.ID).ToList();
                            FornecedorViewModel dadosGerais = new FornecedorViewModel()
                            {
                                CodigoSolicitacao = item.ID.ToString(),
                                Empresa = fornecedor.NOME,
                                GrupoContas = "",
                                OrganizacaoCompras = "",
                                SimplesNacional = "",
                                Nome1 = fornecedor.RAZAO_SOCIAL,
                                NomeFantasia = fornecedor.NOME_FANTASIA,
                                CEP = fornecedor.CEP,
                                Cidade = fornecedor.CIDADE,
                                TipoLogradouro = "",
                                Rua = fornecedor.ENDERECO,
                                Numero = fornecedor.NUMERO,
                                Complemento = fornecedor.COMPLEMENTO,
                                Bairro = fornecedor.BAIRRO,
                                Estado = "",
                                Telefone = fornecedor.CNTT_TEL,
                                EnderecoMail = fornecedor.CNTT_EMAIL,
                                TelefoneCelular = fornecedor.CNTT_CEL,
                                Cliente = "",
                                GrupoEmpresas = "",
                                CNPJ = fornecedor.CNPJ,
                                CPF = fornecedor.CPF,
                                InscricaoEstadual = fornecedor.INSCR_ESTADUAL,
                                InscricaoMunicipal = fornecedor.INSCR_MUNICIPAL,
                                Banco = "",
                                Agencia = fornecedor.AGENCIA,
                                CodigoAgencia = fornecedor.AG_DV,
                                ContaCorrente = fornecedor.CONTA,
                                DVContaCorrente = fornecedor.CONTA_DV,
                                ChaveOrdenacao = "",
                                NomeContatoVendedor = fornecedor.CNTT_NM,
                                TelefoneVendedor = fornecedor.CNTT_TEL,
                                Pais = "",
                            };
                            break;
     */

                        #endregion
                }
            }
            //Criação de Fornecedor Nacional
            if (dadosCriacaoFornecedorList.Count != 0)
                geradorArquivo.GerarArquivo(dadosCriacaoFornecedorList, "_PJPF_Criacao");
            //Ampliação de Fornecedor
            if (dadosAmpliacaoFornecedorList.Count != 0)
                geradorArquivo.GerarArquivo(dadosAmpliacaoFornecedorList, "_PJPF_Ampliacao");
            //Modificação Dados Contatos
            if (dadosContatosList.Count != 0)
                geradorArquivo.GerarArquivo(dadosContatosList, "_PJPF_mod_Contato");
            //Modificação Dados Bancários
            if (dadosBancosList.Count != 0)
                geradorArquivo.GerarArquivo(dadosBancosList, "_PJPF_mod_Bancario");
            //Modificação Dados Fiscais
            if (dadosFiscaisList.Count != 0)
                geradorArquivo.GerarArquivo(dadosFiscaisList, "_PJPF_Mod_Fiscal");

            foreach (var item in lstWfdSolicitacao)
            {
                if (item.WFL_FLUXO == null)
                    throw new WebForLinkException("Existe um problema com os dados retornados da base.", new Exception(string.Format("Fluxo da solicitação Nº{0} está nulo.", item.ID)));

                Tramite.AtualizaTramite(item.CONTRATANTE_ID, item.ID, item.WFL_FLUXO.ID, 6, 2, null);
            }
        }
                
        private static void CriarCargaModificacaoDadosContato(this CargaController controller, SOLICITACAO item, List<string> dadosContatosList)
        {
            List<SOLICITACAO_MODIFICACAO_CONTATO> solDadosContatos =
                item.WFD_SOL_MOD_CONTATO.Where(x => x.SOLICITACAO_ID == item.ID).ToList();
            if (solDadosContatos.Count == 0)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", item.ID)));

            FORNECEDOR fornecedorContatos = controller.pjPfBP.Buscar(x => x.ID == item.PJPF_ID);
            if (fornecedorContatos == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("Fornecedor está nulo.{0}", item.ID)));

            WFD_CONTRATANTE_PJPF fornecedorNovoContatos = controller.contratantePJPFBP.BuscarPorPjPfId((int)item.PJPF_ID);
            if (fornecedorNovoContatos == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("O Fornecedor nacional Nº{0} está nulo.", item.ID)));

            CONTRATANTE empresaContatos = controller.contratanteBP.BuscarPorId(item.CONTRATANTE_ID); //EMPRESA -  CONTRANTE_COD_ERP
            if (empresaContatos == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("Empresa está nulo.{0}", item.ID)));

            FORNECEDOR_CATEGORIA categoriaContatos = controller.pjPfCategoriaBP.BuscarPorId((int)fornecedorNovoContatos.CATEGORIA_ID); //GRUPOCONTAS - CODIGo
            if (categoriaContatos == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("Categoria está nulo.{0}", item.ID)));

            CONTRATANTE_ORGANIZACAO_COMPRAS organizacaoContatos = controller.contratanteOrganizacaoComprasBP.BuscarPorContratanteId(item.CONTRATANTE_ID); //OrganizacaoCompras - ORG_COMPRAS_COD
            if (organizacaoContatos == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("Organização está nulo.{0}", item.ID)));

            foreach (var solContato in solDadosContatos)
            {
                DadosContatoCargaModel dadosContato = new DadosContatoCargaModel
                {
                    CodigoSAP = ValidaVazio(item.ID.ToString()),
                    CodigoSolicitacao = ValidaVazio(empresaContatos.CONTRANTE_COD_ERP),
                    Empresa = ValidaVazio(categoriaContatos.CODIGO),
                    OrganizacaoCompras = ValidaVazio(organizacaoContatos.ORG_COMPRAS_COD),
                    Nome = solContato.NOME,
                    EMail = solContato.EMAIL,
                    Telefone = solContato.TELEFONE,
                    Celular = solContato.CELULAR,
                };
                dadosContatosList.Add(dadosContato.GerarLinhaModificarDadosContato());
            }
        }
                
        private static void CriarCargaModificacaoDadosBancarios(this CargaController controller, SOLICITACAO item, List<string> dadosBancosList)
        {
            List<SOLICITACAO_MODIFICACAO_BANCO> solBancario = item.WFD_SOL_MOD_BANCO
                .Where(x => x.SOLICITACAO_ID == item.ID).ToList();
            if (solBancario.Count == 0)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", item.ID)));

            FORNECEDOR fornecedorBancario = controller.pjPfBP.BuscarPorId((int)item.PJPF_ID);
            if (fornecedorBancario == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("Fornecedor está nulo.{0}", item.ID)));

            WFD_CONTRATANTE_PJPF fornecedorNovoBancario = controller.contratantePJPFBP.BuscarPorID((int)item.PJPF_ID);
            if (fornecedorNovoBancario == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("O Fornecedor nacional Nº{0} está nulo.", item.ID)));

            CONTRATANTE empresaBancario = controller.contratanteBP.BuscarPorId(item.CONTRATANTE_ID);
            if (empresaBancario == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("Empresa está nulo.{0}", item.ID)));

            FORNECEDOR_CATEGORIA categoriaBancario = controller.pjPfCategoriaBP.BuscarPorId((int)fornecedorNovoBancario.CATEGORIA_ID);
            if (categoriaBancario == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("Categoria está nulo.{0}", item.ID)));

            CONTRATANTE_ORGANIZACAO_COMPRAS organizacaoBancario = controller.contratanteOrganizacaoComprasBP.BuscarPorContratanteId(item.CONTRATANTE_ID);
            if (organizacaoBancario == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("Organização está nulo.{0}", item.ID)));

            DadosBancariosCargaModel dadosBancarios = MapearModificacaoBancoComSolicitacaoDadosBancario(item.ID,
                empresaBancario.CONTRANTE_COD_ERP, categoriaBancario.CODIGO, organizacaoBancario.ORG_COMPRAS_COD, solBancario);
            dadosBancosList.Add(dadosBancarios.GerarLinhaModificarDadosBancarios());
        }
                
        private static void CriarCargaModificacaoDadosFiscais(SOLICITACAO item, List<string> dadosFiscaisList)
        {
            //List<WFD_SOL_MOD_DFICAIS_SEQ> solDadosFiscais = item.WFD_SOL_MOD_DFICAIS_SEQ
            //    .Where(x => x.SOLICITACAO_ID == item.ID).ToList();
            //if (solDadosFiscais.Count == 0)
            //    throw new WebForDropException("Existe um problema com os dados retornados da base.",
            //        new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", item.ID)));

            //WFD_PJPF fornecedorFiscais =
            //    BuscarFornecedorPorId(item.PJPF_ID);
            //if (fornecedorFiscais == null)
            //    throw new WebForDropException("Existe um problema com os dados retornados da base.",
            //        new Exception(string.Format("Fornecedor está nulo.{0}", item.ID)));

            //WFD_CONTRATANTE_PJPF fornecedorNovoFiscais =
            //    BuscarContratanteFornecedorPorFornecedorId(item.PJPF_ID);
            //if (fornecedorNovoFiscais == null)
            //    throw new WebForDropException("Existe um problema com os dados retornados da base.",
            //        new Exception(string.Format("O Fornecedor nacional Nº{0} está nulo.", item.ID)));

            //WFD_CONTRATANTE empresaFiscais =
            //    BuscarContrantantePorId(item.CONTRATANTE_ID);
            //if (empresaFiscais == null)
            //    throw new WebForDropException("Existe um problema com os dados retornados da base.",
            //        new Exception(string.Format("Empresa está nulo.{0}", item.ID)));

            //WFD_PJPF_CATEGORIA categoriaFiscais =
            //    BuscarFornecedorCategoriaPorId(fornecedorNovoFiscais.CATEGORIA_ID);
            //if (categoriaFiscais == null)
            //    throw new WebForDropException("Existe um problema com os dados retornados da base.",
            //        new Exception(string.Format("Categoria está nulo.{0}", item.ID)));

            //WFD_CONTRATANTE_ORG_COMPRAS organizacaoFiscais =
            //    BuscarContratanteOrgComprasPorContratanteId(item.CONTRATANTE_ID);
            //if (organizacaoFiscais == null)
            //    throw new WebForDropException("Existe um problema com os dados retornados da base.",
            //        new Exception(string.Format("Organização está nulo.{0}", item.ID)));

            //foreach (var solFiscal in solDadosFiscais)
            //{
            //    DadosFiscaisCargaModel dadosFiscal = new DadosFiscaisCargaModel
            //    {
            //        CodigoSAP = ValidaVazio(item.ID.ToString()),
            //        CodigoSolicitacao = ValidaVazio(empresaFiscais.CONTRANTE_COD_ERP),
            //        Empresa = ValidaVazio(categoriaFiscais.CODIGO),
            //        OrganizacaoCompras = ValidaVazio(organizacaoFiscais.ORG_COMPRAS_COD),
            //        Categoria = solFiscal.CATEG_IRF_ID.ToString(),
            //        Codigo = solFiscal.ID.ToString(),
            //        SujeitoA = solFiscal.SUJEITO_A ? "X" : string.Empty
            //    };
            //    dadosFiscaisList.Add(dadosFiscal.GerarLinhaModificarDadosFiscais());
            //}
        }
                
        private static void CriarCargaAmpliacaoFornecedor(this CargaController controller, SOLICITACAO item, List<string> dadosAmpliacaoFornecedorList)
        {
            SOLICITACAO_CADASTRO_FORNECEDOR fornecedorAmpliado = item.WFD_SOL_CAD_PJPF
                .FirstOrDefault(x => x.SOLICITACAO_ID == item.ID);
            if (fornecedorAmpliado == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", item.ID)));

            FornecedorCargaModel ampliacaoFornecedor = controller.MapearAmpliacaoFornecedorComSolicitacao(item, fornecedorAmpliado);
            dadosAmpliacaoFornecedorList.Add(ampliacaoFornecedor.GerarLinhaCargaAmpliarFornecedor());
        }
                
        private static void CriarCargaCadastroFornecedorEstrangeiro(SOLICITACAO item, List<string> dadosCriacaoFornecedorList)
        {
            SOLICITACAO_CADASTRO_FORNECEDOR fornecedorEstrangeiro = item.WFD_SOL_CAD_PJPF
                .FirstOrDefault(x => x.SOLICITACAO_ID == item.ID);
            if (fornecedorEstrangeiro == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", item.ID)));

            FornecedorCargaModel dadosFornecedorEstrangeiro = MapearFornecedorEstrangeiroComSolicitacao(item,
                fornecedorEstrangeiro);
            dadosCriacaoFornecedorList.Add(dadosFornecedorEstrangeiro.GerarLinhaCargaCriarFornecedor());
        }
                
        private static void CriarCargaCadastroFornecedorNacional(SOLICITACAO item, List<string> dadosCriacaoFornecedorList)
        {
            SOLICITACAO_CADASTRO_FORNECEDOR fornecedorNacional = item.WFD_SOL_CAD_PJPF
                .FirstOrDefault(x => x.SOLICITACAO_ID == item.ID);
            if (fornecedorNacional == null)
                throw new WebForLinkException("Existe um problema com os dados retornados da base.",
                    new Exception(string.Format("O Fornecedor da solicitação Nº{0} está nulo.", item.ID)));
            FornecedorCargaModel dadosFornecedor = MapearFornecedorNacionalComSolicitacao(item, fornecedorNacional);
            dadosCriacaoFornecedorList.Add(dadosFornecedor.GerarLinhaCargaCriarFornecedor());
        }
        #endregion
    }
}