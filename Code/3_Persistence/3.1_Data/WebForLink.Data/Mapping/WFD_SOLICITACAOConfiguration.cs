using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOLICITACAOConfiguration : EntityTypeConfiguration<SOLICITACAO>
    {
        public WFD_SOLICITACAOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.MOTIVO)
                .HasMaxLength(255);

            Property(t => t.TP_PJPF)
                .HasMaxLength(15);

            Property(t => t.MOTIVO_PRORROGACAO)
                .HasMaxLength(2000);

            // Table & Column Mappings
            ToTable("WFL_SOLICITACAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.FLUXO_ID).HasColumnName("FLUXO_ID");
            Property(t => t.SOLICITACAO_DT_CRIA).HasColumnName("SOLICITACAO_DT_CRIA");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");
            Property(t => t.SOLICITACAO_STATUS_ID).HasColumnName("SOLICITACAO_STATUS_ID");
            Property(t => t.MOTIVO).HasColumnName("MOTIVO");
            Property(t => t.PJPF_ID).HasColumnName("PJPF_ID");
            Property(t => t.TP_PJPF).HasColumnName("TP_PJPF");
            Property(t => t.DT_PRAZO).HasColumnName("DT_PRAZO");
            Property(t => t.DT_PRORROGACAO_PRAZO).HasColumnName("DT_PRORROGACAO_PRAZO");
            Property(t => t.MOTIVO_PRORROGACAO).HasColumnName("MOTIVO_PRORROGACAO");
            Property(t => t.PJPF_BASE_ID).HasColumnName("PJPF_BASE_ID");
            Property(t => t.ROBO_EXECUTADO).HasColumnName("ROBO_EXECUTADO");
            Property(t => t.ROBO_TENTATIVAS_EXCEDIDAS).HasColumnName("ROBO_TENTATIVAS_EXCEDIDAS");

            // Relationships
            HasRequired(t => t.Contratante)
                .WithMany(t => t.WFD_SOLICITACAO)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasOptional(t => t.Fornecedor)
                .WithMany(t => t.SolicitacaoList)
                .HasForeignKey(d => d.PJPF_ID);
            HasOptional(t => t.FORNECEDORBASE)
                .WithMany(t => t.WFD_SOLICITACAO)
                .HasForeignKey(d => d.PJPF_BASE_ID);
            HasOptional(t => t.WFD_SOLICITACAO_STATUS)
                .WithMany(t => t.WFD_SOLICITACAO)
                .HasForeignKey(d => d.SOLICITACAO_STATUS_ID);
            HasOptional(t => t.Usuario)
                .WithMany(t => t.WFD_SOLICITACAO)
                .HasForeignKey(d => d.USUARIO_ID);
            HasRequired(t => t.Fluxo)
                .WithMany(t => t.WFD_SOLICITACAO)
                .HasForeignKey(d => d.FLUXO_ID);
        }
    }
}