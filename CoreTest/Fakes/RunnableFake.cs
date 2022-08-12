using Com.DaacoWorks.Protocol.Executor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace CoreTest.Fakes
{
    public class RunnableFake : IRunnable
    {
        int expectedResult;
        private static int order = 0;

        public RunnableFake(CancellationToken token, int expectedResult = 10)
        {
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
            Order = ++order;
            Result = expectedResult;
        }

        public int Result
        {
            get;
            private set;
        }

        public int Order
        {
            get;
            private set;
        }
    }
}
