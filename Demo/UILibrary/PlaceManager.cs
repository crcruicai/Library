using System;
using System.Collections.Generic;
using System.Text;

namespace UILibrary
{
    public sealed class PlaceManager
    {
        private List<int> m_listIndex;//状态
        private int m_maxCount;//最大可显示个数

        public PlaceManager(int maxCount)
        {
            m_listIndex = new List<int>(maxCount);//存储状态
            for (int i = 0; i < maxCount; i++)
                m_listIndex.Add(0);
            m_maxCount = maxCount;
        }
        public void FreePlaceIndex(int placeIndex)
        {
            if (placeIndex < 0 || placeIndex >=m_maxCount)
            {
                return;
            }

            lock (this)
            {
                m_listIndex[placeIndex]=0;//设为可用
            }
        }
        //分配一个缓冲块
        public int GetPlaceIndex()
        {
            lock (this)
            {
                int placeIndex = 0;
                while (m_listIndex[placeIndex] != 0)//找到第一个不被占用的
                {
                    placeIndex++;
                    placeIndex = Math.Min(placeIndex, m_maxCount-1);
                }
                m_listIndex[placeIndex] = 1;
                return placeIndex;
            }
        }
    }
}
