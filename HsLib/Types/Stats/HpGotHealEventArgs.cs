namespace HsLib.Types.Stats
{
    public class HpGotHealEventArgs : EventArgs
    {
        public HpGotHealEventArgs(int amount)
        {
            Amount = amount;
        }

        public int Amount { get; }
    }
}
