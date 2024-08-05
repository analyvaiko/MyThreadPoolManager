using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolManager
{
    internal class MyThreadPool
    {
        private readonly int WAIT_TIME_THERSHOLD = 60;
        private readonly object locker = new object();
        private readonly Thread[] workers;
        private readonly ConcurrentQueue<MyTask> taskQueue = new ConcurrentQueue<MyTask>();
        private readonly ManualResetEvent shutdownEvent = new ManualResetEvent(false);
        private readonly ManualResetEvent pauseEvent = new ManualResetEvent(true);
        private bool isStopping = false;
        private List<int> wait_times = new List<int>();
        private readonly Stopwatch sw = new Stopwatch();
        private int minFullQueueTime = 5000;
        private int maxFullQueueTime = 0;
        private int regectedTasksNum = 0;



        public MyThreadPool(int workerCount)
        {
            workers = new Thread[workerCount];
            for (int i = 0; i < workerCount; i++)
            {
                workers[i] = new Thread(Worker);
                workers[i].Start();
            }
        }

        private void SetQueueTime(int time)
        {
            lock (locker)
            {
                if (time != 0 && time < minFullQueueTime) { minFullQueueTime = time; }
                if (time > maxFullQueueTime) { maxFullQueueTime = time; }
            }
        }
        public void PrintMetrics()
        {
            double avgWaitTime = wait_times.Sum()/wait_times.Count;

            Console.WriteLine($"\nAverage Wait time is {avgWaitTime} ms");
            Console.WriteLine($"\nMin time empty Queue is {minFullQueueTime} ms");
            Console.WriteLine($"\nMax time empty Queue is {maxFullQueueTime} ms");
        }

        public bool AddTask(MyTask task)
        {
            lock (locker)
            {
                if (isStopping)
                {
                    return false;
                }

                int totalTime = taskQueue.Sum(t => t.GetTaskTime()) + task.GetTaskTime();
                if (totalTime > WAIT_TIME_THERSHOLD)
                {
                    if (!sw.IsRunning)
                    {
                        sw.Start();
                    }
                    
                    regectedTasksNum++;

                    return false;
                }

                if (sw.IsRunning)
                {
                    sw.Stop();
                    SetQueueTime(sw.Elapsed.Milliseconds);
                    
                }
                taskQueue.Enqueue(task);
                // Сповіщаємо усі потоки про наявність задачі у черзі
                Monitor.PulseAll(locker);
                return true;
            }
        }
        public void Stop()
        {
            lock (locker)
            {
                isStopping = true;
                pauseEvent.Set();
                shutdownEvent.Set();
                Monitor.PulseAll(locker);
            }

            foreach (var worker in workers)
            {
                worker.Join();
            }
        }


        public void Pause()
        {
            pauseEvent.Reset();
        }

        public void Resume()
        {
            pauseEvent.Set();
        }

        private void Worker()
        {
            while (true)
            {
                Stopwatch sw = new Stopwatch();
                MyTask task = null;

                lock (locker)
                {
                    if (isStopping && shutdownEvent.WaitOne())
                    {
                        break;
                    }

                    if (!taskQueue.TryDequeue(out task) && !isStopping)
                    {
                        Console.WriteLine("No task to run");
                        sw.Start();
                        Monitor.Wait(locker);
                        sw.Stop();
                        wait_times.Add(sw.Elapsed.Milliseconds);

                    }
                  }

                if (task != null)
                {
                    pauseEvent.WaitOne();
                    int duration = task.GetTaskTime();
                    {
                        Console.WriteLine($"Task {task.GetID()} started, will take {duration} seconds.");
                        Thread.Sleep(duration * 1000);
                        Console.WriteLine($"Task finished by {Thread.CurrentThread.ManagedThreadId}.");
                    }
                }
            }
        }
    }
}
