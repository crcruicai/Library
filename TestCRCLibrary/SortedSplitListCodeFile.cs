using System.Collections.Generic;
using System;

namespace TestCRCLibrary
{
    public class CompareByDateId : IComparer<TestObject>
    {
        public int Compare(TestObject x, TestObject y)
        {
            int dateResult = x.Date.CompareTo(y.Date);
            if (dateResult == 0)
            {
                if (x.Id == y.Id)
                    return 0;
                if (x.Id < y.Id)
                    return -1;
                return 1;
            }
            return dateResult;
        }
    }

    public class CompareById : IComparer<TestObject>
    {
        public int Compare(TestObject x, TestObject y)
        {
            if (x.Id == y.Id)
                return 0;
            if (x.Id < y.Id)
                return -1;
            return 1;
        }
    }

    public class CompareByDate : IComparer<TestObject>
    {
        public int Compare(TestObject x, TestObject y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }

    public class TestObject
    {
        public int Id { get; set; }
        public int Id2 { get; set; }
        public DateTime Date { get; set; }
        public string Data { get; set; }
    }    
}