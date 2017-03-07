using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_UNSPSCConfiguration : EntityTypeConfiguration<FORNECEDOR_UNSPSC>
    {
        public WFD_PJPF_UNSPSCConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("WFL_PJPF_UNSPSC");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.PJPF_ID).HasColumnName("PJPF_ID");
            Property(t => t.UNSPSC_ID).HasColumnName("UNSPSC_ID");
            Property(t => t.DT_INCLUSAO).HasColumnName("DT_INCLUSAO");
            Property(t => t.DT_EXCLUSAO).HasColumnName("DT_EXCLUSAO");

            // Relationships
            HasOptional(t => t.T_UNSPSC)
                .WithMany(t => t.WFD_PJPF_UNSPSC)
                .HasForeignKey(d => d.UNSPSC_ID);
            HasOptional(t => t.WFD_PJPF)
                .WithMany(t => t.FornecedorServicoMaterialList)
                .HasForeignKey(d => d.PJPF_ID);
        }
    }
}