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
        IContainer this[Loc loc] { get; }

        Ability Ability { get; set; }
        IEnumerable<ICard> Cards { get; }
        Deck Deck { get; }
        Field Field { get; }
        Hand Hand { get; }
        Hero Hero { get; set; }
        PlayerMp Mp { get; }
        Pid Pid { get; }
        Secrets Secrets { get; }
        Weapon? Weapon { get; set; }
        IPlayer Player { get; set; }

        IContainer? GetContainer(ICard card);
        bool Remove(ICard card);
    }
}