using System.Collections.Generic;
using AutoMapper;
using WebForLink.Domain.Entities;

namespace WebForLink.Web.ViewModels
{
    public class CriarSolicitacaoVM
    {
        public string CodigoFornecedor { get; set; }
        public string LoginUsuario { get; set; }
        public string Assunto { get; set; }
        public string Mensagem { get; set; }
        public List<int> DocumentosSolicitados { get; set; }

        public static Solicitacao ToModel(CriarSolicitacaoVM solicitacao)
        {
            return Mapper.Map<Solicitacao>(solicitacao);
        }

        public static CriarSolicitacaoVM ToViewModel(Solicitacao solicitacao)
        {
            return Mapper.Map<CriarSolicitacaoVM>(solicitacao);
        }
    }
}