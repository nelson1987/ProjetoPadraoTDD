using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_DESCRICAOConfiguration : EntityTypeConfiguration<TIPO_DESCRICAO>
    {
        public WFD_T_DESCRICAOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.DESCRICAO_NM)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_DESCRICAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.DESCRICAO_NM).HasColumnName("DESCRICAO_NM");
            Property(t => t.GRUPO_ID).HasColumnName("GRUPO_ID");

            // Relationships
            HasRequired(t => t.WFD_T_GRUPO)
                .WithMany(t => t.WFD_T_DESCRICAO)
                .HasForeignKey(d => d.GRUPO_ID);
        }
    }
}