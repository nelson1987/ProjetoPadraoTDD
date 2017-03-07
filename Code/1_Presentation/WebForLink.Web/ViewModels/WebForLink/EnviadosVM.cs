using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Web.ViewModels
{
    public class EnviadosPesquisaVM : ModelPesquisar
    {
        public EnviadosPesquisaVM()
        {
            EnviadosGrid = new List<EnviadosVM>();
        }
        public string DataEnvioEntre { get; set; }
        public string Destinatario { get; set; }
        public string Fornecedor { get; set; }
        public int? TipoDocumento { get; set; }
        public int? DescricaoDocumento { get; set; }
        public List<EnviadosVM> EnviadosGrid { get; set; }
    }
    public class EnviadosVM
    {
        public EnviadosVM()
        {
            Emails = new List<EnviadosEmailsVM>();
            DocumentosEnviados = new List<string>();
            DadosContatos = new List<DadosContatoVM>();
            DadosEnderecos = new List<DadosEnderecosVM>();
            DadosBancarios = new List<DadosBancariosVM>();
        }
        public int MeusCompartilhamentosId { get; set; }
        public string EnviadoEm { get; set; }
        public string Para { get; set; }
        public int QtdDocs { get; set; }
        public string Disponibilidade { get; set; }
        public bool SemPrazo { get; set; }
        public string UrlVer { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public string Chave { get; set; }
        public List<EnviadosEmailsVM> Emails { get; set; }
        public string EmailsText { get; set; }
        public string EmailsValue { get; set; }
        public List<string> DocumentosEnviados { get; set; }
        public string DocumentosEnviadosText { get; set; }
        public string DocumentosEnviadosValue { get; set; }
        public List<DadosContatoVM> DadosContatos { get; set; }
        public List<DadosEnderecosVM> DadosEnderecos { get; set; }
        public List<DadosBancariosVM> DadosBancarios { get; set; }

        public static EnviadosVM ModelToViewModel(Compartilhamentos comp, UrlHelper url)
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            EnviadosVM enviado = new EnviadosVM();
            enviado.MeusCompartilhamentosId = comp.ID;
            enviado.EnviadoEm = Convert.ToDateTime(comp.ENVIADO_EM).ToString();
            enviado.Assunto = comp.ASSUNTO;
            enviado.QtdDocs = comp.DocumentosCompartilhados.GroupBy(d => d.PJPF_DOCUMENTO_ID).Count();
            enviado.Disponibilidade = !comp.SEM_PRAZO
                ? Convert.ToDateTime(comp.VALIDADE).ToShortDateString()
                : "Sem Prazo";

            string para = "";

            foreach (dynamic docs in comp.WFD_DESTINATARIO.ToList())
            {
                if (!docs.EMAIL_AVULSO)
                    para += docs.NOME;
                else
                    para += docs.EMAIL;

                para += ", ";
            }
            if (para.Length > 25)
                para = para.Substring(0, 25) + "...";
            else if (para.Length > 2)
                para = para.Substring(0, para.Length - 2);

            enviado.Para = para;
            enviado.UrlVer = url.Action("EnviadosFrm", "MeusDocumentos", new
            {
                chaveurl = Cripto.Criptografar(string.Format("idComp={0}", comp.ID.ToString()), "r10X310y")
            });
            return enviado;
        }
        public static List<EnviadosVM> ModelToViewModel(IList<Compartilhamentos> registrosPagina, UrlHelper url)
        {
            var enviados = new List<EnviadosVM>();
            foreach (Compartilhamentos comp in registrosPagina)
            {
                enviados.Add(EnviadosVM.ModelToViewModel(comp, url));
            }
            return enviados;
        }
    }
}