using System;
using System.Windows.Input;

namespace RestaurantManager.ViewModels
{
    public class DelegateCommand<T> : ICommand
    {
        private readonly Func<T, bool> _canExecuteMethod;
        private readonly Action<T> _excuteMethod;

        #region Constructors

        public DelegateCommand(Action<T> executeMethod)
            : this(executeMethod, null)
        {
        }

        public DelegateCommand(Action<T> executeMethod, Func<T, bool> canExecuteMethod)
        {
            _excuteMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        #endregion Constructors

        #region ICommand Members

        public event EventHandler CanExecuteChanged;

        bool ICommand.CanExecute(object parameter)
        {
            try
            {
                return CanExecute((T)parameter);
            }
            catch { return false; }
        }

        void ICommand.Execute(object parameter)
        {
            Execute((T)parameter);
        }

        #endregion ICommand Members

        #region Public Methods

        public bool CanExecute(T parameter)
        {
            return ((_canExecuteMethod == null) || _canExecuteMethod(parameter));
        }

        public void Execute(T parameter)
        {
            if (_excuteMethod != null)
            {
                _excuteMethod(parameter);
            }
        }

        public void RaiseCanExecuteChanged()
        {
            OnCanExecuteChanged(EventArgs.Empty);
        }

        #endregion Public Methods

        #region Protected Methods

        private void OnCanExecuteChanged(EventArgs e)
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion Protected Methods
    }
}