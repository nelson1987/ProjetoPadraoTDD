using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class MEU_DOCUMENTOS_COMPARTILHADOSConfiguration : EntityTypeConfiguration<DocumentosCompartilhados>
    {
        public MEU_DOCUMENTOS_COMPARTILHADOSConfiguration()
        {
            // Primary Key
            HasKey(t => new {t.CONTRATANTE_ID, t.PJPF_DOCUMENTO_ID, t.COMPARTILHAMENTO_ID});

            // Properties
            Property(t => t.CONTRATANTE_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.PJPF_DOCUMENTO_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.COMPARTILHAMENTO_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            ToTable("WFL_COMP_DOCUMENTO");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.PJPF_DOCUMENTO_ID).HasColumnName("PJPF_DOCUMENTO_ID");
            Property(t => t.COMPARTILHAMENTO_ID).HasColumnName("COMPARTILHAMENTO_ID");

            // Relationships
            HasRequired(t => t.Compartilhamentos)
                .WithMany(t => t.DocumentosCompartilhados)
                .HasForeignKey(d => d.COMPARTILHAMENTO_ID);
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.MEU_DOCUMENTOS_COMPARTILHADOS)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasRequired(t => t.DocumentosDoFornecedor)
                .WithMany(t => t.MEU_DOCUMENTOS_COMPARTILHADOS)
                .HasForeignKey(d => d.PJPF_DOCUMENTO_ID);
        }
    }
}