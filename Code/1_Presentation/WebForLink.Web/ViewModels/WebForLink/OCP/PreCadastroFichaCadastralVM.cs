using System.Collections.Generic;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels.OCP
{
    public class PreCadastroFichaCadastralVM : FichaCadastralModel
    {
        public PreCadastroFichaCadastralVM()
            : base()
        {
        }

        public PreCadastroFichaCadastralVM(OrgaosPublicosFichaCadastralVM dadosRobo,
        List<DadosEnderecosFichaCadastralVM> enderecos, List<DadosBancariosFichaCadastralVM> bancarios, List<DadosContatosFichaCadastralVM> contatos,
        List<UnspscFichaCadastralVM> unspsc, List<InformacaoComplementaresFichaCadastralVM> informacaoComplementares)
            : base(dadosRobo, enderecos, contatos, unspsc, informacaoComplementares)
        {
            //DadosSolicitacao = new DadosSolicitacaoFichaCadastralVM();
            //DadosGerais = new BoxGridVM<DadosGeraisFichaCadastralVM>() { DadosRetornados = gerais };
            //DadosBancarios = new BoxGridVM<DadosBancariosFichaCadastralVM>() { DadosRetornados = bancarios };
            //DadosDocumentos = new BoxGridVM<DocumentosFichaCadastralVM>() { DadosRetornados = documentos };
        }

        //public List<DadosGeraisFichaCadastralVM> Gerais { get; private set; }
        //public List<DocumentosFichaCadastralVM> Documentos { get; private set; }
        public OrgaosPublicosFichaCadastralVM DadosRobo { get; private set; }
        public List<DadosEnderecosFichaCadastralVM> Enderecos { get; private set; }
        public List<DadosBancariosFichaCadastralVM> Bancarios { get; private set; }
        public List<DadosContatosFichaCadastralVM> Contatos { get; private set; }
        public List<UnspscFichaCadastralVM> Unspsc { get; private set; }
        public List<InformacaoComplementaresFichaCadastralVM> InformacaoComplementares { get; private set; }


        public EnumTiposFluxo TipoFluxo { get; set; }
        public int Id { get; internal set; }
        //-- Dados Solicitacao, deve ser substituida pela importação
        //public DadosSolicitacaoFichaCadastralVM DadosSolicitacao { get; set; }
        //-- Dados Gerais
        //public BoxGridVM<DadosGeraisFichaCadastralVM> DadosGerais { get; set; }
        //-- Dados Bancários Importação é sem banco
        //public BoxGridVM<DadosBancariosFichaCadastralVM> DadosBancarios { get; set; }
        //-- Documentos
        //public BoxGridVM<DocumentosFichaCadastralVM> DadosDocumentos { get; set; }
    }
}