//-----------------------------------------------------------------------
// <copyright file="GenericEventArgs.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Helpers
{
    using System;

    /// <summary>
    /// Generic event arguments that wrap a single object
    /// </summary>
    /// <typeparam name="T">The type wrapped by these event args</typeparam>
    public class EventArgs<T> : EventArgs
    {
        /// <summary>
        /// stores the wrapped value
        /// </summary>
        private readonly T value;

        /// <summary>
        /// Initializes a new instance of the EventArgs class
        /// </summary>
        /// <param name="value">the wrapped value</param>
        public EventArgs(T value)
        {
            this.value = value;
        }

        /// <summary>
        /// Gets the wrapped value
        /// </summary>
        public T Value
        {
            get
            {
                return this.value;
            }
        }
    }
}
