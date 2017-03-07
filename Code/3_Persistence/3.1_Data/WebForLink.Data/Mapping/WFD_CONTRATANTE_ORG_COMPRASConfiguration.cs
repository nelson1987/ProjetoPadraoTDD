using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_CONTRATANTE_ORG_COMPRASConfiguration : EntityTypeConfiguration<CONTRATANTE_ORGANIZACAO_COMPRAS>
    {
        public WFD_CONTRATANTE_ORG_COMPRASConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ORG_COMPRAS_COD)
                .IsRequired()
                .HasMaxLength(10);

            Property(t => t.ORG_COMPRAS_DSC)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_CONTRATANTE_ORG_COMPRAS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.ORG_COMPRAS_COD).HasColumnName("ORG_COMPRAS_COD");
            Property(t => t.ORG_COMPRAS_DSC).HasColumnName("ORG_COMPRAS_DSC");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_CONTRATANTE_ORG_COMPRAS)
                .HasForeignKey(d => d.CONTRATANTE_ID);
        }
    }
}