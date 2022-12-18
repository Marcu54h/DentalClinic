namespace BlazorClinic.Delegates
{
    public delegate void ContextIsWorkingEventHandler(object source, ContextEventArgs args);

    public class ContextEventArgs : EventArgs
    {
        public bool IsLoading = false;
    }
}
