using System;
using System.Web.Mvc;
using WebForLink.Application.Interfaces.WebForLink;

namespace WebForLink.Web.Controllers
{
    public class FornecedorIndividualController : Controller
    {
        private readonly ICriacaoContratanteWebForLinkAppService contratantebp;
        public FornecedorIndividualController(ICriacaoContratanteWebForLinkAppService contratante)
        {
            contratantebp = contratante;
        }
        // GET: FornecedorIndividual
        public ActionResult Index()
        {
            contratantebp.Usuario = new Domain.Models.Usuario()
            {
                CONTRATANTE_ID = null,
                NOME = "Nelson Neto",
                EMAIL = "nelson.ash@outlook.com",
                SENHA = "1000:GOVDjUgvS4PqbgHkr6Q3uRmFXy5EFPD5:V+GLrEZIIoGcY1h0gwFMVR+AiPdcSHy9",
                DAT_NASCIMENTO = null,
                PRINCIPAL = true,
                TROCAR_SENHA = null,
                CPF_CNPJ = "05806190790",
                ATIVO = true,
                CARGO = "Analista",
                FIXO = "21934567890",
                CELULAR = "2134567890",
                LOGIN = "nelson.neto",
                LOGIN_SSO = null,
                DOMINIO = null,
                PRIMEIRO_ACESSO = true,
                DT_ATIVACAO = null,
                DT_CRIACAO = DateTime.Now,
                CONTA_TENTATIVA = 0,
                EXPIRA_EM_DIAS = 0
                //ICollection<Compartilhamentos> Compartilhamentos { get; set; }
                //ICollection<WAC_ACESSO_LOG> WAC_ACESSO_LOG { get; set; }
                //Contratante Contratante { get; set; }
                //ICollection<WFD_PJPF_BASE_CONVITE> WFD_PJPF_BASE_CONVITE { get; set; }
                //ICollection<WFD_PJPF_BASE_IMPORTACAO> WFD_PJPF_BASE_IMPORTACAO { get; set; }
                //ICollection<WFD_PJPF_DOCUMENTOS_VERSAO> WFD_PJPF_DOCUMENTOS_VERSAO { get; set; }
                //ICollection<WFD_SOLICITACAO> WFD_SOLICITACAO { get; set; }
                //ICollection<WFD_SOLICITACAO_PRORROGACAO> WFD_SOLICITACAO_PRORROGACAO { get; set; }
                //ICollection<WFD_SOLICITACAO_TRAMITE> WFD_SOLICITACAO_TRAMITE { get; set; }
                //ICollection<WFD_USUARIO_SENHAS_HIST> WFD_USUARIO_SENHAS_HIST { get; set; }
                //ICollection<WAC_PERFIL> WAC_PERFIL { get; set; }
                //ICollection<Contratante> WFD_CONTRATANTE1 { get; set; }
                //ICollection<Papel> Papel { get; set; }
            };
            contratantebp.Inicializar();
            return View();
        }
    }
}