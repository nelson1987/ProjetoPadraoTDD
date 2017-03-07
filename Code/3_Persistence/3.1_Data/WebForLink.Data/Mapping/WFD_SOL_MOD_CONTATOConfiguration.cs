using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOL_MOD_CONTATOConfiguration : EntityTypeConfiguration<SolicitacaoModificacaoDadosContato>
    {
        public WFD_SOL_MOD_CONTATOConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.NOME)
                .HasMaxLength(30);

            Property(t => t.EMAIL)
                .IsRequired()
                .HasMaxLength(241);

            Property(t => t.TELEFONE)
                .HasMaxLength(33);

            Property(t => t.CELULAR)
                .HasMaxLength(30);

            // Table & Column Mappings
            ToTable("WFL_SOL_MOD_CONTATO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");
            Property(t => t.NOME).HasColumnName("NOME");
            Property(t => t.EMAIL).HasColumnName("EMAIL");
            Property(t => t.TELEFONE).HasColumnName("TELEFONE");
            Property(t => t.CELULAR).HasColumnName("CELULAR");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.PJPF_ID).HasColumnName("PJPF_ID");
            Property(t => t.CONTATO_PJPF_ID).HasColumnName("CONTATO_PJPF_ID");
            Property(t => t.TP_CONTATO_ID).HasColumnName("TP_CONTATO_ID");

            // Relationships
            HasOptional(t => t.WFD_PJPF_CONTATOS)
                .WithMany(t => t.WFD_SOL_MOD_CONTATO)
                .HasForeignKey(d => d.CONTATO_PJPF_ID);
            HasRequired(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.SolicitacaoModificacaoDadosContato)
                .HasForeignKey(d => d.SOLICITACAO_ID);
            HasOptional(t => t.WFD_T_TP_CONTATO)
                .WithMany(t => t.WFD_SOL_MOD_CONTATO)
                .HasForeignKey(d => d.TP_CONTATO_ID);
        }
    }
}