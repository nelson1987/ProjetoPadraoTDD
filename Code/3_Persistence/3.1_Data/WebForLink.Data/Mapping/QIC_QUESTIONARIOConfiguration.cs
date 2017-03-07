using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class QIC_QUESTIONARIOConfiguration : EntityTypeConfiguration<QUESTIONARIO>
    {
        public QIC_QUESTIONARIOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.QUEST_NM)
                .HasMaxLength(50);

            Property(t => t.QUEST_DSC)
                .HasMaxLength(500);

            // Table & Column Mappings
            ToTable("WFL_QUESTIONARIO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.QUEST_NM).HasColumnName("QUEST_NM");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.QUEST_DSC).HasColumnName("QUEST_DSC");
            Property(t => t.LE_D_BANCARIO).HasColumnName("LE_D_BANCARIO");
            Property(t => t.LE_D_CONTATO).HasColumnName("LE_D_CONTATO");
            Property(t => t.LE_D_GERAIS).HasColumnName("LE_D_GERAIS");
            Property(t => t.LE_INFO_COMPL).HasColumnName("LE_INFO_COMPL");

            // Relationships
            HasOptional(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.QIC_QUESTIONARIO)
                .HasForeignKey(d => d.CONTRATANTE_ID);
        }
    }
}