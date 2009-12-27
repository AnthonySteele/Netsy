//-----------------------------------------------------------------------
// <copyright file="GenericCommandBase.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.Commands
{
    using System;
    using System.Windows.Input;

    /// <summary>
    /// Base class for command handlers on a view model that implements ICommand
    /// </summary>
    /// <typeparam name="T">the type of view model</typeparam>
    public abstract class GenericCommandBase<T> : ICommand where T : class 
    {
        #region ICommand Members

        /// <summary>
        /// the event for command executablity changed
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Can the command execute now?
        /// Override for the can execute handler
        /// </summary>
        /// <param name="parameter">the method data</param>
        /// <returns>true if the command can execute</returns>
        public virtual bool CanExecute(object parameter)
        {
            T value = parameter as T;
            if (parameter != null)
            {
                return this.CanExecuteOnValue(value);
            }

            return false;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        /// <param name="parameter">the command data</param>
        public void Execute(object parameter)
        {
            T value = parameter as T;
            if (parameter != null)
            {
                this.ExecuteOnValue(value);
            }
        }

        #endregion

        /// <summary>
        /// Call CanExecute with a ViewModel as parameter
        /// </summary>
        /// <param name="value">the view model</param>
        /// <returns>true if the command can execute</returns>
        public virtual bool CanExecuteOnValue(T value)
        {
            // the default implementation always allows the command to fire
            return true;
        }

        /// <summary>
        /// Execute the command with a ViewModel as parameter
        /// </summary>
        /// <param name="value">the view model</param>
        public abstract void ExecuteOnValue(T value);
    }
}
