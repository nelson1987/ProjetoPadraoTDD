using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_BASE_CONTATOSConfiguration : EntityTypeConfiguration<FORNECEDORBASE_CONTATOS>
    {
        public WFD_PJPF_BASE_CONTATOSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME)
                .HasMaxLength(30);

            Property(t => t.EMAIL)
                .IsRequired()
                .HasMaxLength(241);

            Property(t => t.TELEFONE)
                .HasMaxLength(33);

            Property(t => t.CELULAR)
                .HasMaxLength(30);

            // Table & Column Mappings
            ToTable("WFL_PRECADASTRO_CONTATO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.PJPF_BASE_ID).HasColumnName("PJPF_BASE_ID");
            Property(t => t.NOME).HasColumnName("NOME");
            Property(t => t.EMAIL).HasColumnName("EMAIL");
            Property(t => t.TELEFONE).HasColumnName("TELEFONE");
            Property(t => t.CELULAR).HasColumnName("CELULAR");

            // Relationships
            HasRequired(t => t.WFD_PJPF_BASE)
                .WithMany(t => t.WFD_PJPF_BASE_CONTATOS)
                .HasForeignKey(d => d.PJPF_BASE_ID);
        }
    }
}