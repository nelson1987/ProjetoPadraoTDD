using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class ListasSolicitanteMap : EntityTypeConfiguration<ListasSolicitante>
    {
        public ListasSolicitanteMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(50);

            // Ignore Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);

            // Table & Column Mappings
            ToTable("ListasSolicitante");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IdSolicitante).HasColumnName("IdSolicitante");
            Property(t => t.Nome).HasColumnName("Nome");

            // Relationships
            HasRequired(t => t.Solicitante)
                .WithMany(t => t.ListasDocumentosSolicitante)
                .HasForeignKey(d => d.IdSolicitante);
        }
    }
}