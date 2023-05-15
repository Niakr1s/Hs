using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Events;

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
                Bf.Turn.Event += Turn_Event;
                Bf[Pid.P1].Event += Bf.Invoke;
                Bf[Pid.P2].Event += Bf.Invoke;
                foreach (ICard card in Bf.Cards)
                {
                    card.AfterContainerInsert(Bf);
                    card.OnTurnStart(Bf);
                }
                Bf.BattleService.Event += BattleService_Event;

                Bf.Event += Bf_Event;
            }

            private void BattleService_Event(object? sender, BattleEventArgs e)
            {
                Bf.Invoke(sender, e);
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

            private void Bf_Event(object? sender, BattlefieldEventArgs e)
            {
                switch (e.EventArgs)
                {
                    case ContainerCardInsertEventArgs evt:
                        Bf._cards.Add(evt.Card);
                        evt.Card.AfterContainerInsert(Bf);
                        break;

                    case ContainerCardRemoveEventArgs evt:
                        Bf._cards.Remove(evt.Card);
                        evt.Card.AfterContainerRemove(Bf);
                        break;
                }
            }
        }
    }
}
