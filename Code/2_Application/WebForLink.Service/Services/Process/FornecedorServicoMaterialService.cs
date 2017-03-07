using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class FornecedorServicoMaterialWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorServicoMaterialWebForLinkAppService
    {
        private readonly IFornecedorWebForLinkService _fornecedorService;
        private readonly IFornecedorUnspscWebForLinkService _fornecedorServiceUnspsc;

        public FornecedorServicoMaterialWebForLinkAppService(
            IFornecedorWebForLinkService fornecedorService,
            IFornecedorUnspscWebForLinkService fornecedorServiceUnspsc)
        {
            _fornecedorService = fornecedorService;
            _fornecedorServiceUnspsc = fornecedorServiceUnspsc;
            try
            {
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
        public FORNECEDOR_UNSPSC BuscarPorId(int id)
        {
            try
            {
                return _fornecedorServiceUnspsc.Get(id);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um fornecedor não identificado por ID", ex);
            }
        }

        public List<FORNECEDOR_UNSPSC> BuscarPorFornecedorId(int idPjpf)
        {
            try
            {
                return _fornecedorServiceUnspsc.Find(x => x.PJPF_ID == idPjpf).ToList();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a lista Unspsc", ex);
            }
        }

        public void GravaUnspscNoPjPf(List<FORNECEDOR_UNSPSC> unspscs, int fornecedorId, int contratanteId,
            int usuarioId)
        {
            try
            {
                var vUnspscIds = unspscs.Select(x => x.UNSPSC_ID).ToArray();
                var unspscsOri =
                    _fornecedorServiceUnspsc.Find(x => x.PJPF_ID == fornecedorId && x.DT_EXCLUSAO == null).ToList();
                var agora = DateTime.Now;

                foreach (var item in unspscsOri)
                {
                    if (!vUnspscIds.Contains(item.UNSPSC_ID))
                    {
                        item.DT_EXCLUSAO = agora;
                        _fornecedorServiceUnspsc.Update(item);
                    }
                }

                foreach (var item in unspscs)
                {
                    if (!unspscsOri.Any(x => x.UNSPSC_ID == item.UNSPSC_ID))
                    {
                        item.DT_INCLUSAO = agora;
                        _fornecedorServiceUnspsc.Update(item);
                    }
                }

                var pjpf = _fornecedorService.Get(fornecedorId);
                pjpf.DT_ATUALIZACAO_UNSPSC = DateTime.Now;

                _fornecedorService.Update(pjpf);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao Gravar a lista Unspsc", ex);
            }
        }
        
        public void Dispose()
        {
        }

        public FORNECEDOR_UNSPSC Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_UNSPSC Get(string id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_UNSPSC GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_UNSPSC> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_UNSPSC> Find(Expression<Func<FORNECEDOR_UNSPSC, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Create(FORNECEDOR_UNSPSC entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(FORNECEDOR_UNSPSC entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Remove(FORNECEDOR_UNSPSC entity)
        {
            throw new NotImplementedException();
        }


        public FORNECEDOR_UNSPSC Get(int id)
        {
            throw new NotImplementedException();
        }

        public FORNECEDOR_UNSPSC Get(Expression<Func<FORNECEDOR_UNSPSC, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_UNSPSC> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FORNECEDOR_UNSPSC> Find(Expression<Func<FORNECEDOR_UNSPSC, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}