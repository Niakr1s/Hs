namespace Models.Events
{
    public abstract class TurnEventArgs : EventArgs { }

    public class TurnStatusChangedEventArgs : TurnEventArgs { }

    public class TurnEndEventArgs : TurnEventArgs { }

    public class TurnStartEventArgs : TurnEventArgs { }
}
