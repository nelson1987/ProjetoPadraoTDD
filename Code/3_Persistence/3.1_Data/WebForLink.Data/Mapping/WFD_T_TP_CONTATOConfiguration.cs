using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_TP_CONTATOConfiguration : EntityTypeConfiguration<TIPO_CONTATO>
    {
        public WFD_T_TP_CONTATOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.NM_TP_CONTATO)
                .HasMaxLength(15);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_CONTATO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.NM_TP_CONTATO).HasColumnName("NM_TP_CONTATO");
        }
    }
}