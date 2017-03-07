using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class QIC_QUEST_ABA_PERG_PAPELConfiguration : EntityTypeConfiguration<QUESTIONARIO_PAPEL>
    {
        public QIC_QUEST_ABA_PERG_PAPELConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("WFL_QUESTIONARIO_ABA_PERGUNTA_PAPEL");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PERG_ID).HasColumnName("PERG_ID");
            Property(t => t.PAPEL_ID).HasColumnName("PAPEL_ID");
            Property(t => t.LEITURA).HasColumnName("LEITURA");
            Property(t => t.ESCRITA).HasColumnName("ESCRITA");
            Property(t => t.OBRIG).HasColumnName("OBRIG");

            // Relationships
            HasRequired(t => t.QIC_QUEST_ABA_PERG)
                .WithMany(t => t.QIC_QUEST_ABA_PERG_PAPEL)
                .HasForeignKey(d => d.PERG_ID);
            HasRequired(t => t.Papel)
                .WithMany(t => t.QIC_QUEST_ABA_PERG_PAPEL)
                .HasForeignKey(d => d.PAPEL_ID);
        }
    }
}