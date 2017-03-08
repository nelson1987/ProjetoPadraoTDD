using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Win.Banco
{
    public class ClienteMap : EntityTypeConfiguration<Cliente>
    {
        public ClienteMap()
        {
            //ToTable("WFL_CLIENTE");
            ToTable("WFL_CLIENTE_TESTE");
            //HasMany(t => t.Fornecedores)
            //    .WithMany(x => x.Clientes)
            //    .Map(m =>
            //    {
            //        m.ToTable("CLIENTE_FORNECEDOR");
            //        m.MapLeftKey("CLIENTE_ID");
            //        m.MapRightKey("FORNECEDOR_ID");
            //    });
            
            //HasMany(t => t.Fabricantes)
            //    .WithMany(x => x.Clientes)
            //    .Map(m =>
            //    {
            //        m.ToTable("CLIENTE_FABRICANTE");
            //        m.MapLeftKey("CLIENTE_ID");
            //        m.MapRightKey("FABRICANTE_ID");
            //    });
        }
    }
}