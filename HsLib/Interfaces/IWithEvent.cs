namespace HsLib.Interfaces
{
    public interface IWithEvent<TEventArgs>
        where TEventArgs : EventArgs
    {
        event EventHandler<TEventArgs>? Event;
    }
}
