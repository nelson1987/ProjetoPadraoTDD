using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.WebForLink;

namespace WebForLink.Data.Mapping
{
    public class WFD_SOL_MOD_DGERAIS_SEQConfiguration : EntityTypeConfiguration<SOLICITACAO_MODIFICACAO_DADOSGERAIS>
    {
        public WFD_SOL_MOD_DGERAIS_SEQConfiguration()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.DESCRICAOALTERACAO)
                .IsRequired();

            // Table & Column Mappings
            ToTable("WFL_SOL_MOD_DGERAIS_SEQ");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.VISAO_ID).HasColumnName("VISAO_ID");
            Property(t => t.GRUPO_ID).HasColumnName("GRUPO_ID");
            Property(t => t.DESCRICAO_ID).HasColumnName("DESCRICAO_ID");
            Property(t => t.DESCRICAOALTERACAO).HasColumnName("DESCRICAOALTERACAO");
            Property(t => t.SOLICITACAO_ID).HasColumnName("SOLICITACAO_ID");

            // Relationships
            HasOptional(t => t.WFD_SOLICITACAO)
                .WithMany(t => t.WFD_SOL_MOD_DGERAIS_SEQ)
                .HasForeignKey(d => d.SOLICITACAO_ID);
            HasRequired(t => t.WFD_T_DESCRICAO)
                .WithMany(t => t.WFD_SOL_MOD_DGERAIS_SEQ)
                .HasForeignKey(d => d.DESCRICAO_ID);
            HasRequired(t => t.WFD_T_GRUPO)
                .WithMany(t => t.WFD_SOL_MOD_DGERAIS_SEQ)
                .HasForeignKey(d => d.GRUPO_ID);
            HasRequired(t => t.WFD_T_VISAO)
                .WithMany(t => t.WFD_SOL_MOD_DGERAIS_SEQ)
                .HasForeignKey(d => d.VISAO_ID);
        }
    }
}