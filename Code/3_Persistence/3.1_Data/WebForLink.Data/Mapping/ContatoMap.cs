using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class ContatoMap : EntityTypeConfiguration<Contato>
    {
        public ContatoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(30);

            Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(241);

            Property(t => t.Telefone)
                .HasMaxLength(14);

            Property(t => t.Celular)
                .HasMaxLength(14);

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Table & Column Mappings
            ToTable("Contato");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            Property(t => t.Nome).HasColumnName("NOME");
            Property(t => t.Email).HasColumnName("EMAIL");
            Property(t => t.Telefone).HasColumnName("TELEFONE");
            Property(t => t.Celular).HasColumnName("CELULAR");

            // Relationships
            HasRequired(t => t.FichaCadastral)
                .WithMany(t => t.Contato)
                .HasForeignKey(d => d.IdFichaCadastral);
        }
    }
}