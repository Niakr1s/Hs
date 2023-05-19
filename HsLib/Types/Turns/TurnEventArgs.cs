namespace HsLib.Types.Turns
{
    public class TurnEventArgs : EventArgs
    {
        public TurnEventArgs(TurnEventType type)
        {
            Type = type;
        }

        public TurnEventType Type { get; }
    }
}
