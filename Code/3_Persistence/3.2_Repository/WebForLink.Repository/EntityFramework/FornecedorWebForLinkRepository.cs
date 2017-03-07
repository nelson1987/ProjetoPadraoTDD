using System;
using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class FornecedorWebForLinkRepository : EntityFrameworkRepository<Fornecedor, WebForLinkContexto>,
        IFornecedorWebForLinkRepository
    {
        public virtual Fornecedor CarregarDadosPjpf(int idFornecedor)
        {
            try
            {
                return DbSet
                    .Include("WFD_CONTRATANTE_PJPF")
                    .Include("WFD_CONTRATANTE_PJPF.BancoDoFornecedor")
                    .Include("WFD_CONTRATANTE_PJPF.WFD_PJPF_CONTATOS")
                    .Include("WFD_CONTRATANTE_PJPF.Contratante")
                    .Include("WFD_CONTRATANTE_PJPF.Contratante.QIC_QUESTIONARIO")
                    .FirstOrDefault(x => x.ID == idFornecedor);
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Lista de Perguntas por aba", ex);
            }
        }
    }
}