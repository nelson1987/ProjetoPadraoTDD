using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class ArquivoMap : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.NomeOriginal)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            ToTable("Arquivo");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.IdDocumentoAnexado).HasColumnName("IdDocumentoAnexado");
            Property(t => t.NomeOriginal).HasColumnName("NomeOriginal");
            Property(t => t.JaBaixado).HasColumnName("JaBaixado");
            Property(t => t.LocalArquivo).HasColumnName("LocalArquivo");

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Relationships
            HasRequired(t => t.DocumentoAnexado)
                .WithMany(t => t.Arquivos)
                .HasForeignKey(d => d.IdDocumentoAnexado);
        }
    }
}