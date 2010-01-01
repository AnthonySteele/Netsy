//-----------------------------------------------------------------------
// <copyright file="DispatchedTagCategoryService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.DispatchedServices
{
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// TagCategory service wrapped to use a dispatcher 
    /// To put the results back on the Dispatcher's thread
    /// </summary>
    public class DispatchedTagCategoryService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly ITagCategoryService wrappedService;

        /// <summary>
        /// The thread dispatcher
        /// </summary>
        private readonly Dispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the DispatchedTagCategoryService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedTagCategoryService(ITagCategoryService wrappedService, Dispatcher dispatcher)
        {
            this.wrappedService = wrappedService;
            this.dispatcher = dispatcher;
        }
    }
}
