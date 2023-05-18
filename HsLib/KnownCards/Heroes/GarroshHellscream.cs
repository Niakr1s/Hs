using HsLib.KnownCards.Abilities;
using HsLib.Types.Cards;

namespace HsLib.KnownCards.Heroes
{
    public class GarroshHellscream : Hero
    {
        public override Ability ProduceAbility() => new ArmorUp();
    }
}
