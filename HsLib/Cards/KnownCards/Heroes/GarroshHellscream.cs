using HsLib.Cards.KnownCards.Abilities;

namespace HsLib.Cards.KnownCards.Heroes
{
    public class GarroshHellscream : Hero
    {
        public override Ability ProduceAbility() => new ArmorUp();
    }
}
