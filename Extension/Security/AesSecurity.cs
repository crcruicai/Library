// /*********************************************************
// * 开发人员：TopC
// * 创建时间：2013-12-30 11:26
// * 描述说明：
// *
// * 更改历史：
// *
// * *******************************************************/

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CRC.Security
{
    /// <summary>
    ///     AES(对称加密算法) 加密解密
    /// </summary>
    public static class AesSecurity
    {
        
        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static readonly byte[] _Keys = {0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F};

        #region 字符串 加密与解密 

        /// <summary>
        ///     AES字符串加密,返回Base64字符串.
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为32位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptString(string encryptString, string encryptKey)
        {
            if (!CheckString(encryptKey, 32))
                throw new ArgumentException("encryptKey is null or length of encryptKey must be 32 ");
            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            var rijndaelProvider = new RijndaelManaged {Key = Encoding.UTF8.GetBytes(encryptKey), IV = _Keys};
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
            return Convert.ToBase64String(encryptedData);

        }

        /// <summary>
        ///     AES字符串解密
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为32位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串</returns>
        public static string DecryptString(string decryptString, string decryptKey)
        {

            if (!CheckString(decryptKey, 32))
                throw new ArgumentException("decryptKey is null or length of decryptKey must be 32 ");
            byte[] inputData = Convert.FromBase64String(decryptString);
            var rijndaelProvider = new RijndaelManaged {Key = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 32)), IV = _Keys};
            ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();
            byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
            return Encoding.UTF8.GetString(decryptedData);
        }

        #endregion


        #region 对称加密算法AES RijndaelManaged加密解密
        private static readonly string _DefaultAesKey = "@#kim123";
        
        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <returns>加密结果字符串</returns>
        public static string EncryptBase64String(string encryptString)
        {
            return EncryptBase64String(encryptString, _DefaultAesKey.PadRight(32,'A'));
        }
        /// <summary>
        /// 对称加密算法AES RijndaelManaged加密(RijndaelManaged（AES）算法是块式加密算法)
        /// </summary>
        /// <param name="encryptString">待加密字符串</param>
        /// <param name="encryptKey">加密密钥，须半角字符</param>
        /// <returns>加密结果字符串</returns>
        public static string EncryptBase64String(string encryptString, string encryptKey)
        {
            if (!CheckString(encryptKey, 32))
                throw new ArgumentException("encryptKey is null or length of encryptKey must be 32 ");
            RijndaelManaged rijndaelProvider = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(encryptKey), IV = _Keys
            };
            ICryptoTransform rijndaelEncrypt = rijndaelProvider.CreateEncryptor();
            byte[] inputData = Encoding.UTF8.GetBytes(encryptString);
            byte[] encryptedData = rijndaelEncrypt.TransformFinalBlock(inputData, 0, inputData.Length);
            return Convert.ToBase64String(encryptedData);
        }
        /// <summary>
        /// 对称加密算法AES RijndaelManaged解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns>解密成功返回解密后的字符串</returns>
        public static string DecryptBase64String(string decryptString)
        {
            return DecryptBase64String(decryptString, _DefaultAesKey.PadRight(32, 'A'));
        }
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返回空</returns>
        public static string DecryptBase64String(string decryptString, string decryptKey)
        {
            try
            {
                if (!CheckString(decryptKey, 32))
                    throw new ArgumentException("decryptKey is null or length of decryptKey must be 32 ");
                RijndaelManaged rijndaelProvider = new RijndaelManaged
                {
                    Key = Encoding.UTF8.GetBytes(decryptKey), IV = _Keys
                };
                ICryptoTransform rijndaelDecrypt = rijndaelProvider.CreateDecryptor();
                byte[] inputData = Convert.FromBase64String(decryptString);
                byte[] decryptedData = rijndaelDecrypt.TransformFinalBlock(inputData, 0, inputData.Length);
                return Encoding.UTF8.GetString(decryptedData);
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 检查字符串是否为null或不符合指定的长度.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        private static bool CheckString(string source, int length)
        {
            if(string.IsNullOrEmpty(source))
                return false;
            return source.Length == length;
        }
        /// <summary>
        /// 加密文件流
        /// </summary>
        /// <param name="fs">要加密的文件流</param>
        /// <param name="decryptKey">密钥,注意长度为32位.</param>
        /// <returns>返回加密的流</returns>
        public static CryptoStream EncryptStream(FileStream fs, string decryptKey)
        {
            if(!CheckString(decryptKey, 32))
                throw new ArgumentException("decryptKey is null or length of decryptKey must be 32 ");
            RijndaelManaged rijndaelProvider = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(decryptKey), IV = _Keys
            };
            ICryptoTransform encrypto = rijndaelProvider.CreateEncryptor();
            CryptoStream cytptostreamEncr = new CryptoStream(fs, encrypto, CryptoStreamMode.Write);
            return cytptostreamEncr;
        }

        /// <summary>
        /// 解密文件流
        /// </summary>
        /// <param name="fs">要解密的文件流.</param>
        /// <param name="decryptKey">密钥,长度为32位.</param>
        /// <returns>返回加密的流.</returns>
        public static CryptoStream DecryptStream(FileStream fs, string decryptKey)
        {
            if (!CheckString(decryptKey, 32))
                throw new ArgumentException("decryptKey is null or length of decryptKey must be 32 ");
            RijndaelManaged rijndaelProvider = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(decryptKey), IV = _Keys
            };
            ICryptoTransform decrypto = rijndaelProvider.CreateDecryptor();
            CryptoStream cytptostreamDecr = new CryptoStream(fs, decrypto, CryptoStreamMode.Read);
            return cytptostreamDecr;
        }

        /// <summary>
        /// 对指定文件加密
        /// </summary>
        /// <param name="inputFile">指定输入文件的路径</param>
        /// <param name="outputFile">指定输出文件的路径.</param>
        /// <param name="decryptKey">密钥,长度为32位.</param>
        /// <returns></returns>
        public static bool EncryptFile(string inputFile, string outputFile,string decryptKey)
        {
            try
            {
                if (!CheckString(decryptKey, 32))
                    throw new ArgumentException("decryptKey is null or length of decryptKey must be 32 ");
                FileStream fr = new FileStream(inputFile, FileMode.Open);
                FileStream fren = new FileStream(outputFile, FileMode.Create);
                CryptoStream enfr = EncryptStream(fren, decryptKey);
                byte[] bytearrayinput = new byte[fr.Length];
                fr.Read(bytearrayinput, 0, bytearrayinput.Length);
                enfr.Write(bytearrayinput, 0, bytearrayinput.Length);
                enfr.Close();
                fr.Close();
                fren.Close();
            }
            catch
            {
                //文件异常
                return false;
            }
            return true;
        }

        /// <summary>
        /// 对指定的文件解密.
        /// </summary>
        /// <param name="inputFile">输入文件的路径.</param>
        /// <param name="outputFile">输出文件的路径.</param>
        /// <param name="decryptKey">密钥,长度为32位</param>
        /// <returns>如果解密成功,返回true,不成功返回false</returns>
        public static bool DecryptFile(string inputFile, string outputFile,string decryptKey)
        {
            try
            {
                if (!CheckString(decryptKey, 32))
                    throw new ArgumentException("decryptKey is null or length of decryptKey must be 32 ");
                FileStream fr = new FileStream(inputFile, FileMode.Open);
                FileStream frde = new FileStream(outputFile, FileMode.Create);
                CryptoStream defr = DecryptStream(fr, decryptKey);
                byte[] bytearrayoutput = new byte[1024];
                do
                {
                    int mCount = defr.Read(bytearrayoutput, 0, bytearrayoutput.Length);
                    frde.Write(bytearrayoutput, 0, mCount);
                    if (mCount < bytearrayoutput.Length)
                        break;
                } while (true);
                defr.Close();
                fr.Close();
                frde.Close();
            }
            catch
            {
                //文件异常
                return false;
            }
            return true;
        }
        #endregion
    }
}