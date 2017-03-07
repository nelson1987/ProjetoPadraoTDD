using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_STATUSConfiguration : EntityTypeConfiguration<FORNECEDOR_STATUS>
    {
        public WFD_PJPF_STATUSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.STATUS_NM)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_PJPF_STATUS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.STATUS_NM).HasColumnName("STATUS_NM");
        }
    }
}