using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IEffect
    {
        void UseEffect(Battlefield bf, ICard target);
    }
}