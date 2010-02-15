//-----------------------------------------------------------------------
// <copyright file="ServerService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Services
{
    using System;

    using Netsy.Cache;
    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;
    using Netsy.Requests;

    /// <summary>
    /// Implementation of the server service
    /// </summary>
    public class ServerService : IServerService
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
        /// Initializes a new instance of the ServerService class
        /// </summary>
        /// <param name="apiKey">the api key to use</param>
        public ServerService(string apiKey)
            : this(new EtsyContext(apiKey), new DataRetriever())
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServerService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public ServerService(EtsyContext etsyContext)
            : this(etsyContext, new DataRetriever())
        {
        }

        /// <summary>
        /// Initializes a new instance of the ServerService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        /// <param name="dataRetriever">the data retriever to use</param>
        public ServerService(EtsyContext etsyContext, IDataRetriever dataRetriever)
        {
            this.etsyContext = etsyContext;
            this.dataRetriever = dataRetriever;
        }

        #region IServerService Members

        /// <summary>
        /// Event hander for when the PingResult call completes
        /// </summary>
        public event EventHandler<ResultEventArgs<PingResult>> PingCompleted;

        /// <summary>
        /// Event hander for when the GetServerEpoch call completes
        /// </summary>
        public event EventHandler<ResultEventArgs<ServerEpoch>> GetServerEpochCompleted;

        /// <summary>
        /// Event hander for when the GetMethodTable call completes
        /// </summary>
        public event EventHandler<ResultEventArgs<MethodTable>> GetMethodTableCompleted;

        /// <summary>
        /// Check that the server is alive.
        /// </summary>
        /// <returns>The Async state of the call</returns>
        public IAsyncResult Ping()
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.PingCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "server/ping");

            return this.dataRetriever.StartRetrieve(uriBuilder.Result(), this.PingCompleted);
        }

        /// <summary>
        /// Get server time, in epoch seconds notation.
        /// </summary>
        /// <returns>The Async state of the call</returns>
        public IAsyncResult GetServerEpoch()
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetServerEpochCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "server/epoch");

            return this.dataRetriever.StartRetrieve(uriBuilder.Result(), this.GetServerEpochCompleted);
        }

        /// <summary>
        /// Get a list of all methods available.
        /// </summary>
        /// <returns>The Async state of the call</returns>
        public IAsyncResult GetMethodTable()
        {
           if (!RequestHelper.TestCallPrerequisites(this, this.GetMethodTableCompleted, this.etsyContext))
            {
                return null;
            }

           UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext).Append("/");

           return this.dataRetriever.StartRetrieve(uriBuilder.Result(), this.GetMethodTableCompleted);
        }

        #endregion
    }
}
