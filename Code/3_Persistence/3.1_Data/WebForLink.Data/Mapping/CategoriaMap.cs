using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;

namespace WebForLink.Data.Context.Mapping
{
    public class CategoriaMap : EntityTypeConfiguration<Categoria>
    {
        public CategoriaMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Descricao)
                .HasMaxLength(50)
                .IsRequired();

            Property(t => t.Codigo)
                .HasMaxLength(5)
                .IsRequired();

            Ignore(t => t.ValidationResult);


            ToTable("WFD_PJPF_CATEGORIA");
            this.Property(t => t.Id).HasColumnName("ID");
            this.Property(t => t.Descricao).HasColumnName("DESCRICAO");
            this.Property(t => t.Codigo).HasColumnName("CODIGO");
            this.Property(t => t.Ativo).HasColumnName("ATIVO");
        }
    }
}