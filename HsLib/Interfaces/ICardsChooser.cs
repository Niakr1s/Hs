using HsLib.Types;

namespace HsLib.Interfaces
{
    public interface ICardsChooser
    {
        // todo ICard instead of ownerPid
        IEnumerable<ICard> ChooseCards(Pid ownerPid, IEnumerable<ICard> cards);
    }
}
