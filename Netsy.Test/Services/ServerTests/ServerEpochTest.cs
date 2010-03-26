//-----------------------------------------------------------------------
// <copyright file="ServerEpochTest.cs" company="AFS">
//  This source code is part ServerEpochTest Netsy http://github.com/AnthonySteele/Netsy/
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
    /// Test the ping funcion on the server service
    /// </summary>
    [TestClass]
    public class ServerEpochTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetServerEpochApiKeyMissingTest()
        {
            // ARRANGE
            IServerService serverService = ServiceCreationHelper.MakeServerService(string.Empty);
            ResultEventArgs<ServerEpoch> result = null;
            serverService.GetServerEpochCompleted += (s, e) => result = e;

            // ACT
            serverService.GetServerEpoch();

            // check the data
            TestHelpers.CheckResultFailure(result);
        }
    }
}
