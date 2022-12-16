namespace DentalClinic.Windows
{

    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    /// <summary>
    /// 
    /// </summary>
    public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        #region Fields


        #endregion // Fields

        #region Properties

        public AggregateException Exception => Task.Exception;

        public Exception InnerException => Exception?.InnerException;

        public bool IsCompleted => Task.IsCompleted;

        public bool IsNotCompleted => !Task.IsCompleted;

        public bool IsSuccessfullyCompleted => Task.Status == TaskStatus.RanToCompletion;

        public bool IsCanceled => Task.IsCanceled;

        public bool IsFaulted => Task.IsFaulted;

        public event PropertyChangedEventHandler PropertyChanged;

        public Task<TResult> Task { get; private set; }

        public TaskStatus Status => Task.Status;

        public TResult Result => (Task.Status == TaskStatus.RanToCompletion) ? Task.Result : default(TResult);

        public string ErrorMessage => InnerException?.Message;

        #endregion // Properties

        #region Constructors

        public NotifyTaskCompletion(Task<TResult> task)
        {
            Task = task;

            if (!task.IsCompleted)
            {
                var _ = WatchTaskAsync(task);
            }
        }

        #endregion // Constructors

        #region Methods



        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch
            {

            }

            var propertyChanged = PropertyChanged;

            if (propertyChanged is null)
                return;

            propertyChanged(this, new PropertyChangedEventArgs(nameof(Status)));

            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCompleted)));

            propertyChanged(this, new PropertyChangedEventArgs(nameof(IsNotCompleted)));

            if (task.IsCanceled)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsCanceled)));
            }
            else if (task.IsFaulted)
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsFaulted)));

                propertyChanged(this, new PropertyChangedEventArgs(nameof(Exception)));

                propertyChanged(this, new PropertyChangedEventArgs(nameof(InnerException)));

                propertyChanged(this, new PropertyChangedEventArgs(nameof(ErrorMessage)));
            }
            else
            {
                propertyChanged(this, new PropertyChangedEventArgs(nameof(IsSuccessfullyCompleted)));

                propertyChanged(this, new PropertyChangedEventArgs(nameof(Result)));
            }
        }

        #endregion // Methods
    }
}
