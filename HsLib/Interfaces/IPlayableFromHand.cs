using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IPlayableFromHand : ICard
    {
        void PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null);
    }
}
