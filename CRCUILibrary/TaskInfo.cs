using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRC
{
    /// <summary>
    /// 任务的信息.
    /// </summary>
    public class TaskInfo
    {


        #region 字段与变量

        #endregion

        #region 构造函数

        #endregion

        #region 属性

        private int _Value;
        /// <summary>
        /// 距离任务开始的时间(单位:秒)
        /// </summary>
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private int _TimeValue;
        /// <summary>
        /// 每次任务的间隔时间.
        /// </summary>
        public int TimeValue
        {
            get
            {
                return _TimeValue;
            }
            set
            {
                _TimeValue = value;
            }
        }

        private Task _MyTask;
        /// <summary>
        /// 
        /// </summary>
        public Task MyTask
        {
            get
            {
                return _MyTask;
            }
            set
            {
                _MyTask = value;
            }
        }


        private bool _IsStartTask;
        /// <summary>
        /// 
        /// </summary>
        public bool IsStartTask
        {
            get
            {
                return _IsStartTask;
            }
            set
            {
                _IsStartTask = value;
            }
        }
        #endregion

        #region 公共函数

        public void SetTask(DateTime time)
        {
            TimeSpan now = time - DateTime.Now;
            Value = (int)now.TotalSeconds;
        }




        #endregion

        #region 私有函数

        #endregion

       

      
    }
}
