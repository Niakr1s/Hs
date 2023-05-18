using HsLib.Types.Cards;
using HsLib.Types.Events;
using HsLib.Types.Places;
using System.Collections.Specialized;

namespace HsLib.Systems
{
    public partial class Battlefield
    {
        private class EventConnector
        {
            public EventConnector(Battlefield bf)
            {
                Bf = bf;
            }

            public Battlefield Bf { get; }

            public void Connect()
            {
                Bf.Turn.Event += Bf.Invoke;
                Bf[Pid.P1].CollectionChanged += Collection_Event;
                Bf[Pid.P2].CollectionChanged += Collection_Event;

                Bf.Event += Bf_Event;
                Bf.CollectionChanged += Bf_Collection_Event;
            }

            private void Bf_Event(object? sender, BattlefieldEventArgs e)
            {
                switch (e.EventArgs)
                {
                    case (BattleEventArgs battleEventArgs):
                        Battle_Event(sender, battleEventArgs);
                        break;

                    case (TurnEventArgs turnEventArgs):
                        Turn_Event(sender, turnEventArgs);
                        break;
                }
            }

            private void Battle_Event(object? sender, BattleEventArgs e)
            {
                foreach (ICard card in Bf.Cards)
                {
                    switch (e)
                    {
                        case (BattleGotDamageEventArgs gotDamageEventArgs):
                            card.OnGotDamage(Bf, gotDamageEventArgs);
                            break;

                        case (BattleMeleePreAttackEventArgs meleePreAttackEventArgs):
                            card.OnPreAttack(Bf, meleePreAttackEventArgs);
                            break;
                    }
                }
            }

            private void Turn_Event(object? sender, TurnEventArgs e)
            {
                switch (e)
                {
                    case TurnStartEventArgs:
                        foreach (ICard card in Bf.Cards) card.OnTurnStart(Bf);
                        break;

                    case TurnEndEventArgs:
                        foreach (ICard card in Bf.Cards) card.OnTurnEnd(Bf);
                        break;
                }
            }


            private void Collection_Event(object? sender, NotifyCollectionChangedEventArgs e)
            {
                Bf.CollectionChanged?.Invoke(sender, e);
            }

            private void Bf_Collection_Event(object? sender, NotifyCollectionChangedEventArgs e)
            {
                if (e.NewItems is not null)
                {
                    foreach (object item in e.NewItems)
                    {
                        ICard card = (ICard)item;
                        Bf._cards.Add(card);
                    }
                }
                if (e.OldItems is not null)
                {
                    foreach (object item in e.OldItems)
                    {
                        ICard card = (ICard)item;
                        Bf._cards.Remove(card);
                    }
                }
            }
        }
    }
}
