using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Choosers
{
    public class Chooser<TOwner> : IChooser<TOwner>
    {
        public Chooser(ChooserFunc<TOwner> cardChooserFunc)
        {
            _cardChooserFunc = cardChooserFunc;
        }

        private readonly ChooserFunc<TOwner> _cardChooserFunc;

        public IEnumerable<ICard> ChooseCards(Battlefield bf, TOwner owner)
        {
            return _cardChooserFunc.Invoke(bf, owner);
        }
    }
}
