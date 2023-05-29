using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;
using HsLib.Types.Player;
using HsLib.Types.Stats;

namespace HsLib.Systems
{
    public interface IBoardSide
    {
        Pid Pid { get; }
        PlayerMp Mp { get; } // todo to readonly interface
        IPlayer Player { get; set; } // todo: remove this from interface

        IContainer this[Loc loc] { get; }
        IContainer? GetContainer(ICard card);

        Deck Deck { get; } // todo to readonly interface
        Field Field { get; } // todo to readonly interface
        Hand Hand { get; } // todo to readonly interface
        Secrets Secrets { get; } // todo to readonly interface

        Hero Hero { get; set; } // todo to readonly interface
        Ability Ability { get; set; } // todo to readonly interface
        Weapon? Weapon { get; set; } // todo to readonly interface

        /// <summary>
        /// Can return cards in non-chronogical order.
        /// </summary>
        IEnumerable<ICard> Cards { get; }
    }
}