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
    public interface IContratanteFornecedorWebForLinkService : IService<WFD_CONTRATANTE_PJPF>
    {
        List<WFD_CONTRATANTE_PJPF> buscarPorCnpj(string documento);
        WFD_CONTRATANTE_PJPF BuscarPorID(int id);
        WFD_CONTRATANTE_PJPF BuscarPorPjPfId(int id);
        List<WFD_CONTRATANTE_PJPF> ListarPorPjPfId(int id);
        WFD_CONTRATANTE_PJPF BuscarPjpfPorContratanteComEndereco(int contratantePjpfId);
    }

    public class ContratanteFornecedorWebForLinkService : Service<WFD_CONTRATANTE_PJPF>,
        IContratanteFornecedorWebForLinkService
    {
        private readonly IContratanteFornecedorWebForLinkRepository _contratanteFornecedor;

        public ContratanteFornecedorWebForLinkService(IContratanteFornecedorWebForLinkRepository contratanteFornecedor)
            : base(contratanteFornecedor)
        {
            try
            {
                _contratanteFornecedor = contratanteFornecedor;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public List<WFD_CONTRATANTE_PJPF> buscarPorCnpj(string documento)
        {
            try
            {
                return _contratanteFornecedor.All()
                    .Where(x =>
                        x.TP_PJPF == 2
                        && (x.WFD_PJPF.CNPJ == documento || x.WFD_PJPF.CPF == documento))
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public WFD_CONTRATANTE_PJPF BuscarPorID(int id)
        {
            try
            {
                return _contratanteFornecedor.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public WFD_CONTRATANTE_PJPF BuscarPorPjPfId(int id)
        {
            try
            {
                return _contratanteFornecedor.Find(x => x.PJPF_ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public List<WFD_CONTRATANTE_PJPF> ListarPorPjPfId(int id)
        {
            try
            {
                return _contratanteFornecedor.Find(x => x.PJPF_ID == id).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um		por ID", ex);
            }
        }

        public WFD_CONTRATANTE_PJPF BuscarPjpfPorContratanteComEndereco(int contratantePjpfId)
        {
            try
            {
                return _contratanteFornecedor.Get(contratantePjpfId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o Fonrcedor por Contratante", ex);
            }
        }

        //public RetornoPesquisa<WFD_CONTRATANTE_PJPF> BuscarPesquisa(
        //    Expression<Func<WFD_CONTRATANTE_PJPF, bool>> filtros, int tamanhoPagina, int pagina,
        //    Func<WFD_CONTRATANTE_PJPF, IComparable> ordenacao)
        //{
        //    try
        //    {
        //        return _contratanteFornecedor.BuscarPesquisaCostumizada(filtros, tamanhoPagina, pagina, ordenacao);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ServiceWebForLinkException("Erro ao buscar um destinatário por Id", ex);
        //    }
        //}

        public void Dispose()
        {
        }
    }
}