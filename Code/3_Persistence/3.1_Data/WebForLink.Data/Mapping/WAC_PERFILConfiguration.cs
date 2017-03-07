using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WAC_PERFILConfiguration : EntityTypeConfiguration<Perfil>
    {
        public WAC_PERFILConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.PERFIL_NM)
                .IsRequired()
                .HasMaxLength(15);

            Property(t => t.PERFIL_DSC)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_PERFIL");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PERFIL_NM).HasColumnName("PERFIL_NM");
            Property(t => t.PERFIL_DSC).HasColumnName("PERFIL_DSC");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");

            // Relationships
            HasMany(t => t.WFD_USUARIO)
                .WithMany(t => t.WAC_PERFIL)
                .Map(m =>
                {
                    m.ToTable("WFL_USUARIO_PERFIL");
                    m.MapLeftKey("PERFIL_ID");
                    m.MapRightKey("USUARIO_ID");
                });

            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WAC_PERFIL)
                .HasForeignKey(d => d.CONTRATANTE_ID);
        }
    }
}