using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebForLink.Web.ViewModels.Adesao
{
    public class AdesaoCriacaoFormularioVM
    {
        public AdesaoCriacaoFormularioVM()
        {
        }

        public int Id { get; set; }
        public int PlanoId { get; internal set; }

        [DisplayName("Espaço em Disco")]
        [Required(ErrorMessage = "Selecione o espaço")]
        public int EspacoSelecionado { get; set; }
        public List<SelectListItem> EspacoList { get; set; }

        [DisplayName("Quantidade de Usuários")]
        [Required(ErrorMessage = "Selecione os usuários")]
        public int UsuarioSelecionado { get; set; }
        public List<SelectListItem> UsuarioList { get; set; }
        public string Total { get; set; }

        public string MediaEspaçoDocumentos { get; internal set; }

        public void CalcularTotal(int[] planoEscolhido)
        {
            int tamanhoEspaco = planoEscolhido[0];
            int quantidadeUsuarios = planoEscolhido[1];

            double precoEspaco = 20.00;
            double precoUsuario = 4.00;
            double servico = 0.99;

            double precoTotal = (precoEspaco * tamanhoEspaco) + (precoUsuario * quantidadeUsuarios) + servico;
            //TODO: Implementar se necessário
            //double totalArredondado = Math.Round(precoTotal, 2, MidpointRounding.AwayFromZero);

            switch (tamanhoEspaco)
            {
                case 1:
                    precoTotal = 1500.00;
                    break;
                case 2:
                    precoTotal = 2300.00;
                    break;
                case 3:
                    precoTotal = 2900.00;
                    break;
                case 4:
                    precoTotal = 3800.00;
                    break;
            }

            string retorno = string.Format("R$ {0:N}", precoTotal);

            Total = retorno.Replace(",",".").Replace(".", ",");
        }
    }
}