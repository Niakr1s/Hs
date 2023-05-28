using HsLib.Systems;
using HsLib.Types.Stats;

namespace HsLib.Types.Cards
{
    public interface IDamageable : ICard
    {
        bool CanBeMeleeAttacked(Board board);

        Hp Hp { get; }
    }
}