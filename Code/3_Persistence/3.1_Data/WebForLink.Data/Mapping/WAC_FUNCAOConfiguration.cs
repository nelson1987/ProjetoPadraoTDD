using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WAC_FUNCAOConfiguration : EntityTypeConfiguration<FUNCAO>
    {
        public WAC_FUNCAOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.CODIGO)
                .HasMaxLength(50);

            Property(t => t.FUNCAO_NM)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.FUNCAO_TELA)
                .HasMaxLength(50);

            Property(t => t.FUNCAO_DSC)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_FUNCAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CODIGO).HasColumnName("CODIGO");
            Property(t => t.APLICACAO_ID).HasColumnName("APLICACAO_ID");
            Property(t => t.FUNCAO_NM).HasColumnName("FUNCAO_NM");
            Property(t => t.FUNCAO_TELA).HasColumnName("FUNCAO_TELA");
            Property(t => t.FUNCAO_DSC).HasColumnName("FUNCAO_DSC");
            Property(t => t.FUNCAO_PAI).HasColumnName("FUNCAO_PAI");

            // Relationships
            HasMany(t => t.WAC_PERFIL)
                .WithMany(t => t.WAC_FUNCAO)
                .Map(m =>
                {
                    m.ToTable("WFL_PERFIL_FUNCAO");
                    m.MapLeftKey("FUNCAO_ID");
                    m.MapRightKey("PERFIL_ID");
                });

            HasMany(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WAC_FUNCAO)
                .Map(m =>
                {
                    m.ToTable("WFL_CONTRATANTE_FUNCAO");
                    m.MapLeftKey("FUNCAO_ID");
                    m.MapRightKey("CONTRATANTE_ID");
                });

            HasRequired(t => t.WAC_APLICACAO)
                .WithMany(t => t.WAC_FUNCAO)
                .HasForeignKey(d => d.APLICACAO_ID);
            HasOptional(t => t.FUNCAOPRINCIPAL)
                .WithMany(t => t.FUNCOES)
                .HasForeignKey(d => d.FUNCAO_PAI);
        }
    }
}