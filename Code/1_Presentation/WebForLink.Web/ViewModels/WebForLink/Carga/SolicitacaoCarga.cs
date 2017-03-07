using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WebForLink.Web.ViewModels.Carga
{
    //public class CargaTeste
    //{
    //}
    #region ClassesTeste
    [Serializable]
    [XmlRoot("Carga")]
    public class SolicitacaoCarga
    {
        [XmlArrayItem("Solicitacao")]
        public List<SolicitacoesCarga> Solicitacoes { get; set; }
        [XmlArrayItem("Mensagem")]
        public List<MensagensCarga> Mensagens { get; set; }

    }
    [Serializable]
    public class MensagensCarga
    {
        [XmlElement(ElementName = "CodigoSolicitacao")]
        public int SolicitacaoId { get; set; }

        public int TipoAcao { get; set; }

        [XmlElement(ElementName = "CodigoSap")]
        public int CodigoERP { get; set; }

        [XmlElement(ElementName = "Empresa")]
        public int ContratanteId { get; set; }

        public int CodigoRetorno { get; set; }

        public string TextoRetorno { get; set; }
    }
    [Serializable]
    public class SolicitacoesCarga
    {
        [XmlElement(ElementName = "CodigoSolicitacao")]
        public int Id { get; set; }

        public int TipoAcao { get; set; }

        [XmlElement(ElementName = "CodigoFornecedorERP")]
        public int CodigoSap { get; set; }

        [XmlElement(ElementName = "Empresa")]
        public int ContratanteId { get; set; }

        [XmlElement(ElementName = "GrupoContas")]
        public int GrupoContas { get; set; }

        [XmlElement(ElementName = "OrganizacaoCompras")]
        public int OrganizacaoCompras { get; set; }

        //[XmlRootAttribute("Fornecedor")]
        //[XmlElement(ElementName = "Fornecedor")]
        //[XmlArrayItem("Fornecedor")]
        //public FornecedorCargaModel Fornecedor { get; set; }

        [XmlArrayItem("Contato")]
        public List<DadosContatoCargaModel> Contatos { get; set; }

        [XmlArrayItem("Banco")]
        public List<DadosBancariosCargaModel> Bancos { get; set; }

        //[XmlArrayItem("Bloqueio")]
        public BloqueioCargaModel Bloqueio { get; set; }

        //[XmlArrayItem("Desbloqueio")]
        public DesbloqueioCargaModel Desbloqueio { get; set; }


        //public object Enderecos { get; set; }
    }
    #endregion
}