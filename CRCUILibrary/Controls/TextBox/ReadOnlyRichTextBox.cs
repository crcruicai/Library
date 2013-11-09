/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/2 16:49:56
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace CRC.Controls
{
    /// <summary>
    /// 只读模式的RichTextBox.
    /// <para>与RichTextBox设置属性ReadOnly不同,本控件不能选择文字.不能复制.</para>
    /// </summary>
    public class ReadOnlyRichTextBox : RichTextBox
    {
        public ReadOnlyRichTextBox()
        {
            ReadOnly = true;
            TabStop = false;

            SetStyle(ControlStyles.Selectable, false);
        }

        // Font is overridden because assigning the RichTextBox to a new parent sets
        // its Font to the parent's Font and thus loses all formatting of existing RTF content.
        protected Font _font = new Font("Arial", 10);
        /// <summary>
        /// 获取或设置字体.
        /// </summary>
        public override Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
            }
        }

        /// <summary>
        /// 是否只读.
        /// </summary>
        [Browsable(false)]
        public new bool ReadOnly
        {
            get { return true; }
            set { }
        }

        /// <summary>
        /// 
        /// </summary>
        [Browsable(false)]
        public new bool TabStop
        {
            get { return false; }
            set { }
        }

        const int WM_SETFOCUS = 0x0007;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_SETFOCUS://取消焦点
                    // We don't want the RichTextBox to be able to receive focus, so just
                    // pass the focus back to the control it came from.
                    IntPtr prevCtl = m.WParam;
                    Control c = Control.FromHandle(prevCtl);
                    c.Select();
                    return;
            }
            base.WndProc(ref m);
        }
    }
}
