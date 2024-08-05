using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPoolManager
{
    internal class MyTask
    {
        private readonly int MIN_TASK_DURATION = 5;
        private readonly int MAX_TASK_DURATION = 12;
        private string ID;
        private int TaskTime;


        public MyTask()
        {
            Guid guid = Guid.NewGuid();
            Random rnd = new Random();
            ID = guid.ToString();
            TaskTime = rnd.Next(MIN_TASK_DURATION, MAX_TASK_DURATION);
        }

        public int GetTaskTime()
        {
            return TaskTime;
        }

        public string GetID()
        {
            return ID;
        }
    }
}
