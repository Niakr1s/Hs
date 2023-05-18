using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Choosers
{
    public class FieldAdjacentMinionsChooser : IChooser<PlaceInContainer>
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
