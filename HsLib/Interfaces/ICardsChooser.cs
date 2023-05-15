using HsLib.Types;
using HsLib.Types.Cards;

namespace HsLib.Interfaces
{
    public interface ICardsChooser
    {
        IEnumerable<ICard> ChooseCards(Pid ownerPid, IEnumerable<ICard> cards);
    }
}
