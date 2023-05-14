using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    // todo: rename to Secrets, add ISecret interface instead of TCard = Card
    public class Secrets : MultiContainer<Card>
    {
        public Secrets(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) : base(bf, new Place(pid, Loc.Secrets), startCards: cards)
        {
        }
    }
}
