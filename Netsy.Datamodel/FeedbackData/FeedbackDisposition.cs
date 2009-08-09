//-----------------------------------------------------------------------
// <copyright file="FeedbackDisposition.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.DataModel.FeedbackData
{
    /// <summary>
    /// Enumerate the types of feedback
    /// </summary>
    public enum FeedbackDisposition
    {
        /// <summary>
        /// Unknown default value
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// Positive feedback
        /// </summary>
        Positive, 

        /// <summary>
        /// Neutral feedback
        /// </summary>
        Neutral, 

        /// <summary>
        /// Negative feedback
        /// </summary>
        Negative
    }
}
