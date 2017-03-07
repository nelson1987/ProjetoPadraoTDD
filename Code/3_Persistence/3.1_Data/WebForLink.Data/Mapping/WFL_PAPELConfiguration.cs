using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFL_PAPELConfiguration : EntityTypeConfiguration<Papel>
    {
        public WFL_PAPELConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.PAPEL_SGL)
                .IsRequired()
                .HasMaxLength(3);

            Property(t => t.PAPEL_NM)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_PAPEL");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.PAPEL_SGL).HasColumnName("PAPEL_SGL");
            Property(t => t.PAPEL_NM).HasColumnName("PAPEL_NM");
            Property(t => t.PAPEL_TP_ID).HasColumnName("PAPEL_TP_ID");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFL_PAPEL)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasOptional(t => t.TipoDePapel)
                .WithMany(t => t.WFL_PAPEL)
                .HasForeignKey(d => d.PAPEL_TP_ID);
        }
    }
}