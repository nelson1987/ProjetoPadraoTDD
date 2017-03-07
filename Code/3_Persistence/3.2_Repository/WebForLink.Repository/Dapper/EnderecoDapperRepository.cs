using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Transactions;
using Dapper;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class EnderecoDapperRepository : Common.Repository, IEnderecoReadOnlyRepository
    {
        public IEnumerable<Endereco> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Endereco> Find(Expression<Func<Endereco, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Endereco Get(int id)
        {
            throw new NotImplementedException();
        }

        public Endereco GetAllReferences(int id)
        {
            throw new NotImplementedException();
        }

        public Endereco Insert(Endereco entity)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var cn = MusicStoreConnection)
                {
                    try
                    {
                        cn.Execute(
                            @"INSERT INTO [dbo].[Endereco] (IdFichaCadastral, Tipo, Logradouro, Numero, Complemento, CEP, Bairro, Cidade, UF, Pais) VALUES (@idFicha, @tipo, @logradouro, @numero, @complemento, @cep, @bairro, @cidade, @uf, @pais);",
                            new
                            {
                                idFicha = entity.IdFichaCadastral,
                                tipo = entity.Tipo,
                                logradouro = entity.Logradouro,
                                numero = entity.Numero,
                                complemento = entity.Complemento,
                                cep = entity.CEP,
                                bairro = entity.Bairro,
                                cidade = entity.Cidade,
                                uf = entity.UF,
                                pais = "Brasil"
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

        public Endereco Update(Endereco entity)
        {
            using (var transactionScope = new TransactionScope())
            {
                using (var cn = MusicStoreConnection)
                {
                    try
                    {
                        cn.Execute(
                            @"UPDATE [dbo].[Endereco] SET Tipo = @tipo, Logradouro = @logradouro, Numero = @numero, Complemento = @complemento, CEP = @cep, Bairro = @bairro, Cidade = @cidade, UF = @uf, Pais = @pais WHERE ID = @id; ",
                            new
                            {
                                id = entity.Id,
                                tipo = entity.Tipo,
                                logradouro = entity.Logradouro,
                                numero = entity.Numero,
                                complemento = entity.Complemento,
                                cep = entity.CEP,
                                bairro = entity.Bairro,
                                cidade = entity.Cidade,
                                uf = entity.UF,
                                pais = entity.Pais
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