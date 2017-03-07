using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_TP_PJPFConfiguration : EntityTypeConfiguration<TIPO_FORNECEDOR>
    {
        public WFD_T_TP_PJPFConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.TP_CONTRATANTE_NM)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_PJPF");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TP_CONTRATANTE_NM).HasColumnName("TP_CONTRATANTE_NM");
        }
    }
}