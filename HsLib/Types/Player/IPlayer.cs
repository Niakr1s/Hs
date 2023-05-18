using HsLib.Types.Cards;

namespace HsLib.Types.Player
{
    public interface IPlayer
    {
        /// <summary>
        /// Asks player to choose one card. It's garanteed that cards is not empty.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns>Index of chosen card</returns>
        CardId ChooseOne(IEnumerable<CardId> cards);
    }
}
