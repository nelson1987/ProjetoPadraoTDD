using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Win.Banco
{
    public class FabricanteMap : EntityTypeConfiguration<Fabricante>
    {
        public FabricanteMap()
        {
            //Map(m => m.Requires("Tipo").HasValue(3));
            //ToTable("WFL_FABRICANTE");
            HasMany(t => t.Clientes)
                .WithMany(x => x.Fabricantes);
            HasMany(t => t.Fornecedores)
                .WithMany(x => x.Fabricantes);
        }
    }
}