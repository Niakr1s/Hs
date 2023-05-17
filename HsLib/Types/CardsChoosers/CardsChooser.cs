using HsLib.Interfaces;

namespace HsLib.Types.CardsChoosers
{
    public class CardsChooser<TOwner> : ICardsChooser<TOwner>
    {
        public CardsChooser(CardsChooserFunc<TOwner> cardChooserFunc)
        {
            _cardChooserFunc = cardChooserFunc;
        }

        private readonly CardsChooserFunc<TOwner> _cardChooserFunc;

        public IEnumerable<ICard> ChooseCards(TOwner owner, IEnumerable<ICard> cards)
        {
            return _cardChooserFunc.Invoke(owner, cards);
        }
    }
}
