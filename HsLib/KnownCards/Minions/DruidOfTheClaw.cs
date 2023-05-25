using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.KnownCards.Minions
{
    public class DruidOfTheClaw : Minion
    {
        private readonly List<CardId> _chooseIds = new() { CardId.DruidOfTheClawCharge, CardId.DruidOfTheClawTaunt };

        public DruidOfTheClaw() : base(5, 4, 4)
        {
            ChooseOneEffect effect = new(_chooseIds);
            BattlecryEffect = new(this, effect);
        }
    }
}
