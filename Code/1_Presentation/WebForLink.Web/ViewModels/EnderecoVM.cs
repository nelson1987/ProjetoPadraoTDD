using AutoMapper;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using WebForLink.Domain.Entities;

namespace WebForLink.Web.ViewModels
{
    //[UIHint("")]
    public class EnderecoVM
    {
        public EnderecoVM()
        {

        }
        public int Id { get; set; }

        public int IdFichaCadastral { get; set; }


        public int IdForm { get; set; }
        [Required]
        [MinLength(3,ErrorMessage ="Ao menos 3")]
        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        [DisplayName("Tipo De Endereço")]
        public string Tipo { get; set; }

        public List<SelectListItem> TipoList
        {
            get
            {
                return new List<SelectListItem>()
                {
                    new SelectListItem { Value = "0", Text = "-- Selecione --" },
                    new SelectListItem { Value = "1", Text = "Residencial" },
                    new SelectListItem { Value = "2", Text = "Comercial" }
                };
            }
        }

        public List<SelectListItem> EstadosList
        {
            get
            {
                return new List<SelectListItem> {
                    new SelectListItem { Value = "0", Text = "-- Selecione --" },
                    new SelectListItem { Text = "Acre", Value = "AC" },
                    new SelectListItem { Text = "Alagoas", Value = "AL" },
                    new SelectListItem { Text = "Amapá", Value = "AP" },
                    new SelectListItem { Text = "Amazonas", Value = "AM" },
                    new SelectListItem { Text = "Bahia", Value = "BA" },
                    new SelectListItem { Text = "Ceará", Value = "CE" },
                    new SelectListItem { Text = "Distrito Federal", Value = "DF" },
                    new SelectListItem { Text = "Espírito Santo", Value = "ES" },
                    new SelectListItem { Text = "Goiás", Value = "GO" },
                    new SelectListItem { Text = "Maranhão", Value = "MA" },
                    new SelectListItem { Text = "Mato Grosso", Value = "MT" },
                    new SelectListItem { Text = "Mato Grosso do Sul", Value = "MS" },
                    new SelectListItem { Text = "Minas Gerais", Value = "MG" },
                    new SelectListItem { Text = "Pará", Value = "PA" },
                    new SelectListItem { Text = "Paraíba", Value = "PB" },
                    new SelectListItem { Text = "Paraná", Value = "PR" },
                    new SelectListItem { Text = "Pernambuco", Value = "PE" },
                    new SelectListItem { Text = "Piauí", Value = "PI" },
                    new SelectListItem { Text = "Rio de Janeiro", Value = "RJ" },
                    new SelectListItem { Text = "Rio Grande do Norte", Value = "RN" },
                    new SelectListItem { Text = "Rio Grande do Sul", Value = "RS" },
                    new SelectListItem { Text = "Rondônia", Value = "RO" },
                    new SelectListItem { Text = "Roraima", Value = "RR" },
                    new SelectListItem { Text = "Santa Catarina", Value = "SC" },
                    new SelectListItem { Text = "São Paulo", Value = "SP" },
                    new SelectListItem { Text = "Sergipe", Value = "SE" },
                    new SelectListItem { Text = "Tocantins", Value = "TO" }
                };
            }
        }

        [DisplayName("Número")]
        public string Numero { get; set; }

        [DisplayName("Complemento")]
        public string Complemento { get; set; }

        [DisplayName("CEP")]
        public string Cep { get; set; }

        [DisplayName("Bairro")]
        public string Bairro { get; set; }

        [DisplayName("Cidade")]
        public string Cidade { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; }

        [DisplayName("País")]
        public string Pais { get; set; }


        public static List<EnderecoVM> ToViewModel(ICollection<Endereco> model)
        {
            return Mapper.Map<List<EnderecoVM>>(model);
        }

        public static List<Endereco> ToModelList(List<EnderecoVM> viewModel, int id)
        {
            foreach (var item in viewModel)
            {
                item.IdFichaCadastral = id;
            }
            return Mapper.Map<List<Endereco>>(viewModel);
        }
    }
}