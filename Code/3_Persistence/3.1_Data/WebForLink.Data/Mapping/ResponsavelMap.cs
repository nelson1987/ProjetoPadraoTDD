using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class ResponsavelMap : EntityTypeConfiguration<Responsavel>
    {
        public ResponsavelMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Nome)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            Property(t => t.Email)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(50);

            // Ignore Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);

            // Table & Column Mappings
            ToTable("Responsavel");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.Nome).HasColumnName("Nome");
            Property(t => t.Email).HasColumnName("Email");
            Property(t => t.IdSolicitado).HasColumnName("IdSolicitado");

            // Relationships
            HasRequired(t => t.Solicitado)
                .WithMany(t => t.Responsaveis)
                .HasForeignKey(d => d.IdSolicitado);
        }
    }
}