//-----------------------------------------------------------------------
// <copyright file="QueryParamsCreateTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;

    /// <summary>
    /// Test creating the query params
    /// </summary>
    [TestClass]
    public class QueryParamsCreateTest
    {
        /// <summary>
        /// Test simple creation of a user
        /// </summary>
        [TestMethod]
        public void QueryParamsSimpleCreateTest()
        {
            QueryParams queryParams = new QueryParams();
            Assert.IsNotNull(queryParams);
        }

        /// <summary>
        /// Test setting the user status to public
        /// </summary>
        [TestMethod]
        public void QueryParamsSetDetailLevelLowTest()
        {
            QueryParams queryParams = new QueryParams();
            queryParams.DetailLevelString = "low";

            Assert.AreEqual(DetailLevel.Low, queryParams.DetailLevelEnum);
        }

        /// <summary>
        /// Test setting the user status to a bad value
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void UserDetailsSetDetailLevelBadValueTest()
        {
            QueryParams queryParams = new QueryParams();
            queryParams.DetailLevelString = "goofy";
        }
    }
}
