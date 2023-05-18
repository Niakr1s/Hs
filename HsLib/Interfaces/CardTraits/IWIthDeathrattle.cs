using HsLib.Types.Effects.Base;

namespace HsLib.Interfaces.CardTraits
{
    public interface IWithDeathrattle
    {
        DeathrattleEffect? Deathrattle { get; }
    }
}
