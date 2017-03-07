using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Transactions;
using Dapper;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class SolicitacaoDapperRepository : Common.Repository, ISolicitacaoReadOnlyRepository
    {
        public IEnumerable<Solicitacao> All()
        {
            IEnumerable<Solicitacao> retorno = null;
            using (var transactionScope = new TransactionScope())
            {
                using (var cn = MusicStoreConnection)
                {
                    try
                    {
                        retorno = cn.Query<Solicitacao>(@"SELECT * FROM [dbo].[Solicitacao];");
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

        public IEnumerable<Solicitacao> Find(Expression<Func<Solicitacao, bool>> predicate)
        {
            return All().AsQueryable().Where(predicate);
        }

        public Solicitacao Get(int id)
        {
            Solicitacao retorno = null;
            using (var cn = MusicStoreConnection)
            {
                try
                {
                    retorno = cn.Query<Solicitacao>(
                        @"SELECT * FROM [dbo].[Solicitacao] Where Id = @Id;"
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

        public Solicitacao GetAllReferences(int id)
        {
            Solicitacao retorno = null;
            using (var cn = MusicStoreConnection)
            {
                try
                {
                    var lookup = new Dictionary<int, Solicitado>();
                    retorno = cn.Query<Solicitacao, Solicitante, Solicitado, Responsavel, Solicitacao>(
                        @"SELECT * FROM [dbo].[Solicitacao] sol
                        inner join [dbo].[Solicitante] cli on sol.IdSolicitante = cli.Id
                        inner join [dbo].[Solicitado] forn on sol.IdSolicitado = forn.Id
                        inner join [dbo].[Responsavel] resp on sol.IdSolicitado = resp.IdSolicitado
                        WHERE sol.ID = @Id;",
                        (sol, cli, forn, resp) =>
                        {
                            Solicitado shop;
                            if (!lookup.TryGetValue(forn.Id, out shop))
                                lookup.Add(forn.Id, shop = forn);

                            if (shop.Responsaveis == null)
                                shop.Responsaveis = new List<Responsavel>();

                            shop.Responsaveis.Add(resp);

                            sol.IncluirSolicitante(cli);
                            sol.IncluirSolicitado(forn);
                            return sol;
                        }
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

        public Solicitacao GetAllReferencesFichaCadastral(int id)
        {
            Solicitacao retorno = null;
            using (var cn = MusicStoreConnection)
            {
                try
                {
                    var lookup = new Dictionary<int, Solicitado>();
                    retorno = cn.Query<Solicitacao, Solicitante, Solicitado, Responsavel, Solicitacao>(
                        @"SELECT * FROM [dbo].[Solicitacao] sol
                        inner join [dbo].[Solicitante] cli on sol.IdSolicitante = cli.Id
                        inner join [dbo].[Solicitado] forn on sol.IdSolicitado = forn.Id
                        inner join [dbo].[Responsavel] resp on sol.IdSolicitado = resp.IdSolicitado
                        inner join [dbo].[FichaCadastral] ficha on sol.IdFichaCadastral = ficha.Id
                        WHERE sol.ID = @Id;",
                        (sol, cli, forn, resp) =>
                        {
                            Solicitado shop;
                            if (!lookup.TryGetValue(forn.Id, out shop))
                                lookup.Add(forn.Id, shop = forn);

                            if (shop.Responsaveis == null)
                                shop.Responsaveis = new List<Responsavel>();

                            shop.Responsaveis.Add(resp);

                            sol.IncluirSolicitante(cli);
                            sol.IncluirSolicitado(forn);
                            return sol;
                        }
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
    }
}