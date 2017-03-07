using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using WebForLink.Data.Context.Models.Mapping;

namespace WebForLink.Data.Context.Models
{
    public partial class Prd_ChMasterDataContext : DbContext
    {
        static Prd_ChMasterDataContext()
        {
            Database.SetInitializer<Prd_ChMasterDataContext>(null);
        }

        public Prd_ChMasterDataContext()
            : base("Name=Prd_ChMasterDataContext")
        {
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
