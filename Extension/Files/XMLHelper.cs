#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/24 15:48:41
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
using System.Xml;
using System.IO;

namespace CRC.Files
{
    /// <summary>
    /// XML配置读写帮助器.
    /// </summary>
    public class XmlConfigReader
    {
        #region 获取指定XML路径的XmlDocument对象

        /// <summary>
        /// 根据XML文件路径获取XmlDocument对象
        /// </summary>
        /// <param name="xmlFilePath"></param>
        /// <returns></returns>
        public static XmlDocument GetConfigDoc(string xmlFilePath)
        {
            if (string.IsNullOrEmpty(xmlFilePath) || !File.Exists(xmlFilePath))
                return null;


            var xdoc = new XmlDocument();

            try
            {
                xdoc.Load(xmlFilePath);
            }
            catch
            {
                throw new Exception(string.Format("请确认该XML文件格式正确，路径为：{0}", xmlFilePath));
            }

            return xdoc;
        }

        #endregion


        #region 读取节点属性值

        /// <summary>
        /// 读取某个XML节点的属性值（根据属性名）
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public static string ReadAttrValue(XmlNode xmlNode, string attrName)
        {
            return ((XmlElement)xmlNode).GetAttribute(attrName);
        }

        /// <summary>
        /// 读取某个XML文档下某个节点的属性值（根据节点名和属性名）
        /// </summary>
        /// <param name="xmlDoc">XML文档</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="attrName">属性名</param>
        /// <returns></returns>
        public static string ReadAttrValue(XmlDocument xmlDoc, string nodeName, string attrName)
        {
            var attrVals = ReadAttrValues(xmlDoc, nodeName, attrName);
            return (attrVals == null || attrVals.Length == 0) ? null : attrVals[0];
        }

        /// <summary>
        /// 读取某个XML文档下指定名称的节点的属性值的字符串数组
        /// </summary>
        /// <param name="xmlDoc">XML文档</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="attrName">属性名</param>
        /// <returns></returns>
        public static string[] ReadAttrValues(XmlDocument xmlDoc, string nodeName, string attrName)
        {
            if (xmlDoc == null || string.IsNullOrEmpty(nodeName) || string.IsNullOrEmpty(attrName))
                return null;

            var xpathExpr = string.Format("//{0}[@{1}]", nodeName, attrName);
            var nodes = GetXmlNodesByXPathExpr(xmlDoc, xpathExpr);
            if (nodes != null && nodes.Count > 0)
            {
                var nodeCount = nodes.Count;
                var attrVals = new string[nodeCount];
                for (var i = 0; i < nodeCount; i++)
                {
                    attrVals[i] = ((XmlElement)nodes[i]).GetAttribute(attrName);
                }

                return attrVals;
            }

            return null;
        }

        #endregion


        #region 读取节点文本内容

        /// <summary>
        /// 读取XML文档中节点文本的数组
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string[] ReadNodeTexts(XmlDocument xmlDoc, string nodeName)
        {
            if (xmlDoc == null || string.IsNullOrEmpty(nodeName))
                return null;

            var xpathExpr = string.Format("//{0}", nodeName);
            var nodes = GetXmlNodesByXPathExpr(xmlDoc, xpathExpr);
            if (nodes != null && nodes.Count > 0)
            {
                var nodeCount = nodes.Count;
                var nodeTexts = new string[nodeCount];
                for (var i = 0; i < nodeCount; i++)
                {
                    nodeTexts[i] = nodes[i].InnerText;
                }

                return nodeTexts;
            }

            return null;
        }

        /// <summary>
        /// 读取XML文档中第一个节点名为nodeName的节点的文本
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        public static string ReadNodeText(XmlDocument xmlDoc, string nodeName)
        {
            var nodeTexts = ReadNodeTexts(xmlDoc, nodeName);
            if (nodeTexts != null && nodeTexts.Length > 0)
            {
                return nodeTexts[0];
            }

            return null;
        }

        /// <summary>
        /// 获取节点文本
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <returns></returns>
        public static string ReadNodeText(XmlNode xmlNode)
        {
            if (xmlNode == null)
                return null;

            return xmlNode.InnerText;
        }

        #endregion


        #region 获取XML节点（或节点列表）

        public static XmlNode GetNodeByName(XmlDocument xmlDoc, string nodeName)
        {
            var nodes = GetXmlNodesByName(xmlDoc, nodeName);
            if (nodes == null || nodes.Count == 0)
                return null;

            return nodes[0];
        }

        /// <summary>
        /// 根据节点名称获取XML文档的节点列表
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNodeList GetXmlNodesByName(XmlDocument xmlDoc, string nodeName)
        {
            if (xmlDoc == null || string.IsNullOrEmpty(nodeName))
                return null;

            return GetXmlNodesByXPathExpr(xmlDoc, string.Format("//{0}", nodeName));
        }

        /// <summary>
        /// 获取xmlNode节点下满足xpathExpr表达式的XML结点列表
        /// </summary>
        /// <param name="xmlNode"></param>
        /// <param name="xpathExpr"></param>
        /// <returns></returns>   
        public static XmlNodeList GetXmlNodesByXPathExpr(XmlNode xmlNode, string xpathExpr)
        {
            if (xmlNode == null)
                return null;

            return xmlNode.SelectNodes(xpathExpr);
        }

        #endregion


        #region 根据父节点获取子节点或者子节点的文本

        /// <summary>
        /// 获取父节点下所有子节点的文本数组
        /// </summary>
        /// <param name="parentNode"></param>
        /// <returns></returns>
        public static string[] GetChildTexts(XmlNode parentNode)
        {
            var childNodes = GetChildNodes(parentNode);
            if (childNodes == null || childNodes.Count == 0)
                return null;

            var count = childNodes.Count;
            var childTexts = new string[count];
            var i = 0;
            foreach (XmlNode node in childNodes)
            {
                childTexts[i++] = ReadNodeText(node);
            }

            return childTexts;
        }

        /// <summary>
        /// 获取父节点下所有节点名为childNodeName的子节点的文本数组
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="childNodeName"></param>
        /// <returns></returns>
        public static string[] GetChildTexts(XmlNode parentNode, string childNodeName)
        {
            var childNodes = GetChildNodes(parentNode, childNodeName);
            if (childNodes == null || childNodes.Count == 0)
                return null;

            var count = childNodes.Count;
            var childTexts = new string[count];
            var i = 0;
            foreach (XmlNode node in childNodes)
            {
                childTexts[i++] = ReadNodeText(node);
            }

            return childTexts;
        }

        /// <summary>
        /// 获取父节点的子节点列表
        /// </summary>
        /// <param name="parentNode">父节点</param>
        /// <returns></returns>
        public static XmlNodeList GetChildNodes(XmlNode parentNode)
        {
            if (parentNode == null)
                return null;

            return parentNode.ChildNodes;
        }

        /// <summary>
        /// 获取父节点下指定名称的子节点列表
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="childNodeName"></param>
        /// <returns></returns>
        public static XmlNodeList GetChildNodes(XmlNode parentNode, string childNodeName)
        {
            return GetXmlNodesByXPathExpr(parentNode, childNodeName);
        }

        #endregion

    }
}
