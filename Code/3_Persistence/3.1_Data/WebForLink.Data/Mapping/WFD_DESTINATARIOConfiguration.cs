using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_DESTINATARIOConfiguration : EntityTypeConfiguration<DESTINATARIO>
    {
        public WFD_DESTINATARIOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME)
                .HasMaxLength(255);

            Property(t => t.EMAIL)
                .HasMaxLength(255);

            Property(t => t.OBS)
                .HasMaxLength(2000);

            Property(t => t.EMPRESA)
                .HasMaxLength(150);

            Property(t => t.SOBRENOME)
                .HasMaxLength(255);

            Property(t => t.TELEFONE_FIXO)
                .HasMaxLength(33);

            Property(t => t.CELULAR)
                .HasMaxLength(30);

            Property(t => t.TELEFONE_TRABALHO)
                .HasMaxLength(33);

            Property(t => t.FAX)
                .HasMaxLength(33);

            // Table & Column Mappings
            ToTable("WFL_DESTINATARIO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.NOME).HasColumnName("NOME");
            Property(t => t.EMAIL).HasColumnName("EMAIL");
            Property(t => t.OBS).HasColumnName("OBS");
            Property(t => t.VALIDADE).HasColumnName("VALIDADE");
            Property(t => t.EMPRESA).HasColumnName("EMPRESA");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.EMAIL_AVULSO).HasColumnName("EMAIL_AVULSO");
            Property(t => t.SOBRENOME).HasColumnName("SOBRENOME");
            Property(t => t.TELEFONE_FIXO).HasColumnName("TELEFONE_FIXO");
            Property(t => t.CELULAR).HasColumnName("CELULAR");
            Property(t => t.TELEFONE_TRABALHO).HasColumnName("TELEFONE_TRABALHO");
            Property(t => t.FAX).HasColumnName("FAX");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_DESTINATARIO)
                .HasForeignKey(d => d.CONTRATANTE_ID);
        }
    }
}