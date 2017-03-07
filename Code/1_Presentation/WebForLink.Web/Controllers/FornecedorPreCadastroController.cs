using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Web.Controllers.Extensoes;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class FornecedorPreCadastroController : ControllerPadrao
    {
        [Authorize]
        public ActionResult DisponibilizarLink()
        {
            string chave = Db.WFD_CONFIG.FirstOrDefault(x => x.ID == 1).CHAVE_WEBSERVICE;
            List<LinkExternoPreCadastroVM> modelo = Db.Contratante.Select(x => new LinkExternoPreCadastroVM
            {
                IdContratante = x.ID,
                NomeContratante = x.RAZAO_SOCIAL
            }).ToList();
            modelo.ForEach(x=>{
                x.Chave = chave;
                x.Link = Url.Action("Incluir", "FornecedorPreCadastro", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("idContratante={0}&idChave{1}", x.IdContratante, chave), Key)
                }, Request.Url.Scheme);
            });
            return View(modelo);
        }

        [Authorize]
        public ActionResult Incluir(string chaveurl)
        {
            /*
            List<ParametroCriptografia> param = Cripto.DescriptografarUrl(chaveurl, Key);
            int idContratante;
            Int32.TryParse(param.First(p => p.Name == "idContratante").Value, out idContratante);
            InclusaoLinkExternoVM modelo = new InclusaoLinkExternoVM() 
            { 
                Chave = param.First(p => p.Name == "idChave").Value,
                IdContratante = idContratante
            };
            */

            FichaCadastralWebForLinkVM ficha = new FichaCadastralWebForLinkVM();
            return View(ficha); 
        }
    }
}
namespace WebForLink.Web.ViewModels
{
    public class LinkExternoPreCadastroVM
    {
        public LinkExternoPreCadastroVM()
        {
        }
        public int IdContratante { get; set; }
        public string NomeContratante { get; set; }
        public string Link { get; set; }

        public string Chave { get; set; }
    }
    
}