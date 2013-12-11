/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/29 9:19:15
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
namespace CRC.Util
{

    /// <summary>
    /// 震动窗体帮助器.
    /// </summary>
    public class ShockForm
    {
        #region 字段与变量
        /// <summary>
        /// 表示一个点.
        /// </summary>
        Point? origLoc;
        /// <summary>
        /// 用于准确的测量时间.
        /// </summary>
        Stopwatch stopwatch = new Stopwatch();

        /// <summary>
        /// 震动的路径.
        /// </summary>
        Point[] shockPath = new Point[] 
        {
			new Point(5, -2),
			new Point(-0, 0),
			new Point(-4, -1)
		};

        int shockPathIndex = 0;

        #endregion 字段与变量

        #region 构造函数
        public ShockForm()
        {
            Interval = 5.0;
        }
        #endregion 构造函数

        #region 属性
        /// <summary>
        /// 震动时间.
        /// </summary>
        public double Interval { get; set; }


        #endregion 属性

        #region 公共函数


        /// <summary>
        /// 开始震动窗体.
        /// </summary>
        /// <param name="father">指定要震动的窗体.</param>
        public void StartShock(Form father)
        {
            ThreadPool.QueueUserWorkItem(WorkShock, father);
        }



        #endregion 公共函数

        #region 私有函数
        private void WorkShock(object obj)
        {
            Form form = obj as Form;
            if (form == null) return;
            while (true)
            {
                if (!origLoc.HasValue)
                {
                    origLoc = form.Location;
                    stopwatch.Reset();
                    stopwatch.Start();
                    shockPathIndex = 0;
                }
                //拷贝一个新的点.
                Point loc = new Point(origLoc.Value.X, origLoc.Value.Y);
                //进行位置变换.
                loc.Offset(shockPath[shockPathIndex]);
                //将新位置传递给窗体
                QueueInvoke(form, () => form.Location = loc);
                //通过不停的变换位置达到震动的效果.

                shockPathIndex = (shockPathIndex + 1) % shockPath.Length;
                //如果震动已经到达15秒,停止震动.
                if (stopwatch.Elapsed.TotalSeconds >= Interval)
                {
                    if (origLoc != null)
                    {
                        QueueInvoke(form, () => form.Location = origLoc.Value);
                    }

                    origLoc = null;
                    stopwatch.Stop();
                    return;
                }
                Thread.Sleep(50);

            }
        }
        static void QueueInvoke(System.Windows.Forms.Control ctl, Action doit)
        {
            if (ctl == null) throw new ArgumentNullException("ctl");
            ctl.BeginInvoke(doit);
        }
        #endregion 私有函数



    }

}
