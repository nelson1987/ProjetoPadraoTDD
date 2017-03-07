using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_TP_EMAILConfiguration : EntityTypeConfiguration<TIPO_EMAIL>
    {
        public WFD_T_TP_EMAILConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.TP_EMAIL_NM)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_EMAIL");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TP_EMAIL_NM).HasColumnName("TP_EMAIL_NM");
        }
    }
}