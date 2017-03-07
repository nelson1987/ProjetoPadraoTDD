using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_INFORM_COMPLConfiguration : EntityTypeConfiguration<WFD_INFORM_COMPL>
    {
        public WFD_INFORM_COMPLConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.RESPOSTA)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_INFORM_COMPL");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.PERG_ID).HasColumnName("PERG_ID");
            Property(t => t.RESPOSTA).HasColumnName("RESPOSTA");

            // Relationships
            HasRequired(t => t.QIC_QUEST_ABA_PERG)
                .WithMany(t => t.WFD_INFORM_COMPL)
                .HasForeignKey(d => d.PERG_ID);
            HasRequired(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_INFORM_COMPL)
                .HasForeignKey(d => d.SOLICITACAO_ID);
        }
    }
}