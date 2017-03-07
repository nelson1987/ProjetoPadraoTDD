using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebForDocs.Biblioteca
{
    public static class Mascara
    {
        public static string MascararCNPJouCPF(string CNPJouCPF)
        {
            string resultado = string.Empty;

            switch (CNPJouCPF.Length)
            {
                case 14:
                    resultado = Convert.ToUInt64(CNPJouCPF).ToString(@"00\.000\.000\/0000\-00");
                    break;
                case 11:
                    resultado = Convert.ToUInt64(CNPJouCPF).ToString(@"000\.000\.000\-00");
                    break;
                default:
                    resultado = CNPJouCPF;
                    break;

            }

            return resultado;
        }

        public static string MascararTelefone(string telefone)
        {
            string resultado = string.Empty;

            switch (telefone.Length)
            {
                case 10:
                    resultado = String.Format("{0:(##) ####-####}", telefone);
                    break;
                case 11:
                    resultado = String.Format("{0:(##) #####-####}", telefone);
                    break;
            }

            return resultado;
        }

        public static string MascararData(string data, string formato)
        {
            string resultado = string.Empty;

            switch (formato)
            {
                case "dd/MM/yyyy":
                    resultado = String.Format(String.Concat("{0:", formato), data);
                    break;
            }

            return resultado;
        }

        public static string RemoverMascara(string item)
        {
            return new string(item.Where(char.IsLetterOrDigit).ToArray());
        }

        public static string RemoverMascaraTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
                return telefone;
            return string.IsNullOrEmpty(telefone) ? telefone : telefone.Replace("(", "").Replace(")", "").Replace(" ", "").Replace("-", "");
        }

        public static string RemoverMascaraCpfCnpj(string documento)
        {
            if (string.IsNullOrEmpty(documento))
                return documento;
            return string.IsNullOrEmpty(documento) ? documento : documento.Replace(".", "").Replace("/", "").Replace("-", "").Replace("_", "").Trim();
        }
        
        public static string IncluirMascaraTelefone(string telefone)
        {
            if (string.IsNullOrEmpty(telefone))
                return telefone;
            return telefone.Length == 11 ? Convert.ToUInt64(telefone).ToString(@"\(00\) 00000\-0000") : Convert.ToUInt64(telefone).ToString(@"\(00\) 0000\-0000");
        }
    }
}