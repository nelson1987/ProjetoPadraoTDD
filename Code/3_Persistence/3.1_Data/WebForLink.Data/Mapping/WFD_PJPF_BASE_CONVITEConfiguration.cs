using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_BASE_CONVITEConfiguration : EntityTypeConfiguration<FORNECEDORBASE_CONVITE>
    {
        public WFD_PJPF_BASE_CONVITEConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("WFL_PRECADASTRO_CONVITE");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PJPF_BASE_ID).HasColumnName("PJPF_BASE_ID");
            Property(t => t.DT_ENVIO).HasColumnName("DT_ENVIO");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");

            // Relationships
            HasRequired(t => t.WFD_PJPF_BASE)
                .WithMany(t => t.WFD_PJPF_BASE_CONVITE)
                .HasForeignKey(d => d.PJPF_BASE_ID);
            HasOptional(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_PJPF_BASE_CONVITE)
                .HasForeignKey(d => d.SOLICITACAO_ID);
            HasRequired(t => t.WFD_USUARIO)
                .WithMany(t => t.WFD_PJPF_BASE_CONVITE)
                .HasForeignKey(d => d.USUARIO_ID);
        }
    }
}