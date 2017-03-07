using AutoMapper;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Web.Areas.Administracao.Models;

namespace WebForDocs.Areas.Administracao.Controllers
{
    public class TextoModel
    {
        public string dataTexto { get; set; }
    }

    public class HorarioDB
    {
        public DateTime dataTempo { get; set; }
    }

    public class TesteController : Controller
    {
        private string key = "J4Td45d2";
        // GET: Administracao/Teste
        public ActionResult Index()
        {
            try
            {

                /*
                //PROFILE
                Mapper.CreateMap<string, DateTime>().ConvertUsing(new Mappers.DateTimeTypeConverter());
                Mapper.CreateMap<TextoModel, HorarioDB>()
                    .ForMember(dest => dest.dataTempo, src => src.MapFrom(x=>x.dataTexto));
                Mapper.AssertConfigurationIsValid();
                
                //INSTANCIA MODELO
                var modelo = new TextoModel
                {
                    dataTexto = "1980/04/12"
                };
                //
                HorarioDB modeloMapeado = Mapper.Map<TextoModel, HorarioDB>(modelo);
                */

                int idPerfil = 1;
                //int idPapel = 1;
                UsuarioAdministracaoModel usuario = new UsuarioAdministracaoModel
                {
                    ContratanteID = 1,
                    Login = "usuario.contas2",
                    Nome = "Usuário Contas a pagar",
                    Email = "nelson.neto@chconsultoria.com.br",
                    Senha = "1000:kyImaCI1hPRxHR0XMAnok+Pojk9EIuBa:zPckmeapXHjxoCQIeyGcAWDnIq8hcfg8",
                    DtNascimento = "31/12/1980",
                    Administrador = false,
                    CPF = "245.572.631-27",
                    Cargo = "MDM",
                };
                usuario.Ativo = true;
                usuario.PrimeiroAcesso = false;
                usuario.DtCriacao = DateTime.Now;
                usuario.ContaTentativa = 0;

                var usuarioMapeado = Mapper.Map<UsuarioAdministracaoModel, WFD_USUARIO>(usuario);

                db.Entry(usuarioMapeado).State = EntityState.Added;


                //int repeticoes = db.WFD_USUARIO_SENHAS_HIST.Count(x => x.USUARIO_ID == usuario.ID);
                //
                //if (repeticoes >= 6)
                //    ExcluirPrimeiroHistoricoSenhaUsuario(usuario); //Excluir senha mais antiga

                //IncluirHistoricoSenhaUsuario(usuario);
                //
                //IncluirPapelUsuario(idPapel, usuario);
                //
                //IncluirPerfilUsuario(idPerfil, usuario);
                //db.SaveChanges();
            }

            catch (Exception ex)
            {
                ViewBag.Erro = ex.Message;
            }

            return View();
        }

        private void ExcluirPrimeiroHistoricoSenhaUsuario(UsuarioAdministracaoModel usuario)
        {
            var ultimoHistoricoSenha = db.WFD_USUARIO_SENHAS_HIST.FirstOrDefault(x => x.USUARIO_ID == usuario.ID);
            db.Entry(ultimoHistoricoSenha).State = EntityState.Deleted;
        }

        private void IncluirHistoricoSenhaUsuario(UsuarioAdministracaoModel usuario)
        {
            WFD_USUARIO_SENHAS_HIST historico = new WFD_USUARIO_SENHAS_HIST
            {
                SENHA = usuario.Senha,
                SENHA_DT = usuario.DtCriacao,
                WFD_USUARIO = Mapper.Map<WFD_USUARIO>(usuario)
                //USUARIO_ID = usuario.ID
            };
            db.Entry(historico).State = EntityState.Added;
        }

        private void IncluirPapelUsuario(int idPapel, UsuarioAdministracaoModel usuario)
        {
            WFL_PAPEL papel = db.WFL_PAPEL.FirstOrDefault(x => x.ID == idPapel);
            if (papel != null)
            {
                papel.WFD_USUARIO.Add(Mapper.Map<WFD_USUARIO>(usuario));
                db.Entry(papel).State = EntityState.Modified;
            }
            else
            {
                throw new Exception("Problemas relacionados ao Papel");
            }
        }

        private void IncluirPerfilUsuario(int idPerfil, UsuarioAdministracaoModel usuario)
        {
            WAC_PERFIL perfil = db.WAC_PERFIL.FirstOrDefault(x => x.ID == idPerfil && x.CONTRATANTE_ID == usuario.ContratanteID);
            if (perfil != null)
            {
                perfil.WFD_USUARIO.Add(Mapper.Map<WFD_USUARIO>(usuario));
                db.Entry(perfil).State = EntityState.Modified;
            }
            else
            {
                throw new Exception("Problemas relacionados ao Perfil");
            }
        }
    }
}