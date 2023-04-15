using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Containers.Base;

namespace HsLib.Containers
{
    public class HeroC : SingleContainer<Hero>
    {
        public HeroC(Battlefield bf, Pid pid, Hero card) : base(bf, pid, Loc.Hero, card)
        {
        }
    }
}
