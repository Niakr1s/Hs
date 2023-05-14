using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Containers;
using HsLib.Types.Containers.Base;

namespace HsLib.Systems.Services
{
    public class MoveService
    {
        public MoveService(Battlefield bf)
        {
            Bf = bf;
        }

        public Battlefield Bf { get; }

        /// <summary>
        /// Takes card from dack and adds it to hand if possible, otherwise moves it to graveyard.
        /// </summary>
        public void TakeNextCard(Pid pid)
        {
            RemovedCard? card = Bf[pid].Deck.Pop();
            if (card is null) { return; }

            Bf[pid].Hand.Add(card.Card);
        }

        /// <summary>
        /// Equips weapon
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="handIndex"></param>
        /// <param name="boardIndex"></param>
        /// <exception cref="MoveException"></exception>
        public void MoveHandToWeapon(Pid pid, int handIndex)
        {
            Hand hand = Bf[pid].Hand;
            WeaponContainer weaponC = Bf[pid].Weapon;

            ICard? card = hand[handIndex];
            if (card is Weapon w)
            {
                hand.RemoveAt(handIndex);

                Weapon prevWeapon = weaponC.Card;
                weaponC.Card = w;
            }
            else
            {
                throw new MoveException($"{card} is not weapon");
            }
        }

        /// <summary>
        /// Simply move minion to board at last position, or moves to graveyard if no place at field.
        /// </summary>
        /// <param name="handPid"></param>
        /// <param name="handIndex">index at hand to move from</param>
        /// <param name="transformTo">if need minion to be transformed</param>
        /// <param name="fieldIndex">index at field to move to</param>
        /// <param name="fieldOrError">forces exception if field is full</param>
        /// <param name="check">do only check, skip actual movement</param>
        /// <exception cref="MoveException"></exception>
        public void MoveHandToField(Pid handPid, int handIndex,
            Minion? transformTo = null,
            int? fieldIndex = null,
            bool fieldOrError = false,
            bool check = false)
        {
            Hand hand = Bf[handPid].Hand;
            Field field = Bf[handPid].Field;

            Minion? cardInHand = hand[handIndex] as Minion;
            if (cardInHand is null) { throw new MoveException($"{cardInHand} is not minion"); }

            int index = fieldIndex ?? field.Count;
            if (!field.CanBeInsertedAt(index) && fieldOrError) { throw new MoveException("can't be inserted at field"); }

            Minion cardToField = transformTo ?? cardInHand;
            if (!check)
            {
                hand.RemoveAt(handIndex);
                field.Insert(index, cardToField);
            }
        }

        public ICard RemoveCard(PlaceInContainer place)
        {
            BattlefieldPlayer player = Bf[place.Pid];
            return place.Loc switch
            {
                Loc.Deck => player.Deck.RemoveAt(place.Index).Card,
                Loc.Hand => player.Hand.RemoveAt(place.Index).Card,
                Loc.Field => player.Field.RemoveAt(place.Index).Card,
                Loc.Secret => player.Secrets.RemoveAt(place.Index).Card,
                _ => throw new NotSupportedException($"can't remove from {place.Loc}"),
            };
        }
    }

    public class MoveException : Exception
    {
        public MoveException() : base()
        {
        }

        public MoveException(string? message) : base(message)
        {
        }

        public MoveException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}