using HsLib.Types.Cards;

namespace HsLib.KnownCards.Minions
{
    public class DruidOfTheClawCharge : Minion
    {
        public DruidOfTheClawCharge() : base(5, 4, 4)
        {
            Charge.Set(true);
        }
    }
}
