using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class SecretC : MultiContainer<Card>
    {
        public SecretC(Battlefield bf, Pid pid, IEnumerable<Card>? cards = null) : base(bf, pid, Loc.Secret, startCards: cards)
        {
        }

        public override IEnumerable<Card> CleanInactiveCards()
        {
            yield break;
        }
    }
}
