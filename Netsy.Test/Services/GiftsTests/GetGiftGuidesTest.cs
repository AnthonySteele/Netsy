//-----------------------------------------------------------------------
// <copyright file="GetGiftGuidesTest.cs" company="AFS">
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
            IGiftService giftService = ServiceCreationHelper.MakeGiftService(string.Empty);
            ResultEventArgs<Listings> result = null;
            giftService.GetGiftGuidesCompleted += (s, e) => result = e;

            // ACT
            giftService.GetGiftGuides();

            // check the data
            TestHelpers.CheckResultFailure(result, Constants.EmptyApiKeyErrorMessage);
        }
    }
}
