using System;
using System.Collections.Generic;

namespace WebForLink.Web.ViewModels
{
    public class SolicitacaoVM
    {
        public SolicitacaoVM()
        {
            Fluxo = new FluxoVM();
            Tramite = new SolicitacaoTramiteVM();
            Tramites = new List<SolicitacaoTramiteVM>();
        }

        public int ID { get; set; }

        DateTime DataSolicitacao { get; set; }

        public bool Aprovado { get; set; }

        public int Usuario_ID { get; set; }

        public FluxoVM Fluxo { get; set; }

        public SolicitacaoTramiteVM Tramite { get; set; }

        public List<SolicitacaoTramiteVM> Tramites { get; set; }
    }

}