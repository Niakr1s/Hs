using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class AbilityContainer : SingleContainer<Ability>
    {
        public AbilityContainer(IBoard board, Pid pid, Ability ability) : base(board, new(pid, Loc.Ability), ability)
        {
        }
    }
}