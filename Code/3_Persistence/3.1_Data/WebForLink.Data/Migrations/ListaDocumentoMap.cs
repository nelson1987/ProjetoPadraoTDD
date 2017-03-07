using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

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

            // Table & Column Mappings
            this.ToTable("ListaDocumento");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdListaSolicitante).HasColumnName("IdListaSolicitante");
            this.Property(t => t.IdTipoDocumentoCH).HasColumnName("IdTipoDocumentoCH");
            this.Property(t => t.IdDescricaoDocumentoCH).HasColumnName("IdDescricaoDocumentoCH");
            this.Property(t => t.Descricao).HasColumnName("Descricao");

            // Relationships
            this.HasRequired(t => t.ListaSolicitante)
                .WithMany(t => t.ListaDocumento)
                .HasForeignKey(d => d.IdListaSolicitante);

        }
    }
}
