using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace CWebQQ.Data
{


    /// <summary>
    /// 用户自定义的快速回复消息的集合
    /// </summary>
    [Serializable]
    public class Group : List<GroupItem>
    {

    }

    /// <summary>
    /// 用户自定义的一组 快速回复消息
    /// </summary>
    [Serializable]
    public class GroupItem
    {

        /// <summary>
        /// 一组消息.
        /// </summary>
        public GroupItem()
        {
            _GroupList = new List<string>();
        }

        private string _GroupName;
        /// <summary>
        /// 组名称.
        /// </summary>	
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }


        private List<string> _GroupList;
        /// <summary>
        /// 消息列表
        /// </summary>	
        public List<string> GroupList
        {
            get { return _GroupList; }
            set { _GroupList = value; }
        }




    }

    /// <summary>
    /// 系统帮助函数
    /// </summary>
    [Serializable]
    public class SystemHelper
    {

        #region 静态函数

        /// <summary>
        /// 从文件中 加载对象.
        /// </summary>
        /// <param name="path">文件的路径</param>
        /// <returns></returns>
        public static T Load<T>(string path)
        {
            if (!File.Exists(path)) return default(T);
            try
            {
                //读取文件
                using (FileStream fs = File.OpenRead(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    T temp = (T)bf.Deserialize(fs);//序列化
                    fs.Close();
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
        public static bool Save<T>(string path, T temp)
        {

            try
            {
                using (FileStream fs = File.Create(path))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(fs, temp);
                    fs.Close();
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
            catch (Exception e)
            {

                return false;
            }

        }

        #endregion






    }


}
