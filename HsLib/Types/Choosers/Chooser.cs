using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Choosers
{
    public class Chooser : IChooser
    {
        public Chooser(ChooserFunc cardChooserFunc)
        {
            _cardChooserFunc = cardChooserFunc;
        }

        private readonly ChooserFunc _cardChooserFunc;

        public IEnumerable<ICard> ChooseCards(Battlefield bf, ICard owner, IEnumerable<ICard> cards)
        {
            return _cardChooserFunc.Invoke(bf, owner, cards);
        }
    }
}
