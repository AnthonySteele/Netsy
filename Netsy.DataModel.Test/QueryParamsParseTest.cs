//-----------------------------------------------------------------------
// <copyright file="QueryParamsParseTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
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
