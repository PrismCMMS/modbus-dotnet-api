using Com.DaacoWorks.Protocol.Executor;
using CoreTest.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using TestSystem;

namespace CoreTest.Executor
{
    [TestClass]
    public class ScheduledTaskTest
    {
        private ScheduledThreadPoolExecutor executor;

        [TestInitialize]
        public void BeforeEachTest()
        {
            executor = new ScheduledThreadPoolExecutor(1);
        }

        [TestMethod]
        [TestCategory("Core\\ScheduledTaskTest")]
        [Description("Constructor initializes the states")]
        public void Contrucutor_Should_InitializeStates()
        {
            var runnable = new RunnableFake(CancellationToken.None, 5);

            var scheduledTask = new ScheduledTask_Accessor(executor, runnable, TimeSpan.Zero, new TimeSpan(0, 0, 1));

            Assert.IsNotNull(scheduledTask.Executor);
            Assert.IsNotNull(scheduledTask.Tasks);
            Assert.IsFalse(scheduledTask.IsDisposed);
            Assert.IsNotNull(scheduledTask.TimerScheduled);

            scheduledTask.ActualScheduledTask.Dispose();
        }
                

        [TestMethod]
        [TestCategory("Core\\ScheduledTaskTest")]
        [Description("Scheduled Timer should immediately trigger when duetime is zero")]
        public void ScheduledTimer_Should_ImmediatelyTrigger_When_DuetimeIsZero()
        {
            var executor = new ScheduledThreadPoolExecutor(1);
            int actualResult = 0;
            int tasksCount = 0;
            ScheduledTask_Accessor scheduledTask = null;

            var runnable = new ScheduledRunnableFake((result) =>
            {
                actualResult = result;
                tasksCount = scheduledTask.Tasks.Count;
            }, CancellationToken.None, 5);

            scheduledTask = new ScheduledTask_Accessor(executor, runnable, TimeSpan.Zero, new TimeSpan(0, 0, 5));
            Thread.Sleep(100);

            Assert.AreEqual(5, actualResult);
            Assert.AreEqual(1, tasksCount);
            Assert.AreEqual(0, scheduledTask.Tasks.Count);
            executor.Shutdown();
        }

        [TestMethod]
        [TestCategory("Core\\ScheduledTaskTest")]
        [Description("Scheduled Timer should trigger after duetime when it is greater than zero")]
        public void ScheduledTimer_Should_Trigger_AfterDueTime_When_GreaterThanZero()
        {
            var executor = new ScheduledThreadPoolExecutor(1);
            int actualResult = 0;
            int tasksCount = 0;
            ScheduledTask_Accessor scheduledTask = null;

            var runnable = new ScheduledRunnableFake((result) =>
            {
                actualResult = result;
                tasksCount = scheduledTask.Tasks.Count;
            }, CancellationToken.None, 5);

            scheduledTask = new ScheduledTask_Accessor(executor, runnable, new TimeSpan(0,0,1), new TimeSpan(0, 0, 5));
            Thread.Sleep(100);

            Assert.AreEqual(0, actualResult);
            Assert.AreEqual(0, tasksCount);
            Thread.Sleep(1000);

            Assert.AreEqual(5, actualResult);
            Assert.AreEqual(1, tasksCount);
            Assert.AreEqual(0, scheduledTask.Tasks.Count);
            executor.Shutdown();
        }

        [TestMethod]
        [TestCategory("Core\\ScheduledTaskTest")]
        [Description("Scheduled Timer should trigger repeatedly for every time period defined")]
        public void ScheduledTimer_Should_Trigger_Repeatedly_EveryPeriodDefined()
        {
            var executor = new ScheduledThreadPoolExecutor(1);
            int actualResult = 0;
            ScheduledTask_Accessor scheduledTask = null;

            var runnable = new ScheduledRunnableFake((result) =>
            {
                actualResult = result;
            }, CancellationToken.None, 5);

            scheduledTask = new ScheduledTask_Accessor(executor, runnable, TimeSpan.Zero, new TimeSpan(0, 0, 0, 0, 200));
            Thread.Sleep(100);

            Assert.AreEqual(5, actualResult);

            Thread.Sleep(200);
            Assert.AreEqual(6, actualResult);

            Thread.Sleep(200);
            Assert.AreEqual(7, actualResult);

            executor.Shutdown();
            
        }

        [TestMethod]
        [TestCategory("Core\\ScheduledTaskTest")]
        [Description("Dispose should stop timer and cleanup resources")]
        public void Dispose_Should_StopTimer_And_Cleanup()
        {
            var executor = new ScheduledThreadPoolExecutor(1);
            int actualResult = 0;
            ScheduledTask_Accessor scheduledTask = null;

            var runnable = new ScheduledRunnableFake((result) =>
            {
                actualResult = result;
            }, CancellationToken.None, 5);

            scheduledTask = new ScheduledTask_Accessor(executor, runnable, TimeSpan.Zero, new TimeSpan(0, 0, 0, 0, 200));
            Thread.Sleep(100);

            Assert.AreEqual(5, actualResult);

            Thread.Sleep(200);
            Assert.AreEqual(6, actualResult);

            scheduledTask.ActualScheduledTask.Dispose();

            Thread.Sleep(500);
            Assert.AreEqual(6, actualResult);
            Assert.IsNull(scheduledTask.Executor);
            Assert.IsNull(scheduledTask.Tasks);
            Assert.IsTrue(scheduledTask.IsDisposed);
            Assert.IsNull(scheduledTask.TimerScheduled);
        }

        private class ScheduledTask_Accessor
        {
            private ScheduledTask scheduledTask;

            public ScheduledTask_Accessor(ScheduledThreadPoolExecutor executer, IRunnable taskScheduled, TimeSpan dueTime, TimeSpan period)
            {
                scheduledTask = new ScheduledTask(executer, taskScheduled, dueTime, period);

            }

            public ScheduledTask_Accessor(ScheduledTask scheduledTask)
            {
                this.scheduledTask = scheduledTask;
            }

            public ScheduledTask ActualScheduledTask
            {
                get
                {
                    return scheduledTask;
                }
            }

            public ScheduledThreadPoolExecutor Executor
            {
                get
                {
                    return (ScheduledThreadPoolExecutor)scheduledTask.GetPrivateFieldValue("executor");
                }
            }

            public bool IsDisposed
            {
                get
                {
                    return (bool)scheduledTask.GetPrivateFieldValue("isDisposed");
                }
            }

            public ConcurrentDictionary<int, Task> Tasks
            {
                get
                {
                    return (ConcurrentDictionary<int, Task>)scheduledTask.GetPrivateFieldValue("tasks");
                }
            }

            public Timer TimerScheduled
            {
                get
                {
                    return (Timer)scheduledTask.GetPrivateFieldValue("timerScheduled");
                }
            }
        }
    }
}
