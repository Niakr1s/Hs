using HsLib.Interfaces;
using HsLib.Types.Cards;
using HsLib.Types.Effects;
using HsLib.Types.Effects.Base;

namespace HsLib.Cards.Abilities
{
    public class ArmorUp : Ability
    {
        public ArmorUp() : base(2)
        {
            GetArmorEffect effect = new() { Armor = 2 };
            Effect = new TargetEffect(this, effect);
        }

        protected override ITargetEffect Effect { get; }
    }
}
