using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class SolicitanteMap : EntityTypeConfiguration<Solicitante>
    {
        public SolicitanteMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.CodigoCliente)
                .IsRequired()
                .HasMaxLength(50);

            // Ignore Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);

            // Table & Column Mappings
            ToTable("Solicitante");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Login).HasColumnName("Login");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.CodigoCliente).HasColumnName("CodigoCliente");
        }
    }
}