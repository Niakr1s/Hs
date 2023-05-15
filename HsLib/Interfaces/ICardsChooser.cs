using HsLib.Types;

namespace HsLib.Interfaces
{
    public interface ICardsChooser
    {
        IEnumerable<ICard> ChooseCards(Pid ownerPid, IEnumerable<ICard> cards);
    }
}
