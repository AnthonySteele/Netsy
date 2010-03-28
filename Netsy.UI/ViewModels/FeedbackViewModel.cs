//-----------------------------------------------------------------------
// <copyright file="FeedbackViewModel.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.UI.ViewModels
{
    using Netsy.DataModel;

    /// <summary>
    /// View model for a gift guide
    /// </summary>
    public class FeedbackViewModel : BaseViewModel
    {
        /// <summary>
        /// the feedback data transfer object
        /// </summary>
        private readonly Feedback feedback;

        /// <summary>
        /// Initializes a new instance of the FeedbackViewModel class
        /// </summary>
        /// <param name="feedback">the feedback Data transfer object</param>
        public FeedbackViewModel(Feedback feedback)
        {
            this.feedback = feedback;
        }

        /// <summary>
        /// Gets the Feedback data transfer object
        /// </summary>
        public Feedback Feedback
        {
            get { return this.feedback; }
        }
    }
}
