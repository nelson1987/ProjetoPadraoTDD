using AutoMapper;
using System;
using System.Collections.Generic;
using WebForLink.Domain.Entities;

namespace WebForLink.Web.ViewModels
{
    public class ArquivoSalvoVM
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string EnderecoDownload(int solicitacao, int documentoSolicitado)
        {
            if (solicitacao == 0 || documentoSolicitado == 0)
                throw new ArgumentException();
            return string.Format("/DADOS/Solicitacao/{0}/{1}/{2}",solicitacao, documentoSolicitado,Nome);
        }
    }
    public class AnexosVM
    {
        public AnexosVM()
        {
            ArquivosSalvos = new List<ArquivoSalvoVM>();
        }
        public List<ArquivoSalvoVM> ArquivosSalvos { get; set; }
    }
    public class ArquivoAnexadoVM
    {
        public ArquivoAnexadoVM()
        {
            ArquivosAnexos = new List<AnexosVM>();
        }
        public int Id { get; set; }
        public string CodigoCliente { get; set; }
        public int IdSolicitacao { get; set; }
        public string ArquivoAnexado { get; set; }

        public List<AnexosVM> ArquivosAnexos { get; set; }

        public static ArquivoAnexadoVM ToViewModel(DocumentoSolicitacao model)
        {
            return Mapper.Map<ArquivoAnexadoVM>(model);
        }
    }
}