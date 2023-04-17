using HsLib.Battle;
using HsLib.Cards.Effects;

namespace HsLib.Cards.KnownCards.Abilities
{
    public class ArmorUp : Ability
    {
        public ArmorUp() : base(2)
        {
        }

        public override void UseEffect(Battlefield bf, Card owner, Card? target)
        {
            if (target is null)
            {
                bf[owner.Pid].Hero.Card.Armor.Increase(2);
            }
            else
            {
                throw new EffectWrongTargetException();
            }
        }
    }
}
