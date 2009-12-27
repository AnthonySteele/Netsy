//-----------------------------------------------------------------------
// <copyright file="GetListingDetailsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.Listings
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;

    /// <summary>
    /// Test the GetListingDetails function on the listings service
    /// </summary>
    [TestClass]
    public class GetListingDetailsTest
    {
        /// <summary>
        /// the listing id to use
        /// </summary>
        private readonly int testListingId;

        /// <summary>
        /// Initializes a new instance of the GetListingDetailsTest class
        /// </summary>
        public GetListingDetailsTest()
        {
            this.testListingId = TestIdHelper.RetrieveTestListingId();            
        }

        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetListingDetailsApiKeyMissingTest()
        {
            // ARRANGE
            ResultEventArgs<Listings> result = null;
            IListingsService listingsService = new ListingsService(new EtsyContext(string.Empty));
            listingsService.GetListingDetailsCompleted += (s, e) => result = e;

            // ACT
            listingsService.GetListingDetails(this.testListingId, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetListingDetailApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext("InvalidKey"));
                listingsService.GetListingDetailsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                listingsService.GetListingDetails(this.testListingId, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }

        /// <summary>
        /// Test success response
        /// </summary>
        [TestMethod]
        public void GetListingDetailsCallTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingDetailsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                listingsService.GetListingDetails(this.testListingId, DetailLevel.Low);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.AreEqual(1, result.ResultValue.Count);
                Assert.AreEqual(1, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }

        /// <summary>
        /// Test retrieving listing details, all detail levels
        /// </summary>
        [TestMethod]
        public void GetListingDetailsAllDetailLevelsTest()
        {
            this.TestGetListingDetails(DetailLevel.Low);
            this.TestGetListingDetails(DetailLevel.Medium);
            this.TestGetListingDetails(DetailLevel.High);
        }

        /// <summary>
        /// Test retrieving listing details at the given detail level
        /// </summary>
        /// <param name="detailLevel">the given detail level</param>
        private void TestGetListingDetails(DetailLevel detailLevel)
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<Listings> result = null;
                IListingsService listingsService = new ListingsService(new EtsyContext(NetsyData.EtsyApiKey));
                listingsService.GetListingDetailsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                listingsService.GetListingDetails(this.testListingId, detailLevel);
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                Assert.IsNotNull(result);
                TestHelpers.CheckResultSuccess(result);

                Assert.AreEqual(1, result.ResultValue.Count);
                Assert.AreEqual(1, result.ResultValue.Results.Length);
                Assert.IsNotNull(result.ResultValue.Params);
            }
        }
    }
}
