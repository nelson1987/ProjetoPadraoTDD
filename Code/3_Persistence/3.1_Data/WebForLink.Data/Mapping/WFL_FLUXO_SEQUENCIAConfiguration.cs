using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFL_FLUXO_SEQUENCIAConfiguration : EntityTypeConfiguration<FLUXO_SEQUENCIA>
    {
        public WFL_FLUXO_SEQUENCIAConfiguration()
        {
            // Primary Key
            HasKey(t => new {t.CONTRATANTE_ID, t.FLUXO_ID, t.SEQUENCIA});

            // Properties
            Property(t => t.CONTRATANTE_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.FLUXO_ID)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.SEQUENCIA)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.FLUXO_ETAPA_NM)
                .HasMaxLength(30);

            Property(t => t.FLUXO_ETAPA_DSC)
                .HasMaxLength(100);

            Property(t => t.FLUXO_SEQ_ANTERIOR)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("WFL_FLUXO_SEQUENCIA");
            Property(t => t.CONTRATANTE_ID).HasColumnName("CONTRATANTE_ID");
            Property(t => t.FLUXO_ID).HasColumnName("FLUXO_ID");
            Property(t => t.SEQUENCIA).HasColumnName("SEQUENCIA");
            Property(t => t.PAPEL_ID_INI).HasColumnName("PAPEL_ID_INI");
            Property(t => t.PAPEL_ID_FIM).HasColumnName("PAPEL_ID_FIM");
            Property(t => t.FLUXO_ETAPA_NM).HasColumnName("FLUXO_ETAPA_NM");
            Property(t => t.FLUXO_ETAPA_DSC).HasColumnName("FLUXO_ETAPA_DSC");
            Property(t => t.FLUXO_SEQ_ANTERIOR).HasColumnName("FLUXO_SEQ_ANTERIOR");
            Property(t => t.GRUPO_ORIGEM).HasColumnName("GRUPO_ORIGEM");
            Property(t => t.GRUPO_DESTINO).HasColumnName("GRUPO_DESTINO");
            Property(t => t.EXECUCAO_MANUAL).HasColumnName("EXECUCAO_MANUAL");
            Property(t => t.APROV_SEM_ROBO).HasColumnName("APROV_SEM_ROBO");
            Property(t => t.BLOQ_INATIVO_RECEITA).HasColumnName("BLOQ_INATIVO_RECEITA");

            // Relationships
            HasRequired(t => t.WFD_CONTRATANTE)
                .WithMany(t => t.WFL_FLUXO_SEQUENCIA)
                .HasForeignKey(d => d.CONTRATANTE_ID);
            HasRequired(t => t.WFL_FLUXO)
                .WithMany(t => t.WFL_FLUXO_SEQUENCIA)
                .HasForeignKey(d => d.FLUXO_ID);
            HasRequired(t => t.Papel)
                .WithMany(t => t.WFL_FLUXO_SEQUENCIA)
                .HasForeignKey(d => d.PAPEL_ID_INI);
            HasOptional(t => t.Papel1)
                .WithMany(t => t.WFL_FLUXO_SEQUENCIA1)
                .HasForeignKey(d => d.PAPEL_ID_FIM);
        }
    }
}