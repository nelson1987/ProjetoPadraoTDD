using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class DadosBancariosVM
    {
        public static List<DadosBancariosVM> ModelToViewModel(List<BancoDoFornecedor> modelList)
        {
            return Mapper.Map<List<BancoDoFornecedor>, List<DadosBancariosVM>>(modelList);
        }

        public static List<BancoDoFornecedor> ViewModelToModel(List<DadosBancariosVM> modelList)
        {
            return Mapper.Map<List<DadosBancariosVM>, List<BancoDoFornecedor>>(modelList);
        }

        public int? BancoSolicitacaoID { get; set; }
        public int? BancoPJPFID { get; set; }
        public int? ContratanteID { get; set; }
        public int? ContratantePjPfId { get; set; }

        [Required(ErrorMessage = "Informe o Banco")]
        public int? Banco { get; set; }

        public string NomeBanco { get; set; }

        [Required(ErrorMessage = "Informe a Agência")]
        [StringLength(4, MinimumLength = 4)]
        public string Agencia { get; set; }

        public string Digito { get; set; }

        [Required(ErrorMessage = "Informe a Conta Corrente")]
        public string ContaCorrente { get; set; }

        [Required(ErrorMessage = "Informe o Dígito da Conta Corrente")]
        public string ContaCorrenteDigito { get; set; }

        public bool Ativo { get; set; }

        public SelectList Bancos { get; set; }

        public DateTime? DataUpload { get; set; }
        public int? ArquivoID { get; set; }
        public HttpPostedFileBase Arquivo { get; set; }
        public string NomeArquivo { get; set; }
        public int SolicitacaoID { get; set; }
        public bool ExigeValidade { get; set; }
        public int Periodicidade { get; set; }
        public DateTime? DataValidade { get; set; }
        public string UrlArquivo { get; set; }

        public string ArquivoSubidoOriginal { get; set; }
        public string ArquivoSubido { get; set; }
        public string TipoArquivoSubido { get; set; }

        public bool Preenchido(DadosBancariosVM banco)
        {
            return banco.Banco != null && banco.Agencia != null && banco.ContaCorrente != null;
        }
    }
}