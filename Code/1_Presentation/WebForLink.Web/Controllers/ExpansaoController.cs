using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
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
    public class ExpansaoController : ControllerPadrao
    {
        private readonly ITramiteWebForLinkAppService _tramite;
        private readonly IContratanteOrganizacaoComprasWebForLinkAppService _organizacaoComprasBP;
        private readonly IBancoWebForLinkAppService _bancoBP;

        public ExpansaoController(IContratanteOrganizacaoComprasWebForLinkAppService organizacaoCompras,
            IBancoWebForLinkAppService banco, 
            ITramiteWebForLinkAppService tramite)
        {
            _organizacaoComprasBP = organizacaoCompras;
            _bancoBP = banco;
            _tramite = tramite;
        }

        [Authorize]
        public ActionResult FornecedoresExpandirFrm(string chaveurl)
        {
            int fornecedorID = 0;
            int contratanteID = 0;
            int? usuarioId = (int?)Geral.PegaAuthTicket("UsuarioId");
            FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM { Expansao = new ExpansaoVM() };
            try
            {
                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                    Int32.TryParse(param.First(p => p.Name == "FornecedorID").Value, out fornecedorID);
                    Int32.TryParse(param.First(p => p.Name == "ContratanteID").Value, out contratanteID);
                }

                if (fornecedorID != 0)
                {
                    Fornecedor fornecedor = Db.WFD_PJPF
                        .Include("DocumentosDeFornecedor.DescricaoDeDocumentos.TipoDeDocumento")
                        .Include("Contratante")
                        .Include("WFD_CONTRATANTE_PJPF.BancoDoFornecedor")
                        .Include("WFD_PJPF_ROBO")
                        .Include("T_UF")
                        .Include("WFD_CONTRATANTE_PJPF.WFD_PJPF_CONTATOS")
                        .FirstOrDefault(c => c.ID == fornecedorID);

                    List<BancoDoFornecedor> bancos = fornecedor.WFD_CONTRATANTE_PJPF.FirstOrDefault().BancoDoFornecedor.ToList();
                    List<FORNECEDOR_CONTATOS> contatos = fornecedor.WFD_CONTRATANTE_PJPF.FirstOrDefault().WFD_PJPF_CONTATOS.ToList();
                    List<DocumentosDoFornecedor> documentos = fornecedor.DocumentosDoFornecedor.ToList();
                    ficha.FornecedorRobo = new FornecedorRoboVM();

                    if (fornecedor != null)
                    {
                        ficha.ID = fornecedorID;
                        ficha.ContratanteFornecedorID = contratanteID;
                        ficha.RazaoSocial = fornecedor.TIPO_PJPF_ID == 3 ? fornecedor.NOME : fornecedor.RAZAO_SOCIAL;
                        ficha.NomeFantasia = fornecedor.NOME_FANTASIA;
                        //ficha.CNAE = fornecedor.CNAE;
                        ficha.CNPJ_CPF = fornecedor.TIPO_PJPF_ID == 3 ? Convert.ToUInt64(fornecedor.CPF).ToString(@"000\.000\.000\-00") : Convert.ToUInt64(fornecedor.CNPJ).ToString(@"00\.000\.000\/0000\-00");
                        ficha.InscricaoEstadual = fornecedor.INSCR_ESTADUAL;
                        ficha.InscricaoMunicipal = fornecedor.INSCR_MUNICIPAL;
                        ficha.NomeEmpresa = fornecedor.WFD_CONTRATANTE_PJPF.Single(f => f.CONTRATANTE_ID == contratanteID).WFD_CONTRATANTE.RAZAO_SOCIAL;

                        if (fornecedor.ROBO != null)
                            ficha.FornecedorRobo.SimplesNacionalSituacao = fornecedor.ROBO.SIMPLES_NACIONAL_SITUACAO == null ? "" : fornecedor.ROBO.SIMPLES_NACIONAL_SITUACAO;

                        Mapeamento.PopularEndereco(ficha, fornecedor);

                        ficha.Solicitacao = new SolicitacaoVM
                        {
                            Fluxo = new FluxoVM
                            {
                                ID = 3
                            }
                        };
                        ficha.DadosBancarios = new List<DadosBancariosVM>();
                        int cont = 0;
                        foreach (var item in bancos)
                        {
                            ficha.DadosBancarios.Add(new DadosBancariosVM
                            {
                                BancoPJPFID = item.ID,
                                Banco = item.BANCO_ID,
                                Agencia = item.AGENCIA,
                                Digito = item.AG_DV,
                                ContaCorrente = item.CONTA,
                                ContaCorrenteDigito = item.CONTA_DV
                            });
                            cont++;
                        }

                        cont = 5 - cont;
                        for (int i = 0; i < cont; i++)
                        {
                            ficha.DadosBancarios.Add(new DadosBancariosVM { Banco = null });
                        }

                        ficha.DadosContatos = contatos.Select(item => new DadosContatoVM()
                        {
                            ContatoID = item.ID,
                            NomeContato = item.NOME,
                            EmailContato = item.EMAIL,
                            Telefone = item.TELEFONE,
                            Celular = item.CELULAR
                        }).ToList();
                        ficha.DadosContatos.Add(new DadosContatoVM());
                        ficha.SolicitacaoFornecedor = new SolicitacaoFornecedorVM
                        {
                            Solicitacao = false,
                            Documentos = documentos.Select(d => new SolicitacaoDocumentosVM()
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

                        List<int> contratantesForn = Db.WFD_CONTRATANTE_PJPF.Where(c => c.PJPF_ID == fornecedorID).Select(cc => cc.CONTRATANTE_ID).ToList();
                        List<Contratante> contratantes = Db.Contratante.Where(c => c.WFD_USUARIO1.Any(u => u.ID == usuarioId) && !contratantesForn.Contains(c.ID)).ToList();

                        int contID = contratantes.First().ID;

                        ViewBag.Empresa = new SelectList(contratantes, "ID", "RAZAO_SOCIAL");
                        ViewBag.Organizacao = new SelectList(_organizacaoComprasBP.ListarTodosPorIdContratante(contID), "ID", "ORG_COMPRAS_DSC");
                        ViewBag.Bancos = _bancoBP.ListarTodosPorNome();
                        ViewBag.EscolherContato = true;
                        ViewBag.UsarContato = 0;
                    }
                }
                else
                {
                    return RedirectToAction("Alerta", "Home");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
            return View(ficha);
        }

        [Authorize]
        [HttpPost]
        public ActionResult FornecedoresExpandirFrm(FichaCadastralWebForLinkVM model, int Empresa, int Organizacao, int FluxoID, int[] DocumentoID, string[] NomeDocumento, int?[] ArquivoID, int[] BancoOrdem, int[] BancoID, int[] Banco, string[] Agencia, string[] Digito, string[] ContaCorrente, string[] ContaCorrenteDigito, int? UsarContato, int[] ContatoID, string[] NomeContato, string[] Email, string[] Telefone, string[] Celular)
        {
            int? usuarioId = (int?)Geral.PegaAuthTicket("UsuarioId");

            List<int> contratantesForn = Db.WFD_CONTRATANTE_PJPF.Where(c => c.PJPF_ID == model.ID).Select(cc => cc.CONTRATANTE_ID).ToList();

            List<Contratante> contratantes = Db.Contratante.Where(c => c.WFD_USUARIO1.Any(u => u.ID == usuarioId) && !contratantesForn.Contains(c.ID)).ToList();
            int contID = contratantes.FirstOrDefault(c => c.ID == Empresa).ID;

            ViewBag.Empresa = new SelectList(contratantes, "ID", "RAZAO_SOCIAL", Empresa);
            ViewBag.Organizacao = new SelectList(_organizacaoComprasBP.ListarTodosPorIdContratante(contID), "ID", "ORG_COMPRAS_DSC");
            ViewBag.Bancos = _bancoBP.ListarTodosPorNome();
            ViewBag.EscolherContato = true;
            ViewBag.UsarContato = UsarContato;

            model.Expansao = new ExpansaoVM();
            model.Solicitacao = new SolicitacaoVM
            {
                Fluxo = new FluxoVM
                {
                    ID = FluxoID
                }
            };

            if (UsarContato == null)
            {
                ModelState.AddModelError("ContatosValidation", "Informe contato que deseja utilizar!");
            }

            //VERIFICA DADOS DE CONTATOS
            model.DadosContatos = new List<DadosContatoVM>();
            for (int i = 0; i < ContatoID.Length; i++)
            {
                model.DadosContatos.Add(new DadosContatoVM
                {
                    ContatoID = ContatoID[i],
                    NomeContato = (i < NomeContato.Length) ? NomeContato[i] : "",
                    EmailContato = (i < Email.Length) ? Email[i] : "",
                    Telefone = (i < Telefone.Length) ? Telefone[i] : "",
                    Celular = (i < Celular.Length) ? Celular[i] : "",
                });

                if (ContatoID[i] == UsarContato)
                {
                    if (string.IsNullOrEmpty(Email[i]))
                    {
                        ModelState.AddModelError("ContatosValidation", "Informe o email!");
                    }
                }
            }

            // VERIFICA DADOS BANCARIOS
            bool dadosBancariosCompleto = true;
            model.DadosBancarios = new List<DadosBancariosVM>();
            for (int i = 0; i < Banco.Length; i++)
            {
                bool temDadosBancario = false;

                if ((Banco[i] != 0)) temDadosBancario = true;
                if ((!string.IsNullOrEmpty(Agencia[i]))) temDadosBancario = true;
                if ((!string.IsNullOrEmpty(Digito[i]))) temDadosBancario = true;
                if ((!string.IsNullOrEmpty(ContaCorrente[i]))) temDadosBancario = true;
                if ((!string.IsNullOrEmpty(ContaCorrenteDigito[i]))) temDadosBancario = true;

                model.DadosBancarios.Add(new DadosBancariosVM
                {
                    BancoSolicitacaoID = BancoID[i],
                    Banco = (i < Banco.Length) ? Banco[i] : 0,
                    Agencia = (i < Agencia.Length) ? Agencia[i] : "",
                    Digito = (i < Digito.Length) ? Digito[i] : "",
                    ContaCorrente = (i < ContaCorrente.Length) ? ContaCorrente[i] : "",
                    ContaCorrenteDigito = (i < ContaCorrenteDigito.Length) ? ContaCorrenteDigito[i] : ""
                });

                if (temDadosBancario)
                {
                    if ((Banco[i] == 0)) dadosBancariosCompleto = false;
                    if ((string.IsNullOrEmpty(Agencia[i]))) dadosBancariosCompleto = false;
                    if (Agencia[i].Length < 4) dadosBancariosCompleto = false;
                    if ((string.IsNullOrEmpty(ContaCorrente[i]))) dadosBancariosCompleto = false;
                }
            }
            if (!dadosBancariosCompleto)
            {
                ModelState.AddModelError("DadosBancariosValidation", "Dado Bancário Incompleto!");
            }
            if (model.DadosBancarios.Count == 0)
            {
                ModelState.AddModelError("DadosBancariosValidation", "Informe ao menos um Dado Bancário!");
            }

            //VERIFICA ANEXOS
            model.SolicitacaoFornecedor = new SolicitacaoFornecedorVM()
            {
                Solicitacao = false,
                Documentos = new List<SolicitacaoDocumentosVM>()
            };
            if (DocumentoID != null)
            {
                for (int i = 0; i < DocumentoID.Length; i++)
                {
                    SolicitacaoDocumentosVM solicitacaoDocumentosvm = new SolicitacaoDocumentosVM()
                    {
                        ID = DocumentoID[i],
                        Documento = NomeDocumento[i],
                        ArquivoID = ArquivoID[i]
                    };
                    model.SolicitacaoFornecedor.Documentos.Add(solicitacaoDocumentosvm);
                }
            }

            ModelState.Remove("SolicitacaoFornecedor.Assunto");
            ModelState.Remove("SolicitacaoFornecedor.DescricaoSolicitacao");

            if (ModelState.IsValid)
            {
                try
                {
                    WFD_CONTRATANTE_PJPF fornecedor = Db.WFD_CONTRATANTE_PJPF.Include("Fornecedor")
                        .Include("WFD_PJPF_CATEGORIA")
                        .FirstOrDefault(f => f.PJPF_ID == model.ID && f.CONTRATANTE_ID == model.ContratanteFornecedorID);
                    string codigoCategoria = fornecedor.WFD_PJPF_CATEGORIA.CODIGO;
                    FORNECEDOR_CATEGORIA categoria = Db.WFD_PJPF_CATEGORIA.FirstOrDefault(c => c.CONTRATANTE_ID == Empresa && c.CODIGO == codigoCategoria);

                    SOLICITACAO solicitacao = new SOLICITACAO();
                    PopularSolicitacaoEmAprovacao(Empresa, model.ID, usuarioId, FluxoID, solicitacao);

                    Db.Entry(solicitacao).State = EntityState.Added;
                    Db.SaveChanges();

                    SolicitacaoCadastroFornecedor solforn = new SolicitacaoCadastroFornecedor
                    {
                        SOLICITACAO_ID = solicitacao.ID,
                        EhExpansao = true,
                        EXPANSAO_PARA_CONTR_ID = Empresa,
                        CATEGORIA_ID = categoria.ID,
                        ORG_COMPRAS_ID = Organizacao
                    };

                    for (int i = 0; i < BancoID.Length; i++)
                    {
                        solforn.WFD_SOLICITACAO.SolicitacaoModificacaoDadosBancario.Add(
                            new SolicitacaoModificacaoDadosBancario()
                            {
                                BANCO_PJPF_ID = BancoID[i] != 0 ? (int?)BancoID[i] : null,
                                BANCO_ID = (Banco[i] != 0) ? Banco[i] : 0,
                                AGENCIA = (string.IsNullOrEmpty(Agencia[i]) || String.IsNullOrWhiteSpace(Agencia[i])) ? null : Agencia[i],
                                AG_DV = (string.IsNullOrEmpty(Digito[i]) || String.IsNullOrWhiteSpace(Digito[i])) ? null : Digito[i],
                                CONTA = (string.IsNullOrEmpty(ContaCorrente[i]) || String.IsNullOrWhiteSpace(ContaCorrente[i])) ? null : ContaCorrente[i],
                                CONTA_DV = (string.IsNullOrEmpty(ContaCorrenteDigito[i]) || String.IsNullOrWhiteSpace(ContaCorrenteDigito[i])) ? null : ContaCorrenteDigito[i]
                            });
                    }
                    for (int i = 0; i < ContatoID.Length; i++)
                    {
                        if (ContatoID[i] == UsarContato)
                        {
                            solforn.WFD_SOLICITACAO.SolicitacaoModificacaoDadosContato.Add(new SolicitacaoModificacaoDadosContato
                            {
                                EMAIL = Email[i],
                                NOME = NomeContato[i],
                                TELEFONE = Mascara.RemoverMascaraTelefone(Telefone[i]),
                                CELULAR = Celular[i]
                            });
                        }
                    }

                    solforn.OBSERVACAO = model.Observacao;

                    Db.Entry(solforn).State = EntityState.Added;
                    Db.SaveChanges();

                    _tramite.AtualizarTramite(Empresa, solicitacao.ID, FluxoID, 1, 2, null);

                    return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = string.Format("Solicitação {0} de Expansão realizado com Sucesso!", solicitacao.ID) });
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }

            return View(model);
        }
        private void PopularSolicitacaoEmAprovacao(int contratanteId, int fornecedorId, int? usuarioId, int fluxoId, SOLICITACAO solicitacao)
        {
            if (contratanteId != 0)
                solicitacao.CONTRATANTE_ID = contratanteId;

            solicitacao.FLUXO_ID = fluxoId; // Bloqueio
            solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
            solicitacao.SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao; // EM APROVACAO
            solicitacao.USUARIO_ID = usuarioId;
            solicitacao.PJPF_ID = fornecedorId;
        }
    }
}