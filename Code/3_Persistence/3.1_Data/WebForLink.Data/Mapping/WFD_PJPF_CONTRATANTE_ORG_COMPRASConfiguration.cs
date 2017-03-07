using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_CONTRATANTE_ORG_COMPRASConfiguration :
        EntityTypeConfiguration<WFD_PJPF_CONTRATANTE_ORG_COMPRAS>
    {
        public WFD_PJPF_CONTRATANTE_ORG_COMPRASConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("WFL_PJPF_CONTRATANTE_ORG_COMPRAS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRAT_ORG_COMPRAS_ID).HasColumnName("CONTRAT_ORG_COMPRAS_ID");
            Property(t => t.PJPF_ID).HasColumnName("PJPF_ID");
            Property(t => t.PJPF_CONTATO_ID).HasColumnName("PJPF_CONTATO_ID");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE_ORG_COMPRAS)
                .WithMany(t => t.WFD_PJPF_CONTRATANTE_ORG_COMPRAS)
                .HasForeignKey(d => d.CONTRAT_ORG_COMPRAS_ID);
            HasRequired(t => t.WFD_PJPF)
                .WithMany(t => t.WFD_PJPF_CONTRATANTE_ORG_COMPRAS)
                .HasForeignKey(d => d.PJPF_ID);
        }
    }
}