using HsLib.Systems;

namespace HsLib.Types.Cards
{
    public interface IPlayableFromHand : ICard
    {
        /// <summary>
        /// For this function it's allowed to throw exceptionsm, if can't be played from hand.
        /// </summary>
        /// <param name="board"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="effectTarget"></param>
        /// <returns>Action, that actually plays from hand.</returns>
        Action PlayFromHand(IBoard board, int? fieldIndex = null, ICard? effectTarget = null);
    }
}
