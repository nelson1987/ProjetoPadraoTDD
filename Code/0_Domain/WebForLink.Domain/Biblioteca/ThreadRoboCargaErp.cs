using AutoMapper;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using WebForDocs.Business.Process;
using WebForDocs.Dominio.Models;
using WebForDocs.Exceptions;
using WebForDocs.ViewModels.Carga;
using WebForDocs.Business.Infrastructure.Enum;
using WebForDocs.Data.ModeloDB;

namespace WebForDocs.Biblioteca
{
    public class ThreadRoboCargaErp
    {
        static SolicitacaoBP solicitacaoBP = new SolicitacaoBP();

        public static void InicializarRobo()
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
        private static void CriarArquivo(int contratanteId)
        {
            try
            {
                //Serializador xml
                XmlSerializer serializer = new XmlSerializer(typeof(SolicitacaoCarga));

                //Leitura de todos as solicitações
                List<WFD_SOLICITACAO> solicitacoes = solicitacaoBP.ListarSolicitacaoCarga(contratanteId);
                //Mapeamento com a classe xml
                SolicitacaoCarga solicitacao = new SolicitacaoCarga
                {
                    Solicitacoes = Mapper.Map<List<WFD_SOLICITACAO>, List<SolicitacoesCarga>>(solicitacoes)
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
                WFLModel db = new WFLModel();
                PapelBP papelBP = new PapelBP();
                int papelAtual = papelBP.BuscarPorContratanteETipoPapel(contratanteId, (int)EnumTiposPapel.CargaSAP).ID;
                solicitacoes.ForEach(x => Tramite.AtualizaTramite(x.CONTRATANTE_ID, x.ID, x.FLUXO_ID, papelAtual, (int)EnumStatusTramite.Aprovado, null));

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
        private static string NomearDocumento(int contratanteId, string diretorioCarga)
        {
            try
            {
                if (!Directory.Exists(diretorioCarga))
                {
                    Directory.CreateDirectory(diretorioCarga);
                }
                ContratanteConfiguracaoBP contratanteConfig = new ContratanteConfiguracaoBP();
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
