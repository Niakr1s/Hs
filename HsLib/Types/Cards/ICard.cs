using HsLib.Systems;
using HsLib.Types.Places;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    public interface ICard
    {
        Mp Mp { get; }

        Place Place { get; set; }

        /// <summary>
        /// Called to check, if card should be cleaned by clean service.
        /// </summary>
        bool ShouldBeCleaned();

        void Subscribe(Battlefield bf);
        void Unsubscribe(Battlefield bf, Place previousPlace);

        ICard Clone();
    }
}