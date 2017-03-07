using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class DocumentosSolicitacaoMap : EntityTypeConfiguration<DocumentoSolicitacao>
    {
        public DocumentosSolicitacaoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.DescricaoDocumento)
                .IsRequired()
                .HasMaxLength(50);

            // Ignore Properties
            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);

            // Table & Column Mappings
            ToTable("DocumentosSolicitacao");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IdSolicitacao).HasColumnName("IdSolicitacao");
            Property(t => t.DescricaoDocumento).HasColumnName("DescricaoDocumento");
            Property(t => t.IdTipoDocumentoCH).HasColumnName("IdTipoDocumentoCH");

            // Relationships
            HasRequired(t => t.Solicitacao)
                .WithMany(t => t.DocumentoSolicitacao)
                .HasForeignKey(d => d.IdSolicitacao);
        }
    }
}