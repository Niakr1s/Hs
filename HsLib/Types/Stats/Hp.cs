using HsLib.Types.Cards;

namespace HsLib.Types.Stats
{
    public class Hp : IntStat, IMortal
    {
        public Hp(int value) : base(value)
        {
        }

        private bool _forceDead;
        public bool Dead
        {
            get => Value <= 0 || _forceDead;
            set => _forceDead = value;
        }
    }
}
