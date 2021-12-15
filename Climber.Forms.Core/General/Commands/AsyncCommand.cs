using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Climber.Forms.Core
{
    /// <summary>
    /// <see langword="async"/> command to execute Tasks Thread safe.
    /// From https://johnthiriet.com/mvvm-going-async-with-async-command
    /// </summary>
    public class AsyncCommand : IAsyncCommand
    {
        private bool _isExecuting;
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        #region Properties

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructor

        public AsyncCommand(Func<Task> execute)
            : this(execute, null, null)
        {
        }

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute)
            : this(execute, canExecute, null)
        {
        }

        public AsyncCommand(Func<Task> execute, IErrorHandler errorHandler)
            : this(execute, null, errorHandler)
        {
        }

        public AsyncCommand(Func<Task> execute, Func<bool> canExecute, IErrorHandler errorHandler)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        #endregion

        #region Public

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync()
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute().ConfigureAwait(false);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Private

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().FireAndForgetSafeAsync(_errorHandler);
        }

        #endregion
    }

    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        private bool _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Func<T, bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        #region Properties

        public event EventHandler CanExecuteChanged;

        #endregion

        #region Constructor

        public AsyncCommand(Func<T, Task> execute)
            : this(execute, null, null)
        {
        }

        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute)
            : this(execute, canExecute, null)
        {
        }

        public AsyncCommand(Func<T, Task> execute, IErrorHandler errorHandler)
            : this(execute, null, errorHandler)
        {
        }

        public AsyncCommand(Func<T, Task> execute, Func<T, bool> canExecute, IErrorHandler errorHandler)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        #endregion

        #region Public

        public bool CanExecute(T parameter)
        {
            return !_isExecuting && (_canExecute?.Invoke(parameter) ?? true);
        }

        public async Task ExecuteAsync(T parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    _isExecuting = true;
                    await _execute(parameter).ConfigureAwait(false);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Private

        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync((T)parameter).FireAndForgetSafeAsync(_errorHandler);
        }

        #endregion
    }
}