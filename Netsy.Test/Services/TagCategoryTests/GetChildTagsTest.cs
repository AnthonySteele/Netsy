//-----------------------------------------------------------------------
// <copyright file="GetChildTagsTest.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Test.Services.TagCategory
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

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
            ITagCategoryService tagCategoryService = ServiceCreationHelper.MakeTagCategoryService(string.Empty);
            ResultEventArgs<StringResults> result = null;
            tagCategoryService.GetChildTagsCompleted += (s, e) => result = e;

            // ACT
            tagCategoryService.GetChildTags("accessories");

            // check the data
            TestHelpers.CheckResultFailure(result);
        }
    }
}
