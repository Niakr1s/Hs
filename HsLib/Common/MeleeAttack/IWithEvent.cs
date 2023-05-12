namespace HsLib.Common.MeleeAttack
{
    internal interface IWithEvent<TEventArgs>
        where TEventArgs : EventArgs
    {
        event EventHandler<TEventArgs>? Event;
    }
}
