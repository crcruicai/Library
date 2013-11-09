/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/9 9:15:57
 * 描述说明：排序分割表.
 * 它的实现原理:在插入时就进行排序,并将数据分成若干个段,
 * 每个段使用一个List列表存储元素,然后给每个段设置一个索引头,索引项.
 * 每个插入,删除,查找 就使用二叉树来查找.
 * 
 * 更改历史：
 *          测试完成.
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace CRC.Collections
{
    /// <summary>
    /// 排序分割表.
    /// <para>可用于10万行以上的表</para>
    /// 来源:http://www.codeproject.com/Articles/610399/SortedSplitList-An-indexing-algorithm-in-C-sharp
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SortedSplitList<T> : IEnumerable<T>
    {
        /// <summary>
        /// 垂直索引节点
        /// </summary>
        /// <typeparam name="TNode"></typeparam>
        internal sealed class VerticalIndexNode<TNode>
        {
            /// <summary>
            /// 第一个节点
            /// </summary>
            public TNode FirstItem;
            /// <summary>
            /// 节点列表.
            /// </summary>
            public List<TNode> List = new List<TNode>();
            /// <summary>
            /// 开始的索引.
            /// </summary>
            public int BeginIndex;
        }

        /// <summary>
        /// 第一个项比较器.
        /// </summary>
        internal sealed class CompareByFirstItem : IComparer<VerticalIndexNode<T>>
        {
            public IComparer<T> LocalComparer = null;

            public CompareByFirstItem(IComparer<T> defaultComparer)
            {
                LocalComparer = defaultComparer;
            }

            public int Compare(VerticalIndexNode<T> x, VerticalIndexNode<T> y)
            {
                return LocalComparer.Compare(x.FirstItem, y.FirstItem);
            }
        }

        /// <summary>
        /// 节点开始索引比较器.
        /// </summary>
        internal sealed class CompareByBeginIndex : IComparer<VerticalIndexNode<T>>
        {
            public int Compare(VerticalIndexNode<T> x, VerticalIndexNode<T> y)
            {
                return x.BeginIndex - y.BeginIndex;
            }
        }
        /// <summary>
        /// 默认比较器.
        /// </summary>
        private readonly IComparer<T> _defaultComparer;
        /// <summary>
        /// 节点列表.
        /// </summary>
        private readonly List<VerticalIndexNode<T>> _verticalIndex = new List<VerticalIndexNode<T>>();
        /// <summary>
        /// 首项比较器.
        /// </summary>
        private readonly CompareByFirstItem _verticalComparer;
        /// <summary>
        /// 开始索引比较器.
        /// </summary>
        private readonly CompareByBeginIndex _indexComparer = new CompareByBeginIndex();
        /// <summary>
        /// 元素的数量.
        /// </summary>
        private int _count;
        /// <summary>
        /// 集合是否已经被修改.
        /// </summary>
        private bool _isDirty;
        /// <summary>
        /// 深度.
        /// </summary>
        private readonly int _deepness;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public T this[int i]
        {
            get
            {
                if (i < 0) throw new IndexOutOfRangeException("L'index est négatif.");
                RecalcIndexerIfDirty();
                int index = GetHorizontalTable(default(T), i, null);
                int beginIndex = _verticalIndex[index].BeginIndex;
                List<T> currentList = _verticalIndex[index].List;
                return currentList[i - beginIndex];
            }
        }

        /// <summary>
        /// 获取元素的数量.
        /// </summary>
        public int Count { get { return _count; } }

        /// <summary>
        /// 构造一个分割表.
        /// </summary>
        /// <param name="defaultComparer">默认元素比较器.</param>
        /// <param name="deepness"></param>
        public SortedSplitList(IComparer<T> defaultComparer, int deepness = 1000)
        {
            if (defaultComparer == null)
                throw new ArgumentNullException();

            _defaultComparer = defaultComparer;
            _verticalComparer = new CompareByFirstItem(_defaultComparer);
            _deepness = deepness;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return _verticalIndex.SelectMany(verticalIndexNode => verticalIndexNode.List).GetEnumerator();
        }
        /// <summary>
        /// 添加一个元素.
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            if (_verticalIndex.Count == 0)
            {
                _verticalIndex.Add(new VerticalIndexNode<T>());
                _verticalIndex[0].List.Add(item);
                _verticalIndex[0].FirstItem = item;
            }
            else
            {
                InternalAdd(item);
            }
            _count++;
            _isDirty = true;
        }

        /// <summary>
        /// 移除元素.
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            bool deletedNode = false;
            int vi = GetHorizontalTable(item, -1, null);
            List<T> currentTable = _verticalIndex[vi].List;

            int positionToRemove = FastBinarySearch(currentTable, item, _defaultComparer);
            if (positionToRemove >= 0)
            {

                currentTable.RemoveAt(positionToRemove);

                if (currentTable.Count == 0)
                {
                    _verticalIndex.RemoveAt(vi);
                    deletedNode = true;
                }
                else
                    if (vi > 0 && (_verticalIndex[vi - 1].List.Count + currentTable.Count) < _deepness)
                    {
                        _verticalIndex[vi - 1].List.AddRange(currentTable);
                        currentTable.Clear();
                        _verticalIndex.RemoveAt(vi);
                        deletedNode = true;
                    }

                if (deletedNode == false && positionToRemove == 0)
                    _verticalIndex[vi].FirstItem = _verticalIndex[vi].List[0];

                _count--;
                _isDirty = true;
            }
            else
                throw new DeletedRowInaccessibleException("L'element que vous essayez de supprimer est introuvable");

        }

        /// <summary>
        /// 移除指定条件的所有元素.
        /// </summary>
        /// <param name="match"></param>
        public void RemoveAll(Predicate<T> match)
        {
            for (var i = Count - 1; i >= 0; i--)
            {
                var obj = this[i];
                if (match(obj))
                {
                    Remove(obj);
                    _isDirty = false;
                }
            }
            _isDirty = true;
        }

        /// <summary>
        /// 清理所有元素.
        /// </summary>
        public void Clear()
        {
            foreach (VerticalIndexNode<T> node in _verticalIndex)
                node.List.Clear();

            _verticalIndex.Clear();
            _verticalIndex.TrimExcess();
            _count = 0;
            _isDirty = true;
        }

        /// <summary>
        /// 二叉树搜索.
        /// </summary>
        /// <param name="comparisonItem"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public int BinarySearch(T comparisonItem, IComparer<T> comparer = null)
        {
            RecalcIndexerIfDirty();
            int vIndex = GetHorizontalTable(comparisonItem, -1, comparer ?? _defaultComparer);
            if (vIndex < 0)
                return vIndex;
            int begin = _verticalIndex[vIndex].BeginIndex;
            List<T> internalArray = _verticalIndex[vIndex].List;
            int realIndex = FastBinarySearch(internalArray, comparisonItem, comparer ?? _defaultComparer);
            return (realIndex >= 0) ? realIndex + begin : realIndex - begin;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="comparisonItem"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public IEnumerable<T> PartiallyEnumerate(T comparisonItem, IComparer<T> comparer = null)
        {
            IComparer<T> currentComparer = comparer ?? _defaultComparer;
            int index = BinarySearch(comparisonItem, comparer);
            if (index >= 0)
            {
                for (; index > 0 && currentComparer.Compare(comparisonItem, this[index - 1]) == 0; index--) ;
                for (; index < Count && currentComparer.Compare(comparisonItem, this[index]) == 0; index++)
                    yield return this[index];
            }
        }
        public T Retrieve(T comparisonItem, IComparer<T> comparer = null)
        {
            int index = BinarySearch(comparisonItem, comparer ?? _defaultComparer);
            if (index >= 0) return this[index];
            return default(T);
        }
        /// <summary>
        /// 快速二叉树查找.(这是一个二值查找算法,如果查到,就返回元素的索引位置,如果没有查到,
        /// 就返回距离该元素最近的位置的相反值(即该元素可以插入的位置的负值))
        /// </summary>
        /// <param name="array"></param>
        /// <param name="value"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        private int FastBinarySearch(List<T> array, T value, IComparer<T> comparer)
        {
            int num = 0;
            int num2 = array.Count - 1;
            while (num <= num2)
            {
                int num3 = num + ((num2 - num) >> 1);//计算中间的位置.
                int num4 = comparer.Compare(array[num3], value);//比较两个值.
                if (num4 == 0)
                {
                    return num3;//已经查找成功.
                }
                if (num4 < 0)
                {
                    num = num3 + 1;//
                }
                else
                {
                    num2 = num3 - 1;
                }
            }
            return ~num;//没有找到,返回最接近于value 的索引位置.
        }

        /// <summary>
        /// 获取元素在水平表中的位置.
        /// </summary>
        /// <param name="containItem"></param>
        /// <param name="containIndex"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        private int GetHorizontalTable(T containItem, int containIndex, IComparer<T> comparer)
        {
            var localStruct = new VerticalIndexNode<T> { FirstItem = containItem, BeginIndex = containIndex };

            if (comparer != null)//使用给定比较器.
                _verticalComparer.LocalComparer = comparer;

            int index = _verticalIndex.BinarySearch(localStruct, (containIndex < 0) ? _verticalComparer : (IComparer<VerticalIndexNode<T>>)_indexComparer);

            if (comparer != null)//恢复默认的比较器.
                _verticalComparer.LocalComparer = _defaultComparer;

            if (index < 0)
            {
                if (~index < _verticalIndex.Count)
                {

                    if (~index <= 1)
                        return 0;
                    return ~index - 1;
                }
                return _verticalIndex.Count - 1;
            }
            return index;
        }
        /// <summary>
        /// 内部Add方法.
        /// </summary>
        /// <param name="item"></param>
        private void InternalAdd(T item)
        {
            int vi = GetHorizontalTable(item, -1, null);
            List<T> currentTable = _verticalIndex[vi].List;

            int position = FastBinarySearch(currentTable, item, _defaultComparer);

            if (position < 0)
            {
                if (~position < currentTable.Count)
                {
                    position = ~position;
                }
                else
                    position = -1;
            }

            if (currentTable.Count < _deepness)
            {
                if (position == 0)
                {
                    _verticalIndex[vi].FirstItem = item;
                    currentTable.Insert(position, item);
                }
                else if (position > 0)
                    currentTable.Insert(position, item);
                else
                    currentTable.Add(item);
                return;
            }

            int median = _deepness >> 1;
            if (position >= 0 && position <= median)
            { // si l'insertion se fait dans la partie A

                // si nous somme sur la derniere table, ou que la table suivante ne peut pas accueillir
                // la moitier B, nous créont un nouveau noeud
                if (vi == _verticalIndex.Count - 1 || _verticalIndex[vi + 1].List.Count > median)
                {
                    _verticalIndex.Insert(vi + 1, new VerticalIndexNode<T>());
                }

                // deplacement de B dans le noeud d'en dessous
                _verticalIndex[vi + 1].List.InsertRange(0, currentTable.GetRange(median, currentTable.Count - median));
                currentTable.RemoveRange(median, currentTable.Count - median);

                // insertion de x dans A
                currentTable.Insert(position, item);

                // mise à jour des indexs
                _verticalIndex[vi + 1].FirstItem = _verticalIndex[vi + 1].List[0];
                if (position == 0)
                    _verticalIndex[vi].FirstItem = _verticalIndex[vi].List[0];

            }
            else
            { // sinon, l'insertion se fait dans la partie B

                // si nous somme sur la derniere table, ou que la table suivante ne peut pas accueillir
                // la moitier B, nous créont un nouveau noeud
                if (vi == _verticalIndex.Count - 1 || _verticalIndex[vi + 1].List.Count > median)
                {
                    _verticalIndex.Insert(vi + 1, new VerticalIndexNode<T>());
                }

                // insertion de x dans B
                if (position > 0)
                    currentTable.Insert(position, item);
                else
                    currentTable.Add(item);

                // deplacement de B dans le noeud d'en dessous
                _verticalIndex[vi + 1].List.InsertRange(0, currentTable.GetRange(median, currentTable.Count - median));
                currentTable.RemoveRange(median, currentTable.Count - median);

                // mise à jour des indexs
                _verticalIndex[vi + 1].FirstItem = _verticalIndex[vi + 1].List[0];
                if (position == 0)
                    _verticalIndex[vi].FirstItem = _verticalIndex[vi].List[0];
            }
        }
        /// <summary>
        /// 如果已经被修改过,将重新计算索引,
        /// </summary>
        private void RecalcIndexerIfDirty()
        {
            if (_isDirty)
            {
                //重新 计算新的开始索引.这里的索引,表示该元素在列表中位置.
                int begin = 0;
                foreach (VerticalIndexNode<T> item in _verticalIndex)
                {
                    item.BeginIndex = begin;
                    begin += item.List.Count;
                }
                _isDirty = false;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
