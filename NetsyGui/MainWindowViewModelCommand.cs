//-----------------------------------------------------------------------
// <copyright file="MainWindowViewModelCommand.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace NetsyGui
{
    using System;
    using System.Windows.Input;

    using NetsyGui.ViewModels;

    /// <summary>
    /// Base class for command handlers on the main window view model that implements ICommand
    /// </summary>
    public abstract class MainWindowViewModelCommand : ICommand
    {
        #region ICommand Members

        /// <summary>
        /// the event for command executablity changed
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Can the command execute now?
        /// Override for the can execute handler
        /// </summary>
        /// <param name="parameter">the method data</param>
        /// <returns>true if the command can execute</returns>
        public bool CanExecute(object parameter)
        {
            MainWindowViewModel viewModel = parameter as MainWindowViewModel;
            if (parameter != null)
            {
                return this.CanExecuteViewModel(viewModel);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">the command data</param>
        public void Execute(object parameter)
        {
            MainWindowViewModel viewModel = parameter as MainWindowViewModel;
            if (parameter != null)
            {
                this.ExecuteViewModel(viewModel);
            }
        }

        #endregion

        /// <summary>
        /// Fire the CanExecuteChanged event
        /// </summary>
        public void OnCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Call CanExecute with a ViewModel as parameter
        /// </summary>
        /// <param name="viewModel">the view model</param>
        /// <returns>true if the command can execute</returns>
        public virtual bool CanExecuteViewModel(MainWindowViewModel viewModel)
        {
            // the default implementation always allows the command to fire
            return true;
        }

        /// <summary>
        /// Execute the command with a ViewModel as parameter
        /// </summary>
        /// <param name="viewModel">the view model</param>
        public abstract void ExecuteViewModel(MainWindowViewModel viewModel);
    }
}
