using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers.Extensoes
{
    public static class ImportacaoControllerExtensions
    {
        #region Constantes
        private const int LINHA_CABECALHO = 1;
        private const int LINHA_INICIO = 2;
        private const int PAGINA_DEFAULT = 1;
        private const int ITENS_POR_PAGINA = 10;

        private const string TEXT_CENTER = "text-center";
        private const string TEXT_LEFT = "text-left";
        private const string TEXT_RIGHT = "text-right";
        private const string TEXT_DANGER_TEXT_CENTER = "text-danger text-center";
        private const string TEXT_DANGER_TEXT_LEFT = "text-danger text-left";
        private const string TEXT_DANGER_TEXT_RIGHT = "text-danger text-right";
        private const string DANGER_TEXT_CENTER = "danger text-center";
        #endregion

        public static void AtivarExecucaoRobo(this ImportacaoController controller, int[] selecionados)
        {
            controller._fornecedorBaseService.AtivarExecucaoRobo(selecionados);
        }

        public static void MapearFiltro(this ImportacaoController controller, FornecedorBaseListaVM model, ref ImportacaoFornecedoresFiltrosDTO filtro)
        {
            if (model.Filtro != null)
            {
                model.Filtro.ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

                FornecedorBaseFiltroVM filtroVM = model.Filtro;

                filtro = Mapper.Map<FornecedorBaseFiltroVM, ImportacaoFornecedoresFiltrosDTO>(filtroVM);

                if (!string.IsNullOrEmpty(filtro.CNPJ))
                    filtro.CNPJ = Mascara.RemoverMascara(filtro.CNPJ);

                if (!string.IsNullOrEmpty(filtro.CPF))
                    filtro.CPF = Mascara.RemoverMascara(filtro.CPF);
            }
            else
                filtro = new ImportacaoFornecedoresFiltrosDTO { ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId") };
        }

        public static void AplicarMascaras(this ImportacaoController controller, ref FornecedorBaseVM fornecedorBase)
        {
            if (!string.IsNullOrEmpty(fornecedorBase.CNPJ))
                fornecedorBase.CNPJ = Mascara.MascararCNPJouCPF(fornecedorBase.CNPJ);

            if (!string.IsNullOrEmpty(fornecedorBase.CPF))
                fornecedorBase.CPF = Mascara.MascararCNPJouCPF(fornecedorBase.CPF);
        }

        public static void RemoverMascaras(this ImportacaoController controller, ref FORNECEDORBASE fornecedorBase)
        {
            if (!string.IsNullOrEmpty(fornecedorBase.CNPJ))
                fornecedorBase.CNPJ = Mascara.RemoverMascara(fornecedorBase.CNPJ);

            if (!string.IsNullOrEmpty(fornecedorBase.CPF))
                fornecedorBase.CPF = Mascara.RemoverMascara(fornecedorBase.CPF);
        }

        public static void Preenchermodelo(this ImportacaoController controller, int contratanteId, FornecedorBaseListaVM model, CONTRATANTE_CONFIGURACAO_EMAIL configEmail, string stData)
        {
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            Contratante contratante = controller._contratanteService.BuscarPorId(contratanteId);
            Usuario usuario = controller._usuarioService.BuscarPorId(usuarioId);

            model.Arquivos = Mapper.Map<List<SelectListItem>>(controller._fornecedorBaseImportacaoService.ListarTodas(contratanteId));
            model.Arquivos.Insert(0, new SelectListItem { Text = "Todas", Value = null });
            string mensagem = configEmail.CORPO
                    .Replace("^NomeEmpresa^", contratante.NOME_FANTASIA ?? contratante.RAZAO_SOCIAL)
                    .Replace("^NomeUsuario^", usuario.NOME)
                    .Replace("^FixoUsuario1^", usuario.FIXO)
                    .Replace("^CelularUsuario1^", usuario.CELULAR)
                    .Replace("^EmailUsuario^", usuario.EMAIL);

            model.MensagemImportacao = new MensagemImportacaoVM(configEmail.ASSUNTO, mensagem);

            model.ProrrogacaoPrazo = new ProrrogacaoPrazoVM()
            {
                StDataProrrogacao = stData
            };
            model.AprovacaoProrrogacao = new AprovacaoPrazoVM(new SolicitacaoFornecedoresVM());
            model.ReprovacaoProrrogacao = new ReprovacaoPrazoVM()
            {
                Fornecedores = new List<SolicitacaoFornecedoresVM>()
                                {
                                    new SolicitacaoFornecedoresVM()
                                },
                Fornecedor = new SolicitacaoFornecedoresVM(),
            };
        }

        public static void ManipularFiltroEspecifico(this ImportacaoController controller, EnumTiposFuncionalidade funcionalidade, ref ImportacaoFornecedoresFiltrosDTO filtro)
        {
            switch (funcionalidade)
            {
                case EnumTiposFuncionalidade.ValidarEmOrgaosPublicos:
                    if (!filtro.Validados.HasValue) filtro.Validados = false; break;
                case EnumTiposFuncionalidade.Categorizar:
                    if (!filtro.Categorizados.HasValue) filtro.Categorizados = false; break;
                case EnumTiposFuncionalidade.Convidar:
                    if (!filtro.Convidados.HasValue) filtro.Convidados = false; break;
                case EnumTiposFuncionalidade.ProrrogarPrazo:
                    if (!filtro.Prorrogados.HasValue) filtro.Prorrogados = false; break;
                case EnumTiposFuncionalidade.GerarCarga:
                    if (!filtro.Gerados.HasValue) filtro.Gerados = false; break;
                case EnumTiposFuncionalidade.CompletarDados:
                    if (!filtro.Completos.HasValue) filtro.Completos = false; break;
                case EnumTiposFuncionalidade.AprovarPrazo:
                    if (!filtro.Aprovados.HasValue) filtro.Aprovados = 1; break;
                case EnumTiposFuncionalidade.Bloquear:
                    if (!filtro.Bloqueados.HasValue) filtro.Bloqueados = 0; break;
            }
        }

        public static void AplicarValores(this ImportacaoController controller, ref FornecedorBaseListaVM model)
        {
            model.NomeColunas = new List<string>();
            switch (model.TipoFuncionalidade)
            {
                case EnumTiposFuncionalidade.ValidarEmOrgaosPublicos:
                    #region Validar Em Órgãos Públicos
                    model.NomeFuncionalidade = "Validar";
                    model.NomeColunas.Add("Órgãos Públicos");
                    model.FornecedoresBaseFuncionalidade.Select(x =>
                    {
                        x.Colunas = new List<ColunaOpcionalVM>
                        {
                            new ColunaOpcionalVM
                            {
                                CSS = (x.ExecutaRobo.HasValue) ? TEXT_CENTER : DANGER_TEXT_CENTER,
                                Titulo = (x.ExecutaRobo.HasValue) ? string.Empty : "Validação Inativa!",
                                Valor = (x.ExecutaRobo.HasValue) ? "Ativa" : "-"
                            }
                        };
                        return x;
                    }).ToList();
                    #endregion
                    break;

                case EnumTiposFuncionalidade.Categorizar:
                    #region Categorizar
                    model.NomeFuncionalidade = "Categorizar";
                    model.NomeColunas.Add("Categoria");

                    model.FornecedoresBaseFuncionalidade.Select(x =>
                    {
                        x.Colunas = new List<ColunaOpcionalVM>
                        {
                            new ColunaOpcionalVM
                            {
                                CSS = (x.CategoriaId.HasValue) ? TEXT_CENTER : DANGER_TEXT_CENTER,
                                Titulo = (x.CategoriaId.HasValue) ? string.Empty : "Sem Categoria!",
                                Valor =  (x.CategoriaId.HasValue) ? x.CategoriaNome : "-"
                            }
                        };
                        return x;
                    }).ToList();
                    #endregion
                    break;

                case EnumTiposFuncionalidade.ProrrogarPrazo:
                    #region Prorrogar Prazo
                    model.NomeFuncionalidade = "Prorrogar";
                    model.NomeColunas.Add("Prorrogado");
                    model.FornecedoresBaseFuncionalidade.Select(x =>
                    {
                        x.Colunas = new List<ColunaOpcionalVM>
                        {
                            new ColunaOpcionalVM
                            {
                                CSS = (x.Prorrogado) ? TEXT_CENTER : DANGER_TEXT_CENTER,
                                Titulo = (x.Prorrogado) ? string.Empty : "Sem Prorrogação!",
                                Valor = (x.Prorrogado) ? "Prorrogado" : "-"
                            },
                        };
                        return x;
                    }).ToList();
                    #endregion
                    break;

                case EnumTiposFuncionalidade.Convidar:
                    #region Convidar
                    model.NomeFuncionalidade = "Convidar";
                    model.NomeColunas.Add("Convite");
                    model.NomeColunas.Add("Respondido");

                    model.FornecedoresBaseFuncionalidade.Select(x =>
                    {
                        x.Colunas = new List<ColunaOpcionalVM>
                        {
                            new ColunaOpcionalVM
                            {
                                CSS = (x.Convidado) ? TEXT_CENTER : DANGER_TEXT_CENTER,
                                Titulo = (x.Convidado) ? string.Empty : "Sem Convite!",
                                Valor = (x.Convidado) ? "Convidado" : "-"
                            },
                            new ColunaOpcionalVM
                            {
                                CSS = (x.Respondido==true) ? TEXT_CENTER : DANGER_TEXT_CENTER,
                                Titulo = (x.Respondido==true) ? string.Empty : "Sem Resposta!",
                                Valor = (x.Respondido != null)
                                ? (x.Respondido == true)
                                    ? "Sim"
                                    :"Não"
                                : "-"
                            }
                        };
                        return x;
                    }).ToList();
                    #endregion
                    break;

                case EnumTiposFuncionalidade.AprovarPrazo:
                    #region Aprovar Prazo
                    model.NomeFuncionalidade = "Aprovar";
                    model.NomeColunas.Add("Prorrogar para");
                    model.NomeColunas.Add("Motivo");
                    //model.NomeColunas.Add("Aprovado");

                    model.FornecedoresBaseFuncionalidade.Select(x =>
                    {
                        x.Colunas = new List<ColunaOpcionalVM>
                        {
                            new ColunaOpcionalVM
                            {
                                CSS = TEXT_CENTER,
                                Titulo = "Teste",
                                Valor = x.ProrrogarPara
                            },
                            new ColunaOpcionalVM
                            {
                                CSS = TEXT_CENTER,
                                Titulo = "Teste2",
                                Valor = x.Motivo
                            },
                            //new ColunaOpcionalVM
                            //{
                            //    CSS = (x.Aprovado == true) ? TEXT_CENTER : DANGER_TEXT_CENTER,
                            //    Titulo = (x.Aprovado == true) ? string.Empty : "Sem Aprovação!",
                            //    Valor = (x.Aprovado != null)
                            //    ? (x.Aprovado == true) 
                            //        ? "Sim"
                            //        :"Não"
                            //    : "-"
                            //},
                        };
                        return x;

                    }).ToList();
                    #endregion
                    break;

                case EnumTiposFuncionalidade.Bloquear:
                    #region Bloquear
                    model.NomeFuncionalidade = "Bloquear";
                    model.NomeColunas.Add("Bloqueado");

                    model.FornecedoresBaseFuncionalidade.Select(x =>
                    {
                        x.Colunas = new List<ColunaOpcionalVM>
                        {
                            new ColunaOpcionalVM
                            {
                                CSS = (x.Bloqueado == true) ? TEXT_CENTER : DANGER_TEXT_CENTER,
                                Titulo = (x.Bloqueado == true) ? string.Empty : "Sem Bloqueio!",
                                Valor = (x.Bloqueado != null)
                                ? (x.Bloqueado == true)
                                    ? "Sim"
                                    :"Não"
                                : "-"
                            },
                        };
                        return x;

                    }).ToList();
                    #endregion
                    break;
            }
        }

        public static void Categorizar(this ImportacaoController controller, int[] selecionados, int categoriaId)
        {
            controller._fornecedorBaseService.CategorizarFornecedores(selecionados, categoriaId);
        }

        public static void Convidar(this ImportacaoController controller, List<int> selecionados, string mensagem, string assunto, int idUsuario)
        {
            try
            {
                selecionados.ForEach(x =>
                {
                    var convite = controller._importacaoService.ImportarComConvite(x, idUsuario, assunto, mensagem);

                    //se não for primeiro acesso enviar para tela de acesso
                    string url = controller.Url.Action("Index", "Home", new
                    {
                        chaveurl = controller.Cripto.Criptografar(string.Format("SolicitacaoID={0}&Login={1}&TravaLogin=1", convite.SolicitacaoId, convite.Cnpj), controller._metodosGerais.ValorKey())
                    }, controller.Request.Url.Scheme);

                    //se for primeiro acesso enviar para criação de usuário
                    #region BuscarPorEmail
                    //validar se o e-mail já existe na tabela de Usuarios
                    if (!controller._usuarioService.ValidarPorCnpj(convite.Cnpj))
                    {
                        url = controller.Url.Action("CadastrarUsuarioFornecedor", "Home", new
                        {
                            chaveurl = controller.Cripto.Criptografar(string.Format("Login={0}&SolicitacaoID={1}&Email={2}",
                            convite.Cnpj,
                            convite.SolicitacaoId,
                            convite.Email), controller._metodosGerais.ValorKey())
                        }, controller.Request.Url.Scheme);
                    }
                    #endregion

                    string mensagemLink = string.Concat(mensagem, "<p><a href='", url, "'>Link</a>:", url, "</p>");
                    bool emailEnviadoComSucesso = controller._metodosGerais.EnviarEmail(convite.Email, assunto, mensagemLink);

                    var tpFluxo = convite.TipoFornecedor == 1 ? 10 : 30;
                    var fluxoId = controller._fluxoService.BuscarPorTipoEContratante(tpFluxo, convite.ContratanteId).ID;
                    int papelAtual = controller._papelService.BuscarPorContratanteETipoPapel(convite.ContratanteId, 10).ID;
                    int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
                    controller._tramite.AtualizarTramite(convite.ContratanteId, convite.SolicitacaoId, fluxoId, papelAtual, 2, usuarioId);
                });
            }
            catch (Exception ex)
            {
                controller.Log.Error(ex);
            }

        }

        public static void ProrrogarPrazo(this ImportacaoController controller, List<int> selecionados, string motivo, DateTime dataProrrogacao, int idUsuario)
        {
            try
            {
                selecionados.ForEach((Action<int>)(x =>
                {
                    controller._importacaoService.ProrrogarPrazo(x, idUsuario, dataProrrogacao, motivo);
                }));
            }
            catch (Exception ex)
            {
                controller.Log.Error(ex);
            }
        }

        public static void AvaliarPrazo(this ImportacaoController controller, List<int> selecionados, string motivo, int idUsuario, EnumTiposFuncionalidade avaliacao)
        {
            try
            {
                controller._importacaoService.AvaliarProrrogacao(selecionados.ToArray(), idUsuario, motivo, avaliacao);
            }
            catch (Exception ex)
            {
                controller.Log.Error(ex);
            }

        }

        public static void Bloquear(this ImportacaoController controller, List<int> selecionados, int escolha, int idUsuario, int contratanteId)
        {
            try
            {
                selecionados.ForEach(x =>
                {
                    var bloqueio = controller._fornecedorBaseService.ModificarStatusBloqueio(x, escolha, idUsuario, contratanteId);
                    if (escolha == 3)
                        controller._tramite.AtualizarTramite(bloqueio.ContratanteId, bloqueio.SolicitacaoId, bloqueio.FluxoId, bloqueio.PapelAtual, 2, bloqueio.UsuarioId);
                });

            }
            catch (Exception ex)
            {
                controller.Log.Error(ex);
            }
        }

        public static void GerenciarFornecedoresSelecionados(this ImportacaoController controller, List<int> selecionados)
        {
            if (controller.TempData["FornecedoresSelecionados"] == null)
            {
                controller.TempData["FornecedoresSelecionados"] = selecionados;
            }
            else
            {
                var selecionadosEmMemoria = controller.TempData["FornecedoresSelecionados"] as List<int>;
                var novos = selecionados.Where(x => !selecionadosEmMemoria.Contains(x)).ToList<int>();

                selecionadosEmMemoria.AddRange(novos);

                controller.TempData["FornecedoresSelecionados"] = selecionadosEmMemoria;
            }
        }

        public static void ValidarExtensaoArquivo(this ImportacaoController controller, HttpPostedFileBase arquivo)
        {
            string[] extensoesPermitidas = new string[] { ".xls", ".xlsx" };

            if (!extensoesPermitidas.Contains(Path.GetExtension(arquivo.FileName)))
                controller.ModelState.AddModelError("Arquivo", "Selecione um Arquivo com Extensão \".xls\" ou \".xlxs\".");
        }

        public static void ValidarCabecalhoArquivoImportado(this ImportacaoController controller, ExcelWorksheet worksheet)
        {
            string[] cabecalhoOriginal = Array.ConvertAll(ConfigurationManager.AppSettings["CabecalhoPlanilhaImportacaoFornecedor"].Split(','), x => x.TrimStart());

            int quantidadeColunas = cabecalhoOriginal.Length;
            string[] cabecalhoImportado = new string[quantidadeColunas];

            for (var i = 0; i < quantidadeColunas; i++)
            {
                cabecalhoImportado[i] = (string)worksheet.Cells[LINHA_CABECALHO, (i + 1)].Value;
            }

            if (!cabecalhoImportado.SequenceEqual(cabecalhoOriginal))
            {
                controller.ModelState.AddModelError("ImportacaoValidation", "O modelo do arquivo importado deve ser igual ao modelo disponibilizado.");
            }
        }

        public static List<LinhaPlanilhaModel> LerArquivo(this ImportacaoController controller, DadosImportacaoVM model, ExcelWorksheet worksheet)
        {
            List<LinhaPlanilhaModel> linhasArquivoImportado = new List<LinhaPlanilhaModel>();

            var colunas = System.Enum.GetValues(typeof(EnumColunasPlanilha)).Cast<int>().ToList();

            for (var linha = LINHA_INICIO; linha <= worksheet.Dimension.Rows; linha++)
            {
                var celulas = new List<CelulaPlanilhaModel>();

                colunas.ForEach(x =>
                {
                    var celula = worksheet.Cells[linha, x].Value;
                    celulas.Add(new CelulaPlanilhaModel
                    {
                        Linha = linha,
                        Coluna = x,
                        ValorOriginal = celula,
                        Endereco = ExcelAddress.GetAddress(linha, x),
                        ValorManipulado = celula != null ? celula.ToString() : null
                    });
                });

                controller.ValidarCelulas(celulas);

                var tipoErroLinhaPlanilha = controller.ValidarRegraCNPJouCPF(celulas);

                linhasArquivoImportado.Add(new LinhaPlanilhaModel()
                {
                    ExcelRow = worksheet.Row(linha),
                    Celulas = celulas,
                    Erro = tipoErroLinhaPlanilha
                });
            }

            return linhasArquivoImportado;
        }

        public static EnumTiposErroPlanilha ValidarRegraCNPJouCPF(this ImportacaoController controller, List<CelulaPlanilhaModel> celulas)
        {
            var tipoErroLinhaPlanilha = EnumTiposErroPlanilha.NaoAplicavel;

            var celula = celulas.Where(x => x.Coluna.Equals((int)EnumColunasPlanilha.CNPJouCPF)).FirstOrDefault();

            if (celula.Erro.Equals(EnumTiposErroPlanilha.SemCPFouCNPJ))
                return tipoErroLinhaPlanilha = EnumTiposErroPlanilha.SemCPFouCNPJ;

            if (celula.Erro.Equals(EnumTiposErroPlanilha.CNPJouCPFInvalido))
                return tipoErroLinhaPlanilha = EnumTiposErroPlanilha.CNPJouCPFInvalido;

            return tipoErroLinhaPlanilha;
        }

        public static void ValidarCelulas(this ImportacaoController controller, List<CelulaPlanilhaModel> celulas)
        {
            foreach (var celula in celulas)
            {
                switch ((EnumColunasPlanilha)celula.Coluna)
                {
                    case EnumColunasPlanilha.CNPJouCPF:
                        controller.ValidarCNPJouCPF(celula);
                        break;
                    case EnumColunasPlanilha.RazaoSocialOuNome:
                        controller.ValidarRazaoSocialOuNome(celula);
                        break;
                    case EnumColunasPlanilha.DataNascimento:
                        controller.ValidarDataNascimento(celula);
                        break;
                    case EnumColunasPlanilha.NomeContato:
                        break;
                    case EnumColunasPlanilha.Email:
                        controller.ValidarEmail(celula);
                        break;
                    case EnumColunasPlanilha.Telefone:
                    case EnumColunasPlanilha.Celular:
                        controller.ValidarTelefone(celula);
                        break;
                    case EnumColunasPlanilha.NovoFornecedor:
                        break;
                    case EnumColunasPlanilha.CodigoERP:
                        break;
                    case EnumColunasPlanilha.InscricaoEstadual:
                        break;
                }
            }
        }

        public static void ValidarCNPJouCPF(this ImportacaoController controller, CelulaPlanilhaModel celula)
        {
            if (string.IsNullOrEmpty(celula.ValorManipulado))
                celula.Erro = EnumTiposErroPlanilha.SemCPFouCNPJ;
            else
            {
                bool isCNPJValido = false;
                bool isCPFValido = false;

                if (!string.IsNullOrEmpty(celula.ValorManipulado))
                {
                    isCNPJValido = Validacao.ValidaCNPJ(celula.ValorManipulado);
                    isCPFValido = Validacao.ValidaCPF(celula.ValorManipulado);
                }

                if (isCNPJValido || isCPFValido)
                {
                    celula.ValorManipulado = Mascara.RemoverMascara(celula.ValorManipulado);
                    celula.ClasseCSS = TEXT_CENTER;
                }
                else
                {
                    celula.Erro = EnumTiposErroPlanilha.CNPJouCPFInvalido;
                    celula.ClasseCSS = TEXT_DANGER_TEXT_RIGHT;
                }
            }
        }

        public static void ValidarRazaoSocialOuNome(this ImportacaoController controller, CelulaPlanilhaModel celula)
        {
            if (string.IsNullOrEmpty(celula.ValorManipulado))
                celula.Erro = EnumTiposErroPlanilha.SemRazaoSocialOuNome;
        }

        public static void ValidarDataNascimento(this ImportacaoController controller, CelulaPlanilhaModel celula)
        {
            if (string.IsNullOrEmpty(celula.ValorManipulado))
                celula.Erro = EnumTiposErroPlanilha.SemDataNascimento;
            else if (!Validacao.ValidarData(celula.ValorManipulado))
            {
                celula.Erro = EnumTiposErroPlanilha.DataNascimentoInvalida;
                celula.ValorManipulado = string.Empty;
                celula.ClasseCSS = TEXT_DANGER_TEXT_RIGHT;
            }
            else
            {
                celula.ClasseCSS = TEXT_CENTER;
                celula.ValorOriginal = DateTime.Parse(celula.ValorOriginal.ToString()).ToString("dd/MM/yyyy");
            }
        }

        public static void ValidarNomeContato(this ImportacaoController controller, CelulaPlanilhaModel celula)
        {
            if (string.IsNullOrEmpty(celula.ValorManipulado))
                celula.Erro = EnumTiposErroPlanilha.SemNomeContato;
        }

        public static void ValidarEmail(this ImportacaoController controller, CelulaPlanilhaModel celula)
        {
            if (string.IsNullOrEmpty(celula.ValorManipulado))
                celula.Erro = EnumTiposErroPlanilha.SemEmail;
            else if (!Validacao.ValidarEmail(celula.ValorManipulado))
            {
                celula.Erro = EnumTiposErroPlanilha.EmailInvalido;
                celula.ValorManipulado = string.Empty;
                celula.ClasseCSS = TEXT_DANGER_TEXT_LEFT;
            }
        }

        public static void ValidarTelefone(this ImportacaoController controller, CelulaPlanilhaModel celula)
        {
            if (string.IsNullOrEmpty(celula.ValorManipulado))
                celula.Erro = EnumTiposErroPlanilha.SemTelefone;
            else
            {
                celula.ClasseCSS = TEXT_CENTER;
            }
        }

        public static void ValidarCelular(this ImportacaoController controller, CelulaPlanilhaModel celula)
        {
            if (string.IsNullOrEmpty(celula.ValorManipulado))
                celula.Erro = EnumTiposErroPlanilha.SemCelular;
            else
            {
                celula.ClasseCSS = TEXT_CENTER;
            }
        }

        public static void PersistirDadosEmMemoria(this ImportacaoController controller)
        {
            if (controller.TempData["Categorias"] == null)
            {
                int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
                List<FORNECEDOR_CATEGORIA> categorias = controller._fornecedorCategoriaService.BuscarCategorias(contratanteId).OrderBy(x => x.DESCRICAO).ToList();
                List<CategoriaVM> modelo = Mapper.Map<List<CategoriaVM>>(categorias, opt => opt.Items["Url"] = controller.Url);
                controller.TempData["Categorias"] = modelo;
            }

            controller.ViewBag.Categorias = controller.TempData["Categorias"];
            controller.TempData.Keep("Categorias");
        }
    }
}