using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Effects.Base
{
    public abstract class ActiveEffectWithValidation : IActiveEffectWithValidation
    {
        protected ActiveEffectWithValidation(IActiveEffect? activeEffect = null)
        {
            ActiveEffect = activeEffect;
        }

        public IActiveEffect? ActiveEffect { get; set; }

        public IEnumerable<ICard> GetPossibleTargets(Battlefield bf, Pid pid)
        {
            if (ActiveEffect is null) yield break;
            foreach (var i in ActiveEffect.GetPossibleTargets(bf, pid)) { yield return i; }
        }

        public Action UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            if (ActiveEffect is null) { return () => { }; }
            return ActiveEffect.UseEffect(bf, pid, target);
        }

        public abstract void ValidateEffectTarget(Battlefield bf, Pid pid, ICard? effectTarget);
    }
}