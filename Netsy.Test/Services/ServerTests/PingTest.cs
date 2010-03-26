//-----------------------------------------------------------------------
// <copyright file="PingTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services.Server
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// Test the ping function on the server service
    /// </summary>
    [TestClass]
    public class PingTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void PingApiKeyMissingTest()
        {
            // ARRANGE
            IServerService serverService = ServiceCreationHelper.MakeServerService(string.Empty);
            ResultEventArgs<PingResult> result = null;
            serverService.PingCompleted += (s, e) => result = e;

            // ACT
            serverService.Ping();

            // check the data
            TestHelpers.CheckResultFailure(result);
        }
    }
}
