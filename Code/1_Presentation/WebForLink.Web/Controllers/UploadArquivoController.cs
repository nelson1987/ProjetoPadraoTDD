using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using WebForLink.Application.Interfaces;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Services;
using WebForLink.Web.Helpers;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Controllers
{
    [AllowAnonymous]
    public class UploadArquivoController : Controller
    {
        private const string DeleteType = "GET";
        private FilesHelper _filesHelper;
        private string _tempPath = "/somefiles/";
        private string _urlBase = "/Files/somefiles/";
        private string _deleteUrl = "/UploadArquivo/DeleteFile/?file=";
        private string _serverMapPath = "~/Files/somefiles/";

        private readonly IFichaCadastralAppService _fichaCadastralAppService;

        public UploadArquivoController(IFichaCadastralAppService fichaCadastralAppService)
        {
            _fichaCadastralAppService = fichaCadastralAppService;
            _filesHelper = new FilesHelper(_deleteUrl, DeleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);
        }

        private string StorageRoot
        {
            get {
                if (!Directory.Exists(HostingEnvironment.MapPath(_serverMapPath)))
                    Directory.CreateDirectory(HostingEnvironment.MapPath(_serverMapPath));
                return Path.Combine(HostingEnvironment.MapPath(_serverMapPath)); }
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Show()
        {
            var listOfFiles = _filesHelper.GetFileList();
            var model = new FilesViewModel
            {
                Files = listOfFiles.files
            };

            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload(int Id, string CodigoCliente, int IdSolicitacao, string ArquivoAnexado)
        {
            string path = "/DADOS/Solicitacao/";
            Criptografia criptografia = new Criptografia(EnumCripto.Criptografar, 
                string.Format("Id={0}&CodigoCliente={1}&IdSolicitacao={2}&ArquivoAnexado={3}", Id, CodigoCliente, IdSolicitacao, ArquivoAnexado), 
                "r10X310y");

            _serverMapPath = string.Format(@"{0}{1}\{2}\", path, IdSolicitacao, Id);
            _urlBase = string.Format(@"{0}{1}\{2}\", path, IdSolicitacao, 23, Id);
            _deleteUrl = string.Format("/UploadArquivo/DeleteFile/?chaveUrl={0}&file=", criptografia.Resultado, ArquivoAnexado);


            _filesHelper = new FilesHelper(_deleteUrl, DeleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);
            _filesHelper.StorageRoot = StorageRoot;

            var resultList = new List<ViewDataUploadFilesResult>();
            var currentContext = HttpContext;
            if (!_filesHelper.UploadAndShowResults(currentContext, resultList))
            {
                return Json("Error ");
            }

            var files = new JsonFiles(resultList);

            var isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            
            foreach (var item in resultList)
            {
                _fichaCadastralAppService.IncluirArquivo(IdSolicitacao, Id, item.name, item.size, item.url);
            }
            
            //Pegar nome do arquivo, local do Arquivo
            return Json(files);
        }

        public JsonResult GetFileList()
        {
            var list = _filesHelper.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteFile(string file, string chaveUrl)// int Id, string CodigoCliente, int IdSolicitacao, string ArquivoAnexado)
        {
            var descripto = new Criptografia(EnumCripto.LinkDescriptografar, chaveUrl, "r10X310y");

            var id = descripto.Resultados.FirstOrDefault(x => x.Key == "Id").Value;
            var codigoCliente = descripto.Resultados.FirstOrDefault(x => x.Key == "CodigoCliente").Value;
            var idSolicitacao = descripto.Resultados.FirstOrDefault(x => x.Key == "IdSolicitacao").Value;
            var arquivoAnexado = descripto.Resultados.FirstOrDefault(x => x.Key == "ArquivoAnexado").Value;

            _serverMapPath = string.Format("/Files/{0}/Sol_{1}/{2}/File_{3}/", codigoCliente, idSolicitacao, arquivoAnexado, id);
            _urlBase = string.Format("/Files/{0}/Sol_{1}/{2}/File_{3}/", codigoCliente, idSolicitacao, arquivoAnexado, id);
            _filesHelper = new FilesHelper(_deleteUrl, DeleteType, StorageRoot, _urlBase, _tempPath, _serverMapPath);
            _filesHelper.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
    }
}