namespace HsLib.Types.Stats
{
    public class HpGotDamageEventArgs : EventArgs
    {
        public HpGotDamageEventArgs(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; }
    }
}
