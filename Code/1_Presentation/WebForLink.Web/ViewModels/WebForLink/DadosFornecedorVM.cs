using System;
using System.Collections.Generic;
using WebForLink.Web.ViewModels.NovaFichaCadastral;

namespace WebForLink.Web.ViewModels
{
    public class DadosFornecedorVM
    {
    }
    public class DadosFornecedorDetalharVM
    {
        public int SolicitacaoId { get; set; }
        public int ContratanteId { get; set; }
        public DadosFornecedorDetalharSolicitacaoVM DadosSolicitacao { get; set; }
        public DadosFornecedorDetalharGeralVM DadosGeral { get; set; }
        public List<DadosFornecedorDetalharEnderecoVM> DadosEnderecos { get; set; }
        public List<DadosFornecedorDetalharBancoVM> DadosBancarios { get; set; }
        public List<DadosFornecedorDetalharContatoVM> DadosContatos { get; set; }
        public DadosFornecedorDetalharObservacaoVM Observacao { get; set; }
    }
    public class DadosFornecedorDetalharSolicitacaoVM
    {
        public int Codigo { get; set; }
        public string TipoSolicitacao { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Solicitante { get; set; }
        public string ReceitaFederalStatus { get; set; }
        public string SintegraStatus { get; set; }
        public string SimplesNacionalStatus { get; set; }
        public DateTime PrazoEntrega { get; set; }
        public bool isPodeReenviar { get; set; }
        public List<SituacaoSolicitacaoGridVM> SituacaoSolicitacao { get; set; }
    }
    public class DadosFornecedorDetalharGeralVM : DadosGeralDetalharVM
    {
    }
    public class DadosFornecedorDetalharEnderecoVM : DadosEnderecoDetalharVM
    {
    }
    public class DadosFornecedorDetalharBancoVM : DadosBancoDetalharVM
    {
    }
    public class DadosFornecedorDetalharContatoVM : DadosContatoDetalharVM
    {
    }
    public class DadosFornecedorDetalharObservacaoVM : ObservacaoVM
    {
    }
}
namespace WebForLink.Web.ViewModels.NovaFichaCadastral
{
    public class SituacaoSolicitacaoGridVM
    {
        public string Situacao { get; set; }
        public string Status { get; set; }
        public DateTime Data { get; set; }
    }
    public class DadosSolicitacaoDetalharSVM
    {
        public int Id { get; set; }
        public int Codigo { get; set; }
        public string TipoSolicitacao { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Solicitante { get; set; }
        public string ReceitaFederalStatus { get; set; }
        public string SintegraStatus { get; set; }
        public string SimplesNacionalStatus { get; set; }
        public DateTime PrazoEntrega { get; set; }
        public bool Reenviavel { get; set; }
        public List<SituacaoSolicitacaoGridVM> SituacaoSolicitacao { get; set; }
    }
    public class DadosGeralDetalharVM
    {
        public int Id { get; set; }
        public string Categoria { get; set; }
        public string DocumentoUtilizado { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string CNAE { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaMunicipal { get; set; }
        public int SolicitacaoId { get; set; }
        public bool Editavel { get; set; }
    }
    public class DadosEnderecoDetalharVM
    {
        public int Id { get; set; }
        public string TipoEndereco { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int SolicitacaoId { get; set; }
        public bool Editavel { get; set; }
    }
    public class DadosBancoDetalharVM
    {
        public int Id { get; set; }
        public int SolicitacaoId { get; set; }
        public bool Editavel { get; set; }
    }
    public class DadosContatoDetalharVM
    {
        public int Id { get; set; }
        public string NomeContato { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public int SolicitacaoId { get; set; }
        public bool Editavel { get; set; }
    }
    public class ObservacaoVM
    {
        public string Observacao { get; set; }
        public string SolicitacaoId { get; set; }
    }
}