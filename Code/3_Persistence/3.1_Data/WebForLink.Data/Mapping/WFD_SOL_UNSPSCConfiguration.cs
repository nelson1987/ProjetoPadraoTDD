using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOL_UNSPSCConfiguration : EntityTypeConfiguration<SOLICITACAO_UNSPSC>
    {
        public WFD_SOL_UNSPSCConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("WFL_SOL_UNSPSC");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.UNSPSC_ID).HasColumnName("UNSPSC_ID");

            // Relationships
            HasRequired(t => t.T_UNSPSC)
                .WithMany(t => t.WFD_SOL_UNSPSC)
                .HasForeignKey(d => d.UNSPSC_ID);
            HasRequired(t => t.T_UNSPSC1)
                .WithMany(t => t.WFD_SOL_UNSPSC1)
                .HasForeignKey(d => d.UNSPSC_ID);
            HasRequired(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_SOL_UNSPSC)
                .HasForeignKey(d => d.SOLICITACAO_ID);
        }
    }
}