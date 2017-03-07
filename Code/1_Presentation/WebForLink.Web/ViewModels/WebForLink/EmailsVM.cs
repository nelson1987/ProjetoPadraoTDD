using AutoMapper;
using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class EmailsVM 
    {
        public static List<EmailsVM> ModelToViewModel(List<FORNECEDORBASE_CONTATOS> modelList)
        {
            return Mapper.Map<List<EmailsVM>>(modelList);
        }

        public static List<EmailsVM> ModelToViewModel(List<FORNECEDOR_CONTATOS> modelList)
        {
            return Mapper.Map<List<EmailsVM>>(modelList);
        }

        public string value { get; set; }
        public string text { get; set; }
    }
}