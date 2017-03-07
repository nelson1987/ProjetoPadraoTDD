using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_GRUPOConfiguration : EntityTypeConfiguration<TIPO_GRUPO>
    {
        public WFD_T_GRUPOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.GRUPO_NM)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_GRUPO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.GRUPO_NM).HasColumnName("GRUPO_NM");
            Property(t => t.VISAO_ID).HasColumnName("VISAO_ID");

            // Relationships
            HasRequired(t => t.WFD_T_VISAO)
                .WithMany(t => t.WFD_T_GRUPO)
                .HasForeignKey(d => d.VISAO_ID);
        }
    }
}