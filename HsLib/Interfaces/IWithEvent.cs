namespace HsLib.Interfaces
{
    internal interface IWithEvent<TEventArgs>
        where TEventArgs : EventArgs
    {
        event EventHandler<TEventArgs>? Event;
    }
}
