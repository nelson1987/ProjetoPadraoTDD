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
    public interface IFornecedorServicoMaterialWebForLinkService : IService<FORNECEDOR_UNSPSC>
    {
        FORNECEDOR_UNSPSC BuscarPorID(int id);
        List<FORNECEDOR_UNSPSC> BuscarPorPJPFId(int idPJPF);
        void GravaUnspscNoPjPf(List<FORNECEDOR_UNSPSC> unspscs, int fornecedorId, int contratanteId, int usuarioId);
    }

    public class FornecedorServicoMaterialWebForLinkService : Service<FORNECEDOR_UNSPSC>,
        IFornecedorServicoMaterialWebForLinkService
    {
        private readonly IFornecedorWebForLinkRepository _fornecedorRepository;
        private readonly IFornecedorUnspscWebForLinkRepository _fornecedorRepositoryUnspsc;

        public FornecedorServicoMaterialWebForLinkService(IFornecedorWebForLinkRepository fornecedorRepository,
            IFornecedorUnspscWebForLinkRepository fornecedorRepositoryUnspsc) : base(fornecedorRepositoryUnspsc)
        {
            try
            {
                _fornecedorRepository = fornecedorRepository;
                _fornecedorRepositoryUnspsc = fornecedorRepositoryUnspsc;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FORNECEDOR_UNSPSC BuscarPorID(int id)
        {
            try
            {
                return _fornecedorRepositoryUnspsc.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um fornecedor não identificado por ID", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<FORNECEDOR_UNSPSC> BuscarPorPJPFId(int idPJPF)
        {
            try
            {
                return _fornecedorRepositoryUnspsc.Find(x => x.PJPF_ID == idPJPF).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a lista Unspsc", ex);
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void GravaUnspscNoPjPf(List<FORNECEDOR_UNSPSC> unspscs, int fornecedorId, int contratanteId,
            int usuarioId)
        {
            try
            {
                var vUnspscIds = unspscs.Select(x => x.UNSPSC_ID).ToArray();
                var unspscsOri =
                    _fornecedorRepositoryUnspsc.Find(x => x.PJPF_ID == fornecedorId && x.DT_EXCLUSAO == null).ToList();
                var agora = DateTime.Now;

                foreach (var item in unspscsOri)
                {
                    if (!vUnspscIds.Contains(item.UNSPSC_ID))
                    {
                        item.DT_EXCLUSAO = agora;
                        _fornecedorRepositoryUnspsc.Update(item);
                    }
                }

                foreach (var item in unspscs)
                {
                    if (!unspscsOri.Any(x => x.UNSPSC_ID == item.UNSPSC_ID))
                    {
                        item.DT_INCLUSAO = agora;
                        _fornecedorRepositoryUnspsc.Update(item);
                    }
                }

                var pjpf = _fornecedorRepository.Get(fornecedorId);
                pjpf.DT_ATUALIZACAO_UNSPSC = DateTime.Now;

                _fornecedorRepository.Update(pjpf);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao Gravar a lista Unspsc", ex);
            }
        }

        public void Dispose()
        {
        }
    }
}