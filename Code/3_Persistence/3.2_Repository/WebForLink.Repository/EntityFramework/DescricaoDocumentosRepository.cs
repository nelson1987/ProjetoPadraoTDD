using System;
using System.Collections.Generic;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Repository.Infrastructure;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class DescricaoDocumentosWebForLinkRepository :
        EntityFrameworkRepository<DescricaoDeDocumentos, WebForLinkContexto>, IDescricaoDocumentosWebForLinkRepository
    {
        public List<DescricaoDeDocumentos> ListarPorContratanteId(int contratanteId)
        {
            try
            {
                return DbSet
                    .Where(e => e.ATIVO
                                && e.CONTRATANTE_ID == contratanteId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new RepositoryWebForLinkException(
                    "Erro ao buscar uma lista de descrição de documentos por Id de contratante e Tipo de documento", ex);
            }
        }
    }
}