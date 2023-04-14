using Models.Common;
using Models.Stats;

namespace Models.Services.Battle
{
    public interface IAttacker
    {
        public Pid Pid { get; }

        public Loc Loc { get; }

        public Atk Atk { get; }

        public bool Dead { get; }
    }
}
