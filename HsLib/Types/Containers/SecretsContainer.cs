using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    // todo: rename to Secrets, add ISecret interface instead of TCard = Card
    public class SecretsContainer : MultiContainer<Card>
    {
        public SecretsContainer(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) : base(bf, new Place(pid, Loc.Secret), startCards: cards)
        {
        }
    }
}
