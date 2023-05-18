using HsLib.Types.Cards;

namespace HsLib.Types.CardsChoosers
{
    public interface ICardsChooser<TOwner>
    {
        IEnumerable<ICard> ChooseCards(TOwner owner, IEnumerable<ICard> cards);
    }
}
