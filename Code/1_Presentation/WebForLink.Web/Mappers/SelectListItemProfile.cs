using AutoMapper;
using System;
using System.Web.Mvc;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure;

namespace WebForLink.Web.Mappers
{
    public class SelectListItemProfile : Profile
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override string ProfileName
        {
            get { return "RefactorMappingProfile"; }
        }

        [Obsolete("Create a constructor and configure inside of your profile\'s constructor instead. Will be removed in 6.0")]
        protected override void Configure()
        {
            try
            {
                MapeamentoConversores();
                //Mapper.AssertConfigurationIsValid();
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
        public void MapeamentoConversores()
        {
            #region SelectListItem
            CreateMap<FORNECEDORBASE_IMPORTACAO, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.NOME_ARQUIVO.ToString()))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<WFD_CONTRATANTE_PJPF, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.CONTRATANTE_ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.WFD_CONTRATANTE.RAZAO_SOCIAL.ToString()))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<DescricaoDeDocumentos, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.DESCRICAO))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<TipoDeDocumento, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.DESCRICAO))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<Contratante, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<Papel, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.PAPEL_NM))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<Perfil, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.PERFIL_NM))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<TiposDeBanco, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.BANCO_NM))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<QUESTIONARIO_RESPOSTA, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.RESP_DSC))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<FORNECEDORBASE_CONTATOS, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.NOME))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<Fluxo, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.FLUXO_NM))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<FORNECEDOR_CATEGORIA, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.DESCRICAO))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<Contratante, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.RAZAO_SOCIAL))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<CONTRATANTE_ORGANIZACAO_COMPRAS, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.ORG_COMPRAS_DSC))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<QUESTIONARIO_RESPOSTA, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.ID.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => string.Concat(x.RESP_COD, "-", x.RESP_DSC)))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());

            CreateMap<RespostasPossiveis, SelectListItem>()
                .ForMember(dest => dest.Value, ori => ori.MapFrom(x => x.Id.ToString()))
                .ForMember(dest => dest.Text, ori => ori.MapFrom(x => x.Texto))
                .ForMember(dest => dest.Disabled, ori => ori.Ignore())
                .ForMember(dest => dest.Group, ori => ori.Ignore())
                .ForMember(dest => dest.Selected, ori => ori.Ignore());
            #endregion
        }
    }
}