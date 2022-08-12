using Com.DaacoWorks.Protocol.Exception;
using Com.DaacoWorks.Protocol.Executor;
using CoreTest.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading;
using TestSystem;

namespace CoreTest.Executor
{
    [TestClass]
    public class ScheduledThreadPoolExecutorTest
    {

        [TestMethod]
        [TestCategory("Core\\ScheduledThreadPoolExecutor")]
        [Description("Constructor should initialize ScheduledTasks list")]
        public void Constructor_Should_InitializedScheduledTasks()
        {
            var executor = new ScheduledThreadPoolExecutor_Accessor(1);

            Assert.IsNotNull(executor.ScheduledTasks);
            executor.Shutdown();
        }


        [TestMethod]
        [TestCategory("Core\\ScheduledThreadPoolExecutor")]
        [Description("ScheduleWithFixedDelay should create scheduled task")]
        public void ScheduleWithFixedDelay_Should_CreateScheduledTask()
        {
            var executor = new ScheduledThreadPoolExecutor_Accessor(1);            
            var runnable = new RunnableFake(CancellationToken.None, 5);

            executor.Schedule(runnable, TimeSpan.Zero, new TimeSpan(0, 0, 0, 5));

            Assert.AreEqual(1, executor.ScheduledTasks.Count);

            executor.Schedule(runnable, TimeSpan.Zero, new TimeSpan(0, 0, 0, 5));

            Assert.AreEqual(2, executor.ScheduledTasks.Count);

            executor.Shutdown();
        }

        [TestMethod]
        [TestCategory("Core\\ScheduledThreadPoolExecutor")]
        [Description("ScheduleWithFixedDelay should throw exception when called after shutdown")]
        [ExpectedException(typeof(ProtocolException))]
        public void ScheduleWithFixedDelay_ShouldThrowException_When_CalledAfterShutdown()
        {
            var executor = new ScheduledThreadPoolExecutor_Accessor(1);
            var runnable = new RunnableFake(CancellationToken.None, 5);

            executor.Shutdown();
            executor.Schedule(runnable, TimeSpan.Zero, new TimeSpan(0, 0, 0, 5));

        }

        [TestMethod]
        [TestCategory("Core\\ScheduledThreadPoolExecutor")]
        [Description("Dispose should dispose the scheduled task and cleanup")]       
        public void Dispose_Should_Cleanup()
        {
            var executor = new ScheduledThreadPoolExecutor_Accessor(1);
            var runnable = new RunnableFake(CancellationToken.None, 5);

            executor.Dispose();
            Assert.IsNull(executor.ScheduledTasks);
        }

        private class ScheduledThreadPoolExecutor_Accessor : ScheduledThreadPoolExecutor
        {


            public ScheduledThreadPoolExecutor_Accessor(int threads) : base(threads)
            {

            }

            public ScheduledTask Schedule(IRunnable task, TimeSpan dueTime, TimeSpan interval)
            {
                return ScheduleAtFixedRate(task, dueTime, interval);
            }

            public List<ScheduledTask> ScheduledTasks
            {
                get
                {
                    return (List<ScheduledTask>)this.GetBaseTypePrivateFieldValue("scheduledTasks");
                }
            }


        }
    }
}
