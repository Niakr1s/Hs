using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IEffect
    {
        Action UseEffect(Battlefield bf, ICard target);
    }
}