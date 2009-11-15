//-----------------------------------------------------------------------
// <copyright file="TestHelpers.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest
{
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.Helpers;

    /// <summary>
    /// Helper methods on integration tests
    /// </summary>
    public static class TestHelpers
    {
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

        /// <summary>
        /// A short pause for the Etsy Server to catch up
        /// </summary>
        public static void WaitABit()
        {
            Thread.Sleep(500);
        }
    }
}
