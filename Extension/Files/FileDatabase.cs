/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/12/12 11:16:10
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
using System.Globalization;
using System.Xml.Serialization;
using CRC.Util.Strings;

namespace CRC.Files
{
    /// <summary>
    /// 文件数据库，这是一个抽象类。
    /// </summary>
    public abstract class FileDatabase
    {
        #region Fields

        /// <summary>
        /// 文件数据库操作锁
        /// </summary>
        protected static readonly object _OperationLock = new object();
        private static readonly HashSet<char> _InvalidFileNameChars;

        static FileDatabase()
        {
            _InvalidFileNameChars = new HashSet<char>() { '\0', ' ', '.', '$', '/', '\\' };
            foreach (var c in Path.GetInvalidPathChars()) { _InvalidFileNameChars.Add(c); }
            foreach (var c in Path.GetInvalidFileNameChars()) { _InvalidFileNameChars.Add(c); }
        }

        /// <summary>
        /// 文件数据库
        /// </summary>
        /// <param name="directory">数据库文件所在目录</param>
        protected FileDatabase(string directory)
        {
            Directory = directory;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 数据库文件所在目录
        /// </summary>
        public virtual string Directory { get; private set; }

        /// <summary>
        /// 是否输出缩进
        /// </summary>
        public virtual bool OutputIndent { get; set; }

        /// <summary>
        /// 文件扩展名
        /// </summary>
        public virtual string FileExtension { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <param name="document">文档对象</param>
        /// <returns>文档ID</returns>
        public virtual string Save<TDocument>(TDocument document)
        {
            return Save<TDocument>(ObjectId.NewObjectId().ToString(), document);
        }

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <param name="id">文档ID</param>
        /// <param name="document">文档对象</param>
        /// <returns>文档ID</returns>
        public virtual string Save<TDocument>(string id, TDocument document)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            if (document == null)
                throw new ArgumentNullException("document");

            Delete<TDocument>(id);

            try
            {
                string fileName = GenerateFileFullPath<TDocument>(id);
                string output = Serialize(document);

                lock (_OperationLock)
                {
                    System.IO.FileInfo info = new System.IO.FileInfo(fileName);
                    System.IO.Directory.CreateDirectory(info.Directory.FullName);
                    System.IO.File.WriteAllText(fileName, output);
                }
            }
            catch (Exception ex)
            {
                throw new FileDatabaseException(
                  string.Format(CultureInfo.InvariantCulture,
                  "Save document failed with id [{0}].", id), ex.Message);
            }

            return id;
        }

        /// <summary>
        /// 根据文档ID查找文档
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <param name="id">文档ID</param>
        /// <returns>文档对象</returns>
        public virtual TDocument FindOneById<TDocument>(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            try
            {
                string fileName = GenerateFileFullPath<TDocument>(id);
                if (File.Exists(fileName))
                {
                    string fileData = File.ReadAllText(fileName);
                    return Deserialize<TDocument>(fileData);
                }

                return default(TDocument);
            }
            catch (Exception ex)
            {
                throw new FileDatabaseException(
                  string.Format(CultureInfo.InvariantCulture,
                  "Find document by id [{0}] failed.", id), ex.Message);
            }
        }

        /// <summary>
        /// 查找指定类型的所有文档
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <returns>文档对象序列</returns>
        public virtual IEnumerable<TDocument> FindAll<TDocument>()
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(
                  GenerateFilePath<TDocument>(),
                  "*." + FileExtension,
                  SearchOption.TopDirectoryOnly);

                List<TDocument> list = new List<TDocument>();
                foreach (string fileName in files)
                {
                    string fileData = File.ReadAllText(fileName);
                    TDocument document = Deserialize<TDocument>(fileData);
                    if (document != null)
                    {
                        list.Add(document);
                    }
                }

                return list;
            }
            catch (Exception ex)
            {
                throw new FileDatabaseException(
                  "Find all documents failed.", ex.Message);
            }
        }

        /// <summary>
        /// 根据指定文档ID删除文档
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <param name="id">文档ID</param>
        public virtual void Delete<TDocument>(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            try
            {
                string fileName = GenerateFileFullPath<TDocument>(id);
                if (File.Exists(fileName))
                {
                    lock (_OperationLock)
                    {
                        File.Delete(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FileDatabaseException(
                  string.Format(CultureInfo.InvariantCulture,
                  "Delete document by id [{0}] failed.", id), ex.Message);
            }
        }

        /// <summary>
        /// 删除所有指定类型的文档
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        public virtual void DeleteAll<TDocument>()
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(
                  GenerateFilePath<TDocument>(), "*." + FileExtension,
                  SearchOption.TopDirectoryOnly);

                foreach (string fileName in files)
                {
                    lock (_OperationLock)
                    {
                        File.Delete(fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new FileDatabaseException(
                  "Delete all documents failed.", ex.Message);
            }
        }

        /// <summary>
        /// 获取指定类型文档的数量
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <returns>文档的数量</returns>
        public virtual int Count<TDocument>()
        {
            try
            {
                string[] files = System.IO.Directory.GetFiles(
                  GenerateFilePath<TDocument>(),
                  "*." + FileExtension, SearchOption.TopDirectoryOnly);
                return files.Length;
            }
            catch (Exception ex)
            {
                throw new FileDatabaseException(
                  "Count all documents failed.", ex.Message);
            }
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// 生成文件全路径
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <param name="id">文档ID</param>
        /// <returns>文件路径</returns>
        protected virtual string GenerateFileFullPath<TDocument>(string id)
        {
            return Path.Combine(GenerateFilePath<TDocument>(),
              GenerateFileName<TDocument>(id));
        }

        /// <summary>
        /// 生成文件路径
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <returns>文件路径</returns>
        protected virtual string GenerateFilePath<TDocument>()
        {
            return Path.Combine(this.Directory, typeof(TDocument).Name);
        }

        /// <summary>
        /// 生成文件名
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <param name="id">文档ID</param>
        /// <returns>文件名</returns>
        protected virtual string GenerateFileName<TDocument>(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("id");

            foreach (char c in id)
            {
                if (_InvalidFileNameChars.Contains(c))
                {
                    throw new FileDatabaseException(
                      string.Format(CultureInfo.InvariantCulture,
                      "The character '{0}' is not a valid file name identifier.", c),"");
                }
            }

            return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", id, FileExtension);
        }

        /// <summary>
        /// 将指定的文档对象序列化至字符串
        /// </summary>
        /// <param name="value">指定的文档对象</param>
        /// <returns>文档对象序列化后的字符串</returns>
        protected abstract string Serialize(object value);

        /// <summary>
        /// 将字符串反序列化成文档对象
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <param name="data">字符串</param>
        /// <returns>文档对象</returns>
        protected abstract TDocument Deserialize<TDocument>(string data);

        #endregion
    }


    /// <summary>
    /// XML文件数据库
    /// </summary>
    public class XmlDatabase : FileDatabase
    {
        /// <summary>
        /// XML文件数据库
        /// </summary>
        /// <param name="directory">数据库文件所在目录</param>
        public XmlDatabase(string directory)
            : base(directory)
        {
            FileExtension = @"xml";
        }

        /// <summary>
        /// 将指定的文档对象序列化至字符串
        /// </summary>
        /// <param name="value">指定的文档对象</param>
        /// <returns>
        /// 文档对象序列化后的字符串
        /// </returns>
        protected override string Serialize(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            using (StringWriterWithEncoding sw = new StringWriterWithEncoding(Encoding.UTF8))
            {
                XmlSerializer serializer = new XmlSerializer(value.GetType());
                serializer.Serialize(sw, value);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 将字符串反序列化成文档对象
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <param name="data">字符串</param>
        /// <returns>
        /// 文档对象
        /// </returns>
        protected override TDocument Deserialize<TDocument>(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException("data");

            using (StringReader sr = new StringReader(data))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(TDocument));
                return (TDocument)serializer.Deserialize(sr);
            }
        }
    }
    /// <summary>
    /// StringWriterWithEncoding
    /// </summary>
    /// <remarks>
    ///If you want to use XmlTextWriter to write XML into a StringBuilder 
    ///you can create the XmlTextWriter like this:
    ///StringBuilder builder = new StringBuilder();
    ///XmlWriter writer = new XmlTextWriter(new StringWriter(builder));
    ///But this generates a declaration on the resulting XML with the encoding of UTF-16 
    ///(the encoding of a .Net String). 
    ///There doesn't seem to be a straightforward way of making this declaration UTF-8 in this set up.
    ///You can, of course, use a MemoryStream instead of a StringWriter, 
    ///and then use Encoding.UTF8.GetString(...) to convert the bytes to a string, 
    ///but doing this made the resulting string have non-printable characters in it, 
    ///which we don't want.
    /// </remarks>
    public class StringWriterWithEncoding : StringWriter
    {
        private readonly Encoding _Encoding;

        public StringWriterWithEncoding()
            : base() { }

        public StringWriterWithEncoding(IFormatProvider formatProvider)
            : base(formatProvider) { }

        public StringWriterWithEncoding(StringBuilder sb)
            : base(sb) { }

        public StringWriterWithEncoding(StringBuilder sb, IFormatProvider formatProvider)
            : base(sb, formatProvider) { }


        public StringWriterWithEncoding(Encoding encoding)
            : base()
        {
            _Encoding = encoding;
        }

        public StringWriterWithEncoding(IFormatProvider formatProvider, Encoding encoding)
            : base(formatProvider)
        {
            _Encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, Encoding encoding)
            : base(sb)
        {
            _Encoding = encoding;
        }

        public StringWriterWithEncoding(StringBuilder sb, IFormatProvider formatProvider, Encoding encoding)
            : base(sb, formatProvider)
        {
            _Encoding = encoding;
        }

        public override Encoding Encoding
        {
            get
            {
                return _Encoding ?? base.Encoding;
            }
        }
    }
    /// <summary>
    /// JSON文件数据库
    /// </summary>
    public class JsonDatabase : FileDatabase
    {
        /// <summary>
        /// JSON文件数据库
        /// </summary>
        /// <param name="directory">数据库文件所在目录</param>
        public JsonDatabase(string directory)
            : base(directory)
        {
            FileExtension = @"json";
        }

        /// <summary>
        /// 将指定的文档对象序列化至字符串
        /// </summary>
        /// <param name="value">指定的文档对象</param>
        /// <returns>
        /// 文档对象序列化后的字符串
        /// </returns>
        protected override string Serialize(object value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            //return JsonConvert.SerializeObject(value, OutputIndent);
            return null;
        }

        /// <summary>
        /// 将字符串反序列化成文档对象
        /// </summary>
        /// <typeparam name="TDocument">文档类型</typeparam>
        /// <param name="data">字符串</param>
        /// <returns>
        /// 文档对象
        /// </returns>
        protected override TDocument Deserialize<TDocument>(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException("data");
            return default(TDocument);
            //return JsonConvert.DeserializeObject<TDocument>(data);
        }
    }
}
