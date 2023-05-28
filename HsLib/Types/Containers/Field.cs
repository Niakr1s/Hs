using HsLib.Systems;
using HsLib.Types.Cards;
using HsLib.Types.Places;

namespace HsLib.Types.Containers
{
    public class Field : Container<Minion>
    {
        public Field(Board board, Pid pid) : base(board, new Place(pid, Loc.Field), limit: 7)
        {
        }

        public bool HasAnyActiveTaunt()
        {
            return this.Any(c => c.Taunt && !c.Stealth);
        }

        protected override bool IsCardActive(Minion card)
        {
            return !card.Dead;
        }
    }
}
