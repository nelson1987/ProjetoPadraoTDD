using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_BASE_UNSPSCConfiguration : EntityTypeConfiguration<FORNECEDORBASE_UNSPSC>
    {
        public WFD_PJPF_BASE_UNSPSCConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("WFL_PRECADASTRO_UNSPSC");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PJPF_BASE_ID).HasColumnName("PJPF_BASE_ID");
            Property(t => t.UNSPSC_ID).HasColumnName("UNSPSC_ID");
            Property(t => t.DT_INCLUSAO).HasColumnName("DT_INCLUSAO");
            Property(t => t.DT_EXCLUSAO).HasColumnName("DT_EXCLUSAO");

            // Relationships
            HasOptional(t => t.T_UNSPSC)
                .WithMany(t => t.WFD_PJPF_BASE_UNSPSC)
                .HasForeignKey(d => d.UNSPSC_ID);
            HasRequired(t => t.WFD_PJPF_BASE)
                .WithMany(t => t.WFD_PJPF_BASE_UNSPSC)
                .HasForeignKey(d => d.PJPF_BASE_ID);
        }
    }
}