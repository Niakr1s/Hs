using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.CardsChoosers;
using HsLib.Types.Effects;
using HsLib.Types.Effects.Base;

namespace HsLib.Cards.Abilities
{
    public class ArmorUp : Ability
    {
        public ArmorUp() : base(2)
        {
            GetArmorEffect effect = new() { Armor = 2 };
            AbilityEffect = new(new ActiveEffect<Pid>(effect, targetsChooser: new HeroChooser()));
        }

        public override AbilityEffect AbilityEffect { get; }
    }
}
