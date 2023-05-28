using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class Secrets : Container<Card>
    {
        public Secrets(Board board, Pid pid, IEnumerable<Card>? startCards = null) :
            base(board, new Place(pid, Loc.Secrets), startCards: startCards)
        {
        }
    }
}
