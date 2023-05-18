using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class Secrets : Container<Card>
    {
        public Secrets(Battlefield bf, Pid pid, IEnumerable<Card>? startCards = null) :
            base(bf, new Place(pid, Loc.Secrets), startCards: startCards)
        {
        }
    }
}
