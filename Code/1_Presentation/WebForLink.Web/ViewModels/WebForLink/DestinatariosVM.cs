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
    public class ModelPesquisar
    {
        public string MensagemSucesso { get; set; }
        public int? Pagina { get; set; }
    }

    public class DestinatariosPesquisaVM : ModelPesquisar
    {
        public DestinatariosPesquisaVM()
        {
            DestinatarioGrid = new List<DestinatariosVM>();
        }

        public string Nome { get; set; }
        [Display(Name = "E-mail")]
        public string Email { get; set; }
        public string Empresa { get; set; }
        public List<DestinatariosVM> DestinatarioGrid { get; set; }
    }
    public class DestinatariosVM : ViewModelPadrao
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        public string Nome { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Informe o Email")]
        public string Email { get; set; }

        [Display(Name = "Observação")]
        public string Obs { get; set; }

        [Display(Name = "Empresa")]
        public string Empresa { get; set; }

        [Display(Name = "Data de Validade")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DataValidade { get; set; }

        [Display(Name = "Email Avulso")]
        public bool EmailAvulso { get; set; }

        public bool Ativo { get; set; }

        [Display(Name = "Sobrenome")]
        public string Sobrenome { get; set; }

        [Display(Name = "Tel. Fixo")]
        public string TelefoneFixo { get; set; }

        public string Celular { get; set; }

        [Display(Name = "Tel. Trabalho")]
        public string TelefoneTrabalho { get; set; }

        public string Fax { get; set; }

        public void Validar(UrlHelper Url)
        {
            EncryptDecryptQueryString Cripto = new EncryptDecryptQueryString();
            string key = "r10X310y";

            UrlEditar = Url.Action("Editar", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}", ID), key)
            });

            UrlExcluir = Url.Action("Excluir", new
            {
                chaveurl = Cripto.Criptografar(string.Format("id={0}", ID), key)
            });
        }
        public static List<DestinatariosVM> ModelToViewModel(IList<DESTINATARIO> modelList, UrlHelper url)
        {
            var viewModel = Mapper.Map<List<DestinatariosVM>>(modelList);
            foreach (var item in viewModel)
            {
                item.Validar(url);
            }
            return viewModel;
        }
    }
}