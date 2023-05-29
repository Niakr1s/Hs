using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Places;
using HsLib.Types.Player;
using HsLib.Types.Stats;
using System.Collections.Specialized;

namespace HsLib.Systems
{
    public interface IBoardSide : INotifyCollectionChanged
    {
        Pid Pid { get; }
        PlayerMp Mp { get; }
        IPlayer Player { get; set; } // todo: remove this from interface

        IContainer this[Loc loc] { get; }
        IContainer? GetContainer(ICard card);
        bool Remove(ICard card);

        Deck Deck { get; }
        Field Field { get; }
        Hand Hand { get; }
        Secrets Secrets { get; }

        Hero Hero { get; set; }
        Ability Ability { get; set; }
        Weapon? Weapon { get; set; }

        /// <summary>
        /// Can return cards in non-chronogical order.
        /// </summary>
        IEnumerable<ICard> Cards { get; }

    }
}