using HsLib.Types;
using HsLib.Types.Cards;
using HsLib.Types.Containers;

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
            Card? card = Bf[pid].Deck.Pop();
            if (card is null) { return; }

            if (!Bf[pid].Hand.Add(card))
            {
                Bf[pid].Graveyard.Add(card);
            }
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
            Graveyard gy = Bf[pid].Graveyard;

            Card? card = hand[handIndex];
            if (card is Weapon w)
            {
                hand.RemoveAt(handIndex);

                Weapon prevWeapon = weaponC.Card;
                weaponC.Card = w;
                gy.Add(prevWeapon);
            }
            else
            {
                throw new MoveException($"{card} is not weapon");
            }
        }

        /// <summary>
        /// Equips weapon
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="handIndex"></param>
        /// <param name="boardIndex"></param>
        /// <exception cref="MoveException"></exception>
        public void MoveHandToGraveyard(Pid pid, int handIndex)
        {
            Hand hand = Bf[pid].Hand;
            Graveyard gy = Bf[pid].Graveyard;

            Card? card = hand[handIndex];
            if (card is not null)
            {
                hand.RemoveAt(handIndex);
                gy.Add(card);
            }
            else
            {
                throw new MoveException("wrong hand index");
            }
        }

        /// <summary>
        /// Simply move minion to board at last position, or moves to graveyard if no place at field.
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="handIndex"></param>
        /// <exception cref="MoveException"></exception>
        public void MoveHandToField(Pid pid, int handIndex, int? fieldIndex = null, bool fieldOrError = false, bool check = false)
        {
            Hand hand = Bf[pid].Hand;
            Field field = Bf[pid].Field;

            Minion? card = hand[handIndex] as Minion;
            if (card is null) { throw new MoveException($"{card} is not minion"); }

            int index = fieldIndex ?? field.Count;
            if (!field.CanBeInsertedAt(index) && fieldOrError) { throw new MoveException("can't be inserted at field"); }

            if (!check)
            {
                hand.RemoveAt(handIndex);
                if (!field.Insert(index, card))
                {
                    Bf[pid].Graveyard.Add(card);
                }
            }
        }

        public void MoveFieldToGraveyard(Pid pid, int fieldIndex)
        {
            Card? card = Bf[pid].Hand[fieldIndex];
            if (card is Minion m)
            {
                Bf[pid].Hand.RemoveAt(fieldIndex);
                if (!Bf[pid].Field.Add(m))
                {
                    Bf[pid].Graveyard.Add(m);
                }
            }
            else
            {
                throw new MoveException($"{card} is not minion");
            }
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