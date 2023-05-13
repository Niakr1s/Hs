using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Minions
{
    public class AbusiveSergeant : Minion
    {
        public AbusiveSergeant() : base(2, 2, 1)
        {
            Battlecry = new AbusiveSergeantBattlecry(this);
        }
    }

    file class AbusiveSergeantBattlecry : Battlecry
    {
        public AbusiveSergeantBattlecry(Card owner) : base(owner)
        {
            Effect = new GiveDamageBuffEffect(owner, EffectType.Solo, new() { Locs = Loc.Field, Sides = PidSide.Me | PidSide.He })
            {
                TillEndOfTurn = true
            };
        }

        protected override IEffect Effect { get; }
    }
}
