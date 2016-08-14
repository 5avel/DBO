﻿using System;
using System.Windows.Input;

namespace DBO.ViewModel.MVVMLib
{
    class RelayCommand : ICommand
    {
        private readonly Action _handler;
        private bool _isEnabled;

        public RelayCommand(Action handler)
        {
            _handler = handler;
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    _isEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            //return IsEnabled;
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _handler();
        }
    }
   
        public class RelayCommand<T> : ICommand
        {
            #region Fields

            private readonly Action<T> _execute = null;
            private readonly Predicate<T> _canExecute = null;

            #endregion

            #region Constructors

            /// <summary>
            /// Creates a new command that can always execute.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            public RelayCommand(Action<T> execute)
                : this(execute, null)
            {
            }

            /// <summary>
            /// Creates a new command with conditional execution.
            /// </summary>
            /// <param name="execute">The execution logic.</param>
            /// <param name="canExecute">The execution status logic.</param>
            public RelayCommand(Action<T> execute, Predicate<T> canExecute)
            {
                if (execute == null)
                    throw new ArgumentNullException("execute");

                _execute = execute;
                _canExecute = canExecute;
            }

            #endregion

            #region ICommand Members

            public bool CanExecute(object parameter)
            {
                return _canExecute == null ? true : _canExecute((T)parameter);
            }

            public event EventHandler CanExecuteChanged
            {
                add
                {
                    if (_canExecute != null)
                        CommandManager.RequerySuggested += value;
                }
                remove
                {
                    if (_canExecute != null)
                        CommandManager.RequerySuggested -= value;
                }
            }

            public void Execute(object parameter)
            {
                _execute((T)parameter);
            }

            #endregion
        }
 
}
