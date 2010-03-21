//-----------------------------------------------------------------------
// <copyright file="ServerServiceTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services
{
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Requests;
    using Netsy.Services;
    using Netsy.Test.Requests;

    [TestClass]
    public class ServerServiceTest
    {
        private const string PingRawResults = @"{""count"":1,""results"":[""pong""],""params"":null,""type"":""string""}";

        [TestMethod]
        public void CreateWithMockRequestTest()
        {
            EtsyContext etsyContext = new EtsyContext(string.Empty);
            IRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(string.Empty);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            IServerService service = new ServerService(etsyContext, dataRetriever);

            Assert.IsNotNull(service);
        }

        [TestMethod]
        public void PingTest()
        {
            EtsyContext etsyContext = new EtsyContext(Constants.DummyEtsyApiKey);
            MockFixedDataRequestGenerator requestGenerator = new MockFixedDataRequestGenerator(PingRawResults);
            DataRetriever dataRetriever = new DataRetriever(new NullDataCache(), requestGenerator);
            IServerService etsyListingsService = new ServerService(etsyContext, dataRetriever);

            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<PingResult> result = null;
                etsyListingsService.PingCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                etsyListingsService.Ping();
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT

                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                TestHelpers.CheckResultSuccess(result);
            }
        }
    }
}
