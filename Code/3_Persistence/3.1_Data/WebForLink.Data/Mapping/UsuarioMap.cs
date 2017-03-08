using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Login)
                .IsRequired()
                .HasMaxLength(150);

            // Table & Column Mappings
            ToTable("Arquivo");
            Property(t => t.Id).HasColumnName("Id");

            Ignore(t => t.ValidationResult);
            Ignore(t => t.EhValido);
            // Relationships
            //HasRequired(t => t.DocumentoAnexado)
            //    .WithMany(t => t.Arquivos)
            //    .HasForeignKey(d => d.IdDocumentoAnexado);
        }
    }
}