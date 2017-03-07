using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebForLink.Service;
using WebForLink.Data.Contextos;
using WebForLink.Domain.Models;
using WebForLink.Web.Exceptions;

namespace WebForLink.Web.Areas.Administracao.Controllers
{
    public class QuestionarioDinamicoController : ControllerPadrao<UnitOfWork>
    {
        #region IoC
        private static UnitOfWork _repositorios { get; set; }

        public QuestionarioDinamicoController(WebForLinkContexto db)
            : base(_repositorios)
        {
            _db = db;
            try
            {
                if (_repositorios == null)
                    _repositorios = new UnitOfWork(new WebForLinkContexto());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
            }
        }

        #endregion

        /*     
        public List<QIC_QUESTIONARIO> ListarQuestionarios(TesteQuestionarioVM filtro)
        {
            try
            {
                //Filtros
                var predicate = PredicateBuilder.True<QIC_QUESTIONARIO>();

                if (filtro.CategoriaId > 0)
                    predicate = predicate.And(x => x.QIC_QUESTIONARIO_CATEGORIA.Any(y => y.CATEGORIA_ID == filtro.CategoriaId));

                if (filtro.ContratanteId > 0)
                    predicate = predicate.And(x => x.CONTRATANTE_ID == filtro.ContratanteId);

                if (filtro.QuestionarioId > 0)
                    predicate = predicate.And(x => x.ID == filtro.QuestionarioId);

                if (filtro.RespostaUsuario)
                {
                    if (filtro.SolicitacaoId > 0)
                        predicate = predicate.And(x => x
                            .QIC_QUEST_ABA
                            .Any(y => y.QIC_QUEST_ABA_PERG
                                .Any(w => w.WFD_INFORM_COMPL
                                    .Any(wfl => wfl.SOLICITACAO_ID == filtro.SolicitacaoId))));
                }
                else
                {
                    if (filtro.PjpfId > 0)
                        predicate = predicate.And(x => x
                            .QIC_QUEST_ABA
                            .Any(y => y.QIC_QUEST_ABA_PERG
                                .Any(w => w.WFD_PJPF_INFORM_COMPL
                                    .Any(wfl => wfl.CONTRATANTE_PJPF_ID == filtro.ContratanteId))));           }
                if (filtro.PapelId > 0)
                    predicate = predicate.And(x => x
                        .QIC_QUEST_ABA
                        .Any(y => y.QIC_QUEST_ABA_PERG
                            .Any(w => w.QIC_QUEST_ABA_PERG_PAPEL
                                .Any(z => z.PAPEL_ID == filtro.PapelId))));


                return Db.QIC_QUESTIONARIO
                      .Include("QIC_QUESTIONARIO_CATEGORIA")
                      .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.QIC_QUEST_ABA_PERG_PAPEL")
                      .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.QIC_QUEST_ABA_PERG_RESP")
                      .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.WFD_INFORM_COMPL")
                      .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.WFD_PJPF_INFORM_COMPL")
                      .AsExpandable()
                      .Where(predicate)
                      .ToList();
            }
            catch (Exception ex) { return new List<QIC_QUESTIONARIO>(); }
        }
        */
        private WebForLinkContexto _db = new WebForLinkContexto();
        // GET: Administracao/QuestionarioDinamico
        public ActionResult Teste()
        {
            TesteQuestionarioVM modelo = new TesteQuestionarioVM()
            {
                ContratanteId = 3,
                RespostaUsuario = true, //Vem de PJPF
                CategoriaId = 0,
                QuestionarioId = 0,
                PapelId = 0,
                PjpfId = 0,
                SolicitacaoId = 0,
                TipoResposta = EnumTipoResposta.Solicitacao
            };
            try
            {
                modelo.QuestionarioDinamicoList = ListarQuestionarios();
                return View(modelo);
            }
            catch (Exception ex)
            {
                modelo.RespostaUsuario = ex.Message != null;
            }
            return View(modelo);
        }
        public JsonResult RetornaPropriedadeFilhos()
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
        public List<QuestionarioDinamicoVM> ListarQuestionarios()
        {
            return new List<QuestionarioDinamicoVM>() {
                new QuestionarioDinamicoVM{ Id = "1",}, 
                new QuestionarioDinamicoVM{ Id = "2",Filhos="3"}, 
                new QuestionarioDinamicoVM{ Id = "3",}, 
                new QuestionarioDinamicoVM{ Id = "4",},
                new QuestionarioDinamicoVM{ Id = "5",} };
        }


    }
    /*
    public JsonResult ExibirQuestionario()
    {
        var filtro = PredicateBuilder.True<QIC_QUESTIONARIO>();
        filtro.And(x => x.CONTRATANTE_ID == 3);

        List<QIC_QUESTIONARIO> listaQuestionario = _repositorio.QuestionarioDinamico.Buscar(filtro).ToList();


        List<QuestionarioDinamicoDTOJson> retorno = new List<QuestionarioDinamicoDTOJson>();
        foreach (var item in listaQuestionario)
        {
            QuestionarioDinamicoDTOJson questionario = new QuestionarioDinamicoDTOJson();
            questionario.Id = item.ID;
            questionario.Titulo = item.QUEST_NM;
            questionario.Subtitulo = item.QUEST_DSC;
            questionario.MenuAbas = new List<MenuAbaQuestionarioDTOJson>();

            foreach (var aba in item.QIC_QUEST_ABA)
            {
                MenuAbaQuestionarioDTOJson menuAbaQuestionario = new MenuAbaQuestionarioDTOJson();
                menuAbaQuestionario.Id = aba.ID;
                menuAbaQuestionario.Ativo = true;
                menuAbaQuestionario.Titulo = aba.ABA_NM;
                menuAbaQuestionario.Subtitulo = aba.ABA_DSC;
                menuAbaQuestionario.Visivel = true;
                questionario.MenuAbas.Add(menuAbaQuestionario);

                AbasQuestionarioDTOJson abaQuestionario = new AbasQuestionarioDTOJson();
                abaQuestionario.Id = aba.ID;
                abaQuestionario.Titulo = aba.ABA_NM;
                abaQuestionario.Subtitulo = aba.ABA_DSC;
                abaQuestionario.Perguntas = new List<PerguntaAbaQuestionarioDTOJson>();
                foreach (var perguntaAba in aba.QIC_QUEST_ABA_PERG)
                {

                    PerguntaAbaQuestionarioDTOJson pergunta = new PerguntaAbaQuestionarioDTOJson();
                    pergunta.Id = perguntaAba.ID;
                    pergunta.Titulo = perguntaAba.PERG_NM;
                    pergunta.Dominio = perguntaAba.DOMINIO == true;
                    pergunta.Pai = new List<PerguntaPaiQuestionarioDTOJson>();
                    pergunta.Filho = new List<PerguntaFilhoQuestionarioDTOJson>();

                    if (perguntaAba.PERG_PAI.HasValue)
                    {
                        PerguntaPaiQuestionarioDTOJson perguntaP = new PerguntaPaiQuestionarioDTOJson();
                        perguntaP.Id = (int)perguntaAba.PERG_PAI;
                        pergunta.Pai.Add(perguntaP);
                    }
                    if (pergunta.Dominio) 
                    {
                        pergunta.DominioResposta = new List<PerguntaDominioQuestionarioDTOJson>();
                        foreach (var respostaDominio in perguntaAba.QIC_QUEST_ABA_PERG_RESP)
                        {
                            pergunta.DominioResposta.Add(new PerguntaDominioQuestionarioDTOJson
                            {
                                Id = respostaDominio.ID,
                                Valor = respostaDominio.RESP_DSC,// "Copa do Mundo",
                                Selected = false 
                            });
                        }
                    }

                    var papel = perguntaAba.QIC_QUEST_ABA_PERG_PAPEL;
                    var resposta = perguntaAba.QIC_QUEST_ABA_PERG_RESP;

                    var a = perguntaAba.PERG_PAI;
                    var i = perguntaAba.TP_DADO;


                }
                questionario.Abas.Add(abaQuestionario);
            }

            retorno.Add(questionario);
        }
        var QuestionarioDinamico = new QuestionarioDinamicoDTOJson
        {
            Titulo = "Cadastre-se",
            Subtitulo = "Insira aqui seus dados",
            MenuAbas = new List<MenuAbaQuestionarioDTOJson>
            {
                new MenuAbaQuestionarioDTOJson
                {
                    Ativo = true,
                    Id = 1,
                    Titulo = "Dados Pessoais", 
                    Visivel = true
                },
                new MenuAbaQuestionarioDTOJson
                {
                    Ativo = false,
                    Id = 2,
                    Titulo = "Endereço", 
                    Visivel = true
                },
                new MenuAbaQuestionarioDTOJson
                {
                    Ativo = true,
                    Id = 3,
                    Titulo = "Contato", 
                    Visivel = true
                },
                new MenuAbaQuestionarioDTOJson
                {
                    Ativo = true,
                    Id = 1,
                    Titulo = "Bancário", 
                    Visivel = false
                }
            },
            Abas = new List<AbasQuestionarioDTOJson>
            {
                new AbasQuestionarioDTOJson
                {
                    Id = 1,
                    Titulo = "Aba 1",
                    Subtitulo =  "Aba Subtitulo 1", 
                    Perguntas = new List<PerguntaAbaQuestionarioDTOJson>
                    {
                        new PerguntaAbaQuestionarioDTOJson
                        {
                            Id = 1, 
                            Titulo = "Time de Futebol", 
                            Dominio = false, 
                            Resposta = "Flamengo", 
                            Tipo = TipoResposta.Texto,
                            Obrigatorio= true 
                        },
                        new PerguntaAbaQuestionarioDTOJson
                        {
                            Dominio= true,
                            Id= 2, 
                            Titulo = "Pergunta 2", 
                            Resposta = "Campeonato Brasileiro", 
                            Tipo = TipoResposta.Dominio, 
                            Obrigatorio = false,
                            Filho = new List<PerguntaFilhoQuestionarioDTOJson>
                            {
                                new PerguntaFilhoQuestionarioDTOJson
                                { 
                                    Id= 1, 
                                    Respondido= true 
                                }
                            },
                            Pai = new List<PerguntaPaiQuestionarioDTOJson>
                            { 
                                new PerguntaPaiQuestionarioDTOJson
                                { 
                                    Id= 1, 
                                    Respondido= true 
                                }
                            },
                            DominioResposta = new List<PerguntaDominioQuestionarioDTOJson>
                            {
                                new PerguntaDominioQuestionarioDTOJson
                                { 
                                    Id=1, 
                                    Valor= "Copa do Mundo", 
                                    Selected= false 
                                },
                                new PerguntaDominioQuestionarioDTOJson   
                                {
                                    Id=2,
                                    Valor="Copa Libertadores da América",
                                    Selected=false
                                },
                                new PerguntaDominioQuestionarioDTOJson   
                                {
                                    Id=3,
                                    Valor="UEFA Champions League",
                                    Selected=false
                                },
                                new PerguntaDominioQuestionarioDTOJson   
                                {
                                    Id=4,
                                    Valor="Campeonato Brasileiro",
                                    Selected=true
                                }
                            }
                        },
                        new PerguntaAbaQuestionarioDTOJson
                        {
                            Dominio = true, 
                            Id = 3, 
                            Titulo = "Pergunta 3", 
                            Resposta = "", 
                            Tipo = TipoResposta.Checkbox, 
                            Obrigatorio = false,
                                
                            DominioResposta = new List<PerguntaDominioQuestionarioDTOJson>
                            {
                                new PerguntaDominioQuestionarioDTOJson
                                { 
                                    Id=1, 
                                    Valor= "Copa do Mundo", 
                                    Selected= false 
                                },
                                new PerguntaDominioQuestionarioDTOJson
                                { 
                                    Id=2, 
                                    Valor= "Copa do Brasil", 
                                    Selected= false 
                                },
                                new PerguntaDominioQuestionarioDTOJson
                                { 
                                    Id=3, 
                                    Valor= "Campeonato Carioca", 
                                    Selected= true 
                                }
                            }, 
                            Pai = new List<PerguntaPaiQuestionarioDTOJson>
                            { 
                                new PerguntaPaiQuestionarioDTOJson
                                { 
                                    Id= 1, 
                                    Respondido= true 
                                },
                                new PerguntaPaiQuestionarioDTOJson
                                { 
                                    Id= 2, 
                                    Respondido= false 
                                }
                            }
                        }
                    }
                },
                new AbasQuestionarioDTOJson
                {
                    Id = 2,
                    Titulo = "Endereço"
                },
                new AbasQuestionarioDTOJson
                {
                    Id = 2,
                    Titulo = "Contato"
                }
            }
        };
        return Json(QuestionarioDinamico, JsonRequestBehavior.AllowGet);
    }

    private static void PopularQuestionario(List<QIC_QUESTIONARIO> listaQuestionario)
    {
        List<QuestionarioDinamicoDTOJson> questDinamico = new List<QuestionarioDinamicoDTOJson>();
        foreach (var questionario in listaQuestionario)
        {
            List<MenuAbaQuestionarioDTOJson> menuAbas = new List<MenuAbaQuestionarioDTOJson>();
            List<AbasQuestionarioDTOJson> abas = new List<AbasQuestionarioDTOJson>();
            foreach (var aba in questionario.QIC_QUEST_ABA)
            {
                menuAbas.Add(new MenuAbaQuestionarioDTOJson()
                {
                    Id = aba.ID,
                    Ativo = aba.ID == questionario.QIC_QUEST_ABA.FirstOrDefault().ID,
                    Titulo = aba.ABA_NM,
                    Subtitulo = aba.ABA_DSC,
                });
                List<PerguntaAbaQuestionarioDTOJson> perguntas = new List<PerguntaAbaQuestionarioDTOJson>();
                foreach (var item in aba.QIC_QUEST_ABA_PERG)
                {
                    if (item.DOMINIO == true)
                    {
                        List<PerguntaDominioQuestionarioDTOJson> respostas = item.QIC_QUEST_ABA_PERG_RESP.Select(x => new PerguntaDominioQuestionarioDTOJson
                        {
                            Id = x.ID,

                        }).ToList();
                    }
                    perguntas.Add(
                        new PerguntaAbaQuestionarioDTOJson
                        {
                            Dominio = item.DOMINIO == true,
                            Id = item.ID,
                            Titulo = item.EXIBE_NM,
                            Resposta = "Flamengo",
                            Tipo = TipoResposta.Texto,
                            Obrigatorio = true
                        });
                }
                abas.Add(new AbasQuestionarioDTOJson
                {
                    Id = aba.ID,
                    Titulo = aba.ABA_NM,
                    Subtitulo = aba.ABA_DSC,
                    Perguntas = perguntas
                });
            }
            questDinamico.Add(new QuestionarioDinamicoDTOJson
            {
                Titulo = questionario.QUEST_NM,
                Subtitulo = questionario.QUEST_DSC,
                MenuAbas = menuAbas,
                Abas = new List<AbasQuestionarioDTOJson>()
            });
        }
    }*/
    public enum EnumTipoResposta
    {
        Solicitacao = 1,
        Fornecedor = 2
    }
    /*
    //--
    public class QuestionarioDinamicoDTOJson
    {
        public QuestionarioDinamicoDTOJson()
        {
            MenuAbas = new List<MenuAbaQuestionarioDTOJson>();
            Abas = new List<AbasQuestionarioDTOJson>();
        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public List<MenuAbaQuestionarioDTOJson> MenuAbas { get; set; }
        public List<AbasQuestionarioDTOJson> Abas { get; set; }
    }
    public class MenuAbaQuestionarioDTOJson
    {
        public MenuAbaQuestionarioDTOJson()
        {
        }
        public bool Ativo { get; set; }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }

        public bool Visivel { get; set; }
    }
    public class AbasQuestionarioDTOJson
    {
        public AbasQuestionarioDTOJson()
        {
            Perguntas = new List<PerguntaAbaQuestionarioDTOJson>();
        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public List<PerguntaAbaQuestionarioDTOJson> Perguntas { get; set; }
    }
    public class PerguntaAbaQuestionarioDTOJson
    {
        public PerguntaAbaQuestionarioDTOJson()
        {
            Pai = new List<PerguntaPaiQuestionarioDTOJson>();
            DominioResposta = new List<PerguntaDominioQuestionarioDTOJson>();
        }
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public bool Dominio { get; set; }
        public bool Obrigatorio { get; set; }
        public string Resposta { get; set; }
        public TipoResposta Tipo { get; set; }
        public List<PerguntaPaiQuestionarioDTOJson> Pai { get; set; }
        public List<PerguntaFilhoQuestionarioDTOJson> Filho { get; set; }
        public List<PerguntaDominioQuestionarioDTOJson> DominioResposta { get; set; }
    }
    public class PerguntaDominioQuestionarioDTOJson
    {
        public int Id { get; set; }
        public string Valor { get; set; }
        public bool Selected { get; set; }
    }
    public class PerguntaPaiQuestionarioDTOJson
    {
        public int Id { get; set; }
        public bool Respondido { get; set; }
    }
    public enum TipoResposta
    {
        Texto = 1,
        Dominio = 2,
        Checkbox = 3,
        Radiobutton = 4
    }
     */
    /*
    var predicate = PredicateBuilder.True<QIC_QUESTIONARIO>();
    if (modelo.ContratanteId != 0)
        predicate = predicate.And(x => x.CONTRATANTE_ID == modelo.ContratanteId);
    //if (modelo.RespostaUsuario)
    //    predicate = predicate.And(x => x.LE_INFO_COMPL == modelo.RespostaUsuario);

    if (modelo.PapelId != 0)
        predicate = predicate.And(x => x.QIC_QUEST_ABA.Any(
            y => y.QIC_QUEST_ABA_PERG.Any(
                z => z.QIC_QUEST_ABA_PERG_PAPEL.Any(
                    w => w.PAPEL_ID == modelo.PapelId))));
    if(modelo.TipoResposta == EnumTipoResposta.Solicitacao)
    {
        if (modelo.SolicitacaoId != 0)
            predicate = predicate.And(x => x.QIC_QUEST_ABA.Any(
                y => y.QIC_QUEST_ABA_PERG.Any(
                    z => z.WFD_INFORM_COMPL.Any(
                        w => w.SOLICITACAO_ID == modelo.SolicitacaoId))));
    }
    else
    {
        if (modelo.PjpfId != 0)
            predicate = predicate.And(x => x.QIC_QUEST_ABA.Any(
                y => y.QIC_QUEST_ABA_PERG.Any(
                    z => z.WFD_PJPF_INFORM_COMPL.Any(
                        w => w.CONTRATANTE_PJPF_ID == modelo.PjpfId))));
    }

   List<QIC_QUESTIONARIO> listaQuestionario = Db.QIC_QUESTIONARIO
             .Include("QIC_QUESTIONARIO_CATEGORIA")
             .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.QIC_QUEST_ABA_PERG_PAPEL")
             .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.QIC_QUEST_ABA_PERG_RESP")
             .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.WFD_INFORM_COMPL")
             .Include("QIC_QUEST_ABA.QIC_QUEST_ABA_PERG.WFD_PJPF_INFORM_COMPL")
             .AsExpandable()
             .Where(predicate)
             .ToList();
    ////var listaQuestionario = ListarQuestionarios(modelo);
    //List<NovoQuestionarioVM> listaQuestionarios = new List<NovoQuestionarioVM>();
    //foreach (var questionario in listaQuestionario)
    //{
    //    List<NovoAbaVM> NovoQuestionarioAbas = new List<NovoAbaVM>();
    //    foreach (var aba in questionario.QIC_QUEST_ABA)
    //    {
    //        NovoQuestionarioAbas.Add(new NovoAbaVM { 
    //             Id = aba.ID
    //        });
    //        foreach (var pergunta in aba.QIC_QUEST_ABA_PERG)
    //        {
    //            Indice perguntaResposta = new Indice();
    //            //PopularPerguntaPorPapel(pergunta, perguntaResposta, modelo.PapelId);
    //        }
    //    }

    //    listaQuestionarios.Add(new NovoQuestionarioVM { 
    //        Id = questionario.ID,
    //        Abas = NovoQuestionarioAbas
    //    });
    //}
   modelo.QuestionarioList = listaQuestionario.ToList();
     */
    //--
    public class TesteQuestionarioVM
    {
        public TesteQuestionarioVM()
        {
            QuestionarioList = new List<QIC_QUESTIONARIO>();
        }
        public int CategoriaId { get; set; }
        public int SolicitacaoId { get; set; }
        public int ContratanteId { get; set; }
        public int PapelId { get; set; }
        public int PjpfId { get; set; }
        public int QuestionarioId { get; set; }
        public bool RespostaUsuario { get; set; }

        public EnumTipoResposta TipoResposta { get; set; }

        public List<QIC_QUESTIONARIO> QuestionarioList { get; set; }
        public List<QuestionarioDinamicoVM> QuestionarioDinamicoList { get; set; }
    }
    public class QuestionarioDinamicoVM
    {
        public string Id { get; set; }

        public string Filhos { get; set; }
    }


    /*
    public class NovoQuestionarioVM
    {
        public NovoQuestionarioVM()
        {
        }
        public int Id { get; set; }
        public List<NovoAbaVM> Abas { get; set; }
    }
    public class NovoAbaVM
    {
        public NovoAbaVM()
        {
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<Indice> Indices { get; set; }
    }
    public class Indice
    {
        public Indice()
        {
        }
        public int IdPergunta { get; set; }
        public string Questao { get; set; }
        public string Resposta { get; set; }
        public bool Leitura { get; set; }

        public bool Obrigatorio { get; set; }

        public bool Escrita { get; set; }
    }
     */
}