//-----------------------------------------------------------------------
// <copyright file="TagCategoryService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Core
{
    using System;

    using Helpers;

    using Netsy.DataModel;
    using Netsy.Interfaces;

    /// <summary>
    /// Implementation of the Feedback service
    /// </summary>
    public class TagCategoryService : ITagCategoryService
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// Initializes a new instance of the TagCategoryService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public TagCategoryService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }

        #region ITagCategoryService Members

        /// <summary>
        /// GetTopCategories completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<string>> GetTopCategoriesCompleted;

        /// <summary>
        /// GetChildCategories completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<string>> GetChildCategoriesCompleted;

        /// <summary>
        /// GetTopTags completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<string>> GetTopTagsCompleted;

        /// <summary>
        /// GetChildTags completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<string>> GetChildTagsCompleted;

        /// <summary>
        /// Get the list of current top level categories.
        /// </summary>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetTopCategories()
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetTopCategoriesCompleted, this.etsyContext))
            {
                return null;
            } 
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the child categories of a category.
        /// </summary>
        /// <param name="category">the parent category</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetChildCategories(string category)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetChildCategoriesCompleted, this.etsyContext))
            {
                return null;
            }
            
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the list of current top level tags.
        /// </summary>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetTopTags()
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetTopTagsCompleted, this.etsyContext))
            {
                return null;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Get the child tags of a tag.
        /// </summary>
        /// <param name="tag">the parent tag</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetChildTags(string tag)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetChildTagsCompleted, this.etsyContext))
            {
                return null;
            }

            throw new NotImplementedException();
        }

        #endregion
    }
}
