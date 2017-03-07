using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using AutoMapper;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.Areas.Administracao.Models;

namespace WebForLink.Web.ViewModels
{
    public class ListaDocumentosVM : ViewModelPadrao
    {
        public ListaDocumentosVM()
        {
        }
        public ListaDocumentosVM(int id, bool exigeValidade, int? periodicidade)
        {
            ID = id;
            ExigeValidade = exigeValidade;
            Periodicidade = periodicidade;

            if (ExigeValidade)
                TipoAtualizacao = 2;
            else if (Periodicidade != null)
                TipoAtualizacao = 3;
            else
                TipoAtualizacao = 1;
        }
        public int ID { get; set; }

        [Display(Name = "Grupo Documento")]
        public string TipoDocumento { get; set; }

        [Display(Name = "Documento")]
        public string DescricaoDocumento { get; set; }

        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        public int TipoAtualizacao { get; set; }

        public string TipoAtualizacaoDesc { get; set; }

        [Display(Name = "Exige Validade")]
        public bool ExigeValidade { get; set; }

        [Display(Name = "PeriodicidadeDoDocumento")]
        public int? Periodicidade { get; set; }

        public bool Obrigatorio { get; set; }

        public bool Ativo { get; set; }

        public void Validar()
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            string key = "r10X310y";

            UrlEditar = Url.Action("ListaDocumentosFrm", "Documento", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=Alterar", ID), key)
            });

            UrlExcluir = Url.Action("ListaDocumentosFrm", "Documento", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}&Acao=Excluir", ID), key)
            });
        }

        public static List<ListaDocumentosVM> ModelToViewModel(List<ListaDeDocumentosDeFornecedor> listaDocumentos, UrlHelper url)
        {
            var viewModel = Mapper.Map<List<ListaDocumentosVM>>(listaDocumentos);
            foreach (var model in viewModel)
            {
                model.Url = url;
                model.Validar();
            }
            return viewModel;
        }
    }
}