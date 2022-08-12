using Com.DaacoWorks.Protocol.Executor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTest.Executor
{
    [TestClass]
    public class SqhzkTaskTest
    {
        [TestMethod]
        [TestCategory("Core\\SqhzkTask")]
        [Description("SqhzkTask should exist only in Trial or final this should fail")]
        public void SqhzkTask_IsExists()
        {
            Assert.IsNotNull(typeof(GlobalExecutor).Assembly.GetType("Com.DaacoWorks.Protocol.Executor.SqhzkTask"));
        }
    }
}
