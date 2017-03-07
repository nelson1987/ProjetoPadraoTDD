using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForDocs.Biblioteca
{
    public class RoboCPF
    {
        public int code { get; set; }

        public string html { get; set; }

        public dataCPF data { get; set; }

        public string uuid { get; set; }
    }

    public class dataCPF
    {
        public string situacao_cadastral { get; set; }

        public string nome { get; set; }

        public string hora_emissao { get; set; }

        public string cpf { get; set; }

        public string contingencia { get; set; }

        public string digito_verificador { get; set; }

        public string data_emissao { get; set; }

        public string comprovante { get; set; }
    }
}