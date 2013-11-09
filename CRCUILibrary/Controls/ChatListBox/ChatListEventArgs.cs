using System;
using System.Collections.Generic;
using System.Text;

namespace CRC.Controls
{
    //自定义事件参数类
    /// <summary>
    /// ChatListBox的事件类.
    /// </summary>
    public class ChatListEventArgs
    {
        private ChatListSubItem mouseOnSubItem;
        /// <summary>
        /// 被鼠标停留的子项.
        /// </summary>
        public ChatListSubItem MouseOnSubItem {
            get { return mouseOnSubItem; }
        }

        private ChatListSubItem selectSubItem;
        /// <summary>
        /// 被鼠标选中的子项.
        /// </summary>
        public ChatListSubItem SelectSubItem {
            get { return selectSubItem; }
        }

        public ChatListEventArgs(ChatListSubItem mouseonsubitem, ChatListSubItem selectsubitem) {
            this.mouseOnSubItem = mouseonsubitem;
            this.selectSubItem = selectsubitem;
        }
    }
}
