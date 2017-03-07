using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebForLink.Domain.Entities.WebForLink;
using System;
using System.Linq;

namespace WebForLink.Web.ViewModels
{
    public class DadosContatoVM
    {
        public static List<DadosContatoVM> ModelToViewModel(List<FORNECEDOR_CONTATOS> modelList)
        {
            return Mapper.Map<List<FORNECEDOR_CONTATOS>, List<DadosContatoVM>>(modelList);
        }

        public static List<FORNECEDOR_CONTATOS> ViewModelToModel(List<DadosContatoVM> modelList)
        {
            return Mapper.Map<List<DadosContatoVM>, List<FORNECEDOR_CONTATOS>>(modelList);
        }

        public int ContatoID { get; set; }
        public int? PjPfId { get; set; }
        [DisplayName("Nome Contato")]
        public string NomeContato { get; set; }
        [DisplayName("E-Mail")]
        [Required(ErrorMessage = "Informe o E-Mail do Contato")]
        public string EmailContato { get; set; }
        [DisplayName("Telefone")]
        //[RegularExpression(@"^\d{2}?\d{4,5}?\d{4}$", ErrorMessage = "Entered phone format is not valid.")]
        public string Telefone { get; set; }
        [DisplayName("Celular")]
        public string Celular { get; set; }
        public bool Estrangeiro { get; set; }

        public static List<SolicitacaoModificacaoDadosContato> ViewModelToModel(List<DadosContatoVM> dadosContatos, int solicitacaoCriacaoID)
        {
            return Mapper.Map<List<DadosContatoVM>, List<SolicitacaoModificacaoDadosContato>>(dadosContatos)
                   .Select(x =>
                   {
                       x.SOLICITACAO_ID = solicitacaoCriacaoID;
                       return x;
                   }).ToList();
        }
    }
}