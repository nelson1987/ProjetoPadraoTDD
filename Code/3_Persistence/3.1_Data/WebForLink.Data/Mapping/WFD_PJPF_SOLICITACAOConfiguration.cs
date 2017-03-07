using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_SOLICITACAOConfiguration : EntityTypeConfiguration<FORNECEDOR_SOLICITACAO>
    {
        public WFD_PJPF_SOLICITACAOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ASSUNTO)
                .HasMaxLength(1024);

            Property(t => t.DESCRICAO_SOLICITACAO)
                .HasMaxLength(1024);

            // Table & Column Mappings
            ToTable("WFL_PJPF_SOLICITACAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.DATA_SOLICITACAO).HasColumnName("DATA_SOLICITACAO");
            Property(t => t.ASSUNTO).HasColumnName("ASSUNTO");
            Property(t => t.DESCRICAO_SOLICITACAO).HasColumnName("DESCRICAO_SOLICITACAO");
            Property(t => t.DT_NASCIMENTO).HasColumnName("DT_NASCIMENTO");

            // Relationships
            HasOptional(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_PJPF_SOLICITACAO)
                .HasForeignKey(d => d.CONTRATANTE_ID);
        }
    }
}