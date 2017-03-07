using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WAC_ACESSO_LOGConfiguration : EntityTypeConfiguration<WAC_ACESSO_LOG>
    {
        public WAC_ACESSO_LOGConfiguration()
        {
            // Primary Key
            HasKey(t => new {t.USUARIO_ID, t.DATA});

            // Properties
            Property(t => t.USUARIO_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.IP)
                .HasMaxLength(100);

            Property(t => t.NAVEGADOR)
                .HasMaxLength(30);

            // Table & Column Mappings
            ToTable("WFL_ACESSO_LOG");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");
            Property(t => t.DATA).HasColumnName("DATA");
            Property(t => t.IP).HasColumnName("IP");
            Property(t => t.NAVEGADOR).HasColumnName("NAVEGADOR");

            // Relationships
            HasRequired(t => t.WFD_USUARIO)
                .WithMany(t => t.WAC_ACESSO_LOG)
                .HasForeignKey(d => d.USUARIO_ID);
        }
    }
}