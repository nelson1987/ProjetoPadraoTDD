using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFL_FLUXO_SEQ_PRE_REQUISConfiguration : EntityTypeConfiguration<FLUXO_SEQUENCIA_PRE_REQUIS>
    {
        public WFL_FLUXO_SEQ_PRE_REQUISConfiguration()
        {
            // Primary Key
            HasKey(t => t.FLUXO_SEQ_ID);

            // Properties
            Property(t => t.FLUXO_SEQ_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("WFL_FLUXO_SEQ_PRE_REQUIS");
            Property(t => t.FLUXO_SEQ_ID).HasColumnName("FLUXO_SEQ_ID");
            Property(t => t.PAPEL_ID).HasColumnName("PAPEL_ID");

            // Relationships
            HasRequired(t => t.Papel)
                .WithMany(t => t.WFL_FLUXO_SEQ_PRE_REQUIS)
                .HasForeignKey(d => d.PAPEL_ID);
        }
    }
}