namespace HsLib.Events
{
    public abstract class TurnEventArgs : EventArgs { }

    public class TurnEndEventArgs : TurnEventArgs { }

    public class TurnStartEventArgs : TurnEventArgs { }
}
