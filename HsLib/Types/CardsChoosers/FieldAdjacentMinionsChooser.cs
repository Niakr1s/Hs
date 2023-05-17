using HsLib.Interfaces;

namespace HsLib.Types.CardsChoosers
{
    public class FieldAdjacentMinionsChooser : ICardsChooser
    {
        // todo fix
        public IEnumerable<ICard> ChooseCards(Pid ownerPid, IEnumerable<ICard> cards)
        {
            return cards;
        }
    }
}
