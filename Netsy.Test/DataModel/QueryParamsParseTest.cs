//-----------------------------------------------------------------------
// <copyright file="QueryParamsParseTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.DataModel
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// Test reading params from json data
    /// </summary>
    [TestClass]
    public class QueryParamsParseTest
    {
        /// <summary>
        /// A sample response text
        /// </summary>
        private const string ParamsData = @"{""user_id"":1234,""detail_level"":""low""}";

        /// <summary>
        /// Test parsing simple params data
        /// </summary>
        [TestMethod]
        public void QueryParamsSimpleParseTest()
        {
            QueryParams queryParams = ParamsData.Deserialize<QueryParams>();
            
            Assert.IsNotNull(queryParams);
            Assert.AreEqual(DetailLevel.Low, queryParams.DetailLevelEnum);
            Assert.AreEqual(1234, queryParams.UserId);
        }
    }
}
