using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class SolicitacaoDocumentoWebForLinkRepository :
        EntityFrameworkRepository<SolicitacaoDeDocumentos, WebForLinkContexto>,
        ISolicitacaoDocumentoWebForLinkRepository
    {
        public List<SolicitacaoDeDocumentos> ListarPorIdSolicitacao(int id)
        {
            try
            {
                return DbSet
                    .Include(x =>x.DescricaoDeDocumentos.TipoDeDocumento)
                    .Include(x=>x.WFD_ARQUIVOS)
                    .Include(x=>x.ListaDeDocumentosDeFornecedor)
                    .Include(x=>x.WFD_T_PERIODICIDADE)
                    .Where(x => x.SOLICITACAO_ID == id)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException("Erro ao buscar uma Solicitação de documento por id", ex);
            }
        }
    }
}