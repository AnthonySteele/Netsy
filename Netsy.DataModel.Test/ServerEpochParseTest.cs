//-----------------------------------------------------------------------
// <copyright file=""PingTest.cs"" company=""AFS"">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.Helpers;

    /// <summary>
    /// Test deserializing the ping response
    /// </summary>
    [TestClass]
    public class ServerEpochParseTest
    {
        /// <summary>
        /// The ping response
        /// </summary>
        private const string ServerEpochData = @"{""count"":1,""results"":[1250112011],""params"":null,""type"":""int""}";

        /// <summary>
        /// Test parsing ping data
        /// </summary>
        [TestMethod]
        public void ServerEpochParseDataTest()
        {
            ServerEpoch serverEpochResult = ServerEpochData.Deserialize<ServerEpoch>();

            Assert.IsNotNull(serverEpochResult);
            Assert.AreEqual(1, serverEpochResult.Count);
            Assert.AreEqual(1, serverEpochResult.Results.Length);
            Assert.IsTrue(serverEpochResult.Results[0] > 0);
            Assert.IsNull(serverEpochResult.Params);
        }
    }
}
