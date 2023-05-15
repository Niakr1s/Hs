using HsLib.Exceptions;
using HsLib.Interfaces;
using HsLib.Types;

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
        /// Takes next card and puts it at hand if possible. Burn otherwise.
        /// </summary>
        public void TakeNextCard(Pid pid)
        {
            Bf[pid].Deck.TakeNextCard();
        }

        /// <summary>
        /// Plays from hand.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="fieldIndex"></param>
        /// <param name="effectTarget"></param>
        /// <exception cref="ValidationException"></exception>
        /// <returns>Action, that does actual play</returns>
        public Action PlayFromHand(Pid pid, int index, int? fieldIndex = null, ICard? effectTarget = null)
        {
            return Bf[pid].Hand.Play(index, fieldIndex, effectTarget);
        }
    }
}