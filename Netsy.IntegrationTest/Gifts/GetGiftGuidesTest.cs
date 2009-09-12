//-----------------------------------------------------------------------
// <copyright file="GetGiftGuidesTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Gifts
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// Test the GetGiftGuides API Function
    /// </summary>
    [TestClass]
    public class GetGiftGuidesTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetGiftGuidesMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IGiftService giftService = new GiftService(new EtsyContext(string.Empty));
            giftService.GetGiftGuidesCompleted += (s, e) => result = e;

            // ACT
            giftService.GetGiftGuides();

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetGiftGuidesApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IGiftService giftService = new GiftService(new EtsyContext("InvalidKey"));
                giftService.GetGiftGuidesCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                giftService.GetGiftGuides();
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
        /// Test gift guide retrieval
        /// </summary>
        [TestMethod]
        public void GetGiftGuidesGetTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IGiftService giftService = new GiftService(new EtsyContext(NetsyData.EtsyApiKey));
                giftService.GetGiftGuidesCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                giftService.GetGiftGuides();
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                NetsyData.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Results);
                Assert.IsTrue(result.ResultStatus.Success);
                Assert.IsTrue(result.ResultValue.Count > 0);
            }
        }
    }
}
