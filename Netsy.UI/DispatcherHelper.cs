//-----------------------------------------------------------------------
// <copyright file="DispatcherHelper.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Class to get the dispatcher. Differs between WPF and silverlight
    /// </summary>
    public static class DispatcherHelper
    {
        #if (SILVERLIGHT)

        /// <summary>
        /// Gets the current Ui thread dispatcher
        /// </summary>
        public static Dispatcher UiDispatcher
        {
            get
            {
                return Application.Current.RootVisual.Dispatcher;
            }
        }

        /// <summary>
        /// Invoke the method on the Ui thread
        /// </summary>
        /// <param name="method">the method to call</param>
        /// <param name="arg">the method argument</param>
        public static void Invoke(Delegate method, object arg)
        {
            UiDispatcher.BeginInvoke(method, arg);
        }

        #else

        /// <summary>
        /// Gets the current Ui thread dispatcher
        /// </summary>
        public static Dispatcher UIDispatcher
        {
            get
            {
                return Application.Current.Dispatcher;
            }
        }

        /// <summary>
        /// Invoke the method on the Ui thread
        /// </summary>
        /// <param name="method">the method to call</param>
        /// <param name="arg">the method argument</param>
        public static void Invoke(Delegate method, object arg)
        {
            UIDispatcher.Invoke(DispatcherPriority.Normal, method, arg);
        }

        #endif
    }
}
