#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/24 15:58:42
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Xml.Serialization;

namespace CRC.Files
{
    /// <summary>
    /// 序列化帮助器.
    /// </summary>
    public class SerializeHelper
    {
        #region 二进制
        /// <summary>
        /// 加载对象.
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns></returns>
        public static T Load<T>(string path)
        {
            if (!File.Exists(path)) throw new ArgumentException("文件不存在,请检查.");
            try
            {
                //读取文件
                using (FileStream fs = File.OpenRead(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    T temp = (T)bf.Deserialize(fs);//序列化
                    return temp;
                }
            }
            catch (Exception e)
            {
               
                throw e;
            }
        }
        /// <summary>
        /// 保存对象
        /// </summary>
        /// <param name="path">保存文件的路径</param>
        /// <param name="temp">要保存的对象</param>
        /// <returns></returns>
        public static bool Save<T>(T temp, string path)
        {
            if (temp == null) throw new ArgumentNullException("temp");
            try
            {
                //不存在文件目录,创建一个文件目录.
                string dir = path.Substring(0, path.LastIndexOf('\\'));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                using (FileStream fs = File.Create(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, temp);
                    return true;
                } 
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }

       
        /// <summary>
        /// 将对象序列为字节流.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] ObjectToByteArray<T>(T obj) where T : class
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
      
        /// <summary>
        /// 将字节流序列化为对象.
        /// </summary>
        /// <typeparam name="T">指定的对象.</typeparam>
        /// <param name="arrBytes">对象序列化的数组.</param>
        /// <returns></returns>
        public static T ByteArrayToObject<T>(byte[] arrBytes)
        {
            if (arrBytes == null) throw new ArgumentNullException("arrBytes");
            using (MemoryStream memStream = new MemoryStream())
            {
                BinaryFormatter binForm = new BinaryFormatter();
                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);
                T obj = (T)binForm.Deserialize(memStream);
                return obj;
            }
          
           
        }

        #endregion

        #region XML
        /// <summary>
        /// 使用XML序列化方式保存对象.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveXml<T>(T obj, string path)
        {
            if (path == null || obj == null) return false;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    xs.Serialize(stream, obj);
                    stream.Close();
                    return true;
                }
               
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 使用XML序列化方式加载对象.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">指定的文件路径.</param>
        /// <returns></returns>
        public static T LoadXml<T>(string path)
        {
            if (path == null) return default(T);
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    T p = (T)xs.Deserialize(stream);
                    stream.Close();
                    return p;
                }
            }
            catch (Exception)
            {
                return default(T);
            }
        }





        #endregion

        #region Json
        /// <summary>
        /// 将Json形式的字符串,转换为对象.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T JsonParse<T>(string jsonString)
        {
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }
        /// <summary>
        /// 将对象转换为Json字符串.
        /// </summary>
        /// <param name="jsonObject"></param>
        /// <returns></returns>
        public static string JsonStringify(object jsonObject)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return Encoding.UTF8.GetString(ms.ToArray());
            }
        }

        #endregion
    }
}
