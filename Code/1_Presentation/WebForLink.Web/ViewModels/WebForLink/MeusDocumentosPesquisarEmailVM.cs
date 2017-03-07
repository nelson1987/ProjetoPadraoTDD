using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class MeusDocumentosPesquisarEmailVM
    {
        public MeusDocumentosPesquisarEmailVM() 
        {
            Grid = new List<MeusDocumentosPesquisarEmailGridVM>();
        }
        public int ID { get; set; }
        public string IdsIncluidos { get; set; }
        public string Nome {get; set;}
        public string Email {get; set;}
        public string Empresa { get; set; }

        public List<MeusDocumentosPesquisarEmailGridVM> Grid { get; set; }
    }
    public class MeusDocumentosPesquisarEmailGridVM
    {
        public static MeusDocumentosPesquisarEmailGridVM ModelToViewModel(DESTINATARIO model)
        {
            return Mapper.Map<MeusDocumentosPesquisarEmailGridVM>(model);
        }

        public static MeusDocumentosPesquisarEmailGridVM ModelToViewModel(FORNECEDORBASE_CONTATOS model)
        {
            return Mapper.Map<MeusDocumentosPesquisarEmailGridVM>(model);
        }

        public static MeusDocumentosPesquisarEmailGridVM ModelToViewModel(FORNECEDOR_CONTATOS model)
        {
            return Mapper.Map<MeusDocumentosPesquisarEmailGridVM>(model);
        }

        public int Id { get; set; }

        [DisplayName("Nome/Empresa")]
        public string Nome { get; set; }

        public string Origem { get; set; }

        public string Email { get; set; }

        public string Empresa { get; set; }

        public bool IsChecked { get; set; }
    }
}