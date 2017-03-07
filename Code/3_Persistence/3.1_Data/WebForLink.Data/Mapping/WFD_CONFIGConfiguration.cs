using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_CONFIGConfiguration : EntityTypeConfiguration<CONFIGURACAO>
    {
        public WFD_CONFIGConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.ROBO_IMPORTACAO)
                .IsRequired()
                .HasMaxLength(9);

            Property(t => t.ROBO_GOVERNANCA)
                .IsRequired()
                .HasMaxLength(9);

            Property(t => t.CHAVE_CRIPTO)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.CHAVE_WEBSERVICE)
                .IsRequired()
                .HasMaxLength(50);

            Property(t => t.CAMINHO_ARQUIVOS)
                .HasMaxLength(255);

            // Table & Column Mappings
            ToTable("WFL_CONFIGURACAO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.ROBO_IMPORTACAO).HasColumnName("ROBO_IMPORTACAO");
            Property(t => t.ROBO_GOVERNANCA).HasColumnName("ROBO_GOVERNANCA");
            Property(t => t.QTD_ACESSO_ROBO_SIMULTANEO).HasColumnName("QTD_ACESSO_ROBO_SIMULTANEO");
            Property(t => t.CHAVE_CRIPTO).HasColumnName("CHAVE_CRIPTO");
            Property(t => t.CHAVE_WEBSERVICE).HasColumnName("CHAVE_WEBSERVICE");
            Property(t => t.CAMINHO_ARQUIVOS).HasColumnName("CAMINHO_ARQUIVOS");
        }
    }
}