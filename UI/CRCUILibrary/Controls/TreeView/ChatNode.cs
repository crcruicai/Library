#region 说明
/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/21 8:46:48
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
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace CRC.Controls
{
    /// <summary>
    /// ChatTreeView的一个节点.
    /// </summary>
    public class ChatNode
    {
        #region 构造函数

        public ChatNode()
        {
            _Nodes = new ChatNodeCollection(this);
        }

       

	    #endregion


        #region 属性
        private ChatNode  _FatherNode;
        /// <summary>
        /// 节点的父节点.
        /// </summary>
        [Browsable(false)]
        public ChatNode FatherNode
        {
            get { return _FatherNode; }
            set { _FatherNode = value; }
        }

        private Control  _Owner;
        /// <summary>
        /// 节点所依附的控件.
        /// </summary>
        [Browsable(false)]
        public Control Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        private ChatNodeCollection  _Nodes;
        /// <summary>
        /// 节点的集合.
        /// </summary>
        public ChatNodeCollection Nodes
        {
            get { return _Nodes; }

        }

        private string  _Text;
        /// <summary>
        /// 文本.
        /// </summary>
        public string  Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        private Rectangle _Bounds;
        /// <summary>
        /// 获取节点显示的区域.
        /// </summary>
        [Browsable(false)]
        public Rectangle Bounds
        {
            get { return _Bounds; }
            set { _Bounds = value; }
        }


        private bool  _IsOpen=false;
        /// <summary>
        /// 获取或设置节点是否展开.
        /// </summary>
        [DefaultValue(false)]
        public bool IsOpen
        {
            get { return _IsOpen; }
            set { _IsOpen = value; }
        }


        #endregion
    }

    public class ChatNodeCollection :IList<ChatNode> ,ICollection <ChatNode >,IEnumerable <ChatNode >
    {
        #region 字段与变量
        private int _Count;
        private ChatNode _Father;
        private ChatNode[] _Nodes;
        private Control _Owner;


        #endregion


        #region 构造函数

        public ChatNodeCollection()
        {
            _Father = null;
            _Owner =null;
        }

        public ChatNodeCollection(ChatNode father)
        {
            _Father = father;
            _Owner = null;
        }

        public ChatNodeCollection(Control owner):this(owner ,null)
        {
          
        }

        public ChatNodeCollection(Control owner,ChatNode father)
        {
            if (owner == null) throw new ArgumentNullException("owner");
            _Father = father;
            _Owner = owner;
        }

        #endregion





        /// <summary>
        /// 确认存储空间是否足够,不足则自动扩展.
        /// </summary>
        /// <param name="elements">使用空间的数量.</param>
        private void EnsureSpace(int elements)
        {
            if (_Nodes == null)
                _Nodes = new ChatNode[Math.Max(elements, 4)];
            else if (_Count + elements > _Nodes.Length)
            {
                //存储空间不足.必须申请新的空间.
                ChatNode[] arrTemp = new ChatNode[Math.Max(_Count + elements, _Nodes.Length * 2)];
                _Nodes.CopyTo(arrTemp, 0);
                _Nodes = arrTemp;
            }
        }

        /// <summary>
        /// 使所依附的控件整个画面无效,并重绘.
        /// </summary>
        private void Invalidate()
        {
            if (_Owner != null)
            {
                _Owner.Invalidate();
            }
        }
        /// <summary>
        /// 使所依附的控件整个画面无效,并重绘.
        /// </summary>
        /// <param name="region"></param>
        private void Invalidate(Region region)
        {
            if (_Owner != null)
            {
                _Owner.Invalidate(region);
            }
        }

        #region AddRange方法.
        /// <summary>
        /// 将枚举器的元素添加到集合中
        /// <para>如果只有少量,请使用数组添加,因为该方法性能不佳.</para>
        /// <para>或者使用带有指定数量的重载方法.</para>
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<ChatNode> items)
        {
            if (items == null)
                throw new ArgumentNullException("Items");
            try
            {
                foreach (var item in items )
                {
                    if (items == null) throw new ArgumentNullException("Item");
                    this.EnsureSpace(1);
                    item.Owner = _Owner;
                    item.FatherNode = _Father;
                    _Nodes[_Count++] = item;
                }
            }
            finally
            {
                Invalidate();
            }

        }
        /// <summary>
        /// 将枚举器指定数量的元素添加到集合中.
        /// </summary>
        /// <param name="items"></param>
        /// <param name="length">指定的数量.</param>
        public void AddRange(IEnumerable<ChatNode> items,int length)
        {
            if (items == null)
                throw new ArgumentNullException("Items");
            this.EnsureSpace(length);
            try
            {
                int i = 0;
                foreach (var item in items)
                {
                    if (items == null) throw new ArgumentNullException("Item");
                    item.Owner = _Owner;
                    item.FatherNode = _Father;
                    _Nodes[_Count++] = item;
                    i++;
                    if (i >= length) break;
                }
            }
            finally
            {
                Invalidate();
            }

        }
        /// <summary>
        /// 将数组的元素添加到机会中.
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(ChatNode[] items)
        {
            if (items == null)
                throw new ArgumentNullException("Items");
            this.EnsureSpace(items.Length);
            try
            {
                foreach (var item in items)
                {
                    if (items == null) throw new ArgumentNullException("Item");
                    item.Owner = _Owner;
                    item.FatherNode = _Father;
                    _Nodes[_Count++] = item;
                }
            }
            finally
            {
                Invalidate();
            }
        }

        #endregion

        #region IList<ChatNode> 成员
        /// <summary>
        /// 获取列表项所在的索引位置
        /// </summary>
        /// <param name="item">要获取的列表项</param>
        /// <returns>索引位置</returns>
        public int IndexOf(ChatNode item)
        {
            return Array.IndexOf<ChatNode>(_Nodes, item);
        }
        /// <summary>
        /// 根据索引位置插入一个列表项
        /// </summary>
        /// <param name="index">索引位置</param>
        /// <param name="item">要插入的列表项</param>
        public void Insert(int index, ChatNode item)
        {
            if (index < 0 || index >= _Count)
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            }
            if (item == null)
                throw new ArgumentNullException("Item cannot be null");
            this.EnsureSpace(1);
            for (int i = _Count; i < index; i++)
            {
                _Nodes[i] = _Nodes[i - 1];
            }
            item.Owner = _Owner;
            item.FatherNode = _Father;
            _Nodes[index] = item;
            _Count++;
            Invalidate();

        }

        /// <summary>
        /// 根据索引位置删除一个节点.
        /// </summary>
        /// <param name="index">索引位置</param>
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _Count)
            {
                throw new IndexOutOfRangeException("Index was outside the bounds of the array");
            }
            _Count--;
            for (int i = index,Len=_Count; i < Len; i++)
            {
                _Nodes[i] = _Nodes[i + 1];
            }
            Invalidate();
        }
        /// <summary>
        /// 根据索引获取一个列表项
        /// </summary>
        /// <param name="index">索引位置</param>
        /// <returns>列表项</returns>
        public ChatNode this[int index]
        {
            get
            {
                if (index < 0 || index >= _Count)
                {
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                }
                return _Nodes[index];
            }
            set
            {
                if (index < 0 || index >= _Count)
                {
                    throw new IndexOutOfRangeException("Index was outside the bounds of the array");
                }
                _Nodes[index] = value;
            }
        }

        #endregion

        #region ICollection<ChatNode> 成员
        /// <summary>
        /// 添加一个列表项
        /// </summary>
        /// <param name="item">要添加的列表项</param>
        public void Add(ChatNode item)
        {
            if (item == null) throw new ArgumentNullException("item");
            this.EnsureSpace(1);
            item.FatherNode = _Father;
            item.Owner = _Owner;
            _Nodes[_Count++] = item;
            Invalidate();
        }
        /// <summary>
        /// 清空所有列表项
        /// </summary>
        public void Clear()
        {
            _Count = 0;
            _Nodes = null;
            Invalidate();
        }
        /// <summary>
        /// 判断一个列表项是否在集合内
        /// </summary>
        /// <param name="item">要判断的列表项</param>
        /// <returns>是否存在列表项</returns>
        public bool Contains(ChatNode item)
        {
            return IndexOf(item) != -1;
        }
        /// <summary>
        /// 将列表项的集合拷贝至一个数组
        /// </summary>
        /// <param name="array">目标数组</param>
        /// <param name="index">拷贝的索引位置</param>
        public void CopyTo(ChatNode[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array cannot be null");
            _Nodes.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 获取元素的数量.
        /// </summary>
        public int Count
        {
            get { return _Count; }
        }

        /// <summary>
        /// 集合是否为只读.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// 移除一个列表项
        /// </summary>
        /// <param name="item">要移除的列表项</param>
        public bool Remove(ChatNode item)
        {
            int index = this.IndexOf(item);
            if (-1 != index)        //如果存在元素 那么根据索引移除
            {
                this.RemoveAt(index);
                return true;
            }
            return false;
        }

        #endregion

        #region IEnumerable<ChatNode> 成员

        public IEnumerator<ChatNode> GetEnumerator()
        {
            foreach (var item in _Nodes )
            {
                yield return item;
            }
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (var item in _Nodes)
            {
                yield return item;
            }
        }

        #endregion
    }
}
