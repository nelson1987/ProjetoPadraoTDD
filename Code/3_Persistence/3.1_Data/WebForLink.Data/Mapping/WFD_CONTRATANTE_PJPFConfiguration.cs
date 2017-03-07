using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_CONTRATANTE_PJPFConfiguration : EntityTypeConfiguration<WFD_CONTRATANTE_PJPF>
    {
        public WFD_CONTRATANTE_PJPFConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.PJPF_COD_ERP)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_CONTRATANTE_PJPF");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.PJPF_ID).HasColumnName("PJPF_ID");
            Property(t => t.CATEGORIA_ID).HasColumnName("CATEGORIA_ID");
            Property(t => t.PJPF_COD_ERP).HasColumnName("PJPF_COD_ERP");
            Property(t => t.CRIA_DT).HasColumnName("CRIA_DT");
            Property(t => t.CRIA_USUARIO_ID).HasColumnName("CRIA_USUARIO_ID");
            Property(t => t.PJPF_STATUS_ID).HasColumnName("PJPF_STATUS_ID");
            Property(t => t.PJPF_STATUS_DT).HasColumnName("PJPF_STATUS_DT");
            Property(t => t.PJPF_STATUS_TP_SOL).HasColumnName("PJPF_STATUS_TP_SOL");
            Property(t => t.PJPF_STATUS_ID_SOL).HasColumnName("PJPF_STATUS_ID_SOL");
            Property(t => t.TP_PJPF).HasColumnName("TP_PJPF");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_CONTRATANTE_PJPF)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasRequired(t => t.WFD_PJPF)
                .WithMany(t => t.WFD_CONTRATANTE_PJPF)
                .HasForeignKey(d => d.PJPF_ID);
            HasOptional(t => t.WFD_PJPF_CATEGORIA)
                .WithMany(t => t.WFD_CONTRATANTE_PJPF)
                .HasForeignKey(d => d.CATEGORIA_ID);
            HasOptional(t => t.WFD_PJPF_STATUS)
                .WithMany(t => t.WFD_CONTRATANTE_PJPF)
                .HasForeignKey(d => d.PJPF_STATUS_ID);
            HasRequired(t => t.WFD_T_TP_PJPF)
                .WithMany(t => t.WFD_CONTRATANTE_PJPF)
                .HasForeignKey(d => d.TP_PJPF);
        }
    }
}