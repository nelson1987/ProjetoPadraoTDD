using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IServicosMateriaisWebForLinkService : IService<FORNECEDOR_UNSPSC>
    {
        void ManterMeusMateriaisServicos(List<FORNECEDOR_UNSPSC> unspsc, int pjpfId);
        TIPO_UNSPSC BuscarPorID(int id);
        List<TIPO_UNSPSC> BuscarListaPorID(int[] ids);

        List<TIPO_UNSPSC> BuscarServicoPorDescricao(string descricao, List<TIPO_UNSPSC> grupo1, List<TIPO_UNSPSC> grupo2,
            List<TIPO_UNSPSC> grupo3);

        List<TIPO_UNSPSC> BuscarServicoGrupo1();
        List<TIPO_UNSPSC> BuscarServicoGrupo2();
        List<TIPO_UNSPSC> BuscarServicoGrupo3();
        List<TIPO_UNSPSC> BuscarServico(int codigo, int niv);

        List<TIPO_UNSPSC> BuscarMaterialPorDescricao(string descricao, List<TIPO_UNSPSC> grupo1,
            List<TIPO_UNSPSC> grupo2, List<TIPO_UNSPSC> grupo3);

        List<TIPO_UNSPSC> BuscarMaterialGrupo1();
        List<TIPO_UNSPSC> BuscarMaterialGrupo2();
        List<TIPO_UNSPSC> BuscarMaterialGrupo3();
        List<TIPO_UNSPSC> BuscarMaterial(int codigo, int niv);
    }

    public class ServicosMateriaisWebForLinkService : Service<FORNECEDOR_UNSPSC>, IServicosMateriaisWebForLinkService
    {
        private readonly IFornecedorUnspscWebForLinkRepository _pjpfUnspsc;
        private readonly ITipoUnspscWebForLinkRepository _unspsc;
        private readonly int divisorServMat = 70000000;

        public ServicosMateriaisWebForLinkService(
            IFornecedorUnspscWebForLinkRepository pjpfUnspsc,
            ITipoUnspscWebForLinkRepository unspsc) : base(pjpfUnspsc)
        {
            try
            {
                _pjpfUnspsc = pjpfUnspsc;
                _unspsc = unspsc;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public void ManterMeusMateriaisServicos(List<FORNECEDOR_UNSPSC> unspsc, int pjpfId)
        {
            try
            {
                _pjpfUnspsc.Find(x => x.PJPF_ID == pjpfId).ToList().ForEach(x => { _pjpfUnspsc.Delete(x); });
                unspsc.ForEach(x => { _pjpfUnspsc.Add(x); });
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a manter os materiais de serviços", ex);
            }
        }

        public TIPO_UNSPSC BuscarPorID(int id)
        {
            try
            {
                return _unspsc.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o UNSPSC por ID", ex);
            }
        }

        public List<TIPO_UNSPSC> BuscarListaPorID(int[] ids)
        {
            try
            {
                return _unspsc.Find(x => ids.Contains(x.ID)).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a lista de UNSPSC por ID", ex);
            }
        }

        public void Dispose()
        {
        }

        #region SERVICOS

        //public List<TIPO_UNSPSC> BuscarServicoPorDescricao(string descricao, List<TIPO_UNSPSC> grupo1, List<TIPO_UNSPSC> grupo2, List<TIPO_UNSPSC> grupo3)
        //{
        //    try
        //    {
        //        string[] dsc = descricao.Split(' ');
        //        int grupoUNSPSC = 70000000;

        //        var predicate = PredicateBuilder.New<TIPO_UNSPSC>();
        //        predicate = predicate.And(d => d.UNSPSC_COD >= grupoUNSPSC);
        //        foreach (var item in dsc)
        //        {
        //            var ds = item.Trim();
        //            if (ds.Length > 0 && ds.Length < 3)
        //                predicate = predicate.And(d => d.UNSPSC_DSC.Contains(" " + ds + " "));
        //            else if (ds.Length >= 3)
        //                predicate = predicate.And(d => d.UNSPSC_DSC.Contains(ds));
        //        }
        //        var pesquisa = _unspsc.All(predicate, d => d.UNSPSC_COD).ToList();

        //        foreach (var item in pesquisa.ToArray())
        //        {
        //            if (item.NIV > 1)
        //            {
        //                var grp3 = grupo3.FirstOrDefault(x => x.UNSPSC_COD < item.UNSPSC_COD && x.NIV < item.NIV);
        //                if (grp3 != null && !pesquisa.Any(x => x.UNSPSC_COD == grp3.UNSPSC_COD))
        //                    pesquisa.Add(grp3);

        //                var grp2 = grupo2.FirstOrDefault(x => x.UNSPSC_COD < item.UNSPSC_COD && x.NIV < item.NIV);
        //                if (grp2 != null && !pesquisa.Any(x => x.UNSPSC_COD == grp2.UNSPSC_COD))
        //                    pesquisa.Add(grp2);

        //                var grp1 = grupo1.FirstOrDefault(x => x.UNSPSC_COD < item.UNSPSC_COD && x.NIV < item.NIV);
        //                if (grp1 != null && !pesquisa.Any(x => x.UNSPSC_COD == grp1.UNSPSC_COD))
        //                    pesquisa.Add(grp1);
        //            }
        //        }

        //        return pesquisa.OrderBy(x => x.UNSPSC_COD).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar um fornecedor não identificado por ID", ex);
        //    }
        //}

        public List<TIPO_UNSPSC> BuscarServicoGrupo1()
        {
            try
            {
                var grupo1 =
                    _unspsc.Find(x => x.UNSPSC_COD >= 70000000 && x.NIV == 1)
                        .OrderByDescending(y => y.UNSPSC_COD)
                        .ToList();
                return grupo1;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o Grupo 1 dos Serviços", ex);
            }
        }

        public List<TIPO_UNSPSC> BuscarServicoGrupo2()
        {
            try
            {
                var grupo2 =
                    _unspsc.Find(x => x.UNSPSC_COD >= 70000000 && x.NIV == 2)
                        .OrderByDescending(y => y.UNSPSC_COD)
                        .ToList();
                return grupo2;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o Grupo 2 dos Serviços", ex);
            }
        }

        public List<TIPO_UNSPSC> BuscarServicoGrupo3()
        {
            try
            {
                var grupo3 =
                    _unspsc.Find(x => x.UNSPSC_COD >= 70000000 && x.NIV == 3)
                        .OrderByDescending(y => y.UNSPSC_COD)
                        .ToList();
                return grupo3;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o Grupo 3 dos Serviços", ex);
            }
        }

        public List<TIPO_UNSPSC> BuscarServico(int codigo, int niv)
        {
            try
            {
                var nivFilho = niv + 1;
                var sCodigoFim = "";
                if (codigo == 0)
                {
                    codigo = divisorServMat;
                    sCodigoFim = Int32.MaxValue.ToString();
                }
                else
                {
                    sCodigoFim = codigo.ToString().Substring(0, (niv*2));
                    sCodigoFim = (Convert.ToInt32(sCodigoFim) + 1).ToString();
                    sCodigoFim = sCodigoFim.PadRight(8, '0');
                }

                var codigoFim = Convert.ToInt32(sCodigoFim);

                return _unspsc
                    .Find(u => u.UNSPSC_COD >= codigo && u.UNSPSC_COD < codigoFim && u.NIV == nivFilho)
                    .OrderBy(x => x.UNSPSC_COD)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a lista de serviços", ex);
            }
        }

        #endregion

        #region MATERIAIS

        //public List<TIPO_UNSPSC> BuscarMaterialPorDescricao(string descricao, List<TIPO_UNSPSC> grupo1, List<TIPO_UNSPSC> grupo2, List<TIPO_UNSPSC> grupo3)
        //{
        //    try
        //    {
        //        string[] dsc = descricao.Split(' ');
        //        int grupoUNSPSC = 70000000;

        //        var predicate = PredicateBuilder.New<TIPO_UNSPSC>();
        //        predicate = predicate.And(d => d.UNSPSC_COD < grupoUNSPSC);
        //        foreach (var item in dsc)
        //        {
        //            var ds = item.Trim();
        //            if (ds.Length > 0 && ds.Length < 3)
        //                predicate = predicate.And(d => d.UNSPSC_DSC.Contains(" " + ds + " "));
        //            else if (ds.Length >= 3)
        //                predicate = predicate.And(d => d.UNSPSC_DSC.Contains(ds));
        //        }
        //        var pesquisa = _unspsc.All(predicate, d => d.UNSPSC_COD).ToList();

        //        foreach (var item in pesquisa.ToArray())
        //        {
        //            if (item.NIV > 1)
        //            {
        //                var grp3 = grupo3.FirstOrDefault(x => x.UNSPSC_COD < item.UNSPSC_COD && x.NIV < item.NIV);
        //                if (grp3 != null && !pesquisa.Any(x => x.UNSPSC_COD == grp3.UNSPSC_COD))
        //                    pesquisa.Add(grp3);

        //                var grp2 = grupo2.FirstOrDefault(x => x.UNSPSC_COD < item.UNSPSC_COD && x.NIV < item.NIV);
        //                if (grp2 != null && !pesquisa.Any(x => x.UNSPSC_COD == grp2.UNSPSC_COD))
        //                    pesquisa.Add(grp2);

        //                var grp1 = grupo1.FirstOrDefault(x => x.UNSPSC_COD < item.UNSPSC_COD && x.NIV < item.NIV);
        //                if (grp1 != null && !pesquisa.Any(x => x.UNSPSC_COD == grp1.UNSPSC_COD))
        //                    pesquisa.Add(grp1);
        //            }
        //        }

        //        return pesquisa.OrderBy(x => x.UNSPSC_COD).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar um fornecedor não identificado por ID", ex);
        //    }
        //}

        public List<TIPO_UNSPSC> BuscarMaterialGrupo1()
        {
            try
            {
                var grupo1 =
                    _unspsc.Find(x => x.UNSPSC_COD < 70000000 && x.NIV == 1)
                        .OrderByDescending(y => y.UNSPSC_COD)
                        .ToList();
                return grupo1;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o Grupo 1 dos Serviços", ex);
            }
        }

        public List<TIPO_UNSPSC> BuscarMaterialGrupo2()
        {
            try
            {
                var grupo2 =
                    _unspsc.Find(x => x.UNSPSC_COD < 70000000 && x.NIV == 2)
                        .OrderByDescending(y => y.UNSPSC_COD)
                        .ToList();
                return grupo2;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o Grupo 2 dos Serviços", ex);
            }
        }

        public List<TIPO_UNSPSC> BuscarMaterialGrupo3()
        {
            try
            {
                var grupo3 =
                    _unspsc.Find(x => x.UNSPSC_COD < 70000000 && x.NIV == 3)
                        .OrderByDescending(y => y.UNSPSC_COD)
                        .ToList();
                return grupo3;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o Grupo 3 dos Serviços", ex);
            }
        }

        public List<TIPO_UNSPSC> BuscarMaterial(int codigo, int niv)
        {
            try
            {
                var nivFilho = niv + 1;
                var sCodigoFim = "";
                if (codigo == 0)
                {
                    codigo = 10000000;
                    sCodigoFim = Int32.MaxValue.ToString();
                }
                else
                {
                    sCodigoFim = codigo.ToString().Substring(0, (niv*2));
                    sCodigoFim = (Convert.ToInt32(sCodigoFim) + 1).ToString();
                    sCodigoFim = sCodigoFim.PadRight(8, '0');
                }

                var codigoFim = Convert.ToInt32(sCodigoFim);

                return _unspsc
                    .Find(
                        u =>
                            u.UNSPSC_COD >= codigo && u.UNSPSC_COD < divisorServMat && u.UNSPSC_COD < codigoFim &&
                            u.NIV == nivFilho)
                    .OrderBy(x => x.UNSPSC_COD)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a lista de serviços", ex);
            }
        }

        public List<TIPO_UNSPSC> BuscarServicoPorDescricao(string descricao, List<TIPO_UNSPSC> grupo1,
            List<TIPO_UNSPSC> grupo2, List<TIPO_UNSPSC> grupo3)
        {
            throw new NotImplementedException();
        }

        public List<TIPO_UNSPSC> BuscarMaterialPorDescricao(string descricao, List<TIPO_UNSPSC> grupo1,
            List<TIPO_UNSPSC> grupo2, List<TIPO_UNSPSC> grupo3)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}