using HsLib.Types.Cards;

namespace HsLib.Cards.Minions
{
    public class DruidOfTheClawTaunt : Minion
    {
        public DruidOfTheClawTaunt() : base(5, 4, 6)
        {
            Taunt.Set(true);
        }
    }
}
