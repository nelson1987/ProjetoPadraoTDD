using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Interfaces.Repository.ReadOnly;

namespace WebForLink.Data.Repository.Dapper
{
    public class FornecedorDapperRepository : Common.Repository, IFornecedorReadOnlyRepository
    {
        public IEnumerable<Fornecedor> All()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Fornecedor> Find(Expression<Func<Fornecedor, bool>> predicate)
        {
            try
            {
                using (var context = MusicStoreConnection)
                {
                    //TODO: Pode ser modificada por .Query<Fornecedor>
                    return context.Query("SELECT ID, RAZAO_SOCIAL, CNPJ, CONTRATANTE_ID, UF FROM WFD_PJPF")
                        .Select(x =>
                             new Fornecedor((int)x.ID, (string)x.RAZAO_SOCIAL, (string)x.CNPJ, (int)x.CONTRATANTE_ID, (string)x.UF)
                             ).ToList();

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Fornecedor Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Inserir(Fornecedor fornecedor)
        {
            using (var cn = MusicStoreConnection)
            {
                var albuns =
                    cn.Query<Fornecedor>(
                        "INSERT INTO WFM_FORNECEDOR VALUES()");
                return true;
                //return albuns;
            }
    }
    }
}

