﻿//-----------------------------------------------------------------------
// <copyright file="QueryParamsCreateTest.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.Test
{
    using System;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;

    /// <summary>
    /// Test creating the user details
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