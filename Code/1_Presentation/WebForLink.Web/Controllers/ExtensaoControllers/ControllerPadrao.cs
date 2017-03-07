using log4net;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebForLink.Application.Services.Process;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Infrastructure;
using WebForLink.Web.Interfaces;

namespace WebForLink.Web.Controllers.Extensoes
{
    public class ControllerPadrao : Controller
    {
        public ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
        public WebForLinkContexto Db = new WebForLinkContexto();

        public string Key = string.Empty;
        public int TamanhoPagina = 10;
        public string[] extensoesNaoPermitadas = { "BAT", "BIN", "CMD", "COM", "CPL", "EXE", "GADGET", "INF1", "INS", "INX", "ISU", "JOB", "JSE", "LNK", "MSC", "MSI", "MSP", "MST", "PAF", "PIF", "PS1", "REG", "RGS", "SCR", "SCT", "SHB", "SHS", "U3P", "VB", "VBE", "VBS", "VBSCRIPT", "WS", "WSF", "WSH" };

        //public BPRepositories _repositorios { get; set; }

        #region Chamadas Para BP
        //public ITipoDocumentosService _tipoDocumentosBP;
        //public IContratanteArquivoService _contratanteArquivoBP;
        public IGeral _metodosGerais { get { return new Geral(); } }
        //private readonly IConfiguracaoWebForLinkAppService _configService;
        #endregion

        public ControllerPadrao()
        {

            //_metodosGerais = new Geral();
            Key = _metodosGerais.ValorKey();
        }


        public ContentResult UploadArquivo(string cnpjCpf, string arqTmp, string caminho)
        {
            HttpPostedFileBase hpf = Request.Files[0] as HttpPostedFileBase;
            string nomeArquivoSubido = hpf.FileName.Split(new char[] { '\\' }).LastOrDefault();
            string ext = nomeArquivoSubido.Split(new char[] { '.' }).LastOrDefault().ToUpper();

            if (extensoesNaoPermitadas.Contains(ext))
            {
                throw new Exception("Este tipo de Arquivo não é permitido");
            }

            //var caminho = _configService.BuscarConfigGeral().CAMINHO_ARQUIVOS;

            var sCnpjCpf = cnpjCpf.Replace(".", "").Replace("-", "").Replace("/", "");
            caminho += "\\Temp";
            if (!Directory.Exists(caminho))
            {
                Directory.CreateDirectory(caminho);
            }

            if (!String.IsNullOrEmpty(arqTmp))
                if (System.IO.File.Exists(caminho + "\\" + arqTmp))
                    System.IO.File.Delete(caminho + "\\" + arqTmp);

            string nomeArquivo = sCnpjCpf + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "##" + nomeArquivoSubido;
            string arquivoCompleto = caminho + "\\" + nomeArquivo;
            hpf.SaveAs(arquivoCompleto);

            return Content("{\"original\":\"" + nomeArquivoSubido + "\",\"nome\":\"" + nomeArquivo + "\",\"tipo\":\"" + hpf.ContentType + "\"}", "application/json");
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