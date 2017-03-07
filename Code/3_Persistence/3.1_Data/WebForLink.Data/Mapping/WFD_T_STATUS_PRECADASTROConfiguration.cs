using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_STATUS_PRECADASTROConfiguration : EntityTypeConfiguration<TIPO_STATUS_PRECADASTRO>
    {
        public WFD_T_STATUS_PRECADASTROConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.STATUS_NM)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_STATUS_PRECADASTRO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.STATUS_NM).HasColumnName("STATUS_NM");
        }
    }
}