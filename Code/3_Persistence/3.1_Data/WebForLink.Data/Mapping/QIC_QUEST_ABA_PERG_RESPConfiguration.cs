using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class QIC_QUEST_ABA_PERG_RESPConfiguration : EntityTypeConfiguration<QUESTIONARIO_RESPOSTA>
    {
        public QIC_QUEST_ABA_PERG_RESPConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.RESP_COD)
                .HasMaxLength(50);

            Property(t => t.RESP_DSC)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_QUESTIONARIO_ABA_PERGUNTA_RESPOSTA");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PERG_ID).HasColumnName("PERG_ID");
            Property(t => t.RESP_PAI_ID).HasColumnName("RESP_PAI_ID");
            Property(t => t.RESP_COD).HasColumnName("RESP_COD");
            Property(t => t.RESP_DSC).HasColumnName("RESP_DSC");
            Property(t => t.ORDEM).HasColumnName("ORDEM");

            // Relationships
            HasRequired(t => t.QIC_QUEST_ABA_PERG)
                .WithMany(t => t.QIC_QUEST_ABA_PERG_RESP)
                .HasForeignKey(d => d.PERG_ID);
            HasOptional(t => t.QIC_QUEST_ABA_PERG_RESP2)
                .WithMany(t => t.QIC_QUEST_ABA_PERG_RESP1)
                .HasForeignKey(d => d.RESP_PAI_ID);
        }
    }
}