using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_TIPO_CADASTROConfiguration : EntityTypeConfiguration<TIPO_CADASTRO_FORNECEDOR>
    {
        public WFD_TIPO_CADASTROConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.NOME)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_CADASTRO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.NOME).HasColumnName("NOME");
        }
    }
}