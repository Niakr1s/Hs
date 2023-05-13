using HsLib.Interfaces;
using HsLib.Types.Cards;

namespace HsLib.Types.PlayerAskers
{
    public class DefaultPlayer : IPlayer
    {
        CardId IPlayer.ChooseOne(IEnumerable<CardId> cards)
        {
            return cards.First();
        }
    }
}
