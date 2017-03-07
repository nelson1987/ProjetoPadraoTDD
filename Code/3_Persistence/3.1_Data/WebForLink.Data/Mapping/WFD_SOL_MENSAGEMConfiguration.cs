using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOL_MENSAGEMConfiguration : EntityTypeConfiguration<SOLICITACAO_MENSAGEM>
    {
        public WFD_SOL_MENSAGEMConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ASSUNTO)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_SOL_MENSAGEM");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.ASSUNTO).HasColumnName("ASSUNTO");
            Property(t => t.MENSAGEM).HasColumnName("MENSAGEM");
            Property(t => t.DT_ENVIO).HasColumnName("DT_ENVIO");

            // Relationships
            HasRequired(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_SOL_MENSAGEM)
                .HasForeignKey(d => d.SOLICITACAO_ID);
        }
    }
}