namespace Models.Common
{
    public interface IWithEvent<TEventArgs>
        where TEventArgs : EventArgs
    {
        public event EventHandler<TEventArgs>? Event;
    }
}
