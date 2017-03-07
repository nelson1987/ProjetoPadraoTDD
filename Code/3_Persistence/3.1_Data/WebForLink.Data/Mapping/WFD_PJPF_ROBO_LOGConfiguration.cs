using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_ROBO_LOGConfiguration : EntityTypeConfiguration<ROBO_LOG>
    {
        public WFD_PJPF_ROBO_LOGConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ROBO)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_PJPF_ROBO_LOG");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.DATA).HasColumnName("DATA");
            Property(t => t.ROBO).HasColumnName("ROBO");
            Property(t => t.COD_RETORNO).HasColumnName("COD_RETORNO");
            Property(t => t.MENSAGEM).HasColumnName("MENSAGEM");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.PJPF_BASE_ID).HasColumnName("PJPF_BASE_ID");
            Property(t => t.PJPF_ID).HasColumnName("PJPF_ID");

            // Relationships
            HasOptional(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_PJPF_ROBO_LOG)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasOptional(t => t.WFD_PJPF)
                .WithMany(t => t.ROBO_LOG)
                .HasForeignKey(d => d.PJPF_ID);
            HasOptional(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_PJPF_ROBO_LOG)
                .HasForeignKey(d => d.SOLICITACAO_ID);
        }
    }
}