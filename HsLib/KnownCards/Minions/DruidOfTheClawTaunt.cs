using HsLib.Types.Cards;

namespace HsLib.KnownCards.Minions
{
    public class DruidOfTheClawTaunt : Minion
    {
        public DruidOfTheClawTaunt() : base(5, 4, 6)
        {
            Taunt.Set(true);
        }
    }
}
