namespace HsLib.Common
{
    public interface IWithEvent<TEventArgs>
        where TEventArgs : EventArgs
    {
        public event EventHandler<TEventArgs>? Event;
    }
}
