using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Areas.Administracao.Models;

namespace WebForLink.Web.ViewModels
{
    public class MeusDocumentosVM : ViewModelPadrao
    {
        public int ID { get; set; }

        [Display(Name = "Tipo de Documento")]
        public string TipoDocumento { get; set; }

        [Display(Name = "Descrição do Documento")]
        public string DescricaoDocumento { get; set; }

        [Display(Name = "Arquivo")]
        public string NomeArquivo { get; set; }

        [Display(Name = "Data de Upload")]
        public DateTime? DataUpload { get; set; }

        [Display(Name = "Data de Validade")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataValidade { get; set; }

        public string ValidaDataValidade { get { return DataValidade.HasValue ? DataValidade.Value.ToString("dd/MM/yyyy") : "Sem Validade"; } }

        [Display(Name = "Sem Validade")]
        public bool SemValidade { get; set; }

        [Display(Name = "Data de Emissão")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Required(ErrorMessage = "Informe a Data de Emissão")]
        public DateTime? DataEmissao { get; set; }

        public int? ArquivoId { get; set; }

        public byte[] Arquivo { get; set; }

        public string UrlArquivo { get; set; }

        public bool Ativo { get; set; }

        public string ArquivoSubido { get; set; }
        public string TipoArquivoSubido { get; set; }
        public string ArquivoSubidoOriginal { get; set; }

        public static List<MeusDocumentosVM> ModelToViewModel(List<DocumentosDoFornecedor> meusDocumentos, UrlHelper Url)
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            List<MeusDocumentosVM> meusDocumentoVM = Mapper.Map<List<MeusDocumentosVM>>(meusDocumentos);
            meusDocumentoVM.ForEach(x =>
            {
                //x.DescricaoDocumento = x.DescricaoDocumento.DiminuirEAdicionarTresPontosADescricaoDeDocumento(60);
                x.UrlArquivo = Url.Action("MeusDocumentosArquivo", "MeusDocumentos", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("idDoc={0}&local=Interno", x.ID.ToString()), "r10X310y")
                });
                x.UrlEditar = Url.Action("MeusDocumentosFrm", "MeusDocumentos", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=Alterar", x.ID.ToString()), "r10X310y")
                });
                x.UrlExcluir = Url.Action("MeusDocumentosFrm", "MeusDocumentos", new
                {
                    chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=Excluir", x.ID.ToString()), "r10X310y")
                });
            });
            return meusDocumentoVM;
        }
    }
    public static class GridAlteracao
    {
        public static string DiminuirEAdicionarTresPontos(this string descricaoDocumento, int tamanho)
        {
            var ultimoEspacoAntesDe30 = descricaoDocumento.Split(' ');

            if (!string.IsNullOrEmpty(descricaoDocumento))
                if (descricaoDocumento.Length > tamanho)
                    return string.Format("{0} ...", descricaoDocumento.Substring(0, descricaoDocumento.Substring(0, tamanho).LastIndexOf(' ')));
            return descricaoDocumento;
        }
    }
}