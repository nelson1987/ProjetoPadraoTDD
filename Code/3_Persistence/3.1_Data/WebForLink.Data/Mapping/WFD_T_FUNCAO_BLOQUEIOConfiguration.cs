using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_FUNCAO_BLOQUEIOConfiguration : EntityTypeConfiguration<TIPO_FUNCAO_BLOQUEIO>
    {
        public WFD_T_FUNCAO_BLOQUEIOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.FUNCAO_BLOQ_COD)
                .IsRequired()
                .HasMaxLength(2);

            Property(t => t.FUNCAO_BLOQ_DSC)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_BLOQUEIO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.FUNCAO_BLOQ_COD).HasColumnName("FUNCAO_BLOQ_COD");
            Property(t => t.FUNCAO_BLOQ_DSC).HasColumnName("FUNCAO_BLOQ_DSC");
        }
    }
}