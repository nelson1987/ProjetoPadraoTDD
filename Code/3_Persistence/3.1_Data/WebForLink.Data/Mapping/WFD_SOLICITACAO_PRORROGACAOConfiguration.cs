using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOLICITACAO_PRORROGACAOConfiguration : EntityTypeConfiguration<SOLICITACAO_PRORROGACAO>
    {
        public WFD_SOLICITACAO_PRORROGACAOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.MOTIVO_PRORROGACAO)
                .HasMaxLength(2000);

            Property(t => t.MOTIVO_REPROVACAO)
                .HasMaxLength(2000);

            // Table & Column Mappings
            ToTable("WFL_SOLICITACAO_PRORROGACAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.DT_PRORROGACAO_PRAZO).HasColumnName("DT_PRORROGACAO_PRAZO");
            Property(t => t.MOTIVO_PRORROGACAO).HasColumnName("MOTIVO_PRORROGACAO");
            Property(t => t.DT_SOL_PRORROGACAO).HasColumnName("DT_SOL_PRORROGACAO");
            Property(t => t.USUARIO_SOL_ID).HasColumnName("USUARIO_SOL_ID");
            Property(t => t.APROVADO).HasColumnName("APROVADO");
            Property(t => t.MOTIVO_REPROVACAO).HasColumnName("MOTIVO_REPROVACAO");
            Property(t => t.DT_AVALIACAO).HasColumnName("DT_AVALIACAO");
            Property(t => t.USUARIO_AVALIACAO_ID).HasColumnName("USUARIO_AVALIACAO_ID");

            // Relationships
            HasOptional(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_SOLICITACAO_PRORROGACAO)
                .HasForeignKey(d => d.SOLICITACAO_ID);
            HasRequired(t => t.WFD_USUARIO)
                .WithMany(t => t.WFD_SOLICITACAO_PRORROGACAO)
                .HasForeignKey(d => d.USUARIO_SOL_ID);
        }
    }
}