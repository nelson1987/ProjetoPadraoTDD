using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebForLink.Data.Repository.Dapper.Common
{
    public class Repository : IDisposable
    {
        public IDbConnection MusicStoreConnection
        {
            get
            {
                return new SqlConnection(ConfigurationManager.ConnectionStrings["MusicStoreEntities"].ConnectionString);
            }
        }

        public void Dispose()
        {
            SqlConnection.ClearAllPools();
            GC.SuppressFinalize(this);
        }
    }
}