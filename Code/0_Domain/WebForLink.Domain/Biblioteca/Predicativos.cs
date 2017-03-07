using LinqKit;
using System;
using System.Linq;
using System.Linq.Expressions;
using WebForDocs.Business.Infrastructure.Enum;
using WebForDocs.Controllers;
using WebForDocs.Dominio.Models;
using WebForDocs.ViewModels;

namespace WebForDocs.Biblioteca
{
    public static class Predicativos
    {
        public static Expression<Func<WFD_CONTRATANTE_PJPF, bool>> FiltrarFornecedoresPesquisaGrid(PesquisaFornecedoresVM modelo, int grupoId)
        {
            var filtro = PredicateBuilder.True<WFD_CONTRATANTE_PJPF>();
            //TODOS OS ativos que tenham TIPOPJPF = 2 ou ativos que pertence ao grupoId do Contratante
            filtro = filtro.And(p => p.WFD_PJPF.ATIVO == true);
            filtro = filtro.And(p => p.TP_PJPF == 2);
            filtro = filtro.And(p => p.WFD_CONTRATANTE.WFD_GRUPO.Any(g => g.ID == grupoId));

            if (modelo.CategoriaSelecionada != null)
                filtro = filtro.And(f => f.CATEGORIA_ID == modelo.CategoriaSelecionada);
            if (!string.IsNullOrEmpty(modelo.Fornecedor))
                filtro = filtro.And(c => c.WFD_PJPF.RAZAO_SOCIAL.Contains(modelo.Fornecedor));
            if (!string.IsNullOrEmpty(modelo.CNPJ))
                filtro = filtro.And(c => c.WFD_PJPF.CNPJ == modelo.CNPJ);

            return filtro;
        }
        public static Expression<Func<WFD_DESTINATARIO, bool>> FiltrarDestinatarioGrid(DestinatariosPesquisaVM modelo, int contratanteId)
        {
            var filtro = PredicateBuilder.True<WFD_DESTINATARIO>();
            filtro = filtro.And(d => d.CONTRATANTE_ID == contratanteId);
            if (!string.IsNullOrWhiteSpace(modelo.Nome))
                filtro = filtro.And(d => d.NOME.Contains(modelo.Nome));
            if (!string.IsNullOrWhiteSpace(modelo.Email))
                filtro = filtro.And(d => d.EMAIL.Contains(modelo.Email));
            if (!string.IsNullOrWhiteSpace(modelo.Empresa))
                filtro = filtro.And(d => d.EMPRESA.Contains(modelo.Empresa));
            return filtro;
        }
        public static Expression<Func<WFD_SOLICITACAO, bool>> FiltrarAcompanhamentoGrid(AcompanhamentoPesquisaVM modelo, int contratanteId)
        {
            var filtro = PredicateBuilder.True<WFD_SOLICITACAO>();
            filtro = filtro.And(d => d.WFD_CONTRATANTE.WFD_GRUPO.Any(g => g.ID == modelo.GrupoId));
            if (modelo.Pendentes != null)
                filtro = filtro.And(x => x.SOLICITACAO_STATUS_ID != 4);
            if (modelo.TipoSolicitacao != 0)
                filtro = filtro.And(x => x.FLUXO_ID == modelo.TipoSolicitacao);
            if (modelo.CodigoSolicitacao != 0)
                filtro = filtro.And(x => x.ID == modelo.CodigoSolicitacao);
            if (!string.IsNullOrEmpty(modelo.Cnpj))
                filtro = filtro.And(x => (x.WFD_SOL_CAD_PJPF.Any(z => z.CNPJ == modelo.Cnpj || x.WFD_PJPF.CNPJ == modelo.Cnpj)));
            if (!string.IsNullOrEmpty(modelo.Cpf))
                filtro = filtro.And(x => (x.WFD_SOL_CAD_PJPF.Any(z => z.CPF == modelo.Cpf || x.WFD_PJPF.CPF == modelo.Cpf)));
            if (!string.IsNullOrEmpty(modelo.RazaoSocial))
                filtro = filtro.And(x => x.WFD_SOL_CAD_PJPF.Any(z => (z.RAZAO_SOCIAL.Contains(modelo.RazaoSocial) || z.NOME.Contains(modelo.RazaoSocial))) || x.WFD_PJPF.RAZAO_SOCIAL.Contains(modelo.RazaoSocial) || x.WFD_PJPF.NOME.Contains(modelo.RazaoSocial));
            return filtro;
        }
        public static Expression<Func<MEU_COMPARTILHAMENTOS, bool>> FiltrarMeuCompartilhamentosGrid(EnviadosPesquisaVM modelo, int contratanteId)
        {
            var predicate = PredicateBuilder.True<MEU_COMPARTILHAMENTOS>();
            predicate = predicate.And(c => c.CONTRATANTE_ID == contratanteId);
            if (!string.IsNullOrEmpty(modelo.DataEnvioEntre))
            {
                string[] vDatas = modelo.DataEnvioEntre.Split(new char[] { '-' });
                DateTime dtIni, dtFim;
                DateTime.TryParse(vDatas[0].Trim(), out dtIni);
                DateTime.TryParse(vDatas[1].Trim(), out dtFim);

                predicate = predicate.And(c => c.ENVIADO_EM >= dtIni && c.ENVIADO_EM <= dtFim);
            }
            if (modelo.TipoDocumento != null)
            {
                predicate = predicate.And(c => c.MEU_DOCUMENTOS_COMPARTILHADOS
                    .Any(d => d.WFD_PJPF_DOCUMENTOS.WFD_DESCRICAO_DOCUMENTOS.TIPO_DOCUMENTOS_ID == (int)modelo.TipoDocumento));
            }
            if (modelo.DescricaoDocumento != null)
            {
                predicate = predicate.And(c => c.MEU_DOCUMENTOS_COMPARTILHADOS
                    .Any(d => d.WFD_PJPF_DOCUMENTOS.DESCRICAO_DOCUMENTO_ID == (int)modelo.DescricaoDocumento));
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