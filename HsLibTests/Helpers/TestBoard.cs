using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLibTests.Helpers
{
    internal static class TestBoard
    {
        /// <summary>
        /// Creates board for tests.<br/>
        /// Features:<br/>
        /// 1. Immediatly starts.<br/>
        /// 2. Sets Mp to 10<br/>
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        internal static Board New(CardId p1 = CardId.JainaProudmoore, CardId p2 = CardId.GarroshHellscream)
        {
            Board board = new(p1, p2);
            board.Turn.Start();

            board.Player.Mp.Set(10);
            board.Enemy.Mp.Set(10);

            return board;
        }
    }
}
