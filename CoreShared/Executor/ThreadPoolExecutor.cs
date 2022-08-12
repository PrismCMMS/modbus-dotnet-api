using Com.DaacoWorks.Protocol.Exception;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Com.DaacoWorks.Protocol.Executor
{


    /// <summary>
    /// Provides a task scheduler that allows to queue and schedule task
    /// TODO: ensure dequeueing task only after previous task execution is completed
    /// </summary>
    public class ThreadPoolExecutor : TaskScheduler, IDisposable
    {
        private Thread[] pool;
        private BlockingCollection<Task> queue;
        private CountdownEvent threadsExited;
        private readonly TimeSpan TakeFrequency = TimeSpan.FromSeconds(2);
        /// <summary>
        /// represents whether the Executor is shutdown or not
        /// </summary>
        protected bool isShutdown;

        private TaskFactory taskFactory;

        /// <summary>
        /// Instantiates ThreadPoolExecutor
        /// </summary>
        /// <param name="numberOfThreads"></param>
        public ThreadPoolExecutor(int numberOfThreads)
        {
            if (numberOfThreads < 1) throw new ArgumentOutOfRangeException("numberOfThreads");

            queue = new BlockingCollection<Task>();
            isShutdown = false;

            threadsExited = new CountdownEvent(numberOfThreads);
            pool = new Thread[numberOfThreads];
            ThreadStart ts = new ThreadStart(RunPool);
            for (int core = 0; core < pool.Length; core++)
            {
                pool[core] = new Thread(ts);
                pool[core].Start();
            }

            taskFactory = new TaskFactory(this);
        }

        private void RunPool()
        {
            Thread.CurrentThread.IsBackground = true;
            try
            {
                while (!isShutdown)
                {
                    Task t;
                    if (queue.TryTake(out t, TakeFrequency))
                    {
                        base.TryExecuteTask(t);
                    }
                }
            }
            finally
            {
                if (threadsExited != null)
                    threadsExited.Signal();
            }
        }

        /// <summary>
        /// Creates and Submits a task to the queue
        /// </summary>
        /// <param name="runnable"></param>
        /// <returns></returns>
        public Task Submit(IRunnable runnable)
        {
            if (isShutdown)
                throw new ProtocolException("Submit called after executor shutdown");
            return taskFactory.StartNew(runnable.Run, runnable.CancellationToken);
        }

        /// <summary>
        /// Shutsdown ThreadPoolExecutor
        /// </summary>
        public void Shutdown()
        {
            Dispose();
        }

        /// <summary>
        /// Gets max concurrency level
        /// </summary>
        public override int MaximumConcurrencyLevel
        {
            get
            {
                return pool.Length + 1;
            }
        }

        /// <summary>
        /// Gets all scheduled tasks
        /// </summary>
        /// <returns>scheduled tasks</returns>
        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return queue.ToList();
        }

        /// <summary>
        /// Queues the task 
        /// </summary>
        /// <param name="task"></param>
        protected override void QueueTask(Task task)
        {
            if (!isShutdown)
            {
                //TODO: seek option to add to the top of the queue for scheduled task to run first from the queue
                queue.Add(task);
            }
        }

        /// <summary>
        /// Tries executing task inline
        /// </summary>
        /// <param name="task"></param>
        /// <param name="taskWasPreviouslyQueued"></param>
        /// <returns></returns>
        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if (!isShutdown && !taskWasPreviouslyQueued)
            {
                return base.TryExecuteTask(task);
            }

            return false;
        }

        /// <summary>
        /// Gets whether the Executor is shutdown or not
        /// </summary>
        public bool IsShutdown
        {
            get
            {
                return isShutdown;
            }
        }

        /// <summary>
        /// Disposes ThreadpoolExecutor
        /// </summary>
        public virtual void Dispose()
        {
            isShutdown = true;

            if (null != queue)
            {
                queue.CompleteAdding();
                if (null != threadsExited)
                {
                    threadsExited.Wait(TakeFrequency);
                    threadsExited.Dispose();
                    threadsExited = null;
                }
                queue.Dispose();
                queue = null;
            }
            GC.SuppressFinalize(this);

        }

    }
}
