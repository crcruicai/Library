/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/12/12 11:10:06
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.Diagnostics;

namespace CRC.Util.Strings
{
    //http://www.cnblogs.com/gaochundong/archive/2013/04/24/csharp_generate_mongodb_objectid.html
    //    在MongoDB中，文档（document）在集合（collection）中的存储需要一个唯一的_id字段作为主键。这个_id默认使用ObjectId来定义，因为ObjectId定义的足够短小，并尽最大可能的保持唯一性，同时能被快速的生成。

    //ObjectId 是一个 12 Bytes 的 BSON 类型，其包含：

    //4 Bytes 自纪元时间开始的秒数
    //3 Bytes 机器描述符
    //2 Bytes 进程ID
    //3 Bytes 随机数
    //从定义可以看出，在同一秒内，在不同的机器上相同进程ID条件下，非常有可能生成相同的ObjectId。
    //同时可以根据定义判断出，在给定条件下，ObjectId本身即可描述生成的时间顺序

    //ObjectId的存储使用Byte数组，而其展现需将Byte数组转换成字符串进行显示，所以通常我们看到的ObjectId都类似于：ObjectId("507f191e810c19729de860ea")

    /// <summary>
    /// ObjectId实体类.
    /// </summary>
    public class ObjectId
    {
        private string _string;

        public ObjectId()
        {
        }

        public ObjectId(string value)
            : this(DecodeHex(value))
        {
        }

        internal ObjectId(byte[] value)
        {
            Value = value;
        }

        public static ObjectId Empty
        {
            get { return new ObjectId("000000000000000000000000"); }
        }

        public byte[] Value { get; private set; }

        public static ObjectId NewObjectId()
        {
            return new ObjectId { Value = ObjectIdGenerator.Generate() };
        }

        public static bool TryParse(string value, out ObjectId objectId)
        {
            objectId = Empty;
            if (value == null || value.Length != 24)
            {
                return false;
            }

            try
            {
                objectId = new ObjectId(value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        protected static byte[] DecodeHex(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("value");

            var chars = value.ToCharArray();
            var numberChars = chars.Length;
            var bytes = new byte[numberChars / 2];

            for (var i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(new string(chars, i, 2), 16);
            }

            return bytes;
        }

        public override int GetHashCode()
        {
            return Value != null ? ToString().GetHashCode() : 0;
        }

        public override string ToString()
        {
            if (_string == null && Value != null)
            {
                _string = BitConverter.ToString(Value)
                  .Replace("-", string.Empty)
                  .ToLowerInvariant();
            }

            return _string;
        }

        public override bool Equals(object obj)
        {
            var other = obj as ObjectId;
            return Equals(other);
        }

        public bool Equals(ObjectId other)
        {
            return other != null && ToString() == other.ToString();
        }

        public static implicit operator string(ObjectId objectId)
        {
            return objectId == null ? null : objectId.ToString();
        }

        public static implicit operator ObjectId(string value)
        {
            return new ObjectId(value);
        }

        public static bool operator ==(ObjectId left, ObjectId right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (((object)left == null) || ((object)right == null))
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(ObjectId left, ObjectId right)
        {
            return !(left == right);
        }
    }

    /// <summary>
    /// ObjectId生成器.
    /// </summary>
    internal static class ObjectIdGenerator
    {
        private static readonly DateTime Epoch =
          new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private static readonly object _innerLock = new object();
        private static int _counter;
        private static readonly byte[] _machineHash = GenerateHostHash();
        private static readonly byte[] _processId =
          BitConverter.GetBytes(GenerateProcessId());

        public static byte[] Generate()
        {
            var oid = new byte[12];
            var copyidx = 0;

            Array.Copy(BitConverter.GetBytes(GenerateTime()), 0, oid, copyidx, 4);
            copyidx += 4;

            Array.Copy(_machineHash, 0, oid, copyidx, 3);
            copyidx += 3;

            Array.Copy(_processId, 0, oid, copyidx, 2);
            copyidx += 2;

            Array.Copy(BitConverter.GetBytes(GenerateCounter()), 0, oid, copyidx, 3);

            return oid;
        }

        private static int GenerateTime()
        {
            var now = DateTime.UtcNow;
            var nowtime = new DateTime(Epoch.Year, Epoch.Month, Epoch.Day,
              now.Hour, now.Minute, now.Second, now.Millisecond);
            var diff = nowtime - Epoch;
            return Convert.ToInt32(Math.Floor(diff.TotalMilliseconds));
        }

        private static byte[] GenerateHostHash()
        {
            using (var md5 = MD5.Create())
            {
                var host = Dns.GetHostName();
                return md5.ComputeHash(Encoding.Default.GetBytes(host));
            }
        }

        private static int GenerateProcessId()
        {
            var process = Process.GetCurrentProcess();
            return process.Id;
        }

        private static int GenerateCounter()
        {
            lock (_innerLock)
            {
                return _counter++;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ObjectIdTest
    {
       
        static void Test()
        {
            Console.ForegroundColor = ConsoleColor.Red;

            ObjectId emptyOid = ObjectId.Empty;
            Console.WriteLine(emptyOid);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < 10; i++)
            {
                ObjectId oid = ObjectId.NewObjectId();
                Console.WriteLine(oid);
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;

            ObjectId existingOid;
            ObjectId.TryParse("507f191e810c19729de860ea", out existingOid);
            Console.WriteLine(existingOid);

            Console.ReadKey();
        }
    }
  
}
