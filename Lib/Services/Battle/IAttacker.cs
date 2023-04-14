using Models.Common.Place;
using Models.Stats;

namespace Models.Services.Battle
{
    public interface IAttacker : IWithPlace
    {
        public Atk Atk { get; }
    }
}
