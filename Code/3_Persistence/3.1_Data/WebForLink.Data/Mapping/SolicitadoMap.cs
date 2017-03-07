using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class SolicitadoMap : EntityTypeConfiguration<Solicitado>
    {
        public SolicitadoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Cnpj)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.RazaoSocial)
                .HasMaxLength(50);

            Property(t => t.Cidade)
                .HasMaxLength(50);

            Property(t => t.Estado)
                .HasMaxLength(50);

            // Ignore Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);

            // Table & Column Mappings
            ToTable("Solicitado");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Cnpj).HasColumnName("Cnpj");
            Property(t => t.Estado).HasColumnName("Estado");
            Property(t => t.Cidade).HasColumnName("Cidade");
            Property(t => t.RazaoSocial).HasColumnName("RazaoSocial");
        }
    }
}