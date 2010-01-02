//-----------------------------------------------------------------------
// <copyright file="DispatchedServerService.cs" company="AFS">
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
    /// Server service wrapped to use a dispatcher 
    /// To put the results back on the Dispatcher's thread
    /// </summary>
    public class DispatchedServerService : DispatchedService, IServerService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly IServerService wrappedService;

        /// <summary>
        /// Initializes a new instance of the DispatchedServerService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedServerService(IServerService wrappedService, Dispatcher dispatcher) 
            : base(dispatcher)
        {
            this.wrappedService = wrappedService;

            this.wrappedService.GetMethodTableCompleted += (s, e) => this.DispatchEvent(this.GetMethodTableCompleted, s, e);
            this.wrappedService.GetServerEpochCompleted += (s, e) => this.DispatchEvent(this.GetServerEpochCompleted, s, e);
            this.wrappedService.PingCompleted += (s, e) => this.DispatchEvent(this.PingCompleted, s, e);
        }

        /// <summary>
        /// PingResult completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<PingResult>> PingCompleted;

        /// <summary>
        /// GetServerEpoch completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<ServerEpoch>> GetServerEpochCompleted;

        /// <summary>
        /// GetMethodTable completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<MethodTable>> GetMethodTableCompleted;

        /// <summary>
        /// Check that the server is alive.
        /// </summary>
        /// <returns>the async state of the request</returns>
        public IAsyncResult Ping()
        {
            return this.wrappedService.Ping();
        }

        /// <summary>
        /// Get server time, in epoch seconds notation.
        /// </summary>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetServerEpoch()
        {
            return this.wrappedService.GetServerEpoch();
        }

        /// <summary>
        /// Get a list of all methods available.
        /// </summary>
        /// <returns>the async state of the request</returns>
        public IAsyncResult GetMethodTable()
        {
            return this.wrappedService.GetMethodTable();
        }
    }
}
