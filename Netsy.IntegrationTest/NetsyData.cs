//-----------------------------------------------------------------------
// <copyright file="NetsyData.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest
{
    using Helpers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Helpers on integration tests
    /// Input data and output inspection
    /// </summary>
    public static class NetsyData
    {
        /// <summary>
        /// how long to wait before timing out - 100 seconds
        /// </summary>
        public const int WaitTimeout = 100000;

        /// <summary>
        /// The API key to use for testing
        /// </summary>
        public const string EtsyApiKey = "rfc35bh98q3a9hvccfsxe4cc";

        /// <summary>
        /// the user to test on
        /// </summary>
        public const int TestUserId = 7394192;

        /// <summary>
        /// the id of the listing to test on
        /// </summary>
        public const int TestListingId = 27861562;

        /// <summary>
        /// An invalid listing id for testing
        /// </summary>
        public const int TestBadListingId = 1;

        /// <summary>
        /// The gift guide id to test on
        /// </summary>
        public const int TestGiftGuideId = 168;

        /// <summary>
        /// The bad gift guide id to test on
        /// </summary>
        public const int TestBadGiftGuideId = 1;

        /// <summary>
        /// The test category to use
        /// </summary>
        public const string TestCategory = "bags";

        /// <summary>
        /// the name of the user to test on
        /// </summary>
        public const string TestUserName = "ColonialSteele";

        /// <summary>
        /// an invalid user name
        /// </summary>
        public const string TestBadUserName = "ERTZFZ_BadUserName";

        /// <summary>
        /// an invalid user id
        /// </summary>
        public const int TestBadUserId = 1;

        /// <summary>
        /// Check that the result was sucessfull
        /// </summary>
        /// <typeparam name="T">the type of result data</typeparam>
        /// <param name="result">the data to inspect</param>
        public static void CheckResultSuccess<T>(ResultEventArgs<T> result)
        {
            Assert.IsNotNull(result, "Result is null");
            Assert.IsNotNull(result.ResultStatus, "Result Status is null");

            Assert.IsNull(result.ResultStatus.Exception, "Has exception:" + result.ResultStatus.Exception);
            Assert.IsNull(result.ResultStatus.ErrorMessage, "Has error message:" + result.ResultStatus.ErrorMessage);

            Assert.IsTrue(result.ResultStatus.Success, "Call failed");
            Assert.IsNotNull(result.ResultValue, "Result value is null");
        }

        /// <summary>
        /// Check that the result was a failure
        /// </summary>
        /// <typeparam name="T">the type of result data</typeparam>
        /// <param name="result">the data to inspect</param>
        public static void CheckResultFailure<T>(ResultEventArgs<T> result)
        {
            Assert.IsNotNull(result, "Result is null");
            Assert.IsNotNull(result.ResultStatus, "Result Status is null");
            Assert.IsFalse(result.ResultStatus.Success, "Call was expected to fail");
        }
    }
}
