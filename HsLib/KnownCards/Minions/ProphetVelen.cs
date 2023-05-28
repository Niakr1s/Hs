using HsLib.Types.Cards;

namespace HsLib.KnownCards.Minions
{
    public class ProphetVelen : Minion
    {
        public ProphetVelen() : base(7, 7, 7)
        {
            //ProphetVelenCardsChooser chooser = new();
            //ProphetVelenAuraEffect auraEffect = new();

            //AuraSource auraSource = new AuraSource(this, auraEffect, chooser);
            //FieldEffectSources.Add(auraSource);
        }



        //internal class ProphetVelenCardsChooser : IChooser<PlaceInContainer>
        //{
        //    private static readonly Targets _targets = new Targets { Locs = Loc.Ability | Loc.Hand, Sides = PidSide.Me };

        //    public IEnumerable<ICard> ChooseCards(PlaceInContainer owner, IEnumerable<ICard> cards)
        //    {
        //        return _targets.ChooseCards(owner, cards).Where(CardIsValid);
        //    }

        //    private static bool CardIsValid(ICard c) => (c is Ability a && EffectIsValid(a.AbilityEffect.Effect)) ||
        //            (c is Spell s && EffectIsValid(s.SpellEffect.Effect));

        //    private static bool EffectIsValid(IEffect effect) => effect is IDamageEffect or IHealEffect;

        //}

        //internal class ProphetVelenAuraEffect : IAuraEffect
        //{
        //    public IEnchantHandler GiveAura(Battlefield bf, ICard owner, ICard target)
        //    {
        //        IntStat stat = target switch
        //        {
        //            Ability a => TakeIntStat(a.AbilityEffect.Effect),
        //            Spell s => TakeIntStat(s.SpellEffect.Effect),
        //            _ => throw new ValidationException("target is not ability nor spell"),
        //        };

        //        return stat.AddFinalMultiplierAura(2);
        //    }

        //    private static IntStat TakeIntStat(IEffect effect) => effect switch
        //    {
        //        IDamageEffect d => d.DamageAmount,
        //        IHealEffect h => h.HealAmount,
        //        _ => throw new ValidationException("effect is not damage nor heal effect"),
        //    };
        //}
    }

}
