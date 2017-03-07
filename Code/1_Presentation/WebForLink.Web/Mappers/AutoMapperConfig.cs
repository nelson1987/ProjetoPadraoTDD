using AutoMapper;
using log4net;
using System;
using System.Linq;
using System.Reflection;
using WebForLink.Domain.Entities;
using WebForLink.Web.ViewModels;

namespace WebForLink.Web.Mappers
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            //Mapper.Initialize(x => {  });

            Mapper.Initialize(x =>
            {
                x.AddProfile<MappingProfile>();
                x.AddProfile<MappingProfileA>();
                x.AddProfile<ContatoProfile>();
                x.AddProfile<RefactorMappingProfile>();
                x.AddProfile<SelectListItemProfile>();
            });
        }
    }

    public class MappingProfile : Profile
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }

        public MappingProfile()
        {

        //}
        //protected override void Configure()
        //{
            try
            {
                Solicitacao();
                FichaCadastral();
                Mapper.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        protected void Solicitacao()
        {
            CreateMap<EnviarEmailSolicitacaoVM, Solicitacao>()
            .ConstructUsing(source => new Solicitacao(
                new Solicitante(source.CodigoCliente, source.LoginUsuario, ""),
                new Solicitado(source.Cnpj, new Responsavel("", ""))));

            CreateMap<Solicitacao, SolicitacaoConviteVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.Cnpj, ori => ori.MapFrom(x => x.Solicitado.Cnpj))
                .ForMember(dest => dest.RazaoSocial, ori => ori.MapFrom(x => x.Solicitado.RazaoSocial))
                .ForMember(dest => dest.Preenchido, ori => ori.MapFrom(x => x.DocumentoSolicitacao != null))
                .ForMember(dest => dest.IdFichaCadastral, ori => ori.MapFrom(x => x.FichaCadastralId))
                .ForMember(dest => dest.FichaCadastralPreenchido, ori => ori.MapFrom(
                    x => x.FichaCadastral.Banco.Any()
                && x.FichaCadastral.Endereco.Any()
                && x.FichaCadastral.Contato.Any()
                ))
                .ForMember(dest => dest.DocumentosPreenchido, ori => ori.MapFrom(
                    x => x.DocumentoSolicitacao
                    .All(y => y.ValidaArquivos)))
                .ForMember(dest => dest.FichaCadastral, ori => ori.MapFrom(x => x.FichaCadastral));

            CreateMap<FichaCadastral, FichaCadastralVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.Status, ori => ori.MapFrom(x => x.Status))
                .ForMember(dest => dest.Bancos, ori => ori.MapFrom(x => x.Banco))
                .ForMember(dest => dest.Contatos, ori => ori.MapFrom(x => x.Contato))
                //.ForMember(dest => dest.DocumentoAnexados, ori => ori.MapFrom(x => x.DocumentoAnexados))
                .ForMember(dest => dest.Enderecos, ori => ori.MapFrom(x => x.Endereco))
                .ForMember(dest => dest.DocumentoAnexados, ori => ori.Ignore());

            CreateMap<Banco, BancoVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.IdFichaCadastral, ori => ori.MapFrom(x => x.IdFichaCadastral))
                .ForMember(dest => dest.Banco, ori => ori.MapFrom(x => x.Numero))
                .ForMember(dest => dest.AgenciaDV, ori => ori.MapFrom(x => x.AgenciaDv))
                .ForMember(dest => dest.ContaCorrente, ori => ori.MapFrom(x => x.Conta))
                .ForMember(dest => dest.ContaCorrenteDV, ori => ori.MapFrom(x => x.ContaDv));

            CreateMap<Contato, ContatoVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.IdFichaCadastral, ori => ori.MapFrom(x => x.IdFichaCadastral))
                .ForMember(dest => dest.NomeContato, ori => ori.MapFrom(x => x.Nome))
                .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.Telefone))
                .ForMember(dest => dest.Celular, ori => ori.MapFrom(x => x.Celular))
                .ForMember(dest => dest.Email, ori => ori.MapFrom(x => x.Email));

            CreateMap<DocumentoSolicitacao, DocumentoAnexadoVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.Descricao, ori => ori.MapFrom(x => string.IsNullOrEmpty(x.DescricaoDocumentoCH) ? x.DescricaoDocumento : x.DescricaoDocumentoCH));

            CreateMap<DocumentoSolicitacao, ArquivoAnexadoVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.IdSolicitacao, ori => ori.MapFrom(x => x.IdSolicitacao))
                .ForMember(dest => dest.CodigoCliente, ori => ori.MapFrom(x => x.Solicitacao.Solicitante.CodigoCliente))
                .ForMember(dest => dest.ArquivoAnexado, ori => ori.MapFrom(x => x.IdTipoDocumentoCH == 7 ? x.DescricaoDocumento : x.DescricaoDocumentoCH))
                .ForMember(dest => dest.ArquivosAnexos, ori => ori.MapFrom(x => x.DocumentoAnexados));

            CreateMap<DocumentoAnexado, AnexosVM>()
                .ForMember(dest => dest.ArquivosSalvos, ori => ori.MapFrom(x => x.Arquivos));

            CreateMap<Arquivo, ArquivoSalvoVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NomeOriginal))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.LocalArquivo));

            CreateMap<Arquivo, ArquivoSalvoVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NomeOriginal))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.LocalArquivo));

            CreateMap<Endereco, EnderecoVM>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.IdFichaCadastral, ori => ori.MapFrom(x => x.IdFichaCadastral))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.Logradouro))
                .ForMember(dest => dest.Cep, ori => ori.MapFrom(x => x.CEP))
                .ForMember(dest => dest.Estado, ori => ori.MapFrom(x => x.UF));
        }

        protected void FichaCadastral()
        {
            CreateMap<SolicitacaoConviteVM, Solicitacao>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.FichaCadastral, ori => ori.MapFrom(x => x.FichaCadastral));

            CreateMap<FichaCadastralVM, FichaCadastral>()
                .ForMember(dest => dest.Banco, ori => ori.MapFrom(x => x.Bancos))
                .ForMember(dest => dest.Contato, ori => ori.MapFrom(x => x.Contatos))
                //.ForMember(dest => dest.DocumentoAnexados, ori => ori.MapFrom(x => x.DocumentoAnexados))
                .ForMember(dest => dest.Endereco, ori => ori.MapFrom(x => x.Enderecos))
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.Status, ori => ori.MapFrom(x => x.Status));

            CreateMap<BancoVM, Banco>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.IdFichaCadastral, ori => ori.MapFrom(x => x.IdFichaCadastral))
                .ForMember(dest => dest.Numero, ori => ori.MapFrom(x => x.Banco))
                .ForMember(dest => dest.AgenciaDv, ori => ori.MapFrom(x => x.AgenciaDV))
                .ForMember(dest => dest.Conta, ori => ori.MapFrom(x => x.ContaCorrente))
                .ForMember(dest => dest.ContaDv, ori => ori.MapFrom(x => x.ContaCorrenteDV));

            CreateMap<ContatoVM, Contato>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.IdFichaCadastral, ori => ori.MapFrom(x => x.IdFichaCadastral))
                .ForMember(dest => dest.Nome, ori => ori.MapFrom(x => x.NomeContato))
                .ForMember(dest => dest.Telefone, ori => ori.MapFrom(x => x.Telefone))
                .ForMember(dest => dest.Celular, ori => ori.MapFrom(x => x.Celular))
                .ForMember(dest => dest.Email, ori => ori.MapFrom(x => x.Email));

            CreateMap<EnderecoVM, Endereco>()
                .ForMember(dest => dest.Id, ori => ori.MapFrom(x => x.Id))
                .ForMember(dest => dest.IdFichaCadastral, ori => ori.MapFrom(x => x.IdFichaCadastral))
                .ForMember(dest => dest.Logradouro, ori => ori.MapFrom(x => x.Endereco))
                .ForMember(dest => dest.CEP, ori => ori.MapFrom(x => x.Cep))
                .ForMember(dest => dest.UF, ori => ori.MapFrom(x => x.Estado));
        }
    }
}