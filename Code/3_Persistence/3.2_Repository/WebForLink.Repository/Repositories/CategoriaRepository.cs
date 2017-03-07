using Dapper;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Data.Repository.EntityFramework.Common;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Repository.Repository
{
    public class Repository : IDisposable
    {
        public IDbConnection MusicStoreConnection
        {
            //get { return new SqlCeConnection(ConfigurationManager.ConnectionStrings["MusicStoreEntities"].ConnectionString); }
            get { return new SqlConnection(ConfigurationManager.ConnectionStrings["MusicStoreEntities"].ConnectionString); }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public Categoria BuscarPorId(int id)
        {
            using (var cn = MusicStoreConnection)
            {
                var artist = cn.Query<Categoria>("SELECT * FROM Artist WHERE ArtistId = @ArtistId",
                    new { ArtistiId = id }).FirstOrDefault();
                return artist;
            }
        }

        public IEnumerable<Categoria> ListarTodos()
        {
            using (var cn = MusicStoreConnection)
            {
                var artist = cn.Query<Categoria>("SELECT * FROM Artist");
                return artist;
            }
        }

        public IEnumerable<Categoria> ListarTodos(Expression<Func<Categoria, bool>> predicate)
        {
            using (var cn = MusicStoreConnection)
            {
                var artist = cn.GetList<Categoria>(predicate);
                return artist;
            }
        }
    }

}
