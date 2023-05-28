using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLib.Types.Choosers
{
    public interface IChooser<TOwner>
    {
        IEnumerable<ICard> ChooseCards(Board board, TOwner owner);
    }
}
