using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class ProrrogacaoPrazoVM
    {
        public ProrrogacaoPrazoVM()
        {
        }
        public ProrrogacaoPrazoVM(DateTime data)
        {
            this.DataProrrogacao = data;
            this.StDataProrrogacao = data.ToShortDateString();
        }

        public int ID { get; set; }

        public string Motivo { get; set; }

        [Display(Name = "Data de prorrogação")]        
        public DateTime DataProrrogacao { get; set; }

        [Display(Name = "Data de prorrogação")]        
        public string StDataProrrogacao { get; set; }

        public string PrazoPreenchimento { get; set; }

        public string Status { get; set; }

        public DateTime? DataSolicitacaoProrrogacao { get; set; }

        public string MotivoReprovacao { get; set; }

        public bool? Aprovado { get; set; }

        public SolicitacaoFornecedoresVM Fornecedor { get; set; }

        public  static ProrrogacaoPrazoVM ToViewModel(SOLICITACAO_PRORROGACAO solicitacaoProrrogacao)
        {
            return Mapper.Map<ProrrogacaoPrazoVM>(solicitacaoProrrogacao);
        }
    }
}
