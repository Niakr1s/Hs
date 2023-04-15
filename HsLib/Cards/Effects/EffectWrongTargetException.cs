namespace HsLib.Cards.Effects
{
    public class EffectWrongTargetException : Exception
    {
        public EffectWrongTargetException()
        {
        }

        public EffectWrongTargetException(string? message) : base(message)
        {
        }

        public EffectWrongTargetException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
