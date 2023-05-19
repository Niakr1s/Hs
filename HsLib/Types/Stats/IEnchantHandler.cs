namespace HsLib.Types.Stats
{
    public interface IEnchantHandler
    {
        public bool Active { get; }

        public bool Deactivate();
    }
}
