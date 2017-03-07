using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_CATEGORIA_CHConfiguration : EntityTypeConfiguration<FORNECEDOR_CATEGORIA_CH>
    {
        public WFD_PJPF_CATEGORIA_CHConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.DESCRICAO)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_PJPF_CATEGORIA_CH");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
        }
    }
}