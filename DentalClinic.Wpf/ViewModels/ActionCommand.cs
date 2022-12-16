namespace DentalClinic.Wpf
{ 
    using System;
    using System.Windows.Input;

    public sealed class ActionCommand : ICommand
    {
        // 'Action' takes 'object' and performs some actions on it.
        // No value returns. 'Readonly' means it will take value only in time
        // of 'ActionCommand' initialization. No further changes will be allowed.
        private readonly Action<object> action;

        // 'Predicate' returns only 'true' or 'false'
        private readonly Predicate<object> predicate;

        public event EventHandler CanExecuteChanged;

        #region Constructors

        public ActionCommand(Action<object> action) : this(action, null)
        {
        }

        public ActionCommand(Action<object> action, Predicate<object> predicate)
        {
            this.action = action ?? throw new ArgumentNullException("action", "You must specify an Action<T>.");
            this.predicate = predicate;
        }

        #endregion // Constructors

        public bool CanExecute(object parameter)
        {
            if (predicate == null)
            {
                return true;
            }
            return predicate(parameter);
        }

        public void Execute(object parameter)
        {
            action(parameter);
        }

        public void Execute()
        {
            Execute(null);
        }
    }
}
