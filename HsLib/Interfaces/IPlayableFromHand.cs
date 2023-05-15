using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IPlayableFromHand : ICard
    {
        /// <summary>
        /// For this function it's allowed to throw exceptionsm, if can't be played from hand.
        /// </summary>
        /// <param name="bf"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="effectTarget"></param>
        /// <returns>Action, that actually plays from hand.</returns>
        Action PlayFromHand(Battlefield bf, int? fieldIndex = null, ICard? effectTarget = null);
    }
}
