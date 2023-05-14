using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public abstract class Spell : Card, IEffect, IPlayableFromHand
    {
        protected Spell(int mp) : base(mp)
        {
        }

        public abstract EffectType EffectType { get; }

        public bool CanUseEffect(Battlefield bf)
        {
            return Place?.Loc == Loc.Hand;
        }

        public abstract void UseEffect(Battlefield bf, Pid pid, ICard? target);

        public abstract IEnumerable<ICard> UseEffectTargets(Battlefield bf, Pid pid);

        public void PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null)
        {
            bf.BattleService.UseEffect(this, Place!.Pid, effectTarget);
            bf[Place.Pid].Hand.Remove(this);
        }
    }
}
