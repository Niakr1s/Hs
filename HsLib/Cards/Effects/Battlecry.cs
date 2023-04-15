using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers;

namespace HsLib.Cards.Effects
{
    public abstract class Battlecry : IEffect
    {
        protected Target Target { get; set; }

        /// <summary>
        /// It's called after target validity check passes.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="owner"></param>
        /// <param name="target"></param>
        protected abstract void DoUseEffect(Battlefield bf, Card owner, Card? target);

        public void UseEffect(Battlefield bf, Card owner, Card? target)
        {
            if (!Target.IsValidTarget(owner, target))
            {
                // TODO: add stealth checks etc
                throw new EffectWrongTargetException();
            }

            DoUseEffect(bf, owner, target);
        }
    }
}
