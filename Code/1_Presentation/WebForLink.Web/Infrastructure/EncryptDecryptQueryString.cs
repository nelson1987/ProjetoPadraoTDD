using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace WebForLink.Web.Infrastructure
{
    public class Param {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class EncryptDecryptQueryString
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private byte[] _key = { };
        private readonly byte[] _iv = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        public string Decrypt(string stringToDecrypt, string sEncryptionKey)
        {
            stringToDecrypt = PrepareUrl(stringToDecrypt);
            
            byte[] inputByteArray = new byte[stringToDecrypt.Length + 1];
            try
            {
                _key = Encoding.UTF8.GetBytes(sEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                inputByteArray = Convert.FromBase64String(stringToDecrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(_key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                Encoding encoding = Encoding.UTF8;
                return encoding.GetString(ms.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(ex);
                return "";
            }
        }

        public string Encrypt(string stringToEncrypt, string sEncryptionKey)
        {
            try
            {
                _key = Encoding.UTF8.GetBytes(sEncryptionKey);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms,
                  des.CreateEncryptor(_key, _iv), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                string c = Convert.ToBase64String(ms.ToArray());
                c = c.Replace("+", "_11_").Replace("/", "_22_");
                return c;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return e.Message;
            }
        }

        public List<Param> ReadUrl(string pUrl, string key)
        {
            //url = url.Substring(url.IndexOf('?') + 1);
            var url = Decrypt(pUrl, key);

            List<Param> param = new List<Param>();

            if (url.Trim() != "")
            {
                string[] arrMsgs = url.Split('&');
                string[] arrIndMsg;

                if (arrMsgs.Length > 0)
                {
                    foreach (string s in arrMsgs)
                    {
                        arrIndMsg = s.Split('=');
                        param.Add(new Param { Name = arrIndMsg[0].Trim(), Value = arrIndMsg[1].Trim() });
                    }
                }
                else 
                {
                    arrIndMsg = pUrl.Split('=');
                    if (arrIndMsg.Length > 0)
                        param.Add(new Param { Name = arrIndMsg[0].Trim(), Value = arrIndMsg[1].Trim() });
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