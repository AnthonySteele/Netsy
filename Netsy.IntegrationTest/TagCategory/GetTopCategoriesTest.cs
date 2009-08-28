﻿//-----------------------------------------------------------------------
// <copyright file="GetTopCategoriesTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.IntegrationTest.TagCategory
{
    using System.Net;
    using System.Threading;

    using Netsy.Core;
    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test the GetTopCategories API function
    /// </summary>
    [TestClass]
    public class GetTopCategoriesTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetTopCategoriesMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<StringResults> result = null;
            ITagCategoryService tagCategoryService = new TagCategoryService(new EtsyContext(string.Empty));
            tagCategoryService.GetTopCategoriesCompleted += (s, e) => result = e;

            // ACT
            tagCategoryService.GetTopCategories();

            // check the data
            NetsyData.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetTopCategoriesApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<StringResults> result = null;
                ITagCategoryService tagCategoryService = new TagCategoryService(new EtsyContext("InvalidKey"));
                tagCategoryService.GetTopCategoriesCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                tagCategoryService.GetTopCategories();
                bool signalled = waitEvent.WaitOne(NetsyData.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data - should fail
                Assert.IsNotNull(result);
                Assert.IsNotNull(result.ResultStatus);
                Assert.IsFalse(result.ResultStatus.Success);
                Assert.AreEqual(WebExceptionStatus.ProtocolError, result.ResultStatus.WebStatus);
            }
        }
    }
}