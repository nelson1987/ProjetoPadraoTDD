using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Win.Banco
{
    public class SolicitacaoModificacaoBancoMap : EntityTypeConfiguration<SolicitacaoModificacaoBanco>
    {
        public SolicitacaoModificacaoBancoMap()
        {
            ToTable("WFL_SOLICITACAO_MODIFICACAO_BANCO");
            Property(t => t.Id).HasColumnName("SOLICITACAO_ID");
            Property(t => t.PrazoDias).HasColumnName("PRAZO");
            HasMany(t => t.Bancos)
                .WithMany(x => x.SolicitacaoModificacaoBanco)
                .Map(m =>
                {
                    m.ToTable("WFL_BANCO_SOLICITACAO_MODIFICACAO_BANCO");
                    m.MapLeftKey("SOLICITACAO_ID");
                    m.MapRightKey("BANCO_ID");
                });
        }
    }
}
