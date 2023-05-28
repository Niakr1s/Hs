using HsLib.Types.Cards;
using System.Collections.Specialized;

namespace HsLib.Systems
{
    public partial class Battlefield
    {
        private void ConnectEvents()
        {
            CollectionChanged += Collection_Event;
        }

        private void Collection_Event(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems is not null)
            {
                foreach (object item in e.NewItems)
                {
                    ICard card = (ICard)item;
                    _cards.Add(card);
                }
            }
            if (e.OldItems is not null)
            {
                foreach (object item in e.OldItems)
                {
                    ICard card = (ICard)item;
                    _cards.Remove(card);
                }
            }
        }
    }
}
