using System.Data.Entity.ModelConfiguration;
using WebForLink.Domain.Entities;
using WebForLink.Domain.Entities.Tipos;

namespace WebForLink.Win.Banco
{
    public class SolicitacaoCadastroMap : EntityTypeConfiguration<SolicitacaoCadastro>
    {
        public SolicitacaoCadastroMap()
        {
            ToTable("WFL_SOLICITACAO_CADASTRO");
            Property(t => t.Id).HasColumnName("SOLICITACAO_ID");
            Property(t => t.PrazoDias).HasColumnName("PRAZO");
            //Map(m => m.Requires("TIPO_SOLICITACAO").HasValue(1));
        }
    }
}
