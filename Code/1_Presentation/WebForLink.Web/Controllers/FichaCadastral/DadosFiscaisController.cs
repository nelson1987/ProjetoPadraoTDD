using System.Web.Mvc;

namespace WebForLink.Web.Controllers
{
    public class DadosFiscaisController : Controller
    {
        #region Modificação Dados Fiscais
        /*
        [Authorize]
        public ActionResult FornecedoresModificacaoDadosFiscaisFrm(string chaveurl)
        {
            int fornecedorId = 0;
            int fornecedorContratanteID = 0;
            ViewBag.ChaveUrl = chaveurl;

            try
            {
                if (!string.IsNullOrEmpty(chaveurl))
                {
                    List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
                    Int32.TryParse(param.First(p => p.Name == "id").Value, out fornecedorId);
                    Int32.TryParse(param.First(p => p.Name == "contratanteid").Value, out fornecedorContratanteID);
                }

                if (fornecedorId != 0)
                {
                    Fornecedor fornecedor = Db.Fornecedor.Include("WFD_CONTRATANTE_PJPF.Contratante").Include("BancoDoFornecedor").Include("T_UF").Include("ROBO").SingleOrDefault(c => c.ID == fornecedorId);
                    Contratante contratante = fornecedor.WFD_CONTRATANTE_PJPF.SingleOrDefault(c => c.CONTRATANTE_ID == fornecedorContratanteID).Contratante;

                    FichaCadastralVM ficha = new FichaCadastralVM();
                    if (fornecedor != null)
                    {
                        CriarEntidadePartialDadosCadastro(fornecedorId, fornecedor, contratante, ficha);
                    }

                    ViewBag.OutrosDadosVisao = new SelectList(Db.TipoDeVisao.ToList(), "ID", "VISAO_NM");
                    ViewBag.OutrosDadosGrupo = new SelectList(new List<TipoGrupoDeEmpresas>(), "ID", "GRUPO_NM");
                    ViewBag.OutrosDadosDescricao = new SelectList(new List<WFD_T_DESCRICAO>(), "ID", "DESCRICAO_NM");
                    ViewBag.lstFornecedorFiscais = Db.WFD_PJPF_DFISCAIS_SEQ.Include("WFD_CONTRATANTE_PJPF").Include("WFD_T_CATEGORIA_IRF").Include("WFD_T_CATEGORIA_IRF_COD").Where(x => x.CONTRATANTE_PJPF_ID == contratante.ID).ToList();

                    List<WFD_PJPF_DFISCAIS_SEQ> lst = ViewBag.lstFornecedorFiscais;
                    ViewBag.Categoria = Db.WFD_T_CATEGORIA_IRF.ToList();
                    ViewBag.TotalPaginas = lst.Count / 10;
                    ViewBag.TotalRegistros = lst.Count;
                    return View(ficha);
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

            return View();
        }
        */
        /*
        [Authorize]
        [HttpPost]
        public ActionResult FornecedoresModificacaoDadosFiscaisFrm(FichaCadastralVM model, string[] categoria, int[] codigo, string[] sujeito, string[] desc)
        {
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            ViewBag.OutrosDadosVisao = new SelectList(Db.TipoDeVisao.ToList(), "ID", "VISAO_NM", model.OutrosDadosVisao);
            ViewBag.OutrosDadosGrupo = new SelectList(Db.TipoGrupoDeEmpresas.Where(g => g.VISAO_ID == model.OutrosDadosVisao).ToList(), "ID", "GRUPO_NM", model.OutrosDadosGrupo);
            ViewBag.OutrosDadosDescricao = new SelectList(Db.WFD_T_DESCRICAO.Where(d => d.GRUPO_ID == model.OutrosDadosGrupo).ToList(), "ID", "DESCRICAO_NM", model.OutrosDadosDescricao);

            // VERIFICA DADOS

            if (categoria == null || categoria.Length <= 0)
            {
                ViewBag.lstFornecedorFiscais = Db.WFD_PJPF_DFISCAIS_SEQ.Include("WFD_CONTRATANTE_PJPF").Include("WFD_T_CATEGORIA_IRF").Include("WFD_T_CATEGORIA_IRF_COD")
                    .Where(x => x.CONTRATANTE_PJPF_ID == model.ContratanteID).ToList();
                ViewBag.Categoria = Db.WFD_T_CATEGORIA_IRF.ToList();
                ModelState.AddModelError("DadosGeraisValidation", "Ao menos um dados fiscal deve ser utilizado.");
            }

            if (ModelState.IsValid)
            {
                WFD_SOLICITACAO solicitacao = new WFD_SOLICITACAO();

                try
                {
                    int FluxoId = 5;

                    solicitacao.CONTRATANTE_ID = model.ContratanteID;
                    solicitacao.FLUXO_ID = FluxoId; // MODIFICACAO DADOS BANCARIOS
                    solicitacao.SOLICITACAO_DT_CRIA = DateTime.Now;
                    solicitacao.SOLICITACAO_STATUS_ID = (int)EnumStatusTramite.EmAprovacao; // EM APROVACAO
                    solicitacao.USUARIO_ID = usuarioId;
                    solicitacao.PJPF_ID = model.ID;

                    Db.Entry(solicitacao).State = EntityState.Added;
                    Db.SaveChanges();

                    for (int i = 0; i < categoria.Length; i++)
                    {
                        WFD_SOL_MOD_DFICAIS_SEQ solDadosFiscais = new WFD_SOL_MOD_DFICAIS_SEQ
                        {
                            WFD_T_CATEGORIA_IRF_COD = new WFD_T_CATEGORIA_IRF_COD(),
                            WFD_T_CATEGORIA_IRF = new WFD_T_CATEGORIA_IRF(),
                            CATEG_IFR_COD_ID = Convert.ToInt32(codigo[i]),
                            CATEG_IRF_ID = Convert.ToInt32(categoria[i]),
                            CONTRATANTE_ID = model.ContratanteID,
                            PJPF_ID = model.ID,
                            SOLICITACAO_ID = solicitacao.ID,
                            SUJEITO_A = sujeito[i] == "1"
                        };

                        string categIRFCod = codigo[i].ToString();

                        solDadosFiscais.WFD_T_CATEGORIA_IRF = Db.WFD_T_CATEGORIA_IRF.FirstOrDefault(x => x.CATEG_IRF_COD == categIRFCod);
                        solDadosFiscais.WFD_T_CATEGORIA_IRF_COD = Db.WFD_T_CATEGORIA_IRF_COD.FirstOrDefault(x => x.CATEG_IFR_COD_COD == categIRFCod);

                        Db.WFD_SOL_MOD_DFICAIS_SEQ.Add(solDadosFiscais);
                    }

                    Db.SaveChanges();

                    Tramite.AtualizaTramite(model.ContratanteID, solicitacao.ID, FluxoId, 1, 2, usuarioId);
                }
                catch
                {
                    if (solicitacao.ID != 0)
                    {
                        List<WFD_SOL_MOD_DFICAIS_SEQ> dadosFiscais = Db.WFD_SOL_MOD_DFICAIS_SEQ.Where(b => b.SOLICITACAO_ID == solicitacao.ID).ToList();
                        foreach (WFD_SOL_MOD_DFICAIS_SEQ item in dadosFiscais)
                        {
                            Db.WFD_SOL_MOD_DFICAIS_SEQ.Remove(item);
                        }

                        Db.WFD_SOLICITACAO.Remove(solicitacao);
                        Db.SaveChanges();
                    }
                }

                return RedirectToAction("FornecedoresLst", "Fornecedores", new { MensagemSucesso = string.Format("Solicitação {0} de Alteração de Dados Bancários realizado com Sucesso!", solicitacao.ID) });
            }

            return View(model);
        }
        */
        #endregion Modificação Dados Fiscais
    }
}