using HsLib.Interfaces;
using HsLib.Types.Cards;
using HsLib.Types.Effects;

namespace HsLib.Cards.Abilities
{
    public class ArmorUp : Ability
    {
        public ArmorUp() : base(2)
        {
            Effect = new GetArmorEffect(this)
            {
                Armor = 2
            };
        }

        protected override IEffect Effect { get; }
    }
}
