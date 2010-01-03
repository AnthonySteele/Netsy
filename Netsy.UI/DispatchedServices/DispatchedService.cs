//-----------------------------------------------------------------------
// <copyright file="DispatchedService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.DispatchedServices
{
    using System;
    using System.Windows.Threading;

    using Netsy.Helpers;

    /// <summary>
    /// Base clas for dispacthed services
    /// </summary>
    public abstract class DispatchedService
    {
        /// <summary>
        /// The thread dispatcher
        /// </summary>
        private readonly Dispatcher dispatcher;

        /// <summary>
        /// Initializes a new instance of the DispatchedService class
        /// </summary>
        /// <param name="dispatcher">the thread dispatcher</param>
        protected DispatchedService(Dispatcher dispatcher)
        {
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// Gets the thread dispatcher
        /// </summary>
        protected Dispatcher Dispatcher
        {
            get { return this.dispatcher; }
        }

        /// <summary>
        /// Send an event on the dispatcher's thread
        /// </summary>
        /// <typeparam name="T">the event result type</typeparam>
        /// <param name="eventHandler">the event handler</param>
        /// <param name="sender">the event sender</param>
        /// <param name="eventArgs">the event args</param>
        protected void DispatchEvent<T>(EventHandler<ResultEventArgs<T>> eventHandler, object sender, ResultEventArgs<T> eventArgs)
        {
            if (eventHandler != null)
            {
                Action completedSynch = () => eventHandler(sender, eventArgs);
                this.dispatcher.BeginInvoke(completedSynch);
            }
        }
    }
}
