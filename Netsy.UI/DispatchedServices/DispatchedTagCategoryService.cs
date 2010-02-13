//-----------------------------------------------------------------------
// <copyright file="DispatchedTagCategoryService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.DispatchedServices
{
    using System;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// TagCategory service wrapped to use a dispatcher 
    /// To put the results back on the Dispatcher's thread
    /// </summary>
    public class DispatchedTagCategoryService : DispatchedService, ITagCategoryService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly ITagCategoryService wrappedService;

        /// <summary>
        /// Initializes a new instance of the DispatchedTagCategoryService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedTagCategoryService(ITagCategoryService wrappedService, Dispatcher dispatcher) : base(dispatcher)
        {
            if (wrappedService == null)
            {
                throw new ArgumentNullException("wrappedService");
            }

            this.wrappedService = wrappedService;

            this.wrappedService.GetTopCategoriesCompleted += (s, e) => this.DispatchEvent(this.GetTopCategoriesCompleted, s, e);
            this.wrappedService.GetChildCategoriesCompleted += (s, e) => this.DispatchEvent(this.GetChildCategoriesCompleted, s, e);
            this.wrappedService.GetTopTagsCompleted += (s, e) => this.DispatchEvent(this.GetTopTagsCompleted, s, e);
            this.wrappedService.GetChildTagsCompleted += (s, e) => this.DispatchEvent(this.GetChildTagsCompleted, s, e);
        }

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
            return this.wrappedService.GetTopCategories();
        }

        /// <summary>
        /// Get the child categories of a category.
        /// </summary>
        /// <param name="category">the parent category</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetChildCategories(string category)
        {
            return this.wrappedService.GetChildCategories(category);
        }

        /// <summary>
        /// Get the list of current top level tags.
        /// </summary>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetTopTags()
        {
            return this.wrappedService.GetTopTags();
        }

        /// <summary>
        /// Get the child tags of a tag.
        /// </summary>
        /// <param name="tag">the parent tag</param>
        /// <returns>The Async state of the request</returns>
        public IAsyncResult GetChildTags(string tag)
        {
            return this.wrappedService.GetChildTags(tag);
        }
    }
}
