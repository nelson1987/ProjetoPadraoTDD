using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.FiltrosDTO;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Application.Interfaces;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class FornecedoresDiretoController : ControllerPadrao
    {
        private readonly IBancoWebForLinkAppService _bancoService;
        private readonly ICadastroUnicoWebForLinkAppService _cadastroUnicoService;
        private readonly IFornecedorCategoriaWebForLinkAppService _categoriaDeFornecedorService;
        private readonly IContratanteConfiguracaoWebForLinkAppService _configuracaoDoContratanteService;
        private readonly IEnderecoWebForLinkAppService _enderecoService;
        private readonly IFornecedorArquivoWebForLinkAppService _fornecedorArquivoService;
        private readonly IInformacaoComplementarWebForLinkAppService _informacaoComplementarService;
        private readonly IPapelWebForLinkAppService _papelService;
        private readonly ISolicitacaoCadastroFornecedorWebForLinkAppService _solicitacaoCadastroFornecedorService;
        private readonly ISolicitacaoDocumentosFornecedorWebForLinkAppService _solicitacaoDocumentosFornecedorService;
        private readonly ISolicitacaoMaterialEServicoWebForLinkAppService _solicitacaoMaterialService;
        private readonly ISolicitacaoModificacaoBancoWebForLinkAppService _solicitacaoModificacaoDadosBancariosService;
        private readonly ISolicitacaoModificacaoContatoWebForLinkAppService _solicitacaoModificacaoDadosContatoService;
        private readonly ISolicitacaoModificacaoEnderecoWebForLinkAppService _solicitacaoModificacaoEnderecoService;
        private readonly ISolicitacaoWebForLinkAppService _solicitacaoService;
        private readonly ITramiteWebForLinkAppService _tramite;
        private readonly IUsuarioWebForLinkAppService _usuarioService;
        private readonly IServicosMateriaisWebForLinkAppService _unspscBP;
        public FornecedoresDiretoController(
            ISolicitacaoWebForLinkAppService solicitacao,
            ISolicitacaoCadastroFornecedorWebForLinkAppService solicitacaoCadastroFornecedor,
            IUsuarioWebForLinkAppService usuario,
            IBancoWebForLinkAppService banco,
            IEnderecoWebForLinkAppService endereco,
            IContratanteConfiguracaoWebForLinkAppService contratanteConfiguracao,
            IFornecedorArquivoWebForLinkAppService fornecedorArquivo,
            IPapelWebForLinkAppService papel,
            ICadastroUnicoWebForLinkAppService cadastroUnico,
            IFornecedorCategoriaWebForLinkAppService fornecedorCategoria,
            IInformacaoComplementarWebForLinkAppService informacaoComplementar,
            ISolicitacaoModificacaoBancoWebForLinkAppService solicitacaoModificacaoBanco,
            ISolicitacaoModificacaoContatoWebForLinkAppService solicitacaoModificacaoContato,
            ISolicitacaoModificacaoEnderecoWebForLinkAppService solicitacaoModificacaoEndereco,
            ISolicitacaoDocumentosFornecedorWebForLinkAppService pJpFSolicitacaoDocumentos,
            ISolicitacaoMaterialEServicoWebForLinkAppService solMaterialEServico,
            ITramiteWebForLinkAppService tramite,
            IServicosMateriaisWebForLinkAppService unspscBP
            )
        {
            try
            {
                _solicitacaoService = solicitacao;
                _solicitacaoCadastroFornecedorService = solicitacaoCadastroFornecedor;
                _usuarioService = usuario;
                _bancoService = banco;
                _enderecoService = endereco;
                _configuracaoDoContratanteService = contratanteConfiguracao;
                _fornecedorArquivoService = fornecedorArquivo;
                _papelService = papel;
                _cadastroUnicoService = cadastroUnico;
                _categoriaDeFornecedorService = fornecedorCategoria;
                _informacaoComplementarService = informacaoComplementar;
                _solicitacaoModificacaoDadosBancariosService = solicitacaoModificacaoBanco;
                _solicitacaoModificacaoDadosContatoService = solicitacaoModificacaoContato;
                _solicitacaoModificacaoEnderecoService = solicitacaoModificacaoEndereco;
                _solicitacaoDocumentosFornecedorService = pJpFSolicitacaoDocumentos;
                _solicitacaoMaterialService = solMaterialEServico;
                _tramite = tramite;
                _unspscBP = unspscBP;
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        [Authorize]
        public ActionResult FornecedoresDiretoFrm(string chaveurl)
        {
            var solicitacaoId = 0;

            try
            {
                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                    Int32.TryParse(param.First(p => p.Name == "SolicitacaoID").Value, out solicitacaoId);
                }

                if (solicitacaoId != 0)
                {
                    var solicitacao = _solicitacaoService.BuscarPorIdComFornecedoresDireto(solicitacaoId);
                    var ficha = new FichaCadastralWebForLinkVM();

                    if (solicitacao != null)
                    {
                        //if (!solicitacao.SolicitacaoDeDocumentos.Any())
                        //    solicitacao.SolicitacaoDeDocumentos = CriarSolicitacoesDocumentos(solicitacao.ID, solicitacao.SolicitacaoCadastroFornecedor.First().WFD_PJPF_CATEGORIA.ListaDeDocumentosDeFornecedor);

                        PreencherFichaCadastral(solicitacao, ficha, 10);

                        #region Preenchimento Específico

                        ficha.TipoPreenchimento = (int) EnumTiposPreenchimento.Contratante;

                        #endregion

                        PersistirDadosEmMemoria();
                        PersistirDadosEnderecoEmMemoria();

                        return View(ficha);
                    }
                    return RedirectToAction("Alerta", "Home");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                ViewBag.ChaveConferida = true;
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult FornecedoresDiretoFrm(FichaCadastralWebForLinkVM model, string ServicosSelecionados,
            string MateriaisSelecionados)
        {
            model.FornecedoresUnspsc = new List<FornecedorUnspscVM>();
            var unspscVM = new FornecedorUnspscVM();

            var materiaisEServicos = _unspscBP.BuscarListaPorID(ServicosSelecionados.Split(new Char[] { '|' }), MateriaisSelecionados.Split(new Char[] { '|' }));
            model.FornecedoresUnspsc = unspscVM.PreencheModelUnspsc(model.PJPFID, model.SolicitacaoID, materiaisEServicos);

            var preenchimentoValido = (FinalizarFichaCadastral(model));

            var cnpj = model.CNPJ_CPF.Replace(".", "").Replace("-", "").Replace("/", "");

            //se não for primeiro acesso enviar para tela de acesso
            if (Request.Url != null)
            {
                var url = Url.Action("Index", "Home", new
                {
                    chaveurl =
                        Cripto.Criptografar(
                            string.Format("SolicitacaoID={0}&Login={1}&TravaLogin=1", model.SolicitacaoID, cnpj), Key)
                }, Request.Url.Scheme);

                //se for primeiro acesso enviar para criação de usuário

                #region BuscarPorEmail

                //validar se o e-mail já existe na tabela de Usuarios
                if (!_usuarioService.ValidarPorCnpj(cnpj))
                {
                    var contato = model.DadosContatos.FirstOrDefault();
                    if (contato != null)
                        url = Url.Action("CadastrarUsuarioFornecedor", "Home", new
                        {
                            chaveurl = Cripto.Criptografar(string.Format("Login={0}&SolicitacaoID={1}&Email={2}",
                                cnpj,
                                model.SolicitacaoID,
                                contato.EmailContato), Key)
                        }, Request.Url.Scheme);
                }
            }

            #endregion BuscarPorEmail

            PersistirDadosEmMemoria();
            PersistirDadosEnderecoEmMemoria();

            if (preenchimentoValido)
                return RedirectToAction("FornecedoresLst", "Fornecedores", new
                {
                    MensagemSucesso =
                        string.Format("Solicitação Nº {0} de Criação de Fornecedor realizado com Sucesso!",
                            model.Solicitacao.ID)
                });

            return View(model);
        }

        public void PreencherFichaCadastral(SOLICITACAO solicitacao, FichaCadastralWebForLinkVM ficha, int tpPapel)
        {
            var contratante = solicitacao.Contratante;
            var solicitacaoCadastroPJPF = solicitacao.SolicitacaoCadastroFornecedor.First();
            var solicitacaoFornecedorVM = new SolicitacaoFornecedorVM();

            ficha.TipoFornecedor = solicitacaoCadastroPJPF.PJPF_TIPO;
            ficha.ContratanteID = contratante.ID;
            ficha.CategoriaId = solicitacaoCadastroPJPF.CATEGORIA_ID;
            ficha.ContratanteFornecedorID = solicitacao.CONTRATANTE_ID;
            ficha.SolicitacaoID = solicitacao.ID;

            ficha.Categoria = new CategoriaFichaVM
            {
                Id = solicitacaoCadastroPJPF.CATEGORIA_ID,
                Nome = solicitacaoCadastroPJPF.WFD_PJPF_CATEGORIA.CODIGO
            };

            ficha.Solicitacao = new SolicitacaoVM
            {
                ID = solicitacao.ID,
                Fluxo = new FluxoVM
                {
                    ID = solicitacao.FLUXO_ID
                }
            };

            switch ((EnumTiposFornecedor) solicitacaoCadastroPJPF.PJPF_TIPO)
            {
                case EnumTiposFornecedor.EmpresaNacional:
                    ficha.CNPJ_CPF = Convert.ToUInt64(solicitacaoCadastroPJPF.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                    ficha.RazaoSocial = solicitacaoCadastroPJPF.RAZAO_SOCIAL;
                    break;
                case EnumTiposFornecedor.EmpresaEstrangeira:
                    ficha.RazaoSocial = solicitacaoCadastroPJPF.RAZAO_SOCIAL;
                    break;
                case EnumTiposFornecedor.PessoaFisica:
                    ficha.CNPJ_CPF = Convert.ToUInt64(solicitacaoCadastroPJPF.CPF).ToString(@"000\.000\.000\-00");
                    ficha.RazaoSocial = solicitacaoCadastroPJPF.NOME;
                    break;
            }

            ficha.NomeFantasia = solicitacaoCadastroPJPF.NOME_FANTASIA;
            //ficha.CNAE = solicitacaoCadastroPJPF.CNAE;
            ficha.InscricaoEstadual = solicitacaoCadastroPJPF.INSCR_ESTADUAL;
            ficha.InscricaoMunicipal = solicitacaoCadastroPJPF.INSCR_MUNICIPAL;
            ficha.TipoLogradouro = solicitacaoCadastroPJPF.TP_LOGRADOURO;
            ficha.Endereco = solicitacaoCadastroPJPF.ENDERECO;
            ficha.Numero = solicitacaoCadastroPJPF.NUMERO;
            ficha.Complemento = solicitacaoCadastroPJPF.COMPLEMENTO;
            ficha.Cep = solicitacaoCadastroPJPF.CEP;
            ficha.Bairro = solicitacaoCadastroPJPF.BAIRRO;
            ficha.Cidade = solicitacaoCadastroPJPF.CIDADE;
            ficha.Estado = solicitacaoCadastroPJPF.UF;
            ficha.Pais = solicitacaoCadastroPJPF.PAIS;
            ficha.Observacao = solicitacaoCadastroPJPF.OBSERVACAO;

            //Mapear Dados Bancários
            var solicitacoesModBanco = solicitacao.SolicitacaoModificacaoDadosBancario.ToList();

            ficha.DadosBancarios = solicitacoesModBanco.Any()
                ? Mapper.Map<List<SolicitacaoModificacaoDadosBancario>, List<DadosBancariosVM>>(solicitacoesModBanco)
                : new List<DadosBancariosVM>();

            //Mapear Dados de Endereço
            var solicitacoesModEndereco = solicitacao.WFD_SOL_MOD_ENDERECO.ToList();

            if (solicitacaoCadastroPJPF.PJPF_TIPO == 1)
            {
                ficha.DadosEnderecos = solicitacoesModEndereco.Any()
                    ? Mapper.Map<List<SOLICITACAO_MODIFICACAO_ENDERECO>, List<DadosEnderecosVM>>(solicitacoesModEndereco)
                    : new List<DadosEnderecosVM>();
            }
            else
            {
                ficha.DadosEnderecos = solicitacoesModEndereco.Any()
                    ? Mapper.Map<List<SOLICITACAO_MODIFICACAO_ENDERECO>, List<DadosEnderecosVM>>(solicitacoesModEndereco)
                    : new List<DadosEnderecosVM> {new DadosEnderecosVM()};
            }

            //Mapear Dados Contatos
            var solicitacoesModContato = solicitacao.SolicitacaoModificacaoDadosContato.ToList();

            ficha.DadosContatos = solicitacoesModContato.Any()
                ? Mapper.Map<List<SolicitacaoModificacaoDadosContato>, List<DadosContatoVM>>(solicitacoesModContato)
                : new List<DadosContatoVM> {new DadosContatoVM()};

            if (solicitacao.WFD_SOL_MENSAGEM.Any())
            {
                solicitacaoFornecedorVM.Assunto = solicitacao.WFD_SOL_MENSAGEM.First().ASSUNTO;
                solicitacaoFornecedorVM.Mensagem = solicitacao.WFD_SOL_MENSAGEM.First().MENSAGEM;
            }

            solicitacaoFornecedorVM.Fornecedores = new List<SolicitacaoFornecedoresVM>();
            solicitacaoFornecedorVM.SolicitacaoCriacaoID = solicitacao.ID;

            solicitacaoFornecedorVM.Fornecedores =
                solicitacao.SolicitacaoCadastroFornecedor.Select(x => new SolicitacaoFornecedoresVM
                {
                    NomeFornecedor = x.RAZAO_SOCIAL,
                    CNPJ = x.CNPJ
                }).ToList();

            ficha.SolicitacaoFornecedor = solicitacaoFornecedorVM;

            //Mapear os Documentos
            solicitacaoFornecedorVM.Documentos =
                Mapper.Map<List<SolicitacaoDeDocumentos>, List<SolicitacaoDocumentosVM>>(
                    solicitacao.SolicitacaoDeDocumentos.ToList());

            //Mapear UNSPSC
            ficha.FornecedoresUnspsc =
                Mapper.Map<List<SOLICITACAO_UNSPSC>, List<FornecedorUnspscVM>>(solicitacao.WFD_SOL_UNSPSC.ToList());

            var papel = _papelService.BuscarPorContratanteETipoPapel(contratante.ID, tpPapel).ID;

            //Mapear Questionários
            ficha.Questionarios = new RetornoQuestionario<QuestionarioVM>
            {
                QuestionarioDinamicoList =
                    Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                        _cadastroUnicoService.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO
                        {
                            //PapelId = papelAtual,
                            UF = "RJ",
                            ContratanteId = contratante.ID,
                            PapelId = papel,
                            CategoriaId = solicitacaoCadastroPJPF.CATEGORIA_ID,
                            Alteracao = true,
                            SolicitacaoId = solicitacao.ID
                        })
                        )
            };

            ficha.ProrrogacaoPrazo = new ProrrogacaoPrazoVM();
            if (solicitacao.WFD_SOLICITACAO_PRORROGACAO.Count > 0)
            {
                //Busca a ultima solicitacao de prorrogação, ou seja a ativa.
                ficha.ProrrogacaoPrazo =
                    Mapper.Map<SOLICITACAO_PRORROGACAO, ProrrogacaoPrazoVM>(
                        solicitacao.WFD_SOLICITACAO_PRORROGACAO.OrderBy(o => o.ID).LastOrDefault());
            }
            ficha.ProrrogacaoPrazo.PrazoPreenchimento = _configuracaoDoContratanteService.BuscarPrazo(solicitacao);
            if (ficha.ProrrogacaoPrazo.Aprovado != null)
            {
                if ((bool) ficha.ProrrogacaoPrazo.Aprovado)
                    ficha.ProrrogacaoPrazo.Status = "Aprovado";
                else
                    ficha.ProrrogacaoPrazo.Status = "Reprovado";
            }
            else
                ficha.ProrrogacaoPrazo.Status = "Aguardando Aprovação...";
        }

        private void PersistirDadosEmMemoria()
        {
            //ViewBag.Bancos
            if (TempData["Bancos"] == null)
                TempData["Bancos"] = new SelectList(_bancoService.ListarTodosPorNome(), "ID", "BANCO_NM");

            ViewBag.Bancos = TempData["Bancos"] as SelectList;
            TempData.Keep("Bancos");
        }

        private void PersistirDadosEnderecoEmMemoria()
        {
            if (TempData["UF"] == null)
                TempData["UF"] = new SelectList(_enderecoService.ListarTodosPorNome(), "UF_SGL", "UF_NM");

            ViewBag.UF = TempData["UF"] as SelectList;
            TempData.Keep("UF");

            if (TempData["TipoEndereco"] == null)
                TempData["TipoEndereco"] = new SelectList(_enderecoService.ListarTodosTiposEnderecosPorNome(), "ID",
                    "NM_TP_ENDERECO");

            ViewBag.TipoEndereco = TempData["TipoEndereco"] as SelectList;
            TempData.Keep("TipoEndereco");
        }

        public bool FinalizarFichaCadastral(FichaCadastralWebForLinkVM model)
        {
            var preenchimentoValido = false;

            #region Validar Dados do Questionario Dinâmico

            var informacoesComplementar = new List<WFD_INFORM_COMPL>();
            if (model.Questionarios != null)
            {
                if (model.Questionarios.QuestionarioDinamicoList != null)
                {
                    foreach (var questionario in model.Questionarios.QuestionarioDinamicoList)
                    {
                        foreach (var aba in questionario.AbaList)
                        {
                            foreach (var pergunta in aba.PerguntaList)
                            {
                                #region Validar se os campos obrigatórios estão preenchidos

                                //if (pergunta.Obrigatorio)
                                //{
                                //    if (string.IsNullOrEmpty(pergunta.Resposta))
                                //    {
                                //        ModelState.AddModelError("QuestionarioDinamicoValidation", string.Format("Campo {0} obrigatório!", pergunta.Titulo));
                                //    }
                                //}

                                #endregion

                                //if (ModelState.IsValid)
                                //{
                                var infoCompleRespondida = new WFD_INFORM_COMPL
                                {
                                    SOLICITACAO_ID = model.Solicitacao.ID,
                                    PERG_ID = pergunta.Id,
                                    RESPOSTA = pergunta.Resposta
                                };

                                switch (pergunta.TipoDado)
                                {
                                    case "RadioButton":
                                    {
                                        if (pergunta.DominioListId != 0)
                                        {
                                            infoCompleRespondida.RESPOSTA = pergunta.DominioListId.ToString();
                                        }
                                    }
                                        break;
                                    case "Checkbox":
                                    {
                                        var respostaIdentada =
                                            pergunta.RespostaCheckbox.Where(resposta => !resposta.Equals("false"))
                                                .Aggregate(string.Empty,
                                                    (current, resposta) => string.Concat(current, "^", resposta));
                                        if (!string.IsNullOrEmpty(respostaIdentada))
                                            infoCompleRespondida.RESPOSTA = respostaIdentada.Remove(0, 1);
                                    }
                                        break;
                                }
                                informacoesComplementar.Add(infoCompleRespondida);
                                //}
                            }
                        }
                    }
                }
            }

            #endregion

            var categoria = _categoriaDeFornecedorService.BuscarPorId((int) model.CategoriaId);

            if (model.TipoFornecedor != 1)
            {
                if (model.DadosEnderecos == null || !model.DadosEnderecos.Any())
                {
                    ModelState.AddModelError("DadosEnderecoValidation", "Informar ao menos um Endereço!");
                }
                else if (
                    model.DadosEnderecos.Any(
                        x =>
                            x.TipoEnderecoId == 0 || String.IsNullOrEmpty(x.Endereco) ||
                            String.IsNullOrEmpty(x.Numero) || String.IsNullOrEmpty(x.CEP)))
                {
                    ModelState.AddModelError("DadosEnderecoValidation", @"Dados incompletos no Endereço!");
                }
            }

            if (!categoria.ISENTO_DADOSBANCARIOS && !model.ApenasSalvar)
            {
                if (!model.DadosBancarios.Any())
                {
                    ModelState.AddModelError("DadosBancariosValidation", "Informar ao menos um Dado Bancário!");
                    model.DadosBancarios.Add(new DadosBancariosVM());
                }
            }
            else
            {
                //REMOVE AS CRITICAS DOS DADOS BANCARIOS CASO A CATEGORIA SEJA ISENTA
                while (
                    ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Banco")).Value !=
                    null)
                    ModelState.Remove(
                        ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Banco")));

                while (
                    ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Agencia"))
                        .Value != null)
                    ModelState.Remove(
                        ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("Agencia")));

                while (
                    ModelState.FirstOrDefault(
                        ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrente")).Value != null)
                    ModelState.Remove(
                        ModelState.FirstOrDefault(
                            ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrente")));

                while (
                    ModelState.FirstOrDefault(
                        ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrenteDigito")).Value != null)
                    ModelState.Remove(
                        ModelState.FirstOrDefault(
                            ms => ms.Key.Contains("DadosBancarios") && ms.Key.Contains("ContaCorrenteDigito")));
            }

            if (!categoria.ISENTO_CONTATOS && !model.ApenasSalvar)
            {
                if (model.DadosContatos == null || !model.DadosContatos.Any())
                {
                    ModelState.AddModelError("DadosContatosValidation", "Informar os Dados do Contato!");
                    model.DadosContatos = new List<DadosContatoVM>();
                    model.DadosContatos.Add(new DadosContatoVM());
                }
            }
            else
            {
                //REMOVE AS CRITICAS DOS DADOS DE CONTATOS CASO A CATEGORIA SEJA ISENTA
                while (
                    ModelState.FirstOrDefault(ms => ms.Key.Contains("DadosContatos") && ms.Key.Contains("EmailContato"))
                        .Value != null)
                    ModelState.Remove(
                        ModelState.FirstOrDefault(
                            ms => ms.Key.Contains("DadosContatos") && ms.Key.Contains("EmailContato")));
            }

            if (!categoria.ISENTO_DOCUMENTOS && !model.ApenasSalvar && model.TipoFornecedor != 2)
            {
                var docsObrigatorios =
                    model.SolicitacaoFornecedor.Documentos.Where(x => x.Obrigatorio && x.ArquivoSubido == null).ToList();
                if (docsObrigatorios.Any())
                {
                    ModelState.AddModelError("AnexosValidation", "Favor subir os arquivos dos documentos Exigíveis!");
                }
            }

            ModelState.Remove("SolicitacaoFornecedor.Assunto");
            ModelState.Remove("SolicitacaoFornecedor.DescricaoSolicitacao");
            ModelState.Remove("SolicitacaoFornecedor.Mensagem");
            ModelState.Remove("SolicitacaoFornecedor.DadosEnderecos.T_UF.UF_NM");

            var tipoPapel = model.TipoPreenchimento.Equals((int) EnumTiposPreenchimento.Fornecedor)
                ? (int) EnumTiposPapel.Fornecedor
                : (int) EnumTiposPapel.Solicitante;
            var papelAtual = _papelService.BuscarPorContratanteETipoPapel(model.ContratanteID, tipoPapel).ID;
            foreach (var modeloErrado in ModelState.Values)
            {
                if (modeloErrado.Errors.Count != 0)
                {
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var solicitacaoCadastroPJPF =
                        _solicitacaoCadastroFornecedorService.BuscarPorSolicitacaoId(model.Solicitacao.ID);
                    CompletarSolicitacaoCadastroPJPF(ref solicitacaoCadastroPJPF, model);
                    _solicitacaoCadastroFornecedorService.AtualizarSolicitacao(solicitacaoCadastroPJPF);

                    ManterDadosBancarios(
                        model.DadosBancarios.Where(w => w.Banco != null && w.Agencia != null && w.ContaCorrente != null)
                            .ToList(), model.Solicitacao.ID, model.ContratanteID);
                    ManterDadosContatos(model.DadosContatos.Where(w => w.EmailContato != null).ToList(),
                        solicitacaoCadastroPJPF.SOLICITACAO_ID);
                    ManterDadosEnderecos(model.DadosEnderecos.Where(x => x.TipoEnderecoId > 0).ToList(),
                        solicitacaoCadastroPJPF.SOLICITACAO_ID);

                    ManterUnspsc(model.FornecedoresUnspsc.ToList(), model.Solicitacao.ID);

                    if (model.TipoFornecedor != (int) EnumTiposFornecedor.EmpresaEstrangeira)
                        ManterDocumentos(model.SolicitacaoFornecedor.Documentos, model.Solicitacao.ID,
                            model.ContratanteID);

                    _informacaoComplementarService.InsertAll(informacoesComplementar);


                    model.ProrrogacaoPrazo = new ProrrogacaoPrazoVM();
                    if (solicitacaoCadastroPJPF.WFD_SOLICITACAO.WFD_SOLICITACAO_PRORROGACAO.Count > 0)
                    {
                        //Busca a ultima solicitacao de prorrogação, ou seja a ativa.
                        model.ProrrogacaoPrazo =
                            Mapper.Map<SOLICITACAO_PRORROGACAO, ProrrogacaoPrazoVM>(
                                solicitacaoCadastroPJPF.WFD_SOLICITACAO.WFD_SOLICITACAO_PRORROGACAO.OrderBy(o => o.ID)
                                    .LastOrDefault());
                    }
                    model.ProrrogacaoPrazo.PrazoPreenchimento =
                        _configuracaoDoContratanteService.BuscarPrazo(solicitacaoCadastroPJPF.WFD_SOLICITACAO);
                    if (model.ProrrogacaoPrazo.Aprovado != null)
                    {
                        if ((bool) model.ProrrogacaoPrazo.Aprovado)
                            model.ProrrogacaoPrazo.Status = "Aprovado";
                        else
                            model.ProrrogacaoPrazo.Status = "Reprovado";
                    }
                    else
                    {
                        model.ProrrogacaoPrazo.Status = "Aguardando Aprovação...";
                    }


                    if (!model.ApenasSalvar)
                    {
                        _tramite.AtualizarTramite(model.ContratanteID, model.Solicitacao.ID, model.Solicitacao.Fluxo.ID,
                            papelAtual, (int) EnumStatusTramite.Aprovado, null);

                        ViewBag.MensagemSucesso = "Dados Enviados com Sucesso!";
                        ViewBag.StatusTramite = (int) EnumStatusTramite.Aprovado;
                    }
                    else
                    {
                        ViewBag.MensagemSucesso = "Dados Salvos com Sucesso!";
                        ViewBag.StatusTramite = (int) EnumStatusTramite.Aguardando;
                    }
                    preenchimentoValido = true;
                }
                catch (Exception e)
                {
                    ViewBag.MensagemErro = "Erro ao tentar Salvar a ficha cadastral!";
                    ViewBag.StatusTramite = (int) EnumStatusTramite.Aguardando;
                    Log.Error(e);
                }
            }
            else
            {
                ViewBag.MensagemErro = "Não foi possível enviar a Ficha Cadastral! Existem dados incompletos abaixo.";
                ViewBag.StatusTramite = (int) EnumStatusTramite.Aguardando;
            }

            model.Questionarios = new RetornoQuestionario<QuestionarioVM>
            {
                QuestionarioDinamicoList =
                    Mapper.Map<List<QuestionarioDinamico>, List<QuestionarioVM>>(
                        _cadastroUnicoService.BuscarQuestionarioDinamico(new QuestionarioDinamicoFiltrosDTO
                        {
                            //PapelId = papelAtual,
                            UF = "RJ",
                            ContratanteId = model.ContratanteID,
                            PapelId = papelAtual,
                            CategoriaId = categoria.ID,
                            Alteracao = true,
                            SolicitacaoId = model.Solicitacao.ID
                        })
                        )
            };

            //model.ProrrogacaoPrazo = new ProrrogacaoPrazoVM
            //{
            //    PrazoPreenchimento = contratanteConfiguracaoBP.BuscarPrazo(model.ContratanteID, model.Solicitacao.ID)
            //};

            PersistirDadosEnderecoEmMemoria();

            return preenchimentoValido;
        }

        private void CompletarSolicitacaoCadastroPJPF(ref SolicitacaoCadastroFornecedor cadPJPF,
            FichaCadastralWebForLinkVM ficha)
        {
            if (ficha.TipoFornecedor != 1)
            {
                cadPJPF.NOME = ficha.RazaoSocial;
                cadPJPF.NOME_FANTASIA = ficha.NomeFantasia;
                //cadPJPF.CNAE = ficha.CNAE;

                //Transformar em Enum
                if (ficha.TipoFornecedor == 3)
                {
                    cadPJPF.CPF = ficha.CNPJ_CPF.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "");
                    cadPJPF.INSCR_ESTADUAL = ficha.InscricaoEstadual;
                }

                cadPJPF.TP_LOGRADOURO = ficha.TipoLogradouro;
                cadPJPF.ENDERECO = ficha.Endereco;
                cadPJPF.NUMERO = ficha.Numero;
                cadPJPF.COMPLEMENTO = ficha.Complemento;
                cadPJPF.CEP = ficha.Cep;
                cadPJPF.BAIRRO = ficha.Bairro;
                cadPJPF.CIDADE = ficha.Cidade;
                cadPJPF.UF = ficha.Estado;
                cadPJPF.PAIS = ficha.Pais;
            }

            //Transformar em Enum
            if (ficha.TipoFornecedor != 2)
                cadPJPF.INSCR_MUNICIPAL = ficha.InscricaoMunicipal;

            cadPJPF.OBSERVACAO = ficha.Observacao;
        }

        private void ManterDadosBancarios(List<DadosBancariosVM> dadosBancarios, int solicitacaoCriacaoID,
            int contratanteID)
        {
            foreach (var item in dadosBancarios)
            {
                item.SolicitacaoID = solicitacaoCriacaoID;
                item.ContratanteID = contratanteID;

                var arquivoId = 0;
                if (string.IsNullOrEmpty(item.NomeArquivo))
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                        arquivoId = _fornecedorArquivoService.GravarArquivoSolicitacao(contratanteID, item.ArquivoSubido,
                            item.TipoArquivoSubido);
                }
                else
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                        arquivoId = _fornecedorArquivoService.SubstituirArquivoSolicitacaoBancario(contratanteID,
                            (int) item.BancoSolicitacaoID, (int) item.ArquivoID, item.ArquivoSubido,
                            item.TipoArquivoSubido, item.NomeArquivo);
                }


                if (!String.IsNullOrEmpty(item.ArquivoSubido))
                {
                    item.ArquivoID = arquivoId;
                    item.NomeArquivo = _fornecedorArquivoService.PegaNomeArquivoSubido(item.ArquivoSubido);
                    item.ArquivoSubido = null;
                    item.TipoArquivoSubido = null;
                }
            }

            var solicitacoesModBancoMapeadas =
                Mapper.Map<List<DadosBancariosVM>, List<SolicitacaoModificacaoDadosBancario>>(dadosBancarios);

            _solicitacaoModificacaoDadosBancariosService.ManterBancoCadastroFornecedor(solicitacoesModBancoMapeadas,
                solicitacaoCriacaoID);
        }

        private void ManterDadosContatos(List<DadosContatoVM> dadosContatos, int solicitacaoCriacaoID)
        {
            var solicitacoesModContatoMapeadas =
                Mapper.Map<List<DadosContatoVM>, List<SolicitacaoModificacaoDadosContato>>(dadosContatos)
                    .Select(x =>
                    {
                        x.SOLICITACAO_ID = solicitacaoCriacaoID;
                        return x;
                    }).ToList();

            _solicitacaoModificacaoDadosContatoService.ManterContatoCadastroFornecedor(solicitacoesModContatoMapeadas,
                solicitacaoCriacaoID);
        }

        private void ManterDadosEnderecos(List<DadosEnderecosVM> dadosEnderecos, int solicitacaoCriacaoID)
        {
            var solicitacoesModEndereco =
                _solicitacaoModificacaoEnderecoService.ListarPorSolicitacaoId(solicitacaoCriacaoID).ToList();
            var solicitacoesModEnderecoPostadas = dadosEnderecos.Select(x => x.ID).ToArray();
            var solicitacoesModContatoExcluidas =
                solicitacoesModEndereco.Where(x => !solicitacoesModEnderecoPostadas.Contains(x.ID)).ToList();

            _solicitacaoModificacaoEnderecoService.ExcluirSolicitacoes(solicitacoesModContatoExcluidas);

            var solicitacoesModEnderecoMapeadas =
                Mapper.Map<List<DadosEnderecosVM>, List<SOLICITACAO_MODIFICACAO_ENDERECO>>(dadosEnderecos)
                    .Select(x =>
                    {
                        x.SOLICITACAO_ID = solicitacaoCriacaoID;
                        return x;
                    }).ToList();
            _solicitacaoModificacaoEnderecoService.InserirOuAtualizarSolicitacoes(solicitacoesModEnderecoMapeadas);
        }

        private void ManterDocumentos(List<SolicitacaoDocumentosVM> solicitacoesDocumentosVM, int solicitacaoCriacaoID,
            int contratanteId)
        {
            var solicitacoesDocumentos =
                _solicitacaoDocumentosFornecedorService.ListarPorSolicitacaoId(solicitacaoCriacaoID);

            foreach (var item in solicitacoesDocumentosVM)
            {
                var arquivoId = 0;

                var solicitacaoDocumentos = solicitacoesDocumentos.FirstOrDefault(x => x.ID == item.ID);
                if (string.IsNullOrEmpty(item.NomeArquivo))
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                    {
                        arquivoId = _fornecedorArquivoService.GravarArquivoSolicitacao(contratanteId, item.ArquivoSubido,
                            item.TipoArquivoSubido);
                        solicitacaoDocumentos.ARQUIVO_ID = arquivoId;
                    }
                }
                else
                {
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                    {
                        arquivoId = _fornecedorArquivoService.SubstituirArquivoSolicitacaoDocumento(contratanteId,
                            item.ID, (int) item.ArquivoID, item.ArquivoSubido, item.TipoArquivoSubido, item.NomeArquivo);
                        solicitacaoDocumentos.ARQUIVO_ID = arquivoId;
                    }
                }

                if (item.Periodicidade != null)
                {
                    if (item.Periodicidade > 0)
                        solicitacaoDocumentos.DATA_VENCIMENTO = CalculaDataVencimento((int) item.Periodicidade);
                }
                else
                {
                    if (item.PorValidade != null)
                        if ((bool) item.PorValidade)
                            solicitacaoDocumentos.DATA_VENCIMENTO = item.DataValidade;
                }

                _solicitacaoDocumentosFornecedorService.AtualizarSolicitacao(solicitacaoDocumentos);

                item.ID = solicitacaoDocumentos.ID;
                if (!String.IsNullOrEmpty(item.ArquivoSubido))
                {
                    item.NomeArquivo = _fornecedorArquivoService.PegaNomeArquivoSubido(item.ArquivoSubido);
                    item.ArquivoID = arquivoId;
                    item.ArquivoSubido = null;
                    item.TipoArquivoSubido = null;
                }
                if (solicitacaoDocumentos.SOLICITACAO_ID != null)
                    item.SolicitacaoID = (int)solicitacaoDocumentos.SOLICITACAO_ID;
            }
        }

        private void ManterUnspsc(List<FornecedorUnspscVM> unspsc, int solicitacaoCriacaoID)
        {
            var wfdSolUnspsc = new List<SOLICITACAO_UNSPSC>();

            unspsc.ForEach(x => wfdSolUnspsc.Add(new SOLICITACAO_UNSPSC
            {
                SOLICITACAO_ID = solicitacaoCriacaoID,
                UNSPSC_ID = x.UsnpscId
            }));
            _solicitacaoMaterialService.ManterUnspscSolicitacao(wfdSolUnspsc, solicitacaoCriacaoID);
        }

        private DateTime CalculaDataVencimento(int periodoId)
        {
            var vencimento = DateTime.MinValue;
            switch (periodoId)
            {
                case 1:
                    vencimento = DateTime.Now.AddDays(1);
                    break;
                case 2:
                    vencimento = DateTime.Now.AddDays(15);
                    break;
                case 3:
                    vencimento = DateTime.Now.AddMonths(1);
                    break;
                case 4:
                    vencimento = DateTime.Now.AddMonths(2);
                    break;
                case 5:
                    vencimento = DateTime.Now.AddMonths(3);
                    break;
                case 6:
                    vencimento = DateTime.Now.AddMonths(6);
                    break;
                case 7:
                    vencimento = DateTime.Now.AddYears(1);
                    break;
            }

            return vencimento;
        }
    }
}