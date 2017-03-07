using AutoMapper;
using System;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;
using WebForLink.Web.ViewModels;
using WebForLink.Web.ViewModels.FichaCadastral;

namespace WebForLink.Web.Mappers
{
    public class ContatoProfile : Profile
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }
        public ContatoProfile()
        {

        //}
        //protected override void Configure()
        //{
            try
            {
                CreateMap<FORNECEDORBASE_CONTATOS, MeusDocumentosPesquisarEmailGridVM>()
                    .ForMember(destino => destino.Id, origem => origem.MapFrom(x => x.ID))
                    .ForMember(destino => destino.Email, origem => origem.MapFrom(x => x.EMAIL))
                    .ForMember(destino => destino.Origem, origem => origem.MapFrom(x => TipoEmailVM.EmailAvulso))
                    .AfterMap((src, dest) => dest.Nome = !string.IsNullOrEmpty(src.NOME) ? src.NOME.Replace(",", "") + " (" + src.EMAIL + ")" : src.EMAIL)
                    .AfterMap((src, dest) => dest.Empresa = !string.IsNullOrEmpty(src.WFD_PJPF_BASE.RAZAO_SOCIAL) ? src.WFD_PJPF_BASE.RAZAO_SOCIAL : src.WFD_PJPF_BASE.NOME_FANTASIA);

                CreateMap<FORNECEDORBASE_CONTATOS, EmailsVM>()
                    .ForMember(destino => destino.value, origem => origem.MapFrom(x => TipoEmailVM.EmailAvulso + ":" + x.ID.ToString() + ":" + x.EMAIL))
                    .ForMember(destino => destino.text, origem => origem.MapFrom(x => (!string.IsNullOrEmpty(x.NOME) ? x.NOME.Replace(",", "") + " (" + x.EMAIL + ")" : x.EMAIL)));

                CreateMap<FORNECEDORBASE_CONTATOS, FornecedorContatosVM>()
                    .ForMember(dest => dest.ID, ori => ori.MapFrom(x => x.ID))
                    .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME))
                    .ForMember(dest => dest.Email, ori => ori.MapFrom(x => x.EMAIL));

                CreateMap<FORNECEDORBASE_CONTATOS, BaseContatosVM>()
                    .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                    .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.WFD_PJPF_BASE.ID))
                    .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NOME));

                CreateMap<FORNECEDORBASE_CONTATOS, DadosContatoVM>()
                    .ForMember(destino => destino.Celular, origem => origem.MapFrom(x => x.CELULAR))
                    .ForMember(destino => destino.EmailContato, origem => origem.MapFrom(x => x.EMAIL))
                    .ForMember(destino => destino.NomeContato, origem => origem.MapFrom(x => x.NOME))
                    .ForMember(destino => destino.Telefone, origem => origem.MapFrom(x => Mascara.RemoverMascaraTelefone(x.TELEFONE)));

                CreateMap<FORNECEDOR_CONTATOS, FORNECEDORBASE_CONTATOS>()
                    .ForMember(destino => destino.CELULAR, origem => origem.MapFrom(x => x.CELULAR))
                    .ForMember(destino => destino.EMAIL, origem => origem.MapFrom(x => x.EMAIL))
                    .ForMember(destino => destino.NOME, origem => origem.MapFrom(x => x.NOME))
                    .ForMember(destino => destino.TELEFONE, origem => origem.MapFrom(x => x.TELEFONE));

                CreateMap<FORNECEDOR_CONTATOS, DadosContatoVM>()
                    .ForMember(destino => destino.ContatoID, origem => origem.MapFrom(x => x.ID))
                    .ForMember(destino => destino.PjPfId, origem => origem.MapFrom(x => x.CONTRATANTE_PJPF_ID))
                    .ForMember(destino => destino.NomeContato, origem => origem.MapFrom(x => x.NOME))
                    .ForMember(destino => destino.EmailContato, origem => origem.MapFrom(x => x.EMAIL))
                    .ForMember(destino => destino.Telefone, origem => origem.MapFrom(x => x.TELEFONE))
                    .ForMember(destino => destino.Celular, origem => origem.MapFrom(x => x.CELULAR))
                    .ForMember(destino => destino.Estrangeiro, ori => ori.Ignore());

                CreateMap<FORNECEDOR_CONTATOS, MeusDocumentosPesquisarEmailGridVM>()
                    .ForMember(destino => destino.Id, origem => origem.MapFrom(x => x.ID))
                    .ForMember(destino => destino.Email, origem => origem.MapFrom(x => x.EMAIL))
                    .ForMember(destino => destino.Origem, origem => origem.MapFrom(x => TipoEmailVM.Fornecedor))
                    .AfterMap((src, dest) => dest.Nome = !string.IsNullOrEmpty(src.NOME) ? src.NOME.Replace(",", "") + " (" + src.EMAIL + ")" : src.EMAIL)
                    .AfterMap((src, dest) => dest.Empresa = !string.IsNullOrEmpty(src.WFD_CONTRATANTE_PJPF.WFD_PJPF.RAZAO_SOCIAL) ? src.WFD_CONTRATANTE_PJPF.WFD_PJPF.RAZAO_SOCIAL : src.WFD_CONTRATANTE_PJPF.WFD_PJPF.NOME_FANTASIA);

                CreateMap<FORNECEDOR_CONTATOS, EmailsVM>()
                    .ForMember(destino => destino.value, origem => origem.MapFrom(x => TipoEmailVM.Fornecedor + ":" + x.ID.ToString() + ":" + x.EMAIL))
                    .ForMember(destino => destino.text, origem => origem.MapFrom(x => (!string.IsNullOrEmpty(x.NOME) ? x.NOME.Replace(",", "") + " (" + x.EMAIL + ")" : x.EMAIL)));

                CreateMap<DadosContatoVM, FORNECEDOR_CONTATOS>()
                    .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ContatoID))
                    .ForMember(destino => destino.CONTRATANTE_PJPF_ID, origem => origem.MapFrom(x => x.PjPfId))
                    .ForMember(destino => destino.NOME, origem => origem.MapFrom(x => x.NomeContato))
                    .ForMember(destino => destino.EMAIL, origem => origem.MapFrom(x => x.EmailContato))
                    .ForMember(destino => destino.TELEFONE, origem => origem.MapFrom(x => x.Telefone))
                    .ForMember(destino => destino.CELULAR, origem => origem.MapFrom(x => x.Celular));

                CreateMap<DadosContatoVM, SolicitacaoModificacaoDadosContato>()
                    .ForMember(destino => destino.ID, origem => origem.MapFrom(x => x.ContatoID))
                    .ForMember(destino => destino.CONTATO_PJPF_ID, origem => origem.MapFrom(x => x.PjPfId))
                    .ForMember(destino => destino.SOLICITACAO_ID, origem => origem.Ignore())
                    .ForMember(destino => destino.NOME, origem => origem.MapFrom(x => x.NomeContato))
                    .ForMember(destino => destino.EMAIL, origem => origem.MapFrom(x => x.EmailContato))
                    .ForMember(destino => destino.TELEFONE, origem => origem.MapFrom(x => Mascara.RemoverMascaraTelefone(x.Telefone)))
                    .ForMember(destino => destino.CELULAR, origem => origem.MapFrom(x => Mascara.RemoverMascaraTelefone(x.Celular)))
                    .ForMember(destino => destino.CONTRATANTE_ID, origem => origem.Ignore())
                    .ForMember(destino => destino.PJPF_ID, origem => origem.Ignore())
                    .ForMember(destino => destino.TP_CONTATO_ID, origem => origem.Ignore())
                    .ForMember(destino => destino.WFD_PJPF_CONTATOS, origem => origem.Ignore())
                    .ForMember(destino => destino.WFD_SOLICITACAO, origem => origem.Ignore())
                    .ForMember(destino => destino.WFD_T_TP_CONTATO, origem => origem.Ignore());

                CreateMap<FornecedorContatosVM, SolicitacaoModificacaoDadosContato>()
                    .ForMember(dest => dest.NOME, ori => ori.MapFrom(x => x.Nome))
                    .ForMember(dest => dest.EMAIL, ori => ori.MapFrom(x => x.Email))
                    .ForMember(dest => dest.TELEFONE, ori => ori.Ignore())
                    .ForMember(dest => dest.CELULAR, ori => ori.Ignore())
                    .ForMember(dest => dest.WFD_PJPF_CONTATOS, ori => ori.Ignore())
                    .ForMember(dest => dest.WFD_SOLICITACAO, ori => ori.Ignore())
                    .ForMember(dest => dest.WFD_T_TP_CONTATO, ori => ori.Ignore());

                CreateMap<DadosContatoVM, FORNECEDORBASE_CONTATOS>()
                    .ForMember(destino => destino.CELULAR, origem => origem.MapFrom(x => x.Celular))
                    .ForMember(destino => destino.EMAIL, origem => origem.MapFrom(x => x.EmailContato))
                    .ForMember(destino => destino.NOME, origem => origem.MapFrom(x => x.NomeContato))
                    .ForMember(destino => destino.TELEFONE, origem => origem.MapFrom(x => Mascara.RemoverMascaraTelefone(x.Telefone)));
                //-------------------------
                CreateMap<SOLICITACAO, FichaCadastralAcompanhamentoVM>()
                    .ForMember(destino => destino.Id, origem => origem.MapFrom(x => x.ID))
                    .ForMember(destino => destino.TipoFluxoId, origem => origem.MapFrom(x => x.Fluxo.FLUXO_TP_ID))
                    .ForMember(destino => destino.DadosBancarios, origem => origem.MapFrom(x => x.SolicitacaoModificacaoDadosBancario))
                    .ForMember(destino => destino.DadosContatos, origem => origem.MapFrom(x => x.SolicitacaoModificacaoDadosContato))
                    .ForMember(destino => destino.DadosEnderecos, origem => origem.MapFrom(x => x.WFD_SOL_MOD_ENDERECO));
                    //.ForMember(destino => destino.DadosGerais, origem => origem.MapFrom(x => x.WFD_SOL_MOD_DGERAIS_SEQ));

                CreateMap<SOLICITACAO, FichaCadastralDadosSolicitacaoVM>()
                    .ForMember(destino => destino.Id, origem => origem.MapFrom(x => x.ID))
                    .ForMember(destino => destino.CriacaoSolicitacao, origem => origem.MapFrom(x => x.SOLICITACAO_DT_CRIA.ToString()));

                CreateMap<SolicitacaoModificacaoDadosBancario, FichaCadastralDadosBancariosVM>()
                    .ForMember(destino => destino.Id, origem => origem.MapFrom(x => x.ID))
                    .ForMember(dest => dest.BancoPJPFID, ori => ori.MapFrom(x => x.BANCO_PJPF_ID))
                    .ForMember(dest => dest.NomeBanco, ori => ori.MapFrom(x => x.T_BANCO.BANCO_NM))
                    .ForMember(dest => dest.Agencia, ori => ori.MapFrom(x => x.AGENCIA))
                    .ForMember(dest => dest.Digito, ori => ori.MapFrom(x => x.AG_DV))
                    .ForMember(dest => dest.Banco, ori => ori.MapFrom(x => x.BANCO_ID))
                    .ForMember(dest => dest.ContaCorrente, ori => ori.MapFrom(x => x.CONTA))
                    .ForMember(dest => dest.ContaCorrenteDigito, ori => ori.MapFrom(x => x.CONTA_DV))
                    .ForMember(dest => dest.NomeArquivo, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.NOME_ARQUIVO))
                    .ForMember(dest => dest.ArquivoID, ori => ori.MapFrom(x => x.ARQUIVO_ID))
                    .ForMember(dest => dest.DataUpload, ori => ori.MapFrom(x => x.WFD_ARQUIVOS.DATA_UPLOAD))
                    .ForMember(dest => dest.SolicitacaoID, ori => ori.MapFrom(x => x.SOLICITACAO_ID));

                CreateMap<SolicitacaoModificacaoDadosContato, FichaCadastralDadosContatosVM>()
                    .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.ID))
                    .ForMember(dest => dest.PjPfId, ori => ori.MapFrom(x => x.CONTATO_PJPF_ID))
                    .ForMember(dest => dest.NomeContato, ori => ori.MapFrom(x => x.NOME))
                    .ForMember(dest => dest.EmailContato, ori => ori.MapFrom(x => x.EMAIL))
                    .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.TELEFONE))
                    .ForMember(dest => dest.Celular, ori => ori.MapFrom(x => x.CELULAR))
                    .ForMember(dest => dest.Estrangeiro, ori => ori.Ignore());

                CreateMap<SOLICITACAO_MODIFICACAO_ENDERECO, FichaCadastralDadosEnderecosVM>()
                    .ForMember(destino => destino.TipoEndereco, origem => origem.MapFrom(x => x.WFD_T_TP_ENDERECO.NM_TP_ENDERECO))
                    .ForMember(destino => destino.Endereco, origem => origem.MapFrom(x => x.ENDERECO))
                    .ForMember(destino => destino.Numero, origem => origem.MapFrom(x => x.NUMERO))
                    .ForMember(destino => destino.Complemento, origem => origem.MapFrom(x => x.COMPLEMENTO))
                    .ForMember(destino => destino.CEP, origem => origem.MapFrom(x => x.CEP))
                    .ForMember(destino => destino.Bairro, origem => origem.MapFrom(x => x.BAIRRO))
                    .ForMember(destino => destino.Cidade, origem => origem.MapFrom(x => x.CIDADE))
                    .ForMember(destino => destino.UF, origem => origem.MapFrom(x => x.UF))
                    .ForMember(destino => destino.Pais, origem => origem.MapFrom(x => x.PAIS))
                    .ForMember(destino => destino.T_UF, origem => origem.MapFrom(x => x.T_UF))
                    .ForMember(destino => destino.TipoEnderecoId, origem => origem.MapFrom(x => x.TP_ENDERECO_ID))
                    .ForMember(destino => destino.PjPjId, origem => origem.MapFrom(x => x.PJPF_ENDERECO_ID))
                    .ForMember(destino => destino.SolicitacaoID, origem => origem.Ignore())
                    .ForMember(destino => destino.WFD_SOLICITACAO, origem => origem.Ignore())
                    .ForMember(destino => destino.WFD_T_TP_ENDERECO, origem => origem.Ignore());

                //Mapper.AssertConfigurationIsValid();
            }
            catch (Exception)
            {
            }
        }
    }
}