using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using Dapper;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class BancoDapperRepository : Common.Repository, IBancoReadOnlyRepository
    {
        public IEnumerable<Banco> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Banco> Find(Expression<Func<Banco, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Banco Get(int id)
        {
            throw new NotImplementedException();
        }

        public Banco GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }

        public Banco Insert(Banco entity)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var cn = MusicStoreConnection)
                {
                    try
                    {
                        cn.Execute(
                            @"INSERT INTO [dbo].[Banco] (IdFichaCadastral, Numero, Agencia, AgenciaDv, Conta, ContaDv) VALUES (@idFicha, @numero, @agencia, @agenciaDv, @conta, @contaDv);",
                            new
                            {
                                idFicha = entity.IdFichaCadastral,
                                numero = entity.Numero,
                                agencia = entity.Agencia,
                                agenciaDv = entity.AgenciaDv,
                                conta = entity.Conta,
                                contaDv = entity.ContaDv
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

        public Banco Update(Banco entity)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var cn = MusicStoreConnection)
                {
                    try
                    {
                        cn.Execute(
                            @"UPDATE [dbo].[Banco] SET Numero = @numero, Agencia = @agencia, AgenciaDv = @agenciaDv, Conta = @conta, ContaDv = @contaDv WHERE ID = @id;",
                            new
                            {
                                id = entity.Id,
                                numero = entity.Numero,
                                agencia = entity.Agencia,
                                agenciaDv = entity.AgenciaDv,
                                conta = entity.Conta,
                                contaDv = entity.ContaDv
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