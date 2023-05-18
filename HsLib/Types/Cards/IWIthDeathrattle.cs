using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public interface IWithDeathrattle
    {
        DeathrattleEffect? DeathrattleEffect { get; }
    }
}
