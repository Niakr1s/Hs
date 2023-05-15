using HsLib.Interfaces;
using HsLib.Types;
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
                // manually connecting each card in battlefield
                foreach (ICard card in Bf.Cards) { card.AfterContainerInsert(Bf); }

                Bf.Turn.Event += Bf.Invoke;
                Bf[Pid.P1].Event += Bf.Invoke;
                Bf[Pid.P2].Event += Bf.Invoke;

                Bf.Event += Bf_Event;
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

                    case (ContainerEventArgs containerEventArgs):
                        Container_Event(sender, containerEventArgs);
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

            private void Container_Event(object? sender, ContainerEventArgs e)
            {
                switch (e)
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
