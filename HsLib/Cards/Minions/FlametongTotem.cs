using HsLib.Interfaces.CardTraits;
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
                new GiveStatAuraEffect<int>(c => ((IWithAtk)c).Atk) { Value = 2 },
                new FieldAdjacentMinionsChooser());
        }
    }
}
