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
    public interface IFornecedorContatoWebForLinkService : IService<FORNECEDOR_CONTATOS>
    {
        List<FORNECEDOR_CONTATOS> ListarPorContratantePJPFId(int id);
        FORNECEDOR_CONTATOS BuscarPorContratantePJPFId(int id);
        void ManterMeusContatos(List<FORNECEDOR_CONTATOS> contatos, int contratantePjPfId);
    }

    public class FornecedorContatoWebForLinkService : Service<FORNECEDOR_CONTATOS>, IFornecedorContatoWebForLinkService
    {
        private readonly IFornecedorContatoWebForLinkRepository _fornecedorContatoRepository;

        public FornecedorContatoWebForLinkService(IFornecedorContatoWebForLinkRepository fornecedorContato)
            : base(fornecedorContato)
        {
            try
            {
                _fornecedorContatoRepository = fornecedorContato;
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
                return _fornecedorContatoRepository.Find(x => x.CONTRATANTE_PJPF_ID == id).ToList();
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
                return _fornecedorContatoRepository.Get(x => x.CONTRATANTE_PJPF_ID == id);
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
                var ids = contatos.Select(x => x.ID).ToArray();
                _fornecedorContatoRepository.Delete(
                    _fornecedorContatoRepository.Find(
                        x => !ids.Contains(x.ID) && x.CONTRATANTE_PJPF_ID == contratantePjPfId).ToList());

                foreach (var item in contatos)
                {
                    if (item.ID == 0)
                    {
                        item.CONTRATANTE_PJPF_ID = contratantePjPfId;
                        _fornecedorContatoRepository.Add(item);
                    }
                    else
                    {
                        var contato = _fornecedorContatoRepository.Get(item.ID);
                        contato.CELULAR = item.CELULAR;
                        contato.EMAIL = item.EMAIL;
                        contato.NOME = item.NOME;
                        contato.TELEFONE = item.TELEFONE;
                        _fornecedorContatoRepository.Update(contato);
                    }
                }
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