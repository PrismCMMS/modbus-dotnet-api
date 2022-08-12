using Com.DaacoWorks.Protocol.Executor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CoreTest.Fakes
{
    public class ScheduledRunnableFake : IRunnable
    {
        private int expectedResult = 0;
        private int counter = 0;
        private RunnableCallback callback;

        public delegate void RunnableCallback(int result);

        public ScheduledRunnableFake(RunnableCallback callback, CancellationToken token, int expectedResult = 10)
        {
            this.callback = callback;
            this.expectedResult = expectedResult;
            CancellationToken = token;
        }        

        public CancellationToken CancellationToken
        {
            get;
            private set;
        }

        public void Run()
        {
            Result = expectedResult + counter++;
            callback(Result);
        }

        public int Result
        {
            get;
            private set;
        }
    }


}
