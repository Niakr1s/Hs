using HsLib.Systems;
using HsLib.Types;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IEffect
    {
        void UseEffect(Battlefield bf, Pid pid, ICard? target);
    }
}