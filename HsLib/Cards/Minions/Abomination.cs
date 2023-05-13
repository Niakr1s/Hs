using HsLib.Interfaces;
using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Minions
{
    public class Abomintaion : Minion, IWithDeathrattle
    {
        public Abomintaion() : base(5, 4, 4)
        {
            Targets deathrattleTargets = new Targets
            {
                Locs = Loc.Field | Loc.Hero,
                Sides = PidSide.Me | PidSide.He,
            };
            Deathrattle = new DealDamageEffect(this, false, deathrattleTargets)
            {
                Damage = 2,
            };
        }

        public IEffect Deathrattle { get; }
    }
}
