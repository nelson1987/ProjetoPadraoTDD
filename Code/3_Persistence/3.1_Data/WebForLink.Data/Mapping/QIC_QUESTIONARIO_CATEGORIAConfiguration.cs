using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class QIC_QUESTIONARIO_CATEGORIAConfiguration : EntityTypeConfiguration<QUESTIONARIO_CATEGORIA>
    {
        public QIC_QUESTIONARIO_CATEGORIAConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("WFL_QUESTIONARIO_CATEGORIA");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.QUESTIONARIO_ID).HasColumnName("QUESTIONARIO_ID");
            Property(t => t.CATEGORIA_ID).HasColumnName("CATEGORIA_ID");

            // Relationships
            HasRequired(t => t.QIC_QUESTIONARIO)
                .WithMany(t => t.QIC_QUESTIONARIO_CATEGORIA)
                .HasForeignKey(d => d.QUESTIONARIO_ID);
            HasRequired(t => t.WFD_PJPF_CATEGORIA)
                .WithMany(t => t.QIC_QUESTIONARIO_CATEGORIA)
                .HasForeignKey(d => d.CATEGORIA_ID);
        }
    }
}