using System;

namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// RequestExecutor is a executor service which executes and schedules tasks.
    /// </summary>
    internal abstract class RequestExecutor : ScheduledThreadPoolExecutor, IRequestExecutor
    {

        public RequestExecutor(int threads) : base(threads)
        {

        }

        public void Submit(IExecutorTask task)
        {
            base.Submit(task);
        }


        public void Schedule(IExecutorTask task, TimeSpan dueTime, TimeSpan interval)
        {
            base.ScheduleAtFixedRate(task, dueTime, interval);
        }        
       
    }
}