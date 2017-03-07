using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class ResponsavelMap : EntityTypeConfiguration<Responsavel>
    {
        public ResponsavelMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Email)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            // Table & Column Mappings
            this.ToTable("Responsavel");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.IdSolicitado).HasColumnName("IdSolicitado");

            // Relationships
            this.HasRequired(t => t.Solicitado)
                .WithMany(t => t.Responsaveis)
                .HasForeignKey(d => d.IdSolicitado);

        }
    }
}
