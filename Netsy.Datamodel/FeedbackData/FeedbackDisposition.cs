//-----------------------------------------------------------------------
// <copyright file="FeedbackDisposition.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 


namespace Netsy.DataModel.FeedbackData
{
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
