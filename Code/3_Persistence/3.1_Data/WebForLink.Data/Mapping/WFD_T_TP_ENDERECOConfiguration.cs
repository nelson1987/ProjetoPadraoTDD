using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_T_TP_ENDERECOConfiguration : EntityTypeConfiguration<TIPO_ENDERECO>
    {
        public WFD_T_TP_ENDERECOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.NM_TP_ENDERECO)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("UTIL_TIPO_ENDERECO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.NM_TP_ENDERECO).HasColumnName("NM_TP_ENDERECO");
        }
    }
}