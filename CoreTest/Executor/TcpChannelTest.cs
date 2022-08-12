using Com.DaacoWorks.Protocol.Executor;
using Com.DaacoWorks.Protocol.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoreTest.Executor
{
    [TestClass]
    public class TCPChannelTest
    {
        private object syncObj = new object();

        [TestMethod]
        [TestCategory("Core\\TCPChannel")]
        public void TCPChannel_Should_WriteAndReadData()
        {
            var connectionParams = new SocketParameters("win7-PC", 1601, ConnectionType.TCP);
            var channel = new TCPChannel(connectionParams);

            byte[] dataRead = null;
            lock (syncObj)
            {
                var writeBuffer = new MemoryStream(100);
                var readBuffer = new MemoryStream(100);
                channel.OpenChannel();
                channel.Read(readBuffer, (bytesRead) =>
                {
                    lock (syncObj)
                    {
                        readBuffer.Position = 0;
                        dataRead = new byte[bytesRead];
                        readBuffer.Read(dataRead, 0, bytesRead);
                        Monitor.PulseAll(syncObj);
                    }
                });

                var bytes = new byte[] { 1, 0, 0, 0, 0, 6, 255, 1, 0, 9, 0, 2 };
                writeBuffer.Write(bytes, 0, bytes.Length);


                channel.Write(writeBuffer);
                Monitor.Wait(syncObj);
                channel.CloseChannel();
            }

            Assert.IsNotNull(dataRead);
            Assert.IsTrue(dataRead.Length > 0);
        }
    }
}
