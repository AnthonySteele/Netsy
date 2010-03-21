//-----------------------------------------------------------------------
// <copyright file="TagCategoryService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Services
{
    using System;

    using Netsy.Helpers;
    using Netsy.DataModel;
    using Netsy.Interfaces;
    using Netsy.Requests;

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
        /// the data retriever
        /// </summary>
        private readonly IDataRetriever dataRetriever;

        /// <summary>
        /// Initializes a new instance of the TagCategoryService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public TagCategoryService(EtsyContext etsyContext)
            : this(etsyContext, new DataRetriever())
        {
        }

        /// <summary>
        /// Initializes a new instance of the TagCategoryService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        /// <param name="dataRetriever">the data retreiver to use</param>
        public TagCategoryService(EtsyContext etsyContext, IDataRetriever dataRetriever)
        {
            this.etsyContext = etsyContext;
            this.dataRetriever = dataRetriever;
        }

        #region ITagCategoryService Members

        /// <summary>
        /// GetTopCategories completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<StringResults>> GetTopCategoriesCompleted;

        /// <summary>
        /// GetChildCategories completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<StringResults>> GetChildCategoriesCompleted;

        /// <summary>
        /// GetTopTags completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<StringResults>> GetTopTagsCompleted;

        /// <summary>
        /// GetChildTags completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<StringResults>> GetChildTagsCompleted;

        /// <summary>
        /// Get the list of current top level categories.
        /// </summary>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetTopCategories()
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetTopCategoriesCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "categories");

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetTopCategoriesCompleted);
        }

        /// <summary>
        /// Get the child categories of a category.
        /// </summary>
        /// <param name="category">the parent category</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetChildCategories(string category)
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetChildCategoriesCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "categories", category).Append("/children");

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetChildCategoriesCompleted);
        }

        /// <summary>
        /// Get the list of current top level tags.
        /// </summary>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetTopTags()
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetTopTagsCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "tags");

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetTopTagsCompleted);
        }

        /// <summary>
        /// Get the child tags of a tag.
        /// </summary>
        /// <param name="tag">the parent tag</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetChildTags(string tag)
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetChildTagsCompleted, this.etsyContext))
            {
                return null;
            }

            EtsyUriBuilder etsyUriBuilder = EtsyUriBuilder.Start(this.etsyContext, "tags", tag).Append("/children");

            return this.dataRetriever.StartRetrieve(etsyUriBuilder.Result(), this.GetChildTagsCompleted);
        }

        #endregion
    }
}
