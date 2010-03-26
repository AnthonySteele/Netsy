//-----------------------------------------------------------------------
// <copyright file="GetTopCategoriesTest.cs" company="AFS">
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
            ITagCategoryService tagCategoryService = ServiceCreationHelper.MakeTagCategoryService(string.Empty);
            ResultEventArgs<StringResults> result = null;
            tagCategoryService.GetTopCategoriesCompleted += (s, e) => result = e;

            // ACT
            tagCategoryService.GetTopCategories();

            // check the data
            TestHelpers.CheckResultFailure(result);
        }
    }
}
