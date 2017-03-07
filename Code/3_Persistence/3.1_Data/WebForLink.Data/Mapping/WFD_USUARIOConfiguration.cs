using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_USUARIOConfiguration : EntityTypeConfiguration<Usuario>
    {
        public WFD_USUARIOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME)
                .HasMaxLength(255);

            Property(t => t.EMAIL)
                .IsRequired()
                .HasMaxLength(255);

            Property(t => t.EMAIL_ALTERNATIVO)
                .HasMaxLength(255);

            Property(t => t.SENHA)
                .HasMaxLength(255);

            Property(t => t.TROCAR_SENHA)
                .HasMaxLength(50);

            Property(t => t.CPF_CNPJ)
                .HasMaxLength(14);

            Property(t => t.CARGO)
                .HasMaxLength(255);

            Property(t => t.FIXO)
                .HasMaxLength(20);

            Property(t => t.CELULAR)
                .HasMaxLength(20);

            Property(t => t.LOGIN)
                .IsRequired()
                .HasMaxLength(255);

            Property(t => t.LOGIN_SSO)
                .HasMaxLength(255);

            Property(t => t.DOMINIO)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_USUARIO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.NOME).HasColumnName("NOME");
            Property(t => t.EMAIL).HasColumnName("EMAIL");
            Property(t => t.EMAIL_ALTERNATIVO).HasColumnName("EMAIL_ALTERNATIVO");
            Property(t => t.SENHA).HasColumnName("SENHA");
            Property(t => t.DAT_NASCIMENTO).HasColumnName("DAT_NASCIMENTO");
            Property(t => t.PRINCIPAL).HasColumnName("PRINCIPAL");
            Property(t => t.TROCAR_SENHA).HasColumnName("TROCAR_SENHA");
            Property(t => t.CPF_CNPJ).HasColumnName("CPF_CNPJ");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.CARGO).HasColumnName("CARGO");
            Property(t => t.FIXO).HasColumnName("FIXO");
            Property(t => t.CELULAR).HasColumnName("CELULAR");
            Property(t => t.LOGIN).HasColumnName("LOGIN");
            Property(t => t.LOGIN_SSO).HasColumnName("LOGIN_SSO");
            Property(t => t.DOMINIO).HasColumnName("DOMINIO");
            Property(t => t.PRIMEIRO_ACESSO).HasColumnName("PRIMEIRO_ACESSO");
            Property(t => t.DT_ATIVACAO).HasColumnName("DT_ATIVACAO");
            Property(t => t.DT_CRIACAO).HasColumnName("DT_CRIACAO");
            Property(t => t.CONTA_TENTATIVA).HasColumnName("CONTA_TENTATIVA");
            Property(t => t.EXPIRA_EM_DIAS).HasColumnName("EXPIRA_EM_DIAS");

            // Relationships
            HasMany(t => t.WFL_PAPEL)
                .WithMany(t => t.WFD_USUARIO)
                .Map(m =>
                {
                    m.ToTable("WFL_USUARIO_PAPEL");
                    m.MapLeftKey("USUARIO_ID");
                    m.MapRightKey("PAPEL_ID");
                });

            HasOptional(t => t.Contratante)
                .WithMany(t => t.Usuario)
                .HasForeignKey(d => d.CONTRATANTE_ID);
        }
    }
}