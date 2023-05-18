using HsLib.Types.Cards;
using HsLib.Types.CardsChoosers;
using HsLib.Types.Effects;
using HsLib.Types.Places;

namespace HsLib.KnownCards.Minions
{
    public class AbusiveSergeant : Minion
    {
        public AbusiveSergeant() : base(2, 2, 1)
        {
            GiveStatBuffEffect<int> effect = new(c => ((IWithAtk)c).Atk) { Value = 2, TillEndOfTurn = true };
            Targets possibleTargets = new() { Locs = Loc.Field, Sides = PidSide.Me | PidSide.He };
            BattlecryEffect = new(effect, possibleTargetsChooser: possibleTargets);
        }
    }
}
