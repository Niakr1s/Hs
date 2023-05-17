using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLib.Types.Effects.Base;

namespace HsLib.Cards.Minions
{
    public class AbusiveSergeant : Minion
    {
        public AbusiveSergeant() : base(2, 2, 1)
        {
            GiveAtkBuffEffect effect = new() { AtkValue = 2, TillEndOfTurn = true };
            Targets possibleTargets = new() { Locs = Loc.Field, Sides = PidSide.Me | PidSide.He };
            Battlecry.Effect = new ActiveEffect<Pid>(effect, possibleTargetsChooser: possibleTargets);
        }
    }
}
