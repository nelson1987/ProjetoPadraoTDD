using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_CONTRATANTE_CONFIGConfiguration : EntityTypeConfiguration<CONTRATANTE_CONFIGURACAO>
    {
        public WFD_CONTRATANTE_CONFIGConfiguration()
        {
            // Primary Key
            HasKey(t => t.CONTRATANTE_ID);

            // Properties
            Property(t => t.CONTRATANTE_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.FORNECEDOR_CARGA)
                .HasMaxLength(50);

            Property(t => t.FORNECEDOR_RETORNO)
                .HasMaxLength(50);

            Property(t => t.CLIENTE_CARGA)
                .HasMaxLength(50);

            Property(t => t.CLIENTE_RETORNO)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_CONTRATANTE_CONFIG");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.SOLICITA_DOCS).HasColumnName("SOLICITA_DOCS");
            Property(t => t.SOLICITA_FICHA_CAD).HasColumnName("SOLICITA_FICHA_CAD");
            Property(t => t.LOGOTIPO).HasColumnName("LOGOTIPO");
            Property(t => t.TERMO_ACEITE).HasColumnName("TERMO_ACEITE");
            Property(t => t.ROBO_CICLO_ATU).HasColumnName("ROBO_CICLO_ATU");
            Property(t => t.ROBO_DT_PROX_EXEC).HasColumnName("ROBO_DT_PROX_EXEC");
            Property(t => t.BLOQUEIO_MANUAL).HasColumnName("BLOQUEIO_MANUAL");
            Property(t => t.BLOQUIEO_MANUAL_PRAZO).HasColumnName("BLOQUIEO_MANUAL_PRAZO");
            Property(t => t.TOTAL_TENTATIVA_ROBO).HasColumnName("TOTAL_TENTATIVA_ROBO");
            Property(t => t.NIVEIS_CATEGORIA).HasColumnName("NIVEIS_CATEGORIA");
            Property(t => t.QTD_ROBO_SIMULTANEA).HasColumnName("QTD_ROBO_SIMULTANEA");
            Property(t => t.PRAZO_ENTREGA_FICHA).HasColumnName("PRAZO_ENTREGA_FICHA");
            Property(t => t.FORNECEDOR_CARGA).HasColumnName("FORNECEDOR_CARGA");
            Property(t => t.FORNECEDOR_RETORNO).HasColumnName("FORNECEDOR_RETORNO");
            Property(t => t.CLIENTE_CARGA).HasColumnName("CLIENTE_CARGA");
            Property(t => t.CLIENTE_RETORNO).HasColumnName("CLIENTE_RETORNO");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithOptional(t => t.WFD_CONTRATANTE_CONFIG);
        }
    }
}