using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLib.Types.Effects.Base;

namespace HsLib.Cards.Minions
{
    public class Abomintaion : Minion
    {
        public Abomintaion() : base(5, 4, 4)
        {
            DealDamageEffect effect = new() { Damage = 2 };
            Targets possibleTargets = new Targets { Locs = Loc.Field | Loc.Hero, Sides = PidSide.Me | PidSide.He, };
            Deathrattle = new ActiveMultiEffect(effect, possibleTargets);
        }
    }
}
