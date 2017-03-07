using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WAC_APLICACAOConfiguration : EntityTypeConfiguration<APLICACAO>
    {
        public WAC_APLICACAOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.APLICACAO_NM)
                .IsRequired()
                .HasMaxLength(20);

            Property(t => t.APLICACAO_DSC)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_APLICACAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.APLICACAO_NM).HasColumnName("APLICACAO_NM");
            Property(t => t.APLICACAO_DSC).HasColumnName("APLICACAO_DSC");
        }
    }
}