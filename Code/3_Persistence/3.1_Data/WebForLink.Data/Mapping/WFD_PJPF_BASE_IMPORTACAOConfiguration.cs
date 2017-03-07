using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_BASE_IMPORTACAOConfiguration : EntityTypeConfiguration<FORNECEDORBASE_IMPORTACAO>
    {
        public WFD_PJPF_BASE_IMPORTACAOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME_ARQUIVO)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_PRECADASTRO_IMPORTACAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.NOME_ARQUIVO).HasColumnName("NOME_ARQUIVO");
            Property(t => t.DT_UPLOAD).HasColumnName("DT_UPLOAD");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_PJPF_BASE_IMPORTACAO)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasRequired(t => t.WFD_USUARIO)
                .WithMany(t => t.WFD_PJPF_BASE_IMPORTACAO)
                .HasForeignKey(d => d.USUARIO_ID);
        }
    }
}