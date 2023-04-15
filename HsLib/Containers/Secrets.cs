using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
{
    public class Secrets : MultiContainer<Card>
    {
        public Secrets(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) : base(bf, pid, Loc.Secrets, startCards: cards)
        {
        }
    }
}
