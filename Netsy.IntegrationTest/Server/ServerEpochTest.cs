//-----------------------------------------------------------------------
// <copyright file="ServerEpochTest.cs" company="AFS">
//  This source code is part ServerEpochTest Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Server
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;
    using Netsy.Test;

    /// <summary>
    /// Test the ping funcion on the server service
    /// </summary>
    [TestClass]
    public class ServerEpochTest
    {
        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetServerEpochApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<ServerEpoch> result = null;
                IServerService etsyServer = new ServerService(new EtsyContext("InvalidKey"));
                etsyServer.GetServerEpochCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyServer.GetServerEpoch();
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetServerEpochCallTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<ServerEpoch> result = null;
                IServerService etsyServer = new ServerService(new EtsyContext(NetsyData.EtsyApiKey));
                etsyServer.GetServerEpochCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyServer.GetServerEpoch();
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsTrue(result.ResultStatus.Success);
                Assert.IsNotNull(result.ResultValue);
                Assert.AreEqual(1, result.ResultValue.Count);
                Assert.AreEqual(1, result.ResultValue.Results.Length);
                Assert.IsTrue(result.ResultValue.Results[0] > 0);
                Assert.IsNull(result.ResultValue.Params);
            }
        }
    }
}
