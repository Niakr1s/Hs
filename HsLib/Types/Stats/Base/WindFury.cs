namespace HsLib.Types.Stats.Base
{
    public class Windfury : BoolStat
    {
        public Windfury(bool value) : base(value)
        {
        }

        public int AttacksAllowed()
        {
            return Value ? 2 : 1;
        }

        public int AttacksLeft(int attacksDone)
        {
            return AttacksAllowed() - attacksDone;
        }
    }
}