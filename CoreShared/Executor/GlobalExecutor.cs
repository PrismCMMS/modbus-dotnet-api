using Com.DaacoWorks.Protocol.Logger;
using Com.DaacoWorks.Protocol.Model;
using System;
using System.IO;
using System.Threading;

//we are here
namespace Com.DaacoWorks.Protocol.Executor
{
    /// <summary>
    /// Enumerator to hold single threaded executor service for request and response.
    /// </summary>
    internal class GlobalExecutor
    {

        private static Lazy<GlobalExecutor> singleton = new Lazy<GlobalExecutor>(() => new GlobalExecutor());
        private RequestExecutor requestExecutor = new GlobalRequestExecutor(1);
        private ResponseExecutor responseExecutor = new GlobalResponseExecutor(1);
        //AsynchronousChannelGroup channelGroup = null;

        private GlobalExecutor()
        {
            //TODO: replace AsynchronousChannelGroup properly
            //try
            //{
            //    channelGroup = AsynchronousChannelGroup.withThreadPool(Executors.newFixedThreadPool(1));
#if TRIAL
        requestExecutor.Schedule(new SqhzkTask(), new TimeSpan(4, 0, 0), new TimeSpan(4, 0, 0));
#endif
            //}
            //catch (IOException e)
            //{
            //    e.printStackTrace();
            //}
        }

        public static GlobalExecutor Instance
        {
            get
            {
                return singleton.Value;
            }
        }

        /// <summary>
        /// Gets the request executor.
        /// </summary>
        /// <returns></returns>
        public RequestExecutor GetRequestExecutor()
        {
            return requestExecutor;
        }

        /// <summary>
        /// Gets the response executor.
        /// </summary>
        /// <returns></returns>
        public ResponseExecutor GetResponseExecutor()
        {
            return responseExecutor;
        }

    }

    internal class GlobalRequestExecutor : RequestExecutor
    {

        internal GlobalRequestExecutor(int threads) : base(threads)
        {

        }

    }


    internal class GlobalResponseExecutor : ResponseExecutor
    {

        internal GlobalResponseExecutor(int threads) : base(threads)
        {

        }

    }

#if TRIAL
    internal class SqhzkTask : IExecutorTask
    {
        char[] messChars = new char[] {'S','h','u','t','t','i','n','g',' ','d','o','w','n',' ','t','h','e',' ','M','o','d','b','u','s',
            ' ','S','t','a','c','k',' ','a','f','t','e','r',' ','f','o','u','r',' ','h','o','u','r','s',' ','o','f',' ','r','u','n'};

               
        public CancellationToken CancellationToken
        {
            get
            {
                return CancellationToken.None;
            }
        }

        public void Run()
        {
            LoggerFactory.GetLogger(typeof(GlobalExecutor).FullName).Info(new string(messChars));
            var reqEx = (RequestExecutor)ExecutorFactory.GetGlobalRequestExecutor();
            reqEx.Shutdown();

            var resEx = (ResponseExecutor)ExecutorFactory.GetGlobalResponseExecutor();
            resEx.Shutdown();
        }
        
    }
#endif

}