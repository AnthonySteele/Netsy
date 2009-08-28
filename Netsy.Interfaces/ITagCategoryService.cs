//-----------------------------------------------------------------------
// <copyright file="ITagCategoryService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Interfaces
{
    using System;

    using Netsy.DataModel;

    using Netsy.Helpers;

    /// <summary>
    /// Interface to Tag and Category Commands on the Etsy API
    /// </summary>
    public interface ITagCategoryService
    {
        /// <summary>
        /// GetTopCategories completed event
        /// </summary>
        event EventHandler<ResultEventArgs<StringResults>> GetTopCategoriesCompleted;

        /// <summary>
        /// GetChildCategories completed event
        /// </summary>
        event EventHandler<ResultEventArgs<StringResults>> GetChildCategoriesCompleted;

        /// <summary>
        /// GetTopTags completed event
        /// </summary>
        event EventHandler<ResultEventArgs<StringResults>> GetTopTagsCompleted;

        /// <summary>
        /// GetChildTags completed event
        /// </summary>
        event EventHandler<ResultEventArgs<StringResults>> GetChildTagsCompleted;

        /// <summary>
        /// Get the list of current top level categories.
        /// </summary>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetTopCategories();

        /// <summary>
        /// Get the child categories of a category.
        /// </summary>
        /// <param name="category">the parent category</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetChildCategories(string category);

        /// <summary>
        /// Get the list of current top level tags.
        /// </summary>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetTopTags();

        /// <summary>
        /// Get the child tags of a tag.
        /// </summary>
        /// <param name="tag">the parent tag</param>
        /// <returns>The Async state of the request</returns>
        IAsyncResult GetChildTags(string tag);
    }
}
