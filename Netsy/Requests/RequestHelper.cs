//-----------------------------------------------------------------------
// <copyright file="RequestHelper.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Requests
{
    using System;

    using Netsy.DataModel;
    using Netsy.Helpers;

    /// <summary>
    /// Helper methods for services
    /// </summary>
    public static class RequestHelper
    {
        /// <summary>
        /// Test that the api is a sate suitable to make calls
        /// </summary>
        /// <typeparam name="T">the type of data returned</typeparam>
        /// <param name="sender">the event sender</param>
        /// <param name="errorEvent">the error event to send if it's not in a good state</param>
        /// <param name="etsyContext">the context data to inspect</param>
        /// <returns>true if everyting is ok, false if there is an error</returns>
        public static bool TestCallPrerequisites<T>(object sender, EventHandler<ResultEventArgs<T>> errorEvent, EtsyContext etsyContext)
        {
            if (etsyContext == null)
            {
                ResultEventArgs<T> errorResult = new ResultEventArgs<T>(default(T), new ResultStatus("Null Api key", null));
                TestSendEvent(errorEvent, sender, errorResult);
                return false;                
            }

            if (string.IsNullOrEmpty(etsyContext.ApiKey))
            {
                ResultEventArgs<T> errorResult = new ResultEventArgs<T>(default(T), new ResultStatus("Empty Api key", null));
                TestSendEvent(errorEvent, sender, errorResult);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Send an event if any handler is attached
        /// </summary>
        /// <typeparam name="T">the type of data to send</typeparam>
        /// <param name="eventHandler">the event handler to fire</param>
        /// <param name="sender">the event sender</param>
        /// <param name="result">the event result</param>
        public static void TestSendEvent<T>(EventHandler<ResultEventArgs<T>> eventHandler, object sender, ResultEventArgs<T> result)
        {
            if (eventHandler != null)
            {
                eventHandler(sender, result);
            }
        }
    }
}
