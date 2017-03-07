using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class ContatoMap : EntityTypeConfiguration<Contato>
    {
        public ContatoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NOME)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.EMAIL)
                .IsRequired()
                .HasMaxLength(241);

            this.Property(t => t.TELEFONE)
                .HasMaxLength(14);

            this.Property(t => t.CELULAR)
                .HasMaxLength(14);

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Table & Column Mappings
            this.ToTable("Contato");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            this.Property(t => t.NOME).HasColumnName("NOME");
            this.Property(t => t.EMAIL).HasColumnName("EMAIL");
            this.Property(t => t.TELEFONE).HasColumnName("TELEFONE");
            this.Property(t => t.CELULAR).HasColumnName("CELULAR");

            // Relationships
            this.HasRequired(t => t.FichaCadastral)
                .WithMany(t => t.Contatoes)
                .HasForeignKey(d => d.IdFichaCadastral);

        }
    }
}
