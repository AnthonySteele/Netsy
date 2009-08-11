//-----------------------------------------------------------------------
// <copyright file="PingTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Netsy.Helpers;

    /// <summary>
    /// Test deserializing the ping response
    /// </summary>
    [TestClass]
    public class PingParseTest
    {
        /// <summary>
        /// The ping response
        /// </summary>
        private const string PingData = @"{""count"":1,""results"":[""pong""],""params"":null,""type"":""string""}";
    
        /// <summary>
        /// Test parsing ping data
        /// </summary>
        [TestMethod]
        public void PingParseDataTest()
        {
            PingResult pingResult = PingData.Deserialize<PingResult>();

            Assert.IsNotNull(pingResult);
            Assert.AreEqual(1, pingResult.Count);
            Assert.AreEqual(1, pingResult.Results.Length);
            Assert.AreEqual("pong", pingResult.Results[0]);
            Assert.IsNull(pingResult.Params);
        }
    }
}
