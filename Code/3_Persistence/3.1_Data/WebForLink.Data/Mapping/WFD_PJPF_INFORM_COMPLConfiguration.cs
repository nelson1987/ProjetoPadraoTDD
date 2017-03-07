using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_INFORM_COMPLConfiguration : EntityTypeConfiguration<FORNECEDOR_INFORM_COMPL>
    {
        public WFD_PJPF_INFORM_COMPLConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.RESPOSTA)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_PJPF_INFORM_COMPL");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_PJPF_ID).HasColumnName("CONTRATANTE_PJPF_ID");
            Property(t => t.PERG_ID).HasColumnName("PERG_ID");
            Property(t => t.RESPOSTA).HasColumnName("RESPOSTA");

            // Relationships
            HasRequired(t => t.QIC_QUEST_ABA_PERG)
                .WithMany(t => t.WFD_PJPF_INFORM_COMPL)
                .HasForeignKey(d => d.PERG_ID);
            HasRequired(t => t.WFD_CONTRATANTE_PJPF)
                .WithMany(t => t.WFD_PJPF_INFORM_COMPL)
                .HasForeignKey(d => d.CONTRATANTE_PJPF_ID);
        }
    }
}