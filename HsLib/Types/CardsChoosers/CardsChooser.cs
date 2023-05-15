using HsLib.Interfaces;

namespace HsLib.Types.CardsChoosers
{
    public class CardsChooser : ICardsChooser
    {
        public CardsChooser(CardsChooserFunc cardChooserFunc)
        {
            _cardChooserFunc = cardChooserFunc;
        }

        private readonly CardsChooserFunc _cardChooserFunc;

        public IEnumerable<ICard> ChooseCards(Pid ownerPid, IEnumerable<ICard> cards)
        {
            return _cardChooserFunc.Invoke(ownerPid, cards);
        }
    }
}
