using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class ListaDocumentoMap : EntityTypeConfiguration<ListaDocumento>
    {
        public ListaDocumentoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Descricao)
                .HasMaxLength(100);

            // Ignore Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);

            // Table & Column Mappings
            ToTable("ListaDocumento");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IdListaSolicitante).HasColumnName("IdListaSolicitante");
            Property(t => t.IdTipoDocumentoCH).HasColumnName("IdTipoDocumentoCH");
            Property(t => t.IdDescricaoDocumentoCH).HasColumnName("IdDescricaoDocumentoCH");
            Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            HasRequired(t => t.ListaSolicitante)
                .WithMany(t => t.ListaDocumento)
                .HasForeignKey(d => d.IdListaSolicitante);
        }
    }
}