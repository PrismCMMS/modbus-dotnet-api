using Com.DaacoWorks.Protocol.Exception;
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
    public class ThreadPoolExecutorTest
    {
        [TestMethod]
        [TestCategory("Core\\ThreadPoolExecutor")]
        [Description("Constructor hould throw exception when number of concurrent threads is less than zero")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ShouldThrowException_WhenNumberOfThreadsLessThanZero()
        {
            new ThreadPoolExecutor(-1);
        }

        [TestMethod]
        [TestCategory("Core\\ThreadPoolExecutor")]
        [Description("Constructor should throw exception when number of concurrent threads is Zero")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ShouldThrowException_WhenNumberOfThreadsLessEqualsZero()
        {
            new ThreadPoolExecutor(0);
        }

        [TestMethod]
        [TestCategory("Core\\ThreadPoolExecutor")]
        [Description("Constructor should have set MaximumConcurrencyLevel as 2 when number of concurrent threads is Zero")]
        public void Constructor_ShouldHave_MaximumConcurrencyLevelAs2_WhenNumberOfThreadsEqualsOne()
        {
            var executor = new ThreadPoolExecutor_Accessor(1);

            Assert.IsNotNull(executor.ThreadsExited);
            Assert.IsNotNull(executor.TaskFactory);
            Assert.IsNotNull(executor.Queue);
            Assert.IsNotNull(executor.Pool);
            Assert.AreEqual(2, executor.ActualExecutor.MaximumConcurrencyLevel);
            Assert.IsFalse(executor.ActualExecutor.IsShutdown);
            executor.ActualExecutor.Shutdown();
        }

        [TestMethod]
        [TestCategory("Core\\ThreadPoolExecutor")]
        [Description("Constructor should have set MaximumConcurrencyLevel as 3 when number of concurrent threads is 2")]
        public void Constructor_ShouldHave_MaximumConcurrencyLevelAs3_WhenNumberOfThreadsEqualsTwo()
        {
            var executor = new ThreadPoolExecutor_Accessor(2);

            Assert.IsNotNull(executor.ThreadsExited);
            Assert.IsNotNull(executor.TaskFactory);
            Assert.IsNotNull(executor.Queue);
            Assert.IsNotNull(executor.Pool);
            Assert.AreEqual(3, executor.ActualExecutor.MaximumConcurrencyLevel);
            Assert.IsFalse(executor.ActualExecutor.IsShutdown);
            executor.ActualExecutor.Shutdown();
        }

        [TestMethod]
        [TestCategory("Core\\ThreadPoolExecutor")]
        [Description("Submit should run task and generate expected result")]
        [ExpectedException(typeof(ProtocolException))]
        public void Submit_ShouldThrowException_When_CalledAfterShutdown()
        {
            var executor = new ThreadPoolExecutor(1);
            var runnable = new RunnableFake(CancellationToken.None);

            executor.Shutdown();
            var task = executor.Submit(runnable);           
            
        }

        [TestMethod]
        [TestCategory("Core\\ThreadPoolExecutor")]
        [Description("Submit should run task and generate expected result")]
        public void Submit_Should_RunTaskAndGiveResult()
        {
            var executor = new ThreadPoolExecutor(1);
            var runnable = new RunnableFake(CancellationToken.None);

            var task = executor.Submit(runnable);
            task.Wait();

            Assert.AreEqual(10, runnable.Result);
            executor.Shutdown();
        }

        [TestMethod]
        [TestCategory("Core\\ThreadPoolExecutor")]
        [Description("Submit when called multiple times should run all tasks and generate expected results")]
        public void Submit_WhenCalledMultipleTimes_Should_RunTasksAndGiveResults()
        {
            var executor = new ThreadPoolExecutor(1);
            var runnables = new RunnableFake[10];
            for (var i=0;i<10;i++)
            {
                runnables[i] = new RunnableFake(CancellationToken.None, i + 10);
            }

            var tasks = new Task[10];
            for (var i = 0; i < 10; i++)
            {
                tasks[i] = executor.Submit(runnables[i]);
            }
            Task.WaitAll(tasks);

            
            for (var i = 0; i < 10; i++)
            {                
                Assert.AreEqual(i+10, runnables[i].Result);
            }
            executor.Shutdown();
        }

        [TestMethod]
        [TestCategory("Core\\ThreadPoolExecutor")]
        [Description("Submit when called multiple times should run all tasks in same order of submission")]
        public void Submit_WhenCalledMultipleTimes_Should_RunTasksInOrderOfSubmission()
        {
            var executor = new ThreadPoolExecutor(1);
            var runnables = new RunnableFake[10];
            for (var i = 0; i < 10; i++)
            {
                runnables[i] = new RunnableFake(CancellationToken.None, i + 10);
            }

            var tasks = new Task[10];
            for (var i = 0; i < 10; i++)
            {
                tasks[i] = executor.Submit(runnables[i]);
            }
            Task.WaitAll(tasks);
            
            for (var i = 1; i < 10; i++)
            {
                Assert.IsTrue(runnables[i - 1].Order < runnables[i].Order);                
            }
            executor.Shutdown();
        }

        [TestMethod]
        [TestCategory("Core\\ThreadPoolExecutor")]
        [Description("Shutdown should stop executor and cleanup all resourced used")]
        public void Shutdown_Should_StopExecutorAndCleanup()
        {
            var executor = new ThreadPoolExecutor_Accessor(1);

            executor.ActualExecutor.Shutdown();

            Assert.IsFalse(executor.Pool[0].IsAlive);
            Assert.IsNull(executor.ThreadsExited);            
            Assert.IsNull(executor.Queue);
            Assert.IsTrue(executor.ActualExecutor.IsShutdown);
            
        }

        private class ThreadPoolExecutor_Accessor
        {
            private ThreadPoolExecutor executor;

            public ThreadPoolExecutor_Accessor(int threads)
            {
                executor = new ThreadPoolExecutor(threads);
            }

            public ThreadPoolExecutor ActualExecutor
            {
                get
                {
                    return executor;
                }
            }

            public CountdownEvent ThreadsExited
            {
                get
                {
                    return (CountdownEvent)executor.GetPrivateFieldValue("threadsExited");
                }
            }

            public Thread[] Pool
            {
                get
                {
                    return (Thread[])executor.GetPrivateFieldValue("pool");
                }
            }

            public TaskFactory TaskFactory
            {
                get
                {
                    return (TaskFactory)executor.GetPrivateFieldValue("taskFactory");
                }
            }

            public BlockingCollection<Task> Queue
            {
                get
                {
                    return (BlockingCollection<Task>)executor.GetPrivateFieldValue("queue");
                }
            }
        }
    }
}
