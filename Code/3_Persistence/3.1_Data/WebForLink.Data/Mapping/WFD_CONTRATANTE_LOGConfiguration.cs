using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_CONTRATANTE_LOGConfiguration : EntityTypeConfiguration<CONTRATANTE_LOG>
    {
        public WFD_CONTRATANTE_LOGConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("WFL_CONTRATANTE_LOG");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.ATIVO_DT).HasColumnName("ATIVO_DT");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_CONTRATANTE_LOG)
                .HasForeignKey(d => d.CONTRATANTE_ID);
        }
    }
}