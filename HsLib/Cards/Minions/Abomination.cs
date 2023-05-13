using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Minions
{
    public class Abomintaion : Minion
    {
        public Abomintaion() : base(5, 4, 4)
        {
            Targets deathrattleTargets = new Targets
            {
                Locs = Loc.Field | Loc.Hero,
                Sides = PidSide.Me | PidSide.He,
            };
            Deathrattle = new DealDamageEffect(this, EffectType.Mass, deathrattleTargets)
            {
                Damage = 2,
            };
        }
    }
}
