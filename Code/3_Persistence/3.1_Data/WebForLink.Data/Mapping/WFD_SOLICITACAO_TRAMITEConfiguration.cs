using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOLICITACAO_TRAMITEConfiguration : EntityTypeConfiguration<SOLICITACAO_TRAMITE>
    {
        public WFD_SOLICITACAO_TRAMITEConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.OBS)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_SOLICITACAO_TRAMITE");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.PAPEL_ID).HasColumnName("PAPEL_ID");
            Property(t => t.SOLICITACAO_STATUS_ID).HasColumnName("SOLICITACAO_STATUS_ID");
            Property(t => t.TRAMITE_DT_INI).HasColumnName("TRAMITE_DT_INI");
            Property(t => t.TRMITE_DT_FIM).HasColumnName("TRMITE_DT_FIM");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");
            Property(t => t.FLUXO_SEQ_EXC_ID).HasColumnName("FLUXO_SEQ_EXC_ID");
            Property(t => t.GRUPO_DESTINO).HasColumnName("GRUPO_DESTINO");
            Property(t => t.OBS).HasColumnName("OBS");

            // Relationships
            HasRequired(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_SOLICITACAO_TRAMITE)
                .HasForeignKey(d => d.SOLICITACAO_ID);
            HasRequired(t => t.WFD_SOLICITACAO_STATUS)
                .WithMany(t => t.WFD_SOLICITACAO_TRAMITE)
                .HasForeignKey(d => d.SOLICITACAO_STATUS_ID);
            HasOptional(t => t.WFD_USUARIO)
                .WithMany(t => t.WFD_SOLICITACAO_TRAMITE)
                .HasForeignKey(d => d.USUARIO_ID);
            HasRequired(t => t.Papel)
                .WithMany(t => t.WFD_SOLICITACAO_TRAMITE)
                .HasForeignKey(d => d.PAPEL_ID);
        }
    }
}