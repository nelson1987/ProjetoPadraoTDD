using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Service;
using WebForLink.Domain.Interfaces.Service.Common;
using WebForLink.Domain.Validation;

namespace WebForLink.Application.Services.Process
{
    public class ContratanteFornecedorWebForLinkAppService : AppService<WebForLinkContexto>,
        IContratanteFornecedorWebForLinkAppService
    {
        private readonly IContratantePjpfWebForLinkService _contratanteFornecedor;

        public ContratanteFornecedorWebForLinkAppService(IContratantePjpfWebForLinkService contratanteFornecedor)
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
                return _contratanteFornecedor.Get(x => x.PJPF_ID == id);
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

        public RetornoPesquisa<WFD_CONTRATANTE_PJPF> BuscarPesquisa(
            Expression<Func<WFD_CONTRATANTE_PJPF, bool>> filtros, int tamanhoPagina, int pagina,
            Func<WFD_CONTRATANTE_PJPF, IComparable> ordenacao)
        {
            throw new NotImplementedException();
        }

        public WFD_CONTRATANTE_PJPF Get(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public WFD_CONTRATANTE_PJPF Get(Expression<Func<WFD_CONTRATANTE_PJPF, bool>> predicate, bool @readonly = false)
        {
            return _contratanteFornecedor.Get(predicate);
        }

        public WFD_CONTRATANTE_PJPF GetAllReferences(int id, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WFD_CONTRATANTE_PJPF> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WFD_CONTRATANTE_PJPF> Find(Expression<Func<WFD_CONTRATANTE_PJPF, bool>> predicate,
            bool @readonly = false)
        {
            return _contratanteFornecedor.Find(predicate);
        }

        public ValidationResult Add(WFD_CONTRATANTE_PJPF department)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Add(List<WFD_CONTRATANTE_PJPF> entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Update(WFD_CONTRATANTE_PJPF department)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Delete(WFD_CONTRATANTE_PJPF entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Delete(List<WFD_CONTRATANTE_PJPF> entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        List<ValidationResult> IService<WFD_CONTRATANTE_PJPF>.Add(List<WFD_CONTRATANTE_PJPF> entity)
        {
            throw new NotImplementedException();
        }

        List<ValidationResult> IService<WFD_CONTRATANTE_PJPF>.Delete(List<WFD_CONTRATANTE_PJPF> entity)
        {
            throw new NotImplementedException();
        }

        public ValidationResult Modificar(WFD_CONTRATANTE_PJPF entity)
        {
            throw new NotImplementedException();
        }
        
        public WFD_CONTRATANTE_PJPF BuscaFichaCadastralPagante(int contratanteId)
        {
            try
            {
                return _contratanteFornecedor.BuscaFichaCadastralPagante(contratanteId);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o fornecedor por razão social", ex);
            }
        }
    }
}