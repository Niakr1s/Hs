using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;

namespace HsLib.Cards.Abilities
{
    public class ArmorUp : Ability
    {
        public ArmorUp() : base(2)
        {
            EffectTargets = null;
        }

        public override bool EffectMustHaveTarget => false;

        protected override void DoUseEffect(Battlefield bf, Card? target)
        {
            if (Place is null) { return; }
            bf[Place.Pid].Hero.Card.Armor.Increase(2);
        }
    }
}
