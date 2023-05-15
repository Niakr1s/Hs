using HsLib.Interfaces;
using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.CardChoosers
{
    public class CardChooser : ICardChooser
    {
        public CardChooser(ChooseCardFunc cardChooserFunc)
        {
            _cardChooserFunc = cardChooserFunc;
        }

        private readonly ChooseCardFunc _cardChooserFunc;

        public ICard? ChooseCard(Battlefield bf, Pid pid)
        {
            return _cardChooserFunc.Invoke(bf, pid);
        }
    }
}
