using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_CONTRATANTEConfiguration : EntityTypeConfiguration<Contratante>
    {
        public WFD_CONTRATANTEConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.CNPJ)
                .HasMaxLength(20);

            Property(t => t.RAZAO_SOCIAL)
                .HasMaxLength(255);

            Property(t => t.NOME_FANTASIA)
                .HasMaxLength(255);

            Property(t => t.EXTENSAO_IMAGEM)
                .HasMaxLength(500);

            Property(t => t.ESTILO)
                .IsRequired()
                .HasMaxLength(50);

            //Property(t => t.CONTRANTE_COD_ERP)
            //    .HasMaxLength(10);

            Property(t => t.COD_WEBFORMAT)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_CONTRATANTE");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.TIPO_CADASTRO_ID).HasColumnName("TIPO_CADASTRO_ID");
            Property(t => t.CNPJ).HasColumnName("CNPJ");
            Property(t => t.RAZAO_SOCIAL).HasColumnName("RAZAO_SOCIAL");
            Property(t => t.NOME_FANTASIA).HasColumnName("NOME_FANTASIA");
            Property(t => t.DATA_CADASTRO).HasColumnName("DATA_CADASTRO");
            Property(t => t.LOGO_FOTO).HasColumnName("LOGO_FOTO");
            Property(t => t.EXTENSAO_IMAGEM).HasColumnName("EXTENSAO_IMAGEM");
            Property(t => t.ESTILO).HasColumnName("ESTILO");
            //Property(t => t.CONTRANTE_COD_ERP).HasColumnName("CONTRANTE_COD_ERP");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.ATIVO_DT).HasColumnName("ATIVO_DT");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");
            Property(t => t.COD_WEBFORMAT).HasColumnName("COD_WEBFORMAT");
            Property(t => t.TIPO_CONTRATANTE_ID).HasColumnName("TIPO_CONTRATANTE_ID");
            Property(t => t.DATA_NASCIMENTO).HasColumnName("DATA_NASCIMENTO");
            Ignore(x => x.SolicitacoesComRoboExecutado);

            // Relationships
            HasMany(t => t.WFD_GRUPO)
                .WithMany(t => t.WFD_CONTRATANTE)
                .Map(m =>
                {
                    m.ToTable("WFL_GRUPO_CONTRATANTE");
                    m.MapLeftKey("CONTRATANTE_ID");
                    m.MapRightKey("GRUPO_ID");
                });

            HasMany(t => t.WFD_USUARIO1)
                .WithMany(t => t.WFD_CONTRATANTE1)
                .Map(m =>
                {
                    m.ToTable("WFL_USUARIO_CONTRATANTE");
                    m.MapLeftKey("CONTRATANTE_ID");
                    m.MapRightKey("USUARIO_ID");
                });

            HasOptional(t => t.WFD_TIPO_CADASTRO)
                .WithMany(t => t.WFD_CONTRATANTE)
                .HasForeignKey(d => d.TIPO_CADASTRO_ID);
            HasOptional(t => t.WFD_TIPO_CONTRATANTE)
                .WithMany(t => t.WFD_CONTRATANTE)
                .HasForeignKey(d => d.TIPO_CONTRATANTE_ID);
        }
    }
}