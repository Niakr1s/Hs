using HsLib.Cards;
using HsLib.Common.Place;
using HsLib.Events;

namespace HsLib.Battle
{
    public partial class Battlefield
    {
        private class Battlefield_EventConnector
        {
            public Battlefield_EventConnector(Battlefield bf)
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
                foreach (Card card in Bf.Cards)
                {
                    card.AfterContainerInsert(Bf);
                    card.OnTurnStart(Bf);
                }

                Bf.Event += Bf_Event;
            }

            private void Turn_Event(object? sender, TurnEventArgs e)
            {
                switch (e)
                {
                    case TurnStartEventArgs:
                        foreach (Card card in Bf.Cards) card.OnTurnStart(Bf);
                        break;

                    case TurnEndEventArgs:
                        foreach (Card card in Bf.Cards) card.OnTurnEnd(Bf);
                        break;
                }
            }

            private void Bf_Event(object? sender, BattlefieldEventArgs e)
            {
                switch (e.EventArgs)
                {
                    case ContainerCardInsertEventArgs evt:
                        evt.Card.AfterContainerInsert(Bf);
                        break;

                    case ContainerCardRemoveEventArgs evt:
                        evt.Card.AfterContainerRemove(Bf);
                        break;
                }
            }
        }
    }
}
