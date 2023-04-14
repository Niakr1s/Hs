using Models.Common.Place;
using Models.Stats;

namespace Models.Services.Battle
{
    public interface IAttacker
    {
        public Pid Pid { get; }

        public Loc Loc { get; }

        public Atk Atk { get; }
    }
}
