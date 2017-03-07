using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using DapperExtensions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class CategoriaDapperRepository : Common.Repository, ICategoriaReadOnlyRepository
    {
        public Categoria Get(int id)
        {
            using (var cn = MusicStoreConnection)
            {
                var album = cn.Query<Categoria>("SELECT * FROM Album Al").FirstOrDefault();
                return album;
            }
        }

        public IEnumerable<Categoria> All()
        {
            using (var cn = MusicStoreConnection)
            {
                var albuns =
                    cn.Query<Categoria>(
                        "SELECT ID as Id, DESCRICAO as Descricao, CODIGO as Codigo, ATIVO as Ativo FROM WFD_PJPF_CATEGORIA");
                return albuns;
            }
        }

        public IEnumerable<Categoria> Find(Expression<Func<Categoria, bool>> predicate)
        {
            using (var cn = MusicStoreConnection)
            {
                var albuns = cn.GetList<Categoria>(predicate);
                return albuns;
            }
        }
    }
}