namespace HsLib.Types.Stats
{
    public class StatIncreasedEventArgs : EventArgs
    {
        public StatIncreasedEventArgs(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; }
    }
}
