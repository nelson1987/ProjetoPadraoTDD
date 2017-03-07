using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using WebForLink.Application.Interfaces;
using WebForLink.Application.Interfaces.WebForLink;
using WebForLink.Application.Services.Process;
using WebForLink.Domain.Entities.WebForLink;
using WebForLink.Domain.Enums;
using WebForLink.Web.Exceptions;
using WebForLink.Web.ViewModels.Carga;

namespace WebForLink.Web.Infrastructure
{
    public interface IThreadRoboCargaErp
    {
        void InicializarRobo();
    }
    public class ThreadRoboCargaErp : IThreadRoboCargaErp
    {
        private readonly ISolicitacaoWebForLinkAppService solicitacaoBP;
        private readonly ContratanteConfiguracaoWebForLinkAppService contratanteConfig;
        private readonly ITramiteWebForLinkAppService _tramite;
        private readonly IPapelWebForLinkAppService papelBP;
        public void InicializarRobo()
        {
            solicitacaoBP.BuscarSolicitacaoAguardandoCarga()
                .ForEach(x =>
                {
                    CriarArquivo(x);
                });
        }
        /// <summary>
        /// Criar o arquivo na pasta indicada, conforme as solicitações daquele contratante
        /// </summary>
        private void CriarArquivo(int contratanteId)
        {
            try
            {
                //Serializador xml
                XmlSerializer serializer = new XmlSerializer(typeof(SolicitacaoCarga));

                //Leitura de todos as solicitações
                List<SOLICITACAO> solicitacoes = solicitacaoBP.ListarSolicitacaoCarga(contratanteId);
                //Mapeamento com a classe xml
                SolicitacaoCarga solicitacao = new SolicitacaoCarga
                {
                    Solicitacoes = Mapper.Map<List<SOLICITACAO>, List<SolicitacoesCarga>>(solicitacoes)
                };
                //Escrita XML
                var diretorioCarga = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioCarga");
                using (TextWriter writer = new StreamWriter(NomearDocumento(contratanteId, diretorioCarga)))
                {
                    serializer.Serialize(writer, solicitacao);
                }
                var diretorioCargaBackup = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioCargaBackup");
                using (TextWriter writer = new StreamWriter(NomearDocumento(contratanteId, diretorioCargaBackup)))
                {
                    serializer.Serialize(writer, solicitacao);
                }

                // ATUALIZA O TRAMITE DE CADA SOLICITAÇÃO INCLUIDA NO ARQUIVO
                int papelAtual = papelBP.BuscarPorContratanteETipoPapel(contratanteId, (int)EnumTiposPapel.CargaSAP).ID;
                solicitacoes.ForEach(x => _tramite.AtualizarTramite(x.CONTRATANTE_ID, x.ID, x.FLUXO_ID, papelAtual, (int)EnumStatusTramite.Aprovado, null));

            }
            catch (Exception ex)
            {
                throw new WebForLinkException("Erro ao tentar criar o arquivo", ex);
            }
        }
        /// <summary>
        /// Criar o Nome conforme regras da especificação
        /// </summary>
        /// <param name="contratanteId">Id do contratante</param>
        /// <returns>Nome do arquivo .xml</returns>
        private string NomearDocumento(int contratanteId, string diretorioCarga)
        {
            try
            {
                if (!Directory.Exists(diretorioCarga))
                {
                    Directory.CreateDirectory(diretorioCarga);
                }
                var arquivoRetorno = contratanteConfig.BuscarPorID(contratanteId).FORNECEDOR_CARGA.Replace("{DATAHORA}", DateTime.Now.ToString("yyyyMMddHHmmss"));

                return string.Format("{0}\\{1}", diretorioCarga, arquivoRetorno);
            }
            catch (Exception ex)
            {
                throw new WebForLinkException("Erro ao tentar nomear o arquivo", ex);
            }
        }
    }
}
