using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_CONTRATANTE_CONFIG_EMAILConfiguration : EntityTypeConfiguration<CONTRATANTE_CONFIGURACAO_EMAIL>
    {
        public WFD_CONTRATANTE_CONFIG_EMAILConfiguration()
        {
            // Primary Key
            HasKey(t => new {t.CONTRATANTE_ID, t.EMAIL_TP_ID});

            // Properties
            Property(t => t.CONTRATANTE_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.EMAIL_TP_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ASSUNTO)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_CONTRATANTE_CONFIG_EMAIL");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.EMAIL_TP_ID).HasColumnName("EMAIL_TP_ID");
            Property(t => t.ASSUNTO).HasColumnName("ASSUNTO");
            Property(t => t.CORPO).HasColumnName("CORPO");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_CONTRATANTE_CONFIG_EMAIL)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasRequired(t => t.WFD_T_TP_EMAIL)
                .WithMany(t => t.WFD_CONTRATANTE_CONFIG_EMAIL)
                .HasForeignKey(d => d.EMAIL_TP_ID);
        }
    }
}