using HsLib.Interfaces;

namespace HsLib.Types.CardsChoosers
{
    public class FieldAdjacentMinionsChooser : ICardsChooser<PlaceInContainer>
    {
        // todo fix
        public IEnumerable<ICard> ChooseCards(PlaceInContainer ownerPlace, IEnumerable<ICard> cards)
        {
            return cards;
        }
    }
}
