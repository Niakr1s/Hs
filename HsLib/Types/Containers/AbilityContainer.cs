using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class AbilityContainer : SingleContainer<Ability>
    {
        public AbilityContainer(Battlefield bf, Pid pid, Ability ability) : base(bf, new(pid, Loc.Ability), ability)
        {
        }
    }
}