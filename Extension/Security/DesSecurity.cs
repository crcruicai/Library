/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/12/30 11:29:43
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CRC.Security
{
   

    /// <summary>
    ///     封装DES加密与解密算法.
    /// </summary>
    public static class DesSecurity
    {
        /// <summary>
        /// 默认密钥向量
        /// </summary>
        private static readonly byte[] _Keys = { 0x10, 0x24, 0x51, 0x78, 0x80, 0xAC, 0xCD, 0xEF };

        #region 自定义密匙和向量

        /// <summary>
        ///     对字节进行DES加密
        /// </summary>
        /// <param name="pValue">字节数组</param>
        /// <param name="pKey">钥匙 可以使用 System.Text.Encoding.AscII.GetBytes("ABVD") 注意必须是8位 </param>
        /// <param name="vector">向量 如果为NULL 向量和钥匙是一个</param>
        /// <returns>加密后的BYTE</returns>
        public static byte[] EncoderBytes(byte[] pValue, byte[] pKey, byte[] vector)
        {
            byte[] rgbKey = pKey;
            byte[] rgbIv = vector;

            if (rgbKey == null || rgbKey.Length != 8)
                rgbKey = _Keys;
            if (rgbIv == null)
                rgbIv = rgbKey;

            var desc = new DESCryptoServiceProvider();
            ICryptoTransform iCrypto = desc.CreateEncryptor(rgbKey, rgbIv);

            return iCrypto.TransformFinalBlock(pValue, 0, pValue.Length);
        }

        /// <summary>
        ///     对字节进行DES解密
        /// </summary>
        /// <param name="value">字节数组</param>
        /// <param name="key">钥匙 可以使用 System.Text.Encoding.AscII.GetBytes("ABVD") 注意必须是8位 </param>
        /// <param name="vector">向量 如果为NULL 向量和钥匙是一个</param>
        /// <returns>解密后的BYTE</returns>
        public static byte[] DecoderBytes(byte[] value, byte[] key, byte[] vector)
        {
            byte[] rgbKey = key;
            byte[] rgbIv = vector;

            if (rgbKey == null || rgbKey.Length != 8)
                rgbKey = _Keys;
            if (rgbIv == null)
                rgbIv = rgbKey;

            var desc = new DESCryptoServiceProvider();
            ICryptoTransform iCrypto = desc.CreateDecryptor(rgbKey, rgbIv);

            return iCrypto.TransformFinalBlock(value, 0, value.Length);
        }


        /// <summary>
        ///     DES加密
        /// </summary>
        /// <param name="textValue">原始数据</param>
        /// <param name="textEncoding">数据编码</param>
        /// <param name="key">钥匙 可以使用 System.Text.Encoding.AscII.GetBytes("ABVD") 注意必须是8位 </param>
        /// <param name="vector">向量 如果为NULL 向量和钥匙是一个</param>
        /// <returns>加密后的字符串 00-00-00</returns>
        public static string EncoderStringByKey(string textValue, Encoding textEncoding, byte[] key, byte[] vector)
        {
            byte[] dataByte = textEncoding.GetBytes(textValue);
            return BitConverter.ToString(EncoderBytes(dataByte, key, vector));
        }

        /// <summary>
        ///     DES解密
        /// </summary>
        /// <param name="textValue">经过加密数据</param>
        /// <param name="textEncoding">数据编码</param>
        /// <param name="key">钥匙 可以使用 System.Text.Encoding.AscII.GetBytes("ABVD") 注意必须是8位 </param>
        /// <param name="vector">向量 如果为NULL 向量和钥匙是一个</param>
        /// <returns>解密后的字符穿</returns>
        public static string DecoderStringByKey(string textValue, Encoding textEncoding, byte[] key, byte[] vector)
        {

            string[] byteText = textValue.Split('-');
            var dataByte = new byte[byteText.Length];
            for (int i = 0; i != byteText.Length; i++)
                dataByte[i] = Convert.ToByte(byteText[i], 16);
            return textEncoding.GetString(DecoderBytes(dataByte, key, vector));
        }

        #endregion

        #region 默认密匙和向量




        /// <summary>
        ///     DES加密
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns>
        public static string EncryptString(string encryptString, string encryptKey)
        {

            if (encryptKey.Length != 8)
                throw new ArgumentException("encryptKey is length must be 8");
            byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey);
            byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
            var dCsp = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, _Keys), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());

        }

        /// <summary>
        ///     DES解密
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns>
        public static string DecryptString(string decryptString, string decryptKey)
        {
            if (decryptKey.Length != 8)
                throw new ArgumentException("decryptKey length must be 8");
            byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
            byte[] inputByteArray = Convert.FromBase64String(decryptString);
            var dcsp = new DESCryptoServiceProvider();
            var mStream = new MemoryStream();
            var cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, _Keys), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());

        }

        #endregion

        #region 文件加密与解密

        /// <summary>
        ///     使用DES加密文件.
        ///     Encrypt files
        ///     Attention:key must be 8 bits
        /// </summary>
        /// <param name="inFilePath">
        ///     <para>指定加密文件的路径.</para>
        ///     Encrypt file path
        /// </param>
        /// <param name="outFilePath">
        ///     <para>指的输出文件的路径.</para>
        ///     output file
        /// </param>
        /// <param name="strEncrKey">
        ///     <para>指定密匙,密匙的长度必须为8.</para>
        ///     key
        /// </param>
        public static void EncryptFile(string inFilePath, string outFilePath, string strEncrKey)
        {
            byte[] byKey = null;

            byKey = Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8));
            var fin = new FileStream(inFilePath, FileMode.Open, FileAccess.Read);
            var fout = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);
            //Create variables to help with read and write.
            var bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0; //This is the total number of bytes written.
            long totlen = fin.Length; //This is the total length of the input file.
            DES des = new DESCryptoServiceProvider();
            var encStream = new CryptoStream(fout, des.CreateEncryptor(byKey, _Keys), CryptoStreamMode.Write);
            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                int len = fin.Read(bin, 0, 100); //This is the number of bytes to be written at a time.
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }
            encStream.Close();
            fout.Close();
            fin.Close();
        }

        /// <summary>
        ///     解密文件.
        ///     Decrypt files
        ///     Attention:key must be 8 bits
        /// </summary>
        /// <param name="inFilePath">Decrypt filepath</param>
        /// <param name="outFilePath">output filepath</param>
        /// <param name="sDecrKey">key</param>
        public static void DecryptFile(string inFilePath, string outFilePath, string sDecrKey)
        {


            byte[] byKey = Encoding.UTF8.GetBytes(sDecrKey.Substring(0, 8));
            var fin = new FileStream(inFilePath, FileMode.Open, FileAccess.Read);
            var fout = new FileStream(outFilePath, FileMode.OpenOrCreate, FileAccess.Write);
            fout.SetLength(0);
            //Create variables to help with read and write.
            var bin = new byte[100]; //This is intermediate storage for the encryption.
            long rdlen = 0; //This is the total number of bytes written.
            long totlen = fin.Length; //This is the total length of the input file.
            DES des = new DESCryptoServiceProvider();
            var encStream = new CryptoStream(fout, des.CreateDecryptor(byKey, _Keys), CryptoStreamMode.Write);
            //Read from the input file, then encrypt and write to the output file.
            while (rdlen < totlen)
            {
                int len = fin.Read(bin, 0, 100); //This is the number of bytes to be written at a time.
                encStream.Write(bin, 0, len);
                rdlen = rdlen + len;
            }
            encStream.Close();
            fout.Close();
            fin.Close();
        }

        #endregion
    }

}
