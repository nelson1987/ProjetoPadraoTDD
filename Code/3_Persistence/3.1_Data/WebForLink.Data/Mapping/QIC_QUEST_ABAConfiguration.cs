using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class QIC_QUEST_ABAConfiguration : EntityTypeConfiguration<QUESTIONARIO_ABA>
    {
        public QIC_QUEST_ABAConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ABA_NM)
                .HasMaxLength(50);

            Property(t => t.ABA_DSC)
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("WFL_QUESTIONARIO_ABA");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.ABA_NM).HasColumnName("ABA_NM");
            Property(t => t.QUESTIONARIO_ID).HasColumnName("QUESTIONARIO_ID");
            Property(t => t.ABA_DSC).HasColumnName("ABA_DSC");
            Property(t => t.ORDEM).HasColumnName("ORDEM");

            // Relationships
            HasRequired(t => t.QIC_QUESTIONARIO)
                .WithMany(t => t.QIC_QUEST_ABA)
                .HasForeignKey(d => d.QUESTIONARIO_ID);
        }
    }
}