using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class MEU_COMPARTILHAMENTOSConfiguration : EntityTypeConfiguration<Compartilhamentos>
    {
        public MEU_COMPARTILHAMENTOSConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.ASSUNTO)
                .HasMaxLength(1024);

            Property(t => t.CHAVE)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_COMPARTILHAMENTO");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.ENVIADO_EM).HasColumnName("ENVIADO_EM");
            Property(t => t.ASSUNTO).HasColumnName("ASSUNTO");
            Property(t => t.MENSAGEM).HasColumnName("MENSAGEM");
            Property(t => t.SEM_PRAZO).HasColumnName("SEM_PRAZO");
            Property(t => t.VALIDADE).HasColumnName("VALIDADE");
            Property(t => t.CHAVE).HasColumnName("CHAVE");
            Property(t => t.RESTRITA).HasColumnName("RESTRITA");
            Property(t => t.USUARIO_ID).HasColumnName("USUARIO_ID");
            Property(t => t.FICHA_CADASTRAL).HasColumnName("FICHA_CADASTRAL");

            // Relationships
            HasMany(t => t.WFD_PJPF_BANCO)
                .WithMany(t => t.MEU_COMPARTILHAMENTOS)
                .Map(m =>
                {
                    m.ToTable("WFL_COMPARTILHAMENTO_BANCO");
                    m.MapLeftKey("COMPARTILHAMENTO_ID");
                    m.MapRightKey("PJPF_BANCO_ID");
                });

            HasMany(t => t.WFD_PJPF_CONTATOS)
                .WithMany(t => t.MEU_COMPARTILHAMENTOS)
                .Map(m =>
                {
                    m.ToTable("WFL_COMPARTILHAMENTO_CONTATO");
                    m.MapLeftKey("COMPARTILHAMENTO_ID");
                    m.MapRightKey("PJPF_CONTATO_ID");
                });

            HasMany(t => t.WFD_DESTINATARIO)
                .WithMany(t => t.MEU_COMPARTILHAMENTOS)
                .Map(m =>
                {
                    m.ToTable("WFL_COMPARTILHAMENTO_DESTINATARIO");
                    m.MapLeftKey("COMPARTILHAMENTO_ID");
                    m.MapRightKey("DESTINATARIO_ID");
                });

            HasOptional(t => t.WFD_USUARIO)
                .WithMany(t => t.MEU_COMPARTILHAMENTOS)
                .HasForeignKey(d => d.USUARIO_ID);
            HasOptional(t => t.Contratante)
                .WithMany(t => t.MEU_COMPARTILHAMENTOS)
                .HasForeignKey(d => d.CONTRATANTE_ID);
        }
    }
}