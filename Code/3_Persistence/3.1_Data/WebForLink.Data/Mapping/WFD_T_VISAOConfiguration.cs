using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_VISAOConfiguration : EntityTypeConfiguration<TIPO_VISAO>
    {
        public WFD_T_VISAOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.VISAO_NM)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_VISAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.VISAO_NM).HasColumnName("VISAO_NM");
        }
    }
}