namespace Models.Events
{
    public abstract class TurnEventArgs : EventArgs { }

    public class TurnEndEventArgs : TurnEventArgs { }

    public class TurnStartEventArgs : TurnEventArgs { }
}
