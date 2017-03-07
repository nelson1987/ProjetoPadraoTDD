using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_TIPO_DOCUMENTOS_CHConfiguration : EntityTypeConfiguration<TIPO_DOCUMENTOS_CH>
    {
        public WFD_TIPO_DOCUMENTOS_CHConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.DESCRICAO)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_DOCUMENTOS_CH");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
        }
    }
}