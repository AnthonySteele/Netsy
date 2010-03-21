//-----------------------------------------------------------------------
// <copyright file="GetChildTagsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.IntegrationTest.TagCategory
{
    using System.Net;
    using System.Threading;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Services;
    using Netsy.Test;

    /// <summary>
    /// Test the GetChildTags API function
    /// </summary>
    [TestClass]
    public class GetChildTagsTest
    {
        /// <summary>
        /// Test missing API key
        /// </summary>
        [TestMethod]
        public void GetChildTagsMissingApiKeyTest()
        {
            // ARRANGE
            ResultEventArgs<StringResults> result = null;
            ITagCategoryService tagCategoryService = new TagCategoryService(new EtsyContext(string.Empty));
            tagCategoryService.GetChildTagsCompleted += (s, e) => result = e;

            // ACT
            tagCategoryService.GetChildTags("accessories");

            // check the data
            TestHelpers.CheckResultFailure(result);
        }

        /// <summary>
        /// Test invalid API key
        /// </summary>
        [TestMethod]
        public void GetChildTagsApiKeyInvalidTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<StringResults> result = null;
                ITagCategoryService tagCategoryService = new TagCategoryService(new EtsyContext("InvalidKey"));
                tagCategoryService.GetChildTagsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                tagCategoryService.GetChildTags("accessories");
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

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

        /// <summary>
        /// Test retrieval
        /// </summary>
        [TestMethod]
        public void GetChildTagsRetrieveTest()
        {
            // ARRANGE
            using (AutoResetEvent waitEvent = new AutoResetEvent(false))
            {
                ResultEventArgs<StringResults> result = null;
                ITagCategoryService tagCategoryService = new TagCategoryService(new EtsyContext(NetsyData.EtsyApiKey));
                tagCategoryService.GetChildTagsCompleted += (s, e) =>
                {
                    result = e;
                    waitEvent.Set();
                };

                // ACT
                tagCategoryService.GetChildTags("accessories");
                bool signalled = waitEvent.WaitOne(Constants.WaitTimeout);

                // ASSERT
                // check that the event was fired, did not time out
                Assert.IsTrue(signalled, "Not signalled");

                // check the data
                TestHelpers.CheckResultSuccess(result);

                Assert.IsNotNull(result.ResultValue.Results);
                Assert.IsTrue(result.ResultStatus.Success);
                Assert.IsTrue(result.ResultValue.Count > 0);
                Assert.AreEqual(result.ResultValue.Count, result.ResultValue.Results.Length);
            }
        }
    }
}
