using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebForLink.Domain.Biblioteca
{
    public class BibliotecaWFL
    {
        public const string Key = "r10X310y";

        public string ValorKey()
        {
            return Key;
        }

        /// <summary>
        ///     Método estático para remover tag de um ponto inicial até seu ponto final
        ///     EX: string x = "<script>javascript code</script>";
        ///     .RemoveTag("x","<script", "</script>");
        ///     Todo o conteúdo será removido.
        /// </summary>
        /// <param name="html">A string que contém caracter para ser removido </param>
        /// <param name="startTag">Início da remoção</param>
        /// <param name="endTag">Fim da remoção</param>
        /// <returns></returns>
        public string RemoveTag(String html, String startTag, String endTag)
        {
            Boolean bAgain;
            do
            {
                bAgain = false;
                var startTagPos = html.IndexOf(startTag, 0, StringComparison.CurrentCultureIgnoreCase);
                if (startTagPos < 0)
                    continue;
                var endTagPos = html.IndexOf(endTag, startTagPos + 1, StringComparison.CurrentCultureIgnoreCase);
                if (endTagPos <= startTagPos)
                    continue;
                html = html.Remove(startTagPos, endTagPos - startTagPos + endTag.Length);
                bAgain = true;
            } while (bAgain);
            return html;
        }

        /// <summary>
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public string EncodeCodigoHtml(string param)
        {
            Encoding.ASCII.GetString(Encoding.Convert(
                Encoding.UTF8,
                Encoding.GetEncoding(
                    Encoding.ASCII.EncodingName,
                    new EncoderReplacementFallback(string.Empty),
                    new DecoderExceptionFallback()
                    ),
                Encoding.UTF8.GetBytes(param)
                ));

            return param;
        }

        public string FormatarCnpjCpf(string documento)
        {
            if (string.IsNullOrEmpty(documento))
                return string.Empty;
            return documento.Length == 14
                ? Convert.ToUInt64(documento).ToString(@"00\.000\.000\/0000\-00")
                : Convert.ToUInt64(documento).ToString(@"000\.000\.000\-00");
        }
    }

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Append<T>(
            this IEnumerable<T> source, params T[] tail)
        {
            return source.Concat(tail);
        }
    }
}