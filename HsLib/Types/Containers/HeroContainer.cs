using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class HeroContainer : SingleContainer<Hero>
    {
        public HeroContainer(Battlefield bf, Pid pid, Hero hero) : base(bf, new(pid, Loc.Hero), hero)
        {
        }
    }
}