using HsLib.Interfaces;
using HsLib.Systems;

namespace HsLib.Types.Effects.Base
{
    public abstract class PlayableFromHandEffect : IPlayableFromHandEffect
    {
        protected PlayableFromHandEffect(IActiveEffect<Pid>? pidEffect = null)
        {
            Effect = pidEffect;
        }

        public IActiveEffect<Pid>? Effect { get; protected set; }

        public IEnumerable<ICard> GetPossibleTargets(Battlefield bf, Pid ownerPid)
        {
            if (Effect is null) yield break;
            foreach (var i in Effect.GetPossibleTargets(bf, ownerPid)) { yield return i; }
        }

        public Action UseEffect(Battlefield bf, Pid ownerPid, ICard? target)
        {
            if (Effect is null) { return () => { }; }
            return Effect.UseEffect(bf, ownerPid, target);
        }

        public abstract void ValidatePlayFromHandEffectTarget(Battlefield bf, Pid ownerPid, ICard? effectTarget);
    }
}