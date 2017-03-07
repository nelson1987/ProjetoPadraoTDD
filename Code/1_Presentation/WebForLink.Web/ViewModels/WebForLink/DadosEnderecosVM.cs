using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class DadosEnderecosVM
    {
        public static List<DadosEnderecosVM> ModelToViewModel(List<FORNECEDOR_ENDERECO> modelList)
        {
            return Mapper.Map<List<FORNECEDOR_ENDERECO>, List<DadosEnderecosVM>>(modelList);
        }

        public static List<FORNECEDOR_ENDERECO> ViewModelToModel(List<DadosEnderecosVM> modelList)
        {
            return Mapper.Map<List<DadosEnderecosVM>, List<FORNECEDOR_ENDERECO>>(modelList);
        }

        public static List<SOLICITACAO_MODIFICACAO_ENDERECO> ViewModelToModel (List<DadosEnderecosVM> modelList, int solicitacaoId)
        {
            return Mapper.Map<List<DadosEnderecosVM>, List<SOLICITACAO_MODIFICACAO_ENDERECO>>(modelList)
                .Select(x =>
            {
                x.SOLICITACAO_ID = solicitacaoId;
                return x;
            }).ToList(); 
        }

        public int? ID { get; set; }

        [Display(Name = "Tipo de Endereço")]
        public int TipoEnderecoId { get; set; }

        [Display(Name = "Endereço")]
        [Required]
        public string Endereco { get; set; }

        [Display(Name = "Número")]
        public string Numero { get; set; }

        [Display(Name = "Complemento")]
        public string Complemento { get; set; }

        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Display(Name = "Bairro")]
        public string Bairro { get; set; }

        [Display(Name = "Cidade")]
        public string Cidade { get; set; }

        [Display(Name = "Estado")]
        public string UF { get; set; }

        [Display(Name = "País")]
        public string Pais { get; set; }

        public int? PjPjId { get; set; }

        public int? ContratantePjPfId { get; set; }

        public string TipoEndereco { get; set; }

        public int? SolicitacaoID { get; set; }
        public SelectList ListTipoEndereco { get; set; }
        public SelectList ListUF { get; set; }

        public virtual SOLICITACAO WFD_SOLICITACAO { get; set; }
        public virtual TIPO_ENDERECO WFD_T_TP_ENDERECO { get; set; }
        public virtual TiposDeEstado T_UF { get; set; }
    }
}