using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface IPlayer
    {
        /// <summary>
        /// Asks player to choose one card. It's garanteed that cards is not empty.
        /// </summary>
        /// <param name="cards"></param>
        /// <returns>Index of chosen card</returns>
        int ChooseCard(IEnumerable<Card> cards);
    }
}
