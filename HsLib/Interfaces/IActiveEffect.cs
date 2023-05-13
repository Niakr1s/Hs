using HsLib.Systems;

namespace HsLib.Interfaces
{
    public interface IActiveEffect : IEffect
    {
        /// <summary>
        /// Shows if effect can be used physically.
        /// <br/>Examples: Ability should return false, if it was already played;
        /// MindControl spell should return false, if it's side's field is full.
        /// </summary>
        /// <param name="bf"></param>
        /// <returns></returns>
        public bool CanUseEffect(Battlefield bf);
    }
}
