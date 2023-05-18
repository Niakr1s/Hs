using HsLib.Interfaces.CardTraits;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Minions
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
