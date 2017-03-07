using System.Collections.Generic;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels.FichaCadastral
{
    public class FichaCadastralAcompanhamentoVM
    {
        public FichaCadastralAcompanhamentoVM()
        {
            Solicitacao = new FichaCadastralDadosSolicitacaoVM();
            DadosBancarios = new List<FichaCadastralDadosBancariosVM>();
            DadosContatos = new List<FichaCadastralDadosContatosVM>();
            DadosEnderecos = new List<FichaCadastralDadosEnderecosVM>();
            FornecedoresUnspsc = new List<FichaCadastralFornecedoresUnspscVM>();

        }
        public int Id { get; set; }
        public int TipoFluxoId { get; set; }
        public string Observacao { get; set; }
        public FichaCadastralDadosSolicitacaoVM Solicitacao { get; set; }
        public FichaCadastralDadosGeraisVM DadosGerais { get; set; }
        public FichaCadastralRoboVM Robo { get; set; }
        public List<FichaCadastralAnexosVM> Anexos { get; set; }
        public List<FichaCadastralQuestionarioDinamicoVM> QuestionarioDinamico { get; set; }
        public List<FichaCadastralOutrosDadosVM> OutrosDados { get; set; }
        public List<FichaCadastralDadosFiscaisVM> DadosFiscais { get; set; }
        public List<FichaCadastralBloqueioVM> Bloqueio { get; set; }
        public List<FichaCadastralDesbloqueioVM> Desbloqueio { get; set; }
        public List<FichaCadastralDadosBancariosVM> DadosBancarios { get; set; }
        public List<FichaCadastralDadosContatosVM> DadosContatos { get; set; }
        public List<FichaCadastralDadosEnderecosVM> DadosEnderecos { get; set; }
        public List<FichaCadastralFornecedoresUnspscVM> FornecedoresUnspsc { get; set; }
    }
    public class FichaCadastralDadosSolicitacaoVM
    {
        public FichaCadastralDadosSolicitacaoVM()
        {
            TramiteGrid = new List<FichaCadastralTramitesGridVM>();
        }
        public int Id { get; set; }
        public string NomeSolicitacao { get; set; }
        public string CriacaoSolicitacao { get; set; }
        public string Login { get; set; }
        public string RoboReceita { get; set; }
        public string RoboSintegra { get; set; }
        public string RoboSimples { get; set; }
        public string PrazoPreenchimento { get; set; }
        public string UrlReenviarFicha { get; set; }
        public string ContratanteGrupo { get; set; }
        public bool PermiteReenviarFicha { get; set; }
        public bool PertenceGrupoEmpresa { get; set; }
        public EnumTiposFuncionalidade TipoFuncionalidade { get; set; }
        public List<FichaCadastralTramitesGridVM> TramiteGrid { get; set; }
    }
    public class FichaCadastralTramitesGridVM
    {
        public string NomePapel { get; set; }
        public string NomeStatus { get; set; }
        public string DataFim { get; set; }
    }
    public class FichaCadastralRoboVM
    {
        public FichaCadastralRoboVM()
        {
        }
        public int Id { get; set; }
    }
    public class FichaCadastralDadosEnderecosVM : DadosEnderecosVM
    {
        public FichaCadastralDadosEnderecosVM()
        {
        }
        public int Id { get; set; }
    }
    public class FichaCadastralDadosBancariosVM : DadosBancariosVM
    {
        public FichaCadastralDadosBancariosVM()
        {
        }
        public int Id { get; set; }
    }
    public class FichaCadastralDadosContatosVM : DadosContatoVM
    {
        public FichaCadastralDadosContatosVM()
        {
        }
        public int Id { get; set; }
    }
    public class FichaCadastralFornecedoresUnspscVM
    {
        public FichaCadastralFornecedoresUnspscVM()
        {
        }
        public int Id { get; set; }
    }
    public class FichaCadastralDadosGeraisVM : DadosGeraisVM
    {
        public FichaCadastralDadosGeraisVM() { }
    }
    public class FichaCadastralAnexosVM
    {
        public FichaCadastralAnexosVM() { }
    }
    public class FichaCadastralQuestionarioDinamicoVM
    {
        public FichaCadastralQuestionarioDinamicoVM() { }
    }
    public class FichaCadastralOutrosDadosVM
    {
        public FichaCadastralOutrosDadosVM() { }
    }
    public class FichaCadastralDadosFiscaisVM
    {
        public FichaCadastralDadosFiscaisVM() { }
    }
    public class FichaCadastralBloqueioVM
    {
        public FichaCadastralBloqueioVM() { }
    }
    public class FichaCadastralDesbloqueioVM
    {
        public FichaCadastralDesbloqueioVM() { }
    }
}