using System.Collections.Generic;

namespace WebForLink.Web.ViewModels.MarketShare
{
    public class FornecedorMarketShareVM
    {
        public int Id { get; set; }
        public int Tipo { get; set; }
        public double FatiaDeMercado { get; set; }
        public List<EstadoMarketShareVM> Estados { get; set; }
        public List<ClienteMarketShareVM> Clientes { get; set; }
        public List<MaterialMarketShareVM> Materiais { get; set; }
        public List<ServicoMarketShareVM> Servicos { get; set; }

        public int TotalMateriais { get { return Materiais.Count; } }
        public int TotalEstados { get { return Estados.Count; } }
        public int TotalClientes { get { return Clientes.Count; } }
    }

    public class EstadoMarketShareVM
    {
        public string Nome { get; set; }
        public string UF { get; set; }
    }

    public class ClienteMarketShareVM
    {
        public string Nome { get; set; }
        public string Sigla { get; set; }
        public int TotalFornecedores { get; set; }
        public List<MaterialMarketShareVM> Materiais { get; set; }
    }

    public class MaterialMarketShareVM
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Fabricante { get; set; }
        public string CodigoMaterialCliente { get; set; }
    }

    public class ServicoMarketShareVM
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
    }
}