using HsLib.Cards;
using HsLib.Containers;

namespace HsLib.Cards.Effects
{
    public interface IEffect
    {
        /// <summary>
        /// Uses effect on target.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="target"></param>
        /// <exception cref="EffectWrongTargetException"></exception>
        public void UseEffect(Battlefield bf, Card owner, Card? target);
    }
}
