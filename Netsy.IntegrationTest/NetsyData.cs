//-----------------------------------------------------------------------
// <copyright file="NetsyData.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest
{
    using Helpers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        public static void CheckResultSuccess<T>(ResultEventArgs<T> result)
        {
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ResultStatus);
            Assert.IsTrue(result.ResultStatus.Success, "Call failed");
            Assert.IsNotNull(result.ResultValue);
        }
    }
}
