using HsLib.Interfaces;

namespace HsLib.Types.CardsChoosers
{
    public class FieldAdjacentMinionsChooser : ICardsChooser<PlaceInContainer>
    {
        public bool ExcludeLeft { get; init; }

        public bool ExcludeRight { get; init; }

        public IEnumerable<ICard> ChooseCards(PlaceInContainer ownerPlace, IEnumerable<ICard> cards)
        {
            foreach (var card in cards.Where(c =>
                    (!ExcludeLeft && c.PlaceInContainer!.IsLeftOf(ownerPlace)) ||
                    (!ExcludeRight && c.PlaceInContainer!.IsRightOf(ownerPlace))))
            {
                yield return card;
            }
        }
    }
}
