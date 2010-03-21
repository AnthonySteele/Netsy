//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test
{
    /// <summary>
    /// Helpers on tests
    /// </summary>
    public static class Constants
    {
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
    }
}
