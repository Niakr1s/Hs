using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
{
    public class SecretC : MultiContainer<Card>
    {
        public SecretC(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) : base(bf, pid, Loc.Secret, startCards: cards)
        {
        }
    }
}
