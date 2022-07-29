using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace LockedUtil
{
    public class LockedHelper
    {
        /// <summary>
        /// 自定义加密算法
        /// </summary>
        /// <param name="str"></param>
        /// <param name="interval"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public string EncryptStr(string str, int interval, string value)
        {
            for (int i = interval; i < str.Length; i += interval + 1)
                str = str.Insert(i, value);
            return str;
        }
        
        /// <summary>
        /// RAS加密。
        /// </summary>
        public string RSAEncrypt(string originalString,string key)
        {
            var result = string.Empty;
            CspParameters param = new CspParameters();
            param.KeyContainerName = key;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] plaindata = Encoding.Default.GetBytes(originalString);
                byte[] encryptdata = rsa.Encrypt(plaindata, true);

                result = Convert.ToBase64String(encryptdata) + ":" + key;
            }

            return result;

        }

        /// <summary>
        /// RAS解密。
        /// </summary>
        public string RSADecrypt(string securitylString,string key)
        {
            CspParameters param = new CspParameters();
            param.KeyContainerName = key;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(securitylString);
                byte[] decryptdata = rsa.Decrypt(encryptdata, true);
                return Encoding.Default.GetString(decryptdata);
            }
        }
        
    }
}
