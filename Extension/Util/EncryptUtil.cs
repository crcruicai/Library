/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/2 10:49:11
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace CRC.Util
{
    /// <summary>
    /// 加密 算法帮助类.
    /// </summary>
    public class EncryptUtil
    {
        /// <summary>
        /// 给定密码,加密公钥.使用TripleDES进行加密.
        /// </summary>
        /// <param name="pass">密码.</param>
        /// <returns>输出公钥.</returns>
        public static string TripleDESEncrypt(string pass)
        {

            try
            {

                byte[] bt = (new System.Text.UnicodeEncoding()).GetBytes(pass);

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(pass, null);

                byte[] key = pdb.GetBytes(24);

                byte[] iv = pdb.GetBytes(8);

                MemoryStream ms = new MemoryStream();

                TripleDESCryptoServiceProvider tdesc = new TripleDESCryptoServiceProvider();

                CryptoStream cs = new CryptoStream(ms, tdesc.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                cs.Write(bt, 0, bt.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());

            }

            catch (Exception ex)
            {

                throw ex;

            }

        }

        //使用： 

        //string str = Encrypt("bbb"); 

        //Console.WriteLine(Decrypt(str, "bbb")); 
        
        /// <summary>
        /// 解密，使用密码产生加密算法的公钥，并使用TripleDES对加密数据进行解密。
        /// <example >
        /// <code >
        /// string str=EncryptUtil.TripleDESEncrypt("bbb");
        /// Console.WriteLine(EncryptUtil.TripleDESDecrypt(str, "bbb"));
        /// </code>
        /// 
        /// </example>
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static string TripleDESDecrypt(string str, string pass)
        {

            try
            {

                byte[] bt = Convert.FromBase64String(str);

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(pass, null);

                byte[] key = pdb.GetBytes(24);

                byte[] iv = pdb.GetBytes(8);

                MemoryStream ms = new MemoryStream();

                TripleDESCryptoServiceProvider tdesc = new TripleDESCryptoServiceProvider();

                CryptoStream cs = new CryptoStream(ms, tdesc.CreateDecryptor(key, iv), CryptoStreamMode.Write);

                cs.Write(bt, 0, bt.Length);

                cs.FlushFinalBlock();

                return (new System.Text.UnicodeEncoding()).GetString(ms.ToArray());

            }

            catch (Exception ex)
            {

                throw ex;

            }

        }

        
        /// <summary>
        /// 加密，使用密码产生加密算法的公钥，并使用TripleDES对密码进行加密。 
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="p_key"></param>
        /// <returns></returns>
        public static string EncryptWithKey(string pass, string p_key)
        {

            try
            {

                byte[] bt = (new System.Text.UnicodeEncoding()).GetBytes(pass);

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(p_key, null);

                byte[] key = pdb.GetBytes(24);

                byte[] iv = pdb.GetBytes(8);

                MemoryStream ms = new MemoryStream();

                TripleDESCryptoServiceProvider tdesc = new TripleDESCryptoServiceProvider();

                CryptoStream cs = new CryptoStream(ms, tdesc.CreateEncryptor(key, iv), CryptoStreamMode.Write);

                cs.Write(bt, 0, bt.Length);

                cs.FlushFinalBlock();

                return Convert.ToBase64String(ms.ToArray());

            }

            catch (Exception ex)
            {

                throw ex;

            }

        }



        // 
        /// <summary>
        /// 解密，使用密码产生加密算法的公钥，并使用TripleDES对加密数据进行解密。
        /// </summary>
        /// <param name="str"></param>
        /// <param name="p_key"></param>
        /// <returns></returns>
        public static string DecryptWithKey(string str, string p_key)
        {

            try
            {

                byte[] bt = Convert.FromBase64String(str);

                PasswordDeriveBytes pdb = new PasswordDeriveBytes(p_key, null);

                byte[] key = pdb.GetBytes(24);

                byte[] iv = pdb.GetBytes(8);

                MemoryStream ms = new MemoryStream();

                TripleDESCryptoServiceProvider tdesc = new TripleDESCryptoServiceProvider();

                CryptoStream cs = new CryptoStream(ms, tdesc.CreateDecryptor(key, iv), CryptoStreamMode.Write);

                cs.Write(bt, 0, bt.Length);

                cs.FlushFinalBlock();

                return (new System.Text.UnicodeEncoding()).GetString(ms.ToArray());

            }

            catch (Exception ex)
            {

                throw ex;

            }

        }


    }


    public class JhEncrypt
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public JhEncrypt()
        {
        }
        /// <summary>
        /// 使用缺省密钥字符串加密
        /// </summary>
        /// <param name="original">明文</param>
        /// <returns>密文</returns>
        public static string Encrypt(string original)
        {
            return Encrypt(original, "JASONHEUNG");
        }
        /// <summary>
        /// 使用缺省密钥解密
        /// </summary>
        /// <param name="original">密文</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original)
        {
            return Decrypt(original, "JASONHEUNG", System.Text.Encoding.Default);
        }
        /// <summary>
        /// 使用给定密钥解密
        /// </summary>
        /// <param name="original">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original, string key)
        {
            return Decrypt(original, key, System.Text.Encoding.Default);
        }
        /// <summary>
        /// 使用缺省密钥解密,返回指定编码方式明文
        /// </summary>
        /// <param name="original">密文</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>明文</returns>
        public static string Decrypt(string original, Encoding encoding)
        {
            return Decrypt(original, "JASONHEUNG", encoding);
        }
        /// <summary>
        /// 使用给定密钥加密
        /// </summary>
        /// <param name="original">原始文字</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>密文</returns>
        public static string Encrypt(string original, string key)
        {
            byte[] buff = System.Text.Encoding.Default.GetBytes(original);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return Convert.ToBase64String(Encrypt(buff, kb));
        }
        /// <summary>
        /// 使用给定密钥解密
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <param name="encoding">字符编码方案</param>
        /// <returns>明文</returns>
        public static string Decrypt(string encrypted, string key, Encoding encoding)
        {
            byte[] buff = Convert.FromBase64String(encrypted);
            byte[] kb = System.Text.Encoding.Default.GetBytes(key);
            return encoding.GetString(Decrypt(buff, kb));
        }
        /// <summary>
        /// 生成MD5摘要
        /// </summary>
        /// <param name="original">数据源</param>
        /// <returns>摘要</returns>
        public static byte[] MakeMD5(byte[] original)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            byte[] keyhash = hashmd5.ComputeHash(original);
            hashmd5 = null;
            return keyhash;
        }
        /// <summary>
        /// 使用给定密钥加密
        /// </summary>
        /// <param name="original">明文</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] original, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;

            return des.CreateEncryptor().TransformFinalBlock(original, 0, original.Length);
        }
        /// <summary>
        /// 使用给定密钥解密数据
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] encrypted, byte[] key)
        {
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
            des.Key = MakeMD5(key);
            des.Mode = CipherMode.ECB;
            return des.CreateDecryptor().TransformFinalBlock(encrypted, 0, encrypted.Length);
        }

        /// <summary>
        /// 使用给定密钥加密
        /// </summary>
        /// <param name="original">原始数据</param>
        /// <param name="key">密钥</param>
        /// <returns>密文</returns>
        public static byte[] Encrypt(byte[] original)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("JASONHEUNG");
            return Encrypt(original, key);
        }
        /// <summary>
        /// 使用缺省密钥解密数据
        /// </summary>
        /// <param name="encrypted">密文</param>
        /// <param name="key">密钥</param>
        /// <returns>明文</returns>
        public static byte[] Decrypt(byte[] encrypted)
        {
            byte[] key = System.Text.Encoding.Default.GetBytes("JASONHEUNG");
            return Decrypt(encrypted, key);
        }

    }

}
