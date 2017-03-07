using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Dapper;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository;

namespace WebForLink.Data.Repository.Dapper
{
    public class FichaCadastralDapperRepository : Common.Repository, IFichaCadastralReadOnlyRepository
    {
        public IEnumerable<FichaCadastral> All()
        {
            IEnumerable<FichaCadastral> retorno = null;
            using (var transactionScope = new TransactionScope())
            {
                using (var cn = MusicStoreConnection)
                {
                    try
                    {
                        retorno = cn.Query<FichaCadastral>(@"SELECT * FROM [dbo].[FichaCadastral];");
                    }
                    catch (Exception e)
                    {
                        //transactionScope.Dispose();
                        Console.WriteLine(e.Message);
                    }
                }
                transactionScope.Complete();
            }
            return retorno;
        }

        public IEnumerable<FichaCadastral> Find(Expression<Func<FichaCadastral, bool>> predicate)
        {
            return All().AsQueryable().Where(predicate);
        }

        public FichaCadastral Get(int id)
        {
            FichaCadastral retorno = null;
            using (var cn = MusicStoreConnection)
            {
                try
                {
                    retorno = cn.Query<FichaCadastral>(
                        @"SELECT * FROM [dbo].[FichaCadastral] Where Id = @Id;"
                        , new {Id = id})
                        .FirstOrDefault();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return retorno;
        }

        public FichaCadastral GetAllReferences(int id)
        {
            FichaCadastral retorno = null;
            using (var cn = MusicStoreConnection)
            {
                try
                {
                    retorno = cn.Query<FichaCadastral, Contato, Endereco, Banco, FichaCadastral>(
                        @"SELECT * FROM [dbo].[FichaCadastral] sol
                        inner join [dbo].[Contato] cli on sol.Id = cli.IdFichaCadastral
                        inner join [dbo].[Endereco] forn on sol.Id = forn.IdFichaCadastral
                        inner join [dbo].[Banco] resp on sol.Id = resp.IdFichaCadastral
                        WHERE sol.ID = @Id;",
                        (sol, cli, forn, resp) =>
                        {
                            sol.AdicionarContato(cli);
                            sol.AdicionarBanco(resp);
                            sol.AdicionarEndereco(forn);
                            return sol;
                        }
                        , new {Id = id})
                        .FirstOrDefault();
                    return new FichaCadastral();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            return retorno;
        }

        public void Incluir(FichaCadastral ficha, int idSolicitacao)
        {
            using (var cn = MusicStoreConnection)
            {
                try
                {
                    var sql = @"INSERT INTO [dbo].[FichaCadastral] (Status) VALUES(@status);
                                    SELECT CAST(SCOPE_IDENTITY() as int)";

                    var idFichaCadastral = cn.Query<int>(sql, new
                    {
                        status = ficha.Status
                    }).FirstOrDefault();

                    cn.Execute(
                        "UPDATE [dbo].[Solicitacao] SET IdFichaCadastral = @idFichaCadastral where Id = @id;",
                        new
                        {
                            idFichaCadastral,
                            id = idSolicitacao
                        });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void IncluirFichaCadastral(FichaCadastral ficha)
        {
            throw new NotImplementedException();
        }

        public void Add(FichaCadastral entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaCadastral> All(bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public void Delete(FichaCadastral entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaCadastral> Find(Expression<Func<FichaCadastral, bool>> predicate, bool @readonly = false)
        {
            throw new NotImplementedException();
        }

        public void Update(FichaCadastral entity)
        {
            throw new NotImplementedException();
        }
    }
}