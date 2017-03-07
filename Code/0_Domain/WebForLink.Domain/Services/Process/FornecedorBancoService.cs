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
    public interface IFornecedorBancoWebForLinkService : IService<BancoDoFornecedor>
    {
        List<BancoDoFornecedor> BuscarPorContratantePJPFId(int id);
        void ManterMeusBancos(List<BancoDoFornecedor> bancos, int contratantePjPfId);
    }

    public class FornecedorBancoWebForLinkService : Service<BancoDoFornecedor>, IFornecedorBancoWebForLinkService
    {
        private readonly IFornecedorBancoWebForLinkRepository _bancoFornecedorRepository;

        public FornecedorBancoWebForLinkService(IFornecedorBancoWebForLinkRepository bancoFornecedor)
            : base(bancoFornecedor)
        {
            try
            {
                _bancoFornecedorRepository = bancoFornecedor;
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
                return _bancoFornecedorRepository.Find(x => x.CONTRATANTE_PJPF_ID == id).ToList();
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
                var ids = bancos.Select(x => x.ID).ToArray();
                _bancoFornecedorRepository.Delete(
                    _bancoFornecedorRepository.Find(
                        x => !ids.Contains(x.ID) && x.CONTRATANTE_PJPF_ID == contratantePjPfId).ToList());

                foreach (var item in bancos)
                {
                    if (item.ID == 0)
                    {
                        item.CONTRATANTE_PJPF_ID = contratantePjPfId;
                        _bancoFornecedorRepository.Add(item);
                    }
                    else
                    {
                        var banco = _bancoFornecedorRepository.Get(item.ID);
                        banco.AGENCIA = item.AGENCIA;
                        banco.AG_DV = item.AG_DV;
                        banco.ARQUIVO_ID = item.ARQUIVO_ID;
                        banco.ATIVO = item.ATIVO;
                        banco.BANCO_ID = item.BANCO_ID;
                        banco.CONTA = item.CONTA;
                        banco.CONTA_DV = item.CONTA_DV;
                        _bancoFornecedorRepository.Update(banco);
                    }
                }
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