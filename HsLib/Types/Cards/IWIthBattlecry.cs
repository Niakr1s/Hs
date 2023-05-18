using HsLib.Types.Effects;

namespace HsLib.Types.Cards
{
    public interface IWithBattlecry
    {
        BattlecryEffect? BattlecryEffect { get; }
    }
}
