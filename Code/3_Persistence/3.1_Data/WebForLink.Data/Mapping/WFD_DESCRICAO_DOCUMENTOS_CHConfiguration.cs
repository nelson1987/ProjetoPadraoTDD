using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_DESCRICAO_DOCUMENTOS_CHConfiguration : EntityTypeConfiguration<WFD_DESCRICAO_DOCUMENTOS_CH>
    {
        public WFD_DESCRICAO_DOCUMENTOS_CHConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.DESCRICAO)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_DESCRICAO_DOCUMENTOS_CH");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TIPO_DOCUMENTOS_ID).HasColumnName("TIPO_DOCUMENTOS_ID");
            Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");

            // Relationships
            HasRequired(t => t.WFD_TIPO_DOCUMENTOS_CH)
                .WithMany(t => t.WFD_DESCRICAO_DOCUMENTOS_CH)
                .HasForeignKey(d => d.TIPO_DOCUMENTOS_ID);
        }
    }
}