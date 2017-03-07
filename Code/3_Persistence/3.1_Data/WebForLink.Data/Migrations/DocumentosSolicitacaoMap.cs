using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class DocumentosSolicitacaoMap : EntityTypeConfiguration<DocumentoSolicitacao>
    {
        public DocumentosSolicitacaoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.DescricaoDocumento)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("DocumentosSolicitacao");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdSolicitacao).HasColumnName("IdSolicitacao");
            this.Property(t => t.DescricaoDocumento).HasColumnName("DescricaoDocumento");
            this.Property(t => t.IdTipoDocumentoCH).HasColumnName("IdTipoDocumentoCH");

            // Relationships
            this.HasRequired(t => t.Solicitacao)
                .WithMany(t => t.DocumentoSolicitacao)
                .HasForeignKey(d => d.IdSolicitacao);

        }
    }
}
