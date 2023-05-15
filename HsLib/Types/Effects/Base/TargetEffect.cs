using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Effects.Base
{
    public class TargetEffect : ITargetEffect
    {
        public TargetEffect(Card owner, IEffect effect, EffectType type = EffectType.Self, Targets targets = new Targets())
        {
            Owner = owner;
            EffectType = type;
            _effect = effect;
            _targets = targets;
        }

        public Card Owner { get; }

        public EffectType EffectType { get; }

        private readonly Targets _targets;
        private readonly IEffect _effect;

        public virtual IEnumerable<ICard> UseEffectTargets(Battlefield bf, Pid pid)
        {
            return _targets.GetValidTargets(pid, bf.Cards);
        }

        public void UseEffect(Battlefield bf, Pid pid, ICard? target)
        {
            switch (EffectType)
            {
                case EffectType.Self:
                    _effect.UseEffect(bf, pid, null);
                    break;

                case EffectType.Solo:
                    _effect.UseEffect(bf, pid, target);
                    break;

                case EffectType.Mass:
                    foreach (var card in UseEffectTargets(bf, pid))
                    {
                        _effect.UseEffect(bf, pid, card);
                    }
                    break;
            }
        }
    }
}
