using AutoMapper;
using Correios.Net;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class MeusDocumentosController : ControllerPadrao
    {
        private readonly IContratanteWebForLinkAppService _contratanteService;
        private readonly ITipoDocumentosWebForLinkAppService _tipoDocumentosService;
        private readonly IContratanteArquivoWebForLinkAppService _contratanteArquivoService;
        private readonly IServicosMateriaisWebForLinkAppService _servicosMateriaisService;
        private readonly IArquivoWebForLinkAppService _arquivoService;
        private readonly IMeusDocumentosWebForLinkAppService _meusDocumentosService;
        private readonly IMeusCompartilhamentosWebForLinkAppService _meusCompartilhamentosService;
        private readonly IFornecedorArquivoWebForLinkAppService _arquivoFornecedorService;
        private readonly IBancoWebForLinkAppService _bancoService;
        private readonly IFornecedorDocumentoWebForLinkAppService _fornecedorDocumentoService;
        private readonly IDocumentoWebForLinkAppService _documentoService;
        private readonly IFornecedorVersaoDocumentosWebForLinkAppService _fornecedorVersaoDocumentosService;
        private readonly IFornecedorBancoWebForLinkAppService _fornecedorBancoService;
        private readonly IFornecedorContatoWebForLinkAppService _fornecedorContatosService;
        private readonly IEnderecoWebForLinkAppService _enderecoService;
        private readonly IFornecedorEnderecoWebForLinkAppService _fornecedorEnderecoService;
        private readonly IConfiguracaoWebForLinkAppService _configService;


        public MeusDocumentosController(
        IContratanteWebForLinkAppService contratante,
        ITipoDocumentosWebForLinkAppService tipoDocumento,
        IContratanteArquivoWebForLinkAppService contratanteArquivo,
        IServicosMateriaisWebForLinkAppService servicosMateriais,
        IArquivoWebForLinkAppService arquivo,
        IMeusDocumentosWebForLinkAppService meusDocumentos,
        IMeusCompartilhamentosWebForLinkAppService meusCompartilhamentos,
        IFornecedorArquivoWebForLinkAppService arquivoFornecedor,
        IBancoWebForLinkAppService banco,
        IFornecedorDocumentoWebForLinkAppService pjpfDocumentos,
        IDocumentoWebForLinkAppService documentobp,
        IFornecedorVersaoDocumentosWebForLinkAppService pjpfDocumentosService,
        IFornecedorBancoWebForLinkAppService pjpfBancoBP,
        IFornecedorContatoWebForLinkAppService pjpfContatosBP,
        IEnderecoWebForLinkAppService enderecoBP,
        IFornecedorEnderecoWebForLinkAppService pjpfEndereco,
        IConfiguracaoWebForLinkAppService config)
        {
            _contratanteService = contratante;
            _tipoDocumentosService = tipoDocumento;
            _contratanteArquivoService = contratanteArquivo;
            _servicosMateriaisService = servicosMateriais;
            _arquivoService = arquivo;
            _meusDocumentosService = meusDocumentos;
            _meusCompartilhamentosService = meusCompartilhamentos;
            _arquivoFornecedorService = arquivoFornecedor;
            _bancoService = banco;
            _fornecedorDocumentoService = pjpfDocumentos;
            _documentoService = documentobp;
            _fornecedorVersaoDocumentosService = pjpfDocumentosService;
            _fornecedorBancoService = pjpfBancoBP;
            _fornecedorContatosService = pjpfContatosBP;
            _enderecoService = enderecoBP;
            _fornecedorEnderecoService = pjpfEndereco;
            _configService = config;
        }

        #region MEUS DOCUMENTOS

        [Authorize]
        public ActionResult MeusDocumentosLst(int? Pagina, int? TipoDocumento, int? DescricaoDocumento, string MensagemSucesso)
        {

            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            int pagina = Pagina ?? 1;
            ViewBag.MensagemSucesso = MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;
            int totalRegistro;

            List<DocumentosDoFornecedor> lista = _fornecedorDocumentoService.ListarDescricaoDeDocumentosUtilizadasPorContratante(contratanteId);
            ViewBag.TipoDocumento = new SelectList(lista.Select(x => x.DescricaoDeDocumentos.TipoDeDocumento).Distinct().ToList(), "ID", "DESCRICAO", TipoDocumento);
            ViewBag.DescricaoDocumento = new SelectList(lista.Select(x => x.DescricaoDeDocumentos).Distinct().ToList(), "ID", "DESCRICAO", DescricaoDocumento);

            //BUSCA DOCUMENTOS E MONTA PAGINAÇÃO
            List<DocumentosDoFornecedor> meusDocumentos = BuscaDocumentos(contratanteId, TipoDocumento, DescricaoDocumento, true, pagina, null, out totalRegistro);

            var totalPaginas = (int)Math.Ceiling(totalRegistro / (double)TamanhoPagina);
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.TotalRegistros = totalRegistro;

            return View(MeusDocumentosVM.ModelToViewModel(meusDocumentos, Url));
        }

        [Authorize]
        public ActionResult MeusDocumentosFrm(string chaveurl)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int id = 0;
            string acao = "";

            if (!string.IsNullOrEmpty(chaveurl))
            {

                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "id").Value, out id);
                acao = param.First(p => p.Name == "Acao").Value;
            }
            MeusDocumentosVM meuDocumento = new MeusDocumentosVM();

            // INCLUSAO
            if (id == 0)
            {
                ViewBag.TipoDocumentos = new SelectList(MontaTipoDocumento("Frm"), "Value", "Text");
                ViewBag.DescricaoDocumentos = new SelectList(new List<SelectListItem>());

                meuDocumento.Ativo = true;
            }
            else
            {
                ViewBag.Acao = acao;
                DocumentosDoFornecedor md = _fornecedorDocumentoService.BuscarPorIdContratanteId(contratanteId, id);
                meuDocumento = Mapper.Map<MeusDocumentosVM>(md);
                meuDocumento.UrlArquivo = Url.Action("MeusDocumentosArquivo", "MeusDocumentos", new { chaveurl = Cripto.Criptografar(string.Format("idDoc={0}&local=Interno", md.ID), Key) });
                ViewBag.TipoDocumentos = new SelectList(MontaTipoDocumento("Frm"), "Value", "Text", md.DescricaoDeDocumentos.TIPO_DOCUMENTOS_ID);
                ViewBag.DescricaoDocumentos = new SelectList(MontaDescricaoDocumento(md.DescricaoDeDocumentos.TIPO_DOCUMENTOS_ID, "Frm"), "Value", "Text", md.DESCRICAO_DOCUMENTO_ID);
            }

            return View(meuDocumento);
        }

        [Authorize]
        [HttpPost]
        public ActionResult MeusDocumentosFrm(MeusDocumentosVM model, int? TipoDocumentos, int? DescricaoDocumentos, string Acao)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            ViewBag.TipoDocumentos = new SelectList(MontaTipoDocumento("Frm"), "Value", "Text", TipoDocumentos);
            ViewBag.DescricaoDocumentos = new SelectList(MontaDescricaoDocumento(TipoDocumentos, "Frm"), "Value", "Text", DescricaoDocumentos);

            //WFD_MEUS_DOCUMENTOS documento;
            if (Acao != "Excluir")
            {
                if (TipoDocumentos == null)
                    ModelState.AddModelError("TipoDocumento", "Informe o Tipo de Documento");

                if (DescricaoDocumentos == null)
                    ModelState.AddModelError("DescricaoDocumento", "Informe a Descrição do Documento");

                if (model.DataEmissao > DateTime.Today)
                    ModelState.AddModelError("DataEmissao", "Data de Emissão não pode ser maior que a data de hoje.");

                if (!model.SemValidade)
                {
                    if (model.DataValidade == null)
                        ModelState.AddModelError("DataValidade", "Informe a Data de Validade do documento ou informe Sem Validade!.");
                    else
                        if (model.DataValidade < DateTime.Today)
                        ModelState.AddModelError("DataValidade", "Data de Validade não pode ser menor que a data de hoje.");
                }

                if (String.IsNullOrEmpty(model.ArquivoSubido))
                    ModelState.AddModelError("Arquivo", "Selecione o Arquivo");
            }
            else
            {
                // remove dcritica de data de emaissao pq é exclusão
                ModelState.Remove("DataEmissao");
            }

            DocumentosDoFornecedor documento = new DocumentosDoFornecedor();
            if (ModelState.IsValid)
            {
                //CadastrarNovoDocumento
                if (model.ID == 0)
                {
                    int arquivoId = 0;
                    if (!String.IsNullOrEmpty(model.ArquivoSubido))
                        arquivoId = _contratanteArquivoService.GravarArquivo(model.ArquivoSubido, model.TipoArquivoSubido);
                    documento = Mapper.Map<DocumentosDoFornecedor>(model);
                    documento.DESCRICAO_DOCUMENTO_ID = DescricaoDocumentos != null ? (int)DescricaoDocumentos : 0;

                    DocumentosDoFornecedor documentoIncluido = _documentoService.CadastrarNovoDocumento(documento, contratanteId, (int)EnumTipoContratante.ContratanteAncora, arquivoId);
                    CriarVersionamentoDeArquivo(usuarioId, documento);

                    if (documentoIncluido != null)
                        return RedirectToAction("MeusDocumentosLst", "MeusDocumentos", new { MensagemSucesso = "Inclusão realizada com sucesso!" });
                }
                else if (Acao == "Alterar")
                {
                    documento = _documentoService.BuscarPorId(model.ID);
                    int ArquivoAntigoId = (int)documento.ARQUIVO_ID;

                    if (documento != null)
                    {
                        documento.DESCRICAO_DOCUMENTO_ID = (int)DescricaoDocumentos;
                        documento.DATA_EMISSAO = model.DataEmissao;
                        documento.DATA_VENCIMENTO = model.DataValidade;
                        documento.SEM_VALIDADE = model.SemValidade;
                        documento.ATIVO = model.Ativo;

                        int arquivoId = 0;
                        if (!String.IsNullOrEmpty(model.ArquivoSubido))
                        {
                            arquivoId = _contratanteArquivoService.GravarArquivo(model.ArquivoSubido, model.TipoArquivoSubido);
                            documento.ARQUIVO_ID = arquivoId;
                        }
                        CriarVersionamentoDeArquivo(usuarioId, documento);
                        //Ele é substituido
                        _fornecedorDocumentoService.AlterarDocumentos(documento);
                        return RedirectToAction("MeusDocumentosLst", "MeusDocumentos", new { MensagemSucesso = "Alteração realizada com sucesso!" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Erro ao tentar alterar o documento, identificador não encontrado!");
                    }
                }
                else if (Acao == "Excluir")
                {
                    _arquivoService.ExcluirMeusDocumentos((int)model.ArquivoId, model.ID, contratanteId);
                    return RedirectToAction("MeusDocumentosLst", "MeusDocumentos", new { MensagemSucesso = "Exclusão realizada com sucesso!" });
                }
            }
            return View(model);
        }

        private void CriarVersionamentoDeArquivo(int usuarioId, DocumentosDoFornecedor documento)
        {
            VersionamentoDeDocumentoDoFornecedor versao = new VersionamentoDeDocumentoDoFornecedor();

            versao.ARQUIVO_ID = documento.ARQUIVO_ID.Value;
            versao.DOCUMENTO_ID = documento.ID;
            versao.USUARIO_ID = usuarioId;
            versao.DATA_UPLOAD = documento.DATA_UPLOAD.Value;
            _fornecedorVersaoDocumentosService.Inserir(versao);
        }

        #endregion

        #region ENVIAR

        [Authorize]
        public ActionResult EnviarLst(string chaveurl, int? Pagina, int? TipoDocumento, int? DescricaoDocumento, string DocumentosSelecionados, int? AdicionarDocID, int? QtdRegistroPagina, int? RemoverDocID, int? AdicionarTudo, int? RemoverTudo, string MensagemSucesso)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            TamanhoPagina = 5;

            int pagina = Pagina ?? 1;
            ViewBag.MensagemSucesso = MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;
            int totalRegistro = 0;
            int totalPaginas = 0;

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                DocumentosSelecionados = param.First(p => p.Name == "DocsSelecionados").Value;
            }

            ////MONTA AS DropDownList TIPO DOCUMENTO e DESCRICAO DOCUMENTOS
            ViewBag.TipoDocumento = new SelectList(Db.WFD_TIPO_DOCUMENTOS.Where(e => e.ATIVO && e.CONTRATANTE_ID == contratanteId).OrderBy(e => e.DESCRICAO), "ID", "DESCRICAO", TipoDocumento);
            ViewBag.DescricaoDocumento = new SelectList(MontaDescricaoDocumento(TipoDocumento, "Lst"), "Value", "Text");

            //MONTA A GRID DE DOCUMENTOS SELECIONADOS ADICIONANDO OU REMOVENDO
            List<DocumentosDoFornecedor> meusDocumentos = new List<DocumentosDoFornecedor>();

            if (AdicionarTudo != null)
            {
                var DocsID = new HashSet<int>(BuscaDocumentos(contratanteId, TipoDocumento, DescricaoDocumento, true, 0, (string)ViewBag.DocumentosSelecionados, out totalRegistro).Select(d => d.ID));
                foreach (int id in DocsID)
                {
                    DocumentosSelecionados = MontarStringDocsSelecionados(DocumentosSelecionados, id, "Adicionar");
                }
            }
            else if (RemoverTudo != null)
            {
                DocumentosSelecionados = "";
            }
            else
            {
                if (AdicionarDocID != null)
                    DocumentosSelecionados = MontarStringDocsSelecionados(DocumentosSelecionados, AdicionarDocID, "Adicionar");
                else if (RemoverDocID != null)
                    DocumentosSelecionados = MontarStringDocsSelecionados(DocumentosSelecionados, RemoverDocID, "Remover");
            }

            //MONTA OS DOCUMENTOS EXISTENTES
            if (AdicionarDocID != null)
            {
                if (QtdRegistroPagina == 1 && pagina > 1)
                {
                    pagina = pagina - 1;
                    ViewBag.Pagina = pagina;
                }
            }
            meusDocumentos = BuscaDocumentos(contratanteId, TipoDocumento, DescricaoDocumento, true, pagina, DocumentosSelecionados, out totalRegistro);
            totalPaginas = (int)Math.Ceiling(totalRegistro / (double)TamanhoPagina);
            ViewBag.GridDocumentosSelecionados = BuscaDocumentosSelecionados(DocumentosSelecionados);
            ViewBag.DocumentosSelecionados = DocumentosSelecionados;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.TotalRegistros = totalRegistro;
            ViewBag.QtdRegistroPagina = meusDocumentos.Count();

            List<MeusDocumentosVM> modelo = MeusDocumentosVM.ModelToViewModel(meusDocumentos, Url);
            ViewBag.LinkCriarEmail = Url.Action("EnviarFrm", "MeusDocumentos", new { chaveurl = Cripto.Criptografar(string.Format("DocsSelecionados={0}", (string)ViewBag.DocumentosSelecionados), Key) });

            return View(modelo);
        }


        [Authorize]
        public ActionResult EnviarFrm(string chaveurl)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");
            string docsSelecionados = "";

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                docsSelecionados = param.First(p => p.Name == "DocsSelecionados").Value;
            }

            //MONTA OS DOCUMENTOS SELECIONADOS NA TELA ANTERIOR
            List<int> documentos = ConverteLista(docsSelecionados, '|');

            MeusDocumentosEnviarVM modelo = new MeusDocumentosEnviarVM();
            modelo.MeusDocumentos = Db.WFD_PJPF_DOCUMENTOS
                            .Include("DescricaoDeDocumentos.TipoDeDocumento")
                            .Where(d => documentos.Contains(d.ID))
                            .ToList();

            //Criar Mensagem
            string mensagem = string.Empty;
            string docs = "";
            string listaDocumentosEmail = string.Empty;

            foreach (DocumentosDoFornecedor doc in modelo.MeusDocumentos)
            {
                docs += doc.ID + "|";
                listaDocumentosEmail += "<li>" + doc.DescricaoDeDocumentos.DESCRICAO + "</li>";
            }

            CONTRATANTE_CONFIGURACAO_EMAIL configEmail = Db.WFD_CONTRATANTE_CONFIG_EMAIL
                .FirstOrDefault(x => x.EMAIL_TP_ID == 4
                    && x.CONTRATANTE_ID == contratanteId);

            if (configEmail != null)
            {
                modelo.Assunto = configEmail.ASSUNTO;
                mensagem = configEmail.CORPO;
            }
            mensagem = mensagem.Replace("^ListaDocumentos^", listaDocumentosEmail);

            Contratante contratante = Db.Contratante.FirstOrDefault(x => x.ID == contratanteId);
            if (contratante != null)
                mensagem = mensagem.Replace("^NomeEmpresa^", contratante.RAZAO_SOCIAL);

            Usuario usuario = Db.WFD_USUARIO.FirstOrDefault(x => x.ID == usuarioId);
            if (usuario != null)
            {
                mensagem = mensagem.Replace("^NomeUsuario^", usuario.NOME);
                mensagem = mensagem.Replace("^FixoUsuario1^", Mascara.IncluirMascaraTelefone(usuario.FIXO));//Validar Telefone
                mensagem = mensagem.Replace("^CelularUsuario1^", Mascara.IncluirMascaraTelefone(usuario.CELULAR));//Validar Celular
                mensagem = mensagem.Replace("^EmailUsuario^", usuario.EMAIL);
            }

            modelo.Mensagem = mensagem;

            modelo.EnviarFichaCadastral = false;
            PopularDadosBancariosContatos(modelo, contratanteId);

            modelo.Documentos = docs;
            ViewBag.Documentos = docs;
            ViewBag.LinkCancelar = Url.Action("EnviarLst", "MeusDocumentos", new { chaveurl = chaveurl });

            return View(modelo);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EnviarFrm(MeusDocumentosEnviarVM model, string Documentos)
        {
            int ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            //MONTA OS DOCUMENTOS SELECIONADOS NA TELA ANTERIOR
            List<int> documentos = ConverteLista(Documentos, '|');

            List<DocumentosDoFornecedor> documentosSelecionados = Db.WFD_PJPF_DOCUMENTOS
                .Include("DescricaoDeDocumentos.TipoDeDocumento")
                .Where(d => documentos.Contains(d.ID)).ToList();

            model.MeusDocumentos = documentosSelecionados;
            ViewBag.Documentos = Documentos;

            ModelState.Clear();

            if (model.SemPrazo == false && model.DataValidade == null)
                ModelState.AddModelError("DataValidade", "Escolha uma data para prazo de disponibilidade dos documentos ou informe sem prazo!");

            if (ModelState.IsValid)
            {
                //SALVA COMPARTILHAMENTO
                if (SalvarCompartilhamento(model, ContratanteId, false))
                {
                    return RedirectToAction("EnviarLst", "MeusDocumentos", new { MensagemSucesso = "Documentos Enviados com Sucesso!" });
                }
            }
            PopularDadosBancariosContatos(model, ContratanteId);

            return View(model);
        }

        #endregion

        #region ENVIADOS

        [Authorize]
        public ActionResult EnviadosLst(EnviadosPesquisaVM modelo)
        {
            int pagina = modelo.Pagina ?? 1;
            ViewBag.MensagemSucesso = modelo.MensagemSucesso ?? "";
            ViewBag.Pagina = pagina;

            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            List<DocumentosDoFornecedor> lista = _fornecedorDocumentoService.ListarDescricaoDeDocumentosUtilizadasPorContratante(contratanteId);
            ViewBag.TipoDocumento = new SelectList(lista.Select(x => x.DescricaoDeDocumentos.TipoDeDocumento).Distinct().ToList(), "ID", "DESCRICAO", modelo.TipoDocumento);
            ViewBag.DescricaoDocumento = new SelectList(lista.Select(x => x.DescricaoDeDocumentos).Distinct().ToList(), "ID", "DESCRICAO", modelo.DescricaoDocumento);

            //BUSCA DOCUMENTOS E MONTA PAGINAÇÃO
            Expression<Func<Compartilhamentos, bool>> filtro = Predicativos.FiltrarMeuCompartilhamentosGrid(modelo, contratanteId);
            RetornoPesquisa<Compartilhamentos> listaPesquisa = new RetornoPesquisa<Compartilhamentos>();
            Func<Compartilhamentos, IComparable> ordenacao = (Compartilhamentos a) => a.ENVIADO_EM;

            listaPesquisa = _meusDocumentosService.BuscarPesquisaInvertido(filtro, TamanhoPagina, pagina, ordenacao);

            ViewBag.TotalPaginas = listaPesquisa.TotalPaginas;
            ViewBag.TotalRegistros = listaPesquisa.TotalRegistros;
            
            modelo.EnviadosGrid = EnviadosVM.ModelToViewModel(listaPesquisa.RegistrosPagina, Url);
            
            return View(modelo);
        }

        [Authorize]
        public ActionResult EnviadosFrm(string chaveurl)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int idCompartilhamento = 0;

            if (!string.IsNullOrEmpty(chaveurl))
            {
                List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                Int32.TryParse(param.First(p => p.Name == "idComp").Value, out idCompartilhamento);
            }

            EnviadosVM enviado = new EnviadosVM();

            Compartilhamentos comp = _meusCompartilhamentosService
                .Buscar(c => c.CONTRATANTE_ID == contratanteId
                    && c.ID == idCompartilhamento);

            enviado.Emails = new List<EnviadosEmailsVM>();
            enviado.MeusCompartilhamentosId = idCompartilhamento;
            enviado.EnviadoEm = comp.ENVIADO_EM.HasValue ? Convert.ToDateTime(comp.ENVIADO_EM).ToShortDateString() : "";
            enviado.Chave = comp.CHAVE;
            enviado.Assunto = comp.ASSUNTO;
            enviado.Mensagem = comp.MENSAGEM;
            enviado.SemPrazo = comp.SEM_PRAZO;
            enviado.Disponibilidade = comp.VALIDADE != null ? Convert.ToDateTime(comp.VALIDADE).ToShortDateString() : null;

            enviado.DadosContatos = DadosContatoVM.ModelToViewModel(comp.WFD_PJPF_CONTATOS.ToList());
            enviado.DadosBancarios = DadosBancariosVM.ModelToViewModel(comp.WFD_PJPF_BANCO.ToList());

            // MONTA DESTINATARIOS COMPARTILHADOS
            string emailText = "", emailValue = "";

            foreach (DESTINATARIO dest in comp.WFD_DESTINATARIO.ToList())
            {
                EnviadosEmailsVM email = new EnviadosEmailsVM();
                email.ID = (int)dest.ID;
                email.VisualizouAlgumDoc = false;
                enviado.DocumentosEnviados = new List<string>();
                email.DocumentosVisualizados = new List<string>();

                //INFORMA QUAIS DOCUMENTOS CADA DESTINATARIO VISUALIZOU (SERA MUDADO)
                foreach (DocumentosCompartilhados docsVisualizados in comp.DocumentosCompartilhados.OrderBy(d => d.DocumentosDoFornecedor.DescricaoDeDocumentos.DESCRICAO).ToList())
                {
                    email.VisualizouAlgumDoc = true;
                    email.DocumentosVisualizados.Add(docsVisualizados.DocumentosDoFornecedor.DescricaoDeDocumentos.DESCRICAO);
                }

                email.Email = (dest.EMAIL_AVULSO) ? dest.EMAIL : dest.NOME;

                emailValue += TipoEmailVM.Destinatario + ":" + dest.ID.ToString() + ":" + dest.EMAIL + "|";
                emailText += (!string.IsNullOrEmpty(dest.NOME) ? dest.NOME.Replace(",", "") + " (" + dest.EMAIL + ")|" : dest.EMAIL + "|");

                enviado.Emails.Add(email);
            }
            if (!string.IsNullOrEmpty(emailValue))
                enviado.EmailsValue = emailValue.Substring(0, emailValue.Length - 1);
            if (!string.IsNullOrEmpty(emailText))
                enviado.EmailsText = emailText.Substring(0, emailText.Length - 1);

            //MONTA DOCUMENTOS ENVIADOS
            string docsComp = "";
            foreach (DocumentosCompartilhados docEnviado in comp.DocumentosCompartilhados.Distinct())
            {
                enviado.DocumentosEnviados.Add(docEnviado.DocumentosDoFornecedor.DescricaoDeDocumentos.DESCRICAO);
                docsComp += docEnviado.PJPF_DOCUMENTO_ID + "|";
            }

            if (enviado.DocumentosEnviados != null)
                enviado.DocumentosEnviadosText = String.Join("|", enviado.DocumentosEnviados.ToArray());

            if (!string.IsNullOrEmpty(docsComp))
                enviado.DocumentosEnviadosValue = docsComp.Substring(0, docsComp.Length - 1);

            ViewBag.LinkCancelar = Url.Action("EnviadosLst", "MeusDocumentos");
            return View(enviado);
        }

        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EnviadosFrm(EnviadosVM model, string Para, string Acao)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            if (Acao == "AlterarDisponibilidade")
            {
                Compartilhamentos compartilhamentos = Db.MEU_COMPARTILHAMENTOS.FirstOrDefault(c => c.ID == model.MeusCompartilhamentosId);
                compartilhamentos.SEM_PRAZO = model.SemPrazo;
                compartilhamentos.VALIDADE = (model.SemPrazo) ? null : (DateTime?)Convert.ToDateTime(model.Disponibilidade);

                Db.Entry(compartilhamentos).State = EntityState.Modified;
                Db.SaveChanges();

                return RedirectToAction("EnviadosLst", "MeusDocumentos", new { MensagemSucesso = "Disponibilidade alterada com Sucesso!" });
            }
            else if (Acao == "Reenviar")
            {
                List<EmailVM> listaEmails = MontaListaEmailsEnvio(model.Para);
                DateTime agora = DateTime.Now;

                List<int> listaDocsId = ConverteLista(model.DocumentosEnviadosValue, '|');
                List<DocumentosDoFornecedor> documentosSelecionados = Db.WFD_PJPF_DOCUMENTOS.Include("DescricaoDeDocumentos.TipoDeDocumento").Where(d => listaDocsId.Contains(d.ID)).ToList();

                MeusDocumentosEnviarVM enviarVM = new MeusDocumentosEnviarVM();
                enviarVM.Assunto = model.Assunto;
                enviarVM.DataValidade = Convert.ToDateTime(model.Disponibilidade);
                enviarVM.Mensagem = model.Mensagem;
                //enviarVM.ID
                enviarVM.MeusDocumentos = documentosSelecionados;
                enviarVM.Para = model.Para;
                enviarVM.SemPrazo = model.SemPrazo;

                if (SalvarCompartilhamento(enviarVM, contratanteId, true))
                {
                    return RedirectToAction("EnviadosLst", "MeusDocumentos", new { MensagemSucesso = "Documentos Reenviados com Sucesso!" });
                }
            }
            ViewBag.LinkCancelar = Url.Action("EnviadosLst", "MeusDocumentos");

            return View(model);
        }
        #endregion

        #region TIPO DOCUMENTO
        [Authorize]
        public ActionResult TipoDocumento()
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            List<TipoDeDocumento> listaTiposDocumentos = Db.WFD_TIPO_DOCUMENTOS
                .Where(t => t.CONTRATANTE_ID == contratanteId)
                .OrderBy(x => x.CONTRATANTE_ID)
                .ThenBy(x => x.DESCRICAO)
                .ThenBy(x => x.ATIVO)
                .ToList();

            ViewBag.ListaTipoDocumento = listaTiposDocumentos;

            return PartialView("_TipoDocumento");
        }

        [Authorize]
        public JsonResult TipoDocumentos(int? Id, string DescricaoTipoDocumento, bool? Ativo, string Acao)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            string msg = "";
            int erro = 0;

            try
            {
                TipoDeDocumento tipoDocumento = new TipoDeDocumento();
                if (Acao == "Incluir")
                {
                    tipoDocumento.CONTRATANTE_ID = contratanteId;
                    tipoDocumento.DESCRICAO = DescricaoTipoDocumento;
                    tipoDocumento.ATIVO = (bool)Ativo;

                    Db.Entry(tipoDocumento).State = EntityState.Added;
                    Db.SaveChanges();

                    msg = "Tipo de Documento Salvo com Sucesso!";
                }
                else if (Acao == "Alterar")
                {
                    tipoDocumento = Db.WFD_TIPO_DOCUMENTOS.FirstOrDefault(t => t.ID == (int)Id);
                    if (tipoDocumento != null)
                    {
                        tipoDocumento.DESCRICAO = DescricaoTipoDocumento;
                        tipoDocumento.ATIVO = (bool)Ativo;
                    }

                    Db.Entry(tipoDocumento).State = EntityState.Modified;
                    Db.SaveChanges();

                    msg = "Tipo de Documento Salvo com Sucesso!";
                }
                else if (Acao == "Excluir")
                {
                    tipoDocumento = Db.WFD_TIPO_DOCUMENTOS.FirstOrDefault(t => t.ID == (int)Id);
                    if (tipoDocumento != null)
                    {
                        Db.Entry(tipoDocumento).State = EntityState.Deleted;
                        Db.SaveChanges();
                    }

                    msg = "Tipo de Documento Excluído com Sucesso!";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                msg = "Erro ao tentar realizar a requisição do Usuário!";
                erro = 1;
            }

            List<TipoDeDocumento> listaTiposDocumentos = Db.WFD_TIPO_DOCUMENTOS.Where(t => t.CONTRATANTE_ID == contratanteId).ToList();

            ViewBag.ListaTipoDocumento = listaTiposDocumentos;

            //var listaTP = new[] { new { ID = 0, Descricao = "", Ativo = true } }.ToList();
            //listaTiposDocumentos.Clear();
            List<dynamic> listaTP = new List<dynamic>();
            foreach (TipoDeDocumento tp in listaTiposDocumentos)
            {
                listaTP.Add(new { ID = tp.ID, Descricao = tp.DESCRICAO, Ativo = tp.ATIVO, IDCH = tp.TIPO_DOCUMENTOS_CH_ID });
            }

            var retorno = new
            {
                Erro = erro,
                Msg = msg,
                ListaTiposDocumentos = listaTP
            };

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region DESCRICAO DOCUMENTO
        [Authorize]
        public ActionResult DescricaoDocumento(int TipoDocumentoId, string DescricaoDocumento)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            List<DescricaoDeDocumentos> listaDescricaoDocumentos = Db.WFD_DESCRICAO_DOCUMENTOS.Where(t => t.CONTRATANTE_ID == contratanteId && t.TIPO_DOCUMENTOS_ID == TipoDocumentoId).ToList();

            ViewBag.TipoDocumetnoId = TipoDocumentoId;
            ViewBag.TipoDocumentoSelecionado = DescricaoDocumento;
            ViewBag.ListaDescricaoDocumento = listaDescricaoDocumentos;

            return PartialView("_DescricaoDocumento");
        }

        [Authorize]
        public JsonResult DescricaoDocumentos(int? id, int tipoDocumentoIdSelecionado, string nomeDescricaoDocumento, bool? ativo, string acao)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            string msg = "";
            int erro = 0;

            try
            {
                DescricaoDeDocumentos DescricaoDeDocumentos = new DescricaoDeDocumentos();
                if (acao == "Incluir")
                {
                    DescricaoDeDocumentos.CONTRATANTE_ID = contratanteId;
                    DescricaoDeDocumentos.TIPO_DOCUMENTOS_ID = tipoDocumentoIdSelecionado;
                    DescricaoDeDocumentos.DESCRICAO = nomeDescricaoDocumento;
                    DescricaoDeDocumentos.ATIVO = (bool)ativo;

                    Db.Entry(DescricaoDeDocumentos).State = EntityState.Added;
                    Db.SaveChanges();

                    msg = "Descrição do Documento Salvo com Sucesso!";
                }
                else if (acao == "Alterar")
                {
                    DescricaoDeDocumentos = Db.WFD_DESCRICAO_DOCUMENTOS.FirstOrDefault(t => t.ID == (int)id);
                    if (DescricaoDeDocumentos != null)
                    {
                        DescricaoDeDocumentos.DESCRICAO = nomeDescricaoDocumento;
                        DescricaoDeDocumentos.ATIVO = (bool)ativo;
                    }

                    Db.Entry(DescricaoDeDocumentos).State = EntityState.Modified;
                    Db.SaveChanges();

                    msg = "Descrição do Documento Salvo com Sucesso!";
                }
                else if (acao == "Excluir")
                {
                    DescricaoDeDocumentos = Db.WFD_DESCRICAO_DOCUMENTOS.FirstOrDefault(t => t.ID == (int)id);
                    if (DescricaoDeDocumentos != null)
                    {
                        Db.Entry(DescricaoDeDocumentos).State = EntityState.Deleted;
                        Db.SaveChanges();
                    }

                    msg = "Descrição de Documento Excluído com Sucesso!";
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                msg = "Erro ao tentar realizar a requisição do Usuário!";
                erro = 1;
            }

            ViewBag.ListaDescricaoDocumento = Db.WFD_DESCRICAO_DOCUMENTOS.Where(t => t.CONTRATANTE_ID == contratanteId && t.TIPO_DOCUMENTOS_ID == tipoDocumentoIdSelecionado).ToList();

            List<dynamic> listaDD = new List<dynamic>();
            foreach (DescricaoDeDocumentos dd in ViewBag.ListaDescricaoDocumento)
            {
                listaDD.Add(new { ID = dd.ID, Descricao = dd.DESCRICAO, Ativo = dd.ATIVO, IDCH = dd.DESCRICAO_DOCUMENTOS_CH_ID });
            }

            var retorno = new
            {
                Erro = erro,
                Msg = msg,
                ListaDescricaoDocumentos = listaDD
            };

            return Json(retorno, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region MINHA FICHA CADASTRAL

        [Authorize]
        public ActionResult MinhaFichaCadastral()
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            FichaCadastralWebForLinkVM fichaCadastralVM = PopularMinhaFichaView(contratanteId, null, null);
            return View(fichaCadastralVM);
        }

        [Authorize]
        [HttpPost]
        public ActionResult MinhaFichaCadastral(FichaCadastralWebForLinkVM model, string ServicosSelecionados, string MateriaisSelecionados)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            WFD_CONTRATANTE_PJPF contratanteFornecedor = _contratanteService.BuscarPorTipoPjpfId(1, contratanteId);
            Contratante contratante = _contratanteService.BuscarPorId(contratanteId);

            FichaCadastralWebForLinkVM fichaCadastralVM = model;
            fichaCadastralVM.ContratanteID = contratanteId;
            fichaCadastralVM.CNPJ_CPF = contratante.CNPJ;
            fichaCadastralVM.TipoFornecedor = (int)contratante.TIPO_CADASTRO_ID;
            fichaCadastralVM.RazaoSocial = contratante.RAZAO_SOCIAL;
            fichaCadastralVM.NomeFantasia = contratante.NOME_FANTASIA;

            FornecedoresVM robo = new FornecedoresVM();
            robo.RoboReceitaCNPJ = new RoboReceitaCNPJ();
            robo.RoboSintegra = new RoboSintegra();
            robo.RoboSimples = new RoboSimples();

            if (contratanteFornecedor != null)
            {
                Fornecedor pjpf = contratanteFornecedor.WFD_PJPF;

                if (fichaCadastralVM.TipoFornecedor == 1)
                {
                    robo.RoboReceitaCNPJ = RoboReceitaCNPJ.ModelToViewModel(contratanteFornecedor.WFD_PJPF.ROBO);
                    robo.RoboSintegra = RoboSintegra.ModelToViewModel(contratanteFornecedor.WFD_PJPF.ROBO);
                    robo.RoboSimples = RoboSimples.ModelToViewModel(contratanteFornecedor.WFD_PJPF.ROBO);
                }
                else
                {
                    robo.RoboReceitaCPF = RoboReceitaCPF.ModelToViewModel(contratanteFornecedor.WFD_PJPF.ROBO);
                }
                robo.ContratanteID = contratanteId;
            }

            //MEUS ENDERECOS
            if (model.DadosEnderecos != null)
                if (model.DadosEnderecos.Any())
                {
                    var enderecos = DadosEnderecosVM.ViewModelToModel(model.DadosEnderecos.ToList());
                    _fornecedorEnderecoService.AlterarFornecedorEndereco(enderecos, model.ContratanteFornecedorID);
                    model.DadosEnderecos = DadosEnderecosVM.ModelToViewModel(enderecos);
                }

            //MEUS DADOS BANCARIOS
            if (model.DadosBancarios != null)
            {
                if (model.DadosBancarios.Any())
                {
                    ManterMeusBancos(model);
                    _fornecedorBancoService.ManterMeusBancos(DadosBancariosVM.ViewModelToModel(model.DadosBancarios.ToList()), model.ContratanteFornecedorID);
                }
            }

            //MEUS CONTATOS
            if (model.DadosContatos != null)
                if (model.DadosContatos.Any())
                {
                    var contatos = DadosContatoVM.ViewModelToModel(model.DadosContatos.ToList());
                    _fornecedorContatosService.ManterMeusContatos(contatos, model.ContratanteFornecedorID);
                    model.DadosContatos = DadosContatoVM.ModelToViewModel(contatos);
                }

            //UNSPSC
            List<TIPO_UNSPSC> listaUnspsc = _servicosMateriaisService.BuscarListaPorID(ServicosSelecionados.Split(new Char[] { '|' }), MateriaisSelecionados.Split(new Char[] { '|' }));
            FornecedorUnspscVM unspscVM = new FornecedorUnspscVM();
            model.FornecedoresUnspsc = unspscVM.PreencheModelUnspsc(model.PJPFID, null, listaUnspsc);
            var unspscs = FornecedorUnspscVM.ViewModelToModel(model.FornecedoresUnspsc.ToList());
            _servicosMateriaisService.ManterMeusMateriaisServicos(unspscs, model.PJPFID);

            ViewBag.FornecedorVM = robo;
            ViewBag.MinhaFicha = true;
            PersistirDadosEmMemoria();
            PersistirDadosEnderecoEmMemoria();
            ModelState.Clear();

            return View(fichaCadastralVM);
        }

        #endregion

        #region COMPARTILHADOS

        //COMPARTILHADOS
        public ActionResult Compartilhados(string chaveurl)
        {
            int idComp = 0;
            string chave = "";
            int idDest = 0;
            DocumentosCompartilhadosVM modelo = new DocumentosCompartilhadosVM();
            try
            {

                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                    Int32.TryParse(param.First(p => p.Name == "idComp").Value, out idComp);
                    chave = param.First(p => p.Name == "chave").Value;
                    Int32.TryParse(param.First(p => p.Name == "idDest").Value, out idDest);
                }

                if (idComp != 0)
                {
                    Compartilhamentos compartilhamentos = Db.MEU_COMPARTILHAMENTOS
                        .Include("WFD_DESTINATARIO.MEU_COMPARTILHAMENTOS")
                        .Include("WFD_PJPF_BANCO.T_BANCO")
                        .Include("WFD_PJPF_CONTATOS")
                        .Include("DocumentosCompartilhados.DocumentosDoFornecedor.DescricaoDeDocumentos.TipoDeDocumento")
                        .Include("DocumentosCompartilhados.DocumentosDoFornecedor.WFD_ARQUIVOS")
                        .FirstOrDefault(c => c.ID == idComp);
                    modelo.DocumentosCompartilhados = new List<DocsCompartilhadosVM>();
                    if (compartilhamentos != null)
                    {
                        if (compartilhamentos.CHAVE == chave)
                        {
                            Contratante contratante = Db.Contratante.Include("Usuario")
                                .FirstOrDefault(c => c.ID == compartilhamentos.CONTRATANTE_ID);
                            ViewBag.Nome = (contratante.TIPO_CADASTRO_ID == 1) ? contratante.Usuario.FirstOrDefault(u => u.PRINCIPAL == true).NOME : contratante.RAZAO_SOCIAL;
                            ViewBag.Imagem = (contratante.TIPO_CADASTRO_ID == 1) ? "semfoto.png" : "semlogo.png";

                            var caminhoFisico = Server.MapPath("/ImagensUsuarios");
                            var arquivo = string.Concat("ImagemContratante", contratante.ID, ".png");
                            if (System.IO.File.Exists(string.Concat(caminhoFisico, "/", arquivo)))
                                ViewBag.Imagem = arquivo;
                            else
                                ViewBag.Imagem = ((int)contratante.TIPO_CADASTRO_ID == 1) ? "semfoto.png" : "semlogo.png";

                            DocsCompartilhadosVM docsCompartilhadosVM;
                            foreach (DocumentosCompartilhados c in compartilhamentos.DocumentosCompartilhados)
                            {
                                docsCompartilhadosVM = Mapper.Map<DocsCompartilhadosVM>(c);
                                docsCompartilhadosVM.UrlArquivo = Url.Action("MeusDocumentosArquivo", "MeusDocumentos", new { chaveurl = Cripto.Criptografar(string.Format("idComp={0}&idDoc={1}&idDest={2}&local=Externo", idComp, c.DocumentosDoFornecedor.ID, idDest), Key) });
                                modelo.DocumentosCompartilhados.Add(docsCompartilhadosVM);
                            }

                            int[] bancos = compartilhamentos.WFD_PJPF_BANCO.Select(x => x.ID).ToArray();
                            int[] contatos = compartilhamentos.WFD_PJPF_CONTATOS.Select(x => x.ID).ToArray();

                            if ((bool)compartilhamentos.RESTRITA)
                                modelo.FichaCadastral = PopularMinhaFichaView(contratante.ID, bancos, contatos);
                        }
                    }
                    return View(modelo);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }

            return View();

        }

        [HttpPost]
        [Authorize]
        public ContentResult UploadArquivoContratante(string arqTmp)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            string cnpj_cpf = Db.Contratante.FirstOrDefault(x => x.ID == contratanteId).CNPJ;
            var caminho = _configService.BuscarConfigGeral().CAMINHO_ARQUIVOS;
            return base.UploadArquivo(cnpj_cpf, arqTmp, caminho);
        }

        [HttpPost]
        public PartialViewResult ModalPesquisarEmail(string ids)
        {
            MeusDocumentosPesquisarEmailVM modelo = new MeusDocumentosPesquisarEmailVM();
            modelo.IdsIncluidos = ids;
            return PartialView("~/Views/MeusDocumentos/_Modal_PesquisarEmail.cshtml", modelo);
        }

        [HttpPost]
        public PartialViewResult GridModalPesquisarEmail(MeusDocumentosPesquisarEmailVM modelo)
        {
            int ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            string[] arrayDestinatarios = { };
            if (!string.IsNullOrEmpty(modelo.IdsIncluidos)) arrayDestinatarios = modelo.IdsIncluidos.Split(new char[] { ',' }, StringSplitOptions.None);

            #region Popular Destinatarios
            List<DESTINATARIO> destinatarios = Db.WFD_DESTINATARIO
        .Where(d => d.CONTRATANTE_ID == ContratanteId
            && d.ATIVO
            && (d.EMAIL.Contains(modelo.Email) || d.NOME.Contains(modelo.Nome) || d.EMPRESA.Contains(modelo.Empresa)))
            .ToList();

            if (destinatarios.Any())
                destinatarios.ForEach(x =>
                {
                    var mapaVM = MeusDocumentosPesquisarEmailGridVM.ModelToViewModel(x);
                    mapaVM.IsChecked = !string.IsNullOrEmpty(Array.Find(arrayDestinatarios, y => y == x.ID.ToString()));
                    modelo.Grid.Add(mapaVM);
                });
            #endregion

            #region Popular PjpfBaseDestinatarios
            List<FORNECEDORBASE_CONTATOS> baseContatos = Db.WFD_PJPF_BASE_CONTATOS
                        .Include("WFD_PJPF_BASE")
                        .Where(d => d.WFD_PJPF_BASE.CONTRATANTE_ID == ContratanteId
                            && d.WFD_PJPF_BASE.ATIVO
                            && (d.EMAIL.Contains(modelo.Email) || d.NOME.Contains(modelo.Nome) || d.WFD_PJPF_BASE.RAZAO_SOCIAL.Contains(modelo.Empresa) || d.WFD_PJPF_BASE.NOME_FANTASIA.Contains(modelo.Empresa)))
                            .ToList();

            if (baseContatos.Any())
                baseContatos.ForEach(x =>
                {
                    var mapaVM = MeusDocumentosPesquisarEmailGridVM.ModelToViewModel(x);
                    mapaVM.IsChecked = !string.IsNullOrEmpty(Array.Find(arrayDestinatarios, y => y == x.ID.ToString()));
                    modelo.Grid.Add(mapaVM);
                });
            #endregion

            #region Popular PjPfDestinatarios
            List<FORNECEDOR_CONTATOS> pjpfDestinatarios = Db.WFD_PJPF_CONTATOS
                           .Include("WFD_CONTRATANTE_PJPF.WFD_PJPF")
                           .Where(d => d.WFD_CONTRATANTE_PJPF.CONTRATANTE_ID == ContratanteId
                               && d.WFD_CONTRATANTE_PJPF.WFD_PJPF.ATIVO
                               && (d.EMAIL.Contains(modelo.Email) || d.NOME.Contains(modelo.Nome) || d.WFD_CONTRATANTE_PJPF.WFD_PJPF.RAZAO_SOCIAL.Contains(modelo.Empresa) || d.WFD_CONTRATANTE_PJPF.WFD_PJPF.NOME_FANTASIA.Contains(modelo.Empresa)))
                               .ToList();

            if (pjpfDestinatarios.Any())
                pjpfDestinatarios.ForEach(x =>
                {
                    var mapaVM = Mapper.Map<MeusDocumentosPesquisarEmailGridVM>(x);
                    mapaVM.IsChecked = !string.IsNullOrEmpty(Array.Find(arrayDestinatarios, y => y == x.ID.ToString()));
                    modelo.Grid.Add(mapaVM);
                });
            #endregion

            return PartialView("~/Views/MeusDocumentos/_Grid_PesquisarEmail.cshtml", modelo.Grid);
        }

        public FileResult MeusDocumentosArquivo(string chaveurl)
        {

            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            int idDoc = 0, idComp = 0, idDest = 0;
            string local = "";

            if (param.FirstOrDefault(p => p.Name == "idDoc") != null)
            {
                var docId = param.FirstOrDefault(p => p.Name == "idDoc");
                if (docId != null)
                    Int32.TryParse(docId.Value, out idDoc);
            }
            if (param.FirstOrDefault(p => p.Name == "idComp") != null)
            {
                var compId = param.FirstOrDefault(p => p.Name == "idComp");
                if (compId != null)
                    Int32.TryParse(compId.Value, out idComp);
            }
            if (param.FirstOrDefault(p => p.Name == "idDest") != null)
            {
                var destId = param.FirstOrDefault(p => p.Name == "idDest");
                if (destId != null)
                    Int32.TryParse(destId.Value, out idDest);
            }
            if (param.FirstOrDefault(p => p.Name == "local") != null)
            {
                var localId = param.FirstOrDefault(p => p.Name == "local");
                if (localId != null)
                    local = localId.Value;
            }

            if (local != "")
            {
                if (local == "Externo")
                {
                    if (idDoc == 0 || idComp == 0 || idDest == 0)
                    {
                        return File("none", "none");
                    }
                    else
                    {

                        //WFD_MEUS_DOCUMENTOS_COMPARTILHADOS meuDocComp = Db.WFD_MEUS_DOCUMENTOS_COMPARTILHADOS.FirstOrDefault(d => d.MEUS_COMPARTILHAMENTOS_ID == idComp && d.DESTINATARIO_ID == idDest && d.MEUS_DOCUMENTOS_ID == idDoc);
                        //if (meuDocComp != null)
                        //{
                        //    using (var trans = Db.Database.BeginTransaction())
                        //    {
                        //        try
                        //        {
                        //            meuDocComp.VISUALIZADO = true;
                        //            meuDocComp.DATA_VISUALIZACAO = DateTime.Now;

                        //            Db.Entry(meuDocComp).State = EntityState.Modified;
                        //            Db.SaveChanges();
                        //            trans.Commit();
                        //        }
                        //        catch (Exception ex)
                        //        {
                        //            Log.Error(ex.Message);
                        //            trans.Rollback();
                        //        }
                        //    }
                        //}
                    }
                }
                else if (local == "Interno")
                {
                    if (idDoc == 0)
                    {
                        return File("none", "none");
                    }
                }

                DocumentosDoFornecedor doc = Db.WFD_PJPF_DOCUMENTOS.FirstOrDefault(d => d.ID == idDoc);
                ARQUIVOS arquivo = Db.WFD_ARQUIVOS.FirstOrDefault(a => a.ID == doc.ARQUIVO_ID);
                if (doc == null || arquivo == null)
                {
                    return File("none", "none");
                }

                var caminho = arquivo.CAMINHO + arquivo.ID + "##" + arquivo.NOME_ARQUIVO;
                return File(caminho, arquivo.TIPO_ARQUIVO, arquivo.NOME_ARQUIVO);
            }

            return File("none", "none");

        }

        public JsonResult ReceitaFederalCNPJ()
        {
            RoboReceitaCNPJ roboCNPJ = new RoboReceitaCNPJ();
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            try
            {
                var wfdContratante = Db.Contratante.FirstOrDefault(x => x.ID == contratanteId);
                var contratantePjPf = Db.WFD_CONTRATANTE_PJPF
                    .Include("Fornecedor")
                    .FirstOrDefault(x => x.CONTRATANTE_ID == contratanteId && x.TP_PJPF == 1);

                string path = Server.MapPath("~/");
                roboCNPJ = roboCNPJ.CarregaRoboCNPJ(wfdContratante.CNPJ, path);

                ROBO robo = new ROBO();
                roboCNPJ.GravaRoboReceita(roboCNPJ, ref robo);
                Db.Entry(robo).State = EntityState.Added;

                ROBO_LOG entityLog = new ROBO_LOG()
                {
                    COD_RETORNO = roboCNPJ.Code,
                    DATA = DateTime.Now,
                    MENSAGEM = roboCNPJ.Data.Message,
                    ROBO = EnumRobo.ReceitaFederal.ToString(),
                    CONTRATANTE_ID = contratanteId
                };
                Db.Entry(entityLog).State = EntityState.Added;

                Fornecedor pjpf;
                bool modificacao = false;
                if (contratantePjPf != null)
                {
                    pjpf = contratantePjPf.WFD_PJPF;
                    modificacao = true;
                }
                else
                {
                    pjpf = new Fornecedor();

                    contratantePjPf = new WFD_CONTRATANTE_PJPF();
                    contratantePjPf.CONTRATANTE_ID = contratanteId;
                    contratantePjPf.CRIA_DT = DateTime.Now;
                    contratantePjPf.CRIA_USUARIO_ID = usuarioId;
                    contratantePjPf.TP_PJPF = 1;
                    contratantePjPf.WFD_PJPF = pjpf;
                }

                pjpf.CNPJ = wfdContratante.CNPJ;
                pjpf.CONTRATANTE_ID = contratanteId;
                pjpf.BAIRRO = robo.RF_BAIRRO;
                pjpf.CEP = robo.RF_CEP;
                pjpf.CIDADE = robo.RF_MUNICIPIO;
                //pjpf.CNAE = robo.RF_CNAE_COD_PRINCIPAL;
                pjpf.COMPLEMENTO = robo.RF_COMPLEMENTO;
                pjpf.ENDERECO = robo.RF_LOGRADOURO;
                pjpf.NOME_FANTASIA = robo.RF_NOME_FANTASIA;
                pjpf.NUMERO = robo.RF_NUMERO;
                pjpf.RAZAO_SOCIAL = robo.RECEITA_FEDERAL_RAZAO_SOCIAL;
                pjpf.RF_CONSULTA_DTHR = robo.RF_CONSULTA_DTHR;
                pjpf.RF_SIT_CADASTRAL_CNPJ = robo.RF_SIT_CADASTRAL_CNPJ;
                pjpf.RF_SIT_CADASTRAL_CNPJ_DT = robo.RF_SIT_CADSTRAL_CNPJ_DT;
                pjpf.ROBO_ID = robo.ID;
                pjpf.TIPO_PJPF_ID = 1;
                pjpf.UF = robo.RF_UF;

                if (modificacao)
                    Db.Entry(pjpf).State = EntityState.Modified;
                else
                    Db.Entry(contratantePjPf).State = EntityState.Added;

                Db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                throw;
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                Log.Error(string.Format("Error ao executar o método ReceitaFederalCNPJ: {0}", ex));
            }

            return Json(roboCNPJ);
        }

        public JsonResult ReceitaFederalCPF(string cpf, int contratanteId, string dataNascimento)
        {
            string path = Server.MapPath("~/");
            RoboReceitaCPF RoboCPF = new RoboReceitaCPF();

            try
            {
                ROBO robo = new ROBO();

                RoboCPF = RoboCPF.CarregaRoboCPF(cpf, dataNascimento, path);
                int UsuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

                RoboCPF.GravaRoboCpf(RoboCPF, ref robo);
                Db.Entry(robo).State = EntityState.Added;

                ROBO_LOG entityLog = new ROBO_LOG()
                {
                    COD_RETORNO = RoboCPF.Code,
                    DATA = DateTime.Now,
                    MENSAGEM = RoboCPF.Data.Message,
                    ROBO = EnumRobo.ReceitaFederalPF.ToString(),
                    CONTRATANTE_ID = contratanteId
                };

                Db.Entry(entityLog).State = EntityState.Added;
                Db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:", eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                throw;
            }
            catch (Exception ex)
            {
                ViewBag.MensagemErro = "Erro ao tentar Incluir o Novo Fornecedor!";
                //dbContextTransaction.Rollback();
                Log.Error(string.Format("Error ao executar o método ReceitaFederalCPF: {0}", ex));
            }

            return Json(RoboCPF);
        }

        public JsonResult Sintegra()
        {
            string path = Server.MapPath("~/");
            RoboSintegra sintegra = new RoboSintegra();
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            try
            {
                var contratantePjPf = Db.WFD_CONTRATANTE_PJPF
                    .Include("Contratante")
                    .Include("Fornecedor.ROBO")
                    .FirstOrDefault(x => x.CONTRATANTE_ID == contratanteId && x.TP_PJPF == 1);
                var pjpf = contratantePjPf.WFD_PJPF;
                var robo = contratantePjPf.WFD_PJPF.ROBO;

                sintegra = sintegra.CarregaSintegra(contratantePjPf.WFD_PJPF.UF, contratantePjPf.WFD_PJPF.CNPJ, path);

                if (sintegra.Code < 100)
                {
                    sintegra.GravaRoboSintegra(sintegra, ref robo);
                    Db.Entry(robo).State = EntityState.Modified;

                    ROBO_LOG entityLog = new ROBO_LOG()
                    {
                        COD_RETORNO = sintegra.Code,
                        DATA = DateTime.Now,
                        MENSAGEM = sintegra.Data.Message,
                        ROBO = EnumRobo.Sintegra.ToString(),
                        CONTRATANTE_ID = contratanteId
                    };
                    Db.Entry(entityLog).State = EntityState.Added;

                    pjpf.SINT_IE_COD = robo.SINT_IE_COD;

                    Db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error ao executar o método Sintegra: {0}", ex));
            }

            return Json(sintegra, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SimplesNacional()
        {
            string path = Server.MapPath("~/");
            RoboSimples roboSimples = new RoboSimples();
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            try
            {
                var contratantePjPf = Db.WFD_CONTRATANTE_PJPF
                    .Include("Contratante")
                    .Include("Fornecedor.ROBO")
                    .FirstOrDefault(x => x.CONTRATANTE_ID == contratanteId && x.TP_PJPF == 1);
                var pjpf = contratantePjPf.WFD_PJPF;
                var robo = contratantePjPf.WFD_PJPF.ROBO;

                roboSimples = roboSimples.CarregaSimplesCNPJ(contratantePjPf.WFD_PJPF.CNPJ, path);

                if (roboSimples.Code < 100)
                {
                    roboSimples.GravaRoboSimples(roboSimples, ref robo);
                    Db.Entry(robo).State = EntityState.Modified;

                    ROBO_LOG entityLog = new ROBO_LOG()
                    {
                        COD_RETORNO = roboSimples.Code,
                        DATA = DateTime.Now,
                        MENSAGEM = roboSimples.Data.Message,
                        ROBO = EnumRobo.SimplesNacional.ToString(),
                        CONTRATANTE_ID = contratanteId
                    };
                    Db.Entry(entityLog).State = EntityState.Added;
                    Db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Error ao executar o método SimplesNacional: {0}", ex));
            }

            return Json(roboSimples, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaTipoDocumento(string tipo)
        {
            List<SelectListItem> listDocumentos = MontaTipoDocumento(tipo);

            return Json(listDocumentos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ListaDescricaoDocumento(int? tipoDocumento, string tipo, bool filtrado = false)
        {
            List<SelectListItem> listDocumentos = MontaDescricaoDocumento(tipoDocumento, tipo, filtrado);

            return Json(listDocumentos, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscaEmails(string chave)
        {
            int ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId");


            List<DESTINATARIO> destinatarios = Db.WFD_DESTINATARIO
                .Where(d => d.CONTRATANTE_ID == ContratanteId
                    && d.ATIVO
                    && (d.EMAIL.Contains(chave) || d.NOME.Contains(chave)))
                    .ToList();
            List<EmailsVM> emails = MontaListaEmailVM(destinatarios);

            return Json(emails, JsonRequestBehavior.AllowGet);
        }

        public JsonResult BuscaEmailsPorId(string ids)
        {
            int ContratanteId = (int)Geral.PegaAuthTicket("ContratanteId");


            string[] arrayDestinatarios = { };
            if (!string.IsNullOrEmpty(ids)) arrayDestinatarios = ids.Split(new char[] { ',' }, StringSplitOptions.None);


            List<EmailsVM> emails = new List<EmailsVM>();
            List<DESTINATARIO> destinatarios = Db.WFD_DESTINATARIO
                .Where(d => d.CONTRATANTE_ID == ContratanteId
                    && d.ATIVO
                    && arrayDestinatarios.Contains(d.ID.ToString()))
                    .ToList();
            if (destinatarios.Any()) emails = Mapper.Map<List<EmailsVM>>(destinatarios);

            #region Popular PjpfBaseDestinatarios
            List<FORNECEDORBASE_CONTATOS> baseContatos = Db.WFD_PJPF_BASE_CONTATOS
                        .Include("FORNECEDORBASE")
                        .Where(d => d.WFD_PJPF_BASE.CONTRATANTE_ID == ContratanteId
                            && d.WFD_PJPF_BASE.ATIVO
                            && arrayDestinatarios.Contains(d.ID.ToString()))
                            .ToList();

            if (baseContatos.Any()) emails = EmailsVM.ModelToViewModel(baseContatos);

            #endregion

            #region Popular PjPfDestinatarios
            List<FORNECEDOR_CONTATOS> pjpfDestinatarios = Db.WFD_PJPF_CONTATOS
                       .Include("WFD_CONTRATANTE_PJPF.Fornecedor")
                       .Where(d => d.WFD_CONTRATANTE_PJPF.CONTRATANTE_ID == ContratanteId
                           && d.WFD_CONTRATANTE_PJPF.WFD_PJPF.ATIVO
                           && arrayDestinatarios.Contains(d.ID.ToString()))
                           .ToList();

            if (pjpfDestinatarios.Any()) emails = EmailsVM.ModelToViewModel(pjpfDestinatarios);
            #endregion
            return Json(emails, JsonRequestBehavior.AllowGet);
        }

        private FichaCadastralWebForLinkVM PopularMinhaFichaView(int contratanteId, int[] bancos, int[] contatos)
        {
            WFD_CONTRATANTE_PJPF contratanteFornecedor = _contratanteService.BuscarPorTipoPjpfId(1, contratanteId);
            Contratante contratante = ContratanteOuSeraOContratanteDoFornecedorOuUmSelectNaTabelaDeContratante(contratanteId, contratanteFornecedor);

            FichaCadastralWebForLinkVM fichaCadastralVM = new FichaCadastralWebForLinkVM(true);

            FornecedoresVM robo = new FornecedoresVM();
            robo.RoboReceitaCNPJ = new RoboReceitaCNPJ();
            robo.RoboSintegra = new RoboSintegra();
            robo.RoboSimples = new RoboSimples();

            if (contratanteFornecedor != null)
            {
                Fornecedor pjpf = contratanteFornecedor.WFD_PJPF;

                fichaCadastralVM = FichaCadastralWebForLinkVM.ModelToViewModel(pjpf);

                fichaCadastralVM.ContratanteFornecedorID = contratanteFornecedor.ID;
                fichaCadastralVM.PJPFID = pjpf.ID;

                EnderecosBancosEContatosDeFichaCadastral(bancos, contatos, contratanteFornecedor, fichaCadastralVM);

                if (contratante.TIPO_CADASTRO_ID == 1)
                {
                    robo.RoboReceitaCNPJ = RoboReceitaCNPJ.ModelToViewModel(contratanteFornecedor.WFD_PJPF.ROBO);
                    robo.RoboSintegra = RoboSintegra.ModelToViewModel(contratanteFornecedor.WFD_PJPF.ROBO);
                    robo.RoboSimples = RoboSimples.ModelToViewModel(contratanteFornecedor.WFD_PJPF.ROBO);
                }
                else
                {
                    robo.RoboReceitaCPF = RoboReceitaCPF.ModelToViewModel(contratanteFornecedor.WFD_PJPF.ROBO);
                }
                robo.ContratanteID = contratanteId;
                //Mapear UNSPSC
                FornecedorUnspscVM unspscVM = new FornecedorUnspscVM();
                fichaCadastralVM.FornecedoresUnspsc = FornecedorUnspscVM.ModelToViewModel(pjpf.FornecedorServicoMaterialList.Where(x => x.DT_EXCLUSAO == null).ToList());
                ViewBag.DataAtuUnspsc = fichaCadastralVM.DataAtualizacaoUnspsc;
            }
            fichaCadastralVM.ContratanteID = contratanteId;
            fichaCadastralVM.CNPJ_CPF = contratante.CNPJ;
            fichaCadastralVM.TipoFornecedor = (int)contratante.TIPO_CADASTRO_ID;
            fichaCadastralVM.RazaoSocial = contratante.RAZAO_SOCIAL;
            fichaCadastralVM.NomeFantasia = contratante.NOME_FANTASIA;

            ViewBag.FornecedorVM = robo;
            ViewBag.MinhaFicha = true;
            PersistirDadosEmMemoria();
            PersistirDadosEnderecoEmMemoria();
            return fichaCadastralVM;
        }

        private void EnderecosBancosEContatosDeFichaCadastral(int[] bancos, int[] contatos, WFD_CONTRATANTE_PJPF contratanteFornecedor, FichaCadastralWebForLinkVM fichaCadastralVM)
        {
            fichaCadastralVM.DadosEnderecos = DadosEnderecosVM.ModelToViewModel(contratanteFornecedor.WFD_PJPF_ENDERECO.ToList());
            fichaCadastralVM.DadosBancarios = DadosBancariosVM.ModelToViewModel(RetornarTodosBancosVindoDaFicha(bancos, contratanteFornecedor));
            fichaCadastralVM.DadosContatos = DadosContatoVM.ModelToViewModel(RetornarTodosContatosVindosDaFicha(contatos, contratanteFornecedor));

            if (fichaCadastralVM.DadosEnderecos.Count == 0) fichaCadastralVM.DadosEnderecos.Add(new DadosEnderecosVM { });
            if (fichaCadastralVM.DadosBancarios.Count == 0) fichaCadastralVM.DadosBancarios.Add(new DadosBancariosVM { });
            if (fichaCadastralVM.DadosContatos.Count == 0) fichaCadastralVM.DadosContatos.Add(new DadosContatoVM { });
        }

        private List<FORNECEDOR_CONTATOS> RetornarTodosContatosVindosDaFicha(int[] contatos, WFD_CONTRATANTE_PJPF contratanteFornecedor)
        {
            return contatos == null
                ? contratanteFornecedor.WFD_PJPF_CONTATOS.ToList()
                : contratanteFornecedor.WFD_PJPF_CONTATOS.Where(x => contatos.Contains(x.ID)).ToList();
        }

        private List<BancoDoFornecedor> RetornarTodosBancosVindoDaFicha(int[] bancos, WFD_CONTRATANTE_PJPF contratanteFornecedor)
        {
            return bancos == null
                ? contratanteFornecedor.BancoDoFornecedor.ToList()
                : contratanteFornecedor.BancoDoFornecedor.Where(x => bancos.Contains(x.ID)).ToList();
        }

        private Contratante ContratanteOuSeraOContratanteDoFornecedorOuUmSelectNaTabelaDeContratante(int contratanteId, WFD_CONTRATANTE_PJPF contratanteFornecedor)
        {
            return contratanteFornecedor.WFD_CONTRATANTE == null
                            ? _contratanteService.BuscarPorId(contratanteId)
                            : contratanteFornecedor.WFD_CONTRATANTE;
        }

        private List<int> ConverteLista(string docsSelecionados, Char separador)
        {
            if (string.IsNullOrEmpty(docsSelecionados))
                return null;

            int idAux;
            List<int> documentos = new List<int>();

            foreach (string documentoID in docsSelecionados.Split(separador))
            {
                if (Int32.TryParse(documentoID, out idAux))
                    documentos.Add(idAux);
            }

            return documentos;
        }

        private List<EmailsVM> MontaListaEmailVM(List<DESTINATARIO> destinatarios)
        {
            List<EmailsVM> emails = new List<EmailsVM>();
            foreach (DESTINATARIO destinatario in destinatarios)
            {
                var nomeExibido = string.Empty;
                if (!string.IsNullOrEmpty(destinatario.NOME)) destinatario.NOME.Replace(",", "");
                if (!string.IsNullOrEmpty(destinatario.SOBRENOME)) destinatario.SOBRENOME.Replace(",", "");
                nomeExibido = string.Format("{0} {1}{2}", destinatario.NOME, destinatario.SOBRENOME, " (" + destinatario.EMAIL + ")");


                var nomeESobrenome = !string.IsNullOrEmpty(nomeExibido)
                    ? nomeExibido
                    : destinatario.EMAIL;
                emails.Add(new EmailsVM
                {
                    value = TipoEmailVM.Destinatario + ":" + destinatario.ID.ToString() + ":" + destinatario.EMAIL,
                    text = nomeESobrenome + string.Format("{0}", !string.IsNullOrEmpty(destinatario.EMPRESA) ? " - " + destinatario.EMPRESA : ""),
                });
            }

            return emails;
        }

        private List<EmailVM> MontaListaEmailsEnvio(string Emails)
        {
            string[] vEmails = Emails.Split(new Char[] { ',' });
            List<EmailVM> listaEmails = new List<EmailVM>();
            int DocID;

            foreach (string email in vEmails)
            {
                string[] vObj = email.Split(new Char[] { ':' });
                Int32.TryParse(vObj[1], out DocID);

                if (vObj[0] != TipoEmailVM.EmailAvulso)
                    listaEmails.Add(new EmailVM { Tipo = vObj[0], ID = DocID, Endereco = vObj[2] });
                else
                    listaEmails.Add(new EmailVM { Tipo = vObj[0], Endereco = vObj[1] });
            }

            return listaEmails;

        }

        //METODOS AUXILIARES DAS ACTIONS
        private List<DocumentosDoFornecedor> BuscaDocumentos(int ContratanteId, int? TipoDocumento, int? DescricaoDocumento, bool? Ativo, int Pagina, string DocumentosSelecionados, out int TotalRegistro)
        {
            List<DocumentosDoFornecedor> meusDocumentos;
            var predicate = PredicateBuilder.New<DocumentosDoFornecedor>();
            predicate = predicate.And(d => d.WFD_CONTRATANTE_PJPF.CONTRATANTE_ID == ContratanteId);

            if (Ativo != null)
                predicate = predicate.And(d => d.ATIVO == Ativo);
            if (TipoDocumento != null)
                predicate = predicate.And(d => d.DescricaoDeDocumentos.TIPO_DOCUMENTOS_ID == (int)TipoDocumento);
            if (DescricaoDocumento != null)
                predicate = predicate.And(d => d.DESCRICAO_DOCUMENTO_ID == (int)DescricaoDocumento);
            if (!string.IsNullOrEmpty(DocumentosSelecionados))
            {
                List<int> documentos = ConverteLista(DocumentosSelecionados, '|');
                predicate = predicate.And(d => !documentos.Contains(d.ID));
            }

            TotalRegistro = (int)Db.WFD_PJPF_DOCUMENTOS.AsExpandable().Count(predicate);
            if (Pagina > 0)
                meusDocumentos = Db.WFD_PJPF_DOCUMENTOS
                    .Include("DescricaoDeDocumentos.TipoDeDocumento")
                    .Include("WFD_ARQUIVOS")
                    .AsExpandable()
                    .Where(predicate)
                    .OrderBy(d => d.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO)
                    .ThenBy(d => d.DescricaoDeDocumentos.DESCRICAO)
                    .ThenBy(d => d.DATA_VENCIMENTO.Value)
                    .Skip(TamanhoPagina * (Pagina - 1))
                    .Take(TamanhoPagina)
                    .ToList();
            else
                meusDocumentos = Db.WFD_PJPF_DOCUMENTOS
                    .Include("DescricaoDeDocumentos.TipoDeDocumento")
                    .Include("WFD_ARQUIVOS")
                    .AsExpandable()
                    .Where(predicate)
                    .OrderBy(d => d.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO)
                    .ThenBy(d => d.DescricaoDeDocumentos.DESCRICAO)
                    .ThenBy(d => d.DATA_VENCIMENTO.Value)
                    .ToList();

            return meusDocumentos;
        }

        public List<SelectListItem> MontaTipoDocumento(string Tipo)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");

            List<SelectListItem> listDocumentos = new List<SelectListItem>();
            List<TipoDeDocumento> tipoDocumentos = _tipoDocumentosService.ListarPorIdContratante(contratanteId);

            if (Tipo == "Lst")
                listDocumentos.Add(new SelectListItem() { Text = "Todos", Value = "" });
            else
                listDocumentos.Add(new SelectListItem() { Text = "Selecione...", Value = "" });

            listDocumentos.AddRange(
                    tipoDocumentos.Select(td => new SelectListItem()
                    {
                        Text = td.DESCRICAO,
                        Value = td.ID.ToString()
                    }
            ));

            return listDocumentos;
        }


        public List<SelectListItem> MontaDescricaoDocumento(int? TipoDocumento, string Tipo, bool filtrado = false)
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            TipoDocumento = (TipoDocumento != null) ? (int)TipoDocumento : 0;

            List<SelectListItem> listDocumentos = new List<SelectListItem>();
            List<DescricaoDeDocumentos> descricaoDocumentos = new List<DescricaoDeDocumentos>();
            if (filtrado)
            {
                List<DocumentosDoFornecedor> lista = _fornecedorDocumentoService.ListarDescricaoDeDocumentosUtilizadasPorContratante(contratanteId);
                descricaoDocumentos = lista
                    .Where(x => x.DescricaoDeDocumentos.ATIVO
                    && x.DescricaoDeDocumentos.CONTRATANTE_ID == contratanteId
                    && x.DescricaoDeDocumentos.TIPO_DOCUMENTOS_ID == TipoDocumento)
                    .Select(x => x.DescricaoDeDocumentos).Distinct().ToList();
            }
            else
            {
                descricaoDocumentos = Db.WFD_DESCRICAO_DOCUMENTOS.Where(e => e.ATIVO && e.CONTRATANTE_ID == contratanteId && e.TIPO_DOCUMENTOS_ID == TipoDocumento).OrderBy(e => e.DESCRICAO).ToList();
            }
            if (Tipo == "Lst")
                listDocumentos.Add(new SelectListItem() { Text = "Todos", Value = "" });
            else
                listDocumentos.Add(new SelectListItem() { Text = "Selecione...", Value = "" });

            listDocumentos.AddRange(
                descricaoDocumentos.Select(dd => new SelectListItem()
                {
                    Text = dd.DESCRICAO,
                    Value = dd.ID.ToString()
                }));

            return listDocumentos;
        }

        private List<DocumentosDoFornecedor> BuscaDocumentosSelecionados(string DocumentosSelecionados)
        {
            if (!string.IsNullOrEmpty(DocumentosSelecionados))
            {
                List<int> documentos = ConverteLista(DocumentosSelecionados, '|');

                List<DocumentosDoFornecedor> documentosSelecionados = Db.WFD_PJPF_DOCUMENTOS
                    .Include("DescricaoDeDocumentos.TipoDeDocumento")
                    .Include("WFD_ARQUIVOS")
                    .OrderBy(x => x.DescricaoDeDocumentos.TipoDeDocumento.DESCRICAO)
                    .ThenBy(x => x.DescricaoDeDocumentos.DESCRICAO)
                    .Where(d => documentos.Contains(d.ID)).ToList();

                return documentosSelecionados;
            }
            else
                return new List<DocumentosDoFornecedor>();
        }

        private void PopularDadosBancariosContatos(MeusDocumentosEnviarVM model, int ContratanteId)
        {
            var contratanteFornecedor = Db.WFD_CONTRATANTE_PJPF
                .Include("BancoDoFornecedor.WFD_ARQUIVOS")
                .Include("WFD_PJPF_CONTATOS")
                .FirstOrDefault(x => x.CONTRATANTE_ID == ContratanteId && x.TP_PJPF == 1);
            model.DadosBancarios = DadosBancariosVM.ModelToViewModel(contratanteFornecedor.BancoDoFornecedor.ToList());
            model.DadosContatos = DadosContatoVM.ModelToViewModel(contratanteFornecedor.WFD_PJPF_CONTATOS.ToList());
        }

        public void ManterMeusBancos(FichaCadastralWebForLinkVM model)
        {
            foreach (var item in model.DadosBancarios)
            {
                item.ContratanteID = model.ContratanteID;
                item.ContratantePjPfId = model.ContratanteFornecedorID;
                item.Ativo = true;

                var arquivoId = 0;
                if (string.IsNullOrEmpty(item.NomeArquivo))
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                        arquivoId = _arquivoFornecedorService.GravarArquivoSolicitacao(model.ContratanteID, item.ArquivoSubido, item.TipoArquivoSubido);

                    else
                    if (!String.IsNullOrEmpty(item.ArquivoSubido))
                        arquivoId = _arquivoFornecedorService.SubstituirArquivoMeusBancario(model.ContratanteID, (int)item.BancoPJPFID, (int)item.ArquivoID, item.ArquivoSubido, item.TipoArquivoSubido, item.NomeArquivo);

                if (!String.IsNullOrEmpty(item.ArquivoSubido))
                {
                    item.ArquivoID = arquivoId;
                    item.NomeArquivo = _arquivoFornecedorService.PegaNomeArquivoSubido(item.ArquivoSubido);
                    item.ArquivoSubido = null;
                    item.TipoArquivoSubido = null;
                }
            }
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
                TempData["TipoEndereco"] = new SelectList(_enderecoService.ListarTodosTiposEnderecosPorNome(), "ID", "NM_TP_ENDERECO");

            ViewBag.TipoEndereco = TempData["TipoEndereco"] as SelectList;
            TempData.Keep("TipoEndereco");
        }

        private string MontarStringDocsSelecionados(string DocumentosSelecionados, int? DocumentoID, string acao)
        {
            List<int> vdocs;
            if (!string.IsNullOrEmpty(DocumentosSelecionados))
                vdocs = DocumentosSelecionados.Split(new Char[] { '|' }).Select(int.Parse).ToList();
            else
                vdocs = new List<int>();

            if (acao == "Adicionar")
            {
                if (!vdocs.Contains((int)DocumentoID))
                {
                    vdocs.Add((int)DocumentoID);
                }
            }
            else
            {
                vdocs.Remove((int)DocumentoID);
            }

            return String.Join("|", vdocs);
        }

        private bool SalvarCompartilhamento(MeusDocumentosEnviarVM model, int ContratanteId, bool Reenvio)
        {
            try
            {
                List<EmailVM> listaEmails = MontaListaEmailsEnvio(model.Para);

                //INSERE EMAIL AVULSO NO DESTINATARIO
                DESTINATARIO destinatario = new DESTINATARIO();
                foreach (EmailVM email in listaEmails.Where(e => e.Tipo == TipoEmailVM.EmailAvulso))
                {
                    destinatario = _meusCompartilhamentosService.SalvarDestinatario(ContratanteId, email.Endereco);
                    email.ID = destinatario.ID;
                }
                foreach (EmailVM email in listaEmails.Where(e => e.Tipo == TipoEmailVM.Fornecedor))
                    email.ID = _meusCompartilhamentosService.BuscarId(email.Endereco);

                //INSERE MEUS COMPARTILHAMENTOS
                Compartilhamentos compartilhamentos = new Compartilhamentos
                {
                    CONTRATANTE_ID = ContratanteId,
                    ENVIADO_EM = DateTime.Now,
                    MENSAGEM = model.Mensagem.Replace("'", "").Replace("\"", ""),
                    CHAVE = Path.GetRandomFileName().Replace(".", ""),
                    ASSUNTO =
                        (Reenvio && model.Assunto.IndexOf("(Reenvio)") < 0)
                            ? model.Assunto + " (Reenvio)"
                            : model.Assunto,
                    SEM_PRAZO = model.SemPrazo,
                    VALIDADE = (model.SemPrazo) ? null : model.DataValidade,
                    RESTRITA = model.EnviarFichaCadastral
                };

                if (model.DadosContatos != null)
                    model.DadosContatos.ForEach(item =>
                    {
                        compartilhamentos.WFD_PJPF_CONTATOS.Add(_meusCompartilhamentosService.BuscarContatoPorId(item.ContatoID));
                    });

                if (model.DadosBancarios != null)
                    model.DadosBancarios.ForEach(item =>
                    {
                        compartilhamentos.WFD_PJPF_BANCO.Add(_meusCompartilhamentosService.BuscarBancoPorId((int)item.BancoPJPFID));
                    });

                _meusCompartilhamentosService.IncluirCompartilhamento(compartilhamentos);

                //Db.Entry(compartilhamentos).State = EntityState.Added;
                //Db.SaveChanges();

                string mensagem = "";
                mensagem += model.Mensagem;
                mensagem += "<br />Para Acessá-los, clique no link abaixo ou copie e cole em seu navegador<br /><br />";
                mensagem += "<a href='{0}'>Link:<a/> {0}";

                //MEUS DOCUMENTOS COMPARTILHADOS
                List<object> listaEnvio = new List<object>();

                foreach (EmailVM email in listaEmails)
                {
                    string url = Url.Action("Compartilhados", "MeusDocumentos", new
                    {
                        chaveurl = Cripto.Criptografar(string.Format("idComp={0}&chave={1}&idDest={2}", compartilhamentos.ID, compartilhamentos.CHAVE, email.ID), Key)
                    }, Request.Url.Scheme);
                    string msg = String.Format(mensagem, url);

                    foreach (DocumentosDoFornecedor docs in model.MeusDocumentos)
                    {
                        _meusCompartilhamentosService.AlterarCompartilhamento(compartilhamentos, new DocumentosCompartilhados()
                        {
                            COMPARTILHAMENTO_ID = compartilhamentos.ID,
                            CONTRATANTE_ID = ContratanteId,
                            PJPF_DOCUMENTO_ID = docs.ID,
                        }, (int)email.ID);
                    }
                    //Monta Lista de Envio
                    listaEnvio.Add(new
                    {
                        Email = email.Endereco,
                        Assunto = compartilhamentos.ASSUNTO,
                        Mensagem = msg
                    });
                }
                _meusCompartilhamentosService.AlterarMeuCompartilhamento(compartilhamentos);
                //ENVIA OS EMAILS

                foreach (dynamic o in listaEnvio)
                {
                    _metodosGerais.EnviarEmail(o.Email, o.Assunto, o.Mensagem);
                }

                return true;

            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return false;
                throw;
            }
        }

        public JsonResult BuscarCep(string cep)
        {
            //var online = new ConsultaCNPJSimplesNacional();
            //var captchaType = online.GetCaptchaCNPJ();
            //var captcha = online.ConsultaCNPJ("1195269000133", "");
            Address address = SearchZip.GetAddress(cep);
            return Json(new
            {
                Rua = address.Street,     // Avenida Euclides da Cunha
                Bairro = address.District,  // Jardim São Jorge
                Cidade = address.City,      // Paranavaí
                Estado = address.State,     // PR
                Cep = address.Zip,       // 87710130
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}
