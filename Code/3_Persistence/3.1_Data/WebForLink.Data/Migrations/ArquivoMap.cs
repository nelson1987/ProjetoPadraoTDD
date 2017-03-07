using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Models.Mapping
{
    public class ArquivoMap : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NomeOriginal)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Arquivo");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdDocumentoAnexado).HasColumnName("IdDocumentoAnexado");
            this.Property(t => t.NomeOriginal).HasColumnName("NomeOriginal");
            this.Property(t => t.JaBaixado).HasColumnName("JaBaixado");

            // Relationships
            this.HasRequired(t => t.DocumentoAnexado)
                .WithMany(t => t.Arquivos)
                .HasForeignKey(d => d.IdDocumentoAnexado);

        }
    }
}
