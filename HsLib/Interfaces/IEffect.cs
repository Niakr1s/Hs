using HsLib.Systems;

namespace HsLib.Interfaces
{
    public interface IEffect
    {
        Action UseEffect(Battlefield bf, ICard target);
    }
}