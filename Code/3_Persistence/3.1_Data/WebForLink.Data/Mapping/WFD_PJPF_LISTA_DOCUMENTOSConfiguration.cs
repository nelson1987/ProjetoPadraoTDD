using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_PJPF_LISTA_DOCUMENTOSConfiguration : EntityTypeConfiguration<ListaDeDocumentosDeFornecedor>
    {
        public WFD_PJPF_LISTA_DOCUMENTOSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            // Table & Column Mappings
            ToTable("WFL_PJPF_LISTA_DOCUMENTOS");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.DESCRICAO_DOCUMENTO_ID).HasColumnName("DESCRICAO_DOCUMENTO_ID");
            Property(t => t.EXIGE_VALIDADE).HasColumnName("EXIGE_VALIDADE");
            Property(t => t.PERIODICIDADE_ID).HasColumnName("PERIODICIDADE_ID");
            Property(t => t.ATIVO).HasColumnName("ATIVO");
            Property(t => t.OBRIGATORIO).HasColumnName("OBRIGATORIO");

            // Relationships
            HasRequired(t => t.DescricaoDeDocumentos)
                .WithMany(t => t.ListaDeDocumentosDeFornecedor)
                .HasForeignKey(d => d.DESCRICAO_DOCUMENTO_ID);
            HasOptional(t => t.WFD_T_PERIODICIDADE)
                .WithMany(t => t.WFD_PJPF_LISTA_DOCUMENTOS)
                .HasForeignKey(d => d.PERIODICIDADE_ID);
        }
    }
}