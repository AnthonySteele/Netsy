//-----------------------------------------------------------------------
// <copyright file="IServerService.cs" company="AFS">
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
    /// Interface to server functions on the Etsy API
    /// </summary>
    public interface IServerService
    {
        /// <summary>
        /// Ping completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Ping>> PingCompleted;

        /// <summary>
        /// GetServerEpoch completed event
        /// </summary>
        event EventHandler<ResultEventArgs<ServerEpoch>> GetServerEpochCompleted;

        /// <summary>
        /// GetMethodTable completed event
        /// </summary>
        event EventHandler<ResultEventArgs<MethodTable>> GetMethodTableCompleted;

        /// <summary>
        /// Check that the server is alive.
        /// </summary>
        /// <returns>the async state of the request</returns>
        IAsyncResult Ping();

        /// <summary>
        /// Get server time, in epoch seconds notation.
        /// </summary>
        /// <returns>the async state of the request</returns>
        IAsyncResult GetServerEpoch();

        /// <summary>
        /// Get a list of all methods available.
        /// </summary>
        /// <returns>the async state of the request</returns>
        IAsyncResult GetMethodTable();
    }
}
