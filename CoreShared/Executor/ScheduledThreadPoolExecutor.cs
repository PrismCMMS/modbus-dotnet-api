using Com.DaacoWorks.Protocol.Exception;
using System;
using System.Collections.Generic;

namespace Com.DaacoWorks.Protocol.Executor
{

    /// <summary>
    /// Provides a task scheduler that allows to queue and schedule task
    /// TODO: ensure dequeueing task only after previous task execution is completed
    /// </summary>
    internal class ScheduledThreadPoolExecutor : ThreadPoolExecutor
    {
        private List<ScheduledTask> scheduledTasks;

        public ScheduledThreadPoolExecutor(int numberOfThreads) : base(numberOfThreads)
        {
            scheduledTasks = new List<ScheduledTask>();
        }

        protected ScheduledTask ScheduleAtFixedRate(IRunnable runnable, TimeSpan dueTime, TimeSpan period)
        {
            if (isShutdown)
                throw new ProtocolException("ScheduleWithFixedDelay called after executor shutdown");

            var scheduledTask = new ScheduledTask(this, runnable, dueTime, period);
            scheduledTasks.Add(scheduledTask);
            return scheduledTask;

        }

        public override void Dispose()
        {
            base.Dispose();
            if (null != scheduledTasks)
            {
                foreach (var scheduledTask in scheduledTasks)
                {
                    scheduledTask.Dispose();
                }
                scheduledTasks = null;
            }
        }


    }
}
