using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Containers.Base;

namespace HsLib.Types.Containers
{
    public class HeroContainer : SingleContainer<Hero>
    {
        public HeroContainer(Battlefield bf, Pid pid, Hero card) : base(bf, pid, Loc.Hero, card)
        {
        }
        public override IEnumerable<Card> CleanInactiveCards()
        {
            yield break;
        }
    }
}
