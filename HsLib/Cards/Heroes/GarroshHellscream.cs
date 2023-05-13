using HsLib.Cards.Abilities;
using HsLib.Types.Cards;

namespace HsLib.Cards.Heroes
{
    public class GarroshHellscream : Hero
    {
        public override Ability ProduceAbility() => new ArmorUp();
    }
}
