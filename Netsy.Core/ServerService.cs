//-----------------------------------------------------------------------
// <copyright file="IServerService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Core
{
    using System;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

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
        /// Initializes a new instance of the ServerService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public ServerService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
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
            if (!ServiceHelper.TestCallPrerequisites(this, this.PingCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "server/ping" + 
                "?api_key=" + this.etsyContext.ApiKey;

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.PingCompleted);
        }

        /// <summary>
        /// Get server time, in epoch seconds notation.
        /// </summary>
        /// <returns>The Async state of the call</returns>
        public IAsyncResult GetServerEpoch()
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetServerEpochCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "server/epoch" +
            "?api_key=" + this.etsyContext.ApiKey;

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetServerEpochCompleted);
        }

        /// <summary>
        /// Get a list of all methods available.
        /// </summary>
        /// <returns>The Async state of the call</returns>
        public IAsyncResult GetMethodTable()
        {
           if (!ServiceHelper.TestCallPrerequisites(this, this.GetMethodTableCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "/" +
                "?api_key=" + this.etsyContext.ApiKey;

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetMethodTableCompleted);
        }

        #endregion
    }
}
