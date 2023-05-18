using HsLib.KnownCards.Abilities;
using HsLib.Types.Cards;

namespace HsLib.KnownCards.Heroes
{
    public class JainaProudmoore : Hero
    {
        public override Ability ProduceAbility() => new Fireblast();
    }
}
