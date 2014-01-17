/*********************************************************
 * 开发人员：TopC
 * 创建时间：2014/1/15 10:26:36
 * 描述说明：资源文件的相互转化.
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System.Collections;
using System.IO;
using System.Resources;

namespace CRC.Util
{
    /// <summary>
    /// 将resources文件与res文件互相转换的类.
    /// <para>都是输出文件的形式.</para>
    /// </summary>
    public static class ConvertResource
    {
        /// <summary>
        /// 将resources文件转换为resx文件,如果转换成功,返回True
        /// </summary>
        /// <param name="strResources">resources文件的路径,注意请保证路径正确无误</param>
        /// <param name="strResx">res文件的路径,注意请保证路径正确无误</param>
        public static bool ConvertRes(string strResources, string strResx)
        {

            if (string.IsNullOrEmpty(strResources) || string.IsNullOrEmpty(strResx))
            {
                return false;
            }
            if (File.Exists(strResources) && File.Exists(strResx) == false)
            {
                return false;
            }

            //开始转换.
            try
            {
                ResourceReader reader = new ResourceReader(strResources);
                ResXResourceWriter writer = new ResXResourceWriter(strResx);

                foreach (DictionaryEntry en in reader)
                {
                    writer.AddMetadata(en.Key.ToString(), en.Value);
                }
                reader.Close();
                writer.Close();
            }
            catch
            {
                return false;
            }

            return true;


        }
        /// <summary>
        /// 将resx文件转换为resources文件,如果转换成功,返回True
        /// </summary>
        /// <param name="strResources">resources文件的路径,注意请保证路径正确无误</param>
        /// <param name="strResx">resx文件的路径,注意请保证路径正确无误</param>
        public static bool ConvertResources(string strResx, string strResources)
        {
            if (string.IsNullOrEmpty(strResources) || string.IsNullOrEmpty(strResx))
            {
                return false;
            }
            if (File.Exists(strResources) && File.Exists(strResx) == false)
            {
                return false;
            }
            //开始转换.
            try
            {
                //ResourceReader reader = new ResourceReader(strResources);
                ResXResourceSet reader = new ResXResourceSet(strResx);
                //ResXResourceWriter writer = new ResXResourceWriter(strResx);
                ResourceWriter writer = new ResourceWriter(strResources);

                foreach (DictionaryEntry en in reader)
                {
                    writer.AddResource(en.Key.ToString(), en.Value);

                }
                reader.Close();
                writer.Close();
            }
            catch
            {
                return false;
            }

            return true;


        }
    }
}
