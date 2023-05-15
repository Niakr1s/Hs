using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Cards
{
    public abstract class Ability : Card
    {
        protected Ability(int mp) : base(mp)
        {
        }

        public abstract ITargetEffect AbilityEffect { get; }

        public bool EffectUsedThisTurn { get; private set; }

        public void UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            AbilityEffect.UseEffect(bf, pid, target);
            EffectUsedThisTurn = true;
        }

        public override void OnTurnStart(Battlefield bf)
        {
            base.OnTurnStart(bf);
            EffectUsedThisTurn = false;
        }
    }
}
