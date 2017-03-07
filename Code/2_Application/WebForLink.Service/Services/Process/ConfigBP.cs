using System;
using WebForLink.Data.Contextos;
using WebForLink.Domain.Models;
using WebForLink.Repository.Repository;
using WebForLink.Service.Infrastructure.Exceptions;

namespace WebForLink.Service.Process
{
    //public interface IConfiguracaoService
    //{
    //    CONFIGURACAO BuscaConfigGeral();
    //    bool VerificaChaveWebService(string chave);
    //}
    //public class ConfigService : PadraoService<IUnitOfWork>, IDisposable, IConfiguracaoBP
    //{
    //    private readonly IUnitOfWork _unitOfWork;
    //    private readonly IConfiguracaoRepository _configuracaoRepository;

    //    public void Dispose()
    //    {
    //        _unitOfWork.Finalizar();
    //    }

    //    public ConfigService(IUnitOfWork unitOfWork, IConfiguracaoRepository configuracao)
    //    {
    //        try
    //        {
    //            _unitOfWork = unitOfWork;
    //            _configuracaoRepository = configuracao;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ServiceWebForLinkException(ex.Message);
    //        }
    //    }
    //    public CONFIGURACAO BuscaConfigGeral()
    //    {
    //        try
    //        {
    //            return _configuracaoRepository.BuscarPorId(1);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ServiceWebForLinkException("Erro ao buscar a Cofiguração Geral", ex);
    //        }
    //    }
    //    public bool VerificaChaveWebService(string chave)
    //    {
    //        try
    //        {
    //            return _configuracaoRepository.Buscar(x => x.ID == 1 && x.CHAVE_WEBSERVICE == chave) != null;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ServiceWebForLinkException("Erro ao buscar a Cofiguração Geral", ex);
    //        }
    //    }
    //}
}
