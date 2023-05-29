using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class HeroContainer : SingleContainer<Hero>
    {
        public HeroContainer(IBoard board, Pid pid, Hero hero) : base(board, new(pid, Loc.Hero), hero)
        {
        }
    }
}