//-----------------------------------------------------------------------
// <copyright file="GetGiftGuideListingsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services.GiftsTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Test.Services;

    /// <summary>
    /// Test the GetGiftGuideListings API Function
    /// </summary>
    [TestClass]
    public class GetGiftGuideListingsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetGiftGuidesMissingApiKeyTest()
        {
            // ARRANGE
            IGiftService giftService = ServiceCreationHelper.MakeGiftService(string.Empty);
            ResultEventArgs<Listings> result = null;
            giftService.GetGiftGuideListingsCompleted += (s, e) => result = e;

            // ACT
            giftService.GetGiftGuideListings(Constants.TestId, 0, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Empty API key");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetGiftGuidesNegativeOffsetTest()
        {
            // ARRANGE
            IGiftService giftService = ServiceCreationHelper.MakeGiftService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            giftService.GetGiftGuideListingsCompleted += (s, e) => result = e;

            // ACT
            giftService.GetGiftGuideListings(Constants.TestId, -1, 10, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Negative offset of -1");
        }

        /// <summary>
        /// Test a negative offset
        /// </summary>
        [TestMethod]
        public void GetGiftGuidesZeroLimitTest()
        {
            // ARRANGE
            IGiftService giftService = ServiceCreationHelper.MakeGiftService(Constants.DummyEtsyApiKey);
            ResultEventArgs<Listings> result = null;
            giftService.GetGiftGuideListingsCompleted += (s, e) => result = e;

            // ACT
            giftService.GetGiftGuideListings(Constants.TestId, 0, 0, DetailLevel.Low);

            // check the data
            TestHelpers.CheckResultFailure(result, "Bad limit of 0");
        }
    }
}
