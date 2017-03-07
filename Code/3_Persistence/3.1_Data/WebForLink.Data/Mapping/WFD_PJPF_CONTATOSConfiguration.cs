using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_CONTATOSConfiguration : EntityTypeConfiguration<FORNECEDOR_CONTATOS>
    {
        public WFD_PJPF_CONTATOSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME)
                .IsRequired()
                .HasMaxLength(30);

            Property(t => t.EMAIL)
                .IsRequired()
                .HasMaxLength(241);

            Property(t => t.TELEFONE)
                .HasMaxLength(33);

            Property(t => t.CELULAR)
                .HasMaxLength(30);

            // Table & Column Mappings
            ToTable("WFL_PJPF_CONTATO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRAT_ORG_COMPRAS_ID).HasColumnName("CONTRAT_ORG_COMPRAS_ID");
            Property(t => t.NOME).HasColumnName("NOME");
            Property(t => t.EMAIL).HasColumnName("EMAIL");
            Property(t => t.TELEFONE).HasColumnName("TELEFONE");
            Property(t => t.CELULAR).HasColumnName("CELULAR");
            Property(t => t.CONTRATANTE_PJPF_ID).HasColumnName("CONTRATANTE_PJPF_ID");
            Property(t => t.TP_CONTATO_ID).HasColumnName("TP_CONTATO_ID");

            // Relationships
            HasOptional(t => t.WFD_CONTRATANTE_PJPF)
                .WithMany(t => t.WFD_PJPF_CONTATOS)
                .HasForeignKey(d => d.CONTRATANTE_PJPF_ID);
            HasOptional(t => t.WFD_T_TP_CONTATO)
                .WithMany(t => t.WFD_PJPF_CONTATOS)
                .HasForeignKey(d => d.TP_CONTATO_ID);
        }
    }
}