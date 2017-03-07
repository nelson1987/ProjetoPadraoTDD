using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class DocumentoAnexadoMap : EntityTypeConfiguration<DocumentoAnexado>
    {
        public DocumentoAnexadoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("DocumentoAnexado");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdFichaCadastral).HasColumnName("IdFichaCadastral");
            this.Property(t => t.IdDocumentoSolicitado).HasColumnName("IdDocumentoSolicitado");

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Relationships
            this.HasRequired(t => t.FichaCadastral)
                .WithMany(t => t.DocumentoAnexadoes)
                .HasForeignKey(d => d.IdFichaCadastral);

        }
    }
}
