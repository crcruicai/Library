// /*********************************************************
// * ������Ա��TopC
// * ����ʱ�䣺2013-12-30 11:26
// * ����˵����
// *
// * ������ʷ��
// *
// * *******************************************************/

using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CRC.Security
{
    /// <summary>
    ///     AES(�ԳƼ����㷨) ���ܽ���
    /// </summary>
    public static class AesSecurity
    {
        
        /// <summary>
        /// Ĭ����Կ����
        /// </summary>
        private static readonly byte[] _Keys = {0x41, 0x72, 0x65, 0x79, 0x6F, 0x75, 0x6D, 0x79, 0x53, 0x6E, 0x6F, 0x77, 0x6D, 0x61, 0x6E, 0x3F};

        #region �ַ��� ��������� 

        /// <summary>
        ///     AES�ַ�������,����Base64�ַ���.
        /// </summary>
        /// <param name="encryptString">�����ܵ��ַ���</param>
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ32λ</param>
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns>
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
        ///     AES�ַ�������
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ32λ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���</returns>
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


        #region �ԳƼ����㷨AES RijndaelManaged���ܽ���
        private static readonly string _DefaultAesKey = "@#kim123";
        
        /// <summary>
        /// �ԳƼ����㷨AES RijndaelManaged����(RijndaelManaged��AES���㷨�ǿ�ʽ�����㷨)
        /// </summary>
        /// <param name="encryptString">�������ַ���</param>
        /// <returns>���ܽ���ַ���</returns>
        public static string EncryptBase64String(string encryptString)
        {
            return EncryptBase64String(encryptString, _DefaultAesKey.PadRight(32,'A'));
        }
        /// <summary>
        /// �ԳƼ����㷨AES RijndaelManaged����(RijndaelManaged��AES���㷨�ǿ�ʽ�����㷨)
        /// </summary>
        /// <param name="encryptString">�������ַ���</param>
        /// <param name="encryptKey">������Կ�������ַ�</param>
        /// <returns>���ܽ���ַ���</returns>
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
        /// �ԳƼ����㷨AES RijndaelManaged�����ַ���
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���</returns>
        public static string DecryptBase64String(string decryptString)
        {
            return DecryptBase64String(decryptString, _DefaultAesKey.PadRight(32, 'A'));
        }
        /// <summary>
        /// �����ַ���
        /// </summary>
        /// <param name="decryptString">�����ܵ��ַ���</param>
        /// <param name="decryptKey">������Կ,�ͼ�����Կ��ͬ</param>
        /// <returns>���ܳɹ����ؽ��ܺ���ַ���,ʧ�ܷ��ؿ�</returns>
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
        /// ����ַ����Ƿ�Ϊnull�򲻷���ָ���ĳ���.
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
        /// �����ļ���
        /// </summary>
        /// <param name="fs">Ҫ���ܵ��ļ���</param>
        /// <param name="decryptKey">��Կ,ע�ⳤ��Ϊ32λ.</param>
        /// <returns>���ؼ��ܵ���</returns>
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
        /// �����ļ���
        /// </summary>
        /// <param name="fs">Ҫ���ܵ��ļ���.</param>
        /// <param name="decryptKey">��Կ,����Ϊ32λ.</param>
        /// <returns>���ؼ��ܵ���.</returns>
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
        /// ��ָ���ļ�����
        /// </summary>
        /// <param name="inputFile">ָ�������ļ���·��</param>
        /// <param name="outputFile">ָ������ļ���·��.</param>
        /// <param name="decryptKey">��Կ,����Ϊ32λ.</param>
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
                //�ļ��쳣
                return false;
            }
            return true;
        }

        /// <summary>
        /// ��ָ�����ļ�����.
        /// </summary>
        /// <param name="inputFile">�����ļ���·��.</param>
        /// <param name="outputFile">����ļ���·��.</param>
        /// <param name="decryptKey">��Կ,����Ϊ32λ</param>
        /// <returns>������ܳɹ�,����true,���ɹ�����false</returns>
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
                //�ļ��쳣
                return false;
            }
            return true;
        }
        #endregion
    }
}