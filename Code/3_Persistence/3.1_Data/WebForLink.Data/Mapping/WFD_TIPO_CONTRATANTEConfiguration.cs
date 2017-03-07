using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_TIPO_CONTRATANTEConfiguration : EntityTypeConfiguration<TIPO_CONTRATANTE>
    {
        public WFD_TIPO_CONTRATANTEConfiguration()
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
            ToTable("UTIL_TIPO_CONTRATANTE");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.NOME).HasColumnName("NOME");
        }
    }
}