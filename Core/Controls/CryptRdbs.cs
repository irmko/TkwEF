using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
//using System.Web.Security;

namespace TkwEF.Core.Security
{
    public class CryptRdbs
    {
        private CryptRdbs() { }

        /// <summary>
        /// Возвращает случайную строку соли заданного размера
        /// </summary>
        /// <param name="Size"></param>
        /// <returns></returns>
        public static string CreateSalt(int Size)
        {
            // Generate a cryptographic random number using the cryptographic
            // service provider
            RNGCryptoServiceProvider RNG = new RNGCryptoServiceProvider();
            byte[] Buffer = new byte[Size];
            RNG.GetBytes(Buffer);
            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(Buffer);
        }

        /// <summary>
        /// Возвращает хеш пароля и соли по SHA1
        /// </summary>
        /// <param name="_Password"></param>
        /// <param name="_Salt"></param>
        /// <returns></returns>
        public static string CreatePasswordHash(string _Password, string _Salt)
        {
            string SaltAndPwd = String.Concat(_Password, _Salt);
            //string HashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(SaltAndPwd, "SHA1");
            string HashedPwd = GetHashPasswordSHA1(SaltAndPwd);
            return HashedPwd;
        }

        //private class MembershipWeb : System.Web.Security.SqlMembershipProvider
        //{
        //    public string Encrypt()
        //    {
        //        byte[]bytes=base.base.EncryptPassword
        //    }
        //}

        private static string GetHashPasswordSHA1(string password)
        {
            byte[] data = Encoding.Unicode.GetBytes(password);
            byte[] result;
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            result = sha1.ComputeHash(data);
            //return Convert.ToBase64String(result, Base64FormattingOptions.InsertLineBreaks);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
                sb.AppendFormat("{0:x2}", result[i]);

            return sb.ToString();
        }
    }
}
