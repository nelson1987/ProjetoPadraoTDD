using System.Data.Entity;
using System.Linq;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class SolicitacaoRepository : EntityFrameworkRepository<Solicitacao, ChMasterDataContext>,
        ISolicitacaoRepository
    {
        public Solicitacao BuscarArquivo(int id)
        {
            return DbSet.Include("DocumentoSolicitacao.DocumentoAnexados.Arquivos").FirstOrDefault(x => x.Id == id);
        }

        public Solicitacao BuscarFichaCompleta(int id)
        {
            return DbSet
                .Include("FichaCadastral")
                .Include("FichaCadastral.Endereco")
                .Include("FichaCadastral.Contato")
                .FirstOrDefault(x => x.Id == id);
        }
    }
}