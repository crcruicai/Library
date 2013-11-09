/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/11/2 11:34:15
 * 描述说明：
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CRC.Collections;
using System.Diagnostics;

namespace CRC.DebuggerViews
{
    /// <summary>
    /// 为<see cref="StringCollection"/>类 提供调试输出信息 的类.
    /// </summary>
    internal sealed class Fcore_StringCollectionView
    {

        private StringCollection collection;


        public Fcore_StringCollectionView(StringCollection collection)
        {
            if (collection == null)
            {
                throw new Exception("Invalid collection specified");
            }
            this.collection = collection;
        }


        [DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
        public string[] Items
        {
            get
            {
                string[] array = new string[this.collection.Count];
                this.collection.CopyTo(array, 0);
                return array;
            }
        }
    }
}
