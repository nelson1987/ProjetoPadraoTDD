using System;
using System.Linq;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Infrastructure.Exceptions;
using WebForLink.Domain.Interfaces.Repository;
using WebForLink.Domain.Services.Common;

namespace WebForLink.Domain.Services.Process
{
    public interface IConfiguracaoWebForLinkService
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

    public class ConfiguracaoWebForLinkService : Service<CONFIGURACAO>, IConfiguracaoWebForLinkService
    {
        private readonly IConfiguracaoWebForLinkRepository _configuracaoRepository;

        public ConfiguracaoWebForLinkService(IConfiguracaoWebForLinkRepository config) : base(config)
        {
            try
            {
                _configuracaoRepository = config;
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
                return _configuracaoRepository.All(true).FirstOrDefault();
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
                return _configuracaoRepository.Get(1).ROBO_IMPORTACAO;
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
                return _configuracaoRepository.Get(1).ROBO_GOVERNANCA;
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
            return _configuracaoRepository.Get(id).CHAVE_WEBSERVICE;
        }

        public bool AlterarConfig(TimeSpan tempo)
        {
            try
            {
                var config = _configuracaoRepository.Get(1);
                config.ROBO_IMPORTACAO = FormatarTempo(tempo);
                _configuracaoRepository.Update(config);
                return true;
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
                return _configuracaoRepository.Get(1);
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
                return _configuracaoRepository.Find(x => x.ID == 1 && x.CHAVE_WEBSERVICE == chave) != null;
            }
            catch (Exception ex)
            {
                throw new ServiceWebForLinkException("Erro ao buscar a Cofiguração Geral", ex);
            }
        }
    }
}