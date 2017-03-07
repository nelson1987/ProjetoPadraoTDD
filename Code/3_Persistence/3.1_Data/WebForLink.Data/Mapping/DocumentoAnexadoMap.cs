using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class DocumentoAnexadoMap : EntityTypeConfiguration<DocumentoAnexado>
    {
        public DocumentoAnexadoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Table & Column Mappings
            this.ToTable("DocumentoAnexado");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdDocumentoSolicitado).HasColumnName("IdDocumentoSolicitado");
            this.Property(t => t.DataReprovacao).HasColumnName("DataReprovacao");
            this.Property(t => t.MensagemReprovacao).HasColumnName("MensagemReprovacao");
            this.Property(t => t.Reprovado).HasColumnName("Reprovado");
            this.Property(t => t.DataUltimoDownload).HasColumnName("DataUltimoDownload");
            this.Ignore(t => t.EhValido);
            this.Ignore(t => t.ValidationResult);

            // Relationships
            this.HasRequired(t => t.DocumentosSolicitacao)// .Solicitacao)
                .WithMany(t => t.DocumentoAnexados)
                .HasForeignKey(d => d.IdDocumentoSolicitado);
        }
    }
}