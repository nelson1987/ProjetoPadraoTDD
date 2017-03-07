using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class DocumentosSolicitacaoMap : EntityTypeConfiguration<DocumentosSolicitacao>
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

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Relationships
            this.HasRequired(t => t.Solicitacao)
                .WithMany(t => t.DocumentosSolicitacaos)
                .HasForeignKey(d => d.IdSolicitacao);

        }
    }
}
