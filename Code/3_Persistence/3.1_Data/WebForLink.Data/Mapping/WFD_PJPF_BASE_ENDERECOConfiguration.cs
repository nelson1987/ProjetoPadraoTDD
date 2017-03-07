using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_BASE_ENDERECOConfiguration : EntityTypeConfiguration<FORNECEDORBASE_ENDERECO>
    {
        public WFD_PJPF_BASE_ENDERECOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ENDERECO)
                .HasMaxLength(255);

            Property(t => t.NUMERO)
                .HasMaxLength(50);

            Property(t => t.COMPLEMENTO)
                .HasMaxLength(255);

            Property(t => t.CEP)
                .HasMaxLength(9);

            Property(t => t.BAIRRO)
                .HasMaxLength(100);

            Property(t => t.CIDADE)
                .HasMaxLength(100);

            Property(t => t.UF)
                .HasMaxLength(2);

            Property(t => t.PAIS)
                .HasMaxLength(100);

            // Table & Column Mappings
            ToTable("WFL_PRECADASTRO_ENDERECO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TP_ENDERECO_ID).HasColumnName("TP_ENDERECO_ID");
            Property(t => t.ENDERECO).HasColumnName("ENDERECO");
            Property(t => t.NUMERO).HasColumnName("NUMERO");
            Property(t => t.COMPLEMENTO).HasColumnName("COMPLEMENTO");
            Property(t => t.CEP).HasColumnName("CEP");
            Property(t => t.BAIRRO).HasColumnName("BAIRRO");
            Property(t => t.CIDADE).HasColumnName("CIDADE");
            Property(t => t.UF).HasColumnName("UF");
            Property(t => t.PAIS).HasColumnName("PAIS");
            Property(t => t.PJPF_BASE_ID).HasColumnName("PJPF_BASE_ID");

            // Relationships
            HasOptional(t => t.T_UF)
                .WithMany(t => t.WFD_PJPF_BASE_ENDERECO)
                .HasForeignKey(d => d.UF);
            HasRequired(t => t.WFD_PJPF_BASE)
                .WithMany(t => t.WFD_PJPF_BASE_ENDERECO)
                .HasForeignKey(d => d.PJPF_BASE_ID);
            HasRequired(t => t.WFD_T_TP_ENDERECO)
                .WithMany(t => t.WFD_PJPF_BASE_ENDERECO)
                .HasForeignKey(d => d.TP_ENDERECO_ID);
        }
    }
}