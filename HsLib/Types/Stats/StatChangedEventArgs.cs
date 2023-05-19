namespace HsLib.Types.Stats
{

    public class StatChangedEventArgs : EventArgs
    {
        public StatChangedEventArgs(StatChangedEventType type)
        {
            Type = type;
        }

        public StatChangedEventType Type { get; }
    }
}
