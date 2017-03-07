using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_CATEGORIAConfiguration : EntityTypeConfiguration<FORNECEDOR_CATEGORIA>
    {
        public WFD_PJPF_CATEGORIAConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.CODIGO)
                .HasMaxLength(10);

            Property(t => t.DESCRICAO)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_PJPF_CATEGORIA");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CATEGORIA_PAI_ID).HasColumnName("CATEGORIA_PAI_ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.CODIGO).HasColumnName("CODIGO");
            Property(t => t.DESCRICAO).HasColumnName("DESCRICAO");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.PJPF_CATEGORIA_CH_ID).HasColumnName("PJPF_CATEGORIA_CH_ID");
            Property(t => t.ISENTO_DOCUMENTOS).HasColumnName("ISENTO_DOCUMENTOS");
            Property(t => t.ISENTO_DADOSBANCARIOS).HasColumnName("ISENTO_DADOSBANCARIOS");
            Property(t => t.ISENTO_CONTATOS).HasColumnName("ISENTO_CONTATOS");

            // Relationships
            HasMany(t => t.ListaDeDocumentosDeFornecedor)
                .WithMany(t => t.WFD_PJPF_CATEGORIA)
                .Map(m =>
                {
                    m.ToTable("WFL_PJPF_CATEGORIA_DOCUMENTO");
                    m.MapLeftKey("CATEGORIA_ID");
                    m.MapRightKey("LISTA_DOCUMENTOS_ID");
                });

            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFD_PJPF_CATEGORIA)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasOptional(t => t.WFD_PJPF_CATEGORIA2)
                .WithMany(t => t.WFD_PJPF_CATEGORIA1)
                .HasForeignKey(d => d.CATEGORIA_PAI_ID);
            HasOptional(t => t.WFD_PJPF_CATEGORIA_CH)
                .WithMany(t => t.WFD_PJPF_CATEGORIA)
                .HasForeignKey(d => d.PJPF_CATEGORIA_CH_ID);
        }
    }
}