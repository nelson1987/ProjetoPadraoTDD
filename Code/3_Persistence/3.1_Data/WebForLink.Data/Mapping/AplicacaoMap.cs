using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class AplicacaoMap : EntityTypeConfiguration<Aplicacao>
    {
        public AplicacaoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Nome)
                .IsRequired();
            Property(t => t.Descricao)
                .IsRequired();

            // Table & Column Mappings
            ToTable("WFL_APLICACAO");
            Property(t => t.Id).HasColumnName("ID_APLICACAO");
            Property(t => t.Nome).HasColumnName("NOME");
            Property(t => t.Descricao).HasColumnName("DESCRICAO");

            HasMany(x => x.Contratantes)
                .WithRequired(x => x.Aplicacao);
        }
    }
}