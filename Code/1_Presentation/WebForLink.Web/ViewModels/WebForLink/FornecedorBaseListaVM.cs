using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels
{
    public class FornecedorBaseListaVM
    {
        public FornecedorBaseListaVM()
        {
            this.FornecedoresBaseFuncionalidade = new List<FornecedorBaseFuncionalidadeVM>();
            this.Timeline = new TimelineVM();
        }
        public int BloqueioId { get; set; }
        public FornecedorBaseFiltroVM Filtro { get; set; }
        public FornecedorBaseTopoVM Topo { get; set; }
        public List<FornecedorBaseVM> FornecedoresBase { get; set; }
        public TimelineVM Timeline { get; set; }
        public List<FornecedorBaseFuncionalidadeVM> FornecedoresBaseFuncionalidade { get; set; }
        public EnumTiposFuncionalidade TipoFuncionalidade { get; set; }

        public string Selecionados { get; set; }
        public string SelecionadosDetalhes { get; set; }
        public string FornecedoresJSON { get; set; }
        public string MensagemSucesso { get; set; }
        public List<string> NomeColunas { get; set; }
        public string NomeFuncionalidade { get; set; }
        //--
        public ProrrogacaoPrazoVM ProrrogacaoPrazo { get; set; }
        public string Mensagem { get; set; }
        public string Assunto { get; set; }
        //--
        public bool AprovaPrazo { get; set; }

        public AprovacaoPrazoVM AprovacaoProrrogacao { get; set; }

        public ReprovacaoPrazoVM ReprovacaoProrrogacao { get; set; }

        //--
        public MensagemImportacaoVM MensagemImportacao { get; set; }
        public string StDataProrrogacao { get; set; }
        [StringLength(2000, ErrorMessage = "Nome não pode ter mais que 2000 caracteres.")]
        public string Motivo { get; set; }

        //--
        public int CategoriaId { get; set; }
        public int? Pagina { get; set; }       

        public IEnumerable<int> ObterFornecedoresSelecionados()
        {
            return this.FornecedoresBaseFuncionalidade.Where(x => x.Selecionado).Select(x => x.ID).ToList<int>();
        }

        public List<SelectListItem> Arquivos { get; set; }

        public int Escolha { get; set; }
    }
}