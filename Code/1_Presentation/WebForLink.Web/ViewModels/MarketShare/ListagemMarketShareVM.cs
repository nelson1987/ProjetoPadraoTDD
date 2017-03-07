using System;
using System.Collections.Generic;
using System.Linq;

namespace WebForLink.Web.ViewModels.MarketShare
{
    //public class ComparadorMarketShareVM<TEntity> where TEntity : class
    //{
    //    private List<TEntity> Comparadores { get; set; }
    //    private TEntity Comparado { get; set; }
    //    private Func<TEntity, bool> Filtro { get; set; }
    //    private List<Func<TEntity, bool>> Filtros { get; set; }

    //    public int Material { get; private set; }

    //    public ComparadorMarketShareVM(TEntity comparado)
    //    {
    //        Comparado = comparado;
    //        //Fornecedor.FatiaDeMercado = 0;
    //        Comparar();
    //    }

    //    public ComparadorMarketShareVM(TEntity comparado, List<TEntity> comparadores, Func<TEntity, bool> filtro = null, List<Func<TEntity, bool>> filtros = null)
    //    {
    //        Comparadores = comparadores;
    //        Comparado = comparado;
    //        ListagemRetorno = new List<TEntity>();
    //        Filtro = filtro;
    //        Filtros = filtros;
    //    }

    //    public List<TEntity> ListagemRetorno { get; private set; }

    //    public void Comparar()
    //    {
    //        //double fatia = 1;
    //        //double fatiaDeClientes = fatia / Fornecedor.Clientes.Count;
    //        //foreach (var item in Fornecedor.Clientes)
    //        //{
    //        //    if (item.TotalFornecedores == 0)
    //        //        item.TotalFornecedores = 1;
    //        //    Fornecedor.FatiaDeMercado += fatiaDeClientes / item.TotalFornecedores;
    //        //}
    //    }
    //    public void ListarMarketShare()
    //    {
    //        var fornecedoresRetorno = Comparadores
    //            .Where(Filtro)
    //            .ToList();

    //        fornecedoresRetorno.Insert(0, Comparado);

    //        int quantidadeTotalMaterial = fornecedoresRetorno
    //            .Cast<FornecedorMarketShareVM>()
    //            .Select(x=>x.Materiais)
    //            .Count();
    //        Material = quantidadeTotalMaterial;

    //        foreach (var item in fornecedoresRetorno.Cast<FornecedorMarketShareVM>())
    //        {
    //            Material = item.Materiais.Count;
    //            //ValidarMarketShare(item);
    //            item.
    //            ListagemRetorno.Add(item);
    //        }
    //    }

    //    private void ValidarMarketShare(TEntity item)
    //    {

    //        //throw new NotImplementedException();

    //    }
    //}
}