using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;

namespace CRC.Controls
{
    /* 作者：Starts_2000
     * 日期：2009-09-08
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    /// <summary>
    /// 操作命令管理器.
    /// </summary>
    internal class OperateManager : IDisposable
    {
        /// <summary>
        /// 操作命令列表.
        /// </summary>
        private List<OperateObject> _operateList;

        /// <summary>
        /// 最大的操作数量.
        /// </summary>
        private static readonly int MaxOperateCount = 1000;

        public OperateManager()
        {
        }
        /// <summary>
        /// 获取操作命令列表.
        /// </summary>
        public List<OperateObject> OperateList
        {
            get
            {
                if (_operateList == null)
                {
                    _operateList = new List<OperateObject>(100);
                }
                return _operateList;
            }
        }

        /// <summary>
        /// 获取操作命令的数量.
        /// </summary>
        public int OperateCount
        {
            get { return OperateList.Count; }
        }

        /// <summary>
        /// 添加一个操作命令.
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="color"></param>
        /// <param name="data"></param>
        public void AddOperate(OperateType operateType,Color color,object data)
        {
            OperateObject obj = new OperateObject(operateType, color, data);
                
            if (OperateList.Count > MaxOperateCount)
            {
                OperateList.RemoveAt(0);
            }
            OperateList.Add(obj);
        }

        /// <summary>
        /// 撤销一个操作命令.
        /// </summary>
        /// <returns></returns>
        public bool RedoOperate()
        {
            if (OperateList.Count > 0)
            {
                OperateList.RemoveAt(OperateList.Count - 1);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 清理所有操作命令
        /// </summary>
        public void Clear()
        {
            OperateList.Clear();
        }

        #region IDisposable 成员

        public void Dispose()
        {
            if (_operateList != null)
            {
                _operateList.Clear();
                _operateList = null;
            }
        }

        #endregion
    }
}
