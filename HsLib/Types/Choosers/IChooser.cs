using HsLib.Types.Cards;

namespace HsLib.Types.Choosers
{
    public interface IChooser<TOwner>
    {
        IEnumerable<ICard> ChooseCards(TOwner owner, IEnumerable<ICard> cards);
    }
}
