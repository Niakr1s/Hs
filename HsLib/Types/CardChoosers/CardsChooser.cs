using HsLib.Interfaces;
using HsLib.Types.Cards;

namespace HsLib.Types.CardChoosers
{
    public class CardsChooser : ICardsChooser
    {
        public CardsChooser(ChooseCardFunc cardChooserFunc)
        {
            _cardChooserFunc = cardChooserFunc;
        }

        private readonly ChooseCardFunc _cardChooserFunc;

        public IEnumerable<ICard> ChooseCards(Pid ownerPid, IEnumerable<ICard> cards)
        {
            return _cardChooserFunc.Invoke(ownerPid, cards);
        }
    }
}
