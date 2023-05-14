using HsLib.Systems;
using HsLib.Types.Cards;

namespace HsLibTests.Helpers
{
    internal static class TestBattlefield
    {
        /// <summary>
        /// Creates battlefield for tests.<br/>
        /// Features:<br/>
        /// 1. Immediatly starts.<br/>
        /// 2. Sets Mp to 10<br/>
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        internal static Battlefield New(CardId p1 = CardId.JainaProudmoore, CardId p2 = CardId.GarroshHellscream)
        {
            Battlefield bf = new(p1, p2);
            bf.Turn.Start();

            bf.Player.Mp.Set(10);
            bf.Enemy.Mp.Set(10);

            return bf;
        }
    }
}
