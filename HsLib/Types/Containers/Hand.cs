using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class Hand : Container<IPlayableFromHand>
    {
        public Hand(IBoard board, Pid pid) : base(board, new Place(pid, Loc.Hand), limit: 10)
        {
        }
    }
}
