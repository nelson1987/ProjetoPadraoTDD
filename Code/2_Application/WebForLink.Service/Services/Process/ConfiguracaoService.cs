using System;
using WebForLink.Application.Services.Common;
using WebForLink.Data.Context;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Services.Process;

namespace WebForLink.Application.Services.Process
{
    public interface IConfiguracaoWebForLinkAppService
    {
        CONFIGURACAO BuscarConfig();
        string BuscarTempoRoboImportacao();
        string BuscarTempoRoboGovernanca();
        string FormatarTempo(TimeSpan ts);
        string BuscarChave(int id);
        bool AlterarConfig(TimeSpan tempo);
        bool ValidarTempo(string tempo, ref TimeSpan ts);
        CONFIGURACAO BuscarConfigGeral();
        bool VerificarChaveWebService(string chave);
    }

    public class ConfiguracaoWebForLinkAppService : AppService<WebForLinkContexto>, IConfiguracaoWebForLinkAppService
    {
        private readonly IConfiguracaoWebForLinkService _configuracaoService;

        public ConfiguracaoWebForLinkAppService(IConfiguracaoWebForLinkService config)
        {
            try
            {
                _configuracaoService = config;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException(ex.Message);
            }
        }

        public CONFIGURACAO BuscarConfig()
        {
            try
            {
                return _configuracaoService.BuscarConfig();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar um contratante", ex);
            }
        }

        public string BuscarTempoRoboImportacao()
        {
            try
            {
                return _configuracaoService.BuscarTempoRoboImportacao();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o tempo de execução do Robo para Importação", ex);
            }
        }

        public string BuscarTempoRoboGovernanca()
        {
            try
            {
                return _configuracaoService.BuscarTempoRoboGovernanca();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar o tempo de execução do Robo para Governança", ex);
            }
        }

        public string FormatarTempo(TimeSpan ts)
        {
            int horas, minutos, segundos;

            if (ts.TotalHours >= 24)
                horas = ts.Days*24;
            else
                horas = ts.Hours;

            minutos = ts.Minutes;
            segundos = ts.Seconds;

            return horas.ToString().PadLeft(3, '0') + ":" + minutos.ToString().PadLeft(2, '0') + ":" +
                   segundos.ToString().PadLeft(2, '0');
        }

        public string BuscarChave(int id)
        {
            return _configuracaoService.BuscarChave(id);
        }

        public bool AlterarConfig(TimeSpan tempo)
        {
            try
            {
                return _configuracaoService.AlterarConfig(tempo);
            }
            catch
            {
                return false;
            }
        }

        public bool ValidarTempo(string tempo, ref TimeSpan ts)
        {
            var vTmp = tempo.Split(':');
            int horas, minutos, segundos;

            if (!int.TryParse(vTmp[0], out horas))
                return false;
            if (!int.TryParse(vTmp[1], out minutos))
                return false;
            if (!int.TryParse(vTmp[2], out segundos))
                return false;

            ts = new TimeSpan(horas, minutos, segundos);

            if (ts.TotalSeconds == 0)
                return false;

            return true;
        }

        public CONFIGURACAO BuscarConfigGeral()
        {
            try
            {
                return _configuracaoService.BuscarConfigGeral();
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a Cofiguração Geral", ex);
            }
        }

        public bool VerificarChaveWebService(string chave)
        {
            try
            {
                return _configuracaoService.VerificarChaveWebService(chave);
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a Cofiguração Geral", ex);
            }
        }
    }
}