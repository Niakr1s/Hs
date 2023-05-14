using HsLib.Types.Cards;

namespace HsLib.Cards.Minions
{
    public class DruidOfTheClaw : Minion
    {
        public DruidOfTheClaw() : base(5, 4, 4)
        {
            _chooseOne = new() { CardId.DruidOfTheClawCharge, CardId.DruidOfTheClawTaunt };
        }

        private readonly List<CardId> _chooseOne;

        public override IEnumerable<CardId>? ChoseOne => _chooseOne.AsEnumerable();
    }
}
