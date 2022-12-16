namespace DentalClinic.Windows
{

    using System;
    using System.Windows.Input;
    /// <summary>
    /// <see cref="ICommand"/> implementation that wraps as <see cref="Action"/> delegate
    /// </summary>
    public class ActionCommand : ICommand
    {
        #region Fields

        private readonly Action<object> action;
        private readonly Predicate<object> predicate;
        public event EventHandler CanExecuteChanged;        

        #endregion // Fields

        #region Properties



        #endregion // Properties

        #region Constructors

        public ActionCommand(Action<object> action) : this(action, null)
        {

        }

        public ActionCommand(Action<object> action, Predicate<object> predicate)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action", "You must specify an Action<T>");
            }

            this.action = action;
            this.predicate = predicate;
        }

        #endregion // Constructors

        #region Methods

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

        #endregion // Methods
    }
}
