using System.Collections.Generic;
using System.ComponentModel;
using WebForLink.Domain.Enums;

namespace WebForLink.Web.ViewModels.OCP
{
    public abstract class FichaCadastralModel
    {
        public FichaCadastralModel()
        {
        }
        public FichaCadastralModel(OrgaosPublicosFichaCadastralVM dadosRobo,
        List<DadosEnderecosFichaCadastralVM> enderecos, List<DadosContatosFichaCadastralVM> contatos,
        List<UnspscFichaCadastralVM> unspsc, List<InformacaoComplementaresFichaCadastralVM> informacaoComplementares)
        {
            OrgaosPublicos = new BoxGridVM<OrgaosPublicosFichaCadastralVM>() { DadosRetornados = new List<OrgaosPublicosFichaCadastralVM> { dadosRobo } };
            DadosEnderecos = new BoxGridVM<DadosEnderecosFichaCadastralVM>() { DadosRetornados = enderecos };
            DadosContatos = new BoxGridVM<DadosContatosFichaCadastralVM>() { DadosRetornados = contatos };
            DadosUnspsc = new BoxGridVM<UnspscFichaCadastralVM>() { DadosRetornados = unspsc };
            DadosInformacaoComplementares = new BoxGridVM<InformacaoComplementaresFichaCadastralVM>() { DadosRetornados = informacaoComplementares };
        }

        public FichaCadastralModel(List<OrgaosPublicosFichaCadastralVM> dadosRobo,
        List<DadosEnderecosFichaCadastralVM> enderecos, List<DadosContatosFichaCadastralVM> contatos,
        List<UnspscFichaCadastralVM> unspsc, List<InformacaoComplementaresFichaCadastralVM> informacaoComplementares)
        {
            OrgaosPublicos = new BoxGridVM<OrgaosPublicosFichaCadastralVM>() { DadosRetornados = dadosRobo };
            DadosEnderecos = new BoxGridVM<DadosEnderecosFichaCadastralVM>() { DadosRetornados = enderecos };
            DadosContatos = new BoxGridVM<DadosContatosFichaCadastralVM>() { DadosRetornados = contatos };
            DadosUnspsc = new BoxGridVM<UnspscFichaCadastralVM>() { DadosRetornados = unspsc };
            DadosInformacaoComplementares = new BoxGridVM<InformacaoComplementaresFichaCadastralVM>() { DadosRetornados = informacaoComplementares };
        }

        [DisplayName("Solicitação")]
        public int IdSolicitacao { get; set; }
        [DisplayName("Fornecedor")]
        public int IdPjPf { get; set; }
        [DisplayName("Contratante")]
        public int IdContratante { get; set; }
        [DisplayName("ContratanteFornecedor")]
        public int IdContratanteFornecedor { get; set; }
        [DisplayName("Tipo Fornecedor")]
        public int TipoFornecedor { get; set; }
        [DisplayName("Fornecedor Base")]
        public int IdPjPfBase { get; set; }

        //-- Orgãos Públicos
        public BoxGridVM<OrgaosPublicosFichaCadastralVM> OrgaosPublicos { get; set; }
        //-- Dados de Endereços
        public BoxGridVM<DadosEnderecosFichaCadastralVM> DadosEnderecos { get; set; }
        //-- Dados de Contatos
        public BoxGridVM<DadosContatosFichaCadastralVM> DadosContatos { get; set; }
        //-- Servicos / Materiais / Equipamentos oferecidos ao Mercado
        public BoxGridVM<UnspscFichaCadastralVM> DadosUnspsc { get; set; }
        //-- Informações Complementares
        public BoxGridVM<InformacaoComplementaresFichaCadastralVM> DadosInformacaoComplementares { get; set; }
    }

    public class SolicitacaoFichaCadastralVM : FichaCadastralModel
    {
        public SolicitacaoFichaCadastralVM()
            :base()
        {
        }

        public SolicitacaoFichaCadastralVM(List<DadosGeraisFichaCadastralVM> gerais, List<OrgaosPublicosFichaCadastralVM> dadosRobo,
        List<DadosEnderecosFichaCadastralVM> enderecos, List<DadosBancariosFichaCadastralVM> bancarios, List<DadosContatosFichaCadastralVM> contatos,
        List<DocumentosFichaCadastralVM> documentos, List<UnspscFichaCadastralVM> unspsc, List<InformacaoComplementaresFichaCadastralVM> informacaoComplementares)
            :base(dadosRobo,enderecos,contatos,unspsc,informacaoComplementares)
        {
            DadosSolicitacao = new DadosSolicitacaoFichaCadastralVM();
            DadosGerais = new BoxGridVM<DadosGeraisFichaCadastralVM>() { DadosRetornados = gerais };
            DadosBancarios = new BoxGridVM<DadosBancariosFichaCadastralVM>() { DadosRetornados = bancarios };
            DadosDocumentos = new BoxGridVM<DocumentosFichaCadastralVM>() { DadosRetornados = documentos };
        }

        public List<DadosGeraisFichaCadastralVM> Gerais { get; private set; }
        public List<OrgaosPublicosFichaCadastralVM> DadosRobo { get; private set; }
        public List<DadosEnderecosFichaCadastralVM> Enderecos { get; private set; }
        public List<DadosBancariosFichaCadastralVM> Bancarios { get; private set; }
        public List<DadosContatosFichaCadastralVM> Contatos { get; private set; }
        public List<DocumentosFichaCadastralVM> Documentos { get; private set; }
        public List<UnspscFichaCadastralVM> Unspsc { get; private set; }
        public List<InformacaoComplementaresFichaCadastralVM> InformacaoComplementares { get; private set; }

        public EnumTiposFluxo TipoFluxo { get; set; }
        //-- Dados Solicitacao
        public DadosSolicitacaoFichaCadastralVM DadosSolicitacao { get; set; }
        //-- Dados Gerais
        public BoxGridVM<DadosGeraisFichaCadastralVM> DadosGerais { get; set; }
        //-- Dados Bancários
        public BoxGridVM<DadosBancariosFichaCadastralVM> DadosBancarios { get; set; }
        //-- Documentos
        public BoxGridVM<DocumentosFichaCadastralVM> DadosDocumentos { get; set; }
        public int Id { get; internal set; }
    }

    public class DadosSolicitacaoFichaCadastralVM : FichaCadastralModel
    {
        [DisplayName("Tipo Solicitação")]
        public string TipoSolicitacao { get; set; }

        [DisplayName("Data Solicitação")]
        public string DataSolicitacao { get; set; }

        [DisplayName("Solicitante")]
        public string Solicitante { get; set; }

        [DisplayName("Receita Federal")]
        public string StatusReceitaFederal { get; set; }

        [DisplayName("Sintegra")]
        public string StatusSintegra { get; set; }

        [DisplayName("Simples Nacional")]
        public string StatusSimplesNacional { get; set; }

        [DisplayName("Prazo de entrega")]
        public string PrazoEntrega { get; set; }

        [DisplayName("Observação")]
        public string Observacao { get; set; }

        public List<TabelaSituacaoSolicitacao> SituacaoSolicitacaoList { get; set; }
        public int Id { get; internal set; }
    }

    public class TabelaSituacaoSolicitacao
    {
        [DisplayName("Situação")]
        public string Situacao { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Data")]
        public string Data { get; set; }
    }

    public class DadosGeraisFichaCadastralVM : FichaCadastralModel
    {
        [DisplayName("Categoria(Grupo Contas)")]
        public string CategoriaGrupoContas { get; set; }

        [DisplayName("CNPJ")]
        public string CNPJ { get; set; }

        [DisplayName("Razão Social")]
        public string RazaoSocial { get; set; }

        [DisplayName("Nome Fantasia")]
        public string NomeFantasia { get; set; }

        [DisplayName("CNAE")]
        public string CNAE { get; set; }

        [DisplayName("Inscrição Estadual")]
        public string InscricaoEstadual { get; set; }

        [DisplayName("Inscrição Municipal")]
        public string InscricaoMunicipal { get; set; }
        public int Id { get; internal set; }
    }

    public class OrgaosPublicosFichaCadastralVM : FichaCadastralModel
    {
        public ReceitaFederalFichaCadastralVM ReceitaFederal { get; set; }
        public SintegraFichaCadastralVM Sintegra { get; set; }
        public SimplesNacionalFichaCadastralVM SimplesNacional { get; set; }
        public int Id { get; internal set; }
    }

    public class DadosEnderecosFichaCadastralVM : FichaCadastralModel
    {
        [DisplayName("Tipo De Endereço")]
        public string TipoEndereco { get; set; }

        [DisplayName("Endereço")]
        public string Endereco { get; set; }

        [DisplayName("Número")]
        public string Numero { get; set; }

        [DisplayName("Complemento")]
        public string Complemento { get; set; }

        [DisplayName("CEP")]
        public string CEP { get; set; }

        [DisplayName("Bairro")]
        public string Bairro { get; set; }

        [DisplayName("Cidade")]
        public string Cidade { get; set; }

        [DisplayName("Estado")]
        public string Estado { get; set; }

        [DisplayName("País")]
        public string Pais { get; set; }
        public int Id { get; internal set; }
    }

    public class DadosBancariosFichaCadastralVM : FichaCadastralModel
    {
        [DisplayName("Banco")]
        public string Banco { get; set; }

        [DisplayName("Agência")]
        public string Agencia { get; set; }

        [DisplayName("Conta Corrente")]
        public string ContaCorrente { get; set; }

        [DisplayName("Arquivo")]
        public string Arquivo { get; set; }

        public string LinkArquivo { get; set; }
    }

    public class DadosContatosFichaCadastralVM : FichaCadastralModel
    {
        [DisplayName("Nome Contato")]
        public string Nome { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [DisplayName("Celular")]
        public string Celular { get; set; }

        public int Id { get; internal set; }
    }

    public class DocumentosFichaCadastralVM : FichaCadastralModel
    {
        [DisplayName("Documento")]
        public string Documento { get; set; }

        [DisplayName("Validade / Período")]
        public string ValidadePeriodo { get; set; }

        public string LinkDocumento { get; set; }
    }

    public class UnspscFichaCadastralVM : FichaCadastralModel
    {
        public UnspscFichaCadastralVM()
        {
            Materiais = new List<UnspscMaterialFichaCadastralVM>();
            Servicos = new List<UnspscServicoFichaCadastralVM>();
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public List<UnspscMaterialFichaCadastralVM> Materiais { get; set; }
        public List<UnspscServicoFichaCadastralVM> Servicos { get; set; }
    }

    public class UnspscServicoFichaCadastralVM
    {
        public int IdUnspsc { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }
    }

    public class UnspscMaterialFichaCadastralVM
    {
        public int IdUnspsc { get; set; }

        [DisplayName("Nome")]
        public string Nome { get; set; }
    }

    public class InformacaoComplementaresFichaCadastralVM : FichaCadastralModel
    {

    }

    public class BoxGridVM<TEntity> where TEntity : class
    {
        public BoxGridVM()
        {
            Escrita = true;
            Leitura = true;
            DadosRetornados = new List<TEntity>();
        }
        public string Cabecalho { get; set; }
        public bool Escrita { get; set; }
        public bool Leitura { get; set; }
        public List<TEntity> DadosRetornados { get; set; }
    }

    /*


    public class ReceitaFichaCadastralVM
    {
        public int Id { get; set; }
    }




    public class CategoriaFornecedorVM
    {
    }
    public class RoboVM
    {
    }
    public class SolicitacaoVM
    {
    }
    public class SolicitacaoCadastroPjPfVM
    {
        public int ID { get; set; }
        public int SOLICITACAO_ID { get; set; }
        public int CATEGORIA_ID { get; set; }
        public int ORG_COMPRAS_ID { get; set; }
        public int PJPF_TIPO { get; set; }
        public string CPF { get; set; }
        public string CNPJ { get; set; }
        public string RAZAO_SOCIAL { get; set; }
        public string NOME { get; set; }
        public string NOME_FANTASIA { get; set; }
        public string CNAE { get; set; }
        public string INSCR_ESTADUAL { get; set; }
        public string INSCR_MUNICIPAL { get; set; }
        public string TP_LOGRADOURO { get; set; }
        public string ENDERECO { get; set; }
        public string NUMERO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string CEP { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string UF { get; set; }
        public string PAIS { get; set; }
        public string OBSERVACAO { get; set; }
        public bool EXPANSAO { get; set; }
        public Nullable<int> EXPANSAO_PARA_CONTR_ID { get; set; }
        public string COD_PJPF_ERP { get; set; }
        public Nullable<int> ROBO_ID { get; set; }
        public string CLIENTE { get; set; }
        public string GRUPO_EMPRESA { get; set; }
        public Nullable<System.DateTime> DT_NASCIMENTO { get; set; }
        public virtual CategoriaFornecedorVM WFD_PJPF_CATEGORIA { get; set; }
        public virtual RoboVM ROBO { get; set; }
        public virtual SolicitacaoVM WFD_SOLICITACAO { get; set; }
    }

    public abstract class SolicitacaoFichaCadastral : SolicitacaoFichaCadastralVM
    {
        public abstract bool Tipo(EnumTiposFluxo fluxo);
        public abstract SolicitacaoFichaCadastralVM PopularModeloEdicao();
        public abstract DadosGeraisFichaCadastralVM PopularDadosGerais(DateTime dataEntrega);
    }

    public class CadastroFornecedorPFNacionalVM : SolicitacaoFichaCadastral
    {
        public string CPF { get; set; }
        public override SolicitacaoFichaCadastralVM PopularModeloEdicao()
        {
            SolicitacaoFichaCadastralVM modelo = new SolicitacaoFichaCadastralVM();
            modelo.DadosGerais = PopularDadosGerais(DateTime.Now);
            return modelo;
        }
        public override DadosGeraisFichaCadastralVM PopularDadosGerais(DateTime dataEntrega)
        {
            return new DadosGeraisFichaCadastralVM() { Id = IdSolicitacao, Documento = CPF };
        }
        public override bool Tipo(EnumTiposFluxo fluxo)
        {
            return fluxo == EnumTiposFluxo.CadastroFornecedorNacional;
        }
    }
    public class CadastroFornecedorPFEstrangeira : SolicitacaoFichaCadastral
    {
        public string CPF { get; set; }

        public override DadosGeraisFichaCadastralVM PopularDadosGerais(DateTime dataEntrega)
        {
            throw new NotImplementedException();
        }

        public override SolicitacaoFichaCadastralVM PopularModeloEdicao()
        {
            throw new NotImplementedException();
        }

        public override bool Tipo(EnumTiposFluxo fluxo)
        {
            throw new NotImplementedException();
        }
    }
    public class CadastroFornecedorPJNacional : SolicitacaoFichaCadastral
    {
        public override DadosGeraisFichaCadastralVM PopularDadosGerais(DateTime dataEntrega)
        {
            throw new NotImplementedException();
        }

        public override SolicitacaoFichaCadastralVM PopularModeloEdicao()
        {
            throw new NotImplementedException();
        }

        public override bool Tipo(EnumTiposFluxo fluxo)
        {
            throw new NotImplementedException();
        }
    }
    public class CadastroFornecedorPJEstrangeira : SolicitacaoFichaCadastral
    {
        public override DadosGeraisFichaCadastralVM PopularDadosGerais(DateTime dataEntrega)
        {
            throw new NotImplementedException();
        }

        public override SolicitacaoFichaCadastralVM PopularModeloEdicao()
        {
            throw new NotImplementedException();
        }

        public override bool Tipo(EnumTiposFluxo fluxo)
        {
            throw new NotImplementedException();
        }
    }
    */
}