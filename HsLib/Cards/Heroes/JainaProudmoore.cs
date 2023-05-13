using HsLib.Cards.Abilities;
using HsLib.Types.Cards;

namespace HsLib.Cards.Heroes
{
    public class JainaProudmoore : Hero
    {
        public override Ability ProduceAbility() => new Fireblast();
    }
}
