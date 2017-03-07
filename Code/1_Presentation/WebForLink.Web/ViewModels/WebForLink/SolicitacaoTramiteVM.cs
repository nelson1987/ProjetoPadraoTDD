using System;

namespace WebForLink.Web.ViewModels
{
    public class SolicitacaoTramiteVM
    {
        public int ID { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime? DataFim { get; set; }

        public int UsuarioID { get; set; }

        public PapelVM Papel { get; set; }
        public SolicitacaoStatusVM Status { get; set; }

    }
}