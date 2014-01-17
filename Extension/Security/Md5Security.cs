// /*********************************************************
// * 开发人员：TopC
// * 创建时间：2013-12-30 11:31
// * 描述说明：
// *
// * 更改历史：
// *
// * *******************************************************/
namespace CRC.Security
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    ///     MD5加密及验证
    /// </summary>
    public static class Md5Security
    {
        #region 输出不同位数的MD5

        /// <summary>
        ///     MD5加密函数
        /// </summary>
        /// <param name="soucesText">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string Md5(string soucesText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] data = Encoding.Default.GetBytes(soucesText);
            byte[] result = md5.ComputeHash(data);
            return result.Aggregate("", (current, t) => current + t.ToString("x2"));
        }

        /// <summary>
        ///     获得32位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_32(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
            var sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                sb.Append(data[i].ToString("x2"));
            return sb.ToString();
        }

        /// <summary>
        ///     获得16位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_16(string input)
        {
            return GetMD5_32(input).Substring(8, 16);
        }

        /// <summary>
        ///     获得8位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_8(string input)
        {
            return GetMD5_32(input).Substring(8, 8);
        }

        /// <summary>
        ///     获得4位的MD5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GetMD5_4(string input)
        {
            return GetMD5_32(input).Substring(8, 4);
        }

        #endregion


        public static string Md5EncryptHash(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input), 0, input.Length);
            var temp = new char[res.Length];
            Array.Copy(res, temp, res.Length);
            return new String(temp);
        }

        #region 字符串 加密 与 解密

        /// <summary>
        ///     加密字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string EncryptString(string input)
        {
            byte[] encbuff = Encoding.UTF8.GetBytes(input);
            return AddMd5Profix(Convert.ToBase64String(encbuff));
        }

        /// <summary>
        ///     解密加过密的字符串
        /// </summary>
        /// <param name="input"></param>
        /// <param name="throwException">解密失败是否抛异常</param>
        /// <returns></returns>
        public static string DecryptString(string input, bool throwException)
        {
            try
            {
                string res = input;
                if (ValidateValue(res))
                {
                    byte[] decbuff = Convert.FromBase64String(res);
                    return RemoveMd5Profix(Encoding.UTF8.GetString(decbuff));
                }
                throw new Exception("字符串无法转换成功！");
            }
            catch
            {
                if (throwException)
                {
                    throw;
                }
                return "";
            }
        }

        #endregion

        #region 带MD5校验的字符串

        /// <summary>
        ///     添加MD5的前缀，便于检查有无篡改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string AddMd5Profix(string input)
        {
            return GetMD5_4(input) + input;
        }

        /// <summary>
        ///     移除MD5的前缀
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveMd5Profix(string input)
        {
            return input.Substring(4);
        }

        /// <summary>
        ///     验证MD5前缀处理的字符串有无被篡改
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool ValidateValue(string input)
        {
            if (input.Length >= 4)
            {
                string tmp = input.Substring(4);
                if (input.Substring(0, 4) == GetMD5_4(tmp))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region MD5签名验证
        //对文件添加MD5标签及验证
        /*
         *这种方式比较有趣，我也是最近才发现其中的奥妙，其实我们为了防止文件被修改，可以采用这种方式预先添加MD5码，
         *然后在程序代码中进行验证，这样至少可以减少部分篡改的行为吧，因为只要文件有些少的修改，Md5码将会发生变化的。
         */

        /// <summary>
        ///     对给定文件路径的文件加上标签
        /// </summary>
        /// <param name="path">要加密的文件的路径</param>
        /// <returns>标签的值</returns>
        public static bool AddMd5(string path)
        {
            bool isNeed = !CheckMd5(path);
            try
            {
                var fsread = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var md5File = new byte[fsread.Length];
                fsread.Read(md5File, 0, (int)fsread.Length); // 将文件流读取到Buffer中
                fsread.Close();
                if (isNeed)
                {
                    string result = Md5Buffer(md5File, 0, md5File.Length); // 对Buffer中的字节内容算MD5
                    byte[] md5 = Encoding.ASCII.GetBytes(result); // 将字符串转换成字节数组以便写人到文件中
                    var fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fsWrite.Write(md5File, 0, md5File.Length); // 将文件，MD5值 重新写入到文件中。
                    fsWrite.Write(md5, 0, md5.Length);
                    fsWrite.Close();
                }
                else
                {
                    var fsWrite = new FileStream(path, FileMode.Open, FileAccess.ReadWrite);
                    fsWrite.Write(md5File, 0, md5File.Length);
                    fsWrite.Close();
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///     对给定路径的文件进行验证
        /// </summary>
        /// <param name="path"></param>
        /// <returns>是否加了标签或是否标签值与内容值一致</returns>
        public static bool CheckMd5(string path)
        {
            try
            {
                var getFile = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
                var md5File = new byte[getFile.Length]; // 读入文件
                getFile.Read(md5File, 0, (int)getFile.Length);
                getFile.Close();
                string result = Md5Buffer(md5File, 0, md5File.Length - 32); // 对文件除最后32位以外的字节计算MD5，这个32是因为标签位为32位。
                string md5 = Encoding.ASCII.GetString(md5File, md5File.Length - 32, 32); //读取文件最后32位，其中保存的就是MD5值
                return result == md5;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     计算文件的MD5值
        /// </summary>
        /// <param name="md5File">MD5签名文件字符数组</param>
        /// <param name="index">计算起始位置</param>
        /// <param name="count">计算终止位置</param>
        /// <returns>计算结果</returns>
        private static string Md5Buffer(byte[] md5File, int index, int count)
        {
            var getMd5 = new MD5CryptoServiceProvider();
            byte[] hashByte = getMd5.ComputeHash(md5File, index, count);
            string result = BitConverter.ToString(hashByte);
            result = result.Replace("-", "");
            return result;
        }

        #endregion



    }
}