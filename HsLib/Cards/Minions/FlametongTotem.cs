using HsLib.Types.Auras;
using HsLib.Types.Auras.Base;
using HsLib.Types.Cards;
using HsLib.Types.CardsChoosers;

namespace HsLib.Cards.Minions
{
    public class FlametongTotem : Minion
    {
        public FlametongTotem() : base(2, 0, 3)
        {
            AuraSource = new AuraSource(this,
                new GiveAtkAuraEffect() { AtkValue = 2 }, new FieldAdjacentMinionsChooser());
        }
    }
}
