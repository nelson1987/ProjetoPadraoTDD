using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public interface IFornecedorEnderecoWebForLinkAppService
    {
        FORNECEDOR_ENDERECO BuscarPorID(int id);
        List<FORNECEDOR_ENDERECO> BuscarPorContratantePJPFId(int id);
        void AlterarFornecedorEndereco(List<FORNECEDOR_ENDERECO> enderecos, int contratantePjPfId);
    }

    public class FornecedorEnderecoWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorEnderecoWebForLinkAppService
    {
        private readonly IFornecedorEnderecoWebForLinkService _fornecedorEnderecoService;

        public FornecedorEnderecoWebForLinkAppService(FornecedorEnderecoWebForLinkService fornecedorEnderecoService)
        {
            try
            {
                _fornecedorEnderecoService = fornecedorEnderecoService;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public FORNECEDOR_ENDERECO BuscarPorID(int id)
        {
            try
            {
                return _fornecedorEnderecoService.Get(id);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar os dados de endereço.", e);
            }
        }

        public List<FORNECEDOR_ENDERECO> BuscarPorContratantePJPFId(int id)
        {
            try
            {
                return _fornecedorEnderecoService.Find(x => x.CONTRATANTE_PJPF_ID == id).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar os dados de endereço.", e);
            }
        }

        public void AlterarFornecedorEndereco(List<FORNECEDOR_ENDERECO> enderecos, int contratantePjPfId)
        {
            BeginTransaction();
            enderecos = enderecos.Select(c => { c.TP_ENDERECO_ID = 1; return c; }).ToList();
            var ids = enderecos.Select(x => x.ID).ToArray();
            _fornecedorEnderecoService.Delete(
                _fornecedorEnderecoService.Find(x => ids.Contains(x.ID) && x.CONTRATANTE_PJPF_ID == contratantePjPfId)
                    .ToList());

            foreach (var item in enderecos)
            {
                if (item.ID == 0)
                {
                    item.CONTRATANTE_PJPF_ID = contratantePjPfId;
                    item.TP_ENDERECO_ID = 1;
                    _fornecedorEnderecoService.Add(item);
                }
                else
                {
                    FORNECEDOR_ENDERECO endereco = _fornecedorEnderecoService.Get(item.ID);
                    endereco.TP_ENDERECO_ID = item.TP_ENDERECO_ID;
                    endereco.ENDERECO = item.ENDERECO;
                    endereco.NUMERO = item.NUMERO;
                    endereco.COMPLEMENTO = item.COMPLEMENTO;
                    endereco.CEP = item.CEP;
                    endereco.BAIRRO = item.BAIRRO;
                    endereco.CIDADE = item.CIDADE;
                    endereco.UF = item.UF;
                    endereco.PAIS = item.PAIS;
                    endereco.CONTRATANTE_PJPF_ID = item.CONTRATANTE_PJPF_ID;
                    _fornecedorEnderecoService.Update(item);
                }
            }
            Commit();
        }

        public void Dispose()
        {
        }
    }
}