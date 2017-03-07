using System;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository.Common;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public interface IFornecedorInformacaoComplementarComplRepository : IRepository<FORNECEDOR_INFORM_COMPL>
    {
        FORNECEDOR_INFORM_COMPL BuscarPorPerguntaIdFornecedorId(int idPergunta, int idPjpf);
    }

    public class FornecedorInformComplRepository :
        EntityFrameworkRepository<FORNECEDOR_INFORM_COMPL, WebForLinkContexto>,
        IFornecedorInformacaoComplementarComplRepository
    {
        public FORNECEDOR_INFORM_COMPL BuscarPorPerguntaIdFornecedorId(int idPergunta, int idPjpf)
        {
            try
            {
                return DbSet.FirstOrDefault(x => x.PERG_ID == idPergunta && x.CONTRATANTE_PJPF_ID == idPjpf);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }
    }
}