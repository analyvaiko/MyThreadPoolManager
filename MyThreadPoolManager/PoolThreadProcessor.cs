using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolManager
{
    internal class PoolThreadProcessor
    {
        const int NUM_MGRS = 2;
        const int NUM_THREADS = 4;
        static Random random = new Random();
        static bool isRuning, isStopping = false;
        static List<Thread> threads = new List<Thread>();

        public static MyThreadPool Run(int threadsNumber = 1, int minTime = 0, int maxTime= 1)
        {
            if (!isRuning)
            {
                 MyThreadPool pool = new MyThreadPool(NUM_THREADS);

                for (int i = 0; i < threadsNumber; i++)
                {
                    Thread thread = new Thread(() =>
                    {
                        while (!isStopping)
                        {
                            MyTask task = new MyTask();
                            bool added = pool.AddTask(task);

                            if (!added)
                            {
                                Console.WriteLine("Task rejected due to time limit.");
                            }
                            Thread.Sleep(random.Next(minTime*1000, maxTime*1000)); // Random wait before trying to add another task
                        }
                    });
                    threads.Add(thread);
                    thread.Start();
                }
                isRuning = true;
                return pool;
            }
            return null;
        }
        public static void Stop(MyThreadPool pool)
        {
            if (isRuning)
            {
                isStopping = true;
                Debug.WriteLine($"Stop {isStopping}");
                foreach (Thread thread in threads)
                {
                    thread.Join();
                }

                threads.Clear();

                isRuning = false;
                isStopping = false;

                if (pool != null)
                {
                    pool.Stop();
                    pool.PrintMetrics();
                    pool = null;
                }

            }
        }


    }
}
