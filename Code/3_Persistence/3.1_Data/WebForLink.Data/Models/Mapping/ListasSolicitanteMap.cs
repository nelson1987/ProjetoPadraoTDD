using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class ListasSolicitanteMap : EntityTypeConfiguration<ListasSolicitante>
    {
        public ListasSolicitanteMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(10);

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Table & Column Mappings
            this.ToTable("ListasSolicitante");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitante).HasColumnName("IdSolicitante");
            this.Property(t => t.Nome).HasColumnName("Nome");

            // Relationships
            this.HasRequired(t => t.Solicitante)
                .WithMany(t => t.ListasSolicitantes)
                .HasForeignKey(d => d.IdSolicitante);

        }
    }
}
