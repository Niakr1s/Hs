using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Minions
{
    public class AbusiveSergeant : Minion
    {
        public AbusiveSergeant() : base(2, 2, 1)
        {
            Battlecry = new GiveDamageBuffEffect(this, EffectType.Solo, new() { Locs = Loc.Field, Sides = PidSide.Me | PidSide.He })
            {
                TillEndOfTurn = true
            };
        }
    }
}
