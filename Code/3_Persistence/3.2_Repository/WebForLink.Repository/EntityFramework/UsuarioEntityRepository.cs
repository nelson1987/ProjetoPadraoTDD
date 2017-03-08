using System;
using WebForLink.Data.Context;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.EntityFramework
{
    public class UsuarioEntityRepository : EntityFrameworkRepository<Usuario, WebForLinkContexto>,
        IUsuarioRepository
    {
        //public Solicitacao BuscarArquivo(int id)
        //{
        //    return DbSet.Include("DocumentoSolicitacao.DocumentoAnexados.Arquivos").FirstOrDefault(x => x.Id == id);
        //}

        //public Solicitacao BuscarFichaCompleta(int id)
        //{
        //    return DbSet
        //        .Include("FichaCadastral")
        //        .Include("FichaCadastral.Endereco")
        //        .Include("FichaCadastral.Contato")
        //        .FirstOrDefault(x => x.Id == id);
        //}
        public Usuario BuscarArquivo(int id)
        {
            throw new NotImplementedException();
        }

        public Usuario BuscarFichaCompleta(int id)
        {
            throw new NotImplementedException();
        }
    }
}