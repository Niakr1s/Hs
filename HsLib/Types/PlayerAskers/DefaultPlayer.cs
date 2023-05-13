using HsLib.Interfaces;
using HsLib.Types.Cards;

namespace HsLib.Types.PlayerAskers
{
    public class DefaultPlayer : IPlayer
    {
        int IPlayer.ChooseCard(IEnumerable<Card> cards)
        {
            return 0;
        }
    }
}
