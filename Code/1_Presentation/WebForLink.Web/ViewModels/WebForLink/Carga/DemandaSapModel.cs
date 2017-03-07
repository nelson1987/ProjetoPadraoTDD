using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace WebForLink.Web.ViewModels.Carga
{
    [Serializable]
    public class DemandaSapModel
    {
        private int TipoAcao { get; set; }
        public string CodigoSolicitacao { get; set; }
        public string Empresa { get; set; }
        public string CodigoSAP { get; set; }
        public string OrganizacaoCompras { get; set; }
        public void limitarTamanhoPropriedade(string campo, int tamanho, string propriedade, StringBuilder linha)
        {
            if (campo == null)
            {
                campo = string.Empty;
                campo = campo.PadRight(tamanho);
            }
            campo = (campo.Length < tamanho) ? campo = campo.PadRight(tamanho) : campo = campo.Substring(0, tamanho);
            propriedade = campo.PadRight(tamanho);
            propriedade = campo.Substring(0, tamanho);
            linha.Append(propriedade.Trim() + ";");
        }
        public void CriarEscreverDocumentosRetorno<T>(IList<T> listagem, string nomeArquivo)
        {
            var diretorioPadrao = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioRetornoSap");
            var diretorioBackup = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioRetornoSapBackup");
            if (!Directory.Exists(diretorioPadrao))
            {
                Directory.CreateDirectory(diretorioPadrao);
            }
            if (!Directory.Exists(diretorioBackup))
            {
                Directory.CreateDirectory(diretorioBackup);
            }
            var dataConcat = DateTime.Now.ToString("yyyyMMddHHmmss");
            var filenamePadrao = diretorioPadrao + "\\" + dataConcat + nomeArquivo + ".txt";
            var swPadrao = new System.IO.StreamWriter(filenamePadrao, true);
            var filenameBackup = diretorioBackup + "\\" + dataConcat + nomeArquivo + ".txt";
            var swBackup = new System.IO.StreamWriter(filenameBackup, true);
            foreach (var item in listagem)
            {
                swPadrao.WriteLine(item);
                swBackup.WriteLine(item);
            }
            swPadrao.Close();
            swBackup.Close();
        }
        public void CriarEscreverDocumentos<T>(IList<T> listagem, string nomeArquivo)
        {
            var diretorioPadrao = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioCarga");
            var diretorioBackup = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings.Get("DiretorioCargaBackup");
            if (!Directory.Exists(diretorioPadrao))
            {
                Directory.CreateDirectory(diretorioPadrao);
            }
            if (!Directory.Exists(diretorioBackup))
            {
                Directory.CreateDirectory(diretorioBackup);
            }
            var dataConcat = DateTime.Now.ToString("yyyyMMddHHmmss");
            var filenamePadrao = diretorioPadrao + "\\" + dataConcat + nomeArquivo + ".txt";
            var swPadrao = new System.IO.StreamWriter(filenamePadrao, true);
            var filenameBackup = diretorioBackup + "\\" + dataConcat + nomeArquivo + ".txt";
            var swBackup = new System.IO.StreamWriter(filenameBackup, true);
            foreach (var item in listagem)
            {
                swPadrao.WriteLine(item);
                swBackup.WriteLine(item);
            }
            swPadrao.Close();
            swBackup.Close();
        }
        public void GerarArquivo(IList<string> baselinha, string nomeArquivo)
        {
            CriarEscreverDocumentos(baselinha, nomeArquivo);
        }
        public void GerarArquivoRetorno(IList<string> baselinha, string nomeArquivo)
        {
            CriarEscreverDocumentosRetorno(baselinha, nomeArquivo);
        }
        public void GerarArquivoDadosFiscais(IList<DadosFiscaisCargaModel> baselinha, string nomeArquivo)
        {
            CriarEscreverDocumentos(baselinha, nomeArquivo);
        }
    }
}
