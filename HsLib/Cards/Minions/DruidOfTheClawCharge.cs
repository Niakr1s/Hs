using HsLib.Types.Cards;

namespace HsLib.Cards.Minions
{
    public class DruidOfTheClawCharge : Minion
    {
        public DruidOfTheClawCharge() : base(5, 4, 4)
        {
            Charge.Set(true);
        }
    }
}
