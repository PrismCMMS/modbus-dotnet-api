using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// ScheduledTask represents a task being scheduled
    /// </summary>
    public class ScheduledTask : IDisposable
    {
        private ScheduledThreadPoolExecutor executor;
        private Timer timerScheduled;
        private ConcurrentDictionary<int, Task> tasks;
        private bool isDisposed;
        private int period = Timeout.Infinite;


        /// <summary>
        /// Instantiates scheduled task
        /// </summary>
        /// <param name="executor"></param>
        /// <param name="taskScheduled"></param>
        /// <param name="dueTime"></param>
        /// <param name="period"></param>
        internal ScheduledTask(ScheduledThreadPoolExecutor executor, IRunnable taskScheduled, TimeSpan dueTime, TimeSpan period)
        {

            isDisposed = false;
            this.executor = executor;
            tasks = new ConcurrentDictionary<int, Task>();
            this.period = (int)period.TotalMilliseconds;

            this.timerScheduled = new Timer(SubmitInner, taskScheduled, dueTime, period);

        }

        private void SubmitInner(Object parameter)
        {
            if (timerScheduled!=null && !isDisposed && !executor.IsShutdown && parameter is IRunnable taskScheduled)
            {
                timerScheduled.Change(Timeout.Infinite, Timeout.Infinite); //stops the timer
                ScheduleTask(taskScheduled);
                timerScheduled.Change(period, period);
            }
        }

        private Task ScheduleTask(IRunnable taskScheduled)
        {
            var task = executor.Submit(taskScheduled);
            task.ContinueWith((currentTask) =>
            {
                if (!isDisposed)
                {
                    tasks.TryRemove(currentTask.Id, out Task removedTask);
                }
            });

            tasks.TryAdd(task.Id, task);
            return task;
        }

        /// <summary>
        /// Disposes a scheduled task
        /// </summary>
        public void Dispose()
        {
            if (isDisposed) return;
            isDisposed = true;
            executor = null;
            if (timerScheduled != null)
            {
                timerScheduled.Dispose();
            }
            timerScheduled = null;
            tasks.Clear();
            tasks = null;
            GC.SuppressFinalize(this);
        }
    }
}
