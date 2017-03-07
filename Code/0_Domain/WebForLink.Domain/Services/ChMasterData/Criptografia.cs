using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebForLink.Domain.Infrastructure
{
    public class ParametroCriptografia
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public enum EnumCripto
    {
        Descriptografar = 1,
        Criptografar = 2,
        LinkDescriptografar = 3
    }

    [Serializable]
    public class CriptografiaException : Exception
    {
        public CriptografiaException(string mensagem) : base(mensagem)
        {
        }

        public CriptografiaException(string mensagem, Exception innerException) : base(mensagem, innerException)
        {
        }
    }

    public class Criptografia
    {
        private readonly byte[] _iv = {0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef};
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private byte[] _key = {};

        public Criptografia(EnumCripto tipo, string palavra, string chave)
        {
            Tipo = tipo;
            Palavra = palavra;
            Chave = chave;
            Resultados = new Dictionary<string, string>();
            Validar();
            if (Tipo == EnumCripto.Criptografar)
                Encriptografar();
            if (Tipo == EnumCripto.Descriptografar)
                Descriptografar();
            if (Tipo == EnumCripto.LinkDescriptografar)
                Resultados = ReadUrl(palavra, chave);
        }

        public EnumCripto Tipo { get; private set; }
        public string Palavra { get; private set; }
        public string Chave { get; private set; }
        public string Resultado { get; private set; }
        public Dictionary<string, string> Resultados { get; private set; }

        public void Validar()
        {
            if (string.IsNullOrEmpty(Palavra))
                throw new CriptografiaException("Deve-se ter uma palavra para criptografia");
            if (string.IsNullOrEmpty(Chave))
                throw new CriptografiaException("Deve-se ter uma chave para criptografia");
        }

        public void Descriptografar()
        {
            Palavra = PrepareUrl(Palavra);

            var inputByteArray = new byte[Palavra.Length + 1];
            try
            {
                _key = Encoding.UTF8.GetBytes(Chave);
                var des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(Palavra);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(_key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                var encoding = Encoding.UTF8;
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
                var des = new DESCryptoServiceProvider();
                var inputByteArray = Encoding.UTF8.GetBytes(Palavra);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms,
                    des.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                var c = Convert.ToBase64String(ms.ToArray());
                c = c.Replace("+", "_11_").Replace("/", "_22_");
                Resultado = c;
            }
            catch (Exception ex)
            {
                throw new CriptografiaException("Erro ao Encriptografar", ex);
            }
        }

        /*
        public List<ParametroCriptografia> ReadUrl(string pUrl, string key)
        {
            //url = url.Substring(url.IndexOf('?') + 1);
            var url = Decrypt(pUrl, key);

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
        */

        public Dictionary<string, string> ReadUrl(string pUrl, string key)
        {
            Descriptografar();
            //url = url.Substring(url.IndexOf('?') + 1);
            //var url = Descriptografar();
            var url = Resultado;

            var param = new Dictionary<string, string>();

            if (url.Trim() != "")
            {
                var arrMsgs = url.Split('&');
                string[] arrIndMsg;

                if (arrMsgs.Length > 0)
                {
                    foreach (var s in arrMsgs)
                    {
                        arrIndMsg = s.Split('=');
                        param.Add(arrIndMsg[0].Trim(), arrIndMsg[1].Trim());
                    }
                }
                else
                {
                    arrIndMsg = pUrl.Split('=');
                    if (arrIndMsg.Length > 0)
                        param.Add(arrIndMsg[0].Trim(), arrIndMsg[1].Trim());
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

    //public class EncryptDecryptQueryString
    //{
    //    private readonly byte[] _iv = {0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef};
    //    //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //    private byte[] _key = {};

    //    public string Descriptografar(string stringToDecrypt, string sEncryptionKey)
    //    {
    //        stringToDecrypt = CriarUrlCriptografa(stringToDecrypt);

    //        var inputByteArray = new byte[stringToDecrypt.Length + 1];
    //        try
    //        {
    //            _key = Encoding.UTF8.GetBytes(sEncryptionKey);
    //            var des = new DESCryptoServiceProvider();
    //            inputByteArray = Convert.FromBase64String(stringToDecrypt);
    //            var ms = new MemoryStream();
    //            var cs = new CryptoStream(ms, des.CreateDecryptor(_key, _iv), CryptoStreamMode.Write);
    //            cs.Write(inputByteArray, 0, inputByteArray.Length);
    //            cs.FlushFinalBlock();
    //            var encoding = Encoding.UTF8;
    //            return encoding.GetString(ms.ToArray());
    //        }
    //        catch (Exception ex)
    //        {
    //            //Log.Error(ex);
    //            return ex.Message;
    //        }
    //    }

    //    public string Criptografar(string stringToEncrypt, string sEncryptionKey)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrEmpty(stringToEncrypt))
    //                throw new CriptografiaException("Dado para ser criptografado não pode vazio ou nulo");
    //            if (string.IsNullOrEmpty(sEncryptionKey))
    //                throw new CriptografiaException("A senha deve está preenchida não pode vazio ou nulo");

    //            _key = Encoding.UTF8.GetBytes(sEncryptionKey);
    //            var des = new DESCryptoServiceProvider();
    //            var inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
    //            var ms = new MemoryStream();
    //            var cs = new CryptoStream(ms,
    //                des.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
    //            cs.Write(inputByteArray, 0, inputByteArray.Length);
    //            cs.FlushFinalBlock();

    //            var c = Convert.ToBase64String(ms.ToArray());
    //            c = c.Replace("+", "_11_").Replace("/", "_22_");
    //            return c;
    //        }
    //        catch (Exception e)
    //        {
    //            //Log.Error(e);
    //            throw new CriptografiaException("Um erro ocorreu durante a criptografia", e);
    //        }
    //    }

    //    public List<ParametroCriptografia> DescriptografarUrl(string pUrl, string key)
    //    {
    //        try
    //        {
    //            if (string.IsNullOrEmpty(pUrl))
    //                throw new CriptografiaException("O link para ser descriptografado não pode ser vazio ou nulo");
    //            if (string.IsNullOrEmpty(key))
    //                throw new CriptografiaException("A senha deve está preenchida não pode ser vazio ou nulo");

    //            //url = url.Substring(url.IndexOf('?') + 1);
    //            var url = Descriptografar(pUrl, key);

    //            var param = new List<ParametroCriptografia>();

    //            if (url.Trim() != "")
    //            {
    //                var arrMsgs = url.Split('&');
    //                string[] arrIndMsg;

    //                if (arrMsgs.Length > 0)
    //                {
    //                    foreach (var s in arrMsgs)
    //                    {
    //                        arrIndMsg = s.Split('=');
    //                        param.Add(new ParametroCriptografia
    //                        {
    //                            Name = arrIndMsg[0].Trim(),
    //                            Value = arrIndMsg[1].Trim()
    //                        });
    //                    }
    //                }
    //                else
    //                {
    //                    arrIndMsg = pUrl.Split('=');
    //                    if (arrIndMsg.Length > 0)
    //                        param.Add(new ParametroCriptografia
    //                        {
    //                            Name = arrIndMsg[0].Trim(),
    //                            Value = arrIndMsg[1].Trim()
    //                        });
    //                }
    //            }
    //            return param;
    //        }
    //        catch (Exception e)
    //        {
    //            //Log.Error(e);
    //            throw new CriptografiaException("Um erro ocorreu durante a criptografia", e);
    //        }
    //    }

    //    public string CriarUrlCriptografa(string url)
    //    {
    //        if (!string.IsNullOrEmpty(url))
    //            url = url.Replace("_22_", "/").Replace("_11_", "+");

    //        return url;
    //    }
    //}
}