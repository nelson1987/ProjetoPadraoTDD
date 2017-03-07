using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebForLink.CrossCutting.Criptografia
{

    public class ParametroCriptografia
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public enum EnumCripto
    {
        Descriptografar = 1,
        Criptografar = 2
    }
    public class CriptografiaException : Exception
    {
        public CriptografiaException(string mensagem) : base(mensagem) { }
        public CriptografiaException(string mensagem, Exception innerException) : base(mensagem, innerException) { }
    }
    public class Criptografia
    {
        public Criptografia(EnumCripto tipo, string palavra, string chave)
        {
            Tipo = tipo;
            Palavra = palavra;
            Chave = chave;
            Validar();
            if (Tipo == EnumCripto.Criptografar)
                Encriptografar();
            if (Tipo == EnumCripto.Descriptografar)
                Descriptografar();
        }
        public EnumCripto Tipo { get; private set; }
        public string Palavra { get; private set; }
        public string Chave { get; private set; }
        public string Resultado { get; private set; }
        public void Validar()
        {
            if (string.IsNullOrEmpty(Palavra))
                throw new CriptografiaException("Deve-se ter uma palavra para criptografia");
            if (string.IsNullOrEmpty(Palavra))
                throw new CriptografiaException("Deve-se ter uma chave para criptografia");
        }
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private byte[] _key = { };
        private readonly byte[] _iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        public void Descriptografar()
        {
            Palavra = PrepareUrl(Palavra);

            byte[] inputByteArray = new byte[Palavra.Length + 1];
            try
            {
                _key = Encoding.UTF8.GetBytes(Chave);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(Palavra);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(_key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                Resultado = encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("Erro ao Descriptografar", ex);
            }
        }

        public void Encriptografar()
        {
            try
            {
                _key = Encoding.UTF8.GetBytes(Chave);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(Palavra);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                string c = Convert.ToBase64String(ms.ToArray());
                c = c.Replace("+", "_11_").Replace("/", "_22_");
                Resultado = c;
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("Erro ao Encriptografar", ex);
            }
        }

        public List<ParametroCriptografia> ReadUrl(string pUrl, string key)
        {
            //url = url.Substring(url.IndexOf('?') + 1);
            //var url = Descriptografar();
            var url = Resultado;

            List<ParametroCriptografia> param = new List<ParametroCriptografia>();

            if (url.Trim() != "")
            {
                string[] arrMsgs = url.Split('&');
                string[] arrIndMsg;

                if (arrMsgs.Length > 0)
                {
                    foreach (string s in arrMsgs)
                    {
                        arrIndMsg = s.Split('=');
                        param.Add(new ParametroCriptografia { Name = arrIndMsg[0].Trim(), Value = arrIndMsg[1].Trim() });
                    }
                }
                else
                {
                    arrIndMsg = pUrl.Split('=');
                    if (arrIndMsg.Length > 0)
                        param.Add(new ParametroCriptografia { Name = arrIndMsg[0].Trim(), Value = arrIndMsg[1].Trim() });
                }
            }
            return param;
        }

        public string PrepareUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
                url = url.Replace("_22_", "/").Replace("_11_", "+");

            return url;
        }
    }
}
