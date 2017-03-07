using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using Dapper;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class ContatoDapperRepository : Common.Repository, IContatoReadOnlyRepository
    {
        public IEnumerable<Contato> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contato> Find(Expression<Func<Contato, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Contato Get(int id)
        {
            throw new NotImplementedException();
        }

        public Contato GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }

        public Contato Insert(Contato entity)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var cn = MusicStoreConnection)
                {
                    try
                    {
                        cn.Execute(
                            @"INSERT INTO [dbo].[Contato] (IdFichaCadastral, NOME, EMAIL,TELEFONE,CELULAR) VALUES (@idFicha, @nome, @email, @telefone, @celular);",
                            new
                            {
                                idFicha = entity.IdFichaCadastral,
                                nome = entity.Nome,
                                email = entity.Email,
                                telefone = entity.Telefone,
                                celular = entity.Celular
                            });
                    }
                    catch (Exception e)
                    {
                        //transactionScope.Dispose();
                        Console.WriteLine(e.Message);
                    }
                }
                transactionScope.Complete();
            }
            return entity;
        }

        public Contato Update(Contato entity)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var cn = MusicStoreConnection)
                {
                    try
                    {
                        cn.Execute(
                            @"UPDATE [dbo].[Contato] SET NOME = @nome, EMAIL = @email, TELEFONE = @telefone, CELULAR = @celular WHERE ID = @id;",
                            new
                            {
                                id = entity.Id,
                                nome = entity.Nome,
                                email = entity.Email,
                                telefone = entity.Telefone,
                                celular = entity.Celular
                            });
                    }
                    catch (Exception e)
                    {
                        //transactionScope.Dispose();
                        Console.WriteLine(e.Message);
                    }
                }
                transactionScope.Complete();
            }
            return entity;
        }
    }
}