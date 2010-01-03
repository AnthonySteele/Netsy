//-----------------------------------------------------------------------
// <copyright file="Helpers.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI
{
    using System.Windows;

    /// <summary>
    /// Static helper methods
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Turns a bool to a visibility enum, of Visible or Collapsed
        /// </summary>
        /// <param name="value">the value to convert to visiblity</param>
        /// <returns>a visibility</returns>
        public static Visibility ToVisibility(this bool value)
        {
            return value ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
