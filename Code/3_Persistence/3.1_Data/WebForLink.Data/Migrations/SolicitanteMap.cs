using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class SolicitanteMap : EntityTypeConfiguration<Solicitante>
    {
        public SolicitanteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.CodigoCliente)
                .IsRequired()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Solicitante");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Login).HasColumnName("Login");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.CodigoCliente).HasColumnName("Codigocliente");
        }
    }
}
