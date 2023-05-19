﻿using HsLib.Types.Cards;
using HsLib.Types.Turns;
using System.Collections.Specialized;

namespace HsLib.Systems
{
    public partial class Battlefield
    {
        private void ConnectEvents()
        {
            TurnEvent += Turn_Event;
            CollectionChanged += Collection_Event;
        }

        private void Turn_Event(object? sender, TurnEventArgs e)
        {
            switch (e.Type)
            {
                case TurnEventType.Start:
                    foreach (ICard card in Cards) card.OnTurnStart(this);
                    break;

                case TurnEventType.End:
                    foreach (ICard card in Cards) card.OnTurnEnd(this);
                    break;
            }
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
