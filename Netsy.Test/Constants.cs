//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test
{
    using System.Collections.Generic;
    using Netsy.DataModel;

    /// <summary>
    /// Helpers on tests
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Expected error message when the Api Key is empty
        /// </summary>
        public const string EmptyApiKeyErrorMessage = "Empty API key";

        /// <summary>
        /// Expected error message when the wiggle is too large
        /// </summary>
        public const string WiggleTooLargeErrorMessage = "Wiggle must be in the range 0 to 15";

        /// <summary>
        /// how long to wait before timing out - 100 seconds
        /// </summary>
        public const int WaitTimeout = 100000;

        /// <summary>
        /// The API key to use for testing
        /// </summary>
        public const string DummyEtsyApiKey = "DummyEtsyApiKey";

        /// <summary>
        /// the test data id to pass to services
        /// </summary>
        public const int TestId = 1234;

        /// <summary>
        /// The test name to use
        /// </summary>
        public const string TestName = "SomeName";

        /// <summary>
        /// Get test words
        /// </summary>
        /// <returns>a list of test strings</returns>
        public static IEnumerable<string> TestWords
        {
            get
            {
                return new List<string> { "foo", "bar" };
            }
        }

        /// <summary>
        /// Get a test color
        /// </summary>
        public static RgbColor TestColor
        {
            get
            {
                return new RgbColor("76B3DF");
            }
        }
    }
}
