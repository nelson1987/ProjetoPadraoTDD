using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOL_DESBLOQConfiguration : EntityTypeConfiguration<SOLICITACAO_DESBLOQUEIO>
    {
        public WFD_SOL_DESBLOQConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.BLQ_MOTIVO_DSC)
                .IsRequired()
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_SOL_DESBLOQ");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.BLQ_LANCAMENTO_EMP).HasColumnName("BLQ_LANCAMENTO_EMP");
            Property(t => t.BLQ_LANCAMENTO_TODAS_EMP).HasColumnName("BLQ_LANCAMENTO_TODAS_EMP");
            Property(t => t.BLQ_COMPRAS_TODAS_ORG_COMPRAS).HasColumnName("BLQ_COMPRAS_TODAS_ORG_COMPRAS");
            Property(t => t.BLQ_QUALIDADE_FUNCAO_BQL_ID).HasColumnName("BLQ_QUALIDADE_FUNCAO_BQL_ID");
            Property(t => t.BLQ_MOTIVO_DSC).HasColumnName("BLQ_MOTIVO_DSC");

            // Relationships
            HasRequired(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_SOL_DESBLOQ)
                .HasForeignKey(d => d.SOLICITACAO_ID);
            HasOptional(t => t.WFD_T_FUNCAO_BLOQUEIO)
                .WithMany(t => t.WFD_SOL_DESBLOQ)
                .HasForeignKey(d => d.BLQ_QUALIDADE_FUNCAO_BQL_ID);
        }
    }
}