using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_USUARIO_SENHAS_HISTConfiguration : EntityTypeConfiguration<USUARIO_SENHAS>
    {
        public WFD_USUARIO_SENHAS_HISTConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.SENHA)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_USUARIO_HISTORICO_SENHA");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");
            Property(t => t.SENHA).HasColumnName("SENHA");
            Property(t => t.SENHA_DT).HasColumnName("SENHA_DT");

            // Relationships
            HasRequired(t => t.WFD_USUARIO)
                .WithMany(t => t.WFD_USUARIO_SENHAS_HIST)
                .HasForeignKey(d => d.USUARIO_ID);
        }
    }
}