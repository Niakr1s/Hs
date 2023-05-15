using HsLib.Interfaces;

namespace HsLib.Types.CardChoosers
{
    public delegate IEnumerable<ICard> ChooseCardFunc(Pid ownerPid, IEnumerable<ICard> cards);
}
