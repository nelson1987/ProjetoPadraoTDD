using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_ARQUIVOSConfiguration : EntityTypeConfiguration<ARQUIVOS>
    {
        public WFD_ARQUIVOSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME_ARQUIVO)
                .IsRequired()
                .HasMaxLength(255);

            Property(t => t.TIPO_ARQUIVO)
                .IsRequired()
                .HasMaxLength(100);

            Property(t => t.CAMINHO)
                .IsRequired()
                .HasMaxLength(1000);

            // Table & Column Mappings
            ToTable("WFL_ARQUIVOS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.NOME_ARQUIVO).HasColumnName("NOME_ARQUIVO");
            Property(t => t.TIPO_ARQUIVO).HasColumnName("TIPO_ARQUIVO");
            Property(t => t.DATA_UPLOAD).HasColumnName("DATA_UPLOAD");
            Property(t => t.TAMANHO).HasColumnName("TAMANHO");
            Property(t => t.CAMINHO).HasColumnName("CAMINHO");
        }
    }
}