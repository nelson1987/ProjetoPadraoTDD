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
    public interface IFornecedorContatoWebForLinkAppService
    {
        List<FORNECEDOR_CONTATOS> ListarPorContratantePJPFId(int id);
        FORNECEDOR_CONTATOS BuscarPorContratantePJPFId(int id);
        void ManterMeusContatos(List<FORNECEDOR_CONTATOS> contatos, int contratantePjPfId);
    }

    public class FornecedorContatoWebForLinkAppService : AppService<WebForLinkContexto>,
        IFornecedorContatoWebForLinkAppService
    {
        private readonly IFornecedorContatoWebForLinkService _fornecedorContatoService;

        public FornecedorContatoWebForLinkAppService(IFornecedorContatoWebForLinkService fornecedorContato)
        {
            try
            {
                _fornecedorContatoService = fornecedorContato;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public List<FORNECEDOR_CONTATOS> ListarPorContratantePJPFId(int id)
        {
            try
            {
                return _fornecedorContatoService.Find(x => x.CONTRATANTE_PJPF_ID == id).ToList();
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar os dados dos contatos.", e);
            }
        }

        public FORNECEDOR_CONTATOS BuscarPorContratantePJPFId(int id)
        {
            try
            {
                return _fornecedorContatoService.Get(x => x.CONTRATANTE_PJPF_ID == id);
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao buscar os dados dos contatos.", e);
            }
        }

        public void ManterMeusContatos(List<FORNECEDOR_CONTATOS> contatos, int contratantePjPfId)
        {
            try
            {
                BeginTransaction();
                var ids = contatos.Select(x => x.ID).ToArray();
                _fornecedorContatoService.Delete(
                    _fornecedorContatoService.Find(
                        x => !ids.Contains(x.ID) && x.CONTRATANTE_PJPF_ID == contratantePjPfId).ToList());

                foreach (var item in contatos)
                {
                    if (item.ID == 0)
                    {
                        item.CONTRATANTE_PJPF_ID = contratantePjPfId;
                        _fornecedorContatoService.Add(item);
                    }
                    else
                    {
                        var contato = _fornecedorContatoService.Get(item.ID);
                        contato.CELULAR = item.CELULAR;
                        contato.EMAIL = item.EMAIL;
                        contato.NOME = item.NOME;
                        contato.TELEFONE = item.TELEFONE;
                        _fornecedorContatoService.Update(contato);
                    }
                }
                Commit();
            }
            catch (Exception e)
            {
                throw new ServiceWebForLinkException("Ocorreu um erro ao tentar salvar os meus Contatos.", e);
            }
        }

        public void Dispose()
        {
        }
    }
}