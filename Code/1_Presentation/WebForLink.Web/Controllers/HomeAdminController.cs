using System;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.Infrastructure;

namespace WebForLink.Web.Controllers
{
    public class HomeAdminController : ControllerPadrao
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult LogoFoto()
        {
            int contratanteId = (int)Geral.PegaAuthTicket("ContratanteId");
            ViewBag.ContratanteID = contratanteId;
            return View();
        }

        [HttpPost]
        public ActionResult LogoFoto(int ContratanteId, HttpPostedFileBase file)
        {
            ViewBag.ContratanteID = ContratanteId;
            int grupoId = (int)Geral.PegaAuthTicket("Grupo");
            int usuarioId = (int)Geral.PegaAuthTicket("UsuarioId");

            if (file == null)
            {
                ModelState.AddModelError("", "Escolha uma Imagem!");
            }
            else
            {
                MemoryStream target = new MemoryStream();
                file.InputStream.CopyTo(target);

                Bitmap imagem1 = (Bitmap)Image.FromStream(target);
                Bitmap imagemRedimensionada = ScaleImage(imagem1, 220, 130);

                MemoryStream novaImagem = new MemoryStream();
                imagemRedimensionada.Save(novaImagem, System.Drawing.Imaging.ImageFormat.Png);

                Contratante contratante = Db.Contratante.FirstOrDefault(c => c.WFD_GRUPO.Any(g => g.ID == grupoId));
                if (contratante != null)
                {
                    contratante.LOGO_FOTO = novaImagem.ToArray();

                    //string extensao = (file.FileName.IndexOf(".") >= 0) ? file.FileName.Substring(file.FileName.IndexOf(".")) : "";
                    contratante.EXTENSAO_IMAGEM = ".png";

                    Db.Entry(contratante).State = EntityState.Modified;
                }
                Db.SaveChanges();

                string caminhoFisico = Server.MapPath("/ImagensUsuarios");
                string arquivo = "ImagemContratante" + contratante.ID + ".png";
                string caminhoCompleto = caminhoFisico + "\\" + arquivo;
                System.IO.File.WriteAllBytes(caminhoCompleto, novaImagem.ToArray());

                string dados = User.Identity.Name;
                dados = dados.Replace("semfoto.png", arquivo).Replace("semlogo.png", arquivo);

                var usuario = Db.WFD_USUARIO
                    .Include("WAC_PERFIL.WAC_FUNCAO")
                    .FirstOrDefault(u => u.ID == usuarioId);

                string roles = string.Empty;
                foreach (var perfil in usuario.WAC_PERFIL)
                {
                    foreach (var funcao in perfil.WAC_FUNCAO)
                    {
                        if (!roles.Contains(funcao.CODIGO))
                            roles += funcao.CODIGO + ",";
                    }
                }

                _metodosGerais.CriarAuthTicket(dados, roles);
                _metodosGerais.AuthenticateRequest();

                ViewBag.MensagemSucesso = "Imagem Salva com sucesso!";
            }
            return View();
        }

        public static Bitmap ScaleImage(Bitmap Imagem, int maxWidth, int maxHeight)
        {
            var ratioX = (double)maxWidth / Imagem.Width;
            var ratioY = (double)maxHeight / Imagem.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)(Imagem.Width * ratio);
            var newHeight = (int)(Imagem.Height * ratio);

            return new Bitmap(Imagem, new Size(newWidth, newHeight));
        }

        [Authorize]
        public ActionResult MinhaConta()
        {
            return View();
        }

        [Authorize]
        public ActionResult MinhaContaLst()
        {
            return View();
        }

        [Authorize]
        public ActionResult MinhaContaFrm()
        {
            return View();
        }

    }
}
