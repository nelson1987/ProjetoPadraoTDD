using AutoMapper;
using System.Collections.Generic;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Web.ViewModels
{
    public class FornecedorUnspscVM
    {
        public static List<FornecedorUnspscVM> ModelToViewModel(List<FORNECEDOR_UNSPSC> modelList)
        {
            return Mapper.Map<List<FORNECEDOR_UNSPSC>, List<FornecedorUnspscVM>>(modelList);
        }

        public static List<FORNECEDOR_UNSPSC> ViewModelToModel(List<FornecedorUnspscVM> modelList)
        {
            return Mapper.Map<List<FornecedorUnspscVM>, List<FORNECEDOR_UNSPSC>>(modelList);
        }

        //private readonly ServicosMateriaisWebForLinkAppService _unspscBP;
        ////public FornecedorUnspscVM(IServicosMateriaisWebForLinkAppService servicosMateriais)
        ////{
        ////    _unspscBP = servicosMateriais;
        ////}
        //public FornecedorUnspscVM()
        //{
        //    _unspscBP = new ServicosMateriaisWebForLinkAppService();
        //}

        public int ID { get; set; }
        public int? SolicitacaoId { get; set; }
        public int? FornecedorId { get; set; }
        public int UsnpscId { get; set; }
        public int UsnpscCodigo { get; set; }
        public string UsnpscDescricao { get; set; }
        public int Niv { get; set; }

        public List<FornecedorUnspscVM> PreencheModelUnspsc(int FornecedorId, int? idSolicitacao, List<TIPO_UNSPSC> unspscs)
        {
            //string[] servicos = ServicosSelecionados.Split(new Char[] { '|' });
            //string[] materiais = MateriaisSelecionados.Split(new Char[] { '|' });

            //var unspscs = _unspscBP.BuscarListaPorID(servicos, materiais);

            var listaUnspsc = new List<FornecedorUnspscVM>();

            unspscs.ForEach(x => listaUnspsc.Add(new FornecedorUnspscVM
            {
                Niv = (int)x.NIV,
                SolicitacaoId = idSolicitacao,
                UsnpscCodigo = x.UNSPSC_COD,
                UsnpscDescricao = x.UNSPSC_DSC,
                UsnpscId = x.ID,
                FornecedorId = FornecedorId
            }));

            return listaUnspsc;
        }
    }
}