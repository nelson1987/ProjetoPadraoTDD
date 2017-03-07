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
    public interface IFornecedorBancoWebForLinkAppService
    {
        List<BancoDoFornecedor> BuscarPorContratantePJPFId(int id);
        void ManterMeusBancos(List<BancoDoFornecedor> bancos, int contratantePjPfId);
    }

    public class FornecedorBancoWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorBancoWebForLinkAppService
    {
        private readonly IFornecedorBancoWebForLinkService _bancoFornecedorService;

        public FornecedorBancoWebForLinkAppService(IFornecedorBancoWebForLinkService bancoFornecedor)
        {
            try
            {
                _bancoFornecedorService = bancoFornecedor;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public List<BancoDoFornecedor> BuscarPorContratantePJPFId(int id)
        {
            try
            {
                return _bancoFornecedorService.Find(x => x.CONTRATANTE_PJPF_ID == id).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar os dados bancários.", e);
            }
        }

        public void ManterMeusBancos(List<BancoDoFornecedor> bancos, int contratantePjPfId)
        {
            try
            {
                BeginTransaction();
                var ids = bancos.Select(x => x.ID).ToArray();
                _bancoFornecedorService.Delete(
                    _bancoFornecedorService.Find(x => !ids.Contains(x.ID) && x.CONTRATANTE_PJPF_ID == contratantePjPfId)
                        .ToList());

                foreach (var item in bancos)
                {
                    if (item.ID == 0)
                    {
                        item.CONTRATANTE_PJPF_ID = contratantePjPfId;
                        _bancoFornecedorService.Add(item);
                    }
                    else
                    {
                        var banco = _bancoFornecedorService.Get(item.ID);
                        banco.AGENCIA = item.AGENCIA;
                        banco.AG_DV = item.AG_DV;
                        banco.ARQUIVO_ID = item.ARQUIVO_ID;
                        banco.ATIVO = item.ATIVO;
                        banco.BANCO_ID = item.BANCO_ID;
                        banco.CONTA = item.CONTA;
                        banco.CONTA_DV = item.CONTA_DV;
                        _bancoFornecedorService.Update(banco);
                    }
                }
                Commit();
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao tentar salvar os meus bancos.", e);
            }
        }

        public void Dispose()
        {
        }
    }
}