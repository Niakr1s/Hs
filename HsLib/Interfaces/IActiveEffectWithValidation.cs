using HsLib.Systems;
using HsLib.Types;

namespace HsLib.Interfaces
{
    public interface IActiveEffectWithValidation : IActiveEffect
    {
        void ValidateEffectTarget(Battlefield bf, Pid pid, ICard? effectTarget);
    }
}
