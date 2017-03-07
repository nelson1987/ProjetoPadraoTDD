using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class ContatoMap : EntityTypeConfiguration<Contato>
    {
        public ContatoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(241);

            this.Property(t => t.Telefone)
                .HasMaxLength(14);

            this.Property(t => t.Celular)
                .HasMaxLength(14);

            // Table & Column Mappings
            this.ToTable("Contato");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            this.Property(t => t.Nome).HasColumnName("NOME");
            this.Property(t => t.Email).HasColumnName("EMAIL");
            this.Property(t => t.Telefone).HasColumnName("TELEFONE");
            this.Property(t => t.Celular).HasColumnName("CELULAR");

            // Relationships
            this.HasRequired(t => t.FichaCadastral)
                .WithMany(t => t.Contato)
                .HasForeignKey(d => d.IdFichaCadastral);

        }
    }
}
