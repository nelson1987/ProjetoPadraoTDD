using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class QIC_QUEST_ABA_PERGConfiguration : EntityTypeConfiguration<QUESTIONARIO_PERGUNTA>
    {
        public QIC_QUEST_ABA_PERGConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.PERG_NM)
                .HasMaxLength(50);

            Property(t => t.TP_DADO)
                .HasMaxLength(10);

            Property(t => t.EXIBE_NM)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_QUESTIONARIO_ABA_PERGUNTA");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.QUEST_ABA_ID).HasColumnName("QUEST_ABA_ID");
            Property(t => t.PERG_NM).HasColumnName("PERG_NM");
            Property(t => t.TP_DADO).HasColumnName("TP_DADO");
            Property(t => t.EXIBE_NM).HasColumnName("EXIBE_NM");
            Property(t => t.DOMINIO).HasColumnName("DOMINIO");
            Property(t => t.ORDEM).HasColumnName("ORDEM");
            Property(t => t.RESP_TAMANHO).HasColumnName("RESP_TAMANHO");
            Property(t => t.E_PAI).HasColumnName("E_PAI");
            Property(t => t.PERG_PAI).HasColumnName("PERG_PAI");

            // Relationships
            HasRequired(t => t.QIC_QUEST_ABA)
                .WithMany(t => t.QIC_QUEST_ABA_PERG)
                .HasForeignKey(d => d.QUEST_ABA_ID);
        }
    }
}