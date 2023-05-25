using HsLib.Types.Stats;

namespace HsLib.Types.Effects
{
    public interface IDamageEffect : IEffect
    {
        IntStat DamageAmount { get; }
    }
}