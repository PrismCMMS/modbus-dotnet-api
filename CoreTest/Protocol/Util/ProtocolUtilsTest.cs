
using Com.DaacoWorks.Protocol.Util;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;


[TestClass]
public class ProtocolUtilsTest {
	
	[TestMethod]
    [TestCategory("Modbus\\ProtocolUtils")]
    public void TestGetHexByteString() {
		byte [] data = new byte[] {(byte) 0xFD,(byte) 0xFC};
		var byteString = ProtocolUtils.GetHexByteString(new MemoryStream(data));
		Assert.IsTrue(byteString.Equals("FDFC", System.StringComparison.InvariantCultureIgnoreCase));
	}

}
