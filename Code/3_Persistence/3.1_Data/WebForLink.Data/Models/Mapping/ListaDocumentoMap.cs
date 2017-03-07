using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class ListaDocumentoMap : EntityTypeConfiguration<ListaDocumento>
    {
        public ListaDocumentoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Table & Column Mappings
            this.ToTable("ListaDocumento");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdListaSolicitante).HasColumnName("IdListaSolicitante");
            this.Property(t => t.IdTipoDocumentoCH).HasColumnName("IdTipoDocumentoCH");
            this.Property(t => t.IdDescricaoDocumentoCH).HasColumnName("IdDescricaoDocumentoCH");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.ListasSolicitante)
                .WithMany(t => t.ListaDocumentoes)
                .HasForeignKey(d => d.IdListaSolicitante);

        }
    }
}
