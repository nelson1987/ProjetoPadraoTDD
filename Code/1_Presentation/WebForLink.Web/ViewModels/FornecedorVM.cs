using AutoMapper;
using WebForLink.Domain.Entities;

namespace WebForLink.Web.ViewModels
{
    public class FornecedorIndividualVM
    {
        public int Id { get; set; }
        public string Documento { get; set; }
        public string RazaoSocial { get; set; }

        public static FornecedorIndividualVM ToViewModel(Fornecedor Fornecedor)
        {
            return Mapper.Map<FornecedorIndividualVM>(Fornecedor);
        }
        public static Fornecedor ToModel(FornecedorIndividualVM viewModel)
        {
            return Mapper.Map<Fornecedor>(viewModel);
        }
    }
}