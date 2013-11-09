/*********************************************************
 * 开发人员：TopC
 * 创建时间：2013/10/26 9:48:46
 * 描述说明：性能计数器.可以对某个执行函数的执行时间,使用CPU和垃圾回收
 * 次数进行测试.
 * 
 * 更改历史：
 * 
 * *******************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;

namespace CRC.Util
{
    #region 性能计数器2.0版本
    /// <summary>
    /// 表示执行一个任务的接口.
    /// </summary>
    public interface IAction
    {
        void Action();
    }

    /// <summary>
    /// 2.0版本 性能计数器.
    /// </summary>
    public static class CodeTimer2
    {
        #region API
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool GetThreadTimes(IntPtr hThread, out long lpCreationTime,
           out long lpExitTime, out long lpKernelTime, out long lpUserTime);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();
        #endregion

        /// <summary>
        /// 执行任务的委托.
        /// </summary>
        public delegate void ActionDelegate();

        #region 构造器
        /// <summary>
        /// 2.0版本 性能计数器.
        /// </summary>
        static CodeTimer2()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }

        #endregion

        /// <summary>
        /// 获取当前线程执行的时间.
        /// </summary>
        /// <returns></returns>
        private static long GetCurrentThreadTimes()
        {
            long l;
            long kernelTime, userTimer;
            GetThreadTimes(GetCurrentThread(), out l, out l, out kernelTime, out userTimer);
            return kernelTime + userTimer;

        }



        /// <summary>
        /// 执行性能计数器.结构输出到控制台.
        /// </summary>
        /// <param name="name">任务的名称.(必填,为null或Empty都不会执行)</param>
        /// <param name="iteration">重复执行的次数.</param>
        /// <param name="action">执行任务的委托.</param>
        public static void Time(string name, int iteration, ActionDelegate action)
        {
            if (String.IsNullOrEmpty(name)) return;
            if (action == null) return;

            //1. Print name
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);
            // 2. Record the latest GC counts
            //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.Collect(GC.MaxGeneration);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }
            // 3. Run action
            Stopwatch watch = new Stopwatch();
            watch.Start();
            long ticksFst = GetCurrentThreadTimes(); //100 nanosecond one tick

            for (int i = 0; i < iteration; i++) action();
            long ticks = GetCurrentThreadTimes() - ticksFst;
            watch.Stop();
            // 4. Print CPU

            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tTime Elapsed:\t\t" +
               watch.ElapsedMilliseconds.ToString("N0") + "ms");
            Console.WriteLine("\tTime Elapsed (one time):" +
               (watch.ElapsedMilliseconds / iteration).ToString("N0") + "ms");
            Console.WriteLine("\tCPU time:\t\t" + (ticks * 100).ToString("N0")
               + "ns");
            Console.WriteLine("\tCPU time (one time):\t" + (ticks * 100 /
               iteration).ToString("N0") + "ns");

            // 5. Print GC
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGen " + i + ": \t\t\t" + count);
            }
            Console.WriteLine();
        }


        /// <summary>
        /// 执行性能计数器.结构输出到控制台.
        /// </summary>
        /// <param name="name">任务的名称.(必填,为null或Empty都不会执行)</param>
        /// <param name="iteration">重复执行的次数.</param>
        /// <param name="action">执行任务的接口.</param>
        public static void Time(string name, int iteration, IAction action)
        {

            if (String.IsNullOrEmpty(name)) return;

            if (action == null) return;

            //1. Print name
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);

            // 2. Record the latest GC counts
            //GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.Collect(GC.MaxGeneration);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3. Run action
            Stopwatch watch = new Stopwatch();
            watch.Start();
            long ticksFst = GetCurrentThreadTimes(); //100 nanosecond one tick
            for (int i = 0; i < iteration; i++) action.Action();
            long ticks = GetCurrentThreadTimes() - ticksFst;
            watch.Stop();

            // 4. Print CPU
            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tTime Elapsed:\t\t" +
               watch.ElapsedMilliseconds.ToString("N0") + "ms");
            Console.WriteLine("\tTime Elapsed (one time):" +
               (watch.ElapsedMilliseconds / iteration).ToString("N0") + "ms");

            Console.WriteLine("\tCPU time:\t\t" + (ticks * 100).ToString("N0")
                + "ns");

            Console.WriteLine("\tCPU time (one time):\t" + (ticks * 100 /
                iteration).ToString("N0") + "ns");

            // 5. Print GC
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGen " + i + ": \t\t\t" + count);
            }
            Console.WriteLine();
        }

    }
    #endregion

    #region 性能计数器4.0版本
    /// <summary>
    /// 老赵的性能计数器.(该代码使用Net4.0)
    /// <para>代码先执行方法Initialize,然后在执行Time</para>
    /// <para>只能用于控制台.</para> 
    /// <example >
    /// <![CDATA[ 
    ///  
    ///     CodeTimer4.Time("TaskName", 5000, () =>
    ///         {
    ///             int value = 0;
    ///             for (int i = 0; i < 500; i++)
    ///             {
    ///                 value += i;
    ///             }
    ///         });
    /// ]]>
    /// </example>
    /// </summary>
    public static class CodeTimer4
    {

        static CodeTimer4 ()
        {
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;
        }

       

        /// <summary>
        /// 执行性能计数器.结构输出到控制台.
        /// </summary>
        /// <param name="name">任务的名称.</param>
        /// <param name="iteration">重复执行的次数.</param>
        /// <param name="action">任务的执行的方法.</param>
        public static void Time(string name, int iteration, Action action)
        {
            if (String.IsNullOrEmpty(name)) return;

            // 1.保留当前控制台前景色，并使用黄色输出名称参数。
            ConsoleColor currentForeColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(name);

            // 2.强制GC进行收集，并记录目前各代已经收集的次数。
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            // 3.执行代码，记录下消耗的时间及CPU时钟周期1。
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ulong cycleCount = GetCycleCount();
            for (int i = 0; i < iteration; i++) action();
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();

            // 4.恢复控制台默认前景色，并打印出消耗时间及CPU时钟周期。
            Console.ForegroundColor = currentForeColor;
            Console.WriteLine("\tTime Elapsed:\t" + watch.ElapsedMilliseconds.ToString("N0") + "ms");
            Console.WriteLine("\tCPU Cycles:\t" + cpuCycles.ToString("N0"));

            // 5.打印执行过程中各代垃圾收集回收次数。
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                int count = GC.CollectionCount(i) - gcCounts[i];
                Console.WriteLine("\tGen " + i + ": \t\t" + count);
            }

            Console.WriteLine();
        }


        /// <summary>
        /// 执行性能计数器,并获取执行结果.
        /// </summary>
        /// <param name="name">任务的名称.</param>
        /// <param name="iteration">重复执行的次数.</param>
        /// <param name="action">任务的执行的方法.</param>
        public static RunInfo RunAction(string name, int iteration, Action action)
        {
            if (String.IsNullOrEmpty(name)) throw new ArgumentException ("name");

          

            // 强制GC进行收集，并记录目前各代已经收集的次数。
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            int[] gcCounts = new int[GC.MaxGeneration + 1];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gcCounts[i] = GC.CollectionCount(i);
            }

            //执行代码，记录下消耗的时间及CPU时钟周期1。
            Stopwatch watch = new Stopwatch();
            watch.Start();
            ulong cycleCount = GetCycleCount();
            for (int i = 0; i < iteration; i++) action();
            ulong cpuCycles = GetCycleCount() - cycleCount;
            watch.Stop();
            int [] gen=new int [gcCounts.Length];
            for (int i = 0; i <= GC.MaxGeneration; i++)
            {
                gen[i] = GC.CollectionCount(i) - gcCounts[i];

            }
            //返回执行结果.
            RunInfo info = new RunInfo(name,watch.ElapsedMilliseconds, cpuCycles, gen);

            return info;
        }
        /// <summary>
        /// 执行任务结果的信息.
        /// </summary>
        public class RunInfo
        {
            public RunInfo(string name,long time,ulong cycles,int[] gen)
            {
                _Name = name;
                _TimeElapsed = time;
                _CPUCycles = cycles;
                _Gen = gen;
            }

            private string _Name;
            /// <summary>
            /// 执行任务的名称.
            /// </summary>
            public string Name
            {
                get { return _Name; }
               
            }


            private ulong  _CPUCycles;
            /// <summary>
            /// CPU时钟周期
            /// </summary>
            public ulong  CPUCycles
            {
                get { return _CPUCycles; }
                
            }

            private long _TimeElapsed;
            /// <summary>
            /// 消耗时间
            /// </summary>
            public long TimeElapsed
            {
                get { return _TimeElapsed; }
               
            }

            private int[] _Gen;
            /// <summary>
            /// 各代垃圾收集回收次数
            /// </summary>
            public int[] Gen
            {
                get { return _Gen; }
               
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static ulong GetCycleCount()
        {
            ulong cycleCount = 0;
            QueryThreadCycleTime(GetCurrentThread(), ref cycleCount);
            return cycleCount;
        }

        #region API
        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool QueryThreadCycleTime(IntPtr threadHandle, ref ulong cycleTime);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetCurrentThread();
        #endregion
    }

    #endregion
}
