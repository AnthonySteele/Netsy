//-----------------------------------------------------------------------
// <copyright file="GetUsersByNameTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Test the ping funcion on the server service
    /// </summary>
    [TestClass]
    public class PingTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void PingApiKeyMissingTest()
        {
            ResultEventArgs<PingResult> result = null;
            IServerService stsyServer = new ServerService(new EtsyContext(string.Empty));
            stsyServer.PingCompleted += (s, e) => result = e;

            // ACT
            stsyServer.Ping();

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void PingApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<PingResult> result = null;
                IServerService etsyServer = new ServerService(new EtsyContext("InvalidKey"));
                etsyServer.PingCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyServer.Ping();
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

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
        public void PingCallTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<PingResult> result = null;
                IServerService etsyServer = new ServerService(new EtsyContext(NetsyData.EtsyApiKey));
                etsyServer.PingCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyServer.Ping();
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                NetsyData.CheckResultSuccess(result);

                Assert.AreEqual(1, result.ResultValue.Count);
                Assert.AreEqual(1, result.ResultValue.Results.Length);
                Assert.AreEqual("pong", result.ResultValue.Results[0]);
                Assert.IsNull(result.ResultValue.Params);
            }
        }
    }
}
