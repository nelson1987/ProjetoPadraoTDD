using LinqKit;
using System;
using System.Linq;
using System.Linq.Expressions;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.Fornecedores;

namespace WebForLink.Web.Infrastructure
{
    public static class Predicativos
    {
        public static Expression<Func<Fornecedor, bool>> FiltrarFornecedoresPesquisaGrid(PesquisaFornecedorVM modelo, int grupoId, int contratanteId)
        {
            var filtro = PredicateBuilder.New<Fornecedor>();
            //TODOS OS ativos que tenham TIPOPJPF = 2 ou ativos que pertence ao grupoId do Contratante
            //filtro = filtro.And(p => p.Fornecedor.ATIVO == true);
            //filtro = filtro.And(p => p.TP_PJPF == 2);
            filtro = filtro.And(p => p.Contratante.WFD_GRUPO.Any(g => g.ID == grupoId));

            if (modelo.Filtro.CategoriaSelecionada != null)
                filtro = filtro.And(f => f.WFD_CONTRATANTE_PJPF.FirstOrDefault(x=>x.CONTRATANTE_ID == contratanteId).CATEGORIA_ID == modelo.Filtro.CategoriaSelecionada);
            if (!string.IsNullOrEmpty(modelo.Filtro.Fornecedor))
                filtro = filtro.And(c => c.RAZAO_SOCIAL.ToUpper().Contains(modelo.Filtro.Fornecedor.ToUpper()));
            if (!string.IsNullOrEmpty(modelo.Filtro.CNPJ))
                filtro = filtro.And(c => c.CNPJ == modelo.Filtro.CNPJ);

            return filtro;
        }
        public static Expression<Func<DESTINATARIO, bool>> FiltrarDestinatarioGrid(DestinatariosPesquisaVM modelo, int contratanteId)
        {
            var filtro = PredicateBuilder.New<DESTINATARIO>();
            filtro = AtivosEPorContratante(contratanteId, filtro);
            if (!string.IsNullOrWhiteSpace(modelo.Nome))
                filtro = filtro.And(d => d.NOME != null && d.NOME.ToUpper().Contains(modelo.Nome.ToUpper()));
            if (!string.IsNullOrWhiteSpace(modelo.Email))
                filtro = filtro.And(d => d.EMAIL.ToUpper().Contains(modelo.Email.ToUpper()));
            if (modelo.Empresa != null)
                filtro = filtro.And(d => d.EMPRESA != null && d.EMPRESA.ToUpper().Contains(modelo.Empresa.ToUpper()));
            return filtro;
        }

        private static ExpressionStarter<DESTINATARIO> AtivosEPorContratante(int contratanteId, ExpressionStarter<DESTINATARIO> filtro)
        {
            filtro = filtro.And(d => d.CONTRATANTE_ID == contratanteId);
            filtro = filtro.And(d => d.ATIVO);
            return filtro;
        }

        public static Expression<Func<SOLICITACAO, bool>> FiltrarAcompanhamentoGrid(AcompanhamentoPesquisaVM modelo, int contratanteId)
        {
            var filtro = PredicateBuilder.New<SOLICITACAO>();
            filtro = filtro.And(d => d.Contratante.WFD_GRUPO.Any(g => g.ID == modelo.GrupoId));
            if (modelo.Pendentes != null)
                filtro = filtro.And(x => x.SOLICITACAO_STATUS_ID != 4);
            if (modelo.TipoSolicitacao != 0)
                filtro = filtro.And(x => x.FLUXO_ID == modelo.TipoSolicitacao);
            if (modelo.CodigoSolicitacao != 0)
                filtro = filtro.And(x => x.ID == modelo.CodigoSolicitacao);
            if (!string.IsNullOrEmpty(modelo.Cnpj))
                filtro = filtro.And(x => (x.SolicitacaoCadastroFornecedor.Any(z => z.CNPJ == modelo.Cnpj || x.Fornecedor.CNPJ == modelo.Cnpj)));
            if (!string.IsNullOrEmpty(modelo.Cpf))
                filtro = filtro.And(x => (x.SolicitacaoCadastroFornecedor.Any(z => z.CPF == modelo.Cpf || x.Fornecedor.CPF == modelo.Cpf)));
            if (!string.IsNullOrEmpty(modelo.RazaoSocial))
                filtro = filtro.And(x => x.SolicitacaoCadastroFornecedor.Any(z => (z.RAZAO_SOCIAL.Contains(modelo.RazaoSocial) || z.NOME.Contains(modelo.RazaoSocial))) || x.Fornecedor.RAZAO_SOCIAL.Contains(modelo.RazaoSocial) || x.Fornecedor.NOME.Contains(modelo.RazaoSocial));
            return filtro;
        }
        public static Expression<Func<Compartilhamentos, bool>> FiltrarMeuCompartilhamentosGrid(EnviadosPesquisaVM modelo, int contratanteId)
        {
            var predicate = PredicateBuilder.New<Compartilhamentos>();
            predicate = predicate.And(c => c.CONTRATANTE_ID == contratanteId);
            predicate = predicate.And(c => c.WFD_DESTINATARIO.Any());
            if (!string.IsNullOrEmpty(modelo.DataEnvioEntre))
            {
                string[] vDatas = modelo.DataEnvioEntre.Split(new char[] { '-' });
                DateTime dtIni, dtFim;
                DateTime.TryParse(vDatas[0].Trim(), out dtIni);
                DateTime.TryParse(vDatas[1].Trim(), out dtFim);
                dtFim = dtFim.AddDays(1).AddSeconds(-1);
                predicate = predicate.And(c => c.ENVIADO_EM >= dtIni && c.ENVIADO_EM <= dtFim);
            }
            if (modelo.TipoDocumento != null)
            {
                predicate = predicate.And(c => c.DocumentosCompartilhados
                    .Any(d => d.DocumentosDoFornecedor.DescricaoDeDocumentos.TIPO_DOCUMENTOS_ID == (int)modelo.TipoDocumento));
            }
            if (modelo.DescricaoDocumento != null)
            {
                predicate = predicate.And(c => c.DocumentosCompartilhados
                    .Any(d => d.DocumentosDoFornecedor.DESCRICAO_DOCUMENTO_ID == (int)modelo.DescricaoDocumento));
            }
            if (!string.IsNullOrEmpty(modelo.Destinatario))
            {
                predicate = predicate.And(c => c.WFD_DESTINATARIO
                    .Any(d => d.NOME.Contains(modelo.Destinatario) || d.EMAIL.Contains(modelo.Destinatario)));
            }
            if (!string.IsNullOrEmpty(modelo.Fornecedor))
            {
                // FORNECEDOR
            }
            return predicate;
        }
    }
}