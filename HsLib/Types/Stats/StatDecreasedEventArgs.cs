namespace HsLib.Types.Stats
{
    public class StatDecreasedEventArgs : EventArgs
    {
        public StatDecreasedEventArgs(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; }
    }
}
